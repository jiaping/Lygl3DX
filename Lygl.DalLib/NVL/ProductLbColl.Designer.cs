using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// ProductLbColl (read only list).<br/>
    /// This is a generated base class of <see cref="ProductLbColl"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="ProductLb"/> objects.
    /// </remarks>
    [Serializable]
    public partial class ProductLbColl : ReadOnlyListBase<ProductLbColl, ProductLb>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="ProductLb"/> item is in the collection.
        /// </summary>
        /// <param name="itemID">The ItemID of the item to search for.</param>
        /// <returns><c>true</c> if the ProductLb is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(string itemID)
        {
            foreach (var productLb in this)
            {
                if (productLb.ItemID == itemID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Private Fields

        private static ProductLbColl _list;

        #endregion

        #region Cache Management Methods

        /// <summary>
        /// Clears the in-memory ProductLbColl cache so it is reloaded on the next request.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        /// <summary>
        /// Used by async loaders to load the cache.
        /// </summary>
        /// <param name="list">The list to cache.</param>
        internal static void SetCache(ProductLbColl list)
        {
            _list = list;
        }

        internal static bool IsCached
        {
            get { return _list != null; }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="ProductLbColl"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="ProductLbColl"/> collection.</returns>
        public static ProductLbColl GetProductLbColl()
        {
            if (_list == null)
                _list = DataPortal.Fetch<ProductLbColl>();

            return _list;
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="ProductLbColl"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetProductLbColl(EventHandler<DataPortalResult<ProductLbColl>> callback)
        {
            if (_list == null)
                DataPortal.BeginFetch<ProductLbColl>((o, e) =>
                    {
                        _list = e.Object;
                        callback(o, e);
                    });
            else
                callback(null, new DataPortalResult<ProductLbColl>(_list, null, null));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductLbColl"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private ProductLbColl()
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
        /// Loads a <see cref="ProductLbColl"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetProductLbColl", ctx.Connection))
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
        /// Loads all <see cref="ProductLbColl"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<ProductLb>(dr));
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
