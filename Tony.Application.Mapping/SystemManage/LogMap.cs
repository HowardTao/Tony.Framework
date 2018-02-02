using System.Data.Entity.ModelConfiguration;
using Tony.Application.Entity.SystemManage;

namespace Tony.Application.Mapping.SystemManage
{
    /// <summary>
    /// 系统日志
    /// </summary>
   public class LogMap:EntityTypeConfiguration<LogEntity>
    {
        public LogMap()
        {
            #region 表/主键
            ToTable("Base_Log");
            HasKey(t => t.LogId);
            #endregion

            #region 配置关系

            #endregion
        }
    }
}
