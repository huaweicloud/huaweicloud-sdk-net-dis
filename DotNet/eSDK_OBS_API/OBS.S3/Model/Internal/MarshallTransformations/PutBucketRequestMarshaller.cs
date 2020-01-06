using eSDK_OBS_API.OBS.Util; 
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

using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using OBS.S3.Util;
using OBS.Runtime;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Globalization;
using OBS.Util;
using System.Reflection;
using System.Collections;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Bucket Request Marshaller
    /// </summary>       
    public class PutBucketRequestMarshaller : IMarshaller<IRequest, PutBucketRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((PutBucketRequest)input);
		}

        public IRequest Marshall(PutBucketRequest putBucketRequest)
        {
            IRequest request = new DefaultRequest(putBucketRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (putBucketRequest.IsSetCannedACL())
                request.Headers.Add(HeaderKeys.XAmzAclHeader, putBucketRequest.CannedACL.Value);
            else if (putBucketRequest.Grants != null && putBucketRequest.Grants.Count > 0)
                ConvertPutWithACLRequest(putBucketRequest, request);
            if (putBucketRequest.IsSetStorageClass())
                request.Headers.Add(HeaderKeys.XAmzDefaultStorageClassHeader, putBucketRequest.StorageClass);

            var uriResourcePath = string.Concat("/", S3Transforms.ToStringValue(putBucketRequest.BucketName));

            request.ResourcePath = uriResourcePath;
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            var stringWriter = new StringWriter(CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                string regionCode = null;
                var region = putBucketRequest.BucketRegion;
                if (region != null && !string.IsNullOrEmpty(region.Value))
                {
                    regionCode = region.Value;
                }
                else if (!string.IsNullOrEmpty(putBucketRequest.BucketRegionName))
                {
                    if (putBucketRequest.BucketRegionName == "eu-west-1")
                        regionCode = "EU";
                    else if (putBucketRequest.BucketRegionName != "us-east-1")
                        regionCode = putBucketRequest.BucketRegionName;
                }

                if (regionCode != null)
                {
                    xmlWriter.WriteStartElement("CreateBucketConfiguration", "");
                    xmlWriter.WriteElementString("LocationConstraint", "", regionCode);
                    xmlWriter.WriteEndElement();                                        
                }
            }

            try
            {
                var content = stringWriter.ToString();

                request.Content = Encoding.UTF8.GetBytes(content);
                request.Headers[HeaderKeys.ContentTypeHeader] = "application/xml";

                var checksum = ObsS3Util.GenerateChecksumForContent(content, true);

                //2015-5-26 w00322557
                foreach(var head in request.Headers)
                {
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
                }                
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutBucketRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }

        protected internal static void ConvertPutWithACLRequest(PutWithACLRequest request, IRequest irequest)
        {
            Dictionary<S3Permission, string> protoHeaders = new Dictionary<S3Permission, string>();
            foreach (var grant in request.Grants)
            {
                string grantee = null;
                if (grant.Grantee.CanonicalUser != null && !string.IsNullOrEmpty(grant.Grantee.CanonicalUser))
                    grantee = string.Format(CultureInfo.InvariantCulture, "id=\"{0}\"", grant.Grantee.CanonicalUser);
                else if (grant.Grantee.IsSetEmailAddress())
                    grantee = string.Format(CultureInfo.InvariantCulture, "emailAddress=\"{0}\"", grant.Grantee.EmailAddress);
                else if (grant.Grantee.IsSetURI())
                    grantee = string.Format(CultureInfo.InvariantCulture, "uri=\"{0}\"", grant.Grantee.URI);
                else continue;

                string glist = null;
                if (protoHeaders.TryGetValue(grant.Permission, out glist))
                    protoHeaders[grant.Permission] = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", glist, grantee);
                else
                    protoHeaders.Add(grant.Permission, grantee);
            }

            foreach (var permission in protoHeaders.Keys)
            {
                if (permission == S3Permission.READ)
                    irequest.Headers[S3Constants.AmzGrantHeaderRead] = protoHeaders[permission];
                if (permission == S3Permission.WRITE)
                    irequest.Headers[S3Constants.AmzGrantHeaderWrite] = protoHeaders[permission];
                if (permission == S3Permission.READ_ACP)
                    irequest.Headers[S3Constants.AmzGrantHeaderReadAcp] = protoHeaders[permission];
                if (permission == S3Permission.WRITE_ACP)
                    irequest.Headers[S3Constants.AmzGrantHeaderWriteAcp] = protoHeaders[permission];
                if (permission == S3Permission.RESTORE_OBJECT)
                    irequest.Headers[S3Constants.AmzGrantHeaderRestoreObject] = protoHeaders[permission];
                if (permission == S3Permission.FULL_CONTROL)
                    irequest.Headers[S3Constants.AmzGrantHeaderFullControl] = protoHeaders[permission];
            }
        }
    }
}

