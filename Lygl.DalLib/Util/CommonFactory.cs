using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Lygl.DalLib.Edit;
using Lygl.DalLib.NVL;
using Lygl.DalLib.Browse;
using Lygl.DalLib.Business;
using System.Data.SqlClient;
using System.Data;

namespace Lygl.DalLib.Util
{
    /// <summary>
    /// 实现通用的对象工厂方法，由GenFork工具生成的Clsa对象类中，特别是child对象，没有public的工厂方法
    /// 本类就是为此面建立的，以帮助这些类
    /// </summary>
    public static class CommonFactory
    {
        /// <summary>
        /// 通用的创建对象的工厂方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateNew<T>()
        {
            return DataPortal.Create<T>();
        }

        public static void UpdateMxStatus(MxRO mx)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                    using (var cmd = new SqlCommand("dbo.UpdateMxStatus", ctx.Connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MxID", mx.MxID.ToString()).DbType = DbType.String;
                        cmd.ExecuteNonQuery();
                    }
            }
        }

        //public static void UpdateMxStatus(MxRO mx, string currentOperate)
        //{
        //    int newMxStatusValue = -1;

        //    switch (currentOperate)
        //    {
        //        case "预定":
        //            newMxStatusValue = 1;
        //            break;
        //        case "购买":
        //            newMxStatusValue = 2;
        //            break;
        //        case "预约安葬":
        //            if (mx.MxStatusID == 4) newMxStatusValue = 5; else newMxStatusValue = 3;
        //            break;
        //        case "预约立碑":
        //            if (mx.MxStatusID == 3) newMxStatusValue = 5; else newMxStatusValue = 4;
        //            break;
        //        case "安葬":
        //            if (mx.MxStatusID == 4) break;
        //            MxSzEditList szList1 = MxSzEditList.GetMxSzEditListByMxID(mx.MxID);
        //            int maxXs1 = MxXsNVL.GetMxXsNVL().Value(mx.MxTypeID);
        //            BusinessLb lb = BusinessLb.GetBusinessLbByMxID(mx.MxID);
        //            bool ylb = lb.BusinessID == Guid.Empty ? false : true;
        //            if (ylb)
        //            {
        //                if (szList1.Count == maxXs1)
        //                    newMxStatusValue = 49;
        //                else
        //                {
        //                    if (szList1.Count == 0) newMxStatusValue = 40;
        //                    if (szList1.Count == 1) newMxStatusValue = 41;
        //                    if (szList1.Count == 2) newMxStatusValue = 42;
        //                    if (szList1.Count == 3) newMxStatusValue = 43;
        //                }
        //            }
        //            else
        //            {
        //                if (szList1.Count == maxXs1)
        //                    newMxStatusValue = 39;
        //                else
        //                {
        //                    if (szList1.Count == 1) newMxStatusValue = 31;
        //                    if (szList1.Count == 2) newMxStatusValue = 32;
        //                    if (szList1.Count == 3) newMxStatusValue = 33;
        //                }
        //            }

        //            break;
        //        case "立碑":
        //            if (mx.MxStatusID == 3) break;
        //            MxSzEditList szList = MxSzEditList.GetMxSzEditListByMxID(mx.MxID);
        //            int maxXs = MxXsNVL.GetMxXsNVL().Value(mx.MxTypeID);
        //            if (szList.Count == maxXs)
        //                newMxStatusValue = 49;
        //            else
        //            {
        //                if (szList.Count == 1) newMxStatusValue = 41;
        //                if (szList.Count == 2) newMxStatusValue = 42;
        //                if (szList.Count == 3) newMxStatusValue = 43;
        //            }
        //            break;

        //        default:
        //            break;
        //    }

        //    if (newMxStatusValue >= 0)
        //    {
        //        MxEdit mxedit = MxEdit.GetMxEdit(mx.MxID);
        //        mxedit.MxStatus = MxStatusNVL.GetMxStatusNVL().Value(newMxStatusValue);
        //        mxedit.Save();
        //    }
        //    else
        //    {
        //        throw new Exception("因为状态不正确，所以没有更新！");
        //    }
        //}



        #region Test DB connection

        public static bool TestDBConn()
        {
            bool bret = true;
            try
            {
                using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
                {
                    //string exsql = "SELECT count(*) as paramcount FROM ts_SysParam";
                    //using (var cmd = new SqlCommand(exsql, ctx.Connection))
                    //{
                    //    cmd.CommandType = CommandType.Text;
                    //    try
                    //    {
                    //        IDataReader reader = cmd.ExecuteReader();
                    //        reader.Read();
                    //    }
                    //    catch (Exception)
                    //    {
                    //        bret = false;
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                bret = false;
            }
            
            return bret;
        }

        #endregion
    }
}
