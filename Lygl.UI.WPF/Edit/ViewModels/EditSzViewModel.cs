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
using Csla.Core;
using Lygl.UI.Framework;
using Lygl.DalLib.UserManager;
using Lygl.DalLib.Util;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message   

    [Export(typeof(ICommandMessage))]
    public class SaveSzMessage : SaveCommandMessageBase
    {

        public SaveSzMessage() : base() { Name = "SaveSz"; Label = "保存"; ToolTip = "保存修改的逝者信息"; Group = "SzEdit"; }
        
    }
    [Export(typeof(ICommandMessage))]
    public class CancelSzMessage : CancelCommandMessageBase
    {

        public CancelSzMessage() : base() { Name = "CancelSz"; Label = "取消"; ToolTip = "不保存修改的逝者信息"; Group = "SzEdit"; }
     
    }
    [Export(typeof(ICommandMessage))]
    public class AddSzMessage : AddCommandMessageBase
    {
        public AddSzMessage() : base() { Name = "AddSz"; Label = "添加"; ToolTip = "添加逝者"; Group = "SzEdit"; Category = "安葬"; }

    }
    [Export(typeof(ICommandMessage))]
    public class ModifySzMessage : ModifyCommandMessageBase
    {

        public ModifySzMessage() : base() { Name = "ModifySz"; Label = "编辑"; ToolTip = "编辑逝者信息"; Group = "SzEdit"; Category = "安葬"; }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteSzMessage : DeleteCommandMessageBase
    {

        public DeleteSzMessage() : base() { Name = "DeleteSz"; Label = "删除"; ToolTip = "编辑逝者信息"; Group = "SzEdit"; Category = "安葬"; }
    }
    #endregion 

    [Export(typeof(EditSzViewModel))]
    public class EditSzViewModel : CommonViewModelWithMainList<MxSzEditList, MxSzEdit>,
        IHandle<ModifySzMessage>, 
        IHandle<SaveSzMessage>, 
        IHandle<CancelSzMessage>,
        IHandle<AddSzMessage>,
        IHandle<DeleteSzMessage>
    {
        public EditSzViewModel(MxRO mx)
            : base(mx, "SzEdit", "逝者安葬")
        {
        }



        #region NameValueList
        public SexNVL SexList
        {
            get
            {
                return SexNVL.GetSexNVL();
            }
        }

        #endregion

        #region 命令处理
       
        public void Handle(SaveSzMessage message)
        {
            CMSaveList();
            //更新墓穴状态，更新形状显示，更新全局缓存,自动更新Graphy
                IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
            ////如果逝者数达到最大，更新墓穴状态为“已安葬”，更新形状显示，更新全局缓存,自动更新Graphy
            //int maxXs = MxXsNVL.GetMxXsNVL().Value(_currentMx.MxTypeID);
            //if (MainListVM.Model.Count == maxXs)
            //    IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx, "安葬");
        }
        public void Handle(CancelSzMessage message)
        {
            CMCancelList();
        }
        public void Handle(AddSzMessage message)
        {
            int maxXs =MxXsNVL.GetMxXsNVL().Value(_currentMx.MxTypeID);
            if (MainListVM.Model.Count < maxXs)
            {
                CMListAddNew();
            }
            else
            {
                MessageBox.Show("已达设置的最大逝者数，不能添加新的逝者！");
            }
           
        }
        public void Handle(ModifySzMessage message)
        {
            CMModifyItem();
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }

         public void Handle(DeleteSzMessage message)
        {
            CMListDeleteItem(Model);
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        #endregion 

        public override void InitNewItem(MxSzEdit newModel)
        {
            base.InitNewItem(newModel);
            newModel.RmDate = DateTime.Today;
            newModel.MxID = _currentMx.MxID;
            newModel.OperatorCode = SecurityHelper.GetCurrentUserCode();
            newModel.OperateTime = DateTime.Today.ToString();
        }
    }
}
