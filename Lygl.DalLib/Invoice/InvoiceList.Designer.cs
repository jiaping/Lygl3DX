using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Invoice
{

    /// <summary>
    /// InvoiceList (editable root list).<br/>
    /// This is a generated base class of <see cref="InvoiceList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="Invoice"/> objects.
    /// </remarks>
    [Serializable]
    public partial class InvoiceList : BusinessListBase<InvoiceList, Invoice>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="Invoice"/> item from the collection.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID of the item to be removed.</param>
        public void Remove(Guid invoiceID)
        {
            foreach (var invoice in this)
            {
                if (invoice.InvoiceID == invoiceID)
                {
                    Remove(invoice);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="Invoice"/> item is in the collection.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID of the item to search for.</param>
        /// <returns><c>true</c> if the Invoice is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid invoiceID)
        {
            foreach (var invoice in this)
            {
                if (invoice.InvoiceID == invoiceID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="Invoice"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID of the item to search for.</param>
        /// <returns><c>true</c> if the Invoice is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid invoiceID)
        {
            foreach (var invoice in this.DeletedList)
            {
                if (invoice.InvoiceID == invoiceID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Find Methods

        /// <summary>
        /// Finds a <see cref="Invoice"/> item of the <see cref="InvoiceList"/> collection, based on a given Drawee.
        /// </summary>
        /// <param name="drawee">The Drawee.</param>
        /// <returns>A <see cref="Invoice"/> object.</returns>
        public Invoice FindInvoiceByDrawee(string drawee)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].Drawee.Equals(drawee))
                {
                    return this[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Finds a <see cref="Invoice"/> item of the <see cref="InvoiceList"/> collection, based on a given InvoiceID.
        /// </summary>
        /// <param name="invoiceID">The InvoiceID.</param>
        /// <returns>A <see cref="Invoice"/> object.</returns>
        public Invoice FindInvoiceByInvoiceID(Guid invoiceID)
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

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="InvoiceList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the InvoiceList to create.</param>
        /// <returns>A reference to the created <see cref="InvoiceList"/> collection.</returns>
        public static InvoiceList NewInvoiceList(Guid mxID)
        {
            return DataPortal.Create<InvoiceList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="InvoiceList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the InvoiceList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="InvoiceList"/> collection.</returns>
        public static InvoiceList GetInvoiceListByMxID(Guid mxID)
        {
            return DataPortal.Fetch<InvoiceList>(mxID);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private InvoiceList()
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
        /// Loads a <see cref="InvoiceList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetInvoiceListByMxID", ctx.Connection))
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

        private void LoadCollection(SqlCommand cmd)
        {
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                Fetch(dr);
            }
        }

        /// <summary>
        /// Loads all <see cref="InvoiceList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<Invoice>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="InvoiceList"/> object.
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
}
