using Tony.Application.Entity.SystemManage;
using Tony.Application.IService.SystemManage;
using Tony.Application.Service.SystemManage;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System;
using Tony.Application.Entity.SystemManage.ViewModel;

namespace Tony.Application.Business.SystemManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-24 08:57
    /// 描 述：数据字典明细表
    /// </summary>
    public class DataItemDetailBll
    {
        private IDataItemDetailService service = new DataItemDetailService();
        public string CacheKey = "dataItemCache";

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DataItemDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<DataItemDetailEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            return service.GetDataItemList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, DataItemDetailEntity entity)
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
