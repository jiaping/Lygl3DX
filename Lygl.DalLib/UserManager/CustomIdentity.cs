using Csla;
using Csla.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Csla.Security;
using System.Data.SqlClient;


namespace Lygl.DalLib.UserManager
{
    /// <summary>
    /// 注意：防止重名，User增加Code区分唯一性，查询时通过Code查询，同时返回Code和Name
    /// </summary>
    [Serializable()]
    public class CustomIdentity : CslaIdentity
    {
        [NotUndoable]
        //[NonSerialized]
        private RoleList _roles;

        #region  Factory Methods

        internal static CustomIdentity GetIdentity(string username, string password)
        {
            //"51d2ae8b-d2ac-42a7-908a-00d4105165f0"
            //return DataPortal.Fetch<CustomIdentity>(new UsernameCriteria(username, password));
            User user=UserList.GetUserListByUserCodePwd(username, password).FirstOrDefault();
            CustomIdentity ci = new CustomIdentity();
            ci.User = user;
            if (null != user)
            {
                ci.Name = user.Name;
                ci.IsAuthenticated = true;
                ci.CompositePermissionList(user);
            }
            else
            {
                ci.Name = string.Empty;
                ci.IsAuthenticated = false;
                ci._currentUserPermissionList = null;
            }
            return ci;
        }

        public User User { get; set; }

        private CustomIdentity()
        { /* require use of factory methods */ }


        private List<RolePermission> _currentUserPermissionList;
        public List<RolePermission> CurrentUserPermissionList
        {
            get
            {
                //if (_currentUserPermissionList == null)
                //{
                //    if (CurrentUser!=null)   CompositePermissionList(CurrentUser);
                //}
                return _currentUserPermissionList;
            }
        }

        private void CompositePermissionList(User user)
        {
            if (_currentUserPermissionList == null) _currentUserPermissionList = new List<RolePermission>();
            foreach (var item in user.Roles)
            {
                Role role = RoleList.GetRoleListByRoleID(item.RoleID).First();
                IEnumerable<RolePermission> rp = role.Permissions as IEnumerable<RolePermission>;
                CurrentUserPermissionList.AddRange(rp);
            }

        }
#if DEBUG
        public void ReCompositePermissionList()
        {
            _currentUserPermissionList = new List<RolePermission>();
            foreach (var item in this.User.Roles)
            {
                Role role = RoleList.GetRoleListByRoleID(item.RoleID).First();
                IEnumerable<RolePermission> rp = role.Permissions as IEnumerable<RolePermission>;
                CurrentUserPermissionList.AddRange(rp);
            }

        }
#endif 
        #endregion

        #region  Data Access

        //public new RoleList Roles
        //{
        //    get
        //    {
        //        return this._roles;
        //    }
        //    private set
        //    {
        //        this._roles = value;
        //    }
        //}

        /// <summary>
        /// 是否参数代表部门或者它的上级部门
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        //public bool HavePermissionOnOrg(Guid orgId)
        //{
        //    return -1 != OrgsHavePermission.IndexOf(orgId);
        //}

        [NonSerialized]
        private IList<Guid> _orgsHavePermission;
        //private IList<Guid> OrgsHavePermission
//        {
//            get
//            {
//                //缓存拥有权限的部门
//                if (null == _orgsHavePermission)
//                {
//                    _orgsHavePermission = new List<Guid>();
//                    using (var db = DBHelper.CreateDb(ConnectionStringNames.OpenExpressApp))
//                    {
//                        foreach (OrgPosition item in _roles)
//                        {
//                            string sql = string.Format(@"
//                            with orgt
//                            as
//                            (
//	                            select * from Org where Id = '{0}'
//	                            union all 
//	                            select Org.* from orgt inner join Org on Org.Pid = orgt.Id
//                            )
//
//                            select * from orgt
//                            ", item.OrgId);
//                            var orgs = db.Exec(typeof(Org), "sys.sp_sqlexec", new object[] { sql });
//                            foreach (Org org in orgs)
//                                _orgsHavePermission.Add(org.Id);
//                        }
//                    }
//                }
//                return _orgsHavePermission;
//            }
//        }

        public bool HavePermissionOnOperation(Guid businessObjectId, Guid operationId)
        {
            bool result = false;
            foreach (var item in Roles)
            {
            //    result = result || item.OrgPositionOperations.HavePermissionOnOperation(businessObjectId, operationId);
                if (result) break;
            }
            return result;
        }

        /// <summary>
        /// 根据命令名，来查找权限
        /// </summary>
        /// <param name="cmName"></param>
        /// <returns></returns>
        public bool HavePermission(string cmName)
        {
            foreach (var item in _currentUserPermissionList)
            {
                if (item.Name == cmName) return true;
            }
            return false;
        }

        public string[] GetDataPermissionExpr(Guid businessObjectId)
        {
            //string[] exprs = new string[Roles.Count];
            //int index = 0;
            //foreach (var role in Roles)
            //{
            //var items = role.Permissions.Where<Permission>(x => x.BusinessObjectId == businessObjectId);
            //    exprs[index] = String.Empty;
            //    foreach (var item in items)
            //    {
            //        if (String.Empty != item.Expression)
            //            exprs[index] = exprs[index] + "And" + item.Expression;
            //    }
            //    if (!String.IsNullOrEmpty(exprs[index]))
            //    {
            //        exprs[index] = exprs[index].Substring("And".Length);
            //    }
            //    index++;
            //}
            //string[] result = new string[index];
            //index = 0;
            //foreach(var expr in exprs)
            //{
            //    if (!String.IsNullOrEmpty(expr))
            //    {
            //        result[index] = expr;
            //        index++;
            //    }
            //}
            //Array.Resize<string>(ref result, index);
            return  new string[0]; ;// result;
        }

        //private void DataPortal_Fetch(UsernameCriteria criteria)
        //{
        //    using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
        //    {
        //        using (var cmd = new SqlCommand("dbo.GetUserList", ctx.Connection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@Code", crit.Code).DbType = DbType.String;
        //            cmd.Parameters.AddWithValue("@Password", crit.Password).DbType = DbType.String;
        //            var args = new DataPortalHookArgs(cmd, crit);
        //            OnFetchPre(args);
        //            LoadCollection(cmd);
        //            OnFetchPost(args);
        //        }
        //    }
        //    foreach (var user in this)
        //    {
        //        user.FetchChildren();
        //    }
        //    //User = UserList.GetUserList(criteria.Username, criteria.password);


        //    //if (null != User)
        //    //{
        //    //    base.Name = User.Name;
        //    //    base.IsAuthenticated = true;
        //    //    //_roles = OrgPositionList.GetList(User.Id); // list of roles from security store
        //    //}
        //    //else
        //    //{
        //    //    base.Name = string.Empty;
        //    //    base.IsAuthenticated = false;
        //    //    _roles = null;
        //    //}
        //}

        //private void Fetch(User user)
        //{
        //  if (user != null)
        //  {
        //    Name = user.Username;
        //    IsAuthenticated = true;
        //    var roleList = new Csla.Core.MobileList<string>();
        //    var roles = from r in user.Roles select r;
        //    foreach (var role in roles)
        //      roleList.Add(role.Role1);
        //    Roles = roleList;
        //  }
        //  else
        //  {
        //    Name = "";
        //    IsAuthenticated = false;
        //    Roles = new Csla.Core.MobileList<string>();
        //  }
        //}

        #endregion

    }
}
