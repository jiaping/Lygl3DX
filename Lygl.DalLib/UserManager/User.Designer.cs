using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// User (editable child object).<br/>
    /// This is a generated base class of <see cref="User"/> business object.
    /// </summary>
    /// <remarks>
    /// This class contains one child collection:<br/>
    /// - <see cref="Roles"/> of type <see cref="UserRoleList"/> (M:M relation to <see cref="Role"/>)<br/>
    /// This class is an item of <see cref="UserList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class User : BusinessBase<User>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="Code"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code, "Code");
        /// <summary>
        /// Gets or sets the Code.
        /// </summary>
        /// <value>The Code.</value>
        public string Code
        {
            get { return GetProperty(CodeProperty); }
            set { SetProperty(CodeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Name"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Name");
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Password"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PasswordProperty = RegisterProperty<string>(p => p.Password, "Password");
        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        /// <value>The Password.</value>
        public string Password
        {
            get { return GetProperty(PasswordProperty); }
            set { SetProperty(PasswordProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LastLoginTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> LastLoginTimeProperty = RegisterProperty<SmartDate>(p => p.LastLoginTime, "Last Login Time");
        /// <summary>
        /// Gets or sets the Last Login Time.
        /// </summary>
        /// <value>The Last Login Time.</value>
        public string LastLoginTime
        {
            get { return GetPropertyConvert<SmartDate, String>(LastLoginTimeProperty); }
            set { SetPropertyConvert<SmartDate, String>(LastLoginTimeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LoginCount"/> property.
        /// </summary>
        public static readonly PropertyInfo<int?> LoginCountProperty = RegisterProperty<int?>(p => p.LoginCount, "Login Count");
        /// <summary>
        /// Gets or sets the Login Count.
        /// </summary>
        /// <value>The Login Count.</value>
        public int? LoginCount
        {
            get { return GetProperty(LoginCountProperty); }
            set { SetProperty(LoginCountProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MaxLoginCount"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MaxLoginCountProperty = RegisterProperty<int>(p => p.MaxLoginCount, "Max Login Count");
        /// <summary>
        /// Gets or sets the Max Login Count.
        /// </summary>
        /// <value>The Max Login Count.</value>
        public int MaxLoginCount
        {
            get { return GetProperty(MaxLoginCountProperty); }
            set { SetProperty(MaxLoginCountProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="UserID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> UserIDProperty = RegisterProperty<Guid>(p => p.UserID, "User ID");
        /// <summary>
        /// Gets or sets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
        public Guid UserID
        {
            get { return GetProperty(UserIDProperty); }
            set { SetProperty(UserIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="Roles"/> property.
        /// </summary>
        public static readonly PropertyInfo<UserRoleList> RolesProperty = RegisterProperty<UserRoleList>(p => p.Roles, "Roles", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Roles ("self load" child property).
        /// </summary>
        /// <value>The Roles.</value>
        public UserRoleList Roles
        {
            get { return GetProperty(RolesProperty); }
            set { SetProperty(RolesProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private User()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="User"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(LastLoginTimeProperty, null);
            LoadProperty(UserIDProperty, Guid.NewGuid());
            LoadProperty(RolesProperty, DataPortal.CreateChild<UserRoleList>());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="User"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(CodeProperty, dr.GetString("Code"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(PasswordProperty, dr.GetString("Password"));
            LoadProperty(LastLoginTimeProperty, !dr.IsDBNull("LastLoginTime") ? dr.GetSmartDate("LastLoginTime", true) : null);
            LoadProperty(LoginCountProperty, (int?)dr.GetValue("LoginCount"));
            LoadProperty(MaxLoginCountProperty, dr.GetInt32("MaxLoginCount"));
            LoadProperty(UserIDProperty, dr.GetGuid("UserID"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Loads child objects.
        /// </summary>
        internal void FetchChildren()
        {
            LoadProperty(RolesProperty, DataPortal.FetchChild<UserRoleList>(UserID));
        }

        /// <summary>
        /// Inserts a new <see cref="User"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddUser", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", ReadProperty(CodeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Password", ReadProperty(PasswordProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@LastLoginTime", ReadProperty(LastLoginTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@LoginCount", ReadProperty(LoginCountProperty) == null ? (object)DBNull.Value : ReadProperty(LoginCountProperty).Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@MaxLoginCount", ReadProperty(MaxLoginCountProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@UserID", ReadProperty(UserIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="User"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateUser", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Code", ReadProperty(CodeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Password", ReadProperty(PasswordProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@LastLoginTime", ReadProperty(LastLoginTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@LoginCount", ReadProperty(LoginCountProperty) == null ? (object)DBNull.Value : ReadProperty(LoginCountProperty).Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@MaxLoginCount", ReadProperty(MaxLoginCountProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@UserID", ReadProperty(UserIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
            }
        }

        /// <summary>
        /// Self deletes the <see cref="User"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                using (var cmd = new SqlCommand("dbo.DeleteUser", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", ReadProperty(UserIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
            }
            // removes all previous references to children
            LoadProperty(RolesProperty, DataPortal.CreateChild<UserRoleList>());
        }

        #endregion

        #region Pseudo Events

        /// <summary>
        /// Occurs after setting all defaults for object creation.
        /// </summary>
        partial void OnCreate(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Delete, after setting query parameters and before the delete operation.
        /// </summary>
        partial void OnDeletePre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Delete, after the delete operation, before Commit().
        /// </summary>
        partial void OnDeletePost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after setting query parameters and before the fetch operation.
        /// </summary>
        partial void OnFetchPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the fetch operation (object or collection is fully loaded and set up).
        /// </summary>
        partial void OnFetchPost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the low level fetch operation, before the data reader is destroyed.
        /// </summary>
        partial void OnFetchRead(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after setting query parameters and before the update operation.
        /// </summary>
        partial void OnUpdatePre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after the update operation, before setting back row identifiers (RowVersion) and Commit().
        /// </summary>
        partial void OnUpdatePost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after setting query parameters and before the insert operation.
        /// </summary>
        partial void OnInsertPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after the insert operation, before setting back row identifiers (ID and RowVersion) and Commit().
        /// </summary>
        partial void OnInsertPost(DataPortalHookArgs args);

        #endregion

    }
}
