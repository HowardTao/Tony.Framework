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
    /// 描 述：角色信息缓存
    /// </summary>
    public class RoleCache
    {
        private RoleBll roleBll = new RoleBll();

        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            IEnumerable<RoleEntity> data = new List<RoleEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<RoleEntity>>(roleBll.CacheKey);
            if (cacheList == null)
            {
                data = roleBll.GetList();
                CacheFactory.Cache().WriteCache(data, roleBll.CacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList(string organizeId)
        {
            var data = GetList();
            if (!string.IsNullOrEmpty(organizeId)) data = data.Where(t => t.OrganizeId == organizeId);
            return data;
        }

        /// <summary>
        /// 角色实体
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public RoleEntity GetEntity(string roleId)
        {
            var data = GetList();
            if (!string.IsNullOrEmpty(roleId))
            {
                var d = data.Where(t => t.RoleId == roleId).ToList();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new RoleEntity();
        }
    }
}
