using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;

namespace Lygl.UI.Framework
{
    public static class Utils
    {
        /// <summary>
        /// 获取VM绑定的V
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public static UIElement GetBindingView(DependencyObject vm)
        {
            var view = ViewLocator.LocateForModel(vm, null, null);
            ViewModelBinder.Bind(vm, view, null);
            return view;
        }
       
        
    }
}
