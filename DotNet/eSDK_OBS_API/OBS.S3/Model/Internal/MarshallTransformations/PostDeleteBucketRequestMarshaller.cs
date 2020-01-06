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

using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Text;
using OBS.Runtime;
using OBS.Util;
using OBS.S3.Util;
using System.IO;
using System.Xml;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Get Bucket Website Request Marshaller
    /// </summary>       
    public class PostDeleteBucketRequestMarshaller : IMarshaller<IRequest, PostDeleteBucketRequest>, IMarshaller<IRequest, OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PostDeleteBucketRequest)input);
		}

        public IRequest Marshall(PostDeleteBucketRequest postDeleteBucketRequest)
        {
            IRequest request = new DefaultRequest(postDeleteBucketRequest, "ObsS3");

            request.HttpMethod = "POST";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PostDeleteBucketRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(postDeleteBucketRequest.BucketName));
            request.AddSubResource("deletebucket");

            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PostDeleteBucketRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                var requestDeleteBuckeRequestDeleteBucket = postDeleteBucketRequest.BucketName;
                if (requestDeleteBuckeRequestDeleteBucket != null)
                {
                    xmlWriter.WriteStartElement("DeleteBucket", "");
                    if (requestDeleteBuckeRequestDeleteBucket != null)
                    {
                        xmlWriter.WriteElementString("Bucket", "", S3Transforms.ToXmlStringValue(requestDeleteBuckeRequestDeleteBucket));
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
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PostDeleteBucketRequestMarshaller request header key {0}, value {1}", head.Key, head.Value));
                }
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PostDeleteBucketRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }
            
            return request;
        }
    }
}
