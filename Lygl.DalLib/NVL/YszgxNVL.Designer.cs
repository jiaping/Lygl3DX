using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// YszgxNVL (name value list).<br/>
    /// This is a generated base class of <see cref="YszgxNVL"/> business object.
    /// </summary>
    [Serializable]
    public partial class YszgxNVL : NameValueListBase<int, string>
    {

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="YszgxNVL"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="YszgxNVL"/> object.</returns>
        public static YszgxNVL GetYszgxNVL()
        {
            return DataPortal.Fetch<YszgxNVL>();
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="YszgxNVL"/> object.
        /// </summary>
        /// <param name="callback">The completion callback method.</param>
        public static void GetYszgxNVL(EventHandler<DataPortalResult<YszgxNVL>> callback)
        {
            DataPortal.BeginFetch<YszgxNVL>(callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="YszgxNVL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private YszgxNVL()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="YszgxNVL"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetYszgxNVL", ctx.Connection))
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
                        dr.GetInt32("Name"),
                        dr.GetString("Value")));
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
