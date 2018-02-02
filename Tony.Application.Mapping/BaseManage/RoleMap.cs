using Tony.Application.Entity.BaseManage;
using System.Data.Entity.ModelConfiguration;

namespace Tony.Application.Mapping.BaseManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：角色信息表
    /// </summary>
    public class RoleMap : EntityTypeConfiguration<RoleEntity>
    {
        public RoleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Role");
            //主键
            this.HasKey(t => t.RoleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
