using System.Configuration;

namespace Lygl.DalLib.Util
{
    /// <summary>Class that provides the connection
    /// strings used by the application.</summary>
    internal static class Database
    {

        #region Database Connections

        /// <summary>Connection string to the LyglDB database.</summary>
        internal static string LyglDBConnection
        {
            get { return ConfigurationManager.ConnectionStrings["LyglDB"].ConnectionString; }
        }

        /// <summary>Connection string to the security database.</summary>
        internal static string SecurityConnection
        {
            get { return ConfigurationManager.ConnectionStrings["Security"].ConnectionString; }
        }

        #endregion

    }
}
