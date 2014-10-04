using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Edit
{

    /// <summary>
    /// ContactEdit (editable child object).<br/>
    /// This is a generated base class of <see cref="ContactEdit"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="ContactEditList"/> collection.
    /// </remarks>
    [Serializable]
    public partial class ContactEdit : BusinessBase<ContactEdit>
    {

        #region Business Properties

        /// <summary>
        /// Maintains metadata about <see cref="ContactID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> ContactIDProperty = RegisterProperty<Guid>(p => p.ContactID, "Contact ID");
        /// <summary>
        /// Gets or sets the Contact ID.
        /// </summary>
        /// <value>The Contact ID.</value>
        public Guid ContactID
        {
            get { return GetProperty(ContactIDProperty); }
            set { SetProperty(ContactIDProperty, value); }
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
        /// Maintains metadata about <see cref="Dw"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> DwProperty = RegisterProperty<string>(p => p.Dw, "Dw");
        /// <summary>
        /// Gets or sets the Dw.
        /// </summary>
        /// <value>The Dw.</value>
        public string Dw
        {
            get { return GetProperty(DwProperty); }
            set { SetProperty(DwProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Address"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> AddressProperty = RegisterProperty<string>(p => p.Address, "Address");
        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        /// <value>The Address.</value>
        public string Address
        {
            get { return GetProperty(AddressProperty); }
            set { SetProperty(AddressProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Phone"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> PhoneProperty = RegisterProperty<string>(p => p.Phone, "Phone");
        /// <summary>
        /// Gets or sets the Phone.
        /// </summary>
        /// <value>The Phone.</value>
        public string Phone
        {
            get { return GetProperty(PhoneProperty); }
            set { SetProperty(PhoneProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Mobile"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> MobileProperty = RegisterProperty<string>(p => p.Mobile, "Mobile");
        /// <summary>
        /// Gets or sets the Mobile.
        /// </summary>
        /// <value>The Mobile.</value>
        public string Mobile
        {
            get { return GetProperty(MobileProperty); }
            set { SetProperty(MobileProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="SfzID"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> SfzIDProperty = RegisterProperty<string>(p => p.SfzID, "Sfz ID");
        /// <summary>
        /// Gets or sets the Sfz ID.
        /// </summary>
        /// <value>The Sfz ID.</value>
        public string SfzID
        {
            get { return GetProperty(SfzIDProperty); }
            set { SetProperty(SfzIDProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="Yszgx"/> property.
        /// </summary>
        public static readonly PropertyInfo<string> YszgxProperty = RegisterProperty<string>(p => p.Yszgx, "Yszgx");
        /// <summary>
        /// Gets or sets the Yszgx.
        /// </summary>
        /// <value>The Yszgx.</value>
        public string Yszgx
        {
            get { return GetProperty(YszgxProperty); }
            set { SetProperty(YszgxProperty, value); }
        }

        /// <summary>
        /// Maintains metadata about <see cref="MxID"/> property.
        /// </summary>
        public static readonly PropertyInfo<Guid> MxIDProperty = RegisterProperty<Guid>(p => p.MxID, "Mx ID");
        /// <summary>
        /// Gets or sets the Mx ID.
        /// </summary>
        /// <value>The Mx ID.</value>
        public Guid MxID
        {
            get { return GetProperty(MxIDProperty); }
            set { SetProperty(MxIDProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactEdit"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private ContactEdit()
        {
            // Prevent direct creation

            // show the framework that this is a child object
            MarkAsChild();
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads default values for the <see cref="ContactEdit"/> object properties.
        /// </summary>
        [Csla.RunLocal]
        protected override void Child_Create()
        {
            LoadProperty(ContactIDProperty, Guid.NewGuid());
            LoadProperty(DwProperty, null);
            LoadProperty(AddressProperty, null);
            LoadProperty(PhoneProperty, null);
            LoadProperty(MobileProperty, null);
            LoadProperty(SfzIDProperty, null);
            LoadProperty(YszgxProperty, null);
            var args = new DataPortalHookArgs();
            OnCreate(args);
            base.Child_Create();
        }

        /// <summary>
        /// Loads a <see cref="ContactEdit"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            LoadProperty(ContactIDProperty, dr.GetGuid("ContactID"));
            LoadProperty(NameProperty, dr.GetString("Name"));
            LoadProperty(DwProperty, !dr.IsDBNull("Dw") ? dr.GetString("Dw") : null);
            LoadProperty(AddressProperty, !dr.IsDBNull("Address") ? dr.GetString("Address") : null);
            LoadProperty(PhoneProperty, !dr.IsDBNull("Phone") ? dr.GetString("Phone") : null);
            LoadProperty(MobileProperty, !dr.IsDBNull("Mobile") ? dr.GetString("Mobile") : null);
            LoadProperty(SfzIDProperty, !dr.IsDBNull("SfzID") ? dr.GetString("SfzID") : null);
            LoadProperty(YszgxProperty, !dr.IsDBNull("Yszgx") ? dr.GetString("Yszgx") : null);
            LoadProperty(MxIDProperty, dr.GetGuid("MxID"));
            var args = new DataPortalHookArgs(dr);
            OnFetchRead(args);
            // check all object rules and property rules
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Inserts a new <see cref="ContactEdit"/> object in the database.
        /// </summary>
        private void Child_Insert()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.AddContactEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContactID", ReadProperty(ContactIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Dw", ReadProperty(DwProperty) == null ? (object)DBNull.Value : ReadProperty(DwProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Address", ReadProperty(AddressProperty) == null ? (object)DBNull.Value : ReadProperty(AddressProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Phone", ReadProperty(PhoneProperty) == null ? (object)DBNull.Value : ReadProperty(PhoneProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Mobile", ReadProperty(MobileProperty) == null ? (object)DBNull.Value : ReadProperty(MobileProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@SfzID", ReadProperty(SfzIDProperty) == null ? (object)DBNull.Value : ReadProperty(SfzIDProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Yszgx", ReadProperty(YszgxProperty) == null ? (object)DBNull.Value : ReadProperty(YszgxProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnInsertPre(args);
                    cmd.ExecuteNonQuery();
                    OnInsertPost(args);
                }
            }
        }

        /// <summary>
        /// Updates in the database all changes made to the <see cref="ContactEdit"/> object.
        /// </summary>
        private void Child_Update()
        {
            if (!IsDirty)
                return;

            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.UpdateContactEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContactID", ReadProperty(ContactIDProperty)).DbType = DbType.Guid;
                    cmd.Parameters.AddWithValue("@Name", ReadProperty(NameProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Dw", ReadProperty(DwProperty) == null ? (object)DBNull.Value : ReadProperty(DwProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Address", ReadProperty(AddressProperty) == null ? (object)DBNull.Value : ReadProperty(AddressProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Phone", ReadProperty(PhoneProperty) == null ? (object)DBNull.Value : ReadProperty(PhoneProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Mobile", ReadProperty(MobileProperty) == null ? (object)DBNull.Value : ReadProperty(MobileProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@SfzID", ReadProperty(SfzIDProperty) == null ? (object)DBNull.Value : ReadProperty(SfzIDProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@Yszgx", ReadProperty(YszgxProperty) == null ? (object)DBNull.Value : ReadProperty(YszgxProperty)).DbType = DbType.String;
                    cmd.Parameters.AddWithValue("@MxID", ReadProperty(MxIDProperty)).DbType = DbType.Guid;
                    var args = new DataPortalHookArgs(cmd);
                    OnUpdatePre(args);
                    cmd.ExecuteNonQuery();
                    OnUpdatePost(args);
                }
            }
        }

        /// <summary>
        /// Self deletes the <see cref="ContactEdit"/> object from database.
        /// </summary>
        private void Child_DeleteSelf()
        {
            using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
            {
                using (var cmd = new SqlCommand("dbo.DeleteContactEdit", ctx.Connection))
                {
                    cmd.Transaction = ctx.Transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ContactID", ReadProperty(ContactIDProperty)).DbType = DbType.Guid;
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
