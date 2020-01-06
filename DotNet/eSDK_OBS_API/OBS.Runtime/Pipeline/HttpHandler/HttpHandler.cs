using eSDK_OBS_API.OBS.Util; 
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
using OBS.Util;
using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.IO;
using System.Net;

namespace OBS.Runtime.Internal
{
    /// <summary>
    /// The HTTP handler contains common logic for issuing an HTTP request that is 
    /// independent of the underlying HTTP infrastructure.
    /// </summary>
    /// <typeparam name="TRequestContent"></typeparam>
    public class HttpHandler<TRequestContent> : PipelineHandler, IDisposable
    {
        private bool _disposed;
        private IHttpRequestFactory<TRequestContent> _requestFactory;
        /// <summary>
        /// The sender parameter used in any events raised by this handler.
        /// </summary>
        public object CallbackSender { get; private set; }

        /// <summary>
        /// The constructor for HttpHandler.
        /// </summary>
        /// <param name="requestFactory">The request factory used to create HTTP Requests.</param>
        /// <param name="callbackSender">The sender parameter used in any events raised by this handler.</param>
        public HttpHandler(IHttpRequestFactory<TRequestContent> requestFactory, object callbackSender)
        {
            _requestFactory = requestFactory;
            this.CallbackSender = callbackSender;
        }

        /// <summary>
        /// Issues an HTTP request for the current request context.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        public override void InvokeSync(IExecutionContext executionContext)
        {
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                SetMetrics(executionContext.RequestContext);
                IRequest wrappedRequest = executionContext.RequestContext.Request;
                httpRequest = CreateWebRequest(executionContext.RequestContext);
                httpRequest.SetRequestHeaders(wrappedRequest.Headers);
                                
                using (executionContext.RequestContext.Metrics.StartEvent(Metric.HttpRequestTime))
                {
                    // Send request body if present.
                    if (wrappedRequest.HasRequestBody())
                    {
                        var requestContent = httpRequest.GetRequestContent();

                        WriteContentToRequestBody(requestContent, httpRequest, executionContext.RequestContext);
                    }

                    executionContext.ResponseContext.HttpResponse = httpRequest.GetResponse();                    
                }
            }
            finally
            {
                if (httpRequest != null)
                    httpRequest.Dispose();
            }
        }

#if BCL45 || WIN_RT || WINDOWS_PHONE 

        /// <summary>
        /// Issues an HTTP request for the current request context.
        /// </summary>
        /// <typeparam name="T">The response type for the current request.</typeparam>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                SetMetrics(executionContext.RequestContext);
                IRequest wrappedRequest = executionContext.RequestContext.Request;
                httpRequest = CreateWebRequest(executionContext.RequestContext);
                httpRequest.SetRequestHeaders(wrappedRequest.Headers);
                
                using(executionContext.RequestContext.Metrics.StartEvent(Metric.HttpRequestTime))
                {
                    // Send request body if present.
                    if (wrappedRequest.HasRequestBody())
                    {
                        var requestContent = await httpRequest.GetRequestContentAsync().ConfigureAwait(false);
                        WriteContentToRequestBody(requestContent, httpRequest, executionContext.RequestContext);
                    }
                
                    var response = await httpRequest.GetResponseAsync(executionContext.RequestContext.CancellationToken).
                        ConfigureAwait(false);                
                    executionContext.ResponseContext.HttpResponse = response;     
                }
                // The response is not unmarshalled yet.
                return null;
            }            
            finally
            {
                if (httpRequest != null)
                    httpRequest.Dispose();
            }
        }

