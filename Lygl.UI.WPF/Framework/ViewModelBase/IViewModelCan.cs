using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lygl.UI.Framework.ViewModelBase
{
    public interface  IViewModelCan
    {
        bool CanSave { get; }
        bool CanDelete { get; }
        bool CanCancel { get; }
    }
}
