using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Tony.Application.Entity.BaseManage;
using Tony.Application.IService.BaseManage;
using Tony.Data.Repository;
using Tony.Util;
using Tony.Util.Extension;
using Tony.Util.WebControl;

namespace Tony.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// 创 建：陶海华
    /// 日 期：2017-10-23 22:00
    /// 描 述：用户管理
    /// </summary>
    public class UserService : RepositoryFactory<UserEntity>, IUserService
    {
        //private 

        #region 获取数据
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.*,
                                    d.FullName AS DepartmentName 
                            FROM    Base_User u
                                    LEFT JOIN Base_Department d ON d.DepartmentId = u.DepartmentId
                            WHERE   1=1");
            strSql.Append(" AND u.UserId <> 'System' AND u.EnabledMark = 1 AND u.DeleteMark=0");
            return BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetList()
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.DeleteMark == 0).And(t => t.EnabledMark == 1);
            return BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<UserEntity>();
            var queryParam = queryJson.ToJObject();
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 用户列表(All)
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public UserEntity GetEntity(string keyValue)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public UserEntity CheckLogin(string username)
        {
            var expression = LinqExtensions.True<UserEntity>();
            expression = expression.And(t => t.Account == username);
            expression = expression.Or(t => t.Mobile == username);
            expression = expression.Or(t => t.Email == username);
            return BaseRepository().FindEntity(expression);    
        }
        /// <summary>
        /// 导出用户列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetExportList()
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 账户不能重复
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public bool ExistAccount(string account, string keyValue)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 保存用户表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="userEntity">用户实体</param>
        /// <returns></returns>
        public string SaveForm(string keyValue, UserEntity userEntity)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="password">新密码（MD5 小写
        public void RevisePassword(string keyValue, string password)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="state">状态：1-启动；0-禁用</param>
        public void UpdateState(string keyValue, int state)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userEntity">实体对象</param>
        public void UpdateEntity(UserEntity userEntity)
        {
            BaseRepository().Update(userEntity);
        }

        #endregion
    }
}
