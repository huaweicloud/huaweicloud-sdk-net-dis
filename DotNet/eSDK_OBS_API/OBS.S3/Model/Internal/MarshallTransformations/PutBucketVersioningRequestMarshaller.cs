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
    /// Put Bucket Versioning Request Marshaller
    /// </summary>       
    public class PutBucketVersioningRequestMarshaller : IMarshaller<IRequest, PutBucketVersioningRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PutBucketVersioningRequest)input);
		}

        public IRequest Marshall(PutBucketVersioningRequest putBucketVersioningRequest)
        {
            IRequest request = new DefaultRequest(putBucketVersioningRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketVersioningRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (putBucketVersioningRequest.IsSetMfaCodes())
                request.Headers.Add(HeaderKeys.XAmzMfaHeader, putBucketVersioningRequest.MfaCodes.FormattedMfaCodes);

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(putBucketVersioningRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketVersioningRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("versioning");

            var stringWriter = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Encoding = Encoding.UTF8, OmitXmlDeclaration = true }))
            {
                var versioningConfigurationVersioningConfiguration = putBucketVersioningRequest.VersioningConfig;
                if (versioningConfigurationVersioningConfiguration != null)
                {
                    xmlWriter.WriteStartElement("VersioningConfiguration", "");
                    if (versioningConfigurationVersioningConfiguration.IsSetEnableMfaDelete())
                    {
                        xmlWriter.WriteElementString("MFADelete", "", versioningConfigurationVersioningConfiguration.EnableMfaDelete ? "Enabled" : "Disabled");
                    }
                    if (versioningConfigurationVersioningConfiguration.IsSetStatus())
                    {
                        xmlWriter.WriteElementString("Status", "", S3Transforms.ToXmlStringValue(versioningConfigurationVersioningConfiguration.Status));
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
                    LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketVersioningRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
                }
            }
            catch (EncoderFallbackException e)
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketVersioningRequestMarshaller exception: {0}", e.Message));
                throw new ObsServiceException("Unable to marshall request to XML", e);
            }

            return request;
        }
    }
}

