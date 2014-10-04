using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Csla.Rules;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// QtsfItem (editable child object).<br/>
    /// This is a generated base class of <see cref="QtsfItem"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="QtsfItemList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class QtsfItem : BusinessBase<QtsfItem>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="QtsfItemID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> QtsfItemIDProperty = RegisterProperty<Guid>(p => p.QtsfItemID, "Qtsf Item ID");
        /// <summary>
        /// Gets or sets the Qtsf Item ID.
        /// </summary>
        /// <value>The Qtsf Item ID.</value>
        public Guid QtsfItemID
        {
            get { return GetProperty(QtsfItemIDProperty); }
            set { SetProperty(QtsfItemIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="UnitPrice"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> UnitPriceProperty = RegisterProperty<Decimal>(p => p.UnitPrice, "Unit Price");
        /// <summary>
        /// Gets or sets the Unit Price.
        /// </summary>
        /// <value>The Unit Price.</value>
        public Decimal UnitPrice
        {
            get { return GetProperty(UnitPriceProperty); }
            set { SetProperty(UnitPriceProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Unit"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> UnitProperty = RegisterProperty<string>(p => p.Unit, "Unit");
        /// <summary>
        /// Gets or sets the Unit.
        /// </summary>
        /// <value>The Unit.</value>
        public string Unit
        {
            get { return GetProperty(UnitProperty); }
            set { SetProperty(UnitProperty, value); }
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
        /// Maintains metadata about <see cref="SubTotal"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> SubTotalProperty = RegisterProperty<Decimal>(p => p.SubTotal, "Sub Total");
        /// <summary>
        /// Gets or sets the Sub Total.
        /// </summary>
        /// <value>The Sub Total.</value>
        public Decimal SubTotal
        {
            get { return GetProperty(SubTotalProperty); }
            set { SetProperty(SubTotalProperty, value); }
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

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QtsfItem"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private QtsfItem()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
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

            // SubTotal
            BusinessRules.AddRule(new Lygl.DalLib.CustomRule.CalcTotal(SubTotalProperty, QuantityProperty, UnitPriceProperty) { Severity = RuleSeverity.Information });

            AddBusinessRulesExtend();
        }

        /// <summary>
        /// Allows the set up of custom shared business rules.
        /// </summary>
        partial void AddBusinessRulesExtend();

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="QtsfItem"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(QtsfItemIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="QtsfItem"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(QtsfItemIDProperty, dr.GetGuid("QtsfItemID"));
            LoadProperty(UnitPriceProperty, dr.GetDecimal("UnitPrice"));
            LoadProperty(UnitProperty, dr.GetString("Unit"));
            LoadProperty(QuantityProperty, dr.GetInt32("Quantity"));
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(SubTotalProperty, dr.GetDecimal("SubTotal"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="QtsfItem"/> object in the database.
        /// </summary>
        /// <param name="parent">The parent object.</param>
        private void Child_Insert(BusinessQtsf parent)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddQtsfItem", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QtsfItemID", ReadProperty(QtsfItemIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@UnitPrice", ReadProperty(UnitPriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Unit", ReadProperty(UnitProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Quantity", ReadProperty(QuantityProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@SubTotal", ReadProperty(SubTotalProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="QtsfItem"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateQtsfItem", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QtsfItemID", ReadProperty(QtsfItemIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@UnitPrice", ReadProperty(UnitPriceProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Unit", ReadProperty(UnitProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Quantity", ReadProperty(QuantityProperty)).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@BusinessID", ReadProperty(BusinessIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@SubTotal", ReadProperty(SubTotalProperty)).DbType = DbType.Decimal;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="QtsfItem"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteQtsfItem", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@QtsfItemID", ReadProperty(QtsfItemIDProperty)).DbType = DbType.Guid;
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