#elif BCL && !BCL45

        /// <summary>
        /// Issues an HTTP request for the current request context.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <returns>IAsyncResult which represent an async operation.</returns>
        public override IAsyncResult InvokeAsync(IAsyncExecutionContext executionContext)
        {
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                SetMetrics(executionContext.RequestContext);
                httpRequest = CreateWebRequest(executionContext.RequestContext);
                executionContext.RuntimeState = httpRequest;

                IRequest wrappedRequest = executionContext.RequestContext.Request;
                if (executionContext.RequestContext.Retries == 0)
                {
                    // First call, initialize an async result.
                    executionContext.ResponseContext.AsyncResult =
                        new RuntimeAsyncResult(executionContext.RequestContext.Callback, 
                            executionContext.RequestContext.State);                    
                }

                // Set request headers
                httpRequest.SetRequestHeaders(executionContext.RequestContext.Request.Headers);

                executionContext.RequestContext.Metrics.StartEvent(Metric.HttpRequestTime);
                if (wrappedRequest.HasRequestBody())
                {
                    // Send request body if present.
                    httpRequest.BeginGetRequestContent(new AsyncCallback(GetRequestStreamCallback), executionContext);
                }
                else
                {
                    
                    // Get response if there is no response body to send.
                    httpRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), executionContext);
                }
                return executionContext.ResponseContext.AsyncResult;
            }
            catch (Exception exception)
            {
                if (executionContext.ResponseContext.AsyncResult != null)
                {
                    // An exception will be thrown back to the calling code.
                    // Dispose AsyncResult as it will not be used further.
                    executionContext.ResponseContext.AsyncResult.Dispose();
                    executionContext.ResponseContext.AsyncResult = null;
                }

                if (httpRequest != null)
                {                    
                    httpRequest.Dispose();
                }

                // Log this exception as it will not be caught by ErrorHandler.
                this.Logger.Error(exception, "An exception occured while initiating an asynchronous HTTP request.");
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("An exception occured while initiating an asynchronous HTTP request."));
                throw;
            }
        }

        private void GetRequestStreamCallback(IAsyncResult result)
        {
            IAsyncExecutionContext executionContext = null;
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                executionContext = result.AsyncState as IAsyncExecutionContext;
                if (executionContext == null)
                {
                    return;
                }

                httpRequest = executionContext.RuntimeState as IHttpRequest<TRequestContent>;
                if (httpRequest == null)
                {
                    return;
                }

                var requestContent = httpRequest.EndGetRequestContent(result);
                WriteContentToRequestBody(requestContent, httpRequest, executionContext.RequestContext);
                httpRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), executionContext);
            }
            catch(Exception exception)
            {   
                httpRequest.Dispose();

                // Capture the exception and invoke outer handlers to 
                // process the exception.
                executionContext.ResponseContext.AsyncResult.Exception = exception;
                base.InvokeAsyncCallback(executionContext);
            }
        }

        private void GetResponseCallback(IAsyncResult result)
        {
            IAsyncExecutionContext executionContext = null;
            IHttpRequest<TRequestContent> httpRequest = null;
            try
            {
                executionContext = result.AsyncState as IAsyncExecutionContext;
                if (executionContext == null)
                {
                    return;
                }
                httpRequest = executionContext.RuntimeState as IHttpRequest<TRequestContent>;
                if (httpRequest == null)
                {
                    return;
                }

                var httpResponse = httpRequest.EndGetResponse(result);
                executionContext.ResponseContext.HttpResponse = httpResponse;
            }
            catch (Exception exception)
            {   
                // Capture the exception and invoke outer handlers to 
                // process the exception.
                executionContext.ResponseContext.AsyncResult.Exception = exception;
            }
            finally
            {
                if ( executionContext != null )
                {
                    executionContext.RequestContext.Metrics.StopEvent(Metric.HttpRequestTime);
                }
                if ( httpRequest != null)
                {
                    httpRequest.Dispose();
                }
                base.InvokeAsyncCallback(executionContext);
            }
        }       

