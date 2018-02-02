using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tony.Util.Extension;

namespace Tony.Application.Code
{
    /// <summary>
    /// 授权认证
    /// </summary>
   public static class AuthorizeExtensions
    {
        /// <summary>
        /// 获取授权数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToAuthorizeData<T>(this IEnumerable<T> data)
        {
            if (data == null) return null;
            if (OperatorProvider.Provider.Current().IsSystem) return data;
            var dataAutorize = OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
            var parameter = Expression.Parameter(typeof(T), "t");
            var authorCondition = Expression.Constant(dataAutorize)
                .Call("Contains", parameter.Property("CreateUserId"));
            var lambda = authorCondition.ToLambda<Func<T, bool>>(parameter);
            return data.Where(lambda.Compile());
        }
    }
}
