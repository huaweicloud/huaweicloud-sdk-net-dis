using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.DISWeb
{
    public class ResourcePathBuilder
    {
        private static readonly string Version = "v2";
    
        private string _version;

        private string _projectId;

        private StringBuilder _resourcePath;

        public ResourcePathBuilder WithVersion(string version)
        {
            SetVersion(version);
            return this;
        }

        public ResourcePathBuilder WithProjectId(string projectId)
        {
            SetProjectId(projectId);
            return this;
        }

        /**
         * 绑定资源并按照绑定顺序生成RESTful资源路径，生成形式：/pResource/pResourceId/cResource/cResourceId...
         * 
         * @param restResource
         * @return
         */
        public ResourcePathBuilder WithResource(RestResource restResource)
        {
            SetResourcePath(restResource);
            return this;
        }

        public void SetResourcePath(RestResource restResource)
        {
            var resourceId = restResource.GetResourceId();
            var resourceName = restResource.GetResourceName();
            var action = restResource.GetAction();

            if (null == _resourcePath)
            {
                _resourcePath = new StringBuilder("/");
            }

            _resourcePath = _resourcePath.Append(resourceName).Append("/");
            if (!string.IsNullOrEmpty(resourceId))
            {
                _resourcePath.Append(resourceId).Append("/");
            }

            if (!string.IsNullOrEmpty(action))
            {
                _resourcePath.Append(action).Append("/");
            }
        }

        public void SetVersion(string version)
        {
            this._version = version;
        }

        public void SetProjectId(string projectId)
        {
            this._projectId = projectId;
        }

        /**
         * @return Create new instance of builder with all defaults set.
         */
        public static ResourcePathBuilder Standard()
        {
            return new ResourcePathBuilder();
        }

        public string Build()
        {
            _version = (string.IsNullOrEmpty(_version)) ? Version : _version;
            var prefix = "/" + _version;

            // FIXME 暂时取消校验
            /*if (!StringUtils.isNullOrEmpty(projectId))
            {
                prefix = prefix + "/" + projectId;
            }*/
            prefix = prefix + "/" + _projectId;

            return prefix + _resourcePath.ToString();
        }
    }
}
