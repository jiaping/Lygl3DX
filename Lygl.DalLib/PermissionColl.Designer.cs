using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib
{

    /// <summary>
    /// PermissionColl (editable root list).<br/>
    /// This is a generated base class of <see cref="PermissionColl"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="Permission"/> objects.
    /// </remarks>
    [Serializable]
    public partial class PermissionColl : BusinessListBase<PermissionColl, Permission>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="Permission"/> item from the collection.
        /// </summary>
        /// <param name="permissionID">The PermissionID of the item to be removed.</param>
        public void Remove(Guid permissionID)
        {
            foreach (var permission in this)
            {
                if (permission.PermissionID == permissionID)
                {
                    Remove(permission);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="Permission"/> item is in the collection.
        /// </summary>
        /// <param name="permissionID">The PermissionID of the item to search for.</param>
        /// <returns><c>true</c> if the Permission is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid permissionID)
        {
            foreach (var permission in this)
            {
                if (permission.PermissionID == permissionID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="Permission"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="permissionID">The PermissionID of the item to search for.</param>
        /// <returns><c>true</c> if the Permission is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid permissionID)
        {
            foreach (var permission in this.DeletedList)
            {
                if (permission.PermissionID == permissionID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="PermissionColl"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="PermissionColl"/> collection.</returns>
        public static PermissionColl NewPermissionColl()
        {
            return DataPortal.Create<PermissionColl>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="PermissionColl"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="PermissionColl"/> collection.</returns>
        public static PermissionColl GetPermissionColl()
        {
            return DataPortal.Fetch<PermissionColl>();
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="PermissionColl"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewPermissionColl(EventHandler<DataPortalResult<PermissionColl>> callback)
        {
            DataPortal.BeginCreate<PermissionColl>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="PermissionColl"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetPermissionColl(EventHandler<DataPortalResult<PermissionColl>> callback)
        {
            DataPortal.BeginFetch<PermissionColl>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionColl"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private PermissionColl()
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
        /// Loads a <see cref="PermissionColl"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetPermissionColl", ctx.Connection))
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
        /// Loads all <see cref="PermissionColl"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<Permission>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="PermissionColl"/> object.
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
