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
    public partial class StatisticSrList : ReadOnlyBindingList<StatisticSrItem>,  IBusinessObject
    {

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="StatisticSrList"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="StatisticSrList"/> object.</returns>
        public static StatisticSrList GetStatisticSrList(DateTime startDate, DateTime endDate)
        {
            //if (_list == null)
            //    _list = DataPortal.Fetch<StatisticSrList>(new CriteriaGetByStartEndDate( startDate, endDate));

            //return _list;
            return DataPortal.Fetch<StatisticSrList>(new CriteriaGetByUserIDStartEndDate(startDate, endDate)); 
        }

        public static StatisticSrList GetStatisticSrList(string userID, DateTime startDate, DateTime endDate)
        {
            //if (_list == null)
            //    _list = DataPortal.Fetch<StatisticBusinessList>(new CriteriaGetByUserIDStartEndDate( startDate, endDate));

            //return _list;
            return DataPortal.Fetch<StatisticSrList>(new CriteriaGetByUserIDStartEndDate(startDate, endDate, userID));
        }
#endif

        ///// <summary>
        ///// Factory method. Asynchronously loads a <see cref="StatisticSrList"/> object.
        ///// </summary>
        ///// <param name="callback">The completion callback method.</param>
        //public static void StatisticSrList(EventHandler<DataPortalResult<StatisticSrList>> callback)
        //{
        //    if (_list == null)
        //        DataPortal.BeginFetch<StatisticSrList>((o, e) =>
        //            {
        //                _list = e.Object;
        //                callback(o, e);
        //            });
        //    else
        //        callback(null, new DataPortalResult<StatisticSrList>(_list, null, null));
        //}

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticSrList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public StatisticSrList()
#else
        private StatisticSrList()
#endif
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="StatisticSrList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch(CriteriaGetByUserIDStartEndDate crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetStatisticSrList", ctx.Connection))
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
                    Add(new StatisticSrItem(
                            dr.GetString("Name"),
                          dr.GetInt32("Num"),
                          dr.GetDecimal("SubTotal")
                            )
                        );
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

    #region   StatisticSrItem

    [Serializable]
    public class StatisticSrItem : MobileObject
    {
             
        private string _name;
        public string Name
        {
            get { return _name; }
        }
        private int _num;
        public int Num
        {
            get { return _num; }
        }
        private decimal _subTotal;
        public decimal SubTotal
        {
            get { return _subTotal; }
        }

        public StatisticSrItem(string name,int num,decimal subTotal)
        {
            _name = name;
            _num = num;
            _subTotal = subTotal;
         }
               /// <summary>
          /// Returns a string representation of the
          /// value for this item.
          /// </summary>
          public override string ToString()
          {
              return _name;
          }

          /// <summary>
          /// Override this method to manually get custom field
          /// values from the serialization stream.
          /// </summary>
          /// <param name="info">Serialization info.</param>
          /// <param name="mode">Serialization mode.</param>
          //protected override void OnGetState(SerializationInfo info, StateMode mode)
          //{
          //  base.OnGetState(info, mode);
          //  //info.AddValue("NameValuePair._key", _key);
          //  //info.AddValue("NameValuePair._value", _value);
          //}

          /// <summary>
          /// Override this method to manually set custom field
          /// values into the serialization stream.
          /// </summary>
          /// <param name="info">Serialization info.</param>
          /// <param name="mode">Serialization mode.</param>
          //protected override void OnSetState(SerializationInfo info, StateMode mode)
          //{
          //  base.OnSetState(info, mode);
          //  //_key = info.GetValue<K>("NameValuePair._key");
          //  //_value = info.GetValue<V>("NameValuePair._value");
          //}
         }
#endregion 


}
