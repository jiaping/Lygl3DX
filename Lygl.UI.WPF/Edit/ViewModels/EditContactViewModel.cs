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
using System.ComponentModel;
using Lygl.UI.Edit.Views;
using System.Windows;
using Lygl.UI.Framework;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    //[Export(typeof(ICommandMessage))]
    //public class ModifyAreaMessage : CommandMessageBase
    //{
    //    public ModifyAreaMessage() : base() { Name = "EditArea"; Label = "编辑墓区"; ToolTip = "编辑墓区"; Group = "EditArea"; }
    //}

    [Export(typeof(ICommandMessage))]
    public class SaveContactMessage : SaveCommandMessageBase
    {
        public SaveContactMessage() : base() { Name = "SaveContact"; Label = "保存"; ToolTip = "保存修改的联系人信息"; Group = "ContactEdit"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelContactMessage : CancelCommandMessageBase
    {

        public CancelContactMessage() : base() { Name = "CancelContact"; Label = "取消"; ToolTip = "不保存修改的联系人信息"; Group = "ContactEdit"; }
    }
    [Export(typeof(ICommandMessage))]
    public class AddContactMessage : AddCommandMessageBase
    {
        public AddContactMessage() : base() { Name = "AddContact"; Label = "添加"; ToolTip = "添加联系人"; Group = "ContactEdit"; Category = "联系人"; }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyContactMessage : ModifyCommandMessageBase
    {
        public ModifyContactMessage() : base() { Name = "ModifyContact"; Label = "编辑"; ToolTip = "编辑联系人信息"; Group = "ContactEdit"; Category = "联系人"; }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteContactMessage : DeleteCommandMessageBase
    {
        public DeleteContactMessage() : base() { Name = "DeleteContact"; Label = "删除"; ToolTip = "删除当前联系人信息"; Group = "ContactEdit"; Category = "联系人"; }
    }
    #endregion 

    [Export(typeof(EditContactViewModel))]
    class EditContactViewModel : CommonViewModelWithMainList<ContactEditList, ContactEdit>,
        IHandle<ModifyContactMessage>, 
        IHandle<SaveContactMessage>, 
        IHandle<CancelContactMessage>,
        IHandle<AddContactMessage>,
        IHandle<DeleteContactMessage>
    {
        public EditContactViewModel(MxRO mx)
            : base(mx, "ContactEdit", "联系人")
        {
        }
        public YszgxNVL YszgxList
        {
            get
            {
                return YszgxNVL.GetYszgxNVL();
            }
        }
        #region 命令处理
        public void Handle(SaveContactMessage message)
        {
            CMSaveList();
        }
        
        public void Handle(CancelContactMessage message)
        {
            CMCancelList();
        }
        public void Handle(AddContactMessage message)
        {
            CMListAddNew();
        }
        public void Handle(ModifyContactMessage message)
        {
            CMModifyItem();
        }
        public void Handle(DeleteContactMessage message)
        {
            CMListDeleteItem(this.Model);
        }


        #endregion 

        public override void InitNewItem(ContactEdit newModel)
        {
            base.InitNewItem(newModel);
            newModel.MxID = _currentMx.MxID;
        }
    }
}
