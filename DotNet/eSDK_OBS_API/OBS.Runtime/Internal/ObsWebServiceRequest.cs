﻿/*
 * Copyright 2010-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
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
using System.Collections.Generic;
using System.Text;

using OBS.Runtime.Internal;

namespace OBS.Runtime
{
    public abstract partial class ObsWebServiceRequest : IRequestEvents
    {
        /// <summary>
        /// This flag specifies if SigV4 will be used for the current request.
        /// </summary>
        public bool UseSigV4
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets a value indicating if "Expect: 100-continue" HTTP header will be 
        /// sent by the client for this request. The default value is false.
        /// </summary>
        internal virtual bool Expect100Continue
        {
            get { return false; }            
        }

        internal virtual bool IncludeSHA256Header
        {
            get { return true; }
        }

        void IRequestEvents.AddBeforeRequestHandler(RequestEventHandler handler)
        {
            ((ObsWebServiceRequest)this).WithBeforeRequestHandler(handler);
        }

        void IRequestEvents.FireBeforeRequestEvent(object sender, RequestEventArgs args)
        {
            ((ObsWebServiceRequest)this).FireBeforeRequestEvent(sender, args);
        }
    }
}
