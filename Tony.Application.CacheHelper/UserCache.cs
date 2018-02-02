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
    /// 描 述：用户信息缓存
    /// </summary>
    public class UserCache
    {
        private UserBll  userBll = new UserBll();

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList()
        {
            IEnumerable<UserEntity > data = new List<UserEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<UserEntity>>(userBll.CacheKey);
            if (cacheList == null)
            {
                data = userBll.GetList();
                CacheFactory.Cache().WriteCache(data,userBll.CacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList(string departmentId)
        {
            IEnumerable<UserEntity> data = new List<UserEntity>();
            data = GetList();
            if (!string.IsNullOrEmpty(departmentId))
            {
                data = data.Where(t=>t.DepartmentId==departmentId);
            }
            return data;
        }
    }
}
