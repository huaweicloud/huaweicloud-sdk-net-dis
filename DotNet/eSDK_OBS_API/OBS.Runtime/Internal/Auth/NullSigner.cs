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
using System.Linq;
using System.Text;

namespace OBS.Runtime.Internal.Auth
{
    /// <summary>
    /// Null Signer which does a no-op.
    /// </summary>
    internal class NullSigner : AbstractAWSSigner
    {
        public override void Sign(IRequest request, ClientConfig clientConfig, Util.RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            // This is a null signer which a does no-op
            return;
        }

        public override ClientProtocol Protocol
        {
            get { return ClientProtocol.Unknown; }
        }
    }
}
