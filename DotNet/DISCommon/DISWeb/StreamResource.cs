namespace Com.Bigdata.Dis.Sdk.DISCommon.DISWeb
{
    public class StreamResource : RestResource
    {
        private static readonly string DEFAULT_RESOURCE_NAME = "streams";
    
        private string _resourceName;

        private string _resourceId;

        private string _action;

        public StreamResource(string resourceId) : this(null, resourceId)
        {
        }

        public StreamResource(string resourceName, string resourceId) : this(resourceName, resourceId, null)
        {
        }

        public StreamResource(string resourceName, string resourceId, string action)
        {
            _resourceName = resourceName;
            _resourceId = resourceId;
            _action = action;
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
            return _action;
        }
    }
}
