using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BusinessDdList (editable root list).<br/>
    /// This is a generated base class of <see cref="BusinessDdList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="BusinessDd"/> objects.
    /// </remarks>
    [Serializable]
    public partial class BusinessDdList : BusinessListBase<BusinessDdList, BusinessDd>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="BusinessDd"/> item from the collection.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to be removed.</param>
        public void Remove(Guid businessID)
        {
            foreach (var businessDd in this)
            {
                if (businessDd.BusinessID == businessID)
                {
                    Remove(businessDd);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="BusinessDd"/> item is in the collection.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to search for.</param>
        /// <returns><c>true</c> if the BusinessDd is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid businessID)
        {
            foreach (var businessDd in this)
            {
                if (businessDd.BusinessID == businessID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="BusinessDd"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="businessID">The BusinessID of the item to search for.</param>
        /// <returns><c>true</c> if the BusinessDd is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid businessID)
        {
            foreach (var businessDd in this.DeletedList)
            {
                if (businessDd.BusinessID == businessID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Find Methods

        /// <summary>
        /// Finds a <see cref="BusinessDd"/> item of the <see cref="BusinessDdList"/> collection, based on a given BusinessID.
        /// </summary>
        /// <param name="businessID">The BusinessID.</param>
        /// <returns>A <see cref="BusinessDd"/> object.</returns>
        public BusinessDd FindBusinessDdByBusinessID(Guid businessID)
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
        /// Factory method. Creates a new <see cref="BusinessDdList"/> collection.
        /// </summary>
        /// <returns>A reference to the created <see cref="BusinessDdList"/> collection.</returns>
        public static BusinessDdList NewBusinessDdList()
        {
            return DataPortal.Create<BusinessDdList>();
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="BusinessDdList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessDdList to create.</param>
        /// <returns>A reference to the created <see cref="BusinessDdList"/> collection.</returns>
        public static BusinessDdList NewBusinessDdList(Guid mxID)
        {
            return DataPortal.Create<BusinessDdList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessDdList"/> collection.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="BusinessDdList"/> collection.</returns>
        public static BusinessDdList GetBusinessDdList()
        {
            return DataPortal.Fetch<BusinessDdList>();
        }

        /// <summary>
        /// Factory method. Loads a <see cref="BusinessDdList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessDdList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessDdList"/> collection.</returns>
        public static BusinessDdList GetBusinessDdListByMxID(Guid mxID)
        {
            return DataPortal.Fetch<BusinessDdList>(mxID);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessDdList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessDdList(EventHandler<DataPortalResult<BusinessDdList>> callback)
        {
            DataPortal.BeginCreate<BusinessDdList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="BusinessDdList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the BusinessDdList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewBusinessDdList(Guid mxID, EventHandler<DataPortalResult<BusinessDdList>> callback)
        {
            DataPortal.BeginCreate<BusinessDdList>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessDdList"/> collection.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessDdList(EventHandler<DataPortalResult<BusinessDdList>> callback)
        {
            DataPortal.BeginFetch<BusinessDdList>(callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="BusinessDdList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessDdList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetBusinessDdListByMxID(Guid mxID, EventHandler<DataPortalResult<BusinessDdList>> callback)
        {
            DataPortal.BeginFetch<BusinessDdList>(mxID, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessDdList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BusinessDdList()
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
        /// Loads a <see cref="BusinessDdList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessDdList", ctx.Connection))
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
        /// Loads a <see cref="BusinessDdList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetBusinessDdListByMxID", ctx.Connection))
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
        /// Loads all <see cref="BusinessDdList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<BusinessDd>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BusinessDdList"/> object.
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
