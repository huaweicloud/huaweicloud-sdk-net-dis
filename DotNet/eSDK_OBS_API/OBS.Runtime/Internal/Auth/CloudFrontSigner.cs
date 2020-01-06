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
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

using OBS.Util;
using OBS.Runtime;
using OBS.Runtime.Internal.Util;

using System.Globalization;

namespace OBS.Runtime.Internal.Auth
{
    internal class CloudFrontSigner : AbstractAWSSigner
    {
        public override ClientProtocol Protocol
        {
            get { return ClientProtocol.RestProtocol; }
        }

        public override void Sign(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            if (String.IsNullOrEmpty(awsAccessKeyId))
            {
                throw new ArgumentOutOfRangeException("awsAccessKeyId", "The AWS Access Key ID cannot be NULL or a Zero length string");
            }

            string dateTime = AWSSDKUtils.GetFormattedTimestampRFC822(0);
            request.Headers.Add(HeaderKeys.XAmzDateHeader, dateTime);

            string signature = ComputeHash(dateTime, OBS.S3.AesCryptor.Decrypt(awsSecretAccessKey), SigningAlgorithm.HmacSHA1);

            request.Headers.Add(HeaderKeys.AuthorizationHeader, "AWS " + awsAccessKeyId + ":" + signature);
        }
    }
}
