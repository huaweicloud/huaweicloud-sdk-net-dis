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
     ///   PartsItem Unmarshaller
     /// </summary>
    internal class PartDetailUnmarshaller : IUnmarshaller<PartDetail, XmlUnmarshallerContext>, IUnmarshaller<PartDetail, JsonUnmarshallerContext> 
    {
        

        public PartDetail Unmarshall(XmlUnmarshallerContext context) 
        {
            PartDetail partsItem = new PartDetail();
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
                        partsItem.ETag = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PartDetailUnmarshaller ETag: {0}",partsItem.ETag));
                        continue;
                    }
                    if (context.TestExpression("LastModified", targetDepth))
                    {
                        partsItem.LastModified = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PartDetailUnmarshaller LastModified: {0}", partsItem.LastModified.ToLongDateString()));
                        continue;
                    }
                    if (context.TestExpression("PartNumber", targetDepth))
                    {
                        partsItem.PartNumber = IntUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PartDetailUnmarshaller PartNumber: {0}", partsItem.PartNumber));
                        continue;
                    }
                    if (context.TestExpression("Size", targetDepth))
                    {
                        partsItem.Size = IntUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PartDetailUnmarshaller Size: {0}", partsItem.Size));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return partsItem;
                }
            }
                        


            return partsItem;
        }

        public PartDetail Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static PartDetailUnmarshaller _instance;

        public static PartDetailUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PartDetailUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
