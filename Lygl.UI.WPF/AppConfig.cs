/*******************************************************
 * 
 * 作者：胡庆访
 * 创建时间：20100125
 * 说明：服务器配置的所有相关类
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 胡庆访 20100125
 * 
*******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace Lygl.UI
{
    /// <summary>
    /// 一个可以存储的配置
    /// </summary>
    public interface ISavableConfig
    {
        /// <summary>
        /// 保存到配置中
        /// </summary>
        /// <returns></returns>
        bool Save();
    }

    /// <summary>
    /// 连接字符串的配置
    /// 
    /// 配置四个库名，及统一的用户名、密码、服务器地址。
    /// </summary>
    public interface IConnctionStringConfig : ISavableConfig
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        string DB_Server { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        string DB_UserID { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        string DB_Password { get; set; }

        //string DBName_GIX4 { get; set; }

        //string DBName_Estimate { get; set; }

        string DBName_LYGL { get; set; }

        //string DBName_BQNorm { get; set; }
    }

    /// <summary>
    /// WCF通信的配置
    /// </summary>
    public interface IWCFConfig : ISavableConfig
    {
        /// <summary>
        /// WCF端口
        /// </summary>
        int WCFPort { get; set; }

        /// <summary>
        /// 目前使用的远程连接
        /// </summary>
        Uri WCFEndPoint { get; }
    }

    /// <summary>
    /// 自动备份数据库的配置
    /// </summary>
    public interface IDBAutoBackupConfig
    {
        /// <summary>
        /// 自动备份是否启用
        /// </summary>
        bool AutoBackup_Enabled { get; }

        /// <summary>
        /// 要备份的数据库连接名
        /// </summary>
        string[] AutoBackup_ConnectionStringNames { get; }

        /// <summary>
        /// 获取自动备份数据库的文件夹地址
        /// </summary>
        string AutoBackup_Dir { get; }

        /// <summary>
        /// AutoBackup_Dir文件夹中最大能存储的文件个数
        /// </summary>
        int AutoBackup_MaxFileCount { get; }
    }

    /// <summary>
    /// 应用程序配置实现类
    /// </summary>
    public class AppConfig : IConnctionStringConfig, IDBAutoBackupConfig, //, IWCFConfig
        ISavableConfig
    {
        /// <summary>
        /// SingelTon
        /// </summary>
        public static readonly AppConfig Instance;

        static AppConfig()
        {
            Instance = new AppConfig();
        }

        /// <summary>
        /// 本类最终是对这个字段的内容进行修改
        /// </summary>
        private Configuration _innerConfig;

        private AppConfig()
        {
            this._innerConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            this.ReadConfig();
        }

        #region IConnctionStringConfig Members

        private static readonly string CONNECTION_STRING_NAME_LYGL = "LyglDB";
        //private static readonly string CONNECTION_STRING_NAME_GIX4 = "BusinessDBName";
        //private static readonly string CONNECTION_STRING_NAME_BQNORM = "BQNormDBName";
        //private static readonly string CONNECTION_STRING_NAME_ESTIMATE = "EstimateDBName";

        /// <summary>
        /// 已经启用的数据库名
        /// </summary>
        //private static readonly string[] CONNECTION_STRING_NAMES = new string[]{
        //    CONNECTION_STRING_NAME_OEA,
        //    CONNECTION_STRING_NAME_GIX4, 
        //    CONNECTION_STRING_NAME_BQNORM, 
        //    CONNECTION_STRING_NAME_ESTIMATE
        //};

        /// <summary>
        /// 临时存储连接字符串的值
        /// </summary>
        private SqlConnectionStringBuilder _connectionString;

        private Dictionary<string, string> _dbNames;

        public string DB_Server
        {
            get
            {
                return this._connectionString.DataSource;
            }
            set
            {
                this._connectionString.DataSource = value;
            }
        }

        public string DB_UserID
        {
            get
            {
                return this._connectionString.UserID;
            }
            set
            {
                this._connectionString.UserID = value;
            }
        }

        public string DB_Password
        {
            get
            {
                return this._connectionString.Password;
            }
            set
            {
                this._connectionString.Password = value;
            }
        }

        //public string DBName_GIX4
        //{
        //    get
        //    {
        //        return this._dbNames[CONNECTION_STRING_NAME_GIX4];
        //    }
        //    set
        //    {
        //        this._dbNames[CONNECTION_STRING_NAME_GIX4] = value;
        //    }
        //}

        //public string DBName_Estimate
        //{
        //    get
        //    {
        //        return this._dbNames[CONNECTION_STRING_NAME_ESTIMATE];
        //    }
        //    set
        //    {
        //        this._dbNames[CONNECTION_STRING_NAME_ESTIMATE] = value;
        //    }
        //}

        public string DBName_LYGL
        {
            get
            {
                return this._dbNames[CONNECTION_STRING_NAME_LYGL];
            }
            set
            {
                this._dbNames[CONNECTION_STRING_NAME_LYGL] = value;
            }
        }

        //public string DBName_BQNorm
        //{
        //    get
        //    {
        //        return this._dbNames[CONNECTION_STRING_NAME_BQNORM];
        //    }
        //    set
        //    {
        //        this._dbNames[CONNECTION_STRING_NAME_BQNORM] = value;
        //    }
        //}

        //private void ReadConnectionString()
        //{
        //    var settings = this._innerConfig.ConnectionStrings.ConnectionStrings
        //        .Cast<ConnectionStringSettings>().ToArray();

        //    string conString = settings.First(s => CONNECTION_STRING_NAMES.Contains(s.Name)).ConnectionString;
        //    this._connectionString = new SqlConnectionStringBuilder(conString);

        //    this._dbNames = new Dictionary<string, string>();
        //    foreach (var conStrName in CONNECTION_STRING_NAMES)
        //    {
        //        this._dbNames[conStrName] = this.ReadDBName(settings, conStrName);
        //    }
        //}
        private void ReadConnectionString()
        {
            var settings = this._innerConfig.ConnectionStrings.ConnectionStrings
                .Cast<ConnectionStringSettings>().ToArray();

          
            string conString = settings.First(s=> s.Name==CONNECTION_STRING_NAME_LYGL).ConnectionString;
            this._connectionString = new SqlConnectionStringBuilder(conString);

            this._dbNames = new Dictionary<string, string>();
            this._dbNames[CONNECTION_STRING_NAME_LYGL] = this.ReadDBName(settings, CONNECTION_STRING_NAME_LYGL);
        }

        private string ReadDBName(ConnectionStringSettings[] settings, string settingName)
        {
            var conStr = settings.First(s => s.Name == settingName).ConnectionString;
            return new SqlConnectionStringBuilder(conStr).InitialCatalog;
        }

        //private void SaveConnectionString()
        //{
        //    //连接字符串的值是使用在_connectionString字段中，需要在存储前先把这些值写入到_innerConfig中。
        //    foreach (ConnectionStringSettings settings in this._innerConfig.ConnectionStrings.ConnectionStrings)
        //    {
        //        if (!CONNECTION_STRING_NAMES.Contains(settings.Name)) continue;

        //        var connectionString = new SqlConnectionStringBuilder(settings.ConnectionString);
        //        connectionString.DataSource = this._connectionString.DataSource;
        //        connectionString.UserID = this._connectionString.UserID;
        //        connectionString.Password = this._connectionString.Password;
        //        connectionString.InitialCatalog = this._dbNames[settings.Name];

        //        settings.ConnectionString = connectionString.ToString();
        //    }
        //}
        private void SaveConnectionString()
        {
            //连接字符串的值是使用在_connectionString字段中，需要在存储前先把这些值写入到_innerConfig中。
            //foreach (ConnectionStringSettings settings in this._innerConfig.ConnectionStrings.ConnectionStrings)
            //{
            ConnectionStringSettings settings = this._innerConfig.ConnectionStrings.ConnectionStrings[CONNECTION_STRING_NAME_LYGL];
                var connectionString = new SqlConnectionStringBuilder(settings.ConnectionString);
                connectionString.DataSource = this._connectionString.DataSource;
                connectionString.UserID = this._connectionString.UserID;
                connectionString.Password = this._connectionString.Password;
                connectionString.InitialCatalog = this._dbNames[CONNECTION_STRING_NAME_LYGL];
            
                settings.ConnectionString = connectionString.ToString();
                this._innerConfig.ConnectionStrings.SectionInformation.ProtectSection("");
            //}
        }

        #endregion

        #region IWCFConfig Members

        //public int WCFPort
        //{
        //    get
        //    {
        //        return this.WCFEndPoint.Port;
        //    }
        //    set
        //    {
        //        //var servicesSection = this._innerConfig.GetSection("system.serviceModel/services") as ServicesSection;
        //        var service = servicesSection.Services[0];

        //        foreach (ServiceEndpointElement endpoint in service.Endpoints)
        //        {
        //            endpoint.Address = this.ModifyPort(endpoint.Address, value);
        //        }
        //        var baseAddress = service.Host.BaseAddresses[0];
        //        baseAddress.BaseAddress = this.ModifyPort(new Uri(baseAddress.BaseAddress), value).ToString();
        //    }
        //}

        //public Uri WCFEndPoint
        //{
        //    get
        //    {
        //        //var servicesSection = this._innerConfig.GetSection("system.serviceModel/services") as ServicesSection;
        //        return servicesSection.Services[0].Endpoints[0].Address;
        //    }
        //}

        #endregion

        #region Other Members

        public bool AutoBackup_Enabled
        {
            get
            {
                return this.AutoBackup_ConnectionStringNames.Length > 0;
            }
        }
        public string[] AutoBackup_ConnectionStringNames
        {
            get
            {
                var settings = this._innerConfig.AppSettings.Settings["AutoBackup_ConnectionStringNames"];
                return settings != null ?
                    settings.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries) :
                    new string[0];
            }
        }
        public string AutoBackup_Dir
        {
            get
            {
                var settings = this._innerConfig.AppSettings.Settings["AutoBackup_Dir"];
                return settings != null ? settings.Value : @"D:\DB_Backup\";
            }
        }
        public int AutoBackup_MaxFileCount
        {
            get
            {
                var settings = this._innerConfig.AppSettings.Settings["AutoBackup_MaxFileCount"];
                return settings != null ? Convert.ToInt32(settings.Value) : 100;
            }
        }

        public bool Save()
        {
            try
            {
                this.SaveConfig();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 读取配置信息到内存中
        /// </summary>
        private void ReadConfig()
        {
            this.ReadConnectionString();
        }

        /// <summary>
        /// 把内存中的值，存储到配置文件中
        /// </summary>
        private void SaveConfig()
        {
            this.SaveConnectionString();
            try
            {
                this._innerConfig.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (Exception)
            {
                
                throw;
            }
          
        }

        /// <summary>
        /// 更改指定URL的端口。
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="port"></param>
        /// <returns>
        /// 返回更改过端口后的连接地址。
        /// </returns>
        private Uri ModifyPort(Uri uri, int port)
        {
            var builder = new UriBuilder(uri);
            builder.Port = port;
            return builder.Uri;
        }
    }
}

