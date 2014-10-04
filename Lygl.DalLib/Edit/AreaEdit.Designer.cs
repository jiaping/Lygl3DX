using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// AreaEdit (editable root object).<br/>
    /// This is a generated base class of <see cref="AreaEdit"/> business object.
    /// </summary>
    [Serializable]
    public partial class AreaEdit : BusinessBase<AreaEdit>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="AreaID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> AreaIDProperty = RegisterProperty<Guid>(p => p.AreaID, "Area ID");
        /// <summary>
        /// Gets or sets the Area ID.
        /// </summary>
        /// <value>The Area ID.</value>
        public Guid AreaID
        {
            get { return GetProperty(AreaIDProperty); }
            set { SetProperty(AreaIDProperty, value); }
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

        /// <summary>
        /// Maintains metadata about <see cref="Angle"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> AngleProperty = RegisterProperty<int>(p => p.Angle, "Angle");
        /// <summary>
        /// Gets or sets the Angle.
        /// </summary>
        /// <value>The Angle.</value>
        public int Angle
        {
            get { return GetProperty(AngleProperty); }
            set { SetProperty(AngleProperty, value); }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="AreaEdit"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="AreaEdit"/> object.</returns>
        public static AreaEdit NewAreaEdit()
        {
            return DataPortal.Create<AreaEdit>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="AreaEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="areaID">The AreaID parameter of the AreaEdit to fetch.</param>
        /// <returns>A reference to the fetched <see cref="AreaEdit"/> object.</returns>
        public static AreaEdit GetAreaEdit(Guid areaID)
        {
            return DataPortal.Fetch<AreaEdit>(areaID);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="AreaEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="areaID">The AreaID of the AreaEdit to delete.</param>
        public static void DeleteAreaEdit(Guid areaID)
        {
            DataPortal.Delete<AreaEdit>(areaID);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private AreaEdit()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="AreaEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(AreaIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="AreaEdit"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="areaID">The Area ID.</param>
        protected void DataPortal_Fetch(Guid areaID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetAreaEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AreaID", areaID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, areaID);
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
        /// Loads a <see cref="AreaEdit"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(AreaIDProperty, dr.GetGuid("AreaID"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(GeometryTextProperty, dr.GetString("GeometryText"));
            LoadProperty(AngleProperty, dr.GetInt32("Angle"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="AreaEdit"/> object in the database.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddAreaEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AreaID", ReadProperty(AreaIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@GeometryText", ReadProperty(GeometryTextProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Angle", ReadProperty(AngleProperty)).DbType = DbType.Int32;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="AreaEdit"/> object.
        /// </summary>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateAreaEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AreaID", ReadProperty(AreaIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@GeometryText", ReadProperty(GeometryTextProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Angle", ReadProperty(AngleProperty)).DbType = DbType.Int32;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="AreaEdit"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(AreaID);
        }

        /// <summary>
        /// Deletes the <see cref="AreaEdit"/> object from database.
        /// </summary>
        /// <param name="areaID">The delete criteria.</param>
        [Transactional(TransactionalTypes.TransactionScope)]
        protected void DataPortal_Delete(Guid areaID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteAreaEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AreaID", areaID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, areaID);
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
