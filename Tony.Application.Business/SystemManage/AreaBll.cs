using Tony.Application.Entity.SystemManage;
using Tony.Application.IService.SystemManage;
using Tony.Application.Service.SystemManage;
using System.Collections.Generic;
using System;
using System.Linq;
using Tony.CacheHelper;

namespace Tony.Application.Business.SystemManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-26 15:58
    /// 描 述：行政区域管理
    /// </summary>
    public class AreaBll
    {
        private IAreaService service = new AreaService();
        private string cahceKey = "areaCache";

        #region 获取数据
        /// <summary>
        ///区域列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<AreaEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<AreaEntity>>(cahceKey);
            if (cacheList == null)
            {
                var data = service.GetList();
                CacheFactory.Cache().WriteCache(data,cahceKey);
                return data;
            }
            return cacheList;
        }
        /// <summary>
        /// 区域列表（主要是给绑定数据源提供的）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetList(string parentId)
        {
            return GetList().Where(t => t.EnabledMark == 1 && t.ParentId == parentId);
        }
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetList(string parentId,string keyword)
        {
            return service.GetList(parentId, keyword);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AreaEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                CacheFactory.Cache().RemoveCache(cahceKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AreaEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
                CacheFactory.Cache().RemoveCache(cahceKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
