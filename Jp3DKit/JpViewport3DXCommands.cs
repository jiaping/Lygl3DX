using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Jp3DKit
{
    public static class JpViewport3DXCommands
    {

        /// <summary>
        /// Ctrl+S 用于保存操作手势
        /// </summary>
        public static RoutedCommand ControlSave { get { return controlsave; } }
        public static RoutedCommand ControlEnterkeypress { get { return controlenterkeypress; } }
       
        /// <summary>
        /// Escape 用于取消操作手势
        /// </summary>
        public static RoutedCommand Escape { get { return escape; } }
        public static RoutedCommand ControlE { get { return controle; } }
        public static RoutedCommand ControlM { get { return controlm; } }

        private static RoutedCommand controlsave = new RoutedCommand();

        private static RoutedCommand escape = new RoutedCommand();
        private static RoutedCommand controlenterkeypress = new RoutedCommand();
        private static RoutedCommand controle = new RoutedCommand();
        private static RoutedCommand controlm = new RoutedCommand();
    }
}
