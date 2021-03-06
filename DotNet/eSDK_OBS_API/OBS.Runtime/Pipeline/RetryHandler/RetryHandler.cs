﻿/*
 * Copyright 2010-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

using OBS.Runtime.Internal.Util;
using System;
using System.Diagnostics;
using System.Net;

namespace OBS.Runtime.Internal
{
    /// <summary>
    /// The retry handler has the generic logic for retrying requests.
    /// It uses a retry policy which specifies when 
    /// a retry should be performed.
    /// </summary>
    public class RetryHandler : PipelineHandler
    {
        private ILogger _logger;

        /// <summary>
        /// The logger used to log messages.
        /// </summary>
        public override ILogger Logger
        {
            get { return _logger; }
            set
            {
                _logger = value;
                this.RetryPolicy.Logger = value;
            }
        }

        /// <summary>
        /// The retry policy which specifies when 
        /// a retry should be performed.
        /// </summary>
        public RetryPolicy RetryPolicy { get; private set; }
        
        /// <summary>
        /// Constructor which takes in a retry policy.
        /// </summary>
        /// <param name="retryPolicy">Retry Policy</param>
        public RetryHandler(RetryPolicy retryPolicy)
        {
            this.RetryPolicy = retryPolicy;
        }

        /// <summary>
        /// Invokes the inner handler and performs a retry, if required as per the
        /// retry policy.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        public override void InvokeSync(IExecutionContext executionContext)
        {
            var requestContext = executionContext.RequestContext;
            var responseContext = executionContext.ResponseContext;
            bool shouldRetry = false;
            do
            {                
                try
                {
                    base.InvokeSync(executionContext);
                    return;
                }
                catch (Exception exception)
                {
                    shouldRetry = this.RetryPolicy.Retry(executionContext, exception);
                    if (!shouldRetry)
                    {
                        LogForError(requestContext, exception);
                        throw;
                    }
                    else
                    {
                        requestContext.Retries++;
                        requestContext.Metrics.SetCounter(Metric.AttemptCount, requestContext.Retries);
                        LogForRetry(requestContext, exception);                        
                    }
                }

                PrepareForRetry(requestContext);

                using (requestContext.Metrics.StartEvent(Metric.RetryPauseTime))
                    this.RetryPolicy.WaitBeforeRetry(executionContext);

            } while (shouldRetry);
        }

#if BCL45 || WIN_RT || WINDOWS_PHONE 

        /// <summary>
        /// Invokes the inner handler and performs a retry, if required as per the
        /// retry policy.
        /// </summary>
        /// <typeparam name="T">The response type for the current request.</typeparam>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            var requestContext = executionContext.RequestContext;
            var responseContext = executionContext.ResponseContext;
            bool shouldRetry = false;
            do
            {
                try
                {
                    return await base.InvokeAsync<T>(executionContext).ConfigureAwait(false);
                }
                catch (Exception exception)
                {
                    shouldRetry = this.RetryPolicy.Retry(executionContext, exception);
                    if (!shouldRetry)
                    {
                        LogForError(requestContext, exception);
                        throw;
                    }
                    else
                    {
                        requestContext.Retries++;
                        requestContext.Metrics.SetCounter(Metric.AttemptCount, requestContext.Retries);
                        LogForRetry(requestContext, exception);
                    }
                }

                PrepareForRetry(requestContext);

                using (requestContext.Metrics.StartEvent(Metric.RetryPauseTime))
                    this.RetryPolicy.WaitBeforeRetry(executionContext);

            } while (shouldRetry);
            throw new ObsClientException("Neither a response was returned nor an exception was thrown in the Runtime RetryHandler.");
        }

#endif

#if BCL && !BCL45 

        /// <summary>
        /// Invokes the inner handler and performs a retry, if required as per the
        /// retry policy.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        protected override void InvokeAsyncCallback(IAsyncExecutionContext executionContext)
        {
            var requestContext = executionContext.RequestContext;
            var responseContext = executionContext.ResponseContext;
            var exception = responseContext.AsyncResult.Exception;
            
            if (exception != null)
            {
                var syncExecutionContext = ExecutionContext.CreateFromAsyncContext(executionContext);
                var shouldRetry = this.RetryPolicy.Retry(syncExecutionContext, exception);
                if (shouldRetry)
                {
                    requestContext.Retries++;
                    requestContext.Metrics.SetCounter(Metric.AttemptCount, requestContext.Retries);
                    LogForRetry(requestContext, exception);   

                    PrepareForRetry(requestContext);

                    using (requestContext.Metrics.StartEvent(Metric.RetryPauseTime))
                        this.RetryPolicy.WaitBeforeRetry(syncExecutionContext);

                    // Retry by calling InvokeAsync
                    this.InvokeAsync(executionContext);
                    return;
                }
                else
                {
                    LogForError(requestContext, exception);
                }
            }

            // Call outer handler
            base.InvokeAsyncCallback(executionContext);
        }
#endif

        /// <summary>
        /// Prepares the request for retry.
        /// </summary>
        /// <param name="requestContext">Request context containing the state of the request.</param>
        internal static void PrepareForRetry(IRequestContext requestContext)
        {
            if (requestContext.Request.ContentStream != null &&
                requestContext.Request.OriginalStreamPosition >= 0)
            {
                var stream = requestContext.Request.ContentStream;

                // If the stream is wrapped in a HashStream, reset the HashStream
                var hashStream = stream as HashStream;
                if (hashStream != null)
                {
                    hashStream.Reset();
                    stream = hashStream.GetSeekableBaseStream();
                }
                stream.Position = requestContext.Request.OriginalStreamPosition;
            }
        }
        
        private void LogForRetry(IRequestContext requestContext, Exception exception)
        {
            var webException = exception as WebException;
            if (webException != null)
            {
                Logger.InfoFormat("WebException ({1}) making request {2} to {3}. Attempting retry {4} of {5}.",
                          webException.Status,
                          requestContext.RequestName,
                          requestContext.Request.Endpoint.ToString(),
                          requestContext.Retries,
                          this.RetryPolicy.MaxRetries);
            }
            else
            {
                Logger.InfoFormat("{0} making request {1} to {2}. Attempting retry {3} of {4}.",
                          exception.GetType().Name,
                          requestContext.RequestName,
                          requestContext.Request.Endpoint.ToString(),
                          requestContext.Retries,
                          this.RetryPolicy.MaxRetries);
            }
        }

        private void LogForError(IRequestContext requestContext, Exception exception)
        {
            Logger.Error(exception, "{0} making request {1} to {2}. Attempt {3}.",
                          exception.GetType().Name,
                          requestContext.RequestName,
                          requestContext.Request.Endpoint.ToString(),
                          requestContext.Retries + 1);
        }        
    }
}
