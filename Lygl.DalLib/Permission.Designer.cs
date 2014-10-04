using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib
{

    /// <summary>
    /// Permission (editable child object).<br/>
    /// This is a generated base class of <see cref="Permission"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="PermissionColl"/> collection.
    /// </remarks>
    [Serializable]
    public partial class Permission : BusinessBase<Permission>
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
        /// Maintains metadata about <see cref="PermissionID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> PermissionIDProperty = RegisterProperty<Guid>(p => p.PermissionID, "Permission ID");
        /// <summary>
        /// Gets or sets the Permission ID.
        /// </summary>
        /// <value>The Permission ID.</value>
        public Guid PermissionID
        {
            get { return GetProperty(PermissionIDProperty); }
            set { SetProperty(PermissionIDProperty, value); }
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Permission"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private Permission()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="Permission"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(PermissionIDProperty, Guid.NewGuid());
            LoadProperty(DescriptionProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="Permission"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(PermissionIDProperty, dr.GetGuid("PermissionID"));
            LoadProperty(DescriptionProperty, !dr.IsDBNull("Description") ? dr.GetString("Description") : null);
            LoadProperty(RoleIDProperty, dr.GetGuid("RoleID"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="Permission"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddPermission", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@PermissionID", ReadProperty(PermissionIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Description", ReadProperty(DescriptionProperty) == null ? (object)DBNull.Value : ReadProperty(DescriptionProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@RoleID", ReadProperty(RoleIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="Permission"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdatePermission", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@PermissionID", ReadProperty(PermissionIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Description", ReadProperty(DescriptionProperty) == null ? (object)DBNull.Value : ReadProperty(DescriptionProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@RoleID", ReadProperty(RoleIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="Permission"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeletePermission", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PermissionID", ReadProperty(PermissionIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
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
