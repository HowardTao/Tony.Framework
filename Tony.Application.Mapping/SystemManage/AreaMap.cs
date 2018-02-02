using Tony.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace Tony.Application.Mapping.SystemManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-26 15:58
    /// 描 述：行政区域管理
    /// </summary>
    public class AreaMap : EntityTypeConfiguration<AreaEntity>
    {
        public AreaMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Area");
            //主键
            this.HasKey(t => t.AreaId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
