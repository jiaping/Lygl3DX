using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Csla.Data;
using System.Data;

namespace Lygl.DalLib.DBCommand
{
    [Serializable]
    public class UpdateAllMxStatusCommand : Csla.CommandBase<UpdateAllMxStatusCommand>
    {
        protected override void DataPortal_Execute()
        {
            //using (var db = DBHelper.CreateDb(ConnectionStringNames.OpenExpressApp))
            //{
            //    var sql = "DELETE FROM AUDITITEM";
            //    db.Exec("sys.sp_sqlexec", new object[] { sql });
            //}
            //using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            
            {
                using (var cmd = new SqlCommand("exec dbo.UpdateAllMxStatus", ctx.Connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }
    }
}
