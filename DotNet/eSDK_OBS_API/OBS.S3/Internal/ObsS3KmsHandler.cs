/*
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

using OBS.Runtime;
using OBS.Runtime.Internal;
using OBS.S3.Util;
using System;

namespace OBS.S3.Internal
{
    public class ObsS3KmsHandler : GenericHandler
    {
        protected override void PreInvoke(IExecutionContext executionContext)
        {
            var request = executionContext.RequestContext.Request;
            EvaluateIfSigV4Required(request);
        }

        internal static void EvaluateIfSigV4Required(IRequest request)
        {
            //2015-6-9 signature
            if (ObsConfigs.S3Config.UseSignatureVersion4)
            {
                request.UseSigV4 = true;
            }
        }
    }
}
