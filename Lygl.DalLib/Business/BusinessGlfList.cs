using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Lygl.DalLib.Business
{
    public partial class BusinessGlfList
    {
        /// <summary>
        /// Factory method. Loads a <see cref="BusinessGlfList"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the BusinessGlfList to fetch.</param>
        /// <returns>A reference to the fetched <see cref="BusinessGlfList"/> object.</returns>
        public static BusinessGlfList GetBusinessGlfListByDate(DateTime dqDate)
        {
            return DataPortal.Fetch<BusinessGlfList>(dqDate);
        }
        /// <summary>
        /// Loads a <see cref="BusinessGlfList"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The Mx ID.</param>
        protected void DataPortal_Fetch(DateTime dqDate)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetGlfListByDate", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DqDate", dqDate).DbType = DbType.DateTime;
                    var args = new DataPortalHookArgs(cmd, dqDate);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }
        #region OnDeserialized actions

        /*/// <summary>
        /// This method is called on a newly deserialized object
        /// after deserialization is complete.
        /// </summary>
        protected override void OnDeserialized()
        {
            base.OnDeserialized();
            // add your custom OnDeserialized actions here.
        }*/

        #endregion

        #region Pseudo Event Handlers

        //partial void OnFetchPre(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        //partial void OnFetchPost(DataPortalHookArgs args)
        //{
        //    throw new System.Exception("The method or operation is not implemented.");
        //}

        #endregion

    }
}
