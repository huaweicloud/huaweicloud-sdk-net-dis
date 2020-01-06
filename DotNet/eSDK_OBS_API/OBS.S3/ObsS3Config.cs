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
using System;

using OBS.Runtime;
using System.Net;
using System.Text.RegularExpressions;

namespace OBS.S3
{

    /// <summary>
    /// Configuration for accessing ObsS3 service
    /// </summary>
    public class ObsS3Config : ClientConfig
    {
        private bool forcePathStyle = true;
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public ObsS3Config()
        {
            this.AuthenticationServiceName = "s3";
            this.AllowAutoRedirect = false;

            try
            {
                //2015-12-11 w00322557 ��־��ʼ��
                Int32[] logLevel = { -1, -1, -1 };

                string iniFilePath = System.Environment.CurrentDirectory + "\\OBS.ini";
				// use config log path
               // LoggerMgr.logInit(OBS.S3.ObsClient.strProduct, iniFilePath, logLevel, "");
            }
            catch (System.Exception ex)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, ex.ToString());
            }
                        
                                    
#if BCL45
            // Set Timeout and ReadWriteTimeout for S3 to max timeout as per-request
            // timeouts are not supported.
            this.Timeout = ClientConfig.MaxTimeout;
            this.ReadWriteTimeout = ClientConfig.MaxTimeout;
#elif (WIN_RT || WINDOWS_PHONE)
            // Only Timeout property is supported for WinRT and Windows Phone.
            // Set Timeout for S3 to max timeout as per-request
            // timeouts are not supported.
            this.Timeout = ClientConfig.MaxTimeout;
#endif
        }

        ~ObsS3Config()
        {
            LoggerMgr.LogFini(OBS.S3.ObsClient.strProduct);
        }
        /// <summary>
        /// The constant used to lookup in the region hash the endpoint.
        /// </summary>
        public override string RegionEndpointServiceName
        {
            get
            {
                return "s3";
            }
        }

        /// <summary>
        /// Gets the ServiceVersion property.
        /// </summary>
        public override string ServiceVersion
        {
            get
            {
                return "2006-03-01";
            }
        }

        /// <summary>
        /// When true, requests will always use path style addressing.
        /// </summary>
        public bool ForcePathStyle
        {
            get { return forcePathStyle; }
            set { forcePathStyle = value; }
        }

        /// <summary>
        /// Set service url
        /// </summary>
        /// <param name="strURL"></param>
        public void SetServiceURL(string strURL)
        {
            this.ServiceURL = strURL;
            //2015-12-7 w00322557: Accroding to the URL format to decide path style: IP:path style Domain:virtual             
            if (IsIP(this.ServiceURL))
            {
                this.ForcePathStyle = true;
            }
            else
            {
                this.ForcePathStyle = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        private bool IsIP(string strURL)
        {            
            if (string.IsNullOrEmpty(strURL))
            {                
                throw new ArgumentNullException("URL is null");
            }

            HttpWebRequest request = WebRequest.Create(strURL) as HttpWebRequest;
            if (request == null)
            {
                throw new SystemException("Http request is null");
            }
            //string strHost = request.Host;

            Uri uri = request.Address;

            Regex regex = new Regex(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
            //return regex.IsMatch(strHost);
            return regex.IsMatch(uri.Host);
        }
    }
}

    
