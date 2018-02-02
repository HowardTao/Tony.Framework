using Tony.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace Tony.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-24 21:01
    /// 描 述：功能表格列表
    /// </summary>
    public class ModuleColumnMap : EntityTypeConfiguration<ModuleColumnEntity>
    {
        public ModuleColumnMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_ModuleColumn");
            //主键
            this.HasKey(t => t.ModuleColumnId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
