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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace OBS.Runtime.Internal.Transform
{
    public class HttpWebRequestResponseData : IWebResponseData
    {
        HttpWebResponse _response;
        string[] _headerNames;
        HashSet<string> _headerNamesSet;
        HttpWebResponseBody _responseBody;

        public HttpWebRequestResponseData(HttpWebResponse response)
        {
            this._response = response;
            _responseBody = new HttpWebResponseBody(response);

            this.StatusCode = response.StatusCode;
            this.IsSuccessStatusCode = this.StatusCode >= HttpStatusCode.OK && this.StatusCode <= (HttpStatusCode)299;
            this.ContentType = response.ContentType;            
            this.ContentLength = response.ContentLength;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public bool IsSuccessStatusCode { get; private set; }

        public string ContentType { get; private set; }        

        public long ContentLength { get; private set; }
        
        public bool IsHeaderPresent(string headerName)
        {
            if (_headerNamesSet == null)
                SetHeaderNames();
            return _headerNamesSet.Contains(headerName);
        }

        public string[] GetHeaderNames()
        {
            if (_headerNames == null)
            {
                SetHeaderNames();
            }
            return _headerNames;            
        }

        public string GetHeaderValue(string name)
        {
            return this._response.GetResponseHeader(name);
        }

        private void SetHeaderNames()
        {
            var keys = this._response.Headers.Keys;
            _headerNames = new string[keys.Count];
            for (int i = 0; i < keys.Count; i++)
                _headerNames[i] = keys[i];
            _headerNamesSet = new HashSet<string>(_headerNames, StringComparer.OrdinalIgnoreCase);
        }

        public IHttpResponseBody ResponseBody
        {
            get { return _responseBody; }
        }
    }

    public class HttpWebResponseBody : IHttpResponseBody
    {
        HttpWebResponse _response;
        bool _disposed = false;

        public HttpWebResponseBody(HttpWebResponse response)
        {
            _response = response;
        }

        public Stream OpenResponse()
        {
            if (_disposed)
                throw new ObjectDisposedException("HttpWebResponseBody");
            
            return _response.GetResponseStream();
        }
#if BCL45 || WIN_RT || WINDOWS_PHONE 
        public System.Threading.Tasks.Task<Stream> OpenResponseAsync()
        {            
            throw new NotSupportedException();
        }
#endif
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_response != null)
                    _response.Close();

                _disposed = true;
            }
        }
    }
}
