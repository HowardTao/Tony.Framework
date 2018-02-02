using Tony.Application.Entity;

namespace Tony.Application.IService
{
    /// <summary>
    /// 注册用户信息表
    /// </summary>
   public interface IAccountService
    {
        AccountEntity CheckLogin(string mobileCode, string password);
        string GetSecurityCode(string mobileCode);
        void Register(AccountEntity accountEntity);
        void LoginLimit(string platform, string account, string ip, string ipArea);
    }
}
