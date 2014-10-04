using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager.OrgUserTree
{

    /// <summary>
    /// OrgROL (read only list).<br/>
    /// This is a generated base class of <see cref="OrgROL"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="OrgNodeInfo"/> objects.
    /// </remarks>
    [Serializable]
    public partial class OrgROL : ReadOnlyListBase<OrgROL, OrgNodeInfo>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="OrgNodeInfo"/> item is in the collection.
        /// </summary>
        /// <param name="orgID">The OrgID of the item to search for.</param>
        /// <returns><c>true</c> if the OrgNodeInfo is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid orgID)
        {
            foreach (var orgNodeInfo in this)
            {
                if (orgNodeInfo.OrgID == orgID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="OrgROL"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="OrgROL"/> collection.</returns>
        public static OrgROL GetOrgROL()
        {
            return DataPortal.Fetch<OrgROL>();
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="OrgROL"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetOrgROL(EventHandler<DataPortalResult<OrgROL>> callback)
        {
            DataPortal.BeginFetch<OrgROL>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrgROL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private OrgROL()
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
        /// Loads a <see cref="OrgROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="pID">The PID.</param>
        protected void DataPortal_Fetch(Guid pID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetOrgChild", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PID", pID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, pID);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="OrgROL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetOrgROL", ctx.Connection))
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
        /// Loads all <see cref="OrgROL"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<OrgNodeInfo>(dr));
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
