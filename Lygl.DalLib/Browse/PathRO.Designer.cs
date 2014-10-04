using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Browse
{

    /// <summary>
    /// PathRO (read only object).<br/>
    /// This is a generated base class of <see cref="PathRO"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="PathROL"/> collection.
    /// </remarks>
    [Serializable]
    public partial class PathRO : ReadOnlyBase<PathRO>
    {

        #region Business Properties

        /// <summary>
        /// Gets the Path ID.
        /// </summary>
        /// <value>The Path ID.</value>
        public Guid PathID { get; private set; }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Geometry Text.
        /// </summary>
        /// <value>The Geometry Text.</value>
        public string GeometryText { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PathRO"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private PathRO()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="PathRO"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            PathID = dr.GetGuid("PathID");
            Name = dr.GetString("Name");
            GeometryText = dr.GetString("GeometryText");
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
