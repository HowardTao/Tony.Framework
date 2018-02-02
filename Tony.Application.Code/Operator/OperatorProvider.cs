using System;
using Tony.CacheHelper;
using Tony.Util;
using Tony.Util.Security;

namespace Tony.Application.Code
{
   public class OperatorProvider:IOperatorProvider
    {

        #region 静态实例
        /// <summary>
        /// 当前提供者
        /// </summary>
        public static IOperatorProvider Provider { get { return new OperatorProvider();} }
        /// <summary>
        /// 给App调用
        /// </summary>
        public static string  AppUserId { get; set; }
        #endregion

        /// <summary>
        /// 密钥
        /// </summary>
        private readonly string _loginUserKey = "Tony_LoginUserKey";

        /// <summary>
        ///  登陆提供者模式:Session、Cookie 
        /// </summary>
        private readonly string _loginProvider = Config.GetValue("LoginProvider");

        /// <summary>
        /// 写入登录信息
        /// </summary>
        /// <param name="user">成员信息</param>
        public void AddCurrent(Operator user)
        {
            try
            {
                if (_loginProvider == "Cookie")
                {
                    WebHelper.WriteCookie(_loginUserKey,DESEncrypt.Encrypt(user.ToJson()));
                }
                else
                {
                    WebHelper.WriteSession(_loginUserKey,DESEncrypt.Encrypt(user.ToJson()));
                }
                CacheFactory.Cache().WriteCache(user.Token,user.UserId,user.LogTime.AddHours(12));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 当前用户
        /// </summary>
        /// <returns></returns>
        public Operator Current()
        {
            try
            {
                Operator user;
                if (_loginProvider == "Cookie")
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetCookie(_loginUserKey)).ToObject<Operator>();
                }
                else if (_loginProvider == "AppClient")
                {
                    user = CacheFactory.Cache().GetCache<Operator>(AppUserId);
                }
                else
                {
                    user = DESEncrypt.Decrypt(WebHelper.GetSession(_loginUserKey)).ToObject<Operator>();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 删除登录信息
        /// </summary>
        public void EmptyCurrent()
        {
            if (_loginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(_loginUserKey);
            }
            else
            {
                WebHelper.RemoveSession(_loginUserKey);
            }
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public bool IsOverdue()
        {
            var str = _loginProvider == "Cookie"
                ? WebHelper.GetCookie(_loginUserKey)
                : WebHelper.GetSession(_loginUserKey);
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public int IsOnLine()
        {
            var user = _loginProvider == "Cookie"
                ? DESEncrypt.Decrypt(WebHelper.GetCookie(_loginUserKey)).ToObject<Operator>()
                : DESEncrypt.Decrypt(WebHelper.GetSession(_loginUserKey)).ToObject<Operator>();
            object token = CacheFactory.Cache().GetCache<string>(user.UserId);
            if (token == null) return -1; //过期
            if (user.Token == token.ToString()) return 1; //正常
            return 0; //已登录
        }
    }
}
