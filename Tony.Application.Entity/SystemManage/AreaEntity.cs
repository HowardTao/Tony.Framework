using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tony.Application.Code;

namespace Tony.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-26 15:58
    /// 描 述：行政区域管理
    /// </summary>
    public class AreaEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 区域主键
        /// </summary>
        /// <returns></returns>
        [Column("AREAID")]
        public string AreaId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Column("PARENTID")]
        public string ParentId { get; set; }
        /// <summary>
        /// 区域编码
        /// </summary>
        /// <returns></returns>
        [Column("AREACODE")]
        public string AreaCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        /// <returns></returns>
        [Column("AREANAME")]
        public string AreaName { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>
        /// <returns></returns>
        [Column("QUICKQUERY")]
        public string QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        /// <returns></returns>
        [Column("SIMPLESPELLING")]
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
        /// <returns></returns>
        [Column("LAYER")]
        public int? Layer { get; set; }
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
            this.AreaId = Guid.NewGuid().ToString();
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
            this.AreaId = keyValue;
            this.ModifyDate = DateTime.Now;

            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;

            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;

        }
        #endregion
    }
}