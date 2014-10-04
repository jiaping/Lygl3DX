using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lygl.UI.Framework
{
    public interface IWorkspace
    {
        string Icon { get; }
        string IconName { get; }
        string Status { get; }

        void Show();
    }
}
