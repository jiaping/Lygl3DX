using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Lygl.DalLib.Core;
using Lygl.DalLib.NVL;

namespace Lygl.DalLib.Browse
{

    /// <summary>
    /// MxRO (read only object).<br/>
    /// This is a generated base class of <see cref="MxRO"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="MxROL"/> collection.
    /// </remarks>
    [Serializable]
    public partial class MxRO : ReadOnlyBase<MxRO>, IMx
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID, "Mx ID", Guid.NewGuid());
        /// <summary>
        /// Gets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid MxID
        {
            get { return GetProperty(MxIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MxNameProperty = RegisterProperty<string>(p => p.MxName, "Mx Name");
        /// <summary>
        /// Gets the Mx Name.
        /// </summary>
        /// <value>The Mx Name.</value>
        public string MxName
        {
            get { return GetProperty(MxNameProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Pos"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PosProperty = RegisterProperty<string>(p => p.Pos, "Pos");
        /// <summary>
        /// Gets the Pos.
        /// </summary>
        /// <value>The Pos.</value>
        public string Pos
        {
            get { return GetProperty(PosProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxStatusID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MxStatusIDProperty = RegisterProperty<int>(p => p.MxStatusID, "Mx Status ID");
        /// <summary>
        /// Gets the Mx Status ID.
        /// </summary>
        /// <value>The Mx Status ID.</value>
        public int MxStatusID
        {
            get { return GetProperty(MxStatusIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxTypeID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MxTypeIDProperty = RegisterProperty<int>(p => p.MxTypeID, "Mx Type ID");
        /// <summary>
        /// Gets the Mx Type ID.
        /// </summary>
        /// <value>The Mx Type ID.</value>
        public int MxTypeID
        {
            get { return GetProperty(MxTypeIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="AreaID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> AreaIDProperty = RegisterProperty<Guid>(p => p.AreaID, "Area ID");
        /// <summary>
        /// Gets the Area ID.
        /// </summary>
        /// <value>The Area ID.</value>
        public Guid AreaID
        {
            get { return GetProperty(AreaIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Price"/> property.
        /// </summary>
        public static readonly PropertyInfo<Decimal?> PriceProperty = RegisterProperty<Decimal?>(p => p.Price, "Price", null);
        /// <summary>
        /// Gets the Price.
        /// </summary>
        /// <value>The Price.</value>
        public Decimal? Price
        {
            get { return GetProperty(PriceProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Rmsj"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> RmsjProperty = RegisterProperty<SmartDate>(p => p.Rmsj, "Rmsj", null);
        /// <summary>
        /// Gets the Rmsj.
        /// </summary>
        /// <value>The Rmsj.</value>
        public string Rmsj
        {
            get { return GetPropertyConvert<SmartDate, String>(RmsjProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="ManageFeeEndTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> ManageFeeEndTimeProperty = RegisterProperty<SmartDate>(p => p.ManageFeeEndTime, "Manage Fee End Time", null);
        /// <summary>
        /// Gets the Manage Fee End Time.
        /// </summary>
        /// <value>The Manage Fee End Time.</value>
        public string ManageFeeEndTime
        {
            get { return GetPropertyConvert<SmartDate, String>(ManageFeeEndTimeProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="KbrName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> KbrNameProperty = RegisterProperty<string>(p => p.KbrName, "Kbr Name", null);
        /// <summary>
        /// Gets the Kbr Name.
        /// </summary>
        /// <value>The Kbr Name.</value>
        public string KbrName
        {
            get { return GetProperty(KbrNameProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Bwjf"/> property.
        /// </summary>
        public static readonly PropertyInfo<bool?> BwjfProperty = RegisterProperty<bool?>(p => p.Bwjf, "Bwjf", null);
        /// <summary>
        /// Gets the Bwjf.
        /// </summary>
        /// <value><c>true</c> if Bwjf; <c>false</c> if not Bwjf; otherwise, <c>null</c>.</value>
        public bool? Bwjf
        {
            get { return GetProperty(BwjfProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Mz"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MzProperty = RegisterProperty<string>(p => p.Mz, "Mz", null);
        /// <summary>
        /// Gets the Mz.
        /// </summary>
        /// <value>The Mz.</value>
        public string Mz
        {
            get { return GetProperty(MzProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxStyleID"/> property.
        /// </summary>
        public static readonly PropertyInfo<int> MxStyleIDProperty = RegisterProperty<int>(p => p.MxStyleID, "Mx Style ID");
        /// <summary>
        /// Gets the Mx Style ID.
        /// </summary>
        /// <value>The Mx Style ID.</value>
        public int MxStyleID
        {
            get { return GetProperty(MxStyleIDProperty); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="SzName"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SzNameProperty = RegisterProperty<string>(p => p.SzName, "Sz Name", null);
        /// <summary>
        /// Gets the Sz Name.
        /// </summary>
        /// <value>The Sz Name.</value>
        public string SzName
        {
            get { return GetProperty(SzNameProperty); }
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

        /// <summary>
        /// Gets the Mx Status.
        /// </summary>
        /// <value>The Mx Status.</value>
        public string MxStatus
        {
            get
            {
                var result = string.Empty;
                if (MxStatusNVL.GetMxStatusNVL().ContainsKey(MxStatusID))
                    result = MxStatusNVL.GetMxStatusNVL().GetItemByKey(MxStatusID).Value;
                return result;
            }
        }

        /// <summary>
        /// Gets the Mx Type.
        /// </summary>
        /// <value>The Mx Type.</value>
        public string MxType
        {
            get
            {
                var result = string.Empty;
                if (MxTypeNVL.GetMxTypeNVL().ContainsKey(MxTypeID))
                    result = MxTypeNVL.GetMxTypeNVL().GetItemByKey(MxTypeID).Value;
                return result;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxRO"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxRO()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="MxRO"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            LoadProperty(MxNameProperty, dr.GetString("MxName"));
            LoadProperty(PosProperty, dr.GetString("Pos"));
            LoadProperty(MxStatusIDProperty, dr.GetInt32("MxStatusID"));
            LoadProperty(MxTypeIDProperty, dr.GetInt32("MxTypeID"));
            LoadProperty(AreaIDProperty, dr.GetGuid("AreaID"));
            LoadProperty(PriceProperty, (Decimal?)dr.GetValue("Price"));
            LoadProperty(RmsjProperty, !dr.IsDBNull("Rmsj") ? dr.GetSmartDate("Rmsj", true) : null);
            LoadProperty(ManageFeeEndTimeProperty, !dr.IsDBNull("ManageFeeEndTime") ? dr.GetSmartDate("ManageFeeEndTime", true) : null);
            LoadProperty(KbrNameProperty, !dr.IsDBNull("KbrName") ? dr.GetString("KbrName") : null);
            LoadProperty(BwjfProperty, (bool?)dr.GetValue("Bwjf"));
            LoadProperty(MzProperty, !dr.IsDBNull("Mz") ? dr.GetString("Mz") : null);
            LoadProperty(MxStyleIDProperty, dr.GetInt32("MxStyleID"));
            LoadProperty(SzNameProperty, !dr.IsDBNull("SzName") ? dr.GetString("SzName") : null);
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
