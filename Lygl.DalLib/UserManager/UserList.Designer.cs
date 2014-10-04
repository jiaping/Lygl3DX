using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// UserList (editable root list).<br/>
    /// This is a generated base class of <see cref="UserList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="User"/> objects.
    /// </remarks>
    [Serializable]
    public partial class UserList : BusinessListBase<UserList, User>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="User"/> item from the collection.
        /// </summary>
        /// <param name="userID">The UserID of the item to be removed.</param>
        public void Remove(Guid userID)
        {
            foreach (var user in this)
            {
                if (user.UserID == userID)
                {
                    Remove(user);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="User"/> item is in the collection.
        /// </summary>
        /// <param name="userID">The UserID of the item to search for.</param>
        /// <returns><c>true</c> if the User is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid userID)
        {
            foreach (var user in this)
            {
                if (user.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="User"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="userID">The UserID of the item to search for.</param>
        /// <returns><c>true</c> if the User is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid userID)
        {
            foreach (var user in this.DeletedList)
            {
                if (user.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="UserList"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="UserList"/> collection.</returns>
        public static UserList NewUserList()
        {
            return DataPortal.Create<UserList>();
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID of the UserList to create.</param>
        /// <returns>A reference to the created <see cref="UserList"/> collection.</returns>
        public static UserList NewUserList(Guid userID)
        {
            return DataPortal.Create<UserList>(userID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="UserList"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="UserList"/> collection.</returns>
        public static UserList GetUserList()
        {
            return DataPortal.Fetch<UserList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID parameter of the UserList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="UserList"/> collection.</returns>
        public static UserList GetUserListByUserID(Guid userID)
        {
            return DataPortal.Fetch<UserList>(userID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <returns>A reference to the fetched <see cref="UserList"/> collection.</returns>
        public static UserList GetUserListByUserCodePwd(CriteriaUserCodePwd crit)
        {
            return DataPortal.Fetch<UserList>(crit);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="code">The Code parameter of the UserList to fetch.</param>
        /// <param name="password">The Password parameter of the UserList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="UserList"/> collection.</returns>
        public static UserList GetUserListByUserCodePwd(string code, string password)
        {
            return DataPortal.Fetch<UserList>(new CriteriaUserCodePwd(code, password));
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="UserList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewUserList(EventHandler<DataPortalResult<UserList>> callback)
        {
            DataPortal.BeginCreate<UserList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID of the UserList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewUserList(Guid userID, EventHandler<DataPortalResult<UserList>> callback)
        {
            DataPortal.BeginCreate<UserList>(userID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserList(EventHandler<DataPortalResult<UserList>> callback)
        {
            DataPortal.BeginFetch<UserList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID parameter of the UserList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserListByUserID(Guid userID, EventHandler<DataPortalResult<UserList>> callback)
        {
            DataPortal.BeginFetch<UserList>(userID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserListByUserCodePwd(CriteriaUserCodePwd crit, EventHandler<DataPortalResult<UserList>> callback)
        {
            DataPortal.BeginFetch<UserList>(crit, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="code">The Code parameter of the UserList to fetch.</param>
        /// <param name="password">The Password parameter of the UserList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserListByUserCodePwd(string code, string password, EventHandler<DataPortalResult<UserList>> callback)
        {
            DataPortal.BeginFetch<UserList>(new CriteriaUserCodePwd(code, password), callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private UserList()
        {
            // Prevent direct creation

            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            AllowNew = true;
            AllowEdit = true;
            AllowRemove = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="UserList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
            foreach (var item in this)
            {
                item.FetchChildren();
            }
        }

        /// <summary>
        /// Loads a <see cref="UserList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="userID">The User ID.</param>
        protected void DataPortal_Fetch(Guid userID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserListByUserID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, userID);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
            foreach (var item in this)
            {
                item.FetchChildren();
            }
        }

        /// <summary>
        /// Loads a <see cref="UserList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaUserCodePwd crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserListByUserCodePwd", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", crit.Code).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Password", crit.Password).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd, crit);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
            foreach (var item in this)
            {
                item.FetchChildren();
            }
        }

        private void LoadCollection(SqlCommand cmd)
        {
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                Fetch(dr);
            }
        }

        /// <summary>
        /// Loads all <see cref="UserList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<User>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="UserList"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                base.Child_Update();
                ctx.Commit();
            }
        }

        #endregion

        #region Pseudo Events

        /// <summary>
        /// Occurs after setting query parameters and before the fetch operation.
        /// </summary>
        partial void OnFetchPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the fetch operation (object or collection is fully loaded and set up).
        /// </summary>
        partial void OnFetchPost(DataPortalHookArgs args);

        #endregion

    }

    #region Criteria

    /// <summary>
    /// CriteriaUserCodePwd criteria.
    /// </summary>
    [Serializable]
    public class CriteriaUserCodePwd : CriteriaBase<CriteriaUserCodePwd>
    {

        /// <summary>
        /// Maintains metadata about <see cref="Code"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
        /// <summary>
        /// Gets the Code.
        /// </summary>
        /// <value>The Code.</value>
        public string Code
        {
            get { return ReadProperty(CodeProperty); }
            private set { LoadProperty(CodeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Password"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(p => p.Password);
        /// <summary>
        /// Gets the Password.
        /// </summary>
        /// <value>The Password.</value>
        public string Password
        {
            get { return ReadProperty(PasswordProperty); }
            private set { LoadProperty(PasswordProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaUserCodePwd"/> class.
        /// </summary>
        /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CriteriaUserCodePwd()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaUserCodePwd"/> class.
        /// </summary>
        /// <param name="code">The Code.</param>
        /// <param name="password">The Password.</param>
        public CriteriaUserCodePwd(string code, string password)
        {
            Code = code;
            Password = password;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaUserCodePwd)
            {
                var c = (CriteriaUserCodePwd) obj;
                if (!Code.Equals(c.Code))
                    return false;
                if (!Password.Equals(c.Password))
                    return false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>An hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return string.Concat("CriteriaUserCodePwd", Code.ToString(), Password.ToString()).GetHashCode();
        }
    }

    #endregion

}
