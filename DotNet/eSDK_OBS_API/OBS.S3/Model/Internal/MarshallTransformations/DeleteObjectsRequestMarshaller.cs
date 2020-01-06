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
using OBS.Runtime.Internal.Util;
using OBS.Runtime.Internal.Transform;
using System.Globalization;
using OBS.Util;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Delete Objects Request Marshaller
    /// </summary>       
    public class DeleteObjectsRequestMarshaller : IMarshaller<IRequest, DeleteObjectsRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((DeleteObjectsRequest)input);
		}

        public IRequest Marshall(DeleteObjectsRequest deleteObjectsRequest)
        {
            IRequest request = new DefaultRequest(deleteObjectsRequest, "ObsS3");

            request.HttpMethod = "POST";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("DeleteObjectsRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (deleteObjectsRequest.IsSetMfaCodes())
                request.Headers.Add(HeaderKeys.XAmzMfaHeader, deleteObjectsRequest.MfaCodes.FormattedMfaCodes);

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(deleteObjectsRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("DeleteObjectsRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("delete");

            var stringWriter = new StringWriter(CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                xmlWriter.WriteStartElement("Delete", "");

                if (deleteObjectsRequest.IsSetQuiet())
                {
                    xmlWriter.WriteElementString("Quiet", "", deleteObjectsRequest.Quiet.ToString().ToLower(CultureInfo.InvariantCulture));
                }

                var deleteDeleteobjectsList = deleteObjectsRequest.Objects;
                if (deleteDeleteobjectsList != null && deleteDeleteobjectsList.Count > 0)
                {
                    foreach (var deleteDeleteobjectsListValue in deleteDeleteobjectsList)
                    {
                        xmlWriter.WriteStartElement("Object", "");
                        if (deleteDeleteobjectsListValue.IsSetKey())
                        {
                            xmlWriter.WriteElementString("Key", "", S3Transforms.ToXmlStringValue(deleteDeleteobjectsListValue.Key));
                        }
                        if (deleteDeleteobjectsListValue.IsSetVersionId())
                        {
                            xmlWriter.WriteElementString("VersionId", "", S3Transforms.ToXmlStringValue(deleteDeleteobjectsListValue.VersionId));
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

                var checksum = ObsS3Util.GenerateChecksumForContent(content, true);
                request.Headers[HeaderKeys.ContentMD5Header] = checksum;

                //2015-5-26 w00322557
                foreach (var head in request.Headers)
                {
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("DeleteObjectsRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
                } 
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("DeleteObjectsRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

