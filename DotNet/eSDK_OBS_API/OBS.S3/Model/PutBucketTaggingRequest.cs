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
    /// Container for the parameters to the PutBucketLoggingRequest operation.
    /// <para>Set the logging parameters for a bucket and to specify permissions for who can view and modify the logging parameters. To set the
    /// logging status of a bucket, you must be the bucket owner.</para>
    /// </summary>
    public partial class PutBucketTaggingRequest : ObsWebServiceRequest
    {
        private string bucketName;
        private List<Tag> tagList;

        /// <summary>
        /// The name of the bucket to be created.
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
        /// The tag  of the bucket to be created.
        /// </summary>

        public List<Tag> TagList
        {
            get { return this.tagList; }
            set { this.tagList = value; }
        }

        // Check to see if TagConfiguration property is set
        internal bool IsSetConfiguration()
        {
            return this.tagList != null;
        }

    }
}
    
