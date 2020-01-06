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
using System.Collections.Generic;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Bucket Cors Request Marshaller
    /// </summary>       
    public class PutCORSConfigurationRequestMarshaller : IMarshaller<IRequest, PutCORSConfigurationRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((PutCORSConfigurationRequest)input);
		}

        public IRequest Marshall(PutCORSConfigurationRequest putCORSConfigurationRequest)
        {
            IRequest request = new DefaultRequest(putCORSConfigurationRequest, "ObsS3");

            request.HttpMethod = "PUT";
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutCORSConfigurationRequestMarshaller HttpMethod: {0}",request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(putCORSConfigurationRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutCORSConfigurationRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("cors");

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                var configuration = putCORSConfigurationRequest.Configuration;
                if (configuration != null)
                {
                    xmlWriter.WriteStartElement("CORSConfiguration", "");

                    if (configuration != null)
                    {
                        var cORSConfigurationCORSConfigurationcORSRulesList = configuration.Rules;
                        if (cORSConfigurationCORSConfigurationcORSRulesList != null && cORSConfigurationCORSConfigurationcORSRulesList.Count > 0)
                        {
                            foreach (var cORSConfigurationCORSConfigurationcORSRulesListValue in cORSConfigurationCORSConfigurationcORSRulesList)
                            {
                                xmlWriter.WriteStartElement("CORSRule", "");

                                //id
                                if (cORSConfigurationCORSConfigurationcORSRulesListValue.IsSetId())
                                {
                                    xmlWriter.WriteElementString("ID", "", S3Transforms.ToXmlStringValue(cORSConfigurationCORSConfigurationcORSRulesListValue.Id));
                                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ID: {0}", S3Transforms.ToXmlStringValue(cORSConfigurationCORSConfigurationcORSRulesListValue.Id)));
                                }

                                if (cORSConfigurationCORSConfigurationcORSRulesListValue != null)
                                {
                                    var cORSRuleMemberallowedMethodsList = cORSConfigurationCORSConfigurationcORSRulesListValue.AllowedMethods;
                                    if (cORSRuleMemberallowedMethodsList != null && cORSRuleMemberallowedMethodsList.Count > 0)
                                    {
                                        foreach (string cORSRuleMemberallowedMethodsListValue in cORSRuleMemberallowedMethodsList)
                                        {
                                            xmlWriter.WriteStartElement("AllowedMethod", "");
                                            xmlWriter.WriteValue(cORSRuleMemberallowedMethodsListValue);
                                            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("AllowedMethod: {0}", cORSRuleMemberallowedMethodsListValue));
                                            xmlWriter.WriteEndElement();
                                        }
                                    }
                                }

                                if (cORSConfigurationCORSConfigurationcORSRulesListValue != null)
                                {
                                    var cORSRuleMemberallowedOriginsList = cORSConfigurationCORSConfigurationcORSRulesListValue.AllowedOrigins;
                                    if (cORSRuleMemberallowedOriginsList != null && cORSRuleMemberallowedOriginsList.Count > 0)
                                    {
                                        foreach (string cORSRuleMemberallowedOriginsListValue in cORSRuleMemberallowedOriginsList)
                                        {
                                            xmlWriter.WriteStartElement("AllowedOrigin", "");
                                            xmlWriter.WriteValue(cORSRuleMemberallowedOriginsListValue);
                                            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("AllowedOrigin: {0}", cORSRuleMemberallowedOriginsListValue));
                                            xmlWriter.WriteEndElement();
                                        }
                                    }
                                }

                                if (cORSConfigurationCORSConfigurationcORSRulesListValue != null)
                                {
                                    var cORSRuleMemberallowedHeadersList = cORSConfigurationCORSConfigurationcORSRulesListValue.AllowedHeaders;
                                    if (cORSRuleMemberallowedHeadersList != null && cORSRuleMemberallowedHeadersList.Count > 0)
                                    {
                                        foreach (string cORSRuleMemberallowedHeadersListValue in cORSRuleMemberallowedHeadersList)
                                        {
                                            xmlWriter.WriteStartElement("AllowedHeader", "");
                                            xmlWriter.WriteValue(cORSRuleMemberallowedHeadersListValue);
                                            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("AllowedHeader: {0}", cORSRuleMemberallowedHeadersListValue));
                                            xmlWriter.WriteEndElement();
                                        }
                                    }
                                }
                                
                                
                                if (cORSConfigurationCORSConfigurationcORSRulesListValue.IsSetMaxAgeSeconds())
                                {
                                    xmlWriter.WriteElementString("MaxAgeSeconds", "", S3Transforms.ToXmlStringValue(cORSConfigurationCORSConfigurationcORSRulesListValue.MaxAgeSeconds));
                                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MaxAgeSeconds: {0}", S3Transforms.ToXmlStringValue(cORSConfigurationCORSConfigurationcORSRulesListValue.MaxAgeSeconds)));
                                }

                                if (cORSConfigurationCORSConfigurationcORSRulesListValue != null)
                                {
                                    var cORSRuleMemberexposeHeadersList = cORSConfigurationCORSConfigurationcORSRulesListValue.ExposeHeaders;
                                    if (cORSRuleMemberexposeHeadersList != null && cORSRuleMemberexposeHeadersList.Count > 0)
                                    {
                                        foreach (string cORSRuleMemberexposeHeadersListValue in cORSRuleMemberexposeHeadersList)
                                        {
                                            xmlWriter.WriteStartElement("ExposeHeader", "");
                                            xmlWriter.WriteValue(cORSRuleMemberexposeHeadersListValue);
                                            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ExposeHeader: {0}", cORSRuleMemberexposeHeadersListValue));
                                            xmlWriter.WriteEndElement();
                                        }
                                    }
                                }

                                xmlWriter.WriteEndElement();
                            }
                        }
                    }
                    xmlWriter.WriteEndElement();
                }
            }


            try
            {
                var content = stringWriter.ToString();
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutCORSConfigurationRequestMarshaller content: {0}",content));

                request.Content = Encoding.UTF8.GetBytes(content);
                request.Headers[HeaderKeys.ContentTypeHeader] = "application/xml";

                var checksum = ObsS3Util.GenerateChecksumForContent(content, true);
                request.Headers[HeaderKeys.ContentMD5Header] = checksum;
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutCORSConfigurationRequestMarshaller exception: {0}",e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

