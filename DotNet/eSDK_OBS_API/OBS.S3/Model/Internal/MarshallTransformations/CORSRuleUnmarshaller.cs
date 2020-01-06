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
     ///   CORSRule Unmarshaller
     /// </summary>
    internal class CORSRuleUnmarshaller : IUnmarshaller<CORSRule, XmlUnmarshallerContext>, IUnmarshaller<CORSRule, JsonUnmarshallerContext> 
    {
        public CORSRule Unmarshall(XmlUnmarshallerContext context) 
        {
            CORSRule cORSRule = new CORSRule();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("AllowedMethod", targetDepth))
                    {
                        string strAllowedMethod = StringUnmarshaller.GetInstance().Unmarshall(context);
                        cORSRule.AllowedMethods.Add(strAllowedMethod);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CORSRuleUnmarshaller AllowedMethod: {0}",strAllowedMethod));
                        continue;
                    }
                    if (context.TestExpression("AllowedOrigin", targetDepth))
                    {
                        string strAllowedOrigin = StringUnmarshaller.GetInstance().Unmarshall(context);
                        cORSRule.AllowedOrigins.Add(strAllowedOrigin);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CORSRuleUnmarshaller AllowedOrigin: {0}", strAllowedOrigin));
                        continue;
                    }
                    if (context.TestExpression("ExposeHeader", targetDepth))
                    {
                        string strExposeHeader = StringUnmarshaller.GetInstance().Unmarshall(context);
                        cORSRule.ExposeHeaders.Add(strExposeHeader);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CORSRuleUnmarshaller ExposeHeader: {0}", strExposeHeader));
                        continue;
                    }
                    if (context.TestExpression("AllowedHeader", targetDepth))
                    {
                        string strAllowedHeader = StringUnmarshaller.GetInstance().Unmarshall(context);
                        cORSRule.AllowedHeaders.Add(strAllowedHeader);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CORSRuleUnmarshaller AllowedHeader: {0}", strAllowedHeader));
                        continue;
                    }

                    if (context.TestExpression("MaxAgeSeconds", targetDepth))
                    {
                        cORSRule.MaxAgeSeconds = IntUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CORSRuleUnmarshaller MaxAgeSeconds: {0}", cORSRule.MaxAgeSeconds));
                        continue;
                    }
                    if (context.TestExpression("ID", targetDepth))
                    {
                        cORSRule.Id = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CORSRuleUnmarshaller ID: {0}", cORSRule.Id));
                        continue;
                    }

                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return cORSRule;
                }
            }
                        


            return cORSRule;
        }

        public CORSRule Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static CORSRuleUnmarshaller _instance;

        public static CORSRuleUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CORSRuleUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
