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

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    //[Export(typeof(ICommandMessage))]
    //public class ModifyAreaMessage : CommandMessageBase
    //{
    //    public ModifyAreaMessage() : base() { Name = "EditArea"; Label = "编辑墓区"; ToolTip = "编辑墓区"; Group = "EditArea"; }
    //}

    //[Export(typeof(ICommandMessage))]
    //public class SaveAreaDataMessage : CommandMessageBase
    //{

    //    public SaveAreaDataMessage() : base() { Name = "SaveAreaData"; Label = "保存"; ToolTip = "保存修改的墓区数据"; Group = "EditArea"; }
    //    public override bool IsCanExecute()
    //    {
    //        if (ViewModel == null) return false;
    //        return (ViewModel as EditAreaViewModel).CanSave;
    //    }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class CancelAreaDataMessage : CommandMessageBase
    //{

    //    public CancelAreaDataMessage() : base() { Name = "CancelAreaData"; Label = "取消"; ToolTip = "不保存修改的墓区数据"; Group = "EditArea"; }
    //    public override bool IsCanExecute()
    //    {
    //        if (ViewModel == null) return false;
    //        return (ViewModel as EditAreaViewModel).Model.IsDirty;
    //    }
    //}
    #endregion 

    [Export(typeof(SzListItemViewModel))]
    class SzListItemViewModel : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<MxSzEdit>
        //,
        //IHandle<ModifyAreaMessage>, IHandle<SaveAreaDataMessage>, IHandle<CancelAreaDataMessage>
    {
        public bool IsEdit {get;set;}

        public SzListItemViewModel(MxSzEdit sz, bool isEdit = false)
        {
            Model = sz;
            ManageObjectLifetime = false;
            
            //IoC.Get<IEventAggregator>().Subscribe(this);
            IsEdit = isEdit;            
        }

        protected override void OnDeactivate(bool close)
        {
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }
     

        //#region 命令处理
        //public IList<ICommandMessage> toolBar
        //{
        //    get
        //    {
        //        IoC.Get<ICommandMessageAggregator>().SetGroupViewModel("EditArea", this);
        //        return IoC.Get<ICommandMessageAggregator>().GetGroup("EditArea");
        //    }
        //}
        ////public void Handle(ModifyAreaMessage message)
        ////{
        ////    IsEdit = true;
        ////    NotifyOfPropertyChange("IsEdit");
        ////}
        ////public void Handle(SaveAreaDataMessage message)
        ////{
        ////    if (Model.IsSavable)
        ////    {
        ////        Model.ApplyEdit();
        ////        Model=Model.Save(true);
        ////        _shapeData.Name = Model.Name;
        ////        _shapeData.DataChanged();
        ////        this.TryClose(true);
        ////    }
        ////}
        ////public void Handle(CancelAreaDataMessage message)
        ////{
        ////    this.TryClose(false);

        ////}


        //#endregion 

    }
}
