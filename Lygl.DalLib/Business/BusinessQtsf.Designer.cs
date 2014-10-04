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
    /// BusinessQtsf (editable child object).<br/>
    /// This is a generated base class of <see cref="BusinessQtsf"/> business object.
    /// </summary>
    /// <remarks>
    /// This class contains one child collection:<br/>
    /// - <see cref="QtsfItems"/> of type <see cref="QtsfItemList"/> (1:M relation to <see cref="QtsfItem"/>)<br/>
    /// This class is an item of <see cref="BusinessQtsfList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class BusinessQtsf : BusinessBase<BusinessQtsf>
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
        /// Maintains metadata about <see cref="OperateTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> OperateTimeProperty = RegisterProperty<SmartDate>(p => p.OperateTime, "Operate Time");
        /// <summary>
        /// Gets or sets the Operate Time.
        /// </summary>
        /// <value>The Operate Time.</value>
        public string OperateTime
        {
            get { return GetPropertyConvert<SmartDate, String>(OperateTimeProperty); }
            set { SetPropertyConvert<SmartDate, String>(OperateTimeProperty, value); }
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
        /// Maintains metadata about child <see cref="QtsfItems"/> property.
        /// </summary>
        public static readonly PropertyInfo<QtsfItemList> QtsfItemsProperty = RegisterProperty<QtsfItemList>(p => p.QtsfItems, "Qtsf Items", RelationshipTypes.Child);
        /// <summary>
        /// Gets the Qtsf Items ("self load" child property).
        /// </summary>
        /// <value>The Qtsf Items.</value>
        public QtsfItemList QtsfItems
        {
            get { return GetProperty(QtsfItemsProperty); }
            set { SetProperty(QtsfItemsProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessQtsf"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessQtsf()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="BusinessQtsf"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(BusinessIDProperty, Guid.NewGuid());
            LoadProperty(QtsfItemsProperty, DataPortal.CreateChild<QtsfItemList>());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="BusinessQtsf"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(BusinessNameProperty, dr.GetString("BusinessName"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(PriceProperty, dr.GetDecimal("Price"));
            LoadProperty(OperateTimeProperty, dr.GetSmartDate("OperateTime", true));
            LoadProperty(DraweeProperty, dr.GetString("Drawee"));
            LoadProperty(OperatorIDProperty, dr.GetGuid("OperatorID"));
            LoadProperty(PayFlagProperty, dr.GetBoolean("PayFlag"));
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
            LoadProperty(QtsfItemsProperty, DataPortal.FetchChild<QtsfItemList>(BusinessID));
        }

        /// <summary>
        /// Inserts a new <see cref="BusinessQtsf"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddBusinessQtsf", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
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
        /// Updates in the database all changes made to the <see cref="BusinessQtsf"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateBusinessQtsf", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
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
        /// Self deletes the <see cref="BusinessQtsf"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                // flushes all pending data operations
                FieldManager.UpdateChildren(this);
                using (var cmd = new SqlCommand("dbo.DeleteBusinessQtsf", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
            }
            // removes all previous references to children
            LoadProperty(QtsfItemsProperty, DataPortal.CreateChild<QtsfItemList>());
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
