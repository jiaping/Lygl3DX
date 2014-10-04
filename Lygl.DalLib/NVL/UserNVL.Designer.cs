using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// UserNVL (name value list).<br/>
    /// This is a generated base class of <see cref="UserNVL"/> business object.
    /// </summary>
    [Serializable]
    public partial class UserNVL : NameValueListBase<Guid, string>
    {

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="UserNVL"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="UserNVL"/> object.</returns>
        public static UserNVL GetUserNVL()
        {
            return DataPortal.Fetch<UserNVL>();
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserNVL"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserNVL(EventHandler<DataPortalResult<UserNVL>> callback)
        {
            DataPortal.BeginFetch<UserNVL>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNVL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private UserNVL()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="UserNVL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserNVL", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }

        private void LoadCollection(SqlCommand cmd)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                while (dr.Read())
                {
                    Add(new NameValuePair(
                        dr.GetGuid("UserID"),
                        dr.GetString("Code")));
                }
            }
            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
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
