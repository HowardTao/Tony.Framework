using System;
using System.Web;

namespace Tony.CacheHelper
{
    /// <summary>
    /// 缓存操作
    /// </summary>
    public class Cache:ICache
    {
        private static System.Web.Caching.Cache _cache = HttpRuntime.Cache;

        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            if (_cache[cacheKey] != null) return (T) _cache[cacheKey];
            return default(T);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            _cache.Insert(cacheKey,value,null,DateTime.Now.AddMinutes(10),System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            _cache.Insert(cacheKey, value, null, expireTime, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
            var cacheList = _cache.GetEnumerator();
            while (cacheList.MoveNext())
            {
                if (cacheList.Key != null) _cache.Remove(cacheList.Key.ToString());
            }
        }
    }
}
