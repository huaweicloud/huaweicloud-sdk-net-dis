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

namespace OBS.S3.Model.Internal.MarshallTransformations
{
     /// <summary>
     ///   ErrorsItem Unmarshaller
     /// </summary>
    internal class ErrorsItemUnmarshaller : IUnmarshaller<DeleteError, XmlUnmarshallerContext>, IUnmarshaller<DeleteError, JsonUnmarshallerContext> 
    {
        public DeleteError Unmarshall(XmlUnmarshallerContext context) 
        {
            DeleteError errorsItem = new DeleteError();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Code", targetDepth))
                    {
                        errorsItem.Code = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ErrorsItemUnmarshaller Code: {0}", errorsItem.Code));
                        continue;
                    }
                    if (context.TestExpression("Key", targetDepth))
                    {
                        errorsItem.Key = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ErrorsItemUnmarshaller Key: {0}", errorsItem.Key));
                        continue;
                    }
                    if (context.TestExpression("Message", targetDepth))
                    {
                        errorsItem.Message = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ErrorsItemUnmarshaller Message: {0}", errorsItem.Message));
                        continue;
                    }
                    if (context.TestExpression("VersionId", targetDepth))
                    {
                        errorsItem.VersionId = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ErrorsItemUnmarshaller VersionId: {0}", errorsItem.VersionId));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return errorsItem;
                }
            }
            
            return errorsItem;
        }

        public DeleteError Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static ErrorsItemUnmarshaller _instance;

        public static ErrorsItemUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ErrorsItemUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
