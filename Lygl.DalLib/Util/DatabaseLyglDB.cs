using System.Configuration;

namespace Lygl.DalLib.Util
{
    /// <summary>Class that provides the connection
    /// strings used by the application.</summary>
    internal static partial class Database
    {
        /// <summary>Connection string to the LyglDB database.</summary>
        internal static string LyglDBConnection
        {
            get { return  ConfigurationManager.ConnectionStrings["LyglDB"].ConnectionString; }
        }
    }
}
