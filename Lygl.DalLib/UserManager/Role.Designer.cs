using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// Role (editable child object).<br/>
    /// This is a generated base class of <see cref="Role"/> business object.
    /// </summary>
    /// <remarks>
    /// This class contains two child collections:<br/>
    /// - <see cref="Users"/> of type <see cref="RoleUserList"/> (M:M relation to <see cref="User"/>)<br/>
    /// - <see cref="Permissions"/> of type <see cref="RolePermissionList"/> (1:M relation to <see cref="RolePermission"/>)<br/>
    /// This class is an item of <see cref="RoleList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class Role : BusinessBase<Role>
    {

        #region Business Properties

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
        /// Maintains metadata about <see cref="RoleID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> RoleIDProperty = RegisterProperty<Guid>(p => p.RoleID, "Role ID");
        /// <summary>
        /// Gets or sets the Role ID.
        /// </summary>
        /// <value>The Role ID.</value>
        public Guid RoleID
        {
            get { return GetProperty(RoleIDProperty); }
            set { SetProperty(RoleIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Description"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(p => p.Description, "Description");
        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        /// <value>The Description.</value>
        public string Description
        {
            get { return GetProperty(DescriptionProperty); }
            set { SetProperty(DescriptionProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="Users"/> property.
        /// </summary>
        public static readonly PropertyInfo<RoleUserList> UsersProperty = RegisterProperty<RoleUserList>(p => p.Users, "Users", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Users ("self load" child property).
        /// </summary>
        /// <value>The Users.</value>
        public RoleUserList Users
        {
            get { return GetProperty(UsersProperty); }
            private set { LoadProperty(UsersProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="Permissions"/> property.
        /// </summary>
        public static readonly PropertyInfo<RolePermissionList> PermissionsProperty = RegisterProperty<RolePermissionList>(p => p.Permissions, "Permissions", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Permissions ("self load" child property).
        /// </summary>
        /// <value>The Permissions.</value>
        public RolePermissionList Permissions
        {
            get { return GetProperty(PermissionsProperty); }
            set { SetProperty(PermissionsProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private Role()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="Role"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(RoleIDProperty, Guid.NewGuid());
            LoadProperty(DescriptionProperty, null);
            LoadProperty(UsersProperty, DataPortal.CreateChild<RoleUserList>());
            LoadProperty(PermissionsProperty, DataPortal.CreateChild<RolePermissionList>());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="Role"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(RoleIDProperty, dr.GetGuid("RoleID"));
            LoadProperty(DescriptionProperty, !dr.IsDBNull("Description") ? dr.GetString("Description") : null);
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
            LoadProperty(UsersProperty, DataPortal.FetchChild<RoleUserList>(RoleID));
            LoadProperty(PermissionsProperty, DataPortal.FetchChild<RolePermissionList>(RoleID));
        }

        /// <summary>
        /// Inserts a new <see cref="Role"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddRole", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@RoleID", ReadProperty(RoleIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Description", ReadProperty(DescriptionProperty) == null ? (object)DBNull.Value : ReadProperty(DescriptionProperty)).DbType = DbType.String;
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
        /// Updates in the database all changes made to the <see cref="Role"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateRole", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@RoleID", ReadProperty(RoleIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Description", ReadProperty(DescriptionProperty) == null ? (object)DBNull.Value : ReadProperty(DescriptionProperty)).DbType = DbType.String;
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
        /// Self deletes the <see cref="Role"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                using (var cmd = new SqlCommand("dbo.DeleteRole", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", ReadProperty(RoleIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
            }
            // removes all previous references to children
            LoadProperty(UsersProperty, DataPortal.CreateChild<RoleUserList>());
            LoadProperty(PermissionsProperty, DataPortal.CreateChild<RolePermissionList>());
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
