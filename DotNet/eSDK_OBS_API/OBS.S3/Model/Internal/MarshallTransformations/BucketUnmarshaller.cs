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
     ///   Bucket Unmarshaller
     /// </summary>
    internal class BucketUnmarshaller : IUnmarshaller<S3Bucket, XmlUnmarshallerContext>, IUnmarshaller<S3Bucket, JsonUnmarshallerContext> 
    {
        public S3Bucket Unmarshall(XmlUnmarshallerContext context) 
        {
            S3Bucket bucket = new S3Bucket();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("CreationDate", targetDepth))
                    {
                        bucket.CreationDate = DateTimeUnmarshaller.GetInstance().Unmarshall(context);
                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("BucketUnmarshaller CreationDate: {0}", bucket.CreationDate.ToShortDateString()));
                        continue;
                    }
                    if (context.TestExpression("Name", targetDepth))
                    {
                        bucket.BucketName = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("BucketUnmarshaller BucketName: {0}", bucket.BucketName));
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return bucket;
                }
            }
                        


            return bucket;
        }

        public S3Bucket Unmarshall(JsonUnmarshallerContext context) 
        {
            return null;
        }

        private static BucketUnmarshaller _instance;

        public static BucketUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BucketUnmarshaller();
                }
                return _instance;
            }
        }

    }
}
    
