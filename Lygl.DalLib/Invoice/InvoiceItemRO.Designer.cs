using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Invoice
{

    /// <summary>
    /// Only used in BusinessObject get fee info (read only object).<br/>
    /// This is a generated base class of <see cref="InvoiceItemRO"/> business object.
    /// This class is a root object.
    /// </summary>
    [Serializable]
    public partial class InvoiceItemRO : ReadOnlyBase<InvoiceItemRO>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="InvoiceItemID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> InvoiceItemIDProperty = RegisterProperty<Guid>(p => p.InvoiceItemID, "Invoice Item ID", Guid.NewGuid());
        /// <summary>
        /// Gets the Invoice Item ID.
        /// </summary>
        /// <value>The Invoice Item ID.</value>
        public Guid InvoiceItemID
        {
            get { return GetProperty(InvoiceItemIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="InvoiceID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> InvoiceIDProperty = RegisterProperty<Guid>(p => p.InvoiceID, "Invoice ID");
        /// <summary>
        /// Gets the Invoice ID.
        /// </summary>
        /// <value>The Invoice ID.</value>
        public Guid InvoiceID
        {
            get { return GetProperty(InvoiceIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Price"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> PriceProperty = RegisterProperty<Decimal>(p => p.Price, "Price");
        /// <summary>
        /// Gets the Price.
        /// </summary>
        /// <value>The Price.</value>
        public Decimal Price
        {
            get { return GetProperty(PriceProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Quantity"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> QuantityProperty = RegisterProperty<int>(p => p.Quantity, "Quantity");
        /// <summary>
        /// Gets the Quantity.
        /// </summary>
        /// <value>The Quantity.</value>
        public int Quantity
        {
            get { return GetProperty(QuantityProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="BusinessID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> BusinessIDProperty = RegisterProperty<Guid>(p => p.BusinessID, "Business ID");
        /// <summary>
        /// Gets the Business ID.
        /// </summary>
        /// <value>The Business ID.</value>
        public Guid BusinessID
        {
            get { return GetProperty(BusinessIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="BusinessName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> BusinessNameProperty = RegisterProperty<string>(p => p.BusinessName, "Business Name");
        /// <summary>
        /// Gets the Business Name.
        /// </summary>
        /// <value>The Business Name.</value>
        public string BusinessName
        {
            get { return GetProperty(BusinessNameProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PayFlag"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> PayFlagProperty = RegisterProperty<bool>(p => p.PayFlag, "Pay Flag");
        /// <summary>
        /// Gets the Pay Flag.
        /// </summary>
        /// <value><c>true</c> if Pay Flag; otherwise, <c>false</c>.</value>
        public bool PayFlag
        {
            get { return GetProperty(PayFlagProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID, "Mx ID");
        /// <summary>
        /// Gets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid MxID
        {
            get { return GetProperty(MxIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ItemTypeID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> ItemTypeIDProperty = RegisterProperty<int>(p => p.ItemTypeID, "Item Type ID");
        /// <summary>
        /// Gets the Item Type ID.
        /// </summary>
        /// <value>The Item Type ID.</value>
        public int ItemTypeID
        {
            get { return GetProperty(ItemTypeIDProperty); }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemRO"/> object, based on given parameters.
        /// </summary>
        /// <param name="invoiceItemID">The InvoiceItemID parameter of the InvoiceItemRO to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemRO"/> object.</returns>
        public static InvoiceItemRO GetInvoiceItemRO(Guid invoiceItemID)
        {
            return DataPortal.Fetch<InvoiceItemRO>(invoiceItemID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemRO"/> object, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemRO"/> object.</returns>
        public static InvoiceItemRO GetInvoiceItemROByMxIDBusinessID(CriteriaGetbyMxIDBusinessID crit)
        {
            return DataPortal.Fetch<InvoiceItemRO>(crit);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemRO"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the InvoiceItemRO to fetch.</param>
        /// <param name="businessID">The BusinessID parameter of the InvoiceItemRO to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemRO"/> object.</returns>
        public static InvoiceItemRO GetInvoiceItemROByMxIDBusinessID(Guid mxID, Guid businessID)
        {
            return DataPortal.Fetch<InvoiceItemRO>(new CriteriaGetbyMxIDBusinessID(mxID, businessID));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceItemRO"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private InvoiceItemRO()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="InvoiceItemRO"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="invoiceItemID">The Invoice Item ID.</param>
        protected void DataPortal_Fetch(Guid invoiceItemID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceItemRO", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceItemID", invoiceItemID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, invoiceItemID);
                    OnFetchPre(args);
                    Fetch(cmd);
                    OnFetchPost(args);
                }
            }
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Loads a <see cref="InvoiceItemRO"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetbyMxIDBusinessID crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceItemROByMxIDBusinessID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", crit.MxID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.MxID).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BusinessID", crit.BusinessID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.BusinessID).DbType = DbType.Guid;
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
        /// Loads a <see cref="InvoiceItemRO"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(InvoiceItemIDProperty, dr.GetGuid("InvoiceItemID"));
            LoadProperty(InvoiceIDProperty, dr.GetGuid("InvoiceID"));
            LoadProperty(PriceProperty, dr.GetDecimal("Price"));
            LoadProperty(QuantityProperty, dr.GetInt32("Quantity"));
            LoadProperty(BusinessIDProperty, dr.GetGuid("BusinessID"));
            LoadProperty(BusinessNameProperty, dr.GetString("BusinessName"));
            LoadProperty(PayFlagProperty, dr.GetBoolean("PayFlag"));
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(ItemTypeIDProperty, dr.GetInt32("ItemTypeID"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
        }

        #endregion

        #region Pseudo Events

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

        #endregion

    }

    #region Criteria

    /// <summary>
    /// CriteriaGetbyMxIDBusinessID criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetbyMxIDBusinessID : CriteriaBase<CriteriaGetbyMxIDBusinessID>
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
        /// Maintains metadata about <see cref="BusinessID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> BusinessIDProperty = RegisterProperty<Guid>(p => p.BusinessID);
        /// <summary>
        /// Gets the Business ID.
        /// </summary>
        /// <value>The Business ID.</value>
        public Guid BusinessID
        {
            get { return ReadProperty(BusinessIDProperty); }
            private set { LoadProperty(BusinessIDProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetbyMxIDBusinessID"/> class.
        /// </summary>
        /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CriteriaGetbyMxIDBusinessID()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetbyMxIDBusinessID"/> class.
        /// </summary>
        /// <param name="mxID">The MxID.</param>
        /// <param name="businessID">The BusinessID.</param>
        public CriteriaGetbyMxIDBusinessID(Guid mxID, Guid businessID)
        {
            MxID = mxID;
            BusinessID = businessID;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaGetbyMxIDBusinessID)
            {
                var c = (CriteriaGetbyMxIDBusinessID) obj;
                if (!MxID.Equals(c.MxID))
                    return false;
                if (!BusinessID.Equals(c.BusinessID))
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
            return string.Concat("CriteriaGetbyMxIDBusinessID", MxID.ToString(), BusinessID.ToString()).GetHashCode();
        }
    }

    #endregion

}
