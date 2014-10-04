using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Lygl.DalLib.NVL;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BusinessLb (editable root object).<br/>
    /// This is a generated base class of <see cref="BusinessLb"/> business object.
    /// </summary>
    /// <remarks>
    /// This class contains two child collections:<br/>
    /// - <see cref="LbItems"/> of type <see cref="LbItemList"/> (1:M relation to <see cref="LbItem"/>)<br/>
    /// - <see cref="BwSzs"/> of type <see cref="BwSzList"/> (1:M relation to <see cref="BwSz"/>)
    /// </remarks>
    [Serializable]
    public partial class BusinessLb : BusinessBase<BusinessLb>, IBusinessHasPayFlag
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="BusinessID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> BusinessIDProperty = RegisterProperty<Guid>(p => p.BusinessID, "Business ID");
        /// <summary>
        /// Gets or sets the Business ID.
        /// </summary>
        /// <value>The Business ID.</value>
        public Guid BusinessID
        {
            get { return GetProperty(BusinessIDProperty); }
            set { SetProperty(BusinessIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="BusinessName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> BusinessNameProperty = RegisterProperty<string>(p => p.BusinessName, "Business Name");
        /// <summary>
        /// Gets or sets the Business Name.
        /// </summary>
        /// <value>The Business Name.</value>
        public string BusinessName
        {
            get { return GetProperty(BusinessNameProperty); }
            set { SetProperty(BusinessNameProperty, value); }
        }

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
        /// Maintains metadata about <see cref="OperatorID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> OperatorIDProperty = RegisterProperty<Guid>(p => p.OperatorID, "Operator ID");
        /// <summary>
        /// Gets or sets the Operator ID.
        /// </summary>
        /// <value>The Operator ID.</value>
        public Guid OperatorID
        {
            get { return GetProperty(OperatorIDProperty); }
            set
            {
                SetProperty(OperatorIDProperty, value);
                OnPropertyChanged("OperatorCode");
            }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Drawee"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> DraweeProperty = RegisterProperty<string>(p => p.Drawee, "Drawee");
        /// <summary>
        /// Gets or sets the Drawee.
        /// </summary>
        /// <value>The Drawee.</value>
        public string Drawee
        {
            get { return GetProperty(DraweeProperty); }
            set { SetProperty(DraweeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Price"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> PriceProperty = RegisterProperty<Decimal>(p => p.Price, "Price");
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        /// <value>The Price.</value>
        public Decimal Price
        {
            get { return GetProperty(PriceProperty); }
            set { SetProperty(PriceProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PayFlag"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> PayFlagProperty = RegisterProperty<bool>(p => p.PayFlag, "Pay Flag");
        /// <summary>
        /// Gets or sets the Pay Flag.
        /// </summary>
        /// <value><c>true</c> if Pay Flag; otherwise, <c>false</c>.</value>
        public bool PayFlag
        {
            get { return GetProperty(PayFlagProperty); }
            set { SetProperty(PayFlagProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="OperateTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> OperateTimeProperty = RegisterProperty<SmartDate>(p => p.OperateTime, "Operate Time");
        /// <summary>
        /// Gets or sets the Operate Time.
        /// </summary>
        /// <value>The Operate Time.</value>
        public SmartDate OperateTime
        {
            get { return GetProperty(OperateTimeProperty); }
            set { SetProperty(OperateTimeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LbrText"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> LbrTextProperty = RegisterProperty<string>(p => p.LbrText, "Lbr Text");
        /// <summary>
        /// Gets or sets the Lbr Text.
        /// </summary>
        /// <value>The Lbr Text.</value>
        public string LbrText
        {
            get { return GetProperty(LbrTextProperty); }
            set { SetProperty(LbrTextProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LbsjText"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> LbsjTextProperty = RegisterProperty<string>(p => p.LbsjText, "Lbsj Text");
        /// <summary>
        /// Gets or sets the Lbsj Text.
        /// </summary>
        /// <value>The Lbsj Text.</value>
        public string LbsjText
        {
            get { return GetProperty(LbsjTextProperty); }
            set { SetProperty(LbsjTextProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Bx"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> BxProperty = RegisterProperty<string>(p => p.Bx, "Bx");
        /// <summary>
        /// Gets or sets the Bx.
        /// </summary>
        /// <value>The Bx.</value>
        public string Bx
        {
            get { return GetProperty(BxProperty); }
            set { SetProperty(BxProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="LbDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> LbDateProperty = RegisterProperty<SmartDate>(p => p.LbDate, "Lb Date");
        /// <summary>
        /// Gets or sets the Lb Date.
        /// </summary>
        /// <value>The Lb Date.</value>
        public string LbDate
        {
            get { return GetPropertyConvert<SmartDate, String>(LbDateProperty); }
            set { SetPropertyConvert<SmartDate, String>(LbDateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="KzSj"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> KzSjProperty = RegisterProperty<SmartDate>(p => p.KzSj, "Kz Sj");
        /// <summary>
        /// Gets or sets the Kz Sj.
        /// </summary>
        /// <value>The Kz Sj.</value>
        public string KzSj
        {
            get { return GetPropertyConvert<SmartDate, String>(KzSjProperty); }
            set { SetPropertyConvert<SmartDate, String>(KzSjProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="SgSj"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> SgSjProperty = RegisterProperty<SmartDate>(p => p.SgSj, "Sg Sj");
        /// <summary>
        /// Gets or sets the Sg Sj.
        /// </summary>
        /// <value>The Sg Sj.</value>
        public string SgSj
        {
            get { return GetPropertyConvert<SmartDate, String>(SgSjProperty); }
            set { SetPropertyConvert<SmartDate, String>(SgSjProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Bw"/> property.
        /// </summary>
        public static readonly PropertyInfo<byte[]> BwProperty = RegisterProperty<byte[]>(p => p.Bw, "Bw");
        /// <summary>
        /// Gets or sets the Bw.
        /// </summary>
        /// <value>The Bw.</value>
        public byte[] Bw
        {
            get { return GetProperty(BwProperty); }
            set { SetProperty(BwProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Kzg"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> KzgProperty = RegisterProperty<string>(p => p.Kzg, "Kzg");
        /// <summary>
        /// Gets or sets the Kzg.
        /// </summary>
        /// <value>The Kzg.</value>
        public string Kzg
        {
            get { return GetProperty(KzgProperty); }
            set { SetProperty(KzgProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Sgy"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SgyProperty = RegisterProperty<string>(p => p.Sgy, "Sgy");
        /// <summary>
        /// Gets or sets the Sgy.
        /// </summary>
        /// <value>The Sgy.</value>
        public string Sgy
        {
            get { return GetProperty(SgyProperty); }
            set { SetProperty(SgyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Operator Code.
        /// </summary>
        /// <value>The Operator Code.</value>
        public string OperatorCode
        {
            get
            {
                var result = string.Empty;
                if (UserNVL.GetUserNVL().ContainsKey(OperatorID))
                    result = UserNVL.GetUserNVL().GetItemByKey(OperatorID).Value;
                return result;
            }
            set
            {
                if (UserNVL.GetUserNVL().ContainsValue(value))
                {
                    var result = UserNVL.GetUserNVL().GetItemByValue(value).Key;
                    if (result != OperatorID)
                        OperatorID = result;
                }
            }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="LbItems"/> property.
        /// </summary>
        public static readonly PropertyInfo<LbItemList> LbItemsProperty = RegisterProperty<LbItemList>(p => p.LbItems, "Lb Items", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Lb Items ("parent load" child property).
        /// </summary>
        /// <value>The Lb Items.</value>
        public LbItemList LbItems
        {
            get { return GetProperty(LbItemsProperty); }
            private set { LoadProperty(LbItemsProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about child <see cref="BwSzs"/> property.
        /// </summary>
        public static readonly PropertyInfo<BwSzList> BwSzsProperty = RegisterProperty<BwSzList>(p => p.BwSzs, "Bw Szs", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Bw Szs ("parent load" child property).
        /// </summary>
        /// <value>The Bw Szs.</value>
        public BwSzList BwSzs
        {
            get { return GetProperty(BwSzsProperty); }
            private set { LoadProperty(BwSzsProperty, value); }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessLb"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="BusinessLb"/> object.</returns>
        public static BusinessLb NewBusinessLb()
        {
            return DataPortal.Create<BusinessLb>();
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessLb"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessLb to create.</param>
        /// <returns>A reference to the created <see cref="BusinessLb"/> object.</returns>
        public static BusinessLb NewBusinessLb(Guid mxID)
        {
            return DataPortal.Create<BusinessLb>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessLb"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessLb to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessLb"/> object.</returns>
        public static BusinessLb GetBusinessLbByMxID(Guid mxID)
        {
            return DataPortal.Fetch<BusinessLb>(mxID);
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="BusinessLb"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID of the BusinessLb to delete.</param>
        public static void DeleteBusinessLb(Guid businessID)
        {
            DataPortal.Delete<BusinessLb>(businessID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessLb"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessLb(EventHandler<DataPortalResult<BusinessLb>> callback)
        {
            DataPortal.BeginCreate<BusinessLb>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessLb"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessLb to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessLb(Guid mxID, EventHandler<DataPortalResult<BusinessLb>> callback)
        {
            DataPortal.BeginCreate<BusinessLb>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessLb"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessLb to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessLbByMxID(Guid mxID, EventHandler<DataPortalResult<BusinessLb>> callback)
        {
            DataPortal.BeginFetch<BusinessLb>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="BusinessLb"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID of the BusinessLb to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteBusinessLb(Guid businessID, EventHandler<DataPortalResult<BusinessLb>> callback)
        {
            DataPortal.BeginDelete<BusinessLb>(businessID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLb"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessLb()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="BusinessLb"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(BusinessIDProperty, Guid.NewGuid());
            LoadProperty(KzSjProperty, null);
            LoadProperty(SgSjProperty, null);
            LoadProperty(KzgProperty, null);
            LoadProperty(SgyProperty, null);
            LoadProperty(LbItemsProperty, DataPortal.CreateChild<LbItemList>());
            LoadProperty(BwSzsProperty, DataPortal.CreateChild<BwSzList>());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads default values for the <see cref="BusinessLb"/> object properties, based on given criteria.
        /// </summary>
        /// <param name="mxID">The create criteria.</param>
        [Csla.RunLocal]
        protected void DataPortal_Create(Guid mxID)
        {
            LoadProperty(BusinessIDProperty, Guid.NewGuid());
            LoadProperty(KzSjProperty, null);
            LoadProperty(SgSjProperty, null);
            LoadProperty(KzgProperty, null);
            LoadProperty(SgyProperty, null);
            LoadProperty(MxIDProperty, mxID);
            LoadProperty(LbItemsProperty, DataPortal.CreateChild<LbItemList>());
            LoadProperty(BwSzsProperty, DataPortal.CreateChild<BwSzList>());
            var args = new DataPortalHookArgs(mxID);
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="BusinessLb"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessLbByMxID", ctx.Connection))
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
                    FetchChildren(dr);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="BusinessLb"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(BusinessNameProperty, dr.GetString("BusinessName"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(OperatorIDProperty, dr.GetGuid("OperatorID"));
            LoadProperty(DraweeProperty, dr.GetString("Drawee"));
            LoadProperty(PriceProperty, dr.GetDecimal("Price"));
            LoadProperty(PayFlagProperty, dr.GetBoolean("PayFlag"));
            LoadProperty(OperateTimeProperty, dr.GetSmartDate("OperateTime", true));
            LoadProperty(LbrTextProperty, dr.GetString("LbrText"));
            LoadProperty(LbsjTextProperty, dr.GetString("LbsjText"));
            LoadProperty(BxProperty, dr.GetString("Bx"));
            LoadProperty(LbDateProperty, dr.GetSmartDate("LbDate", true));
            LoadProperty(KzSjProperty, !dr.IsDBNull("KzSj") ? dr.GetSmartDate("KzSj", true) : null);
            LoadProperty(SgSjProperty, !dr.IsDBNull("SgSj") ? dr.GetSmartDate("SgSj", true) : null);
            LoadProperty(BwProperty, dr.GetValue("Bw") as byte[]);
            LoadProperty(KzgProperty, !dr.IsDBNull("Kzg") ? dr.GetString("Kzg") : null);
            LoadProperty(SgyProperty, !dr.IsDBNull("Sgy") ? dr.GetString("Sgy") : null);
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Loads child objects from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void FetchChildren(SafeDataReader dr)
        {
            dr.NextResult();
            LoadProperty(LbItemsProperty, DataPortal.FetchChild<LbItemList>(dr));
            dr.NextResult();
            LoadProperty(BwSzsProperty, DataPortal.FetchChild<BwSzList>(dr));
        }

        /// <summary>
        /// Inserts a new <see cref="BusinessLb"/> object in the database.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddBusinessLb", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@LbrText", ReadProperty(LbrTextProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@LbsjText", ReadProperty(LbsjTextProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Bx", ReadProperty(BxProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@LbDate", ReadProperty(LbDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@KzSj", ReadProperty(KzSjProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@SgSj", ReadProperty(SgSjProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Bw", ReadProperty(BwProperty)).DbType = DbType.Binary;
                    cmd.Parameters.AddWithValue("@Kzg", ReadProperty(KzgProperty) == null ? (object)DBNull.Value : ReadProperty(KzgProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Sgy", ReadProperty(SgyProperty) == null ? (object)DBNull.Value : ReadProperty(SgyProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                ctx.Commit();
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BusinessLb"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateBusinessLb", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@LbrText", ReadProperty(LbrTextProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@LbsjText", ReadProperty(LbsjTextProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Bx", ReadProperty(BxProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@LbDate", ReadProperty(LbDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@KzSj", ReadProperty(KzSjProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@SgSj", ReadProperty(SgSjProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Bw", ReadProperty(BwProperty)).DbType = DbType.Binary;
                    cmd.Parameters.AddWithValue("@Kzg", ReadProperty(KzgProperty) == null ? (object)DBNull.Value : ReadProperty(KzgProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Sgy", ReadProperty(SgyProperty) == null ? (object)DBNull.Value : ReadProperty(SgyProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                ctx.Commit();
            }
        }

        /// <summary>
        /// Self deletes the <see cref="BusinessLb"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(BusinessID);
        }

        /// <summary>
        /// Deletes the <see cref="BusinessLb"/> object from database.
        /// </summary>
        /// <param name="businessID">The delete criteria.</param>
        protected void DataPortal_Delete(Guid businessID)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                using (var cmd = new SqlCommand("dbo.DeleteBusinessLb", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", businessID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, businessID);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
                ctx.Commit();
            }
            // removes all previous references to children
            LoadProperty(LbItemsProperty, DataPortal.CreateChild<LbItemList>());
            LoadProperty(BwSzsProperty, DataPortal.CreateChild<BwSzList>());
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
