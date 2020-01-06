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
    /// Complete Multipart Upload Request Marshaller
    /// </summary>       
    public class CompleteMultipartUploadRequestMarshaller : IMarshaller<IRequest, CompleteMultipartUploadRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((CompleteMultipartUploadRequest)input);
		}

        public IRequest Marshall(CompleteMultipartUploadRequest completeMultipartUploadRequest)
        {
            IRequest request = new DefaultRequest(completeMultipartUploadRequest, "ObsS3");

            request.HttpMethod = "POST";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CompleteMultipartUploadRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(completeMultipartUploadRequest.BucketName),
                                                 S3Transforms.ToStringValue(completeMultipartUploadRequest.Key));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CompleteMultipartUploadRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("uploadId", S3Transforms.ToStringValue(completeMultipartUploadRequest.UploadId));

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                xmlWriter.WriteStartElement("CompleteMultipartUpload", "");
                var multipartUploadMultipartUploadpartsList = completeMultipartUploadRequest.PartETags;

                if (multipartUploadMultipartUploadpartsList != null && multipartUploadMultipartUploadpartsList.Count > 0)
                {
                    foreach (var multipartUploadMultipartUploadpartsListValue in multipartUploadMultipartUploadpartsList)
                    {
                        xmlWriter.WriteStartElement("Part", "");
                        if (multipartUploadMultipartUploadpartsListValue.IsSetPartNumber())
                        {
                            xmlWriter.WriteElementString("PartNumber", "", S3Transforms.ToXmlStringValue(multipartUploadMultipartUploadpartsListValue.PartNumber));
                        }

                        if (multipartUploadMultipartUploadpartsListValue.IsSetETag())
                        {
                            xmlWriter.WriteElementString("ETag", "", S3Transforms.ToXmlStringValue(multipartUploadMultipartUploadpartsListValue.ETag));
                        }
                        
                        xmlWriter.WriteEndElement();
                    }
                }
                xmlWriter.WriteEndElement();
            }

            try
            {
                var content = stringWriter.ToString();

                request.Content = Encoding.UTF8.GetBytes(content);
                request.Headers[HeaderKeys.ContentTypeHeader] = "application/xml";
                //2015-5-26 
                foreach (var head in request.Headers)
                {
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CompleteMultipartUploadRequestMarshaller request header: key {0}, value: {1}", head.Key, head.Value));
                }

            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("CompleteMultipartUploadRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

