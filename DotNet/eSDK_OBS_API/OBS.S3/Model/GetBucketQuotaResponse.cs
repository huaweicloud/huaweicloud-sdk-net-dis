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
    /// Returns information about the  GetBucketWebsite response and response metadata.
    /// </summary>
    public class GetBucketQuotaResponse : ObsWebServiceResponse
    {
        string storageQuota;

        /// <summary>
        /// Gets and sets the StorageQuota property.
        /// 
        /// This is where the index document suffix and custom error page are defined.
        /// </summary>
        public string StorageQuota
        {
            get { return this.storageQuota; }
            set { this.storageQuota = value; }
        }

    }
}
    
