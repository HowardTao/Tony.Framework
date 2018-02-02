using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tony.Application.Code;

namespace Tony.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 16:37
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 机构主键
        /// </summary>
        /// <returns></returns>
        [Column("ORGANIZEID")]
        public string OrganizeId { get; set; }
        /// <summary>
        /// 机构分类
        /// </summary>
        /// <returns></returns>
        [Column("CATEGORY")]
        public int? Category { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Column("PARENTID")]
        public string ParentId { get; set; }
        /// <summary>
        /// 机构代码
        /// </summary>
        /// <returns></returns>
        [Column("ENCODE")]
        public string EnCode { get; set; }
        /// <summary>
        /// 机构简称
        /// </summary>
        /// <returns></returns>
        [Column("SHORTNAME")]
        public string ShortName { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        /// <returns></returns>
        [Column("FULLNAME")]
        public string FullName { get; set; }
        /// <summary>
        /// 机构性质
        /// </summary>
        /// <returns></returns>
        [Column("NATURE")]
        public string Nature { get; set; }
        /// <summary>
        /// 外线电话
        /// </summary>
        /// <returns></returns>
        [Column("OUTERPHONE")]
        public string OuterPhone { get; set; }
        /// <summary>
        /// 内线电话
        /// </summary>
        /// <returns></returns>
        [Column("INNERPHONE")]
        public string InnerPhone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        /// <returns></returns>
        [Column("FAX")]
        public string Fax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        /// <returns></returns>
        [Column("POSTALCODE")]
        public string Postalcode { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        /// <returns></returns>
        [Column("EMAIL")]
        public string Email { get; set; }
        /// <summary>
        /// 负责人主键
        /// </summary>
        /// <returns></returns>
        [Column("MANAGERID")]
        public string ManagerId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        /// <returns></returns>
        [Column("MANAGER")]
        public string Manager { get; set; }
        /// <summary>
        /// 省主键
        /// </summary>
        /// <returns></returns>
        [Column("PROVINCEID")]
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市主键
        /// </summary>
        /// <returns></returns>
        [Column("CITYID")]
        public string CityId { get; set; }
        /// <summary>
        /// 县/区主键
        /// </summary>
        /// <returns></returns>
        [Column("COUNTYID")]
        public string CountyId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        /// <returns></returns>
        [Column("ADDRESS")]
        public string Address { get; set; }
        /// <summary>
        /// 公司主页
        /// </summary>
        /// <returns></returns>
        [Column("WEBADDRESS")]
        public string WebAddress { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>
        /// <returns></returns>
        [Column("FOUNDEDTIME")]
        public DateTime? FoundedTime { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>
        /// <returns></returns>
        [Column("BUSINESSSCOPE")]
        public string BusinessScope { get; set; }
        /// <summary>
        /// 层
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
            this.OrganizeId = Guid.NewGuid().ToString();
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
            this.OrganizeId = keyValue;
            this.ModifyDate = DateTime.Now;

            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;

            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;

        }
        #endregion
    }
}