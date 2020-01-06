﻿/*******************************************************************************
 *  Copyright 2008-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 *  API Version: 2006-03-01
 *
 */
using System;
using System.Collections.Generic;
using System.Text;

using OBS.S3.Model;
using OBS.Util;

namespace OBS.S3.Transfer
{
    /// <summary>
    /// Contains all the parameters
    /// that can be set when making a this request with the 
    /// <c>TransferUtility</c> method.
    /// </summary>
    public partial class TransferUtilityDownloadRequest : BaseDownloadRequest
    {
#if BCL
        /// <summary>
        /// 	Get or sets the file path location of where the
        /// 	downloaded Obs S3 object will be written to.
        /// </summary>
        /// <value>
        /// 	The file path location of where the downloaded Obs S3 object will be written to.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Checks if FilePath property is set.
        /// </summary>
        /// <returns>True if FilePath property is set.</returns>
        internal bool IsSetFilePath()
        {
            return !System.String.IsNullOrEmpty(this.FilePath);
        }
#endif
        /// <summary>
        /// The event for WriteObjectProgressEvent notifications. All
        /// subscribers will be notified when a new progress
        /// event is raised.
        /// <para>
        /// The WriteObjectProgressEvent is fired as data
        /// is downloaded from S3.  The delegates attached to the event 
        /// will be passed information detailing how much data
        /// has been downloaded as well as how much will be downloaded.
        /// </para>
        /// </summary>
        /// <remarks>
        /// Subscribe to this event if you want to receive
        /// WriteObjectProgressEvent notifications. Here is how:<br />
        /// 1. Define a method with a signature similar to this one:
        /// <code>
        /// private void displayProgress(object sender, WriteObjectProgressArgs args)
        /// {
        ///     Console.WriteLine(args);
        /// }
        /// </code>
        /// 2. Add this method to the WriteObjectProgressEvent delegate's invocation list
        /// <code>
        /// TransferUtilityDownloadRequest request = new TransferUtilityDownloadRequest();
        /// request.WriteObjectProgressEvent += displayProgress;
        /// </code>
        /// </remarks>
        public event EventHandler<WriteObjectProgressArgs> WriteObjectProgressEvent;

        /// <summary>
        /// Causes the WriteObjectProgressEvent event to be fired.
        /// </summary>
        /// <param name="progressArgs">Progress data for the stream being written to file.</param>        
        internal void OnRaiseProgressEvent(WriteObjectProgressArgs progressArgs)
        {
            AWSSDKUtils.InvokeInBackground(WriteObjectProgressEvent, progressArgs, this);
        }
    }
}
