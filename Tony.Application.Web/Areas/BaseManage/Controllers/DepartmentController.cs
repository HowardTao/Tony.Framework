using System.Collections.Generic;
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
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentController : MvcControllerBase
    {
        private DepartmentBll departmentBll = new DepartmentBll();
        private DepartmentCache departmentCache = new DepartmentCache();
        private OrganizeCache organizeCache = new OrganizeCache();

        #region 视图功能
        /// <summary>
        /// 部门管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 部门表单
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
        /// 部门列表
        /// </summary>
        /// <param name="organizeId">公司Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeJson(string organizeId, string keyword)
        {
            var data = departmentCache.GetList(organizeId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.FullName.Contains(keyword), "DepartmentId");
            }
            var list = data.Select(t => new TreeEntity
            {
                id = t.DepartmentId,
                parentId = t.ParentId,
                text = t.FullName,
                value = t.DepartmentId,
                isexpand = true,
                complete = true,
                hasChildren = data.Count(d => d.ParentId == t.DepartmentId) > 0
            }).ToList();
            return Content(list.TreeToJson());
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回机构+部门树形Json</returns>
        public ActionResult GetOrganizeTreeJson(string keyword)
        {
            var organizedata = organizeCache.GetList().ToList();
            var departmentdata = departmentBll.GetList().ToList();
            var treeList = new List<TreeEntity>();
            foreach (var organizeEntity in organizedata)
            {
                var hasChildren = organizedata.Count(t => t.ParentId == organizeEntity.OrganizeId) > 0;
                if (hasChildren==false)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == organizeEntity.OrganizeId) > 0;
                }
                var tree = new TreeEntity
                {
                    id = organizeEntity.OrganizeId,
                    text = organizeEntity.FullName,
                    value = organizeEntity.OrganizeId,
                    parentId = organizeEntity.ParentId,
                    isexpand = true,
                    complete = true,
                    hasChildren = hasChildren,
                    Attribute = "Sort",
                    AttributeValue = "Organize"
                };
                treeList.Add(tree);
            }
            foreach (var departmentEntity in departmentdata)
            {
                var tree = new TreeEntity
                {
                    id = departmentEntity.DepartmentId,
                    text = departmentEntity.FullName,
                    value = departmentEntity.DepartmentId,
                    parentId = departmentEntity.ParentId=="0"?departmentEntity.OrganizeId:departmentEntity.ParentId,
                    isexpand = true,
                    complete = true,
                    hasChildren = departmentdata.Count(t=>t.ParentId==departmentEntity.DepartmentId)>0,
                    Attribute = "Sort",
                    AttributeValue = "Department"
                };
                treeList.Add(tree);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            }
            return Content(treeList.TreeToJson());
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string condition, string keyword)
        {
            var organizedata = organizeCache.GetList().ToList();
            var departmentdata = departmentBll.GetList().ToList();
            if (!string.IsNullOrEmpty(condition) && !string.IsNullOrEmpty(keyword))
            {
                switch (condition)
                {
                    case "FullName":    //部门名称
                        departmentdata = departmentdata.TreeWhere(t => t.FullName.Contains(keyword), "DepartmentId");
                        break;
                    case "EnCode":      //部门编号
                        departmentdata = departmentdata.TreeWhere(t => t.EnCode.Contains(keyword), "DepartmentId");
                        break;
                    case "ShortName":   //部门简称
                        departmentdata = departmentdata.TreeWhere(t => t.ShortName.Contains(keyword), "DepartmentId");
                        break;
                    case "Manager":     //负责人
                        departmentdata = departmentdata.TreeWhere(t => t.Manager.Contains(keyword), "DepartmentId");
                        break;
                    case "OuterPhone":  //电话号
                        departmentdata = departmentdata.TreeWhere(t => t.OuterPhone.Contains(keyword), "DepartmentId");
                        break;
                    case "InnerPhone":  //分机号
                        departmentdata = departmentdata.TreeWhere(t => t.Manager.Contains(keyword), "DepartmentId");
                        break;

                }
            }
            var treeList = new List<TreeGridEntity>();
            foreach (var item in organizedata)
            {
                var tree = new TreeGridEntity();
                var hasChildren = organizedata.Count(t => t.ParentId == item.OrganizeId) != 0;
                if (!hasChildren)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == item.OrganizeId) != 0;
                    if (!hasChildren) continue;
                }
                tree.id = item.OrganizeId;
                tree.hasChildren = true;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                item.EnCode = ""; item.ShortName = ""; item.Nature = ""; item.Manager = ""; item.OuterPhone = ""; item.InnerPhone = ""; item.Description = "";
                var itemJson = item.ToJson();
                itemJson = itemJson.Insert(1, "\"DepartmentId\":\"" + item.OrganizeId + "\",");
                itemJson = itemJson.Insert(1, "\"Sort\":\"Organize\",");
                tree.entityJson = itemJson;
                treeList.Add(tree);
            }
            foreach (var item in departmentdata)
            {
                var tree = new TreeGridEntity();
                var hasChildren = organizedata.Count(t => t.ParentId == item.DepartmentId) != 0;
                tree.id = item.DepartmentId;
                tree.parentId = item.ParentId == "0" ? item.OrganizeId : item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                var itemJson = item.ToJson();
                itemJson = itemJson.Insert(1, "\"Sort\":\"Department\",");
                tree.entityJson = itemJson;
                treeList.Add(tree);
            }
            return Content(treeList.TreeJson());
        }

        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = departmentBll.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="EnCode">部门编号</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string EnCode, string keyValue)
        {
            var isOk = departmentBll.ExistEnCode(EnCode, keyValue);
            return Content(isOk.ToString());
        }
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="FullName">部门名称</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            var isOk = departmentBll.ExistFullName(FullName, keyValue);
            return Content(isOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            departmentBll.RemoveForm(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">机构实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult SaveForm(string keyValue, DepartmentEntity departmentEntity)
        {
            departmentBll.SaveForm(keyValue, departmentEntity);
            return Success("操作成功！");
        }
        #endregion
    }
}