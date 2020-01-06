using eSDK_OBS_API.OBS.Util; 
﻿/*
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
    ///   LifecycleRuleNoncurrentVersionTransition Unmarshaller
    /// </summary>
    internal class LifecycleRuleNoncurrentVersionTransitionUnmarshaller : IUnmarshaller<LifecycleRuleNoncurrentVersionTransition, XmlUnmarshallerContext>, IUnmarshaller<LifecycleRuleNoncurrentVersionTransition, JsonUnmarshallerContext>
    {
        public LifecycleRuleNoncurrentVersionTransition Unmarshall(XmlUnmarshallerContext context)
        {
            LifecycleRuleNoncurrentVersionTransition transition = new LifecycleRuleNoncurrentVersionTransition();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("NoncurrentDays", targetDepth))
                    {
                        transition.NoncurrentDays = IntUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("LifecycleRuleNoncurrentVersionTransition NoncurrentDays: {0}", transition.NoncurrentDays));
                        continue;
                    }
                    if (context.TestExpression("StorageClass", targetDepth))
                    {
                        transition.StorageClass = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("LifecycleRuleNoncurrentVersionTransition StorageClass: {0}", transition.StorageClass.Value));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return transition;
                }
            }



            return transition;
        }

        public LifecycleRuleNoncurrentVersionTransition Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static LifecycleRuleNoncurrentVersionTransitionUnmarshaller _instance;

        public static LifecycleRuleNoncurrentVersionTransitionUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LifecycleRuleNoncurrentVersionTransitionUnmarshaller();
                }
                return _instance;
            }
        }
    }
}

