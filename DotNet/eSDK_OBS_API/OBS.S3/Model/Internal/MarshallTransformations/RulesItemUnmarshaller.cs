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
     ///   RulesItem Unmarshaller
     /// </summary>
    internal class RulesItemUnmarshaller : IUnmarshaller<LifecycleRule, XmlUnmarshallerContext>, IUnmarshaller<LifecycleRule, JsonUnmarshallerContext> 
    {
        

        public LifecycleRule Unmarshall(XmlUnmarshallerContext context) 
        {
            LifecycleRule rulesItem = new LifecycleRule();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Expiration", targetDepth))
                    {
                        rulesItem.Expiration = ExpirationUnmarshaller.Instance.Unmarshall(context);

                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RulesItemUnmarshaller Expiration."));                        
                        continue;
                    }
                    if (context.TestExpression("ID", targetDepth))
                    {
                        rulesItem.Id = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RulesItemUnmarshaller ID: {0}", rulesItem.Id));
                        continue;
                    }
                    if (context.TestExpression("Prefix", targetDepth))
                    {
                        rulesItem.Prefix = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RulesItemUnmarshaller Prefix: {0}", rulesItem.Prefix));
                        continue;
                    }
                    if (context.TestExpression("Status", targetDepth))
                    {
                        rulesItem.Status = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RulesItemUnmarshaller Status: {0}", rulesItem.Status));
                        continue;
                    }
                    if (context.TestExpression("Transition", targetDepth))
                    {
                        rulesItem.Transition = TransitionUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RulesItemUnmarshaller Transition."));
                        continue;
                    }
                    if (context.TestExpression("NoncurrentVersionTransition", targetDepth))
                    {
                        rulesItem.NoncurrentVersionTransition = LifecycleRuleNoncurrentVersionTransitionUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("RulesItemUnmarshaller NoncurrentVersionTransition."));
                        continue;
                    }
                    if (context.TestExpression("NoncurrentVersionExpiration", targetDepth))
                    {
                        rulesItem.NoncurrentVersionExpiration = LifecycleRuleNoncurrentVersionExpirationUnmarshaller.Instance.Unmarshall(context);

                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return rulesItem;
                }
            }
                        


            return rulesItem;
        }

        public LifecycleRule Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static RulesItemUnmarshaller _instance;

        public static RulesItemUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RulesItemUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
