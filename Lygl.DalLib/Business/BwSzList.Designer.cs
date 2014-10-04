using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BwSzList (editable child list).<br/>
    /// This is a generated base class of <see cref="BwSzList"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is child of <see cref="BusinessLb"/> editable root object.<br/>
    /// The items of the collection are <see cref="BwSz"/> objects.
    /// </remarks>
    [Serializable]
    public partial class BwSzList : BusinessListBase<BwSzList, BwSz>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="BwSz"/> item from the collection.
        /// </summary>
        /// <param name="bwSzID">The BwSzID of the item to be removed.</param>
        public void Remove(Guid bwSzID)
        {
            foreach (var bwSz in this)
            {
                if (bwSz.BwSzID == bwSzID)
                {
                    Remove(bwSz);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="BwSz"/> item is in the collection.
        /// </summary>
        /// <param name="bwSzID">The BwSzID of the item to search for.</param>
        /// <returns><c>true</c> if the BwSz is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid bwSzID)
        {
            foreach (var bwSz in this)
            {
                if (bwSz.BwSzID == bwSzID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="BwSz"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="bwSzID">The BwSzID of the item to search for.</param>
        /// <returns><c>true</c> if the BwSz is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid bwSzID)
        {
            foreach (var bwSz in this.DeletedList)
            {
                if (bwSz.BwSzID == bwSzID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BwSzList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BwSzList()
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
        /// Loads all <see cref="BwSzList"/> collection items from the given SafeDataReader.
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
                Add(DataPortal.FetchChild<BwSz>(dr));
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
