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
     ///   Owner Unmarshaller
     /// </summary>
    internal class OwnerUnmarshaller : IUnmarshaller<Owner, XmlUnmarshallerContext>, IUnmarshaller<Owner, JsonUnmarshallerContext> 
    {
        

        public Owner Unmarshall(XmlUnmarshallerContext context) 
        {
            Owner owner = new Owner();
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
                        owner.DisplayName = StringUnmarshaller.GetInstance().Unmarshall(context);
                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("OwnerUnmarshaller owner DisplayName: {0}", owner.DisplayName));
                        continue;
                    }
                    if (context.TestExpression("ID", targetDepth))
                    {
                        owner.Id = StringUnmarshaller.GetInstance().Unmarshall(context);
                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("OwnerUnmarshaller owner Id: {0}", owner.Id));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return owner;
                }
            }
                        


            return owner;
        }

        public Owner Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static OwnerUnmarshaller _instance;

        public static OwnerUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OwnerUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
