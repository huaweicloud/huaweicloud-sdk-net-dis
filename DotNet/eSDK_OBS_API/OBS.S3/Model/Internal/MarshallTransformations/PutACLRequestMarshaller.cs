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

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Object Acl Request Marshaller
    /// </summary>       
    public class PutACLRequestMarshaller : IMarshaller<IRequest, PutACLRequest>, IMarshaller<IRequest, OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PutACLRequest)input);
		}

        public IRequest Marshall(PutACLRequest putObjectAclRequest)
        {
            IRequest request = new DefaultRequest(putObjectAclRequest, "ObsS3");

            request.HttpMethod = "PUT";

            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutACLRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (putObjectAclRequest.IsSetCannedACL())
                request.Headers.Add(HeaderKeys.XAmzAclHeader, S3Transforms.ToStringValue(putObjectAclRequest.CannedACL));

            // if we are putting the acl onto the bucket, the keyname component will collapse to empty string
            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(putObjectAclRequest.BucketName),
                                                 S3Transforms.ToStringValue(putObjectAclRequest.Key));

            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutACLRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("acl");
            if (putObjectAclRequest.IsSetVersionId())
                request.AddSubResource("versionId", S3Transforms.ToStringValue(putObjectAclRequest.VersionId));

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (
                var xmlWriter = XmlWriter.Create(stringWriter,
                                                 new XmlWriterSettings()
                                                     {
                                                         Encoding = Encoding.UTF8,
                                                         OmitXmlDeclaration = true
                                                     }))
            {
                var accessControlPolicyAccessControlPolicy = putObjectAclRequest.AccessControlList;
                if (accessControlPolicyAccessControlPolicy != null)
                {
                    xmlWriter.WriteStartElement("AccessControlPolicy", "");

                    var ownerOwner = accessControlPolicyAccessControlPolicy.Owner;
                    if (ownerOwner != null)
                    {
                        xmlWriter.WriteStartElement("Owner", "");
                        if (ownerOwner.IsSetDisplayName())
                        {
                            xmlWriter.WriteElementString("DisplayName", "",
                                                         S3Transforms.ToXmlStringValue(ownerOwner.DisplayName));
                        }
                        if (ownerOwner.IsSetId())
                        {
                            xmlWriter.WriteElementString("ID", "", S3Transforms.ToXmlStringValue(ownerOwner.Id));
                        }
                        xmlWriter.WriteEndElement();
                    }

                    var accessControlPolicyAccessControlPolicygrantsList = accessControlPolicyAccessControlPolicy.Grants;
                    if (accessControlPolicyAccessControlPolicygrantsList != null &&
                        accessControlPolicyAccessControlPolicygrantsList.Count > 0)
                    {
                        xmlWriter.WriteStartElement("AccessControlList", "");
                        foreach (
                            var accessControlPolicyAccessControlPolicygrantsListValue in
                                accessControlPolicyAccessControlPolicygrantsList)
                        {
                            xmlWriter.WriteStartElement("Grant", "");
                            if (accessControlPolicyAccessControlPolicygrantsListValue != null)
                            {
                                var granteeGrantee = accessControlPolicyAccessControlPolicygrantsListValue.Grantee;
                                if (granteeGrantee != null)
                                {
                                    xmlWriter.WriteStartElement("Grantee", "");
                                    if (granteeGrantee.IsSetType())
                                    {
                                        xmlWriter.WriteAttributeString("xsi", "type",
                                                                       "http://www.w3.org/2001/XMLSchema-instance",
                                                                       granteeGrantee.Type.ToString());
                                    }
                                    if (granteeGrantee.IsSetDisplayName())
                                    {
                                        xmlWriter.WriteElementString("DisplayName", "",
                                                                     S3Transforms.ToXmlStringValue(
                                                                         granteeGrantee.DisplayName));
                                    }
                                    if (granteeGrantee.IsSetEmailAddress())
                                    {
                                        xmlWriter.WriteElementString("EmailAddress", "",
                                                                     S3Transforms.ToXmlStringValue(
                                                                         granteeGrantee.EmailAddress));
                                    }
                                    if (granteeGrantee.IsSetCanonicalUser())
                                    {
                                        xmlWriter.WriteElementString("ID", "",
                                                                     S3Transforms.ToXmlStringValue(
                                                                         granteeGrantee.CanonicalUser));
                                    }
                                    if (granteeGrantee.IsSetURI())
                                    {
                                        xmlWriter.WriteElementString("URI", "",
                                                                     S3Transforms.ToXmlStringValue(
                                                                         granteeGrantee.URI));
                                    }
                                    xmlWriter.WriteEndElement();
                                }

                                if (accessControlPolicyAccessControlPolicygrantsListValue.IsSetPermission())
                                {
                                    xmlWriter.WriteElementString("Permission", "",
                                                                 S3Transforms.ToXmlStringValue(
                                                                     accessControlPolicyAccessControlPolicygrantsListValue
                                                                         .Permission));
                                }
                            }
                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();

                        //
                    }

                    xmlWriter.WriteEndElement();
                }
            }

            try
            {
                var content = stringWriter.ToString();

                request.Content = Encoding.UTF8.GetBytes(content);
                request.Headers[HeaderKeys.ContentTypeHeader] = "application/xml";

                string checksum = ObsS3Util.GenerateChecksumForContent(content, true);
                request.Headers[HeaderKeys.ContentMD5Header] = checksum;
                //2015-5-26 w00322557
                foreach (var head in request.Headers)
                {
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutACLRequestMarshaller request header key {0}, value {1}", head.Key, head.Value));
                }    
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutACLRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

