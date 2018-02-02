using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tony.Application.Code;

namespace Tony.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：系统按钮
    /// </summary>
    public class ModuleButtonEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 按钮主键
        /// </summary>
        /// <returns></returns>
        [Column("MODULEBUTTONID")]
        public string ModuleButtonId { get; set; }
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
        /// 图标
        /// </summary>
        /// <returns></returns>
        [Column("ICON")]
        public string Icon { get; set; }
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
        /// Action地址
        /// </summary>
        /// <returns></returns>
        [Column("ACTIONADDRESS")]
        public string ActionAddress { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("SORTCODE")]
        public int? SortCode { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ModuleButtonId = Guid.NewGuid().ToString();
            
            
            
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ModuleButtonId = keyValue;
            
            
            
        }
        #endregion
    }
}