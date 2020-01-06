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

using System;

namespace OBS.Runtime.Internal
{    
    /// <summary>
    /// A generic handler that provides overridable PreInvoke and PostInvoke methods. 
    /// This class is intended be used as a base class for handlers which perform simple processing.
    /// </summary>
    public abstract class GenericHandler : PipelineHandler
    {
        /// <summary>
        /// Calls the PreInvoke and PostInvoke methods before and after calling the next handler 
        /// in the pipeline.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        public override void InvokeSync(IExecutionContext executionContext)
        {
            PreInvoke(executionContext);
            base.InvokeSync(executionContext);         
            PostInvoke(executionContext);            
        }
#if BCL45 || WIN_RT || WINDOWS_PHONE 

        /// <summary>
        /// Calls the PreInvoke and PostInvoke methods before and after calling the next handler 
        /// in the pipeline.
        /// </summary>
        /// <typeparam name="T">The response type for the current request.</typeparam>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public override async System.Threading.Tasks.Task<T> InvokeAsync<T>(IExecutionContext executionContext)
        {
            PreInvoke(executionContext);
            var response = await base.InvokeAsync<T>(executionContext).ConfigureAwait(false);
            PostInvoke(executionContext);
            return response;
        }

#elif BCL && !BCL45

        /// <summary>
        /// Calls the PreInvoke method before calling the next handler in the pipeline.
        /// </summary>
        /// <param name="executionContext">The execution context which contains both the
        /// requests and response context.</param>
        /// <returns>IAsyncResult which represent an async operation.</returns>
        public override IAsyncResult InvokeAsync(IAsyncExecutionContext executionContext)
        {
            PreInvoke(ExecutionContext.CreateFromAsyncContext(executionContext));
            return base.InvokeAsync(executionContext);
        }

        /// <summary>
        /// Calls the PostInvoke methods after calling the next handler 
        /// in the pipeline.
        /// </summary>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        protected override void InvokeAsyncCallback(IAsyncExecutionContext executionContext)
        {
            PostInvoke(ExecutionContext.CreateFromAsyncContext(executionContext));
            base.InvokeAsyncCallback(executionContext);           
        }
#endif

        /// <summary>
        /// This method is invoked before calling the next handler in the pipeline.
        /// </summary>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        protected virtual void PreInvoke(IExecutionContext executionContext) { }

        /// <summary>
        /// This method is invoked after calling the next handler in the pipeline.
        /// </summary>
        /// <param name="executionContext">The execution context, it contains the
        /// request and response context.</param>
        protected virtual void PostInvoke(IExecutionContext executionContext) { }        
    }
}