#endif

        private static void SetMetrics(IRequestContext requestContext)
        {
            requestContext.Metrics.AddProperty(Metric.ServiceName, requestContext.Request.ServiceName);
            requestContext.Metrics.AddProperty(Metric.ServiceEndpoint, requestContext.Request.Endpoint);
            requestContext.Metrics.AddProperty(Metric.MethodName, requestContext.Request.RequestName);
        }

        /// <summary>
        /// Determines the content for request body and uses the HTTP request
        /// to write the content to the HTTP request body.
        /// </summary>
        /// <param name="requestContent">Content to be written.</param>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="requestContext">The request context.</param>
        private void WriteContentToRequestBody(TRequestContent requestContent,
            IHttpRequest<TRequestContent> httpRequest,
            IRequestContext requestContext)
        {
            IRequest wrappedRequest = requestContext.Request;

            if (wrappedRequest.ContentStream == null)
            {
                byte[] requestData = wrappedRequest.Content;
                requestContext.Metrics.AddProperty(Metric.RequestSize, requestData.Length);
                httpRequest.WriteToRequestBody(requestContent, requestData, requestContext.Request.Headers);
            }
            else
            {
                var originalStream = wrappedRequest.ContentStream;
                var callback = wrappedRequest.OriginalRequest.StreamUploadProgressCallback;
                if (callback != null)
                {
                    var eventStream = new EventStream(originalStream, true);
                    var tracker = new StreamReadTracker(this.CallbackSender, callback, originalStream.Length,
                        requestContext.ClientConfig.ProgressUpdateInterval);
                    eventStream.OnRead += tracker.ReadProgress;
                    originalStream = eventStream;
                }

                var inputStream = wrappedRequest.UseChunkEncoding && wrappedRequest.AWS4SignerResult != null
                    ? new ChunkedUploadWrapperStream(originalStream,
                                                     requestContext.ClientConfig.BufferSize,
                                                     wrappedRequest.AWS4SignerResult)
                    : originalStream;

                httpRequest.WriteToRequestBody(requestContent, inputStream, 
                    requestContext.Request.Headers, requestContext);

            }
        }

        /// <summary>
        /// Creates the HttpWebRequest and configures the end point, content, user agent and proxy settings.
        /// </summary>
        /// <param name="requestContext">The async request.</param>
        /// <returns>The web request that actually makes the call.</returns>
        protected virtual IHttpRequest<TRequestContent> CreateWebRequest(IRequestContext requestContext)
        {
            IRequest request = requestContext.Request;
            Uri url = ObsServiceClient.ComposeUrl(request);
            var httpRequest = _requestFactory.CreateHttpRequest(url);
                        
            if (httpRequest is HttpRequest)
            {
                LoggerMgr.Log_Run_Debug(OBS.S3.ObsClient.strProduct, "HttpHandle::CreateWebRequest");
                ((HttpRequest)httpRequest).Request.Proxy = null;
                ((HttpRequest)httpRequest).Request.KeepAlive = false;
                ((HttpRequest)httpRequest).Request.UserAgent = "eSDK" + "-" + OBS.S3.ObsClient.strProduct + "-" + OBS.S3.ObsClient.strProductVersion;
            }
            
            httpRequest.ConfigureRequest(requestContext);
            
            httpRequest.Method = request.HttpMethod;
            if (request.MayContainRequestBody())
            {
                if (request.Content == null && (request.ContentStream == null))
                {
                    string queryString = AWSSDKUtils.GetParametersAsString(request.Parameters);
                    request.Content = Encoding.UTF8.GetBytes(queryString);
                }
                
                if (request.Content!=null)
                {
                    request.Headers[HeaderKeys.ContentLengthHeader] = 
                        request.Content.Length.ToString(CultureInfo.InvariantCulture);
                }
                else if (request.ContentStream != null && !request.Headers.ContainsKey(HeaderKeys.ContentLengthHeader))
                {
                    request.Headers[HeaderKeys.ContentLengthHeader] =
                        request.ContentStream.Length.ToString(CultureInfo.InvariantCulture);
                }
            }
            else if (request.UseQueryString &&
                (request.HttpMethod == "POST" ||
                 request.HttpMethod == "PUT"))
            {
                request.Content = new Byte[0];
            }

            if (requestContext.Unmarshaller is JsonResponseUnmarshaller)
            {
                // Currently the signature seems to be valid without including this header in the calculation.
                request.Headers["Accept"] = "application/json";
            }
            
            return httpRequest;
        }

        /// <summary>
        /// Disposes the HTTP handler.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_requestFactory != null)
                    _requestFactory.Dispose();

                _disposed = true;
            }
        }
    }
}
