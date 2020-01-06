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
    /// Returns information about the GetPreSignedUrl response.
    /// The GetPreSignedUrl operation has a void result type.
    /// </summary>
    public partial class GetPreSignedUrlResponse : ObsWebServiceResponse
    {
        private string tempSignUrl;
        IDictionary<string, string> actualSignedRequestHeaders = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public GetPreSignedUrlResponse(string tempSignUrl)
        {
            this.tempSignUrl = tempSignUrl;
        }
        public GetPreSignedUrlResponse(string tempSignUrl, IDictionary<string, string> actualSignedRequestHeaders) 
        {
            this.tempSignUrl = tempSignUrl;
            this.actualSignedRequestHeaders = actualSignedRequestHeaders;
        }
        /// <summary>
        /// the tempsign url
        /// </summary>
        public string TempSignUrl
        {
            get { return this.tempSignUrl; }
            set { this.tempSignUrl = value; }
        }

        // Check to see if TempSignUrl property is set
        internal bool IsSetTempSignUrl()
        {
            return this.tempSignUrl != null;
        }

        /// <summary>
        /// the ActualSignedRequestHeaders
        /// </summary>
        public IDictionary<String,String> ActualSignedRequestHeaders
        {
            get { return this.actualSignedRequestHeaders; }
            set { this.actualSignedRequestHeaders = value; }
        }

        /// <summary>
        /// Checks if SubResources property is set.
        /// </summary>
        /// <returns>true if ActualSignedRequestHeaders property is set.</returns>
        internal bool IsSetActualSignedRequestHeaders()
        {
            return (this.actualSignedRequestHeaders != null);
        }
    }
}
    
