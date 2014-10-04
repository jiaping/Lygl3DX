using System;
using System.Data;
using System.Data.SqlClient;
using Csla;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.DalLib.Seek
{

    /// <summary>
    /// LogRO (read only object).<br/>
    /// This is a generated base class of <see cref="LogRO"/> business object.
    /// </summary>
    /// <remarks>
    /// This class is an item of <see cref="LogROL"/> collection.
    /// </remarks>
    [Serializable]
    public partial class LogRO : ReadOnlyBase<LogRO>
    {

        #region Business Properties

        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>The Id.</value>
        public int Id { get; private set; }

        /// <summary>
        /// Maintains metadata about <see cref="Date"/> property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> DateProperty = RegisterProperty<SmartDate>(p => p.Date, "Date");
        /// <summary>
        /// Gets the Date.
        /// </summary>
        /// <value>The Date.</value>
        public string Date
        {
            get { return GetPropertyConvert<SmartDate, String>(DateProperty); }
        }

        /// <summary>
        /// Gets the Thread.
        /// </summary>
        /// <value>The Thread.</value>
        public string Thread { get; private set; }

        /// <summary>
        /// Gets the Level.
        /// </summary>
        /// <value>The Level.</value>
        public string Level { get; private set; }

        /// <summary>
        /// Gets the Logger.
        /// </summary>
        /// <value>The Logger.</value>
        public string Logger { get; private set; }

        /// <summary>
        /// Gets the Message.
        /// </summary>
        /// <value>The Message.</value>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the Exception.
        /// </summary>
        /// <value>The Exception.</value>
        public string Exception { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LogRO"/> class.
        /// </summary>
        /// <remarks> Do not use to create a Csla object. Use factory methods instead.</remarks>
        private LogRO()
        {
            // Prevent direct creation
        }

        #endregion

        #region Data Access

        /// <summary>
        /// Loads a <see cref="LogRO"/> object from the given SafeDataReader.
        /// </summary>
        /// <param name="dr">The SafeDataReader to use.</param>
        private void Child_Fetch(SafeDataReader dr)
        {
            // Value properties
            Id = dr.GetInt32("Id");
            LoadProperty(DateProperty, dr.GetSmartDate("Date", true));
            Thread = dr.GetString("Thread");
            Level = dr.GetString("Level");
            Logger = dr.GetString("Logger");
            Message = dr.GetString("Message");
            Exception = !dr.IsDBNull("Exception") ? dr.GetString("Exception") : null;
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
