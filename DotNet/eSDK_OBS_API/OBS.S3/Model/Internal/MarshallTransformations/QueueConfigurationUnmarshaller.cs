﻿/*
 * Copyright 2010-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
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
    public class QueueConfigurationUnmarshaller : IUnmarshaller<QueueConfiguration, XmlUnmarshallerContext>, IUnmarshaller<QueueConfiguration, JsonUnmarshallerContext> 
    {
        public QueueConfiguration Unmarshall(XmlUnmarshallerContext context)
        {
            QueueConfiguration queueConfiguration = new QueueConfiguration();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Id", targetDepth))
                    {
                        queueConfiguration.Id = StringUnmarshaller.GetInstance().Unmarshall(context);

                        continue;
                    }
                    if (context.TestExpression("Event", targetDepth))
                    {
                        queueConfiguration.Events.Add(StringUnmarshaller.GetInstance().Unmarshall(context));

                        continue;
                    }
                    if (context.TestExpression("Queue", targetDepth))
                    {
                        queueConfiguration.Queue = StringUnmarshaller.GetInstance().Unmarshall(context);

                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return queueConfiguration;
                }
            }

            return queueConfiguration;
        }

        public QueueConfiguration Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }

        private static QueueConfigurationUnmarshaller _instance;

        public static QueueConfigurationUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QueueConfigurationUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
