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

using OBS.Runtime.Internal;
using System;
using System.Xml;


namespace OBS.Runtime.Internal.Transform
{
    /// <summary>
    ///    Response Unmarshaller for all Errors
    /// </summary>
    public class JsonErrorResponseUnmarshaller : IUnmarshaller<ErrorResponse, JsonUnmarshallerContext>
    {
        private static object lockObj = new object();

        private static JsonErrorResponseUnmarshaller instance;

        /// <summary>
        /// Build an ErrorResponse from json 
        /// </summary>
        /// <param name="context">The json parsing context. 
        /// Usually an <c>OBS.Runtime.Internal.JsonUnmarshallerContext</c>.</param>
        /// <returns>An <c>ErrorResponse</c> object.</returns>
        public ErrorResponse Unmarshall(JsonUnmarshallerContext context)
        {
            ErrorResponse response = new ErrorResponse();
            int firstChar = 60;

            if (context.Peek() == firstChar) //starts with '<' so assuming XML.
            {
                ErrorResponseUnmarshaller xmlUnmarshaller = new ErrorResponseUnmarshaller();
                XmlUnmarshallerContext xmlContext = new XmlUnmarshallerContext(context.Stream, false, null);
                response = xmlUnmarshaller.Unmarshall(xmlContext);
            }
            else
            {
                while (context.Read())
                {
                    if (context.TestExpression("__type"))
                    {
                        string type = StringUnmarshaller.GetInstance().Unmarshall(context);
                        response.Code = type.Substring(type.LastIndexOf("#", StringComparison.Ordinal) + 1);
                        if (Enum.IsDefined(typeof(ErrorType), type))
                        {
                            response.Type = (ErrorType)Enum.Parse(typeof(ErrorType), type, true);
                        }
                        else
                        {
                            response.Type = ErrorType.Unknown;
                        }
                        continue;
                    }
                    if (context.TestExpression("code"))
                    {
                        response.Code = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("message"))
                    {
                        response.Message = StringUnmarshaller.GetInstance().Unmarshall(context);
                        continue;
                    }
                }
            }

            return response;
        }

        /// <summary>
        /// Return an instance of JsonErrorResponseUnmarshaller.
        /// </summary>
        /// <returns></returns>
        public static JsonErrorResponseUnmarshaller GetInstance()
        {
            lock (lockObj) {
                if (instance == null)
                {
                    instance = new JsonErrorResponseUnmarshaller();
                }
            }

            return instance;
        }
    }
}
