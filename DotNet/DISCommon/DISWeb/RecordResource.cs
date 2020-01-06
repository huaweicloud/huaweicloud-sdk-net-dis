using System;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using OBS.Runtime.Internal.Util;

namespace Com.Bigdata.Dis.Sdk.DISCommon.DISWeb
{
    public class RecordResource : RestResource
    {
        private static string DEFAULT_RESOURCE_NAME = "records";

        private string resourceName;

        private string resourceId;

        public RecordResource(string resourceId)
        {
            this.resourceId = resourceId;
        }

        public RecordResource(string resourceName, string resourceId)
        {
            this.resourceName = resourceName;
            this.resourceId = resourceId;
        }

        public override string GetResourceName()
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return DEFAULT_RESOURCE_NAME;
            }
            return resourceName;
        }

        public override string GetResourceId()
        {
            return resourceId;
        }

        public override string GetAction()
        {
            return null;
        }
    }
}