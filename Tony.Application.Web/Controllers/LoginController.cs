using System;
using System.Web.Mvc;
using Tony.Application.Business.AuthorizeManage;
using Tony.Application.Business.BaseManage;
using Tony.Application.Business.SystemManage;
using Tony.Application.Code;
using Tony.Application.Entity.SystemManage;
using Tony.Util;
using Tony.Util.Extension;
using Tony.Util.Security;

namespace Tony.Application.Web.Controllers
{
    /// <summary>
    /// 系统登录
    /// </summary>
    [HandlerLogin(LoginMode.Ignore)]
    public class LoginController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 默认页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Default()
        {
            return View();
        }
        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <param name="autologin">下次自动登陆</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CheckLogin(string username, string password, string verifycode, int autologin)
        {
            var logEntity = new LogEntity
            {
                CategoryId = 1,
                OperateTypeId = ((int)OperationType.Login).ToString(),
                OperateType = EnumAttribute.GetDescription(OperationType.Login),
                OperateAccount = username,
                OperateUserId = username,
                Module = Config.GetValue("SoftName")
            };
            try
            {
                #region 验证码验证

                if (autologin == 0)
                {
                    verifycode = Md5Helper.MD5(verifycode.ToLower(), 16);
                    if (Session["session_verifycode"].IsEmpty() ||
                        verifycode != Session["session_verifycode"].ToString())
                    {
                        throw new Exception("验证码错误，请重新输入");
                    }
                }
                #endregion

                #region 内部账户验证
                var userEntity = new UserBll().CheckLogin(username,password);
                if (userEntity != null)
                {
                    var operators = new Operator
                    {
                        UserId = userEntity.UserId,
                        Code = userEntity.EnCode,
                        Account = userEntity.Account,
                        UserName = userEntity.RealName,
                        Password = userEntity.Password,
                        Secretkey = userEntity.Secretkey,
                        CompanyId = userEntity.OrganizeId,
                        DepartmentId = userEntity.DepartmentId,
                        IpAddress = Net.Ip,
                        IpAddressName = IpLocation.GetLocation(Net.Ip),
                        ObjectId = new PermissionBll().GetObjectStr(userEntity.UserId),
                        LogTime = DateTime.Now,
                        Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString())
                    };
                    var authorizeBll = new AuthorizeBll();
                    var dataAuthorize = new AuthorizeDataModel
                    {
                        ReadAutorize = authorizeBll.GetDataAuthor(operators),
                        ReadAutorizeUserId = authorizeBll.GetDataAuthorUserId(operators),
                        WriteAutorize = authorizeBll.GetDataAuthor(operators, true),
                        WriteAutorizeUserId = authorizeBll.GetDataAuthorUserId(operators, true)
                    };
                    operators.DataAuthorize = dataAuthorize;
                    operators.IsSystem = userEntity.Account == "System";
                    OperatorProvider.Provider.AddCurrent(operators);
                    //写入日志
                    logEntity.ExecuteResult = -1;
                    logEntity.ExecuteResultJson = "登陆成功";
                    logEntity.WriteLog();
                }

                #endregion

                #region 第三方账户验证

                #endregion

                return Success("登陆成功。");
            }
            catch (Exception ex)
            {
                WebHelper.RemoveCookie("tony_autologin");
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }

        public ActionResult Test()
        {
            var entity = new UserBll().CheckLogin("System","0000");
            return Content("OK");
        }
        #endregion
    }
}