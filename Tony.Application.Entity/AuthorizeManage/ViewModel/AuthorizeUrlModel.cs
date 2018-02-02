using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Application.Entity.AuthorizeManage.ViewModel
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-12-19 21:01
    /// 描 述：授权功能Url,操作Url
    /// </summary>
    public class AuthorizeUrlModel
    {
        /// <summary>
        /// 授权主键
        /// </summary>
        public string  AuthorizeId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string  ModuleId { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string  UrlAddress { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string  FullName { get; set; }
    }
}
