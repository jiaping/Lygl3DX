using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Lygl.DalLib.NVL;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// MxEdit (editable root object).<br/>
    /// This is a generated base class of <see cref="MxEdit"/> business object.
    /// </summary>
    [Serializable]
    public partial class MxEdit : BusinessBase<MxEdit>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID, "Mx ID");
        /// <summary>
        /// Gets or sets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid MxID
        {
            get { return GetProperty(MxIDProperty); }
            set { SetProperty(MxIDProperty, value); }
        }

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
        /// Maintains metadata about <see cref="Price"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> PriceProperty = RegisterProperty<Decimal?>(p => p.Price, "Price");
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        /// <value>The Price.</value>
        public Decimal? Price
        {
            get { return GetProperty(PriceProperty); }
            set { SetProperty(PriceProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Rmsj"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> RmsjProperty = RegisterProperty<SmartDate>(p => p.Rmsj, "Rmsj");
        /// <summary>
        /// Gets or sets the Rmsj.
        /// </summary>
        /// <value>The Rmsj.</value>
        public string Rmsj
        {
            get { return GetPropertyConvert<SmartDate, String>(RmsjProperty); }
            set { SetPropertyConvert<SmartDate, String>(RmsjProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ManageFeeEndTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> ManageFeeEndTimeProperty = RegisterProperty<SmartDate>(p => p.ManageFeeEndTime, "Manage Fee End Time");
        /// <summary>
        /// Gets or sets the Manage Fee End Time.
        /// </summary>
        /// <value>The Manage Fee End Time.</value>
        public string ManageFeeEndTime
        {
            get { return GetPropertyConvert<SmartDate, String>(ManageFeeEndTimeProperty); }
            set { SetPropertyConvert<SmartDate, String>(ManageFeeEndTimeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="KbrName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> KbrNameProperty = RegisterProperty<string>(p => p.KbrName, "Kbr Name");
        /// <summary>
        /// Gets or sets the Kbr Name.
        /// </summary>
        /// <value>The Kbr Name.</value>
        public string KbrName
        {
            get { return GetProperty(KbrNameProperty); }
            set { SetProperty(KbrNameProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Bwjf"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool?> BwjfProperty = RegisterProperty<bool?>(p => p.Bwjf, "Bwjf");
        /// <summary>
        /// Gets or sets the Bwjf.
        /// </summary>
        /// <value><c>true</c> if Bwjf; <c>false</c> if not Bwjf; otherwise, <c>null</c>.</value>
        public bool? Bwjf
        {
            get { return GetProperty(BwjfProperty); }
            set { SetProperty(BwjfProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Mz"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MzProperty = RegisterProperty<string>(p => p.Mz, "Mz");
        /// <summary>
        /// Gets or sets the Mz.
        /// </summary>
        /// <value>The Mz.</value>
        public string Mz
        {
            get { return GetProperty(MzProperty); }
            set { SetProperty(MzProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="CreateDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> CreateDateProperty = RegisterProperty<SmartDate>(p => p.CreateDate, "Create Date");
        /// <summary>
        /// Gets or sets the Create Date.
        /// </summary>
        /// <value>The Create Date.</value>
        public SmartDate CreateDate
        {
            get { return GetProperty(CreateDateProperty); }
            set { SetProperty(CreateDateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="SzName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SzNameProperty = RegisterProperty<string>(p => p.SzName, "Sz Name");
        /// <summary>
        /// Gets or sets the Sz Name.
        /// </summary>
        /// <value>The Sz Name.</value>
        public string SzName
        {
            get { return GetProperty(SzNameProperty); }
            set { SetProperty(SzNameProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MxNameProperty = RegisterProperty<string>(p => p.MxName, "Mx Name");
        /// <summary>
        /// Gets or sets the Mx Name.
        /// </summary>
        /// <value>The Mx Name.</value>
        public string MxName
        {
            get { return GetProperty(MxNameProperty); }
            set { SetProperty(MxNameProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxTypeID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MxTypeIDProperty = RegisterProperty<int>(p => p.MxTypeID, "Mx Type ID");
        /// <summary>
        /// Gets or sets the Mx Type ID.
        /// </summary>
        /// <value>The Mx Type ID.</value>
        public int MxTypeID
        {
            get { return GetProperty(MxTypeIDProperty); }
            set
            {
                SetProperty(MxTypeIDProperty, value);
                OnPropertyChanged("MxType");
            }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxStatusID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MxStatusIDProperty = RegisterProperty<int>(p => p.MxStatusID, "Mx Status ID");
        /// <summary>
        /// Gets or sets the Mx Status ID.
        /// </summary>
        /// <value>The Mx Status ID.</value>
        public int MxStatusID
        {
            get { return GetProperty(MxStatusIDProperty); }
            set
            {
                SetProperty(MxStatusIDProperty, value);
                OnPropertyChanged("MxStatus");
            }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxStyleID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MxStyleIDProperty = RegisterProperty<int>(p => p.MxStyleID, "Mx Style ID");
        /// <summary>
        /// Gets or sets the Mx Style ID.
        /// </summary>
        /// <value>The Mx Style ID.</value>
        public int MxStyleID
        {
            get { return GetProperty(MxStyleIDProperty); }
            set
            {
                SetProperty(MxStyleIDProperty, value);
                OnPropertyChanged("MxStyle");
            }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Pos"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PosProperty = RegisterProperty<string>(p => p.Pos, "Pos");
        /// <summary>
        /// Gets or sets the Pos.
        /// </summary>
        /// <value>The Pos.</value>
        public string Pos
        {
            get { return GetProperty(PosProperty); }
            set { SetProperty(PosProperty, value); }
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

        /// <summary>
        /// Gets or sets the Mx Type.
        /// </summary>
        /// <value>The Mx Type.</value>
        public string MxType
        {
            get
            {
                var result = string.Empty;
                if (MxTypeNVL.GetMxTypeNVL().ContainsKey(MxTypeID))
                    result = MxTypeNVL.GetMxTypeNVL().GetItemByKey(MxTypeID).Value;
                return result;
            }
            set
            {
                if (MxTypeNVL.GetMxTypeNVL().ContainsValue(value))
                {
                    var result = MxTypeNVL.GetMxTypeNVL().GetItemByValue(value).Key;
                    if (result != MxTypeID)
                        MxTypeID = result;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Mx Status.
        /// </summary>
        /// <value>The Mx Status.</value>
        public string MxStatus
        {
            get
            {
                var result = string.Empty;
                if (MxStatusNVL.GetMxStatusNVL().ContainsKey(MxStatusID))
                    result = MxStatusNVL.GetMxStatusNVL().GetItemByKey(MxStatusID).Value;
                return result;
            }
            set
            {
                if (MxStatusNVL.GetMxStatusNVL().ContainsValue(value))
                {
                    var result = MxStatusNVL.GetMxStatusNVL().GetItemByValue(value).Key;
                    if (result != MxStatusID)
                        MxStatusID = result;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Mx Style.
        /// </summary>
        /// <value>The Mx Style.</value>
        public string MxStyle
        {
            get
            {
                var result = string.Empty;
                if (MxStyleNVL.GetMxStyleNVL().ContainsKey(MxStyleID))
                    result = MxStyleNVL.GetMxStyleNVL().GetItemByKey(MxStyleID).Value;
                return result;
            }
            set
            {
                if (MxStyleNVL.GetMxStyleNVL().ContainsValue(value))
                {
                    var result = MxStyleNVL.GetMxStyleNVL().GetItemByValue(value).Key;
                    if (result != MxStyleID)
                        MxStyleID = result;
                }
            }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="MxEdit"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="MxEdit"/> object.</returns>
        public static MxEdit NewMxEdit()
        {
            return DataPortal.Create<MxEdit>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="MxEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the MxEdit to fetch.</param>
        /// <returns>A reference to the fetched <see cref="MxEdit"/> object.</returns>
        public static MxEdit GetMxEdit(Guid mxID)
        {
            return DataPortal.Fetch<MxEdit>(mxID);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="MxEdit"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the MxEdit to delete.</param>
        public static void DeleteMxEdit(Guid mxID)
        {
            DataPortal.Delete<MxEdit>(mxID);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxEdit()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="MxEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(MxIDProperty, Guid.NewGuid());
            LoadProperty(RmsjProperty, null);
            LoadProperty(ManageFeeEndTimeProperty, null);
            LoadProperty(KbrNameProperty, null);
            LoadProperty(MzProperty, null);
            LoadProperty(CreateDateProperty, DateTime.Now);
            LoadProperty(SzNameProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="MxEdit"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxEdit", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", mxID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, mxID);
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
        /// Loads a <see cref="MxEdit"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(AreaIDProperty, dr.GetGuid("AreaID"));
            LoadProperty(PriceProperty, (Decimal?)dr.GetValue("Price"));
            LoadProperty(RmsjProperty, !dr.IsDBNull("Rmsj") ? dr.GetSmartDate("Rmsj", true) : null);
            LoadProperty(ManageFeeEndTimeProperty, !dr.IsDBNull("ManageFeeEndTime") ? dr.GetSmartDate("ManageFeeEndTime", true) : null);
            LoadProperty(KbrNameProperty, !dr.IsDBNull("KbrName") ? dr.GetString("KbrName") : null);
            LoadProperty(BwjfProperty, (bool?)dr.GetValue("Bwjf"));
            LoadProperty(MzProperty, !dr.IsDBNull("Mz") ? dr.GetString("Mz") : null);
            LoadProperty(CreateDateProperty, !dr.IsDBNull("CreateDate") ? dr.GetSmartDate("CreateDate", true) : null);
            LoadProperty(SzNameProperty, !dr.IsDBNull("SzName") ? dr.GetString("SzName") : null);
            LoadProperty(MxNameProperty, dr.GetString("MxName"));
            LoadProperty(MxTypeIDProperty, dr.GetInt32("MxTypeID"));
            LoadProperty(MxStatusIDProperty, dr.GetInt32("MxStatusID"));
            LoadProperty(MxStyleIDProperty, dr.GetInt32("MxStyleID"));
            LoadProperty(PosProperty, dr.GetString("Pos"));
            LoadProperty(AngleProperty, dr.GetInt32("Angle"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="MxEdit"/> object in the database.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            SimpleAuditTrail();
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddMxEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@AreaID", ReadProperty(AreaIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty) == null ? (object)DBNull.Value : ReadProperty(PriceProperty).Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Rmsj", ReadProperty(RmsjProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@ManageFeeEndTime", ReadProperty(ManageFeeEndTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@KbrName", ReadProperty(KbrNameProperty) == null ? (object)DBNull.Value : ReadProperty(KbrNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Bwjf", ReadProperty(BwjfProperty) == null ? (object)DBNull.Value : ReadProperty(BwjfProperty).Value).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@Mz", ReadProperty(MzProperty) == null ? (object)DBNull.Value : ReadProperty(MzProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@CreateDate", ReadProperty(CreateDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@SzName", ReadProperty(SzNameProperty) == null ? (object)DBNull.Value : ReadProperty(SzNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxName", ReadProperty(MxNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxTypeID", ReadProperty(MxTypeIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@MxStatusID", ReadProperty(MxStatusIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@MxStyleID", ReadProperty(MxStyleIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@Pos", ReadProperty(PosProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Angle", ReadProperty(AngleProperty)).DbType = DbType.Int32;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="MxEdit"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            SimpleAuditTrail();
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateMxEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@AreaID", ReadProperty(AreaIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty) == null ? (object)DBNull.Value : ReadProperty(PriceProperty).Value).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Rmsj", ReadProperty(RmsjProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@ManageFeeEndTime", ReadProperty(ManageFeeEndTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@KbrName", ReadProperty(KbrNameProperty) == null ? (object)DBNull.Value : ReadProperty(KbrNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Bwjf", ReadProperty(BwjfProperty) == null ? (object)DBNull.Value : ReadProperty(BwjfProperty).Value).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@Mz", ReadProperty(MzProperty) == null ? (object)DBNull.Value : ReadProperty(MzProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@SzName", ReadProperty(SzNameProperty) == null ? (object)DBNull.Value : ReadProperty(SzNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxName", ReadProperty(MxNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxTypeID", ReadProperty(MxTypeIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@MxStatusID", ReadProperty(MxStatusIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@MxStyleID", ReadProperty(MxStyleIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@Pos", ReadProperty(PosProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Angle", ReadProperty(AngleProperty)).DbType = DbType.Int32;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                ctx.Commit();
            }
        }

        private void SimpleAuditTrail()
        {
            if (IsNew)
            {
                LoadProperty(CreateDateProperty, DateTime.Now);
            }
        }

        /// <summary>
        /// Self deletes the <see cref="MxEdit"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(MxID);
        }

        /// <summary>
        /// Deletes the <see cref="MxEdit"/> object from database.
        /// </summary>
        /// <param name="mxID">The delete criteria.</param>
        protected void DataPortal_Delete(Guid mxID)
        {
            // audit the object, just in case soft delete is used on this object
            SimpleAuditTrail();
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteMxEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", mxID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, mxID);
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
