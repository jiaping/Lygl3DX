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
using System.Data.SqlClient;
using System.Windows.Media;
using Csla.Data;
using System.Configuration;
using Lygl.DalLib.Browse;
using Lygl.UI.Shell;
using Lygl.UI.ViewModels;
 
using Lygl.DalLib.Util;
using Lygl.UI.Framework;
using Lygl.UI.Report;
using Lygl.UI.Framework.ViewModelBase;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message

    [Export(typeof(ICommandMessage))]
    public class DispBusinessQtsfMessage : MxBusinessCMBase
    {
        public DispBusinessQtsfMessage() : base() { Name = "DispBusinessQtsf"; Label = "其它收费"; ToolTip = "显示其它收费业务处理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }

        //public override bool IsCanExecute()
        //{
        //    return true;// MxID == Guid.Empty ? false : true;
        //}
    }
    [Export(typeof(ICommandMessage))]
    public class AddBusinessQtsfMessage : AddCommandMessageBase
    {
        public AddBusinessQtsfMessage() : base() { Name = "AddBusinessQtsf"; Label = "添加"; ToolTip = "添加其它收费费业务"; Group = "BusinessQtsf"; Category = "其它收费"; }


    }
    [Export(typeof(ICommandMessage))]
    public class SaveBusinessQtsfMessage : SaveCommandMessageBase
    {
        public SaveBusinessQtsfMessage() : base() { Name = "SaveBusinessQtsf"; Label = "保存"; ToolTip = "保存其它收费业务的修改"; Group = "BusinessQtsf";  }
        //public override bool IsCanExecute()
        //{
        //    if (ViewModel == null) return false;
        //    if ((ViewModel as BusinessQtsfViewModel).Model == null) return false;
        //    return (ViewModel as BusinessQtsfViewModel).CanSave;
        //}
    }
    [Export(typeof(ICommandMessage))]
    public class CancelBusinessQtsfMessage : CancelCommandMessageBase
    {

        public CancelBusinessQtsfMessage() : base() { Name = "CancelBusinessQtsf"; Label = "取消"; ToolTip = "不保存其它收费业务的修改"; Group = "BusinessQtsf"; }
        //public override bool IsCanExecute()
        //{
        //    if (ViewModel == null) return false;
        //    if ((ViewModel as BusinessQtsfViewModel).Model == null) return false;
        //    return (ViewModel as BusinessQtsfViewModel).Model.IsDirty;
        //}
    }
    //[Export(typeof(ICommandMessage))]
    //public class ModifyBusinessQtsfMessage : ModifyCommandMessageBase
    //{
    //    public ModifyBusinessQtsfMessage() : base() { Name = "ModifyBusinessQtsf"; Label = "修改"; ToolTip = "修改其它收费业务"; Group = "BusinessQtsf"; Category = "其它收费"; }

    //    public override bool IsCanExecute()
    //    {
    //        bool baseResult = base.IsCanExecute();
    //        if (!baseResult) return false;
    //        if (((ViewModel as IHaveModel).Model as BusinessQtsf).PayFlag) return false;
    //        return true;
    //    }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class DeleteQtsfMessage : DeleteCommandMessageBase
    //{

    //    public DeleteQtsfMessage() : base() { Name = "DeleteQtsf"; Label = "删除"; ToolTip = "编辑其它收费业务"; Group = "BusinessQtsf"; Category = "其它收费"; }
    //}
    #endregion 

    //[Export(typeof(BusinessQtsfViewModel))]
    public class BusinessQtsfViewModel : BusinessWithMainListViewModel<BusinessQtsfList, BusinessQtsf>,
        //   IHandle<ModifyBusinessQtsfMessage>,//   , IHandle<DeleteQtsfMessage>
        IHandle<AddBusinessQtsfMessage>,
        IHandle<SaveBusinessQtsfMessage>,
        IHandle<CancelBusinessQtsfMessage>    
    {
        public BusinessQtsfViewModel(MxRO currentMx)
            :base(currentMx,"其它收费",new string[] {"BusinessQtsf","InvoiceShare"})
        {
            ////if Lb is empty,return default
            //Model = BusinessQtsf.GetBusinessLbByMxID(_currentMx.MxID);
            ////如果为空，则新建
            //if (Model.BusinessID == Guid.Empty)
            //{
            //    Model= SetupNewBusinessLb();
            //    Model=SetupNewBusinessLb();
            //    IsEdit = true;
            //}
        }
        private BusinessQtsf SetupNewBusinessQtsf()
        {
            ////qtsf = BusinessQtsf.NewBusinessQtsf();
            //Model.BusinessName = "其它收费";
            //Model.MxID = _currentMx.MxID;
            //Model.OperatorCode = SecurityHelper.GetCurrentUserCode();
            //////Model.OperatorID = IoC.Get<IGlobalData>().CurrentUser.UserID;
            //Model.OperateTime = DateTime.Today.ToString();

            foreach (var item in ProductQtsfColl.GetProductQtsfColl())
            {
                QtsfItem qtsfItem = Model.QtsfItems.AddNew();
                qtsfItem.QtsfItemID = Guid.NewGuid();
                qtsfItem.Name = item.Name;
                qtsfItem.UnitPrice = item.UnitPrice;
                qtsfItem.Unit = item.Unit;
                qtsfItem.Quantity = 1;
                qtsfItem.BusinessID = Model.BusinessID;
                qtsfItem.SubTotal = item.UnitPrice;
            }
            return Model;
        }

        public override void InitNewItem(BusinessQtsf newModel)
        {
            base.InitNewItem(newModel);
            newModel.MxID = _currentMx.MxID;
            newModel.OperatorCode = SecurityHelper.GetCurrentUserCode();// IoC.Get<IGlobalData>().CurrentUser.Code;
            newModel.BusinessName = "其它收费";
            newModel.OperateTime = DateTime.Now.ToShortDateString();
        }

        #region Window Event handle
        //可在这里实现基类没有实现的窗口响应事件　
        #endregion 

        #region Binding view
        //可在这里实现基类没有实现的绑定属性

        public void AddContact()
        {
            EditContactViewModel um = new EditContactViewModel(_currentMx);
            IoC.Get<IGlobalData>().ShowDialog<EditContactViewModel>(um);
            if (um.Model != null)
            {
                Model.Drawee = um.Model.Name;
            }
        }
      

        public UserNameNVL UserNameList
        {
            get
            {
                return UserNameNVL.GetUserNameNVL();
            }
        }



        public override void NotifyOfPropertyChange(string propertyName)
        {
            base.NotifyOfPropertyChange(propertyName);
        }
        #endregion

        #region 命令处理
        public void Handle(AddBusinessQtsfMessage message)
        {
            base.CMListAddNew();
            SetupNewBusinessQtsf();
        }
       
        public void Handle(SaveBusinessQtsfMessage message)
        {
            base.CMSaveList();
            //add invoiceItem and save
            AddOrUpdateInvoiceItem(Model);
            //更新墓穴状态，更新形状显示，更新全局缓存,自动更新Graphy
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        public void Handle(CancelBusinessQtsfMessage message)
        {
            base.CMCancelList();
            //if (Model.IsNew)
            //{
            //    base.CMDelete();
            //}
        }
        //public void Handle(ModifyBusinessQtsfMessage message)
        //{
        //    base.CMModifyItem();
        //    IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        //}
        //public void Handle(DeleteQtsfMessage message)
        //{
        //    base.CMListDeleteItem(Model);
        //        IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        //}
        //public void Handle(DisLbInvoiceItemMessage message)
        //{
        //    //BusinessInvoiceViewModel um = new BusinessInvoiceViewModel(IoC.Get<IGlobalData>().CurrentMx.MxID);
        //    BusinessInvoiceViewModel um = new BusinessInvoiceViewModel(IoC.Get<IGlobalData>().CurrentMx, 5, Model.BusinessID);
        //    Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
        //    IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
       
        //}
       
        #endregion 

        private void AddOrUpdateInvoiceItem(BusinessQtsf model)
        {
            InvoiceItemEditList itemList = InvoiceItemEditList.GetInvoiceItemEditListByMxInvoiceTypeBusinessID(model.MxID,20,model.BusinessID);
            //InvoiceItemEdit item = itemList.FindInvoiceItemEditByBusinessID(model.BusinessID);
            //if (item == null )
            //{
            //    item = itemList.AddNew();
            //}

            //这里只增加发票子项目，不管发票，在收费窗口中，发票子项目的invoceID为空，自动增加发票
            //InvoiceList il = InvoiceList.GetInvoiceListByMxID(model.MxID);
            //Invoice invoice = il.FindInvoiceByInvoiceID(item.InvoiceID?? Guid.Empty);
            //if (invoice == null)
            //{
            //    invoice = il.AddNew();
            //}

            if (itemList.Count > 0)
            {
                itemList.Clear();
            }
            decimal totalprice = 0;
            foreach (var qtsfitem in model.QtsfItems)
            {
                InvoiceItemEdit item=itemList.AddNew();
                item.BusinessName = qtsfitem.Name;
                item.Price = qtsfitem.UnitPrice;
                item.Quantity = qtsfitem.Quantity;
                item.ItemTypeID = 20;  //20-其它收费
                item.BusinessID = model.BusinessID;
                item.MxID = model.MxID;
                item.PayFlag = false;
                item.InvoiceID = Guid.Empty;
                totalprice += qtsfitem.SubTotal;
            }

            //invoice.InvoiceAccount = totalprice;
            //invoice.MxID = model.MxID;
            //invoice.InvoiceTime = DateTime.Today;
            //Invoice invoiceTemp = invoice.Clone();


            using (var ctx = ConnectionManager<SqlConnection>.GetManager(ConfigurationManager.ConnectionStrings["LyglDB"].ConnectionString, false))
            {
                using (SqlTransaction st = ctx.Connection.BeginTransaction())
                {
                    try
                    {
                        
                        //invoice = invoiceTemp.Save();
                        //item.InvoiceID = invoice.InvoiceID;
                        InvoiceItemEditList itemListTemp = itemList.Clone();
                        itemList = itemListTemp.Save();
                        st.Commit();
                    }
                    catch (Exception e)
                    {
                        st.Rollback();
                        throw e;
                    }
                }
            }
            

        }
    }
}
