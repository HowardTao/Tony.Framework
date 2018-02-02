using System.Data.Entity.ModelConfiguration;
using Tony.Application.Entity.AuthorizeManage;

namespace Tony.Application.Mapping.AuthorizeManage
{
   public class AuthorizeDataMap:EntityTypeConfiguration<AuthorizeDataEntity>
    {
        /// <summary>
        /// 授权数据范围
        /// </summary>
        public AuthorizeDataMap()
        {
            ToTable("Base_AuthorizeData");
            HasKey(t => t.AuthorizeDataId);
        }
    }
}
