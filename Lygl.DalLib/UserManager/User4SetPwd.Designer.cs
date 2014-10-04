using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// User4SetPwd (editable root object).<br/>
    /// This is a generated base class of <see cref="User4SetPwd"/> business object.
    /// </summary>
    [Serializable]
    public partial class User4SetPwd : BusinessBase<User4SetPwd>
    {

        #region Business Properties

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

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="User4SetPwd"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="User4SetPwd"/> object.</returns>
        public static User4SetPwd NewUser4SetPwd()
        {
            return DataPortal.Create<User4SetPwd>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="User4SetPwd"/> object, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID parameter of the User4SetPwd to fetch.</param>
        /// <returns>A reference to the fetched <see cref="User4SetPwd"/> object.</returns>
        public static User4SetPwd GetUser4SetPwd(Guid userID)
        {
            return DataPortal.Fetch<User4SetPwd>(userID);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="User4SetPwd"/> object, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID of the User4SetPwd to delete.</param>
        public static void DeleteUser4SetPwd(Guid userID)
        {
            DataPortal.Delete<User4SetPwd>(userID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="User4SetPwd"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewUser4SetPwd(EventHandler<DataPortalResult<User4SetPwd>> callback)
        {
            DataPortal.BeginCreate<User4SetPwd>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="User4SetPwd"/> object, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID parameter of the User4SetPwd to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUser4SetPwd(Guid userID, EventHandler<DataPortalResult<User4SetPwd>> callback)
        {
            DataPortal.BeginFetch<User4SetPwd>(userID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="User4SetPwd"/> object, based on given parameters.
        /// </summary>
        /// <param name="userID">The UserID of the User4SetPwd to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteUser4SetPwd(Guid userID, EventHandler<DataPortalResult<User4SetPwd>> callback)
        {
            DataPortal.BeginDelete<User4SetPwd>(userID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="User4SetPwd"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private User4SetPwd()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="User4SetPwd"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(UserIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="User4SetPwd"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="userID">The User ID.</param>
        protected void DataPortal_Fetch(Guid userID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUser4SetPwd", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, userID);
                    OnFetchPre(args);
                    Fetch(cmd);
                    OnFetchPost(args);
                }
            }
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        private void Fetch(SqlCommand cmd)
        {
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                if (dr.Read())
                {
                    Fetch(dr);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="User4SetPwd"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(UserIDProperty, dr.GetGuid("UserID"));
            LoadProperty(PasswordProperty, dr.GetString("Password"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="User4SetPwd"/> object in the database.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddUser4SetPwd", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", ReadProperty(UserIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Password", ReadProperty(PasswordProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="User4SetPwd"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateUser4SetPwd", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", ReadProperty(UserIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Password", ReadProperty(PasswordProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Self deletes the <see cref="User4SetPwd"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(UserID);
        }

        /// <summary>
        /// Deletes the <see cref="User4SetPwd"/> object from database.
        /// </summary>
        /// <param name="userID">The delete criteria.</param>
        protected void DataPortal_Delete(Guid userID)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteUser4SetPwd", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, userID);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
                ctx.Commit();
            }
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
