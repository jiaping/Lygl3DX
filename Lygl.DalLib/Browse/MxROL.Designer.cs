using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Browse
{

    /// <summary>
    /// MxROL (read only list).<br/>
    /// This is a generated base class of <see cref="MxROL"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="MxRO"/> objects.
    /// </remarks>
    [Serializable]
    public partial class MxROL : ReadOnlyListBase<MxROL, MxRO>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="MxRO"/> item is in the collection.
        /// </summary>
        /// <param name="mxID">The MxID of the item to search for.</param>
        /// <returns><c>true</c> if the MxRO is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid mxID)
        {
            foreach (var mxRO in this)
            {
                if (mxRO.MxID == mxID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="MxROL"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="MxROL"/> collection.</returns>
        public static MxROL GetMxROL()
        {
            return DataPortal.Fetch<MxROL>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="MxROL"/> collection, based on given parameters.
        /// </summary>
        /// <param name="areaID">The AreaID parameter of the MxROL to fetch.</param>
        /// <returns>A reference to the fetched <see cref="MxROL"/> collection.</returns>
        public static MxROL GetMxROLByAreaID(Guid areaID)
        {
            return DataPortal.Fetch<MxROL>(areaID);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxROL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxROL()
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
        /// Loads a <see cref="MxROL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxROL", ctx.Connection))
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
        /// Loads a <see cref="MxROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="areaID">The Area ID.</param>
        [Csla.RunLocal]
        protected void DataPortal_Fetch(Guid areaID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxROLByAreaID", ctx.Connection))
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
        /// Loads all <see cref="MxROL"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<MxRO>(dr));
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
