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
using System.Linq;
using System.Text;

using OBS.Util;

namespace OBS.Runtime.Internal
{
    internal class StreamReadTracker
    {
        object sender;
        EventHandler<StreamTransferProgressArgs> callback;
        long contentLength;
        long totalBytesRead;
        long totalIncrementTransferred;
        long progressUpdateInterval;

        internal StreamReadTracker(object sender, EventHandler<StreamTransferProgressArgs> callback, long contentLength, long progressUpdateInterval)
        {
            this.sender = sender;
            this.callback = callback;
            this.contentLength = contentLength;
            this.progressUpdateInterval = progressUpdateInterval;
        }

        public void ReadProgress(int bytesRead)
        {
            if (callback == null)
                return;

            // Invoke the progress callback only if bytes read > 0
            if (bytesRead > 0)
            {
                totalBytesRead += bytesRead;
                totalIncrementTransferred += bytesRead;

                if (totalIncrementTransferred >= this.progressUpdateInterval ||
                    totalBytesRead == contentLength)
                {

                    AWSSDKUtils.InvokeInBackground(
                                        callback,
                                        new StreamTransferProgressArgs(totalIncrementTransferred, totalBytesRead, contentLength),
                                        sender);
                    totalIncrementTransferred = 0;
                }
            }
        }
    }
}
