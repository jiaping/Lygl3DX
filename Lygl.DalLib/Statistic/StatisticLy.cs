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
    public partial class StatisticLy:ReadOnlyBase<StatisticLy>
    {

        public static readonly PropertyInfo<int> TotalMqNumProperty = RegisterProperty<int>(p => p.TotalMqNum, "Ĺ����");

        public int TotalMqNum
        {
            get { return GetProperty(TotalMqNumProperty); }
        }

        public static readonly PropertyInfo<int> TotalMxNumProperty = RegisterProperty<int>(p => p.TotalMxNum, "Ĺ����");

        public int TotalMxNum
        {
            get { return GetProperty(TotalMxNumProperty); }
        }

        public static readonly PropertyInfo<int> MqMxNumProperty = RegisterProperty<int>(p => p.MqMxNum, "Ĺ��ĹѨ��");
        public int MqMxNum
        {
            get { return GetProperty(MqMxNumProperty); }
        }
        public static readonly PropertyInfo<int> MqDsMxNumProperty = RegisterProperty<int>(p => p.MqDsMxNum, "Ĺ������ĹѨ��");
        public int MqDsMxNum
        {
            get { return GetProperty(MqDsMxNumProperty); }
        }
        public static readonly PropertyInfo<int> MqYdMxNumProperty = RegisterProperty<int>(p => p.MqYdMxNum, "Ĺ����Ԥ��ĹѨ��");
        public int MqYdMxNum
        {
            get { return GetProperty(MqYdMxNumProperty); }
        }
        public static readonly PropertyInfo<int> MqYsMxNumProperty = RegisterProperty<int>(p => p.MqYsMxNum, "Ĺ���ѳ���ĹѨ��");
        public int MqYsMxNum
        {
            get { return GetProperty(MqYsMxNumProperty); }
        }


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
        protected void DataPortal_Fetch()
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetLyInfo", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
                    cmd.Parameters.Add("@TotalMqNum",SqlDbType.Int).Direction= ParameterDirection.Output;
                    cmd.Parameters.Add("@TotalMxNum",SqlDbType.Int).Direction= ParameterDirection.Output;
                    //cmd.Parameters.AddWithValue("@TotalMqNum", mxID).DbType = DbType.Guid;
                    //cmd.Parameters.AddWithValue("@TotalMxNum", mxID).DbType = DbType.Guid;
                    LoadCollection(cmd);
                }
            }
        }
        private void LoadCollection(SqlCommand cmd)
        {
            
                var c = cmd.ExecuteNonQuery();
                    LoadProperty(TotalMqNumProperty, (int)cmd.Parameters[0].Value);
                    LoadProperty(TotalMxNumProperty, (int)cmd.Parameters[1].Value);
           
            
            //using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            //{
            //    Fetch(dr);
            //}
        }
        #region ��ȡĹ����Ϣ
        protected void DataPortal_Fetch(Guid mqID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMqInfo", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var args = new DataPortalHookArgs(cmd);
                    cmd.Parameters.AddWithValue("@MqID", mqID).DbType = DbType.Guid;
                    cmd.Parameters.Add("@MqMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MqDsMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MqYdMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@MqYsMxNum", SqlDbType.Int).Direction = ParameterDirection.Output;
                    LoadMqInfoCollection(cmd);
                }
            }
        }
        private void LoadMqInfoCollection(SqlCommand cmd)
        {

            var c = cmd.ExecuteNonQuery();
            LoadProperty(MqMxNumProperty, (int)cmd.Parameters[1].Value);
            LoadProperty(MqDsMxNumProperty, (int)cmd.Parameters[2].Value);
            LoadProperty(MqYdMxNumProperty, (int)cmd.Parameters[3].Value);
            LoadProperty(MqYsMxNumProperty, (int)cmd.Parameters[4].Value);
            //LoadProperty(TotalMxNumProperty, (int)cmd.Parameters[1].Value);


            //using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            //{
            //    Fetch(dr);
            //}
        }
        #endregion

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
