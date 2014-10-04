using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// BusinessTypeNVL (name value list).<br/>
    /// This is a generated base class of <see cref="BusinessTypeNVL"/> business object.
    /// </summary>
    [Serializable]
    public partial class BusinessTypeNVL : NameValueListBase<int, string>
    {

        #region Private Fields

        private static BusinessTypeNVL _list;

        #endregion

        #region Cache Management Methods

        /// <summary>
        /// Clears the in-memory BusinessTypeNVL cache so it is reloaded on the next request.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        /// <summary>
        /// Used by async loaders to load the cache.
        /// </summary>
        /// <param name="list">The list to cache.</param>
        internal static void SetCache(BusinessTypeNVL list)
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
        /// Factory method. Loads a <see cref="BusinessTypeNVL"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="BusinessTypeNVL"/> object.</returns>
        public static BusinessTypeNVL GetBusinessTypeNVL()
        {
            if (_list == null)
                _list = DataPortal.Fetch<BusinessTypeNVL>();

            return _list;
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessTypeNVL"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessTypeNVL(EventHandler<DataPortalResult<BusinessTypeNVL>> callback)
        {
            if (_list == null)
                DataPortal.BeginFetch<BusinessTypeNVL>((o, e) =>
                    {
                        _list = e.Object;
                        callback(o, e);
                    });
            else
                callback(null, new DataPortalResult<BusinessTypeNVL>(_list, null, null));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessTypeNVL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessTypeNVL()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="BusinessTypeNVL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessTypeNVL", ctx.Connection))
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
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                while (dr.Read())
                {
                    Add(new NameValuePair(
                        dr.GetInt32("BusinessTypeID"),
                        dr.GetString("BusinessTypeName")));
                }
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
