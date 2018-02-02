using System.Linq;
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
    /// 日 期：2017-10-24 21:01
    /// 描 述：组织管理
    /// </summary>
    public class OrganizeController : MvcControllerBase
    {
        private OrganizeBll organizeBll = new OrganizeBll();
        private OrganizeCache organizeCache = new OrganizeCache();

        #region 视图功能
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string condition,string keyword)
        {
            var data = organizeCache.GetList().ToList();
            if (!string.IsNullOrEmpty(condition) && !string.IsNullOrEmpty(keyword))
            {
                switch (condition)
                {
                    case "FullName"://公司名称
                        data = data.TreeWhere(t => t.FullName.Contains(keyword), "OrganizeId");
                        break;
                    case "EnCode"://外文名称
                        data = data.TreeWhere(t => t.EnCode.Contains(keyword), "OrganizeId");
                        break;
                    case "ShortName"://中文名称
                        data = data.TreeWhere(t => t.ShortName.Contains(keyword), "OrganizeId");
                        break;
                    case "Manager"://负责人
                        data = data.TreeWhere(t => t.Manager.Contains(keyword), "OrganizeId");
                        break;

                }
            }
            var treeList = data.Select(t => new TreeGridEntity
            {
                id=t.OrganizeId,
                hasChildren = data.Count(d=>d.ParentId==t.OrganizeId)!=0,
                parentId = t.ParentId,
                expanded = true,
                entityJson = t.ToJson()
            }).ToList();
            return Content(treeList.TreeJson());
        }

        /// <summary>
        /// 机构列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeJson(string keyword)
        {
            var data = organizeCache.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "OrganizeId");
            }
            var list = data.Select(t => new TreeEntity()
            {
                id = t.OrganizeId,
                parentId = t.ParentId,
                text = t.FullName,
                value = t.OrganizeId,
                isexpand = true,
                complete = true,
                hasChildren = data.Count(d => d.ParentId == t.OrganizeId) != 0,
            }).ToList();
            return Content(list.TreeToJson());
        }

        /// <summary>
        /// 机构实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = organizeBll.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="EnCode">外文名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string EnCode, string keyValue)
        {
            var isOk = organizeBll.ExistEnCode( EnCode,keyValue);
            return Content(isOk.ToString());
        }
        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="ShortName">中文名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistShortName(string ShortName, string keyValue)
        {
            var isOk = organizeBll.ExistShortName(ShortName, keyValue);
            return Content(isOk.ToString());
        }
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="FullName">公司名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            var isOk = organizeBll.ExistFullName(FullName, keyValue);
            return Content(isOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            organizeBll.RemoveForm(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存机构表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="organizeEntity">机构实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue,OrganizeEntity organizeEntity)
        {
            organizeBll.SaveForm(keyValue,organizeEntity);
            return Success("操作成功！");
        }
        #endregion
    }
}