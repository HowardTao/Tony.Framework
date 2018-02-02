using System.Linq;
using System.Text;
using System.Web.Mvc;
using Tony.Application.Business.SystemManage;
using Tony.Util;

namespace Tony.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：行政区域管理
    /// </summary>
    public class AreaController : MvcControllerBase
    {
        private AreaBll areaBll = new AreaBll();

        #region 视图功能
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeJson(string value)
        {
            var parentId = value ?? "0";
            var filterData = areaBll.GetList(parentId).ToList();
            var sb = new StringBuilder();
            sb.Append("[");
            if (filterData.Count > 0)
            {
                foreach (var entity in filterData)
                {
                    var hasChildren = filterData.Count(t => t.AreaId == entity.AreaId) != 0;
                    sb.Append("{");
                    sb.Append("\"id\":\"" + entity.AreaId + "\",");
                    sb.Append("\"text\":\"" + entity.AreaName + "\",");
                    sb.Append("\"value\":\"" + entity.AreaId + "\",");
                    sb.Append("\"isexpand\":false,");
                    sb.Append("\"complete\":false,");
                    sb.Append("\"hasChildren\":" + hasChildren.ToString().ToLower() + "");
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }

            sb.Append("]");
            return Content(sb.ToString());
        }

        /// <summary>
        /// 区域列表（主要是绑定下拉框）
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ActionResult GetAreaListJson(string parentId)
        {
            var data = areaBll.GetList(parentId ?? "0");
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据

        #endregion


    }
}