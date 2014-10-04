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
    public partial class SeekGlfList : ReadOnlyBindingList<SeekGlfItem>,  IBusinessObject
    {

        #region Factory Methods

#if !SILVERLIGHT

        /// <summary>
        /// Factory method. Loads a <see cref="SeekGlfList"/> object.
        /// </summary>
        /// <returns>A reference to the fetched <see cref="SeekGlfList"/> object.</returns>
        public static SeekGlfList GetSeekGlfList(DateTime startDate, DateTime endDate)
        {
            //if (_list == null)
            //    _list = DataPortal.Fetch<SeekGlfList>(new CriteriaGetByStartEndDate( startDate, endDate));

            //return _list;
            return DataPortal.Fetch<SeekGlfList>(new CriteriaGetByStartEndDate(startDate, endDate)); 
        }
#endif

        ///// <summary>
        ///// Factory method. Asynchronously loads a <see cref="SeekGlfList"/> object.
        ///// </summary>
        ///// <param name="callback">The completion callback method.</param>
        //public static void SeekGlfList(EventHandler<DataPortalResult<SeekGlfList>> callback)
        //{
        //    if (_list == null)
        //        DataPortal.BeginFetch<SeekGlfList>((o, e) =>
        //            {
        //                _list = e.Object;
        //                callback(o, e);
        //            });
        //    else
        //        callback(null, new DataPortalResult<SeekGlfList>(_list, null, null));
        //}

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeekGlfList"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
#if SILVERLIGHT
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public SeekGlfList()
#else
        private SeekGlfList()
#endif
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

#if !SILVERLIGHT

        /// <summary>
        /// Loads a <see cref="SeekGlfList"/> collection from the database.
        /// </summary>
        protected void DataPortal_Fetch(CriteriaGetByStartEndDate crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetSeekGlfList", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDate", crit.StartDate).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@EndDate", crit.EndDate).DbType = DbType.DateTime;
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
                    Add(new SeekGlfItem(
                            dr.GetGuid("MxID"),
                            dr.GetString("MxName"),
                            dr.GetDateTime("StartDate"),
                            dr.GetDateTime("EndDate"),
                             dr.GetGuid("BusinessID")
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

    #region   SeekGlfItem

    [Serializable]
    public class SeekGlfItem : MobileObject
    {
        private Guid _businessID;
        public Guid BusinessID 
        {
            get { return _businessID; }
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
        private DateTime _startDate;
        public DateTime StartDate
        { 
            get {return _startDate;}
        }
        private DateTime _endDate;
        public DateTime EndDate
        {
            get {return _endDate;}
        }

        public SeekGlfItem(Guid mxID,string mxName,DateTime startDate,DateTime endDate,Guid businessID)
        { 
            _mxID = mxID;
            _mxName = mxName;
            _startDate= startDate;
            _endDate=endDate;
            _businessID = businessID;
         }
               /// <summary>
          /// Returns a string representation of the
          /// value for this item.
          /// </summary>
          public override string ToString()
          {
            //return _value.ToString();
              return _mxName;
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

#region  criteria userID Start-end date
    /// <summary>
    /// CriteriaGetByMxName criteria.
    /// </summary>
    [Serializable]
    public class CriteriaGetByStartEndDate : CriteriaBase<CriteriaGetByStartEndDate>
    {
        public static readonly PropertyInfo<DateTime> StartDateProperty = RegisterProperty<DateTime>(p => p.StartDate);
        public DateTime StartDate
        {
            get { return ReadProperty(StartDateProperty); }
            private set { LoadProperty(StartDateProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> EndDateProperty = RegisterProperty<DateTime>(p => p.EndDate);
        public DateTime EndDate
        {
            get { return ReadProperty(EndDateProperty); }
            private set { LoadProperty(EndDateProperty, value); }
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="CriteriaGetByMxName"/> class.
        /// </summary>
        /// <param name="mxName">The MxName.</param>
        public CriteriaGetByStartEndDate(DateTime startDate,DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is CriteriaGetByStartEndDate)
            {
                var c = (CriteriaGetByStartEndDate)obj;
                if (StartDate.Equals(c.StartDate) && EndDate.Equals(c.EndDate))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>An hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return string.Concat("CriteriaGetByStartEndDate", StartDate.ToString(),EndDate.ToString()).GetHashCode();
        }
    }
#endregion 
}
