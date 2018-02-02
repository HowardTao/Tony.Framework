using Tony.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Tony.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 16:37
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeMap : EntityTypeConfiguration<OrganizeEntity>
    {
        public OrganizeMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Organize");
            //主键
            this.HasKey(t => t.OrganizeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
