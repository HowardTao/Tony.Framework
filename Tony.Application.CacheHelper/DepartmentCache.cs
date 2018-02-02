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
    /// 描 述：部门信息缓存
    /// </summary>
    public class DepartmentCache
    {
        private DepartmentBll departmentBll = new DepartmentBll();
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            IEnumerable<DepartmentEntity> data = new List<DepartmentEntity>();
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<DepartmentEntity>>(departmentBll.CacheKey);
            if (cacheList == null)
            {
                data = departmentBll.GetList();
                CacheFactory.Cache().WriteCache(data,departmentBll.CacheKey);
            }
            else
            {
                data = cacheList;
            }
            return data;
        }
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList(string organizeId)
        {
            var data = GetList();
            if (!string.IsNullOrEmpty(organizeId))
            {
                data = data.Where(t => t.OrganizeId == organizeId);
            }
            return data;
        }
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="departmentId">部门Id</param>
        /// <returns></returns>
        public DepartmentEntity GetEntity(string departmentId)
        {
            if (!string.IsNullOrEmpty(departmentId))
            {
                var data = GetList();
                return data.FirstOrDefault(t => t.DepartmentId == departmentId);
            }
            return new DepartmentEntity();
        }
    }
}
