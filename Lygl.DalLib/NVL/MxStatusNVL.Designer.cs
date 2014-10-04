using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// MxStatusNVL (name value list).<br/>
    /// This is a generated base class of <see cref="MxStatusNVL"/> business object.
    /// </summary>
    [Serializable]
    public partial class MxStatusNVL : NameValueListBase<int, string>
    {

        #region Private Fields

        private static MxStatusNVL _list;

        #endregion

        #region Cache Management Methods

        /// <summary>
        /// Clears the in-memory MxStatusNVL cache so it is reloaded on the next request.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        /// <summary>
        /// Used by async loaders to load the cache.
        /// </summary>
        /// <param name="list">The list to cache.</param>
        internal static void SetCache(MxStatusNVL list)
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
        /// Factory method. Loads a <see cref="MxStatusNVL"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="MxStatusNVL"/> object.</returns>
        public static MxStatusNVL GetMxStatusNVL()
        {
            if (_list == null)
                _list = DataPortal.Fetch<MxStatusNVL>();

            return _list;
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="MxStatusNVL"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetMxStatusNVL(EventHandler<DataPortalResult<MxStatusNVL>> callback)
        {
            if (_list == null)
                DataPortal.BeginFetch<MxStatusNVL>((o, e) =>
                    {
                        _list = e.Object;
                        callback(o, e);
                    });
            else
                callback(null, new DataPortalResult<MxStatusNVL>(_list, null, null));
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxStatusNVL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxStatusNVL()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="MxStatusNVL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxStatusNVL", ctx.Connection))
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
                        dr.GetInt32("Name"),
                        dr.GetString("Value")));
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
