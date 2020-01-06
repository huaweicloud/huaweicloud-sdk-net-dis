using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using IRequest = OBS.Runtime.Internal.IRequest;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public class SignerRequestParams<T>
    {
        private IRequest<T> _request;
        private long _signingDateTimeMilli;
        private string _scope;
        private string _regionName;
        private string _serviceName;
        private string _formattedSigningDateTime;
        private string _formattedSigningDate;
        private string _signingAlgorithm;

        public SignerRequestParams(IRequest<T> request, string regionNameOverride, string serviceNameOverride, string signingAlgorithm) :
            this(request, regionNameOverride, serviceNameOverride, signingAlgorithm, null)
        {
        }

        public SignerRequestParams(IRequest<T> request, string regionNameOverride, string serviceNameOverride, string signingAlgorithm, string signDate)
        {
            if (request == null)
            {
                throw new ArgumentException("Request cannot be null");
            }

            if (signingAlgorithm == null)
            {
                throw new ArgumentException("Signing Algorithm cannot be null");
            }

            this._signingDateTimeMilli = null == signDate ? this.GetSigningDate(request) : this.GetSigningDate(signDate);

            this._request = request;
            this._formattedSigningDate = SignerUtils.FormatDateStamp(this._signingDateTimeMilli);
            this._serviceName = serviceNameOverride ?? "";
            this._regionName = regionNameOverride ?? "";
            this._scope = this.GenerateScope(this._formattedSigningDate, this._serviceName, this._regionName);
            this._formattedSigningDateTime = SignerUtils.FormatTimestamp(this._signingDateTimeMilli);
            this._signingAlgorithm = signingAlgorithm;
        }

        private long GetSigningDate(IRequest<T> request)
        {
            return DateTime.Now.Millisecond - (long)(request.GetTimeOffset() * 1000);
        }

        private long GetSigningDate(string signDate)
        {
            return SignerUtils.ParseMillis(signDate);
        }

        private string GenerateScope(string dateStamp, string serviceName, string regionName)
        {
            var scopeBuilder = new StringBuilder();
            return scopeBuilder.Append(dateStamp).Append("/").Append(regionName).Append("/").Append(serviceName).Append("/").Append("sdk_request").ToString();
        }

        public IRequest<T> GetRequest()
        {
            return this._request;
        }

        public string GetScope()
        {
            return this._scope;
        }

        public string GetFormattedSigningDateTime()
        {
            return this._formattedSigningDateTime;
        }

        public long GetSigningDateTimeMilli()
        {
            return this._signingDateTimeMilli;
        }

        public string GetRegionName()
        {
            return this._regionName;
        }

        public string GetServiceName()
        {
            return this._serviceName;
        }

        public string GetFormattedSigningDate()
        {
            return this._formattedSigningDate;
        }

        public string GetSigningAlgorithm()
        {
            return this._signingAlgorithm;
        }
    }

    public class SignerRequestParamsEx
    {
        private IRequest _request;
        private long _signingDateTimeMilli;
        private string _scope;
        private string _regionName;
        private string _serviceName;
        private string _formattedSigningDateTime;
        private string _formattedSigningDate;
        private string _signingAlgorithm;

        public SignerRequestParamsEx(IRequest request, string regionNameOverride, string serviceNameOverride,
            string signingAlgorithm) :
            this(request, regionNameOverride, serviceNameOverride, signingAlgorithm, null)
        {
        }

        public SignerRequestParamsEx(IRequest request, string regionNameOverride, string serviceNameOverride,
            string signingAlgorithm, string signDate)
        {
            if (request == null)
            {
                throw new ArgumentException("Request cannot be null");
            }

            if (signingAlgorithm == null)
            {
                throw new ArgumentException("Signing Algorithm cannot be null");
            }

            this._signingDateTimeMilli =
                null == signDate ? this.GetSigningDate(request) : this.GetSigningDate(signDate);

            this._request = request;
            this._formattedSigningDate = SignerUtils.FormatDateStamp(this._signingDateTimeMilli);
            this._serviceName = serviceNameOverride ?? "";
            this._regionName = regionNameOverride ?? "";
            this._scope = this.GenerateScope(this._formattedSigningDate, this._serviceName, this._regionName);
            this._formattedSigningDateTime = SignerUtils.FormatTimestamp(this._signingDateTimeMilli);
            this._signingAlgorithm = signingAlgorithm;
        }

        private long GetSigningDate(IRequest request)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0));

            var timeSpan = DateTime.Now.AddHours(-8) - startTime;
            //var jj = ;
            //long milliseconds = kk.Ticks / TimeSpan.TicksPerMillisecond;
            return (long)timeSpan.TotalMilliseconds - (long)(request.TimeOffset * 1000);


        }

        private long GetSigningDate(string signDate)
        {
            return SignerUtils.ParseMillis(signDate);
        }

        private string GenerateScope(string dateStamp, string serviceName, string regionName)
        {
            var scopeBuilder = new StringBuilder();
            return scopeBuilder.Append(dateStamp).Append("/").Append(regionName).Append("/").Append(serviceName)
                .Append("/").Append("sdk_request").ToString();
        }

        public IRequest GetRequest()
        {
            return this._request;
        }

        public string GetScope()
        {
            return this._scope;
        }

        public string GetFormattedSigningDateTime()
        {
            return this._formattedSigningDateTime;
        }

        public long GetSigningDateTimeMilli()
        {
            return this._signingDateTimeMilli;
        }

        public string GetRegionName()
        {
            return this._regionName;
        }

        public string GetServiceName()
        {
            return this._serviceName;
        }

        public string GetFormattedSigningDate()
        {
            return this._formattedSigningDate;
        }

        public string GetSigningAlgorithm()
        {
            return this._signingAlgorithm;
        }
    }
}
