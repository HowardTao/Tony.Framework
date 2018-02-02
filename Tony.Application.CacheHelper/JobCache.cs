using System.Collections.Generic;
using System.Linq;
using Tony.Application.Business.BaseManage;
using Tony.Application.Entity.BaseManage;
using Tony.CacheHelper;

namespace Tony.Application.CacheHelper
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：职位信息缓存
    /// </summary>
    public class JobCache
    {
        private JobBll jobBll = new JobBll();

        /// <summary>
        /// 职位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            IEnumerable<RoleEntity> data = new List<RoleEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(jobBll.CacheKey);
            if (cacheList == null)
            {
                data = jobBll.GetList();
                CacheFactory.Cache().WriteCache(data,jobBll.CacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList(string organizeId)
        {
            var data = GetList();
            if (!string.IsNullOrEmpty(organizeId)) data = data.Where(t => t.OrganizeId == organizeId);
            return data;
        }
    }
}
