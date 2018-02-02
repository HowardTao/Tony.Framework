using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tony.Application.Business.AuthorizeManage;
using Tony.Application.Business.BaseManage;
using Tony.Application.CacheHelper;
using Tony.Application.Code;

namespace Tony.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-12-27 09:16
    /// 描 述：角色权限
    /// </summary>
    public class PermissionRoleController : MvcControllerBase
    {
        private readonly OrganizeBll organizeBll = new OrganizeBll();
        private readonly DepartmentBll departmentBll = new DepartmentBll();
        private readonly DepartmentCache departmentCache = new DepartmentCache();
        private readonly RoleBll roleBll = new RoleBll();
        private readonly UserBll userBll = new UserBll();
        private readonly PermissionBll permissionBll = new PermissionBll();
        private readonly AuthorizeBll authorizeBll = new AuthorizeBll();

        #region 视图功能
        /// <summary>
        /// 角色权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllotAuthorize()
        {
            return View();
        }
        /// <summary>
        /// 角色成员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllotMember()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserListJson(string roleId)
        {
            var existMember = permissionBll.GetMemberList(roleId);
            var userData = userBll.GetTable();
            return null;
        }
        #endregion

        #region 提交数据

        #endregion
    }
}