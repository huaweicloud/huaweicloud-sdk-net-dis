using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Com.Bigdata.Dis.Sdk.DISCommon.DISWeb
{
    public class AppResource : RestResource
    {
        private static readonly string DEFAULT_RESOURCE_NAME = "apps";

        private readonly string _resourceName;

        private readonly string _resourceId;

        public AppResource(string resourceId)
        {
            _resourceId = resourceId;
        }

        public AppResource(string resourceName, string resourceId)
        {
            _resourceName = resourceName;
            _resourceId = resourceId;
        }

        public override string GetResourceName()
        {
            return string.IsNullOrEmpty(_resourceName) ? DEFAULT_RESOURCE_NAME : _resourceName;
        }

        public override string GetResourceId()
        {
            return _resourceId;
        }

        public override string GetAction()
        {
            return null;
        }
    }
}
