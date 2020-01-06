namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils.Cache
{
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICacheManager<T>
    {
        /// <summary>
        /// 加入缓存
        /// </summary>
        /// <param name="t">缓存内容</param>
        void PutToCache(T t);

        /// <summary>
        /// 是否还有足够的缓存空间
        /// </summary>
        /// <param name="data">待缓存的数据</param>
        /// <returns>true:空间足够</returns>
        /// <returns>false:空间不足</returns>
        bool HasEnoughSpace(string data);
    }
}