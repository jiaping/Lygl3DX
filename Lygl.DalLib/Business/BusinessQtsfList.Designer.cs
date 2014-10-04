using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BusinessQtsfList (editable root list).<br/>
    /// This is a generated base class of <see cref="BusinessQtsfList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="BusinessQtsf"/> objects.
    /// </remarks>
    [Serializable]
    public partial class BusinessQtsfList : BusinessListBase<BusinessQtsfList, BusinessQtsf>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="BusinessQtsf"/> item from the collection.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to be removed.</param>
        public void Remove(Guid businessID)
        {
            foreach (var businessQtsf in this)
            {
                if (businessQtsf.BusinessID == businessID)
                {
                    Remove(businessQtsf);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="BusinessQtsf"/> item is in the collection.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to search for.</param>
        /// <returns><c>true</c> if the BusinessQtsf is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid businessID)
        {
            foreach (var businessQtsf in this)
            {
                if (businessQtsf.BusinessID == businessID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="BusinessQtsf"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to search for.</param>
        /// <returns><c>true</c> if the BusinessQtsf is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid businessID)
        {
            foreach (var businessQtsf in this.DeletedList)
            {
                if (businessQtsf.BusinessID == businessID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Find Methods

        /// <summary>
        /// Finds a <see cref="BusinessQtsf"/> item of the <see cref="BusinessQtsfList"/> collection, based on a given BusinessID.
        /// </summary>
        /// <param name="businessID">The BusinessID.</param>
        /// <returns>A <see cref="BusinessQtsf"/> object.</returns>
        public BusinessQtsf FindBusinessQtsfByBusinessID(Guid businessID)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if (this[i].BusinessID.Equals(businessID))
                {
                    return this[i];
                }
            }

            return null;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessQtsfList"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="BusinessQtsfList"/> collection.</returns>
        public static BusinessQtsfList NewBusinessQtsfList()
        {
            return DataPortal.Create<BusinessQtsfList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessQtsfList"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="BusinessQtsfList"/> collection.</returns>
        public static BusinessQtsfList GetBusinessQtsfList()
        {
            return DataPortal.Fetch<BusinessQtsfList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessQtsfList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessQtsfList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessQtsfList"/> collection.</returns>
        public static BusinessQtsfList GetBusinessQtsfListByMxID(Guid mxID)
        {
            return DataPortal.Fetch<BusinessQtsfList>(mxID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessQtsfList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessQtsfList(EventHandler<DataPortalResult<BusinessQtsfList>> callback)
        {
            DataPortal.BeginCreate<BusinessQtsfList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessQtsfList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessQtsfList(EventHandler<DataPortalResult<BusinessQtsfList>> callback)
        {
            DataPortal.BeginFetch<BusinessQtsfList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessQtsfList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessQtsfList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessQtsfListByMxID(Guid mxID, EventHandler<DataPortalResult<BusinessQtsfList>> callback)
        {
            DataPortal.BeginFetch<BusinessQtsfList>(mxID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessQtsfList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessQtsfList()
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
        /// Loads a <see cref="BusinessQtsfList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessQtsfList", ctx.Connection))
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

        /// <summary>
        /// Loads a <see cref="BusinessQtsfList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessQtsfListByMxID", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", mxID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, mxID);
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
        /// Loads all <see cref="BusinessQtsfList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<BusinessQtsf>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BusinessQtsfList"/> object.
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
