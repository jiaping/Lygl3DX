#if !SILVERLIGHT
using Lygl.DalLib.Util;
using Lygl.DalLib.Core;
using Csla;
using System.Data.SqlClient;
using Csla.Data;
using System.Data;
using System;
#endif

namespace Lygl.DalLib.Statistic
{
    public partial class SeekGlfDq : ReadOnlyBase<SeekGlfDq>
    {

        public SeekGlfDq(Guid mxID,string mxName,DateTime startDate,DateTime endDate)
        {
            LoadProperty(MxIDProperty, mxID);
            LoadProperty(MxNameProperty, mxName);
            LoadProperty(StartDateProperty, startDate);
            LoadProperty(EndDateProperty, endDate);
            //LoadProperty(TotalMxNumProperty, (int)cmd.Parameters[1].Value);
        }
        #region Business Properties

        public static readonly PropertyInfo<string> MxNameProperty = RegisterProperty<string>(p => p.MxName, "墓穴名");
        public string MxName
        {
            get { return GetProperty(MxNameProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="BusinessName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> BusinessNameProperty = RegisterProperty<string>(p => p.BusinessName, "Business Name");
        /// <summary>
        /// Gets or sets the Business Name.
        /// </summary>
        /// <value>The Business Name.</value>
        public string BusinessName
        {
            get { return GetProperty(BusinessNameProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID, "Mx ID");
        /// <summary>
        /// Gets or sets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid MxID
        {
            get { return GetProperty(MxIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Price"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal> PriceProperty = RegisterProperty<Decimal>(p => p.Price, "Price");
        /// <summary>
        /// Gets or sets the Price.
        /// </summary>
        /// <value>The Price.</value>
        public Decimal Price
        {
            get { return GetProperty(PriceProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="StartDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> StartDateProperty = RegisterProperty<SmartDate>(p => p.StartDate, "Start Date");
        /// <summary>
        /// Gets or sets the Start Date.
        /// </summary>
        /// <value>The Start Date.</value>
        public string StartDate
        {
            get { return GetPropertyConvert<SmartDate, String>(StartDateProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="EndDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> EndDateProperty = RegisterProperty<SmartDate>(p => p.EndDate, "End Date");
        /// <summary>
        /// Gets or sets the End Date.
        /// </summary>
        /// <value>The End Date.</value>
        public string EndDate
        {
            get { return GetPropertyConvert<SmartDate, String>(EndDateProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Drawee"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> DraweeProperty = RegisterProperty<string>(p => p.Drawee, "Drawee");
        /// <summary>
        /// Gets or sets the Drawee.
        /// </summary>
        /// <value>The Drawee.</value>
        public string Drawee
        {
            get { return GetProperty(DraweeProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="OperatorID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> OperatorIDProperty = RegisterProperty<Guid>(p => p.OperatorID, "Operator ID");
        /// <summary>
        /// Gets or sets the Operator ID.
        /// </summary>
        /// <value>The Operator ID.</value>
        public Guid OperatorID
        {
            get { return GetProperty(OperatorIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="PayFlag"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool> PayFlagProperty = RegisterProperty<bool>(p => p.PayFlag, "Pay Flag");
        /// <summary>
        /// Gets or sets the Pay Flag.
        /// </summary>
        /// <value><c>true</c> if Pay Flag; otherwise, <c>false</c>.</value>
        public bool PayFlag
        {
            get { return GetProperty(PayFlagProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="OperateTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> OperateTimeProperty = RegisterProperty<SmartDate>(p => p.OperateTime, "Operate Time");
        /// <summary>
        /// Gets or sets the Operate Time.
        /// </summary>
        /// <value>The Operate Time.</value>
        public string OperateTime
        {
            get { return GetPropertyConvert<SmartDate, String>(OperateTimeProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="OperatorCode"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> OperatorCodeProperty = RegisterProperty<string>(p => p.OperatorCode, "Operator Code");
        /// <summary>
        /// Gets or sets the Operator Code.
        /// </summary>
        /// <value>The Operator Code.</value>
        public string OperatorCode
        {
            get { return GetProperty(OperatorCodeProperty); }
        }

        #endregion


        public static StatisticLy GetStatisticLy()
        {
            return DataPortal.Fetch<StatisticLy>();
        }

        public static StatisticLy GetMqInfo(Guid mqID)
        {
            return DataPortal.Fetch<StatisticLy>(mqID);
        }

        /// <summary>
        /// Loads a <see cref="MxROL"/> collection from the database.
        /// </summary>
        //protected void DataPortal_Fetch()
        //{
        //    using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
        //    {
        //        using (var cmd = new SqlCommand("dbo.GetLyInfo", ctx.Connection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            var args = new DataPortalHookArgs(cmd);
        //            cmd.Parameters.Add("@TotalMqNum",SqlDbType.Int).Direction= ParameterDirection.Output;
        //            cmd.Parameters.Add("@TotalMxNum",SqlDbType.Int).Direction= ParameterDirection.Output;
        //            //cmd.Parameters.AddWithValue("@TotalMqNum", mxID).DbType = DbType.Guid;
        //            //cmd.Parameters.AddWithValue("@TotalMxNum", mxID).DbType = DbType.Guid;
        //            LoadCollection(cmd);
        //        }
        //    }
        //}
        //private void LoadCollection(SqlCommand cmd)
        //{
            
        //        var c = cmd.ExecuteNonQuery();
        //            LoadProperty(TotalMqNumProperty, (int)cmd.Parameters[0].Value);
        //            LoadProperty(TotalMxNumProperty, (int)cmd.Parameters[1].Value);
           
            
        //    //using (var dr = new SafeDataReader(cmd.ExecuteReader()))
        //    //{
        //    //    Fetch(dr);
        //    //}
        //}
        //#region 获取墓区信息
        //protected void DataPortal_Fetch(Guid mqID)
        //{
        //    using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
        //    {
        //        using (var cmd = new SqlCommand("dbo.GetMqInfo", ctx.Connection))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            var args = new DataPortalHookArgs(cmd);
        //            cmd.Parameters.AddWithValue("@MqID", mqID).DbType = DbType.Guid;
        //            cmd.Parameters.Add("@MqMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("@MqDsMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("@MqYdMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("@MqYsMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            LoadMqInfoCollection(cmd);
        //        }
        //    }
        //}
        //private void LoadMqInfoCollection(SqlCommand cmd)
        //{

        //    var c = cmd.ExecuteNonQuery();
        //    LoadProperty(MqMxNumProperty, (int)cmd.Parameters[1].Value);
        //    LoadProperty(MqDsMxNumProperty, (int)cmd.Parameters[2].Value);
        //    LoadProperty(MqYdMxNumProperty, (int)cmd.Parameters[3].Value);
        //    LoadProperty(MqYsMxNumProperty, (int)cmd.Parameters[4].Value);
        //    //LoadProperty(TotalMxNumProperty, (int)cmd.Parameters[1].Value);


        //    //using (var dr = new SafeDataReader(cmd.ExecuteReader()))
        //    //{
        //    //    Fetch(dr);
        //    //}
        //}
        //#endregion

        /// <summary>
        /// Loads all <see cref="MxROL"/> collection items from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        //private void Fetch(SafeDataReader dr)
        //{
        //    dr.Read();
        //    LoadProperty(TotalMqNumProperty, dr.GetInt32("TotalMqNum"));
        //    LoadProperty(TotalMxNumProperty, dr.GetInt32("TotalMxNum"));
        //    //IsReadOnly = false;
        //    //var rlce = RaiseListChangedEvents;
        //    //RaiseListChangedEvents = false;
        //    ////while (dr.Read())
        //    ////{
        //    ////    Add(DataPortal.FetchChild<MxRO>(dr));
        //    ////}
        //    //RaiseListChangedEvents = rlce;
        //    //IsReadOnly = true;
        //}

       
    }
}
