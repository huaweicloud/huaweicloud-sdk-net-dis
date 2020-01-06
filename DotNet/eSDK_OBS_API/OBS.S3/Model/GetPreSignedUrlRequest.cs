/*******************************************************************************
 *  Copyright 2008-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 *  API Version: 2006-03-01
 *
 */

using System;
using System.Xml.Serialization;
using OBS.Runtime;
using OBS.S3.Util;
using System.Collections.Generic;

namespace OBS.S3.Model
{
    /// <summary>
    /// The parameters to create a pre-signed URL to a bucket or object. 
    /// </summary>
    /// <remarks>
    /// For more information, refer to: <see href="http://docs.amazonwebservices.com/ObsS3/latest/dev/S3_QSAuth.html"/>.
    /// <br />Required Parameters: BucketName, Expires
    /// <br />Optional Parameters: Key, VersionId, Verb: default is GET
    /// </remarks>
    public class GetPreSignedUrlRequest : ObsWebServiceRequest
    {
        #region Private Members

        string bucketName;
        string key;
        DateTime? expires;

        HttpVerb verb;

        private HeadersCollection headersCollection = new HeadersCollection();
        private MetadataCollection metadataCollection = new MetadataCollection();
        IDictionary<string, string> subResources = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        IDictionary<string, string> parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);


        #endregion

        #region BucketName

        /// <summary>
        /// The name of the bucket to create a pre-signed url to, or containing the object.
        /// </summary>
        public string BucketName
        {
            get { return this.bucketName; }
            set { this.bucketName = value; }
        }

        /// <summary>
        /// Checks if BucketName property is set.
        /// </summary>
        /// <returns>true if BucketName property is set.</returns>
        internal bool IsSetBucketName()
        {
            return !System.String.IsNullOrEmpty(this.bucketName);
        }

        #endregion

        #region Key

        /// <summary>
        /// The key to the object for which a pre-signed url should be created.
        /// </summary>
        public string Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        /// <summary>
        /// Checks if Key property is set.
        /// </summary>
        /// <returns>true if Key property is set.</returns>
        internal bool IsSetKey()
        {
            return !System.String.IsNullOrEmpty(this.key);
        }

        #endregion

        #region ContentType
        /// <summary>
        /// A standard MIME type describing the format of the object data.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The content type for the content being uploaded. This property defaults to "binary/octet-stream".
        /// For more information, refer to: <see href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html#sec14.17"/>.
        /// </para>
        /// <para>
        /// Note that if content type is specified, it should also be included in the HttpRequest headers
        /// of the eventual upload request, otherwise a signature error may result.
        /// </para>
        /// </remarks>
        public string ContentType
        {
            get { return this.headersCollection.ContentType; }
            set { this.headersCollection.ContentType = value; }
        }

        #endregion

        #region Expires
        /// <summary>
        /// The expiry date and time for the pre-signed url.
        /// </summary>
        public DateTime Expires
        {
            get { return this.expires.GetValueOrDefault(); }
            set { this.expires = value; }
        }

        /// <summary>
        /// Checks if Expires property is set.
        /// </summary>
        /// <returns>true if Expires property is set.</returns>
        public bool IsSetExpires()
        {
            return this.expires.HasValue;
        }

        #endregion


        #region Verb
        /// <summary>
        /// The verb for the pre-signed url. 
        /// </summary>
        /// <remarks>
        /// Accepted verbs are GET, PUT, DELETE and HEAD.
        /// Default is GET.
        /// </remarks>
        public HttpVerb Verb
        {
            get { return this.verb; }
            set { this.verb = value; }
        }

        #endregion


        



        #region Headers

        /// <summary>
        /// The collection of headers for the request.
        /// </summary>
        public HeadersCollection Headers
        {
            get
            {
                if (this.headersCollection == null)
                    this.headersCollection = new HeadersCollection();
                return this.headersCollection;
            }
            internal set
            {
                this.headersCollection = value;
            }
        }

        #endregion

        #region Metadata

        /// <summary>
        /// The collection of meta data for the request.
        /// </summary>
        public MetadataCollection Metadata
        {
            get
            {
                if (this.metadataCollection == null)
                    this.metadataCollection = new MetadataCollection();
                return this.metadataCollection;
            }
            internal set
            {
                this.metadataCollection = value;
            }
        }

        #endregion
        #region SubResources
        /// <summary>
        /// the SubResources for the request
        /// </summary>
        public IDictionary<String,String> SubResources
        {
            get { return this.subResources; }
            set { this.subResources = value; }
        }
        /// <summary>
        /// Checks if SubResources property is set.
        /// </summary>
        /// <returns>true if SubResources property is set.</returns>
        internal bool IsSetSubResources()
        {
            return (this.subResources != null);
        }
        #endregion
        #region Parameters
        /// <summary>
        /// the Parameters for the request
        /// </summary>
        public IDictionary<String, String> Parameters
        {
            get { return this.parameters; }
            set { this.parameters = value; }
        }
        /// <summary>
        /// Checks if Parameters property is set.
        /// </summary>
        /// <returns>true if SubResources property is set.</returns>
        internal bool IsSetParameters()
        {
            return (this.parameters != null);
        }
        #endregion
    }
}