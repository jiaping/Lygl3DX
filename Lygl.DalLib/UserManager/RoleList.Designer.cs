using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Lygl.DalLib.Core;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// RoleList (editable root list).<br/>
    /// This is a generated base class of <see cref="RoleList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="Role"/> objects.
    /// </remarks>
    [Serializable]
    public partial class RoleList : BusinessListBase<RoleList, Role>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="Role"/> item from the collection.
        /// </summary>
        /// <param name="roleID">The RoleID of the item to be removed.</param>
        public void Remove(Guid roleID)
        {
            foreach (var role in this)
            {
                if (role.RoleID == roleID)
                {
                    Remove(role);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="Role"/> item is in the collection.
        /// </summary>
        /// <param name="roleID">The RoleID of the item to search for.</param>
        /// <returns><c>true</c> if the Role is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid roleID)
        {
            foreach (var role in this)
            {
                if (role.RoleID == roleID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="Role"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="roleID">The RoleID of the item to search for.</param>
        /// <returns><c>true</c> if the Role is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid roleID)
        {
            foreach (var role in this.DeletedList)
            {
                if (role.RoleID == roleID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="RoleList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="roleID">The RoleID of the RoleList to create.</param>
        /// <returns>A reference to the created <see cref="RoleList"/> collection.</returns>
        public static RoleList NewRoleList(Guid roleID)
        {
            return DataPortal.Create<RoleList>(roleID);
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="RoleList"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="RoleList"/> collection.</returns>
        public static RoleList NewRoleList()
        {
            return DataPortal.Create<RoleList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="RoleList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="roleID">The RoleID parameter of the RoleList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="RoleList"/> collection.</returns>
        public static RoleList GetRoleListByRoleID(Guid roleID)
        {
            return DataPortal.Fetch<RoleList>(roleID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="RoleList"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="RoleList"/> collection.</returns>
        public static RoleList GetRoleList()
        {
            return DataPortal.Fetch<RoleList>();
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="RoleList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="roleID">The RoleID of the RoleList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewRoleList(Guid roleID, EventHandler<DataPortalResult<RoleList>> callback)
        {
            DataPortal.BeginCreate<RoleList>(roleID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="RoleList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewRoleList(EventHandler<DataPortalResult<RoleList>> callback)
        {
            DataPortal.BeginCreate<RoleList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="RoleList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="roleID">The RoleID parameter of the RoleList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetRoleListByRoleID(Guid roleID, EventHandler<DataPortalResult<RoleList>> callback)
        {
            DataPortal.BeginFetch<RoleList>(roleID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="RoleList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetRoleList(EventHandler<DataPortalResult<RoleList>> callback)
        {
            DataPortal.BeginFetch<RoleList>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private RoleList()
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
        /// Loads a <see cref="RoleList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="roleID">The Role ID.</param>
        protected void DataPortal_Fetch(Guid roleID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetRoleListByRoleID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, roleID);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
            foreach (var item in this)
            {
                item.FetchChildren();
            }
        }

        /// <summary>
        /// Loads a <see cref="RoleList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetRoleList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
            foreach (var item in this)
            {
                item.FetchChildren();
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
        /// Loads all <see cref="RoleList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<Role>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="RoleList"/> object.
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
