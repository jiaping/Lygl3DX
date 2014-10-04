using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// PathEdit (editable root object).<br/>
    /// This is a generated base class of <see cref="PathEdit"/> business object.
    /// </summary>
    [Serializable]
    public partial class PathEdit : BusinessBase<PathEdit>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="PathID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> PathIDProperty = RegisterProperty<Guid>(p => p.PathID, "Path ID");
        /// <summary>
        /// Gets or sets the Path ID.
        /// </summary>
        /// <value>The Path ID.</value>
        public Guid PathID
        {
            get { return GetProperty(PathIDProperty); }
            set { SetProperty(PathIDProperty, value); }
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
        /// Maintains metadata about <see cref="GeometryText"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> GeometryTextProperty = RegisterProperty<string>(p => p.GeometryText, "Geometry Text");
        /// <summary>
        /// Gets or sets the Geometry Text.
        /// </summary>
        /// <value>The Geometry Text.</value>
        public string GeometryText
        {
            get { return GetProperty(GeometryTextProperty); }
            set { SetProperty(GeometryTextProperty, value); }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="PathEdit"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="PathEdit"/> object.</returns>
        public static PathEdit NewPathEdit()
        {
            return DataPortal.Create<PathEdit>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="PathEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="pathID">The PathID parameter of the PathEdit to fetch.</param>
        /// <returns>A reference to the fetched <see cref="PathEdit"/> object.</returns>
        public static PathEdit GetPathEdit(Guid pathID)
        {
            return DataPortal.Fetch<PathEdit>(pathID);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="PathEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="pathID">The PathID of the PathEdit to delete.</param>
        public static void DeletePathEdit(Guid pathID)
        {
            DataPortal.Delete<PathEdit>(pathID);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PathEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private PathEdit()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="PathEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(PathIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="PathEdit"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="pathID">The Path ID.</param>
        protected void DataPortal_Fetch(Guid pathID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetPathEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PathID", pathID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, pathID);
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
        /// Loads a <see cref="PathEdit"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(PathIDProperty, dr.GetGuid("PathID"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(GeometryTextProperty, dr.GetString("GeometryText"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="PathEdit"/> object in the database.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddPathEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PathID", ReadProperty(PathIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@GeometryText", ReadProperty(GeometryTextProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="PathEdit"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdatePathEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PathID", ReadProperty(PathIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@GeometryText", ReadProperty(GeometryTextProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Self deletes the <see cref="PathEdit"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(PathID);
        }

        /// <summary>
        /// Deletes the <see cref="PathEdit"/> object from database.
        /// </summary>
        /// <param name="pathID">The delete criteria.</param>
        protected void DataPortal_Delete(Guid pathID)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeletePathEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PathID", pathID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, pathID);
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
