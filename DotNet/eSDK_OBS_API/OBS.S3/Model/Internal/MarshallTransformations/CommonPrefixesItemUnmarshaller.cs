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
     ///   CommonPrefixesItem Unmarshaller
     /// </summary>
    internal class CommonPrefixesItemUnmarshaller : IUnmarshaller<string, XmlUnmarshallerContext>, IUnmarshaller<string, JsonUnmarshallerContext> 
    {
        public string Unmarshall(XmlUnmarshallerContext context) 
        {
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;

            string prefix = null;
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Prefix", targetDepth))
                    {
                        prefix = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CommonPrefixesItemUnmarshaller Prefix: {0}", prefix));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return prefix;
                }
            }



            return prefix;
        }

        public string Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static CommonPrefixesItemUnmarshaller _instance;

        public static CommonPrefixesItemUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CommonPrefixesItemUnmarshaller();
                }
                return _instance;
            }
        }

    }
}
    
