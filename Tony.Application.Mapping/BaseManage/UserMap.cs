using System.Data.Entity.ModelConfiguration;
using Tony.Application.Entity.BaseManage;

namespace Tony.Application.Mapping.BaseManage
{
   public class UserMap:EntityTypeConfiguration<UserEntity>
    {
        /// <summary>
        /// 用户管理
        /// </summary>
        public UserMap()
        {
            #region 表、主键
            //表
            ToTable("Base_User");
            //主键
            HasKey(t => t.UserId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
