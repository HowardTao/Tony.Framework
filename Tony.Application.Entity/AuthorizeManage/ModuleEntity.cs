using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tony.Application.Code;

namespace Tony.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：系统功能
    /// </summary>
    public class ModuleEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 功能主键
        /// </summary>
        /// <returns></returns>
        [Column("MODULEID")]
        public string ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Column("PARENTID")]
        public string ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        [Column("ENCODE")]
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [Column("FULLNAME")]
        public string FullName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        [Column("ICON")]
        public string Icon { get; set; }
        /// <summary>
        /// 导航地址
        /// </summary>
        /// <returns></returns>
        [Column("URLADDRESS")]
        public string UrlAddress { get; set; }
        /// <summary>
        /// 导航目标
        /// </summary>
        /// <returns></returns>
        [Column("TARGET")]
        public string Target { get; set; }
        /// <summary>
        /// 菜单选项
        /// </summary>
        /// <returns></returns>
        [Column("ISMENU")]
        public int? IsMenu { get; set; }
        /// <summary>
        /// 允许展开
        /// </summary>
        /// <returns></returns>
        [Column("ALLOWEXPAND")]
        public int? AllowExpand { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        /// <returns></returns>
        [Column("ISPUBLIC")]
        public int? IsPublic { get; set; }
        /// <summary>
        /// 允许编辑
        /// </summary>
        /// <returns></returns>
        [Column("ALLOWEDIT")]
        public int? AllowEdit { get; set; }
        /// <summary>
        /// 允许删除
        /// </summary>
        /// <returns></returns>
        [Column("ALLOWDELETE")]
        public int? AllowDelete { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("SORTCODE")]
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ModuleId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;

            this.CreateUserId = OperatorProvider.Provider.Current().UserId;

            this.CreateUserName = OperatorProvider.Provider.Current().UserName;

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ModuleId = keyValue;
            this.ModifyDate = DateTime.Now;

            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;

            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;

        }
        #endregion
    }
}