using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Invoice
{

    /// <summary>
    /// InvoiceItemEditList (editable root list).<br/>
    /// This is a generated base class of <see cref="InvoiceItemEditList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="InvoiceItemEdit"/> objects.
    /// </remarks>
    [Serializable]
    public partial class InvoiceItemEditList : BusinessListBase<InvoiceItemEditList, InvoiceItemEdit>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="InvoiceItemEdit"/> item from the collection.
        /// </summary>
        /// <param name="invoiceItemID">The InvoiceItemID of the item to be removed.</param>
        public void Remove(Guid invoiceItemID)
        {
            foreach (var invoiceItemEdit in this)
            {
                if (invoiceItemEdit.InvoiceItemID == invoiceItemID)
                {
                    Remove(invoiceItemEdit);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="InvoiceItemEdit"/> item is in the collection.
        /// </summary>
        /// <param name="invoiceItemID">The InvoiceItemID of the item to search for.</param>
        /// <returns><c>true</c> if the InvoiceItemEdit is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid invoiceItemID)
        {
            foreach (var invoiceItemEdit in this)
            {
                if (invoiceItemEdit.InvoiceItemID == invoiceItemID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="InvoiceItemEdit"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="invoiceItemID">The InvoiceItemID of the item to search for.</param>
        /// <returns><c>true</c> if the InvoiceItemEdit is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid invoiceItemID)
        {
            foreach (var invoiceItemEdit in this.DeletedList)
            {
                if (invoiceItemEdit.InvoiceItemID == invoiceItemID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Find Methods

        /// <summary>
        /// Finds a <see cref="InvoiceItemEdit"/> item of the <see cref="InvoiceItemEditList"/> collection, based on a given BusinessID.
        /// </summary>
        /// <param name="businessID">The BusinessID.</param>
        /// <returns>A <see cref="InvoiceItemEdit"/> object.</returns>
        public InvoiceItemEdit FindInvoiceItemEditByBusinessID(Guid businessID)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].BusinessID.Equals(businessID))
                {
                    return this[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="InvoiceItemEdit"/> item of the <see cref="InvoiceItemEditList"/> collection, based on a given InvoiceID.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID.</param>
        /// <returns>A <see cref="InvoiceItemEdit"/> object.</returns>
        public InvoiceItemEdit FindInvoiceItemEditByInvoiceID(Guid? invoiceID)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].InvoiceID.Equals(invoiceID))
                {
                    return this[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="InvoiceItemEdit"/> item of the <see cref="InvoiceItemEditList"/> collection, based on a given ItemTypeID.
        /// </summary>
        /// <param name="itemTypeID">The ItemTypeID.</param>
        /// <returns>A <see cref="InvoiceItemEdit"/> object.</returns>
        public InvoiceItemEdit FindInvoiceItemEditByItemTypeID(int itemTypeID)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].ItemTypeID.Equals(itemTypeID))
                {
                    return this[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="InvoiceItemEdit"/> item of the <see cref="InvoiceItemEditList"/> collection, based on a given PayFlag.
        /// </summary>
        /// <param name="payFlag">The PayFlag.</param>
        /// <returns>A <see cref="InvoiceItemEdit"/> object.</returns>
        public InvoiceItemEdit FindInvoiceItemEditByPayFlag(bool payFlag)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].PayFlag.Equals(payFlag))
                {
                    return this[i];
                }
            }

            return null;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the InvoiceItemEditList to create.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(Guid mxID)
        {
            return DataPortal.Create<InvoiceItemEditList>(mxID);
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The create criteria.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(CriteriaGetByMxInvoicetypeBusinessID crit)
        {
            return DataPortal.Create<InvoiceItemEditList>(crit);
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the InvoiceItemEditList to create.</param>
        /// <param name="itemTypeID">The ItemTypeID of the InvoiceItemEditList to create.</param>
        /// <param name="businessID">The BusinessID of the InvoiceItemEditList to create.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(Guid mxID, int itemTypeID, Guid businessID)
        {
            return DataPortal.Create<InvoiceItemEditList>(new CriteriaGetByMxInvoicetypeBusinessID(mxID, itemTypeID, businessID));
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The create criteria.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(CriteriaGetNotPay crit)
        {
            return DataPortal.Create<InvoiceItemEditList>(crit);
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the InvoiceItemEditList to create.</param>
        /// <param name="payFlag">The PayFlag of the InvoiceItemEditList to create.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(Guid mxID, bool payFlag)
        {
            return DataPortal.Create<InvoiceItemEditList>(new CriteriaGetNotPay(mxID, payFlag));
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The create criteria.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(CriteriaGetByInvoiceID crit)
        {
            return DataPortal.Create<InvoiceItemEditList>(crit);
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID of the InvoiceItemEditList to create.</param>
        /// <param name="mxID">The MxID of the InvoiceItemEditList to create.</param>
        /// <returns>A reference to the created <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList NewInvoiceItemEditList(Guid invoiceID, Guid mxID)
        {
            return DataPortal.Create<InvoiceItemEditList>(new CriteriaGetByInvoiceID(invoiceID, mxID));
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the InvoiceItemEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListByMxID(Guid mxID)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListByMxInvoiceTypeBusinessID(CriteriaGetByMxInvoicetypeBusinessID crit)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(crit);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the InvoiceItemEditList to fetch.</param>
        /// <param name="itemTypeID">The ItemTypeID parameter of the InvoiceItemEditList to fetch.</param>
        /// <param name="businessID">The BusinessID parameter of the InvoiceItemEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListByMxInvoiceTypeBusinessID(Guid mxID, int itemTypeID, Guid businessID)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(new CriteriaGetByMxInvoicetypeBusinessID(mxID, itemTypeID, businessID));
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListbyPayFlag(CriteriaGetNotPay crit)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(crit);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the InvoiceItemEditList to fetch.</param>
        /// <param name="payFlag">The PayFlag parameter of the InvoiceItemEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListbyPayFlag(Guid mxID, bool payFlag)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(new CriteriaGetNotPay(mxID, payFlag));
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListByMxInvoice(CriteriaGetByInvoiceID crit)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(crit);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceItemEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID parameter of the InvoiceItemEditList to fetch.</param>
        /// <param name="mxID">The MxID parameter of the InvoiceItemEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceItemEditList"/> collection.</returns>
        public static InvoiceItemEditList GetInvoiceItemEditListByMxInvoice(Guid invoiceID, Guid mxID)
        {
            return DataPortal.Fetch<InvoiceItemEditList>(new CriteriaGetByInvoiceID(invoiceID, mxID));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceItemEditList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private InvoiceItemEditList()
        {
            // Prevent direct creation

            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            AllowNew = true;
            AllowEdit = true;
            AllowRemove = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="InvoiceItemEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceItemEditList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", mxID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, mxID);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="InvoiceItemEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetByMxInvoicetypeBusinessID crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceItemEditListByMxInvoiceTypeBusinessID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", crit.MxID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.MxID).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@ItemTypeID", crit.ItemTypeID).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@BusinessID", crit.BusinessID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.BusinessID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, crit);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="InvoiceItemEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetNotPay crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceItemEditListbyPayFlag", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", crit.MxID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.MxID).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@PayFlag", crit.PayFlag).DbType = DbType.Boolean;
                    var args = new DataPortalHookArgs(cmd, crit);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="InvoiceItemEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetByInvoiceID crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceItemEditListByMxInvoice", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", crit.InvoiceID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.InvoiceID).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@MxID", crit.MxID.Equals(Guid.Empty) ? (object)DBNull.Value : crit.MxID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, crit);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        private void LoadCollection(SqlCommand cmd)
        {
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                Fetch(dr);
            }
        }

        /// <summary>
        /// Loads all <see cref="InvoiceItemEditList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<InvoiceItemEdit>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="InvoiceItemEditList"/> object.
        /// </summary>
        protected override void DataPortal_Update()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                base.Child_Update();
                ctx.Commit();
            }
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

        #endregion

    }

    #region Criteria

    /// <summary>
    /// CriteriaGetByMxInvoicetypeBusinessID criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetByMxInvoicetypeBusinessID : CriteriaBase<CriteriaGetByMxInvoicetypeBusinessID>
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
        /// Maintains metadata about <see cref="ItemTypeID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> ItemTypeIDProperty = RegisterProperty<int>(p => p.ItemTypeID);
        /// <summary>
        /// Gets or sets the Item Type ID.
        /// </summary>
        /// <value>The Item Type ID.</value>
        public int ItemTypeID
        {
            get { return ReadProperty(ItemTypeIDProperty); }
            set { LoadProperty(ItemTypeIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="BusinessID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> BusinessIDProperty = RegisterProperty<Guid>(p => p.BusinessID);
        /// <summary>
        /// Gets or sets the Business ID.
        /// </summary>
        /// <value>The Business ID.</value>
        public Guid BusinessID
        {
            get { return ReadProperty(BusinessIDProperty); }
            set { LoadProperty(BusinessIDProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByMxInvoicetypeBusinessID"/> class.
        /// </summary>
        /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CriteriaGetByMxInvoicetypeBusinessID()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByMxInvoicetypeBusinessID"/> class.
        /// </summary>
        /// <param name="mxID">The MxID.</param>
        /// <param name="itemTypeID">The ItemTypeID.</param>
        /// <param name="businessID">The BusinessID.</param>
        public CriteriaGetByMxInvoicetypeBusinessID(Guid mxID, int itemTypeID, Guid businessID)
        {
            MxID = mxID;
            ItemTypeID = itemTypeID;
            BusinessID = businessID;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaGetByMxInvoicetypeBusinessID)
            {
                var c = (CriteriaGetByMxInvoicetypeBusinessID) obj;
                if (!MxID.Equals(c.MxID))
                    return false;
                if (!ItemTypeID.Equals(c.ItemTypeID))
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
            return string.Concat("CriteriaGetByMxInvoicetypeBusinessID", MxID.ToString(), ItemTypeID.ToString(), BusinessID.ToString()).GetHashCode();
        }
    }

    /// <summary>
    /// CriteriaGetNotPay criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetNotPay : CriteriaBase<CriteriaGetNotPay>
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
        /// Maintains metadata about <see cref="PayFlag"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> PayFlagProperty = RegisterProperty<bool>(p => p.PayFlag);
        /// <summary>
        /// Gets the Pay Flag.
        /// </summary>
        /// <value><c>true</c> if Pay Flag; otherwise, <c>false</c>.</value>
        public bool PayFlag
        {
            get { return ReadProperty(PayFlagProperty); }
            private set { LoadProperty(PayFlagProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetNotPay"/> class.
        /// </summary>
        /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CriteriaGetNotPay()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetNotPay"/> class.
        /// </summary>
        /// <param name="mxID">The MxID.</param>
        /// <param name="payFlag">The PayFlag.</param>
        public CriteriaGetNotPay(Guid mxID, bool payFlag)
        {
            MxID = mxID;
            PayFlag = payFlag;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaGetNotPay)
            {
                var c = (CriteriaGetNotPay) obj;
                if (!MxID.Equals(c.MxID))
                    return false;
                if (!PayFlag.Equals(c.PayFlag))
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
            return string.Concat("CriteriaGetNotPay", MxID.ToString(), PayFlag.ToString()).GetHashCode();
        }
    }

    /// <summary>
    /// CriteriaGetByInvoiceID criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetByInvoiceID : CriteriaBase<CriteriaGetByInvoiceID>
    {

        /// <summary>
        /// Maintains metadata about <see cref="InvoiceID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> InvoiceIDProperty = RegisterProperty<Guid>(p => p.InvoiceID);
        /// <summary>
        /// Gets the Invoice ID.
        /// </summary>
        /// <value>The Invoice ID.</value>
        public Guid InvoiceID
        {
            get { return ReadProperty(InvoiceIDProperty); }
            private set { LoadProperty(InvoiceIDProperty, value); }
        }

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
        /// Initializes a new instance of the <see cref="CriteriaGetByInvoiceID"/> class.
        /// </summary>
        /// <remarks> A parameterless constructor is required by the MobileFormatter.</remarks>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public CriteriaGetByInvoiceID()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByInvoiceID"/> class.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID.</param>
        /// <param name="mxID">The MxID.</param>
        public CriteriaGetByInvoiceID(Guid invoiceID, Guid mxID)
        {
            InvoiceID = invoiceID;
            MxID = mxID;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaGetByInvoiceID)
            {
                var c = (CriteriaGetByInvoiceID) obj;
                if (!InvoiceID.Equals(c.InvoiceID))
                    return false;
                if (!MxID.Equals(c.MxID))
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
            return string.Concat("CriteriaGetByInvoiceID", InvoiceID.ToString(), MxID.ToString()).GetHashCode();
        }
    }

    #endregion

}
