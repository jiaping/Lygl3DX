using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Xaml;
using System.Windows;


namespace Lygl.UI.ViewModels
{
    public class LoginViewModel: DependencyObject
    {
        public LoginViewModel()
        {
            //var presenter = (Bxf.IPresenter)Bxf.Shell.Instance;
            //presenter.OnShowError += (message, caption) =>
            //  {
            //    MessageBox.Show(message, caption, MessageBoxButton.OK);
            //  };

            //presenter.OnShowStatus += (status) =>
            //  {
            //  };

            //presenter.OnShowView += (view, region) =>
            //  {
            //    if (region == "Content")
            //    {
            //      MainContent.Add(view.ViewInstance);
            //      while (MainContent.Count > 1)
            //        MainContent.RemoveAt(0);
            //    }
            //  };
        }

        public bool Login()
        {
            bool result = false;
            //this._result = false;
            //if (CustomPrincipal.Login(txtUserName.Text, StringHelper.MD5(txtPassword.Password)) == false)
            //{
            //    MessageBox.Show("用户或密码错误，请重新输入！", "登录失败");

            //    AuditLogService.LogAsync(new AuditLogItem()
            //    {
            //        Title = "登录失败",
            //        Type = AuditLogType.Login
            //    });
            //}
            //else
            //{
            //    //记录登录用户名
            //    AppConfig.Instance.LoginName = txtUserName.Text;
            //    AppConfig.Instance.Save();
            //    User user = (ApplicationContext.User.Identity as OEAIdentity).User;

            //    this._result = user.TryLogin();
            //    if (this._result == false)
            //    {
            //        MessageBox.Show("登录次数已到！");
            //    }

            //    AuditLogService.LogAsync(new AuditLogItem()
            //    {
            //        Title = this._result ? "登录成功" : "登录次数已到",
            //        Type = AuditLogType.Login
            //    });

            //    this.Close();
            //}
       
            return result;
        }
        public void Cancel()
        {

        }
    }
}
