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
     ///   Initiator Unmarshaller
     /// </summary>
    internal class InitiatorUnmarshaller : IUnmarshaller<Initiator, XmlUnmarshallerContext>, IUnmarshaller<Initiator, JsonUnmarshallerContext> 
    {
        public Initiator Unmarshall(XmlUnmarshallerContext context) 
        {
            Initiator initiator = new Initiator();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("DisplayName", targetDepth))
                    {
                        initiator.DisplayName = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("InitiatorUnmarshaller DisplayName: {0}", initiator.DisplayName));
                        continue;
                    }
                    if (context.TestExpression("ID", targetDepth))
                    {
                        initiator.Id = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("InitiatorUnmarshaller ID: {0}", initiator.Id));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return initiator;
                }
            }
                        


            return initiator;
        }

        public Initiator Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static InitiatorUnmarshaller _instance;

        public static InitiatorUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InitiatorUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
