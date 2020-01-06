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

using OBS.Runtime.Internal;

using OBS.Util;
using OBS.S3.Util;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for all Errors
    /// </summary>
    public class S3ErrorResponseUnmarshaller : IUnmarshaller<S3ErrorResponse, XmlUnmarshallerContext>
    {
        /// <summary>
        /// Build an S3ErrorResponse from XML 
        /// </summary>
        /// <param name="context">The XML parsing context. 
        /// Usually an <c>OBS.Runtime.Internal.UnmarshallerContext</c>.</param>
        /// <returns>An <c>S3ErrorResponse</c> object.</returns>
        public S3ErrorResponse Unmarshall(XmlUnmarshallerContext context)
        {
            S3ErrorResponse response = new S3ErrorResponse();

            response.Code = context.ResponseData.StatusCode.ToString();
            if (context.ResponseData.IsHeaderPresent(HeaderKeys.XAmzRequestIdHeader))
                response.RequestId = context.ResponseData.GetHeaderValue(HeaderKeys.XAmzRequestIdHeader);
            if (context.ResponseData.IsHeaderPresent(HeaderKeys.XAmzId2Header))
                response.Id2 = context.ResponseData.GetHeaderValue(HeaderKeys.XAmzId2Header);

            if ((int)context.ResponseData.StatusCode >= 500)
                response.Type = ErrorType.Receiver;
            else if ((int)context.ResponseData.StatusCode >= 400)
                response.Type = ErrorType.Sender;
            else
                response.Type = ErrorType.Unknown;

            string contentLengthHeader = null;
            if (context.ResponseData.IsHeaderPresent(HeaderKeys.ContentLengthHeader))
                contentLengthHeader = context.ResponseData.GetHeaderValue(HeaderKeys.ContentLengthHeader);

            long contentLength;
            if (string.IsNullOrEmpty(contentLengthHeader) || !long.TryParse(contentLengthHeader, out contentLength))
            {
                contentLength = -1;
            }
            if (contentLength < 0)
            {
                try
                {
                    contentLength = context.Stream.Length;
                }
                catch
                {
                    contentLength = -1;
                }
            }

            if (context.Stream.CanRead && contentLength != 0)
            {
                try
                {
                    while (context.Read())
                    {
                        if (context.IsStartElement)
                        {
                            if (context.TestExpression("Error/Code"))
                            {
                                response.Code = StringUnmarshaller.GetInstance().Unmarshall(context);
                                continue;
                            }
                            if (context.TestExpression("Error/Message"))
                            {
                                response.Message = StringUnmarshaller.GetInstance().Unmarshall(context);
                                continue;
                            }
                            if (context.TestExpression("Error/Resource"))
                            {
                                response.Resource = StringUnmarshaller.GetInstance().Unmarshall(context);
                                continue;
                            }
                            if (context.TestExpression("Error/RequestId"))
                            {
                                response.RequestId = StringUnmarshaller.GetInstance().Unmarshall(context);
                                continue;
                            }
                            if (context.TestExpression("Error/HostId"))
                            {
                                response.Id2 = StringUnmarshaller.GetInstance().Unmarshall(context);
                                continue;
                            }
                        }
                    }
                }
                catch
                {
                    // Error response was not XML
                }
            }

            return response;
        }


        private static S3ErrorResponseUnmarshaller _instance;

        public static S3ErrorResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new S3ErrorResponseUnmarshaller();
                }
                return _instance;
            }
        }
    }

    public class S3ErrorResponse : ErrorResponse
    {
        private string resource;
        private string id2;

        public string Resource
        {
            get { return resource; }
            set { resource = value; }
        }

        public string Id2
        {
            get { return id2; }
            set { id2 = value; }
        }
    }
}
