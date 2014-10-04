using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Lygl.UI.UserManager;
using System.Windows.Threading;
using log4net;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace Lygl.UI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ////Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CHS");
            ////FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            ////    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage("zh-CN")));

            this.DispatcherUnhandledException += OnDispatcherUnhandledException;

            ////是否能登录成功
            //if (LoginWindow.Execute())
            //{
            //    MainWindow.Show();
            //    //if (Compose())
            //    //{
            //    //    //Application.Current.Run(_container.GetExportedValue<Window>("OpenExpressApp.MainWindow")); 
            //    //    _container.GetExportedValue<Window>("OpenExpressApp.MainWindow").Show();
            //    //}
            //}
            //else
            //{
            //    this.Shutdown();
            //}

        }


        #region 处理异常
        /// <summary>
        /// 应用程序异常处理。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                //Exception ex = LastInnerException(e.Exception);
                //TaskDialog taskDialog = new TaskDialog
                //{
                //    Width = 600,
                //    Title = "错误",
                //    Content = ex.Message,
                //    ExpansionPosition = TaskDialogExpansionPosition.Footer,
                //    ExpansionButtonContent = "详细信息",
                //    ExpansionContent = new TextBox()
                //    {
                //        AcceptsReturn = true,
                //        Text = e.Exception.ToString(),
                //        Height = 300
                //    }
                //};

                //taskDialog.Show();

                ILog log = LogManager.GetLogger("Lygl");
                log.Error("系统未捕获异常", e.Exception);

                e.Handled = true;
            }
            catch { }
        }

        private Exception LastInnerException(Exception ex)
        {
            Exception result = ex;
            while (null != result)
            {
                if (null == result.InnerException)
                {
                    return result;
                }
                result = result.InnerException;
            }
            return ex;
        }

        //private GException FirstGException(Exception ex)
        //{
        //    Exception result = ex;
        //    while ((null != result) && (!(result is GException)))
        //    {
        //        result = result.InnerException;
        //    }
        //    if (result is GException)
        //    {
        //        return result as GException;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        #endregion
    }
}
