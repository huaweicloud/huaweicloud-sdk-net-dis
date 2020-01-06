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
using eSDK_OBS_API.OBS.Util;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Bucket Notification Request Marshaller
    /// </summary>       
    public class PutBucketNotificationRequestMarshaller : IMarshaller<IRequest, PutBucketNotificationRequest>, IMarshaller<IRequest, OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PutBucketNotificationRequest)input);
		}

        public IRequest Marshall(PutBucketNotificationRequest putBucketnotificationRequest)
        {
            IRequest request = new DefaultRequest(putBucketnotificationRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("PutBucketNotificationRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(putBucketnotificationRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("PutBucketNotificationRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("notification");

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                var notificationConfiguration = putBucketnotificationRequest.NotificationConfiguration;
                if (notificationConfiguration != null)
                {
                        xmlWriter.WriteStartElement("NotificationConfiguration", "");
                            var topicList = notificationConfiguration.TopicConfiguration;
                    if(topicList != null && topicList.Count > 0){
                        foreach (var topicConfiguration in topicList){
                                xmlWriter.WriteStartElement("TopicConfiguration", "");
                                if (topicConfiguration.IsSetId())
                                {
                                    xmlWriter.WriteElementString("Id", "", S3Transforms.ToXmlStringValue(topicConfiguration.Id));
                                }
                            if(topicConfiguration.IsSetTopic()){
                                xmlWriter.WriteElementString("Topic", "", S3Transforms.ToXmlStringValue(topicConfiguration.Topic));
                            }
                            if (topicConfiguration.Events != null && topicConfiguration.Events.Count > 0)
                            {
                                foreach (var eventType in topicConfiguration.Events)
                                {
                                    xmlWriter.WriteElementString("Event", "", S3Transforms.ToXmlStringValue(eventType));
                                }
                            }
                            if (topicConfiguration.filterRuleList != null && topicConfiguration.filterRuleList.Count > 0)
                            {
                                xmlWriter.WriteStartElement("Filter", "");
                                xmlWriter.WriteStartElement("S3Key", "");
                                foreach (var filterRule in topicConfiguration.filterRuleList)
                                {
                                    xmlWriter.WriteStartElement("FilterRule", "");
                                    xmlWriter.WriteElementString("Name", "", S3Transforms.ToXmlStringValue(filterRule.Name));
                                    xmlWriter.WriteElementString("Value", "", S3Transforms.ToXmlStringValue(filterRule.Value));
                                    xmlWriter.WriteEndElement();
                                }
                                xmlWriter.WriteEndElement();
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

