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

using OBS.S3.Model;
using OBS.Runtime.Internal.Transform;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
     /// <summary>
     ///   UploadsItem Unmarshaller
     /// </summary>
    internal class MultipartUploadUnmarshaller : IUnmarshaller<MultipartUpload, XmlUnmarshallerContext>, IUnmarshaller<MultipartUpload, JsonUnmarshallerContext> 
    {
        

        public MultipartUpload Unmarshall(XmlUnmarshallerContext context) 
        {
            MultipartUpload uploadsItem = new MultipartUpload();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Initiated", targetDepth))
                    {
                        uploadsItem.Initiated = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller IsTruncated: {0}", uploadsItem.Initiated.ToLongDateString()));
                        continue;
                    }
                    if (context.TestExpression("Initiator", targetDepth))
                    {
                        uploadsItem.Initiator = InitiatorUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller initiator display name: {0}", uploadsItem.Initiator.DisplayName));
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller initiator id: {0}", uploadsItem.Initiator.Id));
                        continue;
                    }
                    if (context.TestExpression("Key", targetDepth))
                    {
                        uploadsItem.Key = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller Key: {0}", uploadsItem.Key));
                        continue;
                    }
                    if (context.TestExpression("Owner", targetDepth))
                    {
                        uploadsItem.Owner = OwnerUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller Owner display name: {0}", uploadsItem.Owner.DisplayName));
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller Owner Id: {0}", uploadsItem.Owner.Id));
                        continue;
                    }
                    if (context.TestExpression("StorageClass", targetDepth))
                    {
                        uploadsItem.StorageClass = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller StorageClass: {0}", uploadsItem.StorageClass.Value));
                        continue;
                    }
                    if (context.TestExpression("UploadId", targetDepth))
                    {
                        uploadsItem.UploadId = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("MultipartUploadUnmarshaller UploadId: {0}", uploadsItem.UploadId));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return uploadsItem;
                }
            }
                        


            return uploadsItem;
        }

        public MultipartUpload Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static MultipartUploadUnmarshaller _instance;

        public static MultipartUploadUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MultipartUploadUnmarshaller();
                }
                return _instance;
            }
        }

    }
}
    
