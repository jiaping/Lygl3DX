using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Browse
{

    /// <summary>
    /// AreaROL (read only list).<br/>
    /// This is a generated base class of <see cref="AreaROL"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="AreaRO"/> objects.
    /// </remarks>
    [Serializable]
    public partial class AreaROL : ReadOnlyListBase<AreaROL, AreaRO>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="AreaRO"/> item is in the collection.
        /// </summary>
        /// <param name="areaID">The AreaID of the item to search for.</param>
        /// <returns><c>true</c> if the AreaRO is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid areaID)
        {
            foreach (var areaRO in this)
            {
                if (areaRO.AreaID == areaID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Find Methods

        /// <summary>
        /// Finds a <see cref="AreaRO"/> item of the <see cref="AreaROL"/> collection, based on a given AreaID.
        /// </summary>
        /// <param name="areaID">The AreaID.</param>
        /// <returns>A <see cref="AreaRO"/> object.</returns>
        public AreaRO FindAreaROByAreaID(Guid areaID)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].AreaID.Equals(areaID))
                {
                    return this[i];
                }
            }

            return null;
        }

        #endregion

        #region Private Fields

        private static AreaROL _list;

        #endregion

        #region Cache Management Methods

        /// <summary>
        /// Clears the in-memory AreaROL cache so it is reloaded on the next request.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        /// <summary>
        /// Used by async loaders to load the cache.
        /// </summary>
        /// <param name="list">The list to cache.</param>
        internal static void SetCache(AreaROL list)
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
        /// Factory method. Loads a <see cref="AreaROL"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="AreaROL"/> collection.</returns>
        public static AreaROL GetAreaROL()
        {
            if (_list == null)
                _list = DataPortal.Fetch<AreaROL>();

            return _list;
        }

        /// <summary>
        /// Factory method. Loads a <see cref="AreaROL"/> collection, based on given parameters.
        /// </summary>
        /// <param name="areaID">The AreaID parameter of the AreaROL to fetch.</param>
        /// <returns>A reference to the fetched <see cref="AreaROL"/> collection.</returns>
        public static AreaROL GetAreaROLByID(Guid areaID)
        {
            return DataPortal.Fetch<AreaROL>(areaID);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaROL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private AreaROL()
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
        /// Loads a <see cref="AreaROL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetAreaROL", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="AreaROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="areaID">The Area ID.</param>
        protected void DataPortal_Fetch(Guid areaID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetAreaROLByID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AreaID", areaID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, areaID);
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
        /// Loads all <see cref="AreaROL"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<AreaRO>(dr));
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
