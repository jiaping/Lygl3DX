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
using Lygl.DalLib.UserManager;
using Lygl.UI.Framework;
using Lygl.UI.Framework.ViewModelBase;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Lygl.DalLib.Util;


namespace Lygl.UI.UserManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public class ResetPwdViewModel : ScreenWithModelBase<User>
    {
        public  bool _result;

        private User4SetPwd  user;
        private bool needCheck;
        public ResetPwdViewModel(Guid userID,bool needCheck=true)
        {
           this._result = false;
           this.needCheck = needCheck;
           this.user = User4SetPwd.GetUser4SetPwd(userID);
        }

       
        private string _passwordOrg;
        private string _passwordNew1;
        private string _passwordNew2;
        public void PasswordOrgChanged(object sender, RoutedEventArgs e)
        {
            _passwordOrg = (sender as PasswordBox).Password;
        }
        public void PasswordNew1Changed(object sender, RoutedEventArgs e)
        {
            _passwordNew1 = (sender as PasswordBox).Password;
        }
        public void PasswordNew2Changed(object sender, RoutedEventArgs e)
        {
            _passwordNew2 = (sender as PasswordBox).Password;
        }


        protected override void OnViewLoaded(object view)
        {
            
            base.OnViewLoaded(view);
            if (!needCheck)
            {
                if (view is Window)
                {
                    ((Label)((view as Window).Content as FrameworkElement).FindName("OrgLabel")).Visibility = Visibility.Hidden;
                    ((PasswordBox)((view as Window).Content as FrameworkElement).FindName("OrgPassword")).Visibility = Visibility.Hidden;
                } 
            }
        }

        public void btnCancel()
        {
            this._result = false;
           this.TryClose();
        }

        public void ResetPwd()
        {
            if (user.UserID == Guid.Empty)
                MessageBox.Show("用户不存在！", "提示");
            if (needCheck)
            {
                if (SecurityHelper.GetMd5Hash(_passwordOrg.Trim()) == user.Password)
                {
                    if (_passwordNew1 != _passwordNew2) { MessageBox.Show("新密码输入不一致！请重新输入", "提示"); return; }
                    user.Password = SecurityHelper.GetMd5Hash(_passwordNew1);
                    user.Save();
                    this.TryClose();
                    MessageBox.Show("密码已重新设置！", "提示");
                }
                else
                {
                    MessageBox.Show("原密码不正确！请重新输入", "提示");
                }

            }
            else
            {
                if (_passwordNew1 != _passwordNew2) { MessageBox.Show("新密码输入不一致！请重新输入", "提示"); return; }
                user.Password = SecurityHelper.GetMd5Hash(_passwordNew1);
                user.Save();
                this.TryClose();
                MessageBox.Show("密码已重新设置！", "提示");
            }
           
        }

        
    }
}