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
using System.IO;

using OBS.Runtime;
using OBS.Runtime.Internal;

namespace OBS.S3.Model
{
    /// <summary>
    /// Container for the parameters to the HeadObject operation.
    /// <para>The HEAD operation retrieves metadata from an bucket without returning the bucket itself. This operation is useful if you''re only
    /// interested in an bucket''s metadata. To use HEAD, you must have READ access to the bucket.</para>
    /// </summary>
    public partial class GetBucketMetadataRequest : ObsWebServiceRequest
    {
        private string bucketName;
        private string origin;
        private List<string> accessControlRequestHeaders;



        /// <summary>
        /// The name of the bucket that contains the object.
        /// </summary>
        public string BucketName
        {
            get { return this.bucketName; }
            set { this.bucketName = value; }
        }

        // Check to see if BucketName property is set
        internal bool IsSetBucketName()
        {
            return this.bucketName != null;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Origin
        {
            get { return this.origin; }
            set { this.origin = value; }
        }

        // Check to see if Origin property is set
        internal bool IsSetOrigin()
        {
            return this.origin != null;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> AccessControlRequestHeaders
        {
            get { return this.accessControlRequestHeaders; }
            set { this.accessControlRequestHeaders = value; }
        }

        // Check to see if AccessControlRequestHeaders property is set
        internal bool IsSetAccessControlRequestHeaders()
        {
            return this.accessControlRequestHeaders != null;
        }

    }
}
    
