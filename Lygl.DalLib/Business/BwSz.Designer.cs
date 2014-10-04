using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Business
{

    /// <summary>
    /// BwSz (editable child object).<br/>
    /// This is a generated base class of <see cref="BwSz"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="BwSzList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class BwSz : BusinessBase<BwSz>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="BwSzID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> BwSzIDProperty = RegisterProperty<Guid>(p => p.BwSzID, "Bw Sz ID");
        /// <summary>
        /// Gets or sets the Bw Sz ID.
        /// </summary>
        /// <value>The Bw Sz ID.</value>
        public Guid BwSzID
        {
            get { return GetProperty(BwSzIDProperty); }
            set { SetProperty(BwSzIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Ch"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> ChProperty = RegisterProperty<string>(p => p.Ch, "Ch");
        /// <summary>
        /// Gets or sets the Ch.
        /// </summary>
        /// <value>The Ch.</value>
        public string Ch
        {
            get { return GetProperty(ChProperty); }
            set { SetProperty(ChProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Sheng"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> ShengProperty = RegisterProperty<string>(p => p.Sheng, "Sheng");
        /// <summary>
        /// Gets or sets the Sheng.
        /// </summary>
        /// <value>The Sheng.</value>
        public string Sheng
        {
            get { return GetProperty(ShengProperty); }
            set { SetProperty(ShengProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Gu"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> GuProperty = RegisterProperty<string>(p => p.Gu, "Gu");
        /// <summary>
        /// Gets or sets the Gu.
        /// </summary>
        /// <value>The Gu.</value>
        public string Gu
        {
            get { return GetProperty(GuProperty); }
            set { SetProperty(GuProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BwSz"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private BwSz()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="BwSz"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(BwSzIDProperty, Guid.NewGuid());
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="BwSz"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(BwSzIDProperty, dr.GetGuid("BwSzID"));
            LoadProperty(ChProperty, dr.GetString("Ch"));
            LoadProperty(ShengProperty, dr.GetString("Sheng"));
            LoadProperty(GuProperty, dr.GetString("Gu"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="BwSz"/> object in the database.
        /// </summary>
        /// <param name="parent">The parent object.</param>
        private void Child_Insert(BusinessLb parent)
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddBwSz", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BusinessID", parent.BusinessID).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@BwSzID", ReadProperty(BwSzIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Ch", ReadProperty(ChProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Sheng", ReadProperty(ShengProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Gu", ReadProperty(GuProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="BwSz"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateBwSz", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BwSzID", ReadProperty(BwSzIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Ch", ReadProperty(ChProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Sheng", ReadProperty(ShengProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Gu", ReadProperty(GuProperty)).DbType = DbType.String;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="BwSz"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteBwSz", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BwSzID", ReadProperty(BwSzIDProperty)).DbType = DbType.Guid;
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
