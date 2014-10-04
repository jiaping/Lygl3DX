using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.UserManager.OrgUserTree
{

    /// <summary>
    /// UserNodeInfo (read only object).<br/>
    /// This is a generated base class of <see cref="UserNodeInfo"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="UserROL"/> collection.
    /// </remarks>
    [Serializable]
    public partial class UserNodeInfo : ReadOnlyBase<UserNodeInfo>, IOrgUserTreeNodeInfo
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
        /// Gets the User ID.
        /// </summary>
        /// <value>The User ID.</value>
        public Guid UserID { get; private set; }

        /// <summary>
        /// Gets the Org ID.
        /// </summary>
        /// <value>The Org ID.</value>
        public Guid OrgID { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="UserNodeInfo"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private UserNodeInfo()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="UserNodeInfo"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            Code = dr.GetString("Code");
            Name = dr.GetString("Name");
            UserID = dr.GetGuid("UserID");
            OrgID = dr.GetGuid("OrgID");
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        #endregion

        #region Pseudo Events

        /// <summary>
        /// Occurs after the low level fetch operation, before the data reader is destroyed.
        /// </summary>
        partial void OnFetchRead(DataPortalHookArgs args);

        #endregion

    }
}
