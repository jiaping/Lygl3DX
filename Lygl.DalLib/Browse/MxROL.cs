using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Lygl.DalLib.Browse
{
    public partial class MxROL
    {
        public void Add(string mxID)
        {
            MxRO mxRO = MxROL.GetMxROLByMxID(mxID).First();
            try
            {
                IsReadOnly = false;
                Add(mxRO);
            }
            finally
            {
                IsReadOnly = true;
            }
        }

        public bool Remove(string mxID)
        {
            MxRO mxRO = MxROL.GetMxROLByMxID(mxID).First();
            try
            {
                IsReadOnly = false;
                return Remove(mxRO);
            }
            finally
            {
                IsReadOnly = true;
            }
        }
        /// Loads <see cref="MxROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="mxID">The fetch criteria.</param>
        protected void DataPortal_Fetch(string mxID)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager(Database.LyglDBConnection, false))
            {
                using (var cmd = new SqlCommand("dbo.GetMxROLItem", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxID", new Guid(mxID)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd, mxID);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }
        /// <summary>
        /// Factory method. Asynchronously loads an existing <see cref="MxROL"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the MxROL to fetch.</param>
        /// <param name="callback">The completion callback method.</param>
        public static void GetMxROLByMxID(string mxID, EventHandler<DataPortalResult<MxROL>> callback)
        {
            DataPortal.BeginFetch<MxROL>(mxID, callback);
        }
        /// <summary>
        /// Factory method. Loads an existing <see cref="MxROL"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxID">The MxID parameter of the MxROL to fetch.</param>
        /// <returns>A reference to the fetched <see cref="MxROL"/> object.</returns>
        public static MxROL GetMxROLByMxID(string mxID)
        {
            return DataPortal.Fetch<MxROL>(mxID);
        }


        #region Criteria for GetByMxName

        /// <summary>
        /// CriteriaGetByMxName criteria.
        /// </summary>
        [Serializable]
        public class CriteriaGetByMxName : CriteriaBase<CriteriaGetByMxName>
        {

            /// <summary>
            /// Maintains metadata about <see cref="MxName"/> property.
            /// </summary>
            public static readonly PropertyInfo<string> MxNameProperty = RegisterProperty<string>(p => p.MxName);
            /// <summary>
            /// Gets the Mx Name.
            /// </summary>
            /// <value>The Mx Name.</value>
            public string MxName
            {
                get { return ReadProperty(MxNameProperty); }
                private set { LoadProperty(MxNameProperty, value); }
            }



            /// <summary>
            /// Initializes a new instance of the <see cref="CriteriaGetByMxName"/> class.
            /// </summary>
            /// <param name="mxName">The MxName.</param>
            public CriteriaGetByMxName(string mxName)
            {
                MxName = mxName;
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.</returns>
            public override bool Equals(object obj)
            {
                if (obj is CriteriaGetByMxName)
                {
                    var c = (CriteriaGetByMxName)obj;
                    if (!MxName.Equals(c.MxName))
                        return false;
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
                return string.Concat("CriteriaGetByMxName", MxName.ToString()).GetHashCode();
            }
        }


        /// <summary>
        /// Factory method. Loads a <see cref="MxROL"/> object, based on given parameters.
        /// </summary>
        /// <param name="mxName">The MxName parameter of the MxROL to fetch.</param>
        /// <returns>A reference to the fetched <see cref="MxROL"/> object.</returns>
        public static MxROL GetMxROLByMxName(string mxName)
        {
            return DataPortal.Fetch<MxROL>(new CriteriaGetByMxName(mxName));
        }
        /// <summary>
        /// Loads a <see cref="MxROL"/> collection from the database, based on given criteria.
        /// </summary>
        /// <param name="crit">The fetch criteria.</param>
        protected void DataPortal_Fetch(CriteriaGetByMxName crit)
        {
            using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.GetMxROLByMxName", ctx.Connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MxName", crit.MxName).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd, crit);
                    OnFetchPre(args);
                    LoadCollection(cmd);
                    OnFetchPost(args);
                }
            }
        }
        #endregion
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
