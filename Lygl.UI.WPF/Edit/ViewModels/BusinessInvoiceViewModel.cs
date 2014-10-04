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
using Lygl.DalLib.Invoice;
using Lygl.DalLib.Browse;
using Lygl.UI.Framework;
using Csla.Data;
using System.Data.SqlClient;
using Csla.Core;
using Lygl.UI.Report;
using Csla;
using Lygl.DalLib.UserManager;
using Lygl.DalLib.Util;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    [Export(typeof(ICommandMessage))]
    public class LookNotPayInvoiceItemMessage : CommandMessageBase
    {
        public LookNotPayInvoiceItemMessage() : base() { Name = "LookNotPayInvoiceItem"; Label = "查看未付款"; Group = "Invoice"; ToolTip = "查看未付款"; Category = "收费"; }
    }
    [Export(typeof(ICommandMessage))]
    public class AddInvoiceMessage : AddCommandMessageBase
    {
        public AddInvoiceMessage() : base() { Name = "AddInvoice"; Label = "添加"; Group = "Invoice"; ToolTip = "添加新的发票"; Category = "收费"; }
    }
    [Export(typeof(ICommandMessage))]
    public class SaveInvoiceMessage : SaveCommandMessageBase
    {
        public SaveInvoiceMessage() : base() { Name = "SaveInvoice"; Label = "保存"; ToolTip = "保存修改的发票"; Group = "Invoice"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelInvoiceMessage : CancelCommandMessageBase
    {
        public CancelInvoiceMessage() : base() { Name = "CancelInvoice"; Label = "取消"; ToolTip = "不保存修改的发票"; Group = "Invoice"; }
    }

    [Export(typeof(ICommandMessage))]
    public class PrintInvoiceMessage : CommandMessageBusinessBase
    {
        public PrintInvoiceMessage() : base() { Name = "PrintInvoice"; Label = "打印"; ToolTip = "打印当前发票"; Group = "Invoice"; Category = "收费"; }

        public override bool IsCanExecute()
        {
            if (IoC.Get<IGlobalData>().CurrentMx == null) return false;
            if (ViewModel == null) return false;
            if ((ViewModel as BusinessInvoiceViewModel).IsEdit) return false;
            return true;
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyInvoiceMessage : ModifyCommandMessageBase
    {
        public ModifyInvoiceMessage() : base() { Name = "ModifyInvoice"; Label = "修改"; ToolTip = "修改选择的发票"; Group = "Invoice"; Category = "收费"; }

       
    }

    //[Export(typeof(ICommandMessage))]
    //public class CreateNewInvoiceMessage : CommandMessageBusinessBase
    //{
    //    public CreateNewInvoiceMessage() : base() { Name = "CreateNewInvoice"; Label = "新发票"; ToolTip = "建立新的发票"; Group = "Invoice"; }

    //    public override bool IsCanExecute()
    //    {
    //        if (ROMx == null) return false;
    //        if (ViewModel == null) return false;
    //        if ((ViewModel as BusinessLbViewModel).IsEdit) return false;
    //        return true;
    //    }
    //}
    #endregion 

    //[Export(typeof(InvoiceViewModel))]
    public class BusinessInvoiceViewModel : BusinessWithMainListViewModel<InvoiceList,Invoice> ,
        IHandle<SaveInvoiceMessage>,
        IHandle<CancelInvoiceMessage>,
        IHandle<PrintInvoiceMessage>,
        IHandle<ModifyInvoiceMessage>,
        IHandle<AddInvoiceMessage>,
        IHandle<LookNotPayInvoiceItemMessage>
    {
        /// <summary>
        /// 用于正常浏览业务用
        /// 
        /// </summary>
        /// <param name="MxID"></param>
        /// <param name="isEdit"></param>
        //public BusinessInvoiceViewModel(Guid MxID)
        //{
        //    _currentMxID = MxID;
        //    DisplayName = "收费";
        //    Model = InvoiceList.GetInvoiceListByMxID(MxID).First();
        //    ItemList = InvoiceItemList.GetInvoiceItemListByMxID(MxID);

        //    //Model = InvoiceItemList.GetInvoiceItemListByMxID(MxID);
        //    //需要订阅和取消订阅
        //    IoC.Get<IEventAggregator>().Subscribe(this);
        //}
        public BusinessInvoiceViewModel(MxRO mx)
            : base(mx, "收费", "Invoice")
        {
        }
        //public BusinessInvoiceViewModel(MxRO mx, int businessType, Guid businessID)
        //    : base(mx,  "收费","Invoice")
        //{
        //    //if (businessType == 1)  //1预定收费
        //    //{
        //    //    ItemList = InvoiceItemEditList.GetInvoiceItemEditListByMxInvoiceTypeBusinessID(_currentMx.MxID, businessType, businessID);
        //    //    if (ItemList.Count != 1) throw new Exception("未找到发票项，或预定超过1次");

        //    //    InvoiceItemEdit ii = ItemList.First();
        //    //    if ((ii.InvoiceID == Guid.Empty))
        //    //    {
        //    //        bool canNew = false;
        //    //        //如果为空，则表示没有预定，如果进入则自动创建新的预定，需要权限
        //    //        if ((Csla.ApplicationContext.User.Identity as CustomIdentity).HavePermission("AddInvoice"))
        //    //        {
        //    //            canNew = true;
        //    //        }
        //    //        else
        //    //        {
        //    //            MessageBox.Show("没有添加新发票的权限！\n请与管理员联系。", "提示");
        //    //        }
        //    //        if (canNew)
        //    //        {
        //    //            Invoice tempModel = InvoiceList.GetInvoiceListByMxID(_currentMx.MxID).AddNew();
        //    //            tempModel.InvoiceAccount = ii.Price * ii.Quantity;
        //    //            tempModel.MxID = mx.MxID;
        //    //            tempModel.InvoiceTime = DateTime.Today;
        //    //            Model = tempModel;
        //    //            this.Refresh();
        //    //            IsEdit = true;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        Model = InvoiceList.GetInvoiceListByMxID(_currentMx.MxID).First();
        //    //        IsEdit = false;
        //    //    }
        //    //}
        //    //if (businessType == 5) //5立碑收费
        //    //{
        //    //    ItemList = InvoiceItemEditList.GetInvoiceItemEditListByMxInvoiceTypeBusinessID(_currentMx.MxID, businessType, businessID);
        //    //    if (ItemList.Count != 1) throw new Exception("未找到发票项，或预定超过1次");

        //    //    InvoiceItemEdit ii = ItemList.First();
        //    //    if ((ii.InvoiceID == Guid.Empty))
        //    //    {
        //    //       MainListVM.CMListAddNew();
        //    //        IsEdit = true;
        //    //    }
        //    //    else
        //    //    {
        //    //        Model = InvoiceList.GetInvoiceListByMxID(_currentMx.MxID).First();
        //    //        IsEdit = false;
        //    //    }
        //    //}
        //    ////需要订阅和取消订阅
        //    //IoC.Get<IEventAggregator>().Subscribe(this);
        //}
      
        #region Window Event handle
        //在这里实现基类没有实现的窗口响应事件

        
        #endregion 

        #region Binding view
        //在这里实现基类没有实现的绑定属性
        //表示发票的数据
        private InvoiceItemEditList _itemList;
        public InvoiceItemEditList ItemList
        {
            get
            {
                return _itemList;
            }
            set
            {
                _itemList = value;
                RaisePropertyChangedEventImmediately("ItemList");
            }
        }

        public void SelectContact()
        {
            EditContactViewModel um = new EditContactViewModel(_currentMx);
            IoC.Get<IGlobalData>().ShowDialog<EditContactViewModel>(um);
            if (um.Model != null)
            {
                Model.Drawee = um.Model.Name;
            }
        }

        protected override void OnModelChanged(Invoice oldValue, Invoice newValue)
        {
            base.OnModelChanged(oldValue, newValue);
            if (newValue == null) { _itemList = null; }
            else
            {
                if (newValue.IsNew)
                {
                    _itemList = InvoiceItemEditList.GetInvoiceItemEditListbyPayFlag(new CriteriaGetNotPay(_currentMx.MxID, false));
                }
                else
                {
                    _itemList = InvoiceItemEditList.GetInvoiceItemEditListByMxInvoice(newValue.InvoiceID, _currentMx.MxID);
                }
            }
            RaisePropertyChangedEventImmediately("ItemList");
        }

        public InvoiceItemEditList NotPayItemList
        {
            get { return InvoiceItemEditList.GetInvoiceItemEditListbyPayFlag(new CriteriaGetNotPay(_currentMx.MxID,false)); }
        }
        #endregion

        #region 命令处理
        public void Handle(LookNotPayInvoiceItemMessage message)
        {
            RaisePropertyChangedEventImmediately("NotPayItemList");
            //ItemList = InvoiceItemEditList.GetInvoiceItemEditListbyPayFlag(_currentMx.MxID);
        }
        public void Handle(AddInvoiceMessage message)
        {
            base.CMListAddNew();
        }
        public override void InitNewItem(Invoice newModel)
        {
            base.InitNewItem(newModel);
            newModel.MxID = _currentMx.MxID;
            newModel.OperatorCode = SecurityHelper.GetCurrentUserCode();
            newModel.InvoiceAccount = GetNewInvoiceAccount();
            newModel.InvoiceTime = DateTime.Now;
        }

        private decimal GetNewInvoiceAccount()
        {
            decimal total = 0;

            ItemList  = InvoiceItemEditList.GetInvoiceItemEditListbyPayFlag(new CriteriaGetNotPay(_currentMx.MxID,false));
            foreach (var item in ItemList)
            {
                //if (!item.PayFlag)
                    total += item.Price * item.Quantity;
            }
            return total;
        }
        public void Handle(SaveInvoiceMessage message)
        {
            base.CMSaveListWithTrans();
                 //using (var ctx = TransactionManager<SqlConnection, SqlTransaction>.GetManager("LyglDB"))
                 //{
                 //    try
                 //    {

                 //        base.CMSaveList();
                 //        ctx.Commit();
                 //    }
                 //    catch (Exception e)
                 //    {
                 //        ctx.Transaction.Rollback();
                 //        throw e;
                 //    }
                 //}
            RaisePropertyChangedEventImmediately("NotPayItemList");
        }
        /// <summary>
        /// 通过订阅事件，响应model保存后，保存相关列表数据
        /// </summary>
        public override void CurrentModelItemSaved()
        {
            foreach (var item in ItemList)
            {
                item.InvoiceID = Model.InvoiceID;
                item.PayFlag = true;

                //更新业务的付款标志
                UpdateBusinessPayFlag(item);
            }
            InvoiceItemEditList tempList = ItemList.Clone();
            ItemList = tempList.Save();
        }

        private void UpdateBusinessPayFlag(InvoiceItemEdit invoiceItem)
        {
            if (invoiceItem.ItemTypeID == 1)
            {
                BusinessYd business = DataPortal.Fetch<BusinessYd>(invoiceItem.BusinessID);
                business.PayFlag = true;
                business.Save();
            }

            //if (invoiceItem.ItemTypeID == 20)
            //{
            //    BusinessQtsfList qtsfList = DataPortal.Fetch<BusinessQtsfList>(invoiceItem.MxID);
            //    BusinessQtsf business = qtsfList.FindBusinessQtsfByBusinessID(invoiceItem.BusinessID);
            //    business.PayFlag = true;
            //    //business.Save();
            //    qtsfList.Save();
            //}

        }
        public void Handle(ModifyInvoiceMessage message)
        {
            base.CMModifyItem();
        }
        public void Handle(PrintInvoiceMessage message)
        {
            ReportInvoiceDataHelper dataHelper = new ReportInvoiceDataHelper(this);
            PrintDialogViewModel um = new PrintDialogViewModel(dataHelper);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "Height", 300 }, { "Width", 300 }, { "WindowState", WindowState.Normal } };
            IoC.Get<IWindowManager>().ShowWindow(um, null, settings);
        }
        public void Handle(CancelInvoiceMessage message)
        {
            base.CMCancelList();
        }
        #endregion 
    }
}
