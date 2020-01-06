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
    /// Container for the parameters to the PutBucketNotificationConfiguration operation.
    /// <para>Set the NotificationConfiguration  for a bucket.</para>
    /// </summary>
    public partial class PutBucketNotificationRequest : ObsWebServiceRequest
    {
        private string bucketName;
        private NotificationConfiguration notificationConfiguration;

        /// <summary>
        /// The name of the bucket to apply the configuration to.
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
        /// The notification configuration to apply. The configuration defines the index
        /// </summary>
        public NotificationConfiguration NotificationConfiguration
        {
            get { return this.notificationConfiguration; }
            set { this.notificationConfiguration = value; }
        }

        // Check to see if WebsiteConfiguration property is set
        internal bool IsSetWebsiteConfiguration()
        {
            return this.notificationConfiguration != null;
        }

    }
}
    
