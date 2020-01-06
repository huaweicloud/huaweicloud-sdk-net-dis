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
     ///   ContentsItem Unmarshaller
     /// </summary>
    internal class ContentsItemUnmarshaller : IUnmarshaller<S3Object, XmlUnmarshallerContext>, IUnmarshaller<S3Object, JsonUnmarshallerContext> 
    {
        public S3Object Unmarshall(XmlUnmarshallerContext context) 
        {
            S3Object contentsItem = new S3Object();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("ETag", targetDepth))
                    {
                        contentsItem.ETag = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ContentsItemUnmarshaller ETag: {0}", contentsItem.ETag));
                        continue;
                    }
                    if (context.TestExpression("Key", targetDepth))
                    {
                        contentsItem.Key = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ContentsItemUnmarshaller Key: {0}", contentsItem.Key));
                        continue;
                    }
                    if (context.TestExpression("LastModified", targetDepth))
                    {
                        contentsItem.LastModified = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ContentsItemUnmarshaller LastModified: {0}", contentsItem.LastModified.ToShortDateString()));
                        continue;
                    }
                    if (context.TestExpression("Owner", targetDepth))
                    {
                        contentsItem.Owner = OwnerUnmarshaller.Instance.Unmarshall(context);                        
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,"ContentsItemUnmarshaller Owner.");
                        continue;
                    }
                    if (context.TestExpression("Size", targetDepth))
                    {
                        contentsItem.Size = LongUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ContentsItemUnmarshaller Size: {0}", contentsItem.Size));
                        continue;
                    }
                    if (context.TestExpression("StorageClass", targetDepth))
                    {
                        contentsItem.StorageClass = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ContentsItemUnmarshaller StorageClass: {0}", contentsItem.StorageClass.Value));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return contentsItem;
                }
            }
               
            return contentsItem;
        }

        public S3Object Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static ContentsItemUnmarshaller _instance;

        public static ContentsItemUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContentsItemUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
