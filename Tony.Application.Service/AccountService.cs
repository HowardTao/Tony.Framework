using System;
using System.Linq;
using Tony.Application.Entity;
using Tony.Application.IService;
using Tony.Data.Repository;
using Tony.Util.Extension;

namespace Tony.Application.Service
{
    /// <summary>
    /// 注册账户
    /// </summary>
    public class AccountService:RepositoryFactory<AccountEntity>, IAccountService
    {
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="mobileCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AccountEntity CheckLogin(string mobileCode, string password)
        {            
            var expression = LinqExtensions.True<AccountEntity>();
            expression = expression.And(t => t.MobileCode == mobileCode);
            expression = expression.And(t => t.DeleteMark == 0);
            return BaseRepository("AccountDb").FindEntity(expression);
        }

        public string GetSecurityCode(string mobileCode)
        {
            if (!BaseRepository("AccountDb").IQueryable(t => t.MobileCode == mobileCode).Any())
            {
                
            }
            throw new Exception("手机号已被注册");
        }

        public void LoginLimit(string platform, string account, string ip, string ipArea)
        {
            throw new NotImplementedException();
        }

        public void Register(AccountEntity accountEntity)
        {
            throw new NotImplementedException();
        }
    }
}
