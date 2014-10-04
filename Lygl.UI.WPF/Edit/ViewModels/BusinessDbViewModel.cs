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
using Lygl.DalLib.Business;
using System.Windows;
using Lygl.UI.Edit.Views;
using System.ComponentModel;
using Lygl.UI.Framework;
using Lygl.DalLib.Browse;
using Lygl.DalLib.UserManager;
using Lygl.DalLib.Util;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    /// <summary>
    /// 显示于墓穴窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispBusinessDdMessage : MxBusinessCMBase
    {
        public DispBusinessDdMessage() : base() { Name = "DispBusinessDd"; Label = "点灯"; ToolTip = "显示点灯业务处理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }

        //public override bool IsCanExecute()
        //{
        //    return true;// MxID == Guid.Empty ? false : true;
        //}
    }
    /// <summary>
    /// 显示于点灯业务窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class SaveBusinessDdMessage : SaveCommandMessageBase
    {
        public SaveBusinessDdMessage() : base() { Name = "SaveBusinessDd"; Label = "保存"; ToolTip = "保存修改的点灯业务"; Group = "BusinessDd"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelBusinessDdMessage : CancelCommandMessageBase
    {

        public CancelBusinessDdMessage() : base() { Name = "CancelBusinessDd"; Label = "取消"; ToolTip = "不保存修改的点灯业务"; Group = "BusinessDd"; }
    }

    

    [Export(typeof(ICommandMessage))]
    public class AddBusinessDdMessage : AddCommandMessageBase
    {
        public AddBusinessDdMessage() : base() { Name = "AddBusinessDd"; Label = "添加"; ToolTip = "添加新点灯业务"; Group = "BusinessDd"; Category = "点灯";}

        public override bool IsCanExecute()
        {
            if (IoC.Get<IGlobalData>().CurrentMx == null) return false;
            if (ViewModel == null) return false;
            if ((ViewModel as BusinessDdViewModel).IsEdit) return false;
            return true;
        }
    }
   [Export(typeof(ICommandMessage))]
    public class DeleteBusinessDdMessage : DeleteCommandMessageBase
    {
       public DeleteBusinessDdMessage() : base() { Name = "DeleteBusinessDd"; Label = "删除"; ToolTip = "删除点灯业务"; Group = "BusinessDd"; Category = "点灯"; }

     }
    [Export(typeof(ICommandMessage))]
    public class ModifyBusinessDdMessage : ModifyCommandMessageBase
    {

        public ModifyBusinessDdMessage() : base() { Name = "ModifyBusinessDd"; Label = "修改"; ToolTip = "修改选择的点灯业务"; Group = "BusinessDd"; Category = "点灯"; }

    }
    #endregion 

    [Export(typeof(BusinessDdViewModel))]
    class BusinessDdViewModel : BusinessWithMainListViewModel<BusinessDdList, BusinessDd>,
        IHandle<AddBusinessDdMessage>,
        IHandle<ModifyBusinessDdMessage>,
        IHandle<SaveBusinessDdMessage>,
        IHandle<CancelBusinessDdMessage>,
        IHandle<DeleteBusinessDdMessage>
    {
        /// <summary>
        /// 用于正常浏览业务用
        /// 
        /// </summary>
        /// <param name="MxID"></param>
        /// <param name="isEdit"></param>
        public BusinessDdViewModel(MxRO mx)
            :base(mx,"BusinessDd")
        {
            DisplayName = "点灯业务";
        }

        #region Window Event handle
        //在这里实现基类没有实现的窗口响应事件
        #endregion 

        #region Binding view
        //在这里实现基类没有实现的绑定属性
        #endregion

        #region 命令处理

        public void Handle(AddBusinessDdMessage message)
        {
            base.CMListAddNew();
        }
        public void Handle(DeleteBusinessDdMessage message)
        {
            base.CMListDeleteItem(Model);
        }
        public override void InitNewItem(BusinessDd newModel)
        {
            base.InitNewItem(newModel);
            newModel.MxID = _currentMx.MxID;
            newModel.OperatorCode = SecurityHelper.GetCurrentUserCode();// IoC.Get<IGlobalData>().CurrentUser.Code;
            newModel.BusinessName = "点灯";
            newModel.OperateTime = DateTime.Now;
        }
       
        public void Handle(ModifyBusinessDdMessage message)
        {
            base.CMModifyItem();
        }
        public void Handle(SaveBusinessDdMessage message)
        {
            base.CMSaveList();
        }

        public void Handle(CancelBusinessDdMessage message)
        {
            base.CMCancelList();
        }
        #endregion 

    }
}
