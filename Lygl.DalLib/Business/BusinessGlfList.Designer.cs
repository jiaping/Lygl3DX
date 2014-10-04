using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BusinessGlfList (editable root list).<br/>
    /// This is a generated base class of <see cref="BusinessGlfList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="BusinessGlf"/> objects.
    /// </remarks>
    [Serializable]
    public partial class BusinessGlfList : BusinessListBase<BusinessGlfList, BusinessGlf>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="BusinessGlf"/> item from the collection.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to be removed.</param>
        public void Remove(Guid businessID)
        {
            foreach (var businessGlf in this)
            {
                if (businessGlf.BusinessID == businessID)
                {
                    Remove(businessGlf);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="BusinessGlf"/> item is in the collection.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to search for.</param>
        /// <returns><c>true</c> if the BusinessGlf is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid businessID)
        {
            foreach (var businessGlf in this)
            {
                if (businessGlf.BusinessID == businessID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="BusinessGlf"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to search for.</param>
        /// <returns><c>true</c> if the BusinessGlf is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid businessID)
        {
            foreach (var businessGlf in this.DeletedList)
            {
                if (businessGlf.BusinessID == businessID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessGlfList"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="BusinessGlfList"/> collection.</returns>
        public static BusinessGlfList NewBusinessGlfList()
        {
            return DataPortal.Create<BusinessGlfList>();
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessGlfList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessGlfList to create.</param>
        /// <returns>A reference to the created <see cref="BusinessGlfList"/> collection.</returns>
        public static BusinessGlfList NewBusinessGlfList(Guid mxID)
        {
            return DataPortal.Create<BusinessGlfList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessGlfList"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="BusinessGlfList"/> collection.</returns>
        public static BusinessGlfList GetBusinessGlfList()
        {
            return DataPortal.Fetch<BusinessGlfList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessGlfList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessGlfList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessGlfList"/> collection.</returns>
        public static BusinessGlfList GetBusinessGlfListGetByMx(Guid mxID)
        {
            return DataPortal.Fetch<BusinessGlfList>(mxID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessGlfList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessGlfList(EventHandler<DataPortalResult<BusinessGlfList>> callback)
        {
            DataPortal.BeginCreate<BusinessGlfList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessGlfList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessGlfList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessGlfList(Guid mxID, EventHandler<DataPortalResult<BusinessGlfList>> callback)
        {
            DataPortal.BeginCreate<BusinessGlfList>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessGlfList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessGlfList(EventHandler<DataPortalResult<BusinessGlfList>> callback)
        {
            DataPortal.BeginFetch<BusinessGlfList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessGlfList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessGlfList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessGlfListGetByMx(Guid mxID, EventHandler<DataPortalResult<BusinessGlfList>> callback)
        {
            DataPortal.BeginFetch<BusinessGlfList>(mxID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessGlfList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessGlfList()
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
        /// Loads a <see cref="BusinessGlfList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessGlfList", ctx.Connection))
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
        /// Loads a <see cref="BusinessGlfList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessGlfListGetByMx", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", mxID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, mxID);
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
        /// Loads all <see cref="BusinessGlfList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<BusinessGlf>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BusinessGlfList"/> object.
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
