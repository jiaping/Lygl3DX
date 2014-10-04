using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// MxTypeNVL (name value list).<br/>
    /// This is a generated base class of <see cref="MxTypeNVL"/> business object.
    /// </summary>
    [Serializable]
    public partial class MxTypeNVL : NameValueListBase<int, string>
    {

        #region Private Fields

        private static MxTypeNVL _list;

        #endregion

        #region Cache Management Methods

        /// <summary>
        /// Clears the in-memory MxTypeNVL cache so it is reloaded on the next request.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        /// <summary>
        /// Used by async loaders to load the cache.
        /// </summary>
        /// <param name="list">The list to cache.</param>
        internal static void SetCache(MxTypeNVL list)
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
        /// Factory method. Loads a <see cref="MxTypeNVL"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="MxTypeNVL"/> object.</returns>
        public static MxTypeNVL GetMxTypeNVL()
        {
            if (_list == null)
                _list = DataPortal.Fetch<MxTypeNVL>();

            return _list;
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="MxTypeNVL"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetMxTypeNVL(EventHandler<DataPortalResult<MxTypeNVL>> callback)
        {
            if (_list == null)
                DataPortal.BeginFetch<MxTypeNVL>((o, e) =>
                    {
                        _list = e.Object;
                        callback(o, e);
                    });
            else
                callback(null, new DataPortalResult<MxTypeNVL>(_list, null, null));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxTypeNVL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxTypeNVL()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="MxTypeNVL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxTypeNVL", ctx.Connection))
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
                        dr.GetInt32("MxTypeID"),
                        dr.GetString("MxTypeName")));
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
