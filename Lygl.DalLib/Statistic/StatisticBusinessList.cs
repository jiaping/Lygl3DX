#if !SILVERLIGHT
using Lygl.DalLib.Util;
using Lygl.DalLib.Core;
using Csla;
using System.Data.SqlClient;
using Csla.Data;
using System.Data;
using System;
using Csla.Core;
using Csla.Serialization.Mobile;
#endif

namespace Lygl.DalLib.Statistic
{
    [Serializable]
    public partial class StatisticBusinessList : ReadOnlyBindingList<StatisticBusinessItem>,  IBusinessObject
    {

         #region Private Fields

        private static StatisticBusinessList _list;

        #endregion

        #region Cache Management Methods

        /// <summary>
        /// Clears the in-memory StatisticBusinessList cache so it is reloaded on the next request.
        /// </summary>
        public static void InvalidateCache()
        {
            _list = null;
        }

        /// <summary>
        /// Used by async loaders to load the cache.
        /// </summary>
        /// <param name="list">The list to cache.</param>
        internal static void SetCache(StatisticBusinessList list)
        {
            _list = list;
        }

        internal static bool IsCached
        {
            get { return _list != null; }
        }

        #endregion

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="StatisticBusinessList"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="StatisticBusinessList"/> object.</returns>
        public static StatisticBusinessList GetStatisticBusinessList(DateTime startDate, DateTime endDate)
        {
            //if (_list == null)
            //    _list = DataPortal.Fetch<StatisticBusinessList>(new CriteriaGetByUserIDStartEndDate( startDate, endDate));

            //return _list;
            return DataPortal.Fetch<StatisticBusinessList>(new CriteriaGetByUserIDStartEndDate(startDate, endDate)); 
        }
        public static StatisticBusinessList GetStatisticBusinessList(string userID,DateTime startDate, DateTime endDate)
        {
            //if (_list == null)
            //    _list = DataPortal.Fetch<StatisticBusinessList>(new CriteriaGetByUserIDStartEndDate( startDate, endDate));

            //return _list;
            return DataPortal.Fetch<StatisticBusinessList>(new CriteriaGetByUserIDStartEndDate(startDate, endDate, userID));
        }
#endif

        ///// <summary>
        ///// Factory method. Asynchronously loads a <see cref="StatisticBusinessList"/> object.
        ///// </summary>
        ///// <param name="callback">The completion callback method.</param>
        //public static void StatisticBusinessList(EventHandler<DataPortalResult<StatisticBusinessList>> callback)
        //{
        //    if (_list == null)
        //        DataPortal.BeginFetch<StatisticBusinessList>((o, e) =>
        //            {
        //                _list = e.Object;
        //                callback(o, e);
        //            });
        //    else
        //        callback(null, new DataPortalResult<StatisticBusinessList>(_list, null, null));
        //}

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticBusinessList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public StatisticBusinessList()
#else
        private StatisticBusinessList()
#endif
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="StatisticBusinessList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch(CriteriaGetByUserIDStartEndDate crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetStatisticBusinessList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserID", crit.UserID).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@StartDate", crit.StartDate).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@EndDate", crit.EndDate).DbType = DbType.DateTime;
                    //cmd.Parameters.AddWithValue("@DqDate", SeekText).DbType = DbType.DateTime;
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
                    Add(new StatisticBusinessItem(
                        dr.GetGuid("BusinessID"),
                        dr.GetString("BusinessName"),
                        dr.GetGuid("MxID"),
                        dr.GetString("MxName")
                        ));
                }
            }
            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

#endif

        #endregion

        #region Pseudo Events

#if !SILVERLIGHT

        /// <summary>
        /// Occurs after setting query parameters and before the fetch operation.
        /// </summary>
        partial void OnFetchPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs after the fetch operation (object or collection is fully loaded and set up).
        /// </summary>
        partial void OnFetchPost(DataPortalHookArgs args);

#endif

        #endregion

    }

    #region   statisticBusinessItem

    [Serializable]
    public class StatisticBusinessItem : MobileObject
        {
      private Guid _businessID;
      public Guid BusinessID 
      {
        get { return _businessID; }
      }

      private string _businessName;
         public string  BusinessName 
      {
        get { return _businessName; }
      }

         private string _mxName;
         public string MxName
         {
             get { return _mxName; }
         }
         private Guid _mxID;
         public Guid MxID
         {
             get { return _mxID; }
         }

      /// <summary>
      /// Creates an instance of the object.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="value">The value.</param>
         public StatisticBusinessItem(Guid id, string name,Guid mxID,string mxName)
      {
          _businessID = id;
          _businessName = name;
          _mxID = mxID;
          _mxName = mxName;
      }
           /// <summary>
      /// Returns a string representation of the
      /// value for this item.
      /// </summary>
      public override string ToString()
      {
        //return _value.ToString();
          return _businessName;
      }

      /// <summary>
      /// Override this method to manually get custom field
      /// values from the serialization stream.
      /// </summary>
      /// <param name="info">Serialization info.</param>
      /// <param name="mode">Serialization mode.</param>
      protected override void OnGetState(SerializationInfo info, StateMode mode)
      {
        base.OnGetState(info, mode);
        //info.AddValue("NameValuePair._key", _key);
        //info.AddValue("NameValuePair._value", _value);
      }

      /// <summary>
      /// Override this method to manually set custom field
      /// values into the serialization stream.
      /// </summary>
      /// <param name="info">Serialization info.</param>
      /// <param name="mode">Serialization mode.</param>
      protected override void OnSetState(SerializationInfo info, StateMode mode)
      {
        base.OnSetState(info, mode);
        //_key = info.GetValue<K>("NameValuePair._key");
        //_value = info.GetValue<V>("NameValuePair._value");
      }
     }
#endregion 
}
