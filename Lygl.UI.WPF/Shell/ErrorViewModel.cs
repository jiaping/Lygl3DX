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

namespace Lygl.Shell
{
    

    /// <summary>
    /// Interaction logic for AboutFrm.xaml
    /// </summary>
    public partial class ErrorViewModel : ScreenWithModelBase<AreaRO>
    {
        private Exception _error;
        /// <summary>
        /// Default constructor is protected so callers must use one with a parent.
        /// </summary>
        public ErrorViewModel(Exception error)
        {
            DisplayName = "出错";
            _error = error;
            System.Threading.ThreadPool.QueueUserWorkItem(o =>
            {
                GC.GetTotalMemory(true);
            });
        }


    

      
       

        /// <summary>
        /// Gets the description about the application.
        /// </summary>
        public string Description
        {
            get { return _error.ToString(); }
        }

     

      
    }
}
