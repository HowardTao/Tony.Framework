using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Application.Service.BaseManage;
using Tony.Util.WebControl;
using System.Collections.Generic;
using System;
using Tony.CacheHelper;

namespace Tony.Application.Business.BaseManage
{
    /// <summary>
    /// 版 本 
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：角色信息表
    /// </summary>
    public class RoleBll
    {
        private IRoleService service = new RoleService();
        public string CacheKey = "RoleCache";

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public IEnumerable<RoleEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoleEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 角色编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            return service.ExistEnCode(enCode,keyValue);
        }
        /// <summary>
        /// 角色名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
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
                CacheFactory.Cache().RemoveCache(CacheKey);
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
        public void SaveForm(string keyValue, RoleEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
                CacheFactory.Cache().RemoveCache(CacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}
