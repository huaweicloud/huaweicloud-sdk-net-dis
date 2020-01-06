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

namespace OBS.S3.Encryption
{
    /// <summary>
    /// ObsS3CryptoConfiguration allows customers
    /// to set storage mode for encryption credentials
    /// </summary>
    public class ObsS3CryptoConfiguration: ObsS3Config
    {
        public ObsS3CryptoConfiguration()
        {
            // By default, store encryption info in metadata
            StorageMode = CryptoStorageMode.ObjectMetadata;
        }

        public CryptoStorageMode StorageMode
        { get; set; }
    }
}
