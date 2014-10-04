using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// MxSzEditList (editable root list).<br/>
    /// This is a generated base class of <see cref="MxSzEditList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="MxSzEdit"/> objects.
    /// </remarks>
    [Serializable]
    public partial class MxSzEditList : BusinessListBase<MxSzEditList, MxSzEdit>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="MxSzEdit"/> item from the collection.
        /// </summary>
        /// <param name="szID">The SzID of the item to be removed.</param>
        public void Remove(Guid szID)
        {
            foreach (var mxSzEdit in this)
            {
                if (mxSzEdit.SzID == szID)
                {
                    Remove(mxSzEdit);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="MxSzEdit"/> item is in the collection.
        /// </summary>
        /// <param name="szID">The SzID of the item to search for.</param>
        /// <returns><c>true</c> if the MxSzEdit is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid szID)
        {
            foreach (var mxSzEdit in this)
            {
                if (mxSzEdit.SzID == szID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="MxSzEdit"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="szID">The SzID of the item to search for.</param>
        /// <returns><c>true</c> if the MxSzEdit is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid szID)
        {
            foreach (var mxSzEdit in this.DeletedList)
            {
                if (mxSzEdit.SzID == szID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the MxSzEditList to create.</param>
        /// <returns>A reference to the created <see cref="MxSzEditList"/> collection.</returns>
        public static MxSzEditList NewMxSzEditList(Guid mxID)
        {
            return DataPortal.Create<MxSzEditList>(mxID);
        }

        /// <summary>
        /// Factory method. Creates a new <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="name">The Name of the MxSzEditList to create.</param>
        /// <returns>A reference to the created <see cref="MxSzEditList"/> collection.</returns>
        public static MxSzEditList NewMxSzEditList(string name)
        {
            return DataPortal.Create<MxSzEditList>(name);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the MxSzEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="MxSzEditList"/> collection.</returns>
        public static MxSzEditList GetMxSzEditListByMxID(Guid mxID)
        {
            return DataPortal.Fetch<MxSzEditList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="name">The Name parameter of the MxSzEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="MxSzEditList"/> collection.</returns>
        public static MxSzEditList GetMxSzEditListBySzName(string name)
        {
            return DataPortal.Fetch<MxSzEditList>(name);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the MxSzEditList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewMxSzEditList(Guid mxID, EventHandler<DataPortalResult<MxSzEditList>> callback)
        {
            DataPortal.BeginCreate<MxSzEditList>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="name">The Name of the MxSzEditList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewMxSzEditList(string name, EventHandler<DataPortalResult<MxSzEditList>> callback)
        {
            DataPortal.BeginCreate<MxSzEditList>(name, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the MxSzEditList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetMxSzEditListByMxID(Guid mxID, EventHandler<DataPortalResult<MxSzEditList>> callback)
        {
            DataPortal.BeginFetch<MxSzEditList>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="MxSzEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="name">The Name parameter of the MxSzEditList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetMxSzEditListBySzName(string name, EventHandler<DataPortalResult<MxSzEditList>> callback)
        {
            DataPortal.BeginFetch<MxSzEditList>(name, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxSzEditList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxSzEditList()
        {
            // Prevent direct creation

            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            AllowNew = false;
            AllowEdit = false;
            AllowRemove = false;
            RaiseListChangedEvents = rlce;
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="MxSzEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxSzEditListByMxID", ctx.Connection))
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

        /// <summary>
        /// Loads a <see cref="MxSzEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="name">The Name.</param>
        protected void DataPortal_Fetch(string name)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxSzEditListBySzName", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd, name);
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
        /// Loads all <see cref="MxSzEditList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<MxSzEdit>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="MxSzEditList"/> object.
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
