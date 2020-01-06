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
    /// Put Bucket Website Request Marshaller
    /// </summary>       
    public class PutBucketWebsiteRequestMarshaller : IMarshaller<IRequest, PutBucketWebsiteRequest>, IMarshaller<IRequest, OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PutBucketWebsiteRequest)input);
		}

        public IRequest Marshall(PutBucketWebsiteRequest putBucketWebsiteRequest)
        {
            IRequest request = new DefaultRequest(putBucketWebsiteRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketWebsiteRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(putBucketWebsiteRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketWebsiteRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("website");

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                var websiteConfigurationWebsiteConfiguration = putBucketWebsiteRequest.WebsiteConfiguration;
                if (websiteConfigurationWebsiteConfiguration != null)
                {
                    xmlWriter.WriteStartElement("WebsiteConfiguration", "");
                    if (websiteConfigurationWebsiteConfiguration != null)
                    {
                        string errorDocumentErrorDocument = websiteConfigurationWebsiteConfiguration.ErrorDocument;
                        if (errorDocumentErrorDocument != null)
                        {
                            xmlWriter.WriteStartElement("ErrorDocument", "");
                            xmlWriter.WriteElementString("Key", "", S3Transforms.ToXmlStringValue(errorDocumentErrorDocument));
                            xmlWriter.WriteEndElement();
                        }
                    }
                    if (websiteConfigurationWebsiteConfiguration != null)
                    {
                        string indexDocumentIndexDocument = websiteConfigurationWebsiteConfiguration.IndexDocumentSuffix;
                        if (indexDocumentIndexDocument != null)
                        {
                            xmlWriter.WriteStartElement("IndexDocument", "");
                            xmlWriter.WriteElementString("Suffix", "", S3Transforms.ToXmlStringValue(indexDocumentIndexDocument));
                            xmlWriter.WriteEndElement();
                        }
                    }
                    if (websiteConfigurationWebsiteConfiguration != null)
                    {
                        var redirectAllRequestsToRedirectAllRequestsTo = websiteConfigurationWebsiteConfiguration.RedirectAllRequestsTo;
                        if (redirectAllRequestsToRedirectAllRequestsTo != null)
                        {
                            xmlWriter.WriteStartElement("RedirectAllRequestsTo", "");
                            if (redirectAllRequestsToRedirectAllRequestsTo.IsSetHostName())
                            {
                                xmlWriter.WriteElementString("HostName", "", S3Transforms.ToXmlStringValue(redirectAllRequestsToRedirectAllRequestsTo.HostName));
                            }
                            if (redirectAllRequestsToRedirectAllRequestsTo.IsSetProtocol())
                            {
                                xmlWriter.WriteElementString("Protocol", "", S3Transforms.ToXmlStringValue(redirectAllRequestsToRedirectAllRequestsTo.Protocol));
                            }

                            xmlWriter.WriteEndElement();                            
                        }
                    }

                    if (websiteConfigurationWebsiteConfiguration != null)
                    {
                        var websiteConfigurationWebsiteConfigurationroutingRulesList = websiteConfigurationWebsiteConfiguration.RoutingRules;
                        if (websiteConfigurationWebsiteConfigurationroutingRulesList != null && websiteConfigurationWebsiteConfigurationroutingRulesList.Count > 0)
                        {
                            xmlWriter.WriteStartElement("RoutingRules", "");
                            foreach (var websiteConfigurationWebsiteConfigurationroutingRulesListValue in websiteConfigurationWebsiteConfigurationroutingRulesList)
                            {
                                xmlWriter.WriteStartElement("RoutingRule", "");
                                if (websiteConfigurationWebsiteConfigurationroutingRulesListValue != null)
                                {
                                    var conditionCondition = websiteConfigurationWebsiteConfigurationroutingRulesListValue.Condition;
                                    if (conditionCondition != null)
                                    {
                                        xmlWriter.WriteStartElement("Condition", "");
                                        if (conditionCondition.IsSetHttpErrorCodeReturnedEquals())
                                        {
                                            xmlWriter.WriteElementString("HttpErrorCodeReturnedEquals", "", S3Transforms.ToXmlStringValue(conditionCondition.HttpErrorCodeReturnedEquals));
                                        }
                                        if (conditionCondition.IsSetKeyPrefixEquals())
                                        {
                                            xmlWriter.WriteElementString("KeyPrefixEquals", "", S3Transforms.ToXmlStringValue(conditionCondition.KeyPrefixEquals));
                                        }
                                        xmlWriter.WriteEndElement();
                                    }
                                }
                                if (websiteConfigurationWebsiteConfigurationroutingRulesListValue != null)
                                {
                                    var redirectRedirect = websiteConfigurationWebsiteConfigurationroutingRulesListValue.Redirect;
                                    if (redirectRedirect != null)
                                    {
                                        xmlWriter.WriteStartElement("Redirect", "");
                                        if (redirectRedirect.IsSetHostName())
                                        {
                                            xmlWriter.WriteElementString("HostName", "", S3Transforms.ToXmlStringValue(redirectRedirect.HostName));
                                        }
                                        if (redirectRedirect.IsSetHttpRedirectCode())
                                        {
                                            xmlWriter.WriteElementString("HttpRedirectCode", "", S3Transforms.ToXmlStringValue(redirectRedirect.HttpRedirectCode));
                                        }
                                        if (redirectRedirect.IsSetProtocol())
                                        {
                                            xmlWriter.WriteElementString("Protocol", "", S3Transforms.ToXmlStringValue(redirectRedirect.Protocol));
                                        }
                                        if (redirectRedirect.IsSetReplaceKeyPrefixWith())
                                        {
                                            xmlWriter.WriteElementString("ReplaceKeyPrefixWith", "", S3Transforms.ToXmlStringValue(redirectRedirect.ReplaceKeyPrefixWith));
                                        }
                                        if (redirectRedirect.IsSetReplaceKeyWith())
                                        {
                                            xmlWriter.WriteElementString("ReplaceKeyWith", "", S3Transforms.ToXmlStringValue(redirectRedirect.ReplaceKeyWith));
                                        }
                                        xmlWriter.WriteEndElement();
                                    }
                                }
                                xmlWriter.WriteEndElement();
                            }
                            xmlWriter.WriteEndElement();
                        }
                    }
                    xmlWriter.WriteEndElement();
                }
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
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketWebsiteRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
                }
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketWebsiteRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

