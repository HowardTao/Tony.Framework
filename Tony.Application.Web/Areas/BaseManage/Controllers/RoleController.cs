using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tony.Application.Business.BaseManage;
using Tony.Application.CacheHelper;
using Tony.Application.Code;
using Tony.Application.Entity.BaseManage;
using Tony.Util;
using Tony.Util.WebControl;

namespace Tony.Application.Web.Areas.BaseManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-12-21 21:01
    /// 描 述：角色管理
    /// </summary>
    public class RoleController : MvcControllerBase
    {
        private readonly RoleBll roleBll = new RoleBll();
        private readonly RoleCache roleCache = new RoleCache();

        #region 试图功能
        /// <summary>
        /// 角色管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 角色表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = roleBll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(jsonData.ToJson());
        }
        /// <summary>
        /// 角色实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            var data = roleBll.GetEntity(keyValue);
            return Content(data.ToJson("yyyy-MM-dd HH:mm"));
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 角色编号不能重复
        /// </summary>
        /// <param name="EnCode">编号</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string EnCode, string keyValue)
        {
            var isOk = roleBll.ExistEnCode(EnCode, keyValue);
            return Content(isOk.ToString());
        }
        /// <summary>
        /// 角色名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            var isOk = roleBll.ExistFullName(FullName, keyValue);
            return Content(isOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            roleBll.RemoveForm(keyValue);
            return Success("删除成功");
        }
        /// <summary>
        /// 保存角色表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleEntity">角色实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, RoleEntity roleEntity)
        {
            roleBll.SaveForm(keyValue,roleEntity);
            return Success("操作成功");
        }
        #endregion
    }
}