using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.NVL
{

    /// <summary>
    /// productQtsf (read only object).<br/>
    /// This is a generated base class of <see cref="productQtsf"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="ProductQtsfColl"/> collection.
    /// </remarks>
    [Serializable]
    public partial class productQtsf : ReadOnlyBase<productQtsf>
    {

        #region Business Properties

        /// <summary>
        /// Gets the Item ID.
        /// </summary>
        /// <value>The Item ID.</value>
        public string ItemID { get; private set; }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Unit Price.
        /// </summary>
        /// <value>The Unit Price.</value>
        public Decimal UnitPrice { get; private set; }

        /// <summary>
        /// Gets the Unit.
        /// </summary>
        /// <value>The Unit.</value>
        public string Unit { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="productQtsf"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private productQtsf()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="productQtsf"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            ItemID = dr.GetString("ItemID");
            Name = dr.GetString("Name");
            UnitPrice = dr.GetDecimal("UnitPrice");
            Unit = dr.GetString("Unit");
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
