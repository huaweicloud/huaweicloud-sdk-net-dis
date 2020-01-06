using eSDK_OBS_API.OBS.Util; 
/*
 * Copyright 2010-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Reflection;
using OBS.Util;
using System.Globalization;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Head Bucket Request Marshaller
    /// </summary>       
    internal class OptionsObjectRequestMarshaller : IMarshaller<IRequest, OptionsObjectRequest>, IMarshaller<IRequest, OBS.Runtime.ObsWebServiceRequest>
    {
        public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
        {
            return this.Marshall((OptionsObjectRequest)input);
        }

        public IRequest Marshall(OptionsObjectRequest optionsRequest)
        {
            IRequest request = new DefaultRequest(optionsRequest, "ObsS3");

            request.HttpMethod = "OPTIONS";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("OptionsObjectResponseUnmarshaller HttpMethod: {0}", request.HttpMethod));

            //Origin
            if (optionsRequest.IsSetOrigin())
                request.Headers.Add(HeaderKeys.Origin, optionsRequest.Origin);

            //Access-Control-Request-Method
            if (optionsRequest.IsSetAccessControlRequestMethod())
            {
                foreach (string method in optionsRequest.AccessControlRequestMethod)
                {
                    if (!request.Headers.ContainsKey(HeaderKeys.AccessControlRequestMethod))
                    {
                        request.Headers.Add(HeaderKeys.AccessControlRequestMethod, method);
                    }
                    else
                    {
                        request.Headers[HeaderKeys.AccessControlRequestMethod] = method;
                    }
                } 
            }

            //Access-Control-Request-Headers
            if (optionsRequest.IsSetAccessControlRequestHeaders())
            {
                foreach (string method in optionsRequest.AccessControlRequestHeaders)
                {
                    if (!request.Headers.ContainsKey(HeaderKeys.AccessControlRequestHeaders))
                    {
                        request.Headers.Add(HeaderKeys.AccessControlRequestHeaders, method);
                    }
                    else
                    {
                        request.Headers[HeaderKeys.AccessControlRequestHeaders] = method;
                    }
                }
            }

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(optionsRequest.BucketName),
                                                 S3Transforms.ToStringValue(optionsRequest.Key));

            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("OptionsObjectResponseUnmarshaller ResourcePath: {0}", request.ResourcePath));

            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
