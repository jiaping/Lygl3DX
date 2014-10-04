using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Seek
{

    /// <summary>
    /// LogROL (read only list).<br/>
    /// This is a generated base class of <see cref="LogROL"/> business object.
    /// This class is a root collection.
    /// </summary>
    /// <remarks>
    /// The items of the collection are <see cref="LogRO"/> objects.
    /// </remarks>
    [Serializable]
    public partial class LogROL : ReadOnlyListBase<LogROL, LogRO>
    {

        #region Collection Business Methods

        /// <summary>
        /// Determines whether a <see cref="LogRO"/> item is in the collection.
        /// </summary>
        /// <param name="id">The Id of the item to search for.</param>
        /// <returns><c>true</c> if the LogRO is a collection item; otherwise, <c>false</c>.</returns>
        public bool Contains(int id)
        {
            foreach (var logRO in this)
            {
                if (logRO.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Factory method. Loads a <see cref="LogROL"/> collection, based on given parameters.
        /// </summary>
        /// <param name="date">The Date parameter of the LogROL to fetch.</param>
        /// <returns>A reference to the fetched <see cref="LogROL"/> collection.</returns>
        public static LogROL GetLogROL(SmartDate date)
        {
            return DataPortal.Fetch<LogROL>(date);
        }

        /// <summary>
        /// Factory method. Asynchronously loads a <see cref="LogROL"/> collection, based on given parameters.
        /// </summary>
        /// <param name="date">The Date parameter of the LogROL to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetLogROL(SmartDate date, EventHandler<DataPortalResult<LogROL>> callback)
        {
            DataPortal.BeginFetch<LogROL>(date, callback);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogROL"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private LogROL()
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
        /// Loads a <see cref="LogROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="date">The Date.</param>
        protected void DataPortal_Fetch(SmartDate date)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetLogROL", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Date", date.DBValue).DbType = DbType.DateTime;
                    var args = new DataPortalHookArgs(cmd, date);
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
        /// Loads all <see cref="LogROL"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Fetch(SafeDataReader dr)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            while (dr.Read())
            {
                Add(DataPortal.FetchChild<LogRO>(dr));
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
