using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public class SignerConfigJsonHelper
    {
        private string signerType;

        SignerConfigJsonHelper(string signerType)
        {
            this.signerType = signerType;
        }

        public SignerConfigJsonHelper()
        {
        }

        public string GetSignerType()
        {
            return this.signerType;
        }

        void SetSignerType(string signerType)
        {
            this.signerType = signerType;
        }

        public SignerConfig Build()
        {
            return new SignerConfig(this.signerType);
        }
    }
}
