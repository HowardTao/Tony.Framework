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
    /// 描 述：组织架构缓存
    /// </summary>
    public class OrganizeCache
    {
        private OrganizeBll organizeBll = new OrganizeBll();

        /// <summary>
        /// 组织列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetList()
        {
            IEnumerable<OrganizeEntity> data = new List<OrganizeEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<OrganizeEntity>>(organizeBll.CacheKey);
            if (cacheList == null)
            {
                data = organizeBll.GetList();
                CacheFactory.Cache().WriteCache(data, organizeBll.CacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }

        /// <summary>
        /// 组织实体
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public OrganizeEntity GetEntity(string organizeId)
        {
            if(string.IsNullOrEmpty(organizeId)) return new OrganizeEntity();
            var data = GetList();
            var d = data.FirstOrDefault(t => t.OrganizeId == organizeId);
            return d;
        }
    }
}
