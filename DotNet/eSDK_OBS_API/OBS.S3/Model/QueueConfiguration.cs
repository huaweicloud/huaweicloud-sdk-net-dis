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
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;

namespace OBS.S3.Model
{
    /// <summary>
    /// This class contains the configuration Obs S3 uses to figure out what events you want to listen 
    /// and send the event to an Obs SQS queue.
    /// </summary>
    public class QueueConfiguration
    {
        /// <summary>
        /// Gets and set the Id property. The Id will be provided in the event content and can be used 
        /// to identify which configuration caused an event to fire. If the Id is not provided for the configuration, one will be generated.
        /// </summary>
        public string Id { get; set; }

        internal bool IsSetId()
        {
            return this.Id != null;
        }

        List<EventType> _events;
        /// <summary>
        /// Gets and sets the Events property. These are the events the configuration will listen to and send to the Obs SQS queue.
        /// </summary>
        public List<EventType> Events
        {
            get
            {
                if (this._events == null)
                    this._events = new List<EventType>();

                return this._events;
            }
            set { this._events = value; }
        }

        // Check to see if Event property is set
        internal bool IsSetEvents()
        {
            return this._events != null && this._events.Count > 0;
        }

        /// <summary>
        /// Gets and sets the Queue property. Obs SQS queue to which Obs S3 will publish a message 
        /// to report the specified events for the bucket.
        /// </summary>
        public string Queue { get; set; }

        // Check to see if Queue property is set
        internal bool IsSetQueue()
        {
            return this.Queue != null;
        }
    }
}
