using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// UserRoleList (editable child list).<br/>
    /// This is a generated base class of <see cref="UserRoleList"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is child of <see cref="User"/> editable child object.<br/>
    /// The items of the collection are <see cref="UserRole"/> objects.
    /// </remarks>
    [Serializable]
    public partial class UserRoleList : BusinessListBase<UserRoleList, UserRole>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="UserRole"/> item from the collection.
        /// </summary>
        /// <param name="roleID">The RoleID of the item to be removed.</param>
        public void Remove(Guid roleID)
        {
            foreach (var userRole in this)
            {
                if (userRole.RoleID == roleID)
                {
                    Remove(userRole);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="UserRole"/> item is in the collection.
        /// </summary>
        /// <param name="roleID">The RoleID of the item to search for.</param>
        /// <returns><c>true</c> if the UserRole is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid roleID)
        {
            foreach (var userRole in this)
            {
                if (userRole.RoleID == roleID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="UserRole"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="roleID">The RoleID of the item to search for.</param>
        /// <returns><c>true</c> if the UserRole is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid roleID)
        {
            foreach (var userRole in this.DeletedList)
            {
                if (userRole.RoleID == roleID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private UserRoleList()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();

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
        /// Loads a <see cref="UserRoleList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="userID">The User ID.</param>
        protected void Child_Fetch(Guid userID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserRoleList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", userID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, userID);
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
        /// Loads all <see cref="UserRoleList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<UserRole>(dr));
            }
            RaiseListChangedEvents = rlce;
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
