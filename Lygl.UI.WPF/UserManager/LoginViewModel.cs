using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Csla;
using Lygl.UI.Framework;
using Lygl.UI.Framework.ViewModelBase;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using Lygl.DalLib.UserManager;


namespace Lygl.UI.UserManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    [Export( typeof(LoginViewModel))]
    public class LoginViewModel : ScreenWithModel<User>
    {
        public  bool _result;

        public LoginViewModel()
        {
           this._result = false;
            //txtUserName.Text = "loginName";// AppConfig.Instance.LoginName;
        }

        public bool Result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }

        private string _password;
        public void PasswordChanged(object sender, RoutedEventArgs e)
        {
            _password = (sender as PasswordBox).Password;
        }

        public void Login(string userName)
        {
            

            this._result = false;
            if ((userName == null) || (userName == string.Empty) || (_password == null) || (_password == string.Empty))
            {
                MessageBox.Show("用户或密码错误，请重新输入！", "登录失败");
                this._result = false;
                return;
            }
            if (!CustomPrincipal.Login(userName,Lygl.DalLib.Util.SecurityHelper.GetMd5Hash( _password.Trim())) )       // if (CustomPrincipal.Login(txtUserName.Text, StringHelper.MD5(txtPassword.Password)) == false)
            {
                MessageBox.Show("用户或密码错误，请重新输入！", "登录失败");

                //AuditLogService.LogAsync(new AuditLogItem()
                //{
                //    Title = "登录失败",
                //    Type = AuditLogType.Login
                //});
            }
            else
            {
                //记录登录用户名
                //Csla.ApplicationContext.User = (ApplicationContext.User.Identity as CustomIdentity).User;

                //AppConfig.Instance.LoginName = txtUserName.Text;
                //AppConfig.Instance.Save();
                //User user = (ApplicationContext.User.Identity as OEAIdentity).User;

                //this._result = user.TryLogin();
                //if (this._result == false)
                //{
                //    MessageBox.Show("登录次数已到！");
                //}

                //AuditLogService.LogAsync(new AuditLogItem()
                //{
                //    Title = this._result ? "登录成功" : "登录次数已到",
                //    Type = AuditLogType.Login
                //});
                this._result = true;
                this.TryClose();
            }
        }

        public string txtPassword
        {
            get;
            set;
        }
        public void btnCancel()
        {
            this._result = false;
           this.TryClose();
        }

//        public static bool Execute()
//        {
////            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;  //防止登录后Application退出

////            //Bxf.Shell.Instance.ShowView(typeof(LoginWindow).AssemblyQualifiedName, "loginViewModelViewSource", new ViewModel.LoginViewModel(), "");
////            LoginWindow loginWin = new LoginWindow();
////#if NOLOGIN    
////                loginWin.btnLogin_Click(loginWin.btnLogin,new RoutedEventArgs());
////#else
////            loginWin.ShowDialog();
////#endif
////            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
//            //return loginWin.Result;
//            return true;
//        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource loginViewModelViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("loginViewModelViewSource")));
            // 通过设置 CollectionViewSource.Source 属性加载数据:
            // loginViewModelViewSource.Source = [一般数据源]
        }
    }
}