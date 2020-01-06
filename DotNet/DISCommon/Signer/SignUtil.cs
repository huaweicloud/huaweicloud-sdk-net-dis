using System;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using OBS.Runtime.Internal;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public class SignUtil
    {
        public static OBS.Runtime.Internal.IRequest Sign(OBS.Runtime.Internal.IRequest request, String ak, String sk, String region)
        {
            // sign request
            AbstractDISSigner signer = SignerFactory.GetSigner(Constants.SERVICENAME, region);
            signer.Sign(request, new BasicCredentials(ak, sk));
            return request;
        }

        public static IRequest<HttpRequest> Sign(IRequest<HttpRequest> request, String ak, String sk, String region)
        {
            return null;
        }

        /*
        public static IRequest<HttpRequest> Sign(IRequest<HttpRequest> request, String ak, String sk, String region)
        {
            // sign request
            ISigner signer = SignerFactory.GetSigner(Constants.SERVICENAME, region);
            signer.Sign(request, new BasicCredentials(ak, sk));
            return request;
        }
        */
    }
}
