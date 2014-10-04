using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// UserNameNVL (name value list).<br/>
    /// This is a generated base class of <see cref="UserNameNVL"/> business object.
    /// </summary>
    [Serializable]
    public partial class UserNameNVL : NameValueListBase<Guid, string>
    {

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="UserNameNVL"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="UserNameNVL"/> object.</returns>
        public static UserNameNVL GetUserNameNVL()
        {
            return DataPortal.Fetch<UserNameNVL>();
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="UserNameNVL"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetUserNameNVL(EventHandler<DataPortalResult<UserNameNVL>> callback)
        {
            DataPortal.BeginFetch<UserNameNVL>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNameNVL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private UserNameNVL()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="UserNameNVL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetUserNameNVL", ctx.Connection))
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
                        dr.GetString("Name")));
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
