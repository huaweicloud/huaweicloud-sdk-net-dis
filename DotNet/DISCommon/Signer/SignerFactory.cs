using System;
//using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Signer;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public class Factory
    {
        private static InternalConfig SINGLETON;


        public Factory()
        {
        }

        public static InternalConfig GetInternalConfig()
        {
            if (SINGLETON == null)
            {
                //InternalConfig InternalConfig = InternalConfig.Load();
                //SINGLETON = InternalConfig;
            }

            return SINGLETON;
        }
    }

    public class SignerFactory
    {
        //private static ConcurrentDictionary<string, ISigner> SIGNERS = new ConcurrentDictionary<string, ISigner>();
        private static Dictionary<string, ISigner> SIGNERS = new Dictionary<string, ISigner>();

        private SignerFactory()
        {
        }

        public static AbstractDISSigner GetSigner(string serviceName, string regionName)
        {
            return LookupAndCreateSigner(serviceName, regionName);
        }

        public static void registerSigner(string signerType, ISigner signerClass)
        {
            if (signerType == null)
            {
                throw new ArgumentException("signerType cannot be null");
            }

            if (signerClass == null)
            {
                throw new ArgumentException("signerClass cannot be null");
            }

            SIGNERS[signerType] = signerClass;
        }

        private static AbstractDISSigner LookupAndCreateSigner(string serviceName, string regionName)
        {
            var config = Factory.GetInternalConfig();
            if (config != null)
            {
                var signerConfig = config.GetSignerConfig(serviceName, regionName);
                var signerType = signerConfig.GetSignerType();
                return CreateSigner(signerType, serviceName, regionName);
            }
            
            return CreateSigner("", serviceName, regionName);
        }

        private static AbstractDISSigner CreateSigner(string signerType, string serviceName, string regionName)
        {
            return new DISSignerEx(serviceName, regionName);
        }
    }
}
