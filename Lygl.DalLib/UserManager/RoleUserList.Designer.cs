using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager
{

    /// <summary>
    /// RoleUserList (editable child list).<br/>
    /// This is a generated base class of <see cref="RoleUserList"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is child of <see cref="Role"/> editable child object.<br/>
    /// The items of the collection are <see cref="RoleUser"/> objects.
    /// </remarks>
    [Serializable]
    public partial class RoleUserList : BusinessListBase<RoleUserList, RoleUser>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="RoleUser"/> item from the collection.
        /// </summary>
        /// <param name="userID">The UserID of the item to be removed.</param>
        public void Remove(Guid userID)
        {
            foreach (var roleUser in this)
            {
                if (roleUser.UserID == userID)
                {
                    Remove(roleUser);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="RoleUser"/> item is in the collection.
        /// </summary>
        /// <param name="userID">The UserID of the item to search for.</param>
        /// <returns><c>true</c> if the RoleUser is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid userID)
        {
            foreach (var roleUser in this)
            {
                if (roleUser.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="RoleUser"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="userID">The UserID of the item to search for.</param>
        /// <returns><c>true</c> if the RoleUser is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid userID)
        {
            foreach (var roleUser in this.DeletedList)
            {
                if (roleUser.UserID == userID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleUserList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private RoleUserList()
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
        /// Loads a <see cref="RoleUserList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="roleID">The Role ID.</param>
        protected void Child_Fetch(Guid roleID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetRoleUserList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, roleID);
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
        /// Loads all <see cref="RoleUserList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<RoleUser>(dr));
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
