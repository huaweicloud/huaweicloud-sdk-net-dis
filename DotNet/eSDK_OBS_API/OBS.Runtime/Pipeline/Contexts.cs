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

using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Auth;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;
using System;

namespace OBS.Runtime
{
    public interface IRequestContext
    {
        ObsWebServiceRequest OriginalRequest { get; }
        string RequestName { get; }
        IMarshaller<IRequest, ObsWebServiceRequest> Marshaller { get; }
        ResponseUnmarshaller Unmarshaller { get; }
        RequestMetrics Metrics { get; }
        AbstractAWSSigner Signer { get; }
        ClientConfig ClientConfig { get; }
        ImmutableCredentials ImmutableCredentials { get; set; }

        IRequest Request { get; set; }
        bool IsSigned { get; set; }
        bool IsAsync { get; }
        int Retries { get; set; }

#if BCL45 || WIN_RT || WINDOWS_PHONE
        System.Threading.CancellationToken CancellationToken { get; }
#endif
    }

    public interface IResponseContext
    {
        ObsWebServiceResponse Response { get; set; }
        IWebResponseData HttpResponse { get; set; }
    }

    public interface IAsyncRequestContext : IRequestContext
    {
        AsyncCallback Callback { get; }
        object State { get; }
    }    

    public interface IAsyncResponseContext : IResponseContext
    {
        OBS.Runtime.Internal.RuntimeAsyncResult AsyncResult { get; set; }
    }

    public interface IExecutionContext
    {
        IResponseContext ResponseContext { get; }
        IRequestContext RequestContext { get; }

    }

    public interface IAsyncExecutionContext
    {
        IAsyncResponseContext ResponseContext { get; }
        IAsyncRequestContext RequestContext { get; }

        object RuntimeState { get; set; }
    }
}

namespace OBS.Runtime.Internal
{
    public class RequestContext : IRequestContext
    {
        public RequestContext(bool enableMetrics)
        {
            this.Metrics = new RequestMetrics();
            this.Metrics.IsEnabled = enableMetrics;
        }

        public IRequest Request { get; set; }
        public RequestMetrics Metrics { get; private set; }
        public AbstractAWSSigner Signer { get; set; }
        public ClientConfig ClientConfig { get; set; }
        public int Retries { get; set; }
        public bool IsSigned { get; set; }
        public bool IsAsync { get; set; }
        public ObsWebServiceRequest OriginalRequest { get; set; }
        public IMarshaller<IRequest, ObsWebServiceRequest> Marshaller { get; set; }
        public ResponseUnmarshaller Unmarshaller { get; set; }
        public ImmutableCredentials ImmutableCredentials { get; set; }


#if BCL45 || WIN_RT || WINDOWS_PHONE
        public System.Threading.CancellationToken CancellationToken { get; set; }
#endif

        public string RequestName
        {
            get { return this.OriginalRequest.GetType().Name; }
        }
    }

    public class AsyncRequestContext : RequestContext, IAsyncRequestContext
    {
        public AsyncRequestContext(bool enableMetrics):
            base(enableMetrics)
        {
        }

        public AsyncCallback Callback { get; set; }
        public object State { get; set; }
    }

    public class ResponseContext : IResponseContext
    {
        public ObsWebServiceResponse Response { get; set; }        
        public IWebResponseData HttpResponse { get; set; }
    }

    public class AsyncResponseContext : ResponseContext, IAsyncResponseContext
    {
        public OBS.Runtime.Internal.RuntimeAsyncResult AsyncResult { get; set; }
    }

    public class ExecutionContext : IExecutionContext
    {
        public IRequestContext RequestContext { get; private set; }
        public IResponseContext ResponseContext { get; private set; }        

        public ExecutionContext(bool enableMetrics)
        {
            this.RequestContext = new RequestContext(enableMetrics);
            this.ResponseContext = new ResponseContext();
        }

        public ExecutionContext(IRequestContext requestContext, IResponseContext responseContext)
        {
            this.RequestContext = requestContext;
            this.ResponseContext = responseContext;
        }

        public static IExecutionContext CreateFromAsyncContext(IAsyncExecutionContext asyncContext)
        {
            return new ExecutionContext(asyncContext.RequestContext,
                asyncContext.ResponseContext);
        }
    }

    public class AsyncExecutionContext : IAsyncExecutionContext
    {
        public IAsyncResponseContext ResponseContext { get; private set; }
        public IAsyncRequestContext RequestContext { get; private set; }

        public object RuntimeState { get; set; }

        public AsyncExecutionContext(bool enableMetrics)
        {
            this.RequestContext = new AsyncRequestContext(enableMetrics);
            this.ResponseContext = new AsyncResponseContext();
        }

        public AsyncExecutionContext(IAsyncRequestContext requestContext, IAsyncResponseContext responseContext)
        {
            this.RequestContext = requestContext;
            this.ResponseContext = responseContext;
        }
    }
}
