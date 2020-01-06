using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public class SignerConfig
    {
        private readonly string _signerType;

        public SignerConfig(string signerType)
        {
            this._signerType = signerType;
        }

        public SignerConfig(SignerConfig from)
        {
            this._signerType = from.GetSignerType();
        }

        public string GetSignerType()
        {
            return this._signerType;
        }

        public override string ToString()
        {
            return this._signerType;
        }
    }
}
