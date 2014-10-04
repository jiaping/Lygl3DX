using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Lygl.UI.Framework.ViewModelBase;
using Lygl.DalLib.Browse;
using System.Configuration;
using Caliburn.Micro;
using Lygl.UI;

namespace Lygl.Shell
{
    /// <summary>
    /// Interaction logic for AboutFrm.xaml
    /// </summary>
    public partial class DbConfigViewModel : Screen
    {
        /// <summary>
        /// Default constructor is protected so callers must use one with a parent.
        /// </summary>
        public DbConfigViewModel()
        {
            DisplayName = "数据库配置";
            ReadConfig();
            //System.Threading.ThreadPool.QueueUserWorkItem(o =>
            //{
            //    GC.GetTotalMemory(true);
            //});
        }

        private bool _Modified;

        #region AboutData Provider

        #region Member data

      

        #endregion

        #region Properties
        public string ServerName { get; set; }
        public string DBName { get; set; }
        public string ServerPort { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }


        private void ReadConfig()
        {
            var config = AppConfig.Instance;

            ServerName = config.DB_Server;
            Pwd = config.DB_Password;
            UserName = config.DB_UserID;
            //ServerPort = config.WCFPort.ToString();
            //txtDBName_BQNorm.Text = config.DBName_BQNorm;
            //txtDBName_Estimate.Text = config.DBName_Estimate;
            //txtDBName_GIX4.Text = config.DBName_GIX4;
            DBName = config.DBName_LYGL;

            this._Modified = false;
        }

        private void SaveConfig()
        {
            #region Check input

            //if (this._Modified == false)
            //{
            //    //this.DialogResult = false;
            //    return;
            //}

            //int port = 0;
            //if (int.TryParse(ServerPort, out port) == false)
            //{
            //    MessageBox.Show("端口后必须是整数。");
            //    return;
            //}

            #endregion

            var config = AppConfig.Instance;

            config.DB_Server = ServerName;
            config.DB_Password = Pwd;
            config.DB_UserID = UserName;
            //config.WCFPort = port;
            //config.DBName_BQNorm = txtDBName_BQNorm.Text;
            //config.DBName_Estimate = txtDBName_Estimate.Text;
            //config.DBName_GIX4 = txtDBName_GIX4.Text;
            config.DBName_LYGL = DBName;

            if (config.Save())
            {
                MessageBox.Show("保存连接数据成功！");
                this.TryClose();
                //if (needReboot == MessageBoxResult.Yes)
                //{
                //string fileName = Process.GetCurrentProcess().MainModule.FileName;
                string fileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
                Application.Current.Shutdown();
                Process.Start(fileName);
                //}
            }
            else
            {
                MessageBox.Show("保存连接数据失败！");
            }
        }
        #endregion

        #region Resource location methods

        public void TextChanged()
        {
            this._Modified = true;
        }

        public  void btnOK()
        {
            SaveConfig();
            //try
            //{
            //    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            //    string csName = "LyglDB";

            //    ConnectionStringSettings csSettings = new ConnectionStringSettings(csName, 
            //                "Database=" + DBName + ";Server=" + ServerName + ";User ID=" + UserName + ";Password=" + Pwd + ";",
            //                "System.Data.SqlClient"
            //                );

            //    ConnectionStringsSection csSection = config.ConnectionStrings;
            //    csSection.ConnectionStrings.Remove(csName);
            //    // Add the new element.
            //    csSection.ConnectionStrings.Add(csSettings);


            //    // Save the configuration file.
            //    config.Save(ConfigurationSaveMode.Modified);
            //    ConfigurationManager.RefreshSection("connectionStrings");
                
            //    MessageBox.Show("写入数据成功！", "信息");
            //    this.TryClose();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("写入数据失败！" + ex.Message, "信息");
            //}
        }

        public void btnCancel()
        {
            this.TryClose();
            string fileName = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Application.Current.Shutdown();
        }

        #endregion

      

        #endregion
    }
}
