using Tony.Application.Entity.AuthorizeManage;
using Tony.Application.IService.AuthorizeManage;
using Tony.Application.Service.AuthorizeManage;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Tony.Application.Business.AuthorizeManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：系统功能
    /// </summary>
    public class ModuleBll
    {
        private IModuleService service = new ModuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ModuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<ModuleEntity> GetList(string parentId = "")
        {
            var data = service.GetList().ToList();
            if (!string.IsNullOrEmpty(parentId)) data = data.FindAll(t => t.ParentId == parentId);
            return data;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, ModuleEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
