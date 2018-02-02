using System;
using System.Collections.Generic;
using System.Data;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Application.Service.BaseManage;
using Tony.Util;
using Tony.Util.Extension;
using Tony.Util.Security;

namespace Tony.Application.Business.BaseManage
{
    /// <summary>
    /// 用户管理
    /// </summary>
   public class UserBll
    {
        private  IUserService service = new UserService();
        public string CacheKey = "UserCache";

        #region 验证数据
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public UserEntity CheckLogin(string username, string password)
        {
            var userEntity = service.CheckLogin(username);
            if (userEntity != null)
            {
                if (userEntity.EnabledMark == 1)
                {
                    var dbPassword = Md5Helper
                        .MD5(DESEncrypt.Encrypt(password.ToLower(), userEntity.Secretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userEntity.Password)
                    {
                        var logOnCount = userEntity.LogOnCount.ToInt() + 1;
                        if (userEntity.LastVisit != null) userEntity.PreviousVisit = userEntity.LastVisit.ToDate();
                        userEntity.LastVisit = DateTime.Now;
                        userEntity.LogOnCount = logOnCount;
                        userEntity.UserOnLine = 1;
                        service.UpdateEntity(userEntity);
                        return userEntity;
                    }
                    throw new Exception("密码和账户名不匹配");
                }
                throw new Exception("账户被系统锁定，请联系管理员");
            }
            throw new Exception("账户不存在，请重新输入");
        }
        #endregion

        #region 获取数据  
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            return service.GetTable();
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList()
        {
            return service.GetList();
        } 
        #endregion

      
    }
}
