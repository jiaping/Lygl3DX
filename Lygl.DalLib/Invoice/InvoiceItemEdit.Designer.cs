using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Invoice
{

    /// <summary>
    /// InvoiceItemEdit (editable child object).<br/>
    /// This is a generated base class of <see cref="InvoiceItemEdit"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="InvoiceItemEditList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class InvoiceItemEdit : BusinessBase<InvoiceItemEdit>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="InvoiceItemID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> InvoiceItemIDProperty = RegisterProperty<Guid>(p => p.InvoiceItemID, "Invoice Item ID");
        /// <summary>
        /// Gets or sets the Invoice Item ID.
        /// </summary>
        /// <value>The Invoice Item ID.</value>
        public Guid InvoiceItemID
        {
            get { return GetProperty(InvoiceItemIDProperty); }
            set { SetProperty(InvoiceItemIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="InvoiceID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> InvoiceIDProperty = RegisterProperty<Guid>(p => p.InvoiceID, "Invoice ID");
        /// <summary>
        /// Gets or sets the Invoice ID.
        /// </summary>
        /// <value>The Invoice ID.</value>
        public Guid InvoiceID
        {
            get { return GetProperty(InvoiceIDProperty); }
            set { SetProperty(InvoiceIDProperty, value); }
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
        /// Maintains metadata about <see cref="Quantity"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> QuantityProperty = RegisterProperty<int>(p => p.Quantity, "Quantity");
        /// <summary>
        /// Gets or sets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity
        {
            get { return GetProperty(QuantityProperty); }
            set { SetProperty(QuantityProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ItemTypeID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> ItemTypeIDProperty = RegisterProperty<int>(p => p.ItemTypeID, "Item Type ID");
        /// <summary>
        /// Gets or sets the Item Type ID.
        /// </summary>
        /// <value>The Item Type ID.</value>
        public int ItemTypeID
        {
            get { return GetProperty(ItemTypeIDProperty); }
            set { SetProperty(ItemTypeIDProperty, value); }
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceItemEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private InvoiceItemEdit()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="InvoiceItemEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(InvoiceItemIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="InvoiceItemEdit"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(InvoiceItemIDProperty, dr.GetGuid("InvoiceItemID"));
            LoadProperty(InvoiceIDProperty, dr.GetGuid("InvoiceID"));
            LoadProperty(PriceProperty, dr.GetDecimal("Price"));
            LoadProperty(QuantityProperty, dr.GetInt32("Quantity"));
            LoadProperty(ItemTypeIDProperty, dr.GetInt32("ItemTypeID"));
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(PayFlagProperty, dr.GetBoolean("PayFlag"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(BusinessNameProperty, dr.GetString("BusinessName"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="InvoiceItemEdit"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddInvoiceItemEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceItemID", ReadProperty(InvoiceItemIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@InvoiceID", ReadProperty(InvoiceIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Quantity", ReadProperty(QuantityProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@ItemTypeID", ReadProperty(ItemTypeIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="InvoiceItemEdit"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateInvoiceItemEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceItemID", ReadProperty(InvoiceItemIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@InvoiceID", ReadProperty(InvoiceIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Price", ReadProperty(PriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Quantity", ReadProperty(QuantityProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@ItemTypeID", ReadProperty(ItemTypeIDProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", ReadProperty(PayFlagProperty)).DbType = DbType.Boolean;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessName", ReadProperty(BusinessNameProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="InvoiceItemEdit"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteInvoiceItemEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceItemID", ReadProperty(InvoiceItemIDProperty)).DbType = DbType.Guid;
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
