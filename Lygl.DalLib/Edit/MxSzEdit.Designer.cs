using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;
using Lygl.DalLib.NVL;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// MxSzEdit (editable child object).<br/>
    /// This is a generated base class of <see cref="MxSzEdit"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="MxSzEditList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class MxSzEdit : BusinessBase<MxSzEdit>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="SzID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> SzIDProperty = RegisterProperty<Guid>(p => p.SzID, "Sz ID");
        /// <summary>
        /// Gets or sets the Sz ID.
        /// </summary>
        /// <value>The Sz ID.</value>
        public Guid SzID
        {
            get { return GetProperty(SzIDProperty); }
            set { SetProperty(SzIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Name"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name, "Name");
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        /// <value>The Name.</value>
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Sex"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SexProperty = RegisterProperty<string>(p => p.Sex, "Sex");
        /// <summary>
        /// Gets or sets the Sex.
        /// </summary>
        /// <value>The Sex.</value>
        public string Sex
        {
            get { return GetProperty(SexProperty); }
            set { SetProperty(SexProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid?> MxIDProperty = RegisterProperty<Guid?>(p => p.MxID, "Mx ID");
        /// <summary>
        /// Gets or sets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid? MxID
        {
            get { return GetProperty(MxIDProperty); }
            set { SetProperty(MxIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="RmDate"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> RmDateProperty = RegisterProperty<SmartDate>(p => p.RmDate, "Rm Date");
        /// <summary>
        /// Gets or sets the Rm Date.
        /// </summary>
        /// <value>The Rm Date.</value>
        public SmartDate RmDate
        {
            get { return GetProperty(RmDateProperty); }
            set { SetProperty(RmDateProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Age"/> property.
        /// </summary>
        public static readonly PropertyInfo<int?> AgeProperty = RegisterProperty<int?>(p => p.Age, "Age");
        /// <summary>
        /// Gets or sets the Age.
        /// </summary>
        /// <value>The Age.</value>
        public int? Age
        {
            get { return GetProperty(AgeProperty); }
            set { SetProperty(AgeProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="OperatorID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> OperatorIDProperty = RegisterProperty<Guid>(p => p.OperatorID, "Operator ID");
        /// <summary>
        /// Gets or sets the Operator ID.
        /// </summary>
        /// <value>The Operator ID.</value>
        public Guid OperatorID
        {
            get { return GetProperty(OperatorIDProperty); }
            set
            {
                SetProperty(OperatorIDProperty, value);
                OnPropertyChanged("OperatorCode");
            }
        }

        /// <summary>
        /// Maintains metadata about <see cref="OperateTime"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> OperateTimeProperty = RegisterProperty<SmartDate>(p => p.OperateTime, "Operate Time");
        /// <summary>
        /// Gets or sets the Operate Time.
        /// </summary>
        /// <value>The Operate Time.</value>
        public string OperateTime
        {
            get { return GetPropertyConvert<SmartDate, String>(OperateTimeProperty); }
            set { SetPropertyConvert<SmartDate, String>(OperateTimeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Operator Code.
        /// </summary>
        /// <value>The Operator Code.</value>
        public string OperatorCode
        {
            get
            {
                var result = string.Empty;
                if (UserNVL.GetUserNVL().ContainsKey(OperatorID))
                    result = UserNVL.GetUserNVL().GetItemByKey(OperatorID).Value;
                return result;
            }
            set
            {
                if (UserNVL.GetUserNVL().ContainsValue(value))
                {
                    var result = UserNVL.GetUserNVL().GetItemByValue(value).Key;
                    if (result != OperatorID)
                        OperatorID = result;
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MxSzEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private MxSzEdit()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="MxSzEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(SzIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="MxSzEdit"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(SzIDProperty, dr.GetGuid("SzID"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(SexProperty, dr.GetString("Sex"));
            LoadProperty(MxIDProperty, (Guid?)dr.GetValue("MxID"));
            LoadProperty(RmDateProperty, dr.GetSmartDate("RmDate", true));
            LoadProperty(AgeProperty, (int?)dr.GetValue("Age"));
            LoadProperty(OperatorIDProperty, dr.GetGuid("OperatorID"));
            LoadProperty(OperateTimeProperty, dr.GetSmartDate("OperateTime", true));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="MxSzEdit"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddMxSzEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SzID", ReadProperty(SzIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Sex", ReadProperty(SexProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty).Equals(Guid.Empty) ? (object)DBNull.Value : ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@RmDate", ReadProperty(RmDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Age", ReadProperty(AgeProperty) == null ? (object)DBNull.Value : ReadProperty(AgeProperty).Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="MxSzEdit"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateMxSzEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SzID", ReadProperty(SzIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Sex", ReadProperty(SexProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty).Equals(Guid.Empty) ? (object)DBNull.Value : ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@RmDate", ReadProperty(RmDateProperty).DBValue).DbType = DbType.DateTime;
                    cmd.Parameters.AddWithValue("@Age", ReadProperty(AgeProperty) == null ? (object)DBNull.Value : ReadProperty(AgeProperty).Value).DbType = DbType.Int32;
                    cmd.Parameters.AddWithValue("@OperatorID", ReadProperty(OperatorIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@OperateTime", ReadProperty(OperateTimeProperty).DBValue).DbType = DbType.DateTime;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="MxSzEdit"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteMxSzEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SzID", ReadProperty(SzIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnDeletePre(args);
                    cmd.ExecuteNonQuery();
                    OnDeletePost(args);
                }
            }
        }

        #endregion

        #region Pseudo Events

        /// <summary>
        /// Occurs after setting all defaults for object creation.
        /// </summary>
        partial void OnCreate(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Delete, after setting query parameters and before the delete operation.
        /// </summary>
        partial void OnDeletePre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Delete, after the delete operation, before Commit().
        /// </summary>
        partial void OnDeletePost(DataPortalHookArgs args);

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

        /// <summary>
        /// Occurs after setting query parameters and before the update operation.
        /// </summary>
        partial void OnUpdatePre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after the update operation, before setting back row identifiers (RowVersion) and Commit().
        /// </summary>
        partial void OnUpdatePost(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after setting query parameters and before the insert operation.
        /// </summary>
        partial void OnInsertPre(DataPortalHookArgs args);

        /// <summary>
        /// Occurs in DataPortal_Insert, after the insert operation, before setting back row identifiers (ID and RowVersion) and Commit().
        /// </summary>
        partial void OnInsertPost(DataPortalHookArgs args);

        #endregion

    }
}
