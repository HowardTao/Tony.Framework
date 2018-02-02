using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tony.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-12-27 09:16
    /// 描 述：用户权限
    /// </summary>
    public class PermissionUserController : MvcControllerBase
    {
        // GET: AuthorizeManage/PermissionUser
        public ActionResult Index()
        {
            return View();
        }
    }
}