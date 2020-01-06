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
     ///   Redirect Unmarshaller
     /// </summary>
    internal class RoutingRuleRedirectUnmarshaller : IUnmarshaller<RoutingRuleRedirect, XmlUnmarshallerContext>, IUnmarshaller<RoutingRuleRedirect, JsonUnmarshallerContext> 
    {
        

        public RoutingRuleRedirect Unmarshall(XmlUnmarshallerContext context) 
        {
            RoutingRuleRedirect redirect = new RoutingRuleRedirect();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("HostName", targetDepth))
                    {
                        redirect.HostName = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RoutingRuleRedirectUnmarshaller HostName: {0}", redirect.HostName));
                        continue;
                    }
                    if (context.TestExpression("HttpRedirectCode", targetDepth))
                    {
                        redirect.HttpRedirectCode = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RoutingRuleRedirectUnmarshaller HttpRedirectCode: {0}", redirect.HttpRedirectCode));
                        continue;
                    }
                    if (context.TestExpression("Protocol", targetDepth))
                    {
                        redirect.Protocol = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RoutingRuleRedirectUnmarshaller Protocol: {0}", redirect.Protocol));
                        continue;
                    }
                    if (context.TestExpression("ReplaceKeyPrefixWith", targetDepth))
                    {
                        redirect.ReplaceKeyPrefixWith = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RoutingRuleRedirectUnmarshaller ReplaceKeyPrefixWith: {0}", redirect.ReplaceKeyPrefixWith));
                        continue;
                    }
                    if (context.TestExpression("ReplaceKeyWith", targetDepth))
                    {
                        redirect.ReplaceKeyWith = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RoutingRuleRedirectUnmarshaller ReplaceKeyWith: {0}", redirect.ReplaceKeyWith));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return redirect;
                }
            }
                        


            return redirect;
        }

        public RoutingRuleRedirect Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static RoutingRuleRedirectUnmarshaller _instance;

        public static RoutingRuleRedirectUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RoutingRuleRedirectUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
