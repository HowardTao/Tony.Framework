using System.Data.Entity.ModelConfiguration;
using Tony.Application.Entity.BaseManage;

namespace Tony.Application.Mapping.BaseManage
{
    /// <summary>
    /// 用户关系
    /// </summary>
   public class UserRelationMap:EntityTypeConfiguration<UserRelationEntity>
    {
        public UserRelationMap()
        {
            ToTable("Base_UserRelation");
            HasKey(t => t.UserRelationId);
        }
    }
}
