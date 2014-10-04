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
    /// BusinessGm (editable root object).<br/>
    /// This is a generated base class of <see cref="BusinessGm"/> business object.
    /// </summary>
    [Serializable]
    public partial class BusinessGm : BusinessBase<BusinessGm>, IBusinessHasPayFlag
    {

        #region Business Properties

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
        /// Maintains metadata about <see cref="PaymentPay"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> PaymentPayProperty = RegisterProperty<Decimal>(p => p.PaymentPay, "Payment Pay");
        /// <summary>
        /// Gets or sets the Payment Pay.
        /// </summary>
        /// <value>The Payment Pay.</value>
        public Decimal PaymentPay
        {
            get { return GetProperty(PaymentPayProperty); }
            set { SetProperty(PaymentPayProperty, value); }
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
        /// Maintains metadata about <see cref="GmDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> GmDateProperty = RegisterProperty<SmartDate>(p => p.GmDate, "Gm Date");
        /// <summary>
        /// Gets or sets the Gm Date.
        /// </summary>
        /// <value>The Gm Date.</value>
        public SmartDate GmDate
        {
            get { return GetProperty(GmDateProperty); }
            set { SetProperty(GmDateProperty, value); }
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
        /// Factory method. Creates a new <see cref="BusinessGm"/> object.
        /// </summary>
        /// <returns>A reference to the created <see cref="BusinessGm"/> object.</returns>
        public static BusinessGm NewBusinessGm()
        {
            return DataPortal.Create<BusinessGm>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID parameter of the BusinessGm to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessGm"/> object.</returns>
        public static BusinessGm GetBusinessGm(Guid businessID)
        {
            return DataPortal.Fetch<BusinessGm>(businessID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <returns>A reference to the fetched <see cref="BusinessGm"/> object.</returns>
        public static BusinessGm GetBusinessGmByMx(CriteriaGetByMx crit)
        {
            return DataPortal.Fetch<BusinessGm>(crit);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessGm to fetch.</param>
        /// <param name="mxIDFlag">The MxIDFlag parameter of the BusinessGm to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessGm"/> object.</returns>
        public static BusinessGm GetBusinessGmByMx(Guid mxID, Guid mxIDFlag)
        {
            return DataPortal.Fetch<BusinessGm>(new CriteriaGetByMx(mxID, mxIDFlag));
        }

        /// <summary>
        /// Factory method. Deletes a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID of the BusinessGm to delete.</param>
        public static void DeleteBusinessGm(Guid businessID)
        {
            DataPortal.Delete<BusinessGm>(businessID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessGm"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessGm(EventHandler<DataPortalResult<BusinessGm>> callback)
        {
            DataPortal.BeginCreate<BusinessGm>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID parameter of the BusinessGm to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessGm(Guid businessID, EventHandler<DataPortalResult<BusinessGm>> callback)
        {
            DataPortal.BeginFetch<BusinessGm>(businessID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessGmByMx(CriteriaGetByMx crit, EventHandler<DataPortalResult<BusinessGm>> callback)
        {
            DataPortal.BeginFetch<BusinessGm>(crit, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessGm to fetch.</param>
        /// <param name="mxIDFlag">The MxIDFlag parameter of the BusinessGm to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessGmByMx(Guid mxID, Guid mxIDFlag, EventHandler<DataPortalResult<BusinessGm>> callback)
        {
            DataPortal.BeginFetch<BusinessGm>(new CriteriaGetByMx(mxID, mxIDFlag), callback);
        }

        /// <summary>
        /// Factory method. Asynchronously deletes a <see cref="BusinessGm"/> object, based on given parameters.
        /// </summary>
        /// <param name="businessID">The BusinessID of the BusinessGm to delete.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void DeleteBusinessGm(Guid businessID, EventHandler<DataPortalResult<BusinessGm>> callback)
        {
            DataPortal.BeginDelete<BusinessGm>(businessID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessGm"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessGm()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="BusinessGm"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void DataPortal_Create()
        {
            LoadProperty(BusinessIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.DataPortal_Create();
        }

        /// <summary>
        /// Loads a <see cref="BusinessGm"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="businessID">The Business ID.</param>
        protected void DataPortal_Fetch(Guid businessID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessGm", ctx.Connection))
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
        /// Loads a <see cref="BusinessGm"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetByMx crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessGmByMx", ctx.Connection))
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
        /// Loads a <see cref="BusinessGm"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(BusinessNameProperty, dr.GetString("BusinessName"));
            LoadProperty(PaymentPayProperty, dr.GetDecimal("PaymentPay"));
            LoadProperty(PriceProperty, dr.GetDecimal("Price"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(DraweeProperty, dr.GetString("Drawee"));
            LoadProperty(OperatorIDProperty, dr.GetGuid("OperatorID"));
            LoadProperty(PayFlagProperty, dr.GetBoolean("PayFlag"));
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(OperateTimeProperty, dr.GetSmartDate("OperateTime", true));
            LoadProperty(GmDateProperty, !dr.IsDBNull("GmDate") ? dr.GetSmartDate("GmDate", true) : null);
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        /// <summary>
        /// Inserts a new <see cref="BusinessGm"/> object in the database.
        /// </summary>
        protected override void DataPortal_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddBusinessGm", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@PaymentPay", ReadProperty(PaymentPayProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@GmDate", ReadProperty(GmDateProperty).DBValue).DbType = DbType.DateTime;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BusinessGm"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateBusinessGm", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@PaymentPay", ReadProperty(PaymentPayProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@GmDate", ReadProperty(GmDateProperty).DBValue).DbType = DbType.DateTime;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
                ctx.Commit();
            }
        }

        /// <summary>
        /// Self deletes the <see cref="BusinessGm"/> object.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(BusinessID);
        }

        /// <summary>
        /// Deletes the <see cref="BusinessGm"/> object from database.
        /// </summary>
        /// <param name="businessID">The delete criteria.</param>
        protected void DataPortal_Delete(Guid businessID)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteBusinessGm", ctx.Connection))
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

    #region Criteria

    /// <summary>
    /// CriteriaGetByMx criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetByMx : CriteriaBase<CriteriaGetByMx>
    {

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID);
        /// <summary>
        /// Gets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid MxID
        {
            get { return ReadProperty(MxIDProperty); }
            private set { LoadProperty(MxIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxIDFlag"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDFlagProperty = RegisterProperty<Guid>(p => p.MxIDFlag);
        /// <summary>
        /// Gets the Mx IDFlag.
        /// </summary>
        /// <value>The Mx IDFlag.</value>
        public Guid MxIDFlag
        {
            get { return ReadProperty(MxIDFlagProperty); }
            private set { LoadProperty(MxIDFlagProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByMx"/> class.
        /// </summary>
        /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CriteriaGetByMx()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByMx"/> class.
        /// </summary>
        /// <param name="mxID">The MxID.</param>
        /// <param name="mxIDFlag">The MxIDFlag.</param>
        public CriteriaGetByMx(Guid mxID, Guid mxIDFlag)
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
            if (obj is CriteriaGetByMx)
            {
                var c = (CriteriaGetByMx) obj;
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
            return string.Concat("CriteriaGetByMx", MxID.ToString(), MxIDFlag.ToString()).GetHashCode();
        }
    }

    #endregion

}
