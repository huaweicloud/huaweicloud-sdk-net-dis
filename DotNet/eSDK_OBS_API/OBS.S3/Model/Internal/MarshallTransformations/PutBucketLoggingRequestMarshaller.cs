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
using OBS.Util;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Enable Bucket Logging Request Marshaller
    /// </summary>       
    public class PutBucketLoggingRequestMarshaller : IMarshaller<IRequest, PutBucketLoggingRequest>, IMarshaller<IRequest, OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PutBucketLoggingRequest)input);
		}

        public IRequest Marshall(PutBucketLoggingRequest putBucketLoggingRequest)
        {
            IRequest request = new DefaultRequest(putBucketLoggingRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketLoggingRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(putBucketLoggingRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketLoggingRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("logging");

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);

            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = System.Text.Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                xmlWriter.WriteStartElement("BucketLoggingStatus", "");
                var bucketLoggingStatusBucketLoggingStatus = putBucketLoggingRequest.LoggingConfig;
                if (bucketLoggingStatusBucketLoggingStatus != null)
                {
                    if (bucketLoggingStatusBucketLoggingStatus != null)
                    {
                        var loggingEnabledLoggingEnabled = bucketLoggingStatusBucketLoggingStatus;
                        if (loggingEnabledLoggingEnabled != null && loggingEnabledLoggingEnabled.IsSetTargetBucket())
                        {
                            xmlWriter.WriteStartElement("LoggingEnabled", "");
                            xmlWriter.WriteElementString("TargetBucket", "", S3Transforms.ToXmlStringValue(loggingEnabledLoggingEnabled.TargetBucketName));

                            //w00322557
                            if (loggingEnabledLoggingEnabled.IsSetTargetPrefix())
                            {
                                xmlWriter.WriteElementString("TargetPrefix", "", S3Transforms.ToXmlStringValue(loggingEnabledLoggingEnabled.TargetPrefix));
                            }
                            else
                            {
                                xmlWriter.WriteStartElement("TargetPrefix");
                                xmlWriter.WriteEndElement();
                            }
                            

                            var loggingEnabledLoggingEnabledtargetGrantsList = loggingEnabledLoggingEnabled.Grants;
                            if (loggingEnabledLoggingEnabledtargetGrantsList != null && loggingEnabledLoggingEnabledtargetGrantsList.Count > 0)
                            {
                                xmlWriter.WriteStartElement("TargetGrants", "");
                                foreach (var loggingEnabledLoggingEnabledtargetGrantsListValue in loggingEnabledLoggingEnabledtargetGrantsList)
                                {
                                    xmlWriter.WriteStartElement("Grant", "");
                                    if (loggingEnabledLoggingEnabledtargetGrantsListValue != null)
                                    {
                                        var granteeGrantee = loggingEnabledLoggingEnabledtargetGrantsListValue.Grantee;
                                        if (granteeGrantee != null)
                                        {
                                            xmlWriter.WriteStartElement("Grantee", "");
                                            if (granteeGrantee.IsSetType())
                                            {
                                                xmlWriter.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", granteeGrantee.Type.ToString());
                                            }
                                            if (granteeGrantee.IsSetDisplayName())
                                            {
                                                xmlWriter.WriteElementString("DisplayName", "", S3Transforms.ToXmlStringValue(granteeGrantee.DisplayName));
                                            }
                                            if (granteeGrantee.IsSetEmailAddress())
                                            {
                                                xmlWriter.WriteElementString("EmailAddress", "", S3Transforms.ToXmlStringValue(granteeGrantee.EmailAddress));
                                            }
                                            if (granteeGrantee.IsSetCanonicalUser())
                                            {
                                                xmlWriter.WriteElementString("ID", "", S3Transforms.ToXmlStringValue(granteeGrantee.CanonicalUser));
                                            }
                                            if (granteeGrantee.IsSetURI())
                                            {
                                                xmlWriter.WriteElementString("URI", "", S3Transforms.ToXmlStringValue(granteeGrantee.URI));
                                            }
                                            xmlWriter.WriteEndElement();
                                        }

                                        if (loggingEnabledLoggingEnabledtargetGrantsListValue.IsSetPermission())
                                        {
                                            xmlWriter.WriteElementString("Permission", "", S3Transforms.ToXmlStringValue(loggingEnabledLoggingEnabledtargetGrantsListValue.Permission));
                                        }
                                    }

                                    xmlWriter.WriteEndElement();
                                }
                                xmlWriter.WriteEndElement();
                            }
                            //w00322557 origin
                            xmlWriter.WriteEndElement();
                        }
                    }
                }

                xmlWriter.WriteEndElement();
            }

            try
            {
                var content = stringWriter.ToString();

                request.Content = Encoding.UTF8.GetBytes(content);
                request.Headers[HeaderKeys.ContentTypeHeader] = "application/xml";

                var checksum = ObsS3Util.GenerateChecksumForContent(content, true);
                request.Headers[HeaderKeys.ContentMD5Header] = checksum;

                //2015-5-26 w00322557
                foreach (var head in request.Headers)
                {
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketLoggingRequestMarshaller request header key {0}, value {1}", head.Key, head.Value));
                }     
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutBucketLoggingRequestMarshaller exception: {0}", e.Message));

                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

