using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// QtsfItemList (editable child list).<br/>
    /// This is a generated base class of <see cref="QtsfItemList"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is child of <see cref="BusinessQtsf"/> editable child object.<br/>
    /// The items of the collection are <see cref="QtsfItem"/> objects.
    /// </remarks>
    [Serializable]
    public partial class QtsfItemList : BusinessListBase<QtsfItemList, QtsfItem>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="QtsfItem"/> item from the collection.
        /// </summary>
        /// <param name="qtsfItemID">The QtsfItemID of the item to be removed.</param>
        public void Remove(Guid qtsfItemID)
        {
            foreach (var qtsfItem in this)
            {
                if (qtsfItem.QtsfItemID == qtsfItemID)
                {
                    Remove(qtsfItem);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="QtsfItem"/> item is in the collection.
        /// </summary>
        /// <param name="qtsfItemID">The QtsfItemID of the item to search for.</param>
        /// <returns><c>true</c> if the QtsfItem is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid qtsfItemID)
        {
            foreach (var qtsfItem in this)
            {
                if (qtsfItem.QtsfItemID == qtsfItemID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="QtsfItem"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="qtsfItemID">The QtsfItemID of the item to search for.</param>
        /// <returns><c>true</c> if the QtsfItem is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid qtsfItemID)
        {
            foreach (var qtsfItem in this.DeletedList)
            {
                if (qtsfItem.QtsfItemID == qtsfItemID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="QtsfItemList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private QtsfItemList()
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
        /// Loads a <see cref="QtsfItemList"/> collection from the database.
        /// </summary>
        protected void Child_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetQtsfItemList", ctx.Connection))
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
        /// Loads a <see cref="QtsfItemList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="businessID">The Business ID.</param>
        protected void Child_Fetch(Guid businessID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetQtsfItemListByBusinessID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", businessID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, businessID);
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
        /// Loads all <see cref="QtsfItemList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<QtsfItem>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Pseudo Events

        /// <summary>
        /// Occurs after setting all defaults for object creation.
        /// </summary>
        partial void OnCreate(DataPortalHookArgs args);

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
