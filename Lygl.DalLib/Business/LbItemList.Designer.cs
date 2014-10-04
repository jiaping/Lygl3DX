using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// LbItemList (editable child list).<br/>
    /// This is a generated base class of <see cref="LbItemList"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is child of <see cref="BusinessLb"/> editable root object.<br/>
    /// The items of the collection are <see cref="LbItem"/> objects.
    /// </remarks>
    [Serializable]
    public partial class LbItemList : BusinessListBase<LbItemList, LbItem>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="LbItem"/> item from the collection.
        /// </summary>
        /// <param name="lbItemID">The LbItemID of the item to be removed.</param>
        public void Remove(Guid lbItemID)
        {
            foreach (var lbItem in this)
            {
                if (lbItem.LbItemID == lbItemID)
                {
                    Remove(lbItem);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="LbItem"/> item is in the collection.
        /// </summary>
        /// <param name="lbItemID">The LbItemID of the item to search for.</param>
        /// <returns><c>true</c> if the LbItem is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid lbItemID)
        {
            foreach (var lbItem in this)
            {
                if (lbItem.LbItemID == lbItemID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="LbItem"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="lbItemID">The LbItemID of the item to search for.</param>
        /// <returns><c>true</c> if the LbItem is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid lbItemID)
        {
            foreach (var lbItem in this.DeletedList)
            {
                if (lbItem.LbItemID == lbItemID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LbItemList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private LbItemList()
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
        /// Loads all <see cref="LbItemList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            var args = new DataPortalHookArgs(dr);
            OnFetchPre(args);
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<LbItem>(dr));
            }
            OnFetchPost(args);
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
