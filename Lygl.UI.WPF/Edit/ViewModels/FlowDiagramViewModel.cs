using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.DalLib.Edit;
 
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Caliburn.Micro;
using Lygl.DalLib.Core;
using Lygl.DalLib.NVL;
using Lygl.DalLib.Browse;

namespace Lygl.UI.Edit.ViewModels
{
    [Export(typeof(FlowDiagramViewModel))]
    class FlowDiagramViewModel : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<MxRO>
    {
        public bool IsEdit {get;set;}
        public FlowDiagramViewModel(MxRO mx)
        {
            Model = mx;
            IoC.Get<IEventAggregator>().Subscribe(this);
            IsEdit = true;
        }

        protected override void OnDeactivate(bool close)
        {
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }

        public void DispYd()
        {
            IoC.Get<IEventAggregator>().Publish(new Lygl.UI.Framework.DispBusinessYdMessage(), Execute.OnUIThread);
        }

        public void DispGm()
        {
            IoC.Get<IEventAggregator>().Publish(new Lygl.UI.Framework.DispBusinessGmMessage(), Execute.OnUIThread);
        }

        public void DispGlf()
        {
            IoC.Get<IEventAggregator>().Publish(new DispBusinessGlfMessage(),Execute.OnUIThread);
        }

    }
}
