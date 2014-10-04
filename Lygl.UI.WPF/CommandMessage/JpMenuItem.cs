using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Lygl.UI.CommandMessage
{
    public class JpMenuItem:MenuItem
    {
        ICommandMessage _cm;
        public JpMenuItem(ICommandMessage cm)
            : base()
        {
            _cm = cm;
            this.Click += new System.Windows.RoutedEventHandler(JpMenuItem_Click);
            this.IsEnabled = _cm.CanExecute;
            this.Header = _cm.Label;
        }

        

        void JpMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this._cm.Execute();
            e.Handled = true;
        }



    }
}
