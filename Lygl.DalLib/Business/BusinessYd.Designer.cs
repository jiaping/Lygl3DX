using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Csla.Rules.CommonRules;
using Lygl.DalLib.NVL;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BusinessYd (editable root object).<br/>
    /// This is a generated base class of <see cref="BusinessYd"/> business object.
    /// </summary>
    [Serializable]
    public partial class BusinessYd : BusinessBase<BusinessYd>, IBusinessHasPayFlag
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
        /// Maintains metadata about <see cref="DownPayment"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> DownPaymentProperty = RegisterProperty<Decimal>(p => p.DownPayment, "Down Payment");
        /// <summary>
        /// Gets or sets the Down Payment.
        /// </summary>
        /// <value>The Down Payment.</value>
        public Decimal DownPayment
        {
            get { return GetProperty(DownPaymentProperty); }
            set { SetProperty(DownPaymentProperty, value); }
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
        /// Maintains metadata about <see cref="YDDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> YDDateProperty = RegisterProperty<SmartDate>(p => p.YDDate, "YDDate");
        /// <summary>
        /// Gets or sets the YDDate.
        /// </summary>
        /// <value>The YDDate.</value>
        public SmartDate YDDate
        {
            get { return GetProperty(YDDateProperty); }
            set { SetProperty(YDDateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Syz"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SyzProperty = RegisterProperty<string>(p => p.Syz, "Syz");
        /// <summary>
        /// Gets or sets the Syz.
        /// </summary>
        /// <value>The Syz.</value>
        public string Syz
        {
            get { return GetProperty(SyzProperty); }
            set { SetProperty(SyzProperty, value); }
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

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessYd"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="BusinessYd"/> object.</returns>
        public static BusinessYd NewBusinessYd()
        {
            return DataPortal.Create<BusinessYd>();
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessYd to create.</param>
        /// <param name="mxIDFlag">The MxIDFlag of the BusinessYd to create.</param>
        /// <returns>A reference to the created <see cref="BusinessYd"/> object.</returns>
        public static BusinessYd NewBusinessYd(Guid mxID, Guid mxIDFlag)
        {
            return DataPortal.Create<BusinessYd>(new CriteriaGetByMxID(mxID, mxIDFlag));
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID parameter of the BusinessYd to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessYd"/> object.</returns>
        public static BusinessYd GetBusinessYd(Guid businessID)
        {
            return DataPortal.Fetch<BusinessYd>(businessID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessYd to fetch.</param>
        /// <param name="mxIDFlag">The MxIDFlag parameter of the BusinessYd to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessYd"/> object.</returns>
        public static BusinessYd GetBusinessYdByMxID(Guid mxID, Guid mxIDFlag)
        {
            return DataPortal.Fetch<BusinessYd>(new CriteriaGetByMxID(mxID, mxIDFlag));
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID of the BusinessYd to delete.</param>
        public static void DeleteBusinessYd(Guid businessID)
        {
            DataPortal.Delete<BusinessYd>(businessID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessYd"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessYd(EventHandler<DataPortalResult<BusinessYd>> callback)
        {
            DataPortal.BeginCreate<BusinessYd>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessYd to create.</param>
        /// <param name="mxIDFlag">The MxIDFlag of the BusinessYd to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessYd(Guid mxID, Guid mxIDFlag, EventHandler<DataPortalResult<BusinessYd>> callback)
        {
            DataPortal.BeginCreate<BusinessYd>(new CriteriaGetByMxID(mxID, mxIDFlag), callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID parameter of the BusinessYd to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessYd(Guid businessID, EventHandler<DataPortalResult<BusinessYd>> callback)
        {
            DataPortal.BeginFetch<BusinessYd>(businessID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessYd to fetch.</param>
        /// <param name="mxIDFlag">The MxIDFlag parameter of the BusinessYd to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessYdByMxID(Guid mxID, Guid mxIDFlag, EventHandler<DataPortalResult<BusinessYd>> callback)
        {
            DataPortal.BeginFetch<BusinessYd>(new CriteriaGetByMxID(mxID, mxIDFlag), callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="BusinessYd"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID of the BusinessYd to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteBusinessYd(Guid businessID, EventHandler<DataPortalResult<BusinessYd>> callback)
        {
            DataPortal.BeginDelete<BusinessYd>(businessID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessYd"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessYd()
        {
            // Prevent direct creation
        }

        #endregion

        #region Criteria

        /// <summary>
        /// CriteriaGetByMxID criteria.
        /// </summary>
        [Serializable]
        protected class CriteriaGetByMxID : CriteriaBase<CriteriaGetByMxID>
        {

            /// <summary>
            /// Maintains metadata about <see cref="MxID"/> property.
            /// </summary>
            public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID);
            /// <summary>
            /// Gets or sets the Mx ID.
            /// </summary>
            /// <value>The Mx ID.</value>
            public Guid MxID
            {
                get { return ReadProperty(MxIDProperty); }
                set { LoadProperty(MxIDProperty, value); }
            }

            /// <summary>
            /// Maintains metadata about <see cref="MxIDFlag"/> property.
            /// </summary>
            public static readonly PropertyInfo<Guid> MxIDFlagProperty = RegisterProperty<Guid>(p => p.MxIDFlag);
            /// <summary>
            /// Gets or sets the Mx IDFlag.
            /// </summary>
            /// <value>The Mx IDFlag.</value>
            public Guid MxIDFlag
            {
                get { return ReadProperty(MxIDFlagProperty); }
                set { LoadProperty(MxIDFlagProperty, value); }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CriteriaGetByMxID"/> class.
            /// </summary>
            /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            public CriteriaGetByMxID()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CriteriaGetByMxID"/> class.
            /// </summary>
            /// <param name="mxID">The MxID.</param>
            /// <param name="mxIDFlag">The MxIDFlag.</param>
            public CriteriaGetByMxID(Guid mxID, Guid mxIDFlag)
            {
                MxID = mxID;
                MxIDFlag = mxIDFlag;
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
            public override bool Equals(object obj)
            {
                if (obj is CriteriaGetByMxID)
                {
                    var c = (CriteriaGetByMxID) obj;
                    if (!MxID.Equals(c.MxID))
                        return false;
                    if (!MxIDFlag.Equals(c.MxIDFlag))
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
                return string.Concat("CriteriaGetByMxID", MxID.ToString(), MxIDFlag.ToString()).GetHashCode();
            }
        }

        #endregion

        #region Business Rules and Property Authorization

        /// <summary>
        /// Override this method in your business class to be notified when you need to set up shared business rules.
        /// </summary>
        /// <remarks>
        /// This method is automatically called by CSLA.NET when your object should associate
        /// per-type validation rules with its properties.
        /// </remarks>
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // Property Business Rules

            // DownPayment
            BusinessRules.AddRule(new MinValue<Decimal>(DownPaymentProperty, 1));
            // Price
            BusinessRules.AddRule(new MinValue<Decimal>(PriceProperty, 1));
            // Drawee
            BusinessRules.AddRule(new Required(DraweeProperty));

            AddBusinessRulesExtend();
        }

        /// <summary>
        /// Allows the set up of custom shared business rules.
        /// </summary>
        partial void AddBusinessRulesExtend();

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="BusinessYd"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(BusinessIDProperty, Guid.NewGuid());
            LoadProperty(SyzProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads default values for the <see cref="BusinessYd"/> object properties, based on given criteria.
        /// </summary>
        /// <param name="crit">The create criteria.</param>
        [Csla.RunLocal]
        protected void DataPortal_Create(CriteriaGetByMxID crit)
        {
            LoadProperty(BusinessIDProperty, Guid.NewGuid());
            LoadProperty(SyzProperty, null);
            LoadProperty(MxIDProperty, crit.MxID);
            var args = new DataPortalHookArgs(crit);
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="BusinessYd"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="businessID">The Business ID.</param>
        protected void DataPortal_Fetch(Guid businessID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessYd", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", businessID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, businessID);
                    OnFetchPre(args);
                    Fetch(cmd);
                    OnFetchPost(args);
                }
            }
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Loads a <see cref="BusinessYd"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetByMxID crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessYdByMxID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", crit.MxID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.MxID).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@MxIDFlag", crit.MxIDFlag.Equals(Guid.Empty) ? (object)DBNull.Value : crit.MxIDFlag).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, crit);
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
        /// Loads a <see cref="BusinessYd"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(BusinessNameProperty, dr.GetString("BusinessName"));
            LoadProperty(DownPaymentProperty, dr.GetDecimal("DownPayment"));
            LoadProperty(PriceProperty, dr.GetDecimal("Price"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(DraweeProperty, dr.GetString("Drawee"));
            LoadProperty(OperatorIDProperty, dr.GetGuid("OperatorID"));
            LoadProperty(PayFlagProperty, dr.GetBoolean("PayFlag"));
            LoadProperty(OperateTimeProperty, dr.GetSmartDate("OperateTime", true));
            LoadProperty(YDDateProperty, !dr.IsDBNull("YDDate") ? dr.GetSmartDate("YDDate", true) : null);
            LoadProperty(SyzProperty, !dr.IsDBNull("Syz") ? dr.GetString("Syz") : null);
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="BusinessYd"/> object in the database.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddBusinessYd", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@DownPayment", ReadProperty(DownPaymentProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@YDDate", ReadProperty(YDDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Syz", ReadProperty(SyzProperty) == null ? (object)DBNull.Value : ReadProperty(SyzProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BusinessYd"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateBusinessYd", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@DownPayment", ReadProperty(DownPaymentProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@YDDate", ReadProperty(YDDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Syz", ReadProperty(SyzProperty) == null ? (object)DBNull.Value : ReadProperty(SyzProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Self deletes the <see cref="BusinessYd"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(BusinessID);
        }

        /// <summary>
        /// Deletes the <see cref="BusinessYd"/> object from database.
        /// </summary>
        /// <param name="businessID">The delete criteria.</param>
        protected void DataPortal_Delete(Guid businessID)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteBusinessYd", ctx.Connection))
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
