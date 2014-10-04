using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Lygl.DalLib.NVL;

namespace Lygl.DalLib.Invoice
{

    /// <summary>
    /// Invoice (editable child object).<br/>
    /// This is a generated base class of <see cref="Invoice"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="InvoiceList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class Invoice : BusinessBase<Invoice>
    {

        #region Business Properties

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
        /// Maintains metadata about <see cref="InvoiceAccount"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> InvoiceAccountProperty = RegisterProperty<Decimal>(p => p.InvoiceAccount, "Invoice Account");
        /// <summary>
        /// Gets or sets the Invoice Account.
        /// </summary>
        /// <value>The Invoice Account.</value>
        public Decimal InvoiceAccount
        {
            get { return GetProperty(InvoiceAccountProperty); }
            set { SetProperty(InvoiceAccountProperty, value); }
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
        /// Maintains metadata about <see cref="InvoiceTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> InvoiceTimeProperty = RegisterProperty<SmartDate>(p => p.InvoiceTime, "Invoice Time");
        /// <summary>
        /// Gets or sets the Invoice Time.
        /// </summary>
        /// <value>The Invoice Time.</value>
        public SmartDate InvoiceTime
        {
            get { return GetProperty(InvoiceTimeProperty); }
            set { SetProperty(InvoiceTimeProperty, value); }
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
        /// Maintains metadata about <see cref="InvoiceNumber"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> InvoiceNumberProperty = RegisterProperty<string>(p => p.InvoiceNumber, "Invoice Number");
        /// <summary>
        /// Gets or sets the Invoice Number.
        /// </summary>
        /// <value>The Invoice Number.</value>
        public string InvoiceNumber
        {
            get { return GetProperty(InvoiceNumberProperty); }
            set { SetProperty(InvoiceNumberProperty, value); }
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
        /// Maintains metadata about <see cref="IsPrinted"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> IsPrintedProperty = RegisterProperty<bool>(p => p.IsPrinted, "Is Printed");
        /// <summary>
        /// Gets or sets the Is Printed.
        /// </summary>
        /// <value><c>true</c> if Is Printed; otherwise, <c>false</c>.</value>
        public bool IsPrinted
        {
            get { return GetProperty(IsPrintedProperty); }
            set { SetProperty(IsPrintedProperty, value); }
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

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Invoice"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private Invoice()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="Invoice"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(InvoiceIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="Invoice"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(InvoiceIDProperty, dr.GetGuid("InvoiceID"));
            LoadProperty(InvoiceAccountProperty, dr.GetDecimal("InvoiceAccount"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(InvoiceTimeProperty, dr.GetSmartDate("InvoiceTime", true));
            LoadProperty(DraweeProperty, dr.GetString("Drawee"));
            LoadProperty(InvoiceNumberProperty, dr.GetString("InvoiceNumber"));
            LoadProperty(OperatorIDProperty, dr.GetGuid("OperatorID"));
            LoadProperty(IsPrintedProperty, dr.GetBoolean("IsPrinted"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="Invoice"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddInvoice", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", ReadProperty(InvoiceIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@InvoiceAccount", ReadProperty(InvoiceAccountProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@InvoiceTime", ReadProperty(InvoiceTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@InvoiceNumber", ReadProperty(InvoiceNumberProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@IsPrinted", ReadProperty(IsPrintedProperty)).DbType = DbType.Boolean;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="Invoice"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateInvoice", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", ReadProperty(InvoiceIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@InvoiceAccount", ReadProperty(InvoiceAccountProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@InvoiceTime", ReadProperty(InvoiceTimeProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Drawee", ReadProperty(DraweeProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@InvoiceNumber", ReadProperty(InvoiceNumberProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@IsPrinted", ReadProperty(IsPrintedProperty)).DbType = DbType.Boolean;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="Invoice"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteInvoice", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", ReadProperty(InvoiceIDProperty)).DbType = DbType.Guid;
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
