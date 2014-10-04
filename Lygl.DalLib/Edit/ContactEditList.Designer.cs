using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// ContactEditList (editable root list).<br/>
    /// This is a generated base class of <see cref="ContactEditList"/> business object.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="ContactEdit"/> objects.
    /// </remarks>
    [Serializable]
    public partial class ContactEditList : BusinessListBase<ContactEditList, ContactEdit>
    {

        #region Collection Business Methods

        /// <summary>
        /// Removes a <see cref="ContactEdit"/> item from the collection.
        /// </summary>
        /// <param name="contactID">The ContactID of the item to be removed.</param>
        public void Remove(Guid contactID)
        {
            foreach (var contactEdit in this)
            {
                if (contactEdit.ContactID == contactID)
                {
                    Remove(contactEdit);
                    break;
                }
            }
        }

        /// <summary>
        /// Determines whether a <see cref="ContactEdit"/> item is in the collection.
        /// </summary>
        /// <param name="contactID">The ContactID of the item to search for.</param>
        /// <returns><c>true</c> if the ContactEdit is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(Guid contactID)
        {
            foreach (var contactEdit in this)
            {
                if (contactEdit.ContactID == contactID)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determines whether a <see cref="ContactEdit"/> item is in the collection's DeletedList.
        /// </summary>
        /// <param name="contactID">The ContactID of the item to search for.</param>
        /// <returns><c>true</c> if the ContactEdit is a deleted collection item; otherwise, <c>false</c>.</returns>
        public bool ContainsDeleted(Guid contactID)
        {
            foreach (var contactEdit in this.DeletedList)
            {
                if (contactEdit.ContactID == contactID)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Creates a new <see cref="ContactEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the ContactEditList to create.</param>
        /// <returns>A reference to the created <see cref="ContactEditList"/> collection.</returns>
        public static ContactEditList NewContactEditList(Guid mxID)
        {
            return DataPortal.Create<ContactEditList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="ContactEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the ContactEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="ContactEditList"/> collection.</returns>
        public static ContactEditList GetContactEditListByMxID(Guid mxID)
        {
            return DataPortal.Fetch<ContactEditList>(mxID);
        }

        /// <summary>
        /// Factory method. Loads a <see cref="ContactEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="name">The Name parameter of the ContactEditList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="ContactEditList"/> collection.</returns>
        public static ContactEditList GetContactEditListByName(string name)
        {
            return DataPortal.Fetch<ContactEditList>(name);
        }

        /// <summary>
        /// Factory method. Asynchronously creates a new <see cref="ContactEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID of the ContactEditList to create.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void NewContactEditList(Guid mxID, EventHandler<DataPortalResult<ContactEditList>> callback)
        {
            DataPortal.BeginCreate<ContactEditList>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="ContactEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the ContactEditList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetContactEditListByMxID(Guid mxID, EventHandler<DataPortalResult<ContactEditList>> callback)
        {
            DataPortal.BeginFetch<ContactEditList>(mxID, callback);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="ContactEditList"/> collection, based on given parameters.
        /// </summary>
        /// <param name="name">The Name parameter of the ContactEditList to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetContactEditListByName(string name, EventHandler<DataPortalResult<ContactEditList>> callback)
        {
            DataPortal.BeginFetch<ContactEditList>(name, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactEditList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private ContactEditList()
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
        /// Loads a <see cref="ContactEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(Guid mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetContactEditListByMxID", ctx.Connection))
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
        /// Loads a <see cref="ContactEditList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="name">The Name.</param>
        protected void DataPortal_Fetch(string name)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetContactEditListByName", ctx.Connection))
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
        /// Loads all <see cref="ContactEditList"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<ContactEdit>(dr));
            }
            RaiseListChangedEvents = rlce;
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="ContactEditList"/> object.
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
