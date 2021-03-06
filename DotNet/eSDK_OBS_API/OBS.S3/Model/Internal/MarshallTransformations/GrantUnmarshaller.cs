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
     ///   Grant Unmarshaller
     /// </summary>
    internal class GrantUnmarshaller : IUnmarshaller<S3Grant, XmlUnmarshallerContext>, IUnmarshaller<S3Grant, JsonUnmarshallerContext> 
    {
        public S3Grant Unmarshall(XmlUnmarshallerContext context) 
        {
            S3Grant grant = new S3Grant();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Grantee", targetDepth))
                    {
                        grant.Grantee = GranteeUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GrantUnmarshaller grant grantee."));
                        continue;
                    }
                    if (context.TestExpression("Permission", targetDepth))
                    {
                        grant.Permission = StringUnmarshaller.GetInstance().Unmarshall(context);
                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GrantUnmarshaller grant permission: {0}", grant.Permission.Value));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return grant;
                }
            }
                        


            return grant;
        }

        public S3Grant Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static GrantUnmarshaller _instance;

        public static GrantUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GrantUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
