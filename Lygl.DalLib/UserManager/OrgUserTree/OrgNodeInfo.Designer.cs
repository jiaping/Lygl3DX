using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager.OrgUserTree
{

    /// <summary>
    /// OrgNodeInfo (read only object).<br/>
    /// This is a generated base class of <see cref="OrgNodeInfo"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="OrgROL"/> collection.
    /// </remarks>
    [Serializable]
    public partial class OrgNodeInfo : ReadOnlyBase<OrgNodeInfo>, IOrgUserTreeNodeInfo
    {

        #region Business Properties

        /// <summary>
        /// Gets the Code.
        /// </summary>
        /// <value>The Code.</value>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Order No.
        /// </summary>
        /// <value>The Order No.</value>
        public int? OrderNo { get; private set; }

        /// <summary>
        /// Gets the Org ID.
        /// </summary>
        /// <value>The Org ID.</value>
        public Guid OrgID { get; private set; }

        /// <summary>
        /// Gets the PID.
        /// </summary>
        /// <value>The PID.</value>
        public Guid? PID { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrgNodeInfo"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private OrgNodeInfo()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="OrgNodeInfo"/> object from the database, based on given criteria.
        /// </summary>
        /// <param name="orgID">The Org ID.</param>
        protected void Child_Fetch(Guid orgID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetOrgNodeInfo", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrgID", orgID).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, orgID);
                    OnFetchPre(args);
                    Fetch(cmd);
                    OnFetchPost(args);
                }
            }
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        private void Fetch(SqlCommand cmd)
        {
            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
            {
                if (dr.Read())
                {
                    Fetch(dr);
                }
            }
        }

        /// <summary>
        /// Loads a <see cref="OrgNodeInfo"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            Code = dr.GetString("Code");
            Name = dr.GetString("Name");
            OrderNo = (int?)dr.GetValue("OrderNo");
            OrgID = dr.GetGuid("OrgID");
            PID = (Guid?)dr.GetValue("PID");
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
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

        /// <summary>
        /// Occurs after the low level fetch operation, before the data reader is destroyed.
        /// </summary>
        partial void OnFetchRead(DataPortalHookArgs args);

        #endregion

    }
}
