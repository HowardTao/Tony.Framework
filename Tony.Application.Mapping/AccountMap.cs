using System.Data.Entity.ModelConfiguration;
using Tony.Application.Entity;

namespace Tony.Application.Mapping
{
   public class AccountMap: EntityTypeConfiguration<AccountEntity>
    {
        public AccountMap()
        {
            #region 表/主键
            ToTable("Account");
            HasKey(t => t.AccountId);
            #endregion

            #region 配置关系

            #endregion
        }
    }
}
