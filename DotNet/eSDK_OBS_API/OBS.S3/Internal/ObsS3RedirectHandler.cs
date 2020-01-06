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
using OBS.Util;

namespace OBS.S3.Internal
{
    public class ObsS3RedirectHandler : RedirectHandler
    {
        protected override void FinalizeForRedirect(IExecutionContext executionContext, string redirectedLocation)
        {
            var request = executionContext.RequestContext.Request;
            if (request.UseChunkEncoding)
            {
                if (request.Headers.ContainsKey(HeaderKeys.XAmzDecodedContentLengthHeader))
                {
                    request.Headers[HeaderKeys.ContentLengthHeader] =
                        request.Headers[HeaderKeys.XAmzDecodedContentLengthHeader];
                }
            }

            if (request.Headers.ContainsKey(HeaderKeys.HostHeader))
            {
                request.Headers.Remove(HeaderKeys.HostHeader);
            }

            // FinalizeForRedirect() sets the correct endpoint as per the redirected location.
            base.FinalizeForRedirect(executionContext, redirectedLocation);

            // Evaluate if this request requires SigV4. The endpoint set by FinalizeForRedirect()
            // is one of the inputs to decide if SigV4 is required.
            ObsS3KmsHandler.EvaluateIfSigV4Required(executionContext.RequestContext.Request);

            var redirect = new ObsS3Uri(redirectedLocation);
            if (ObsConfigs.S3Config.UseSignatureVersion4 ||
                request.UseSigV4 ||
                redirect.Region.GetEndpointForService("s3").SignatureVersionOverride == "4")
            {
                // Resign if sigV4 is enabled, the request explicitly requires SigV4 or if the redirected region mandates sigV4.
                // resign appropriately for the redirected region, re-instating the user's client 
                // config to original state when done

                request.AuthenticationRegion = redirect.Region.SystemName;
                Signer.SignRequest(executionContext.RequestContext);
            }
        }
    }
}
