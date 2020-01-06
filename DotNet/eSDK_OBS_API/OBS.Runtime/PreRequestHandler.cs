﻿/*
 * Copyright 2011-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
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
using System.Net;
using System.Collections.Specialized;

namespace OBS.Runtime
{
    public class PreRequestEventArgs : EventArgs
    {
        #region Constructor

        protected PreRequestEventArgs() { }
        
        #endregion

        #region Properties

        public ObsWebServiceRequest Request { get; protected set; }

        #endregion

        #region Creator method

        internal static PreRequestEventArgs Create(ObsWebServiceRequest request)
        {
            PreRequestEventArgs args = new PreRequestEventArgs
            {
                Request = request
            };
            return args;
        }

        #endregion
    }

    public delegate void PreRequestEventHandler(object sender, PreRequestEventArgs e);
}
