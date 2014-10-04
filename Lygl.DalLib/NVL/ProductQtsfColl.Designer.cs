using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// ProductQtsfColl (read only list).<br/>
    /// This is a generated base class of <see cref="ProductQtsfColl"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="productQtsf"/> objects.
    /// </remarks>
    [Serializable]
    public partial class ProductQtsfColl : ReadOnlyListBase<ProductQtsfColl, productQtsf>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="productQtsf"/> item is in the collection.
        /// </summary>
        /// <param name="itemID">The ItemID of the item to search for.</param>
        /// <returns><c>true</c> if the productQtsf is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(string itemID)
        {
            foreach (var productQtsf in this)
            {
                if (productQtsf.ItemID == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="ProductQtsfColl"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="ProductQtsfColl"/> collection.</returns>
        public static ProductQtsfColl GetProductQtsfColl()
        {
            return DataPortal.Fetch<ProductQtsfColl>();
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="ProductQtsfColl"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetProductQtsfColl(EventHandler<DataPortalResult<ProductQtsfColl>> callback)
        {
            DataPortal.BeginFetch<ProductQtsfColl>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQtsfColl"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private ProductQtsfColl()
        {
            // Prevent direct creation

            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            AllowNew = false;
            AllowEdit = false;
            AllowRemove = false;
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="ProductQtsfColl"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetProductQtsfColl", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
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
        /// Loads all <see cref="ProductQtsfColl"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<productQtsf>(dr));
            }
            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
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
