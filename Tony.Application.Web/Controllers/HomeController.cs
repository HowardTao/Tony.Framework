using System.Web.Mvc;
using Tony.Application.Code;

namespace Tony.Application.Web.Controllers
{
    /// <summary>
    /// 系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : Controller
    {
        #region 视图功能
        public ActionResult AdminDefault()
        {
            return View();
        }
        public ActionResult AdminLTE()
        {
            return View();
        }
        public ActionResult AdminWindos()
        {
            return View();
        }
        public ActionResult AdminPretty()
        {
            return View();
        }
        public ActionResult AdminDefaultDesktop()
        {
            return View();
        }
        public ActionResult AdminLTEDesktop()
        {
            return View();
        }
        public ActionResult AdminWindosDesktop()
        {
            return View();
        }
        public ActionResult AdminPrettyDesktop()
        {
            return View();
        }
        #endregion

        #region 提交数据

        #endregion
    }
}