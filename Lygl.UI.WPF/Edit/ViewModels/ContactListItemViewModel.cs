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
    [Export(typeof(ContactListItemViewModel))]
    class ContactListItemViewModel : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<ContactEdit>
    {
        public bool IsEdit {get;set;}
        public ContactListItemViewModel(ContactEdit contact, bool isEdit = false)
        {
            Model = contact;
            //IoC.Get<IEventAggregator>().Subscribe(this);
            IsEdit = isEdit;            
        }
        public ContactListItemViewModel(MxRO mx)
        {
            ContactEditList _list = ContactEditList.GetContactEditListByMxID(mx.MxID);
            Model = _list.AddNew();
           
            Model.MxID = mx.MxID;
            Model.Name = "新联系人";
            IoC.Get<IEventAggregator>().Subscribe(this);
            IsEdit = true;
        }

        protected override void OnDeactivate(bool close)
        {
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }
     


    }
}
