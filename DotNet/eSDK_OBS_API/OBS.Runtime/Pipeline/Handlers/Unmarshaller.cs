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

using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;

namespace OBS.Runtime.Internal
{
    /// <summary>
    /// This handler unmarshalls the HTTP response.
    /// </summary>
    public class Unmarshaller : PipelineHandler
    {
        private bool _supportsResponseLogging;

        /// <summary>
        /// The constructor for Unmarshaller.
        /// </summary>
        /// <param name="supportsResponseLogging">
        /// Boolean value which indicated if the unmarshaller 
        /// handler supports response logging.
        /// </param>
        public Unmarshaller(bool supportsResponseLogging)
        {
            _supportsResponseLogging = supportsResponseLogging;
        }

        /// <summary>
        /// Unmarshalls the response returned by the HttpHandler.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        public override void InvokeSync(IExecutionContext executionContext)
        {
            base.InvokeSync(executionContext);

            if (executionContext.ResponseContext.HttpResponse.IsSuccessStatusCode)
            {
                // Unmarshall the http response.
                Unmarshall(executionContext);  
            }                      
        }

#if BCL45

        /// <summary>
        /// Unmarshalls the response returned by the HttpHandler.
        /// </summary>
        /// <typeparam name="T">The response type for the current request.</typeparam>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            await base.InvokeAsync<T>(executionContext).ConfigureAwait(false);
            // Unmarshall the response
            Unmarshall(executionContext);            
            return (T)executionContext.ResponseContext.Response;
        }

#elif WIN_RT || WINDOWS_PHONE 

        /// <summary>
        /// Unmarshalls the response returned by the HttpHandler.
        /// </summary>
        /// <typeparam name="T">The response type for the current request.</typeparam>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            await base.InvokeAsync<T>(executionContext).ConfigureAwait(false);
            // Unmarshall the response
            await UnmarshallAsync(executionContext).ConfigureAwait(false);            
            return (T)executionContext.ResponseContext.Response;
        }

#elif BCL && !BCL45

        /// <summary>
        /// Unmarshalls the response returned by the HttpHandler.
        /// </summary>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        protected override void InvokeAsyncCallback(IAsyncExecutionContext executionContext)
        {
            // Unmarshall the response if an exception hasn't occured
            if (executionContext.ResponseContext.AsyncResult.Exception == null)
            {
                Unmarshall(ExecutionContext.CreateFromAsyncContext(executionContext));
            }            
            base.InvokeAsyncCallback(executionContext);
        }
#endif

        /// <summary>
        /// Unmarshalls the HTTP response.
        /// </summary>
        /// <param name="executionContext">
        /// The execution context, it contains the request and response context.
        /// </param>
        private void Unmarshall(IExecutionContext executionContext)
        {
            var requestContext = executionContext.RequestContext;
            var responseContext = executionContext.ResponseContext;

            using (requestContext.Metrics.StartEvent(Metric.ResponseProcessingTime))
            {
                var unmarshaller = requestContext.Unmarshaller;
                try
                {
                    var readEntireResponse = _supportsResponseLogging &&
                        (requestContext.ClientConfig.LogResponse || requestContext.ClientConfig.ReadEntireResponse
                        || ObsConfigs.LoggingConfig.LogResponses != ResponseLoggingOption.Never);

                    var context = unmarshaller.CreateContext(responseContext.HttpResponse,
                        readEntireResponse,
                        responseContext.HttpResponse.ResponseBody.OpenResponse(),
                        requestContext.Metrics);

                    var response = UnmarshallResponse(context, requestContext);
                    responseContext.Response = response;                    
                }
                finally
                {
                    if (!unmarshaller.HasStreamingProperty)
                        responseContext.HttpResponse.ResponseBody.Dispose();                 
                }
            }
        }

#if WIN_RT || WINDOWS_PHONE

        /// <summary>
        /// Unmarshalls the HTTP response.
        /// </summary>
        /// <param name="executionContext">
        /// The execution context, it contains the request and response context.
        /// </param>
        private async System.Threading.Tasks.Task UnmarshallAsync(IExecutionContext executionContext)
        {
            var requestContext = executionContext.RequestContext;
            var responseContext = executionContext.ResponseContext;

            using (requestContext.Metrics.StartEvent(Metric.ResponseProcessingTime))
            {
                var unmarshaller = requestContext.Unmarshaller;
                try
                {
                    var readEntireResponse = _supportsResponseLogging &&
                        (requestContext.ClientConfig.LogResponse || requestContext.ClientConfig.ReadEntireResponse
                        || AWSConfigs.LoggingConfig.LogResponses != ResponseLoggingOption.Never);

                    var responseStream = await responseContext.HttpResponse.
                        ResponseBody.OpenResponseAsync().ConfigureAwait(false);
                    var context = unmarshaller.CreateContext(responseContext.HttpResponse,
                        readEntireResponse,
                        responseStream,
                        requestContext.Metrics);

                    var response = UnmarshallResponse(context, requestContext);
                    responseContext.Response = response;
                }
                finally
                {
                    if (!unmarshaller.HasStreamingProperty)
                        responseContext.HttpResponse.ResponseBody.Dispose();
                }
            }
        }
#endif

        private ObsWebServiceResponse UnmarshallResponse(UnmarshallerContext context,
            IRequestContext requestContext)
        {
            var unmarshaller = requestContext.Unmarshaller;
            ObsWebServiceResponse response = null;
            using (requestContext.Metrics.StartEvent(Metric.ResponseUnmarshallTime))
            {
                response = unmarshaller.UnmarshallResponse(context);
            }

            requestContext.Metrics.AddProperty(Metric.StatusCode, response.HttpStatusCode);
            requestContext.Metrics.AddProperty(Metric.BytesProcessed, response.ContentLength);
            if (response.ResponseMetadata != null)
            {
                requestContext.Metrics.AddProperty(Metric.AWSRequestID, response.ResponseMetadata.RequestId);
            }

            var logResponseBody = _supportsResponseLogging && (requestContext.ClientConfig.LogResponse ||
                ObsConfigs.LoggingConfig.LogResponses == ResponseLoggingOption.Always);

            if (logResponseBody)
            {
                this.Logger.DebugFormat("Received response: [{0}]", context.ResponseBody);
            }

            context.ValidateCRC32IfAvailable();
            return response;
        }
    }
}
