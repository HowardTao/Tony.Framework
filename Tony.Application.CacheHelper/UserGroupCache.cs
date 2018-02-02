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
    /// 描 述：用户组信息缓存
    /// </summary>
    public class UserGroupCache
    {
        private UserGroupBll userGroupBll = new UserGroupBll();
        /// <summary>
        /// 岗位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            IEnumerable<RoleEntity> data = new List<RoleEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(userGroupBll.CacheKey);
            if (cacheList == null)
            {
                data = userGroupBll.GetList();
                CacheFactory.Cache().WriteCache(data, userGroupBll.CacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }
        /// <summary>
        /// 岗位列表
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
