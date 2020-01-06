/*
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
using System.Xml.Serialization;
using System.Text;

using OBS.Runtime;

namespace OBS.S3.Model
{
    /// <summary>
    /// Returns information about the  HeadBucket response and response metadata.
    /// </summary>
    public class GetBucketMetadataResponse : ObsWebServiceResponse
    {
        /**
        * 如果请求的Origin满足服务端的CORS配置，则在响应中包含allowOrigin,allowHeaders,maxAge,allowMethods,exposeHeaders,storageClass
        */
        //private List<String> allowOrigin;
        //private List<String> allowHeaders;
        //private int maxAge;
        //private List<String> allowMethods;
        //private List<String> exposeHeaders;
        //private string storageClass;

        private IDictionary<string, string> headersCollection = new Dictionary<string, string>();
        /// <summary>
        /// The collection of headers for the request.
        /// </summary>
        public IDictionary<string, string> Headers
        {
            get
            {
                if (this.headersCollection == null)
                    this.headersCollection = new Dictionary<string, string>();
                return this.headersCollection;
            }
        }

    }
}
    
