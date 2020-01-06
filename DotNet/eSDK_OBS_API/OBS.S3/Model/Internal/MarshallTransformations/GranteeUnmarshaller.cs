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
     ///   Grantee Unmarshaller
     /// </summary>
    internal class GranteeUnmarshaller : IUnmarshaller<S3Grantee, XmlUnmarshallerContext>, IUnmarshaller<S3Grantee, JsonUnmarshallerContext> 
    {
        public S3Grantee Unmarshall(XmlUnmarshallerContext context) 
        {
            S3Grantee grantee = new S3Grantee();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("@Type", targetDepth - 1))
                    {                        
                        continue;
                    }
                    if (context.TestExpression("DisplayName", targetDepth))
                    {
                        grantee.DisplayName = StringUnmarshaller.GetInstance().Unmarshall(context);
                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GranteeUnmarshaller grantee display name: {0}", grantee.DisplayName));
                        continue;
                    }
                    if (context.TestExpression("EmailAddress", targetDepth))
                    {
                        grantee.EmailAddress = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GranteeUnmarshaller grantee email address: {0}", grantee.EmailAddress));
                        continue;
                    }
                    if (context.TestExpression("ID", targetDepth))
                    {
                        grantee.CanonicalUser = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GranteeUnmarshaller grantee canonical user: {0}", grantee.CanonicalUser));
                        continue;
                    }
                    if (context.TestExpression("URI", targetDepth))
                    {
                        grantee.URI = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GranteeUnmarshaller grantee URI: {0}", grantee.URI));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return grantee;
                }
            }
                        


            return grantee;
        }

        public S3Grantee Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static GranteeUnmarshaller _instance;

        public static GranteeUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GranteeUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
