using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tony.Application.Business.SystemManage;
using Tony.Application.CacheHelper;
using Tony.Application.Code;
using Tony.Util;

namespace Tony.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：数据字典明细管理
    /// </summary>
    public class DataItemDetailController : MvcControllerBase
    {
        private DataItemDetailBll dataItemDetailBll = new DataItemDetailBll();
        private DataItemCache dataItemCache = new DataItemCache();

        #region 视图功能
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="enCode">代码</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetDataItemListJson(string enCode)
        {
            var data = dataItemCache.GetDataItemList(enCode);
            return Content(data.Distinct().ToJson());
        }
        #endregion

        #region 验证数据

        #endregion

        #region 提交数据

        #endregion
    }
}