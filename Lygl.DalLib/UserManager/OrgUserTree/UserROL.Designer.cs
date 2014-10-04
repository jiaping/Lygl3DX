using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager.OrgUserTree
{

    /// <summary>
    /// UserROL (read only list).<br/>
    /// This is a generated base class of <see cref="UserROL"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="UserNodeInfo"/> objects.
    /// </remarks>
    [Serializable]
    public partial class UserROL : ReadOnlyListBase<UserROL, UserNodeInfo>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="UserNodeInfo"/> item is in the collection.
        /// </summary>
        /// <param name="userID">The UserID of the item to search for.</param>
        /// <returns><c>true</c> if the UserNodeInfo is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid userID)
        {
            foreach (var userNodeInfo in this)
            {
                if (userNodeInfo.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="UserROL"/> collection, based on given parameters.
        /// </summary>
        /// <param name="orgID">The OrgID parameter of the UserROL to fetch.</param>
        /// <returns>A reference to the fetched <see cref="UserROL"/> collection.</returns>
        public static UserROL GetUserROL(Guid orgID)
        {
            return DataPortal.Fetch<UserROL>(orgID);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserROL"/> collection, based on given parameters.
        /// </summary>
        /// <param name="orgID">The OrgID parameter of the UserROL to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserROL(Guid orgID, EventHandler<DataPortalResult<UserROL>> callback)
        {
            DataPortal.BeginFetch<UserROL>(orgID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserROL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private UserROL()
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
        /// Loads a <see cref="UserROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="orgID">The Org ID.</param>
        protected void DataPortal_Fetch(Guid orgID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserROL", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", orgID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, orgID);
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
        /// Loads all <see cref="UserROL"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<UserNodeInfo>(dr));
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
