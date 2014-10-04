using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Browse
{

    /// <summary>
    /// AreaRO (read only object).<br/>
    /// This is a generated base class of <see cref="AreaRO"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="AreaROL"/> collection.
    /// </remarks>
    [Serializable]
    public partial class AreaRO : ReadOnlyBase<AreaRO>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="AreaID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> AreaIDProperty = RegisterProperty<Guid>(p => p.AreaID, "Area ID", Guid.NewGuid());
        /// <summary>
        /// Gets the Area ID.
        /// </summary>
        /// <value>The Area ID.</value>
        public Guid AreaID
        {
            get { return GetProperty(AreaIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Name"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Name");
        /// <summary>
        /// Gets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name
        {
            get { return GetProperty(NameProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="GeometryText"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> GeometryTextProperty = RegisterProperty<string>(p => p.GeometryText, "Geometry Text");
        /// <summary>
        /// Gets the Geometry Text.
        /// </summary>
        /// <value>The Geometry Text.</value>
        public string GeometryText
        {
            get { return GetProperty(GeometryTextProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Angle"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> AngleProperty = RegisterProperty<int>(p => p.Angle, "Angle");
        /// <summary>
        /// Gets the Angle.
        /// </summary>
        /// <value>The Angle.</value>
        public int Angle
        {
            get { return GetProperty(AngleProperty); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AreaRO"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private AreaRO()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="AreaRO"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(AreaIDProperty, dr.GetGuid("AreaID"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(GeometryTextProperty, dr.GetString("GeometryText"));
            LoadProperty(AngleProperty, dr.GetInt32("Angle"));
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
