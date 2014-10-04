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
using Lygl.DalLib.UserManager;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message

   
    /// <summary>
    /// 保存立碑业务的修改
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class SaveBusinessLbMessage : SaveCommandMessageBase
    {
        public SaveBusinessLbMessage() : base() { Name = "SaveBusinessLb"; Label = "保存"; ToolTip = "保存立碑业务的修改"; Group = "BusinessLb"; }
        //public override bool IsCanExecute()
        //{
        //    if (ViewModel == null) return false;
        //    if ((ViewModel as BusinessLbViewModel).Model == null) return false;
        //    return (ViewModel as BusinessLbViewModel).CanSave;
        //}
    }
    [Export(typeof(ICommandMessage))]
    public class CancelBusinessLbMessage : CancelCommandMessageBase
    {

        public CancelBusinessLbMessage() : base() { Name = "CancelBusinessLb"; Label = "取消"; ToolTip = "不保存立碑业务的修改"; Group = "BusinessLb"; }
        //public override bool IsCanExecute()
        //{
        //    if (ViewModel == null) return false;
        //    if ((ViewModel as BusinessLbViewModel).Model == null) return false;
        //    return (ViewModel as BusinessLbViewModel).Model.IsDirty;
        //}
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyBusinessLbMessage : BusinessModifyCommandMessageBase
    {
        public ModifyBusinessLbMessage() : base() { Name = "ModifyBusinessLb"; Label = "修改"; ToolTip = "修改立碑业务"; Group = "BusinessLb"; Category = "立碑"; }

        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            if ((ViewModel as BusinessLbViewModel).IsEdit) return false;
            if ((ViewModel as BusinessLbViewModel).Model==null) return false;
            return true;
        }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteLbMessage : DeleteCommandMessageBase
    {

        public DeleteLbMessage() : base() { Name = "DeleteLb"; Label = "删除"; ToolTip = "编辑立碑信息"; Group = "BusinessLb";  Category = "立碑"; }
    }

    [Export(typeof(ICommandMessage))]
    public class PrintLbMessage : CommandMessageBusinessBase
    {
        public PrintLbMessage() : base() { Name = "PrintLb"; Label = "打印"; ToolTip = "打印碑文"; Group = "BusinessLb"; Category = "立碑"; }

        public override bool IsCanExecute()
        {
            ////if (MxID == Guid.Empty) return false;
            //if (ViewModel == null) return false;
            //if ((ViewModel as BusinessLbViewModel).IsEdit) return false;
            //if ((ViewModel as BusinessLbViewModel).Model == null) return false;
            return true;
        }
    }

    #endregion 

    //[Export(typeof(BusinessLbViewModel))]
    public class BusinessLbViewModel : Lygl.UI.Framework.BusinessSimpleViewModel<BusinessLb>,
        IHandle<ModifyBusinessLbMessage>,
        IHandle<SaveBusinessLbMessage>,
        IHandle<CancelBusinessLbMessage>,
        IHandle<PrintLbMessage>,
        IHandle<DeleteLbMessage>
    {
        public BusinessLbViewModel(MxRO currentMx)
            :base(currentMx,"墓穴立碑",new string[] {"BusinessLb","InvoiceShare"})
        {
            //if Lb is empty,return default
            Model = BusinessLb.GetBusinessLbByMxID( _currentMx.MxID);
            //如果为空，则新建
            if (Model.BusinessID == Guid.Empty)
            {
                Model= SetupNewBusinessLb();
                // Model = SetupNewBusinessLb();
                IsEdit = true;
            }
        }
        private BusinessLb SetupNewBusinessLb()
        {
            BusinessLb lb;
            lb = BusinessLb.NewBusinessLb();
            lb.BusinessName = "立碑";
            lb.MxID = _currentMx.MxID;
            lb.OperatorCode = SecurityHelper.GetCurrentUserCode();
            //Model.OperatorID = IoC.Get<IGlobalData>().CurrentUser.UserID;
            lb.OperateTime = DateTime.Today;
            lb.LbDate = DateTime.Today.ToString();

            int xs= MxXsNVL.GetMxXsNVL().Value(_currentMx.MxTypeID);
            for (int i = 0; i < xs; i++)
            {
                lb.BwSzs.AddNew();
            }
            foreach (var item in ProductLbColl.GetProductLbColl())
            {
                LbItem lbItem= lb.LbItems.AddNew();
                lbItem.LbItemID = Guid.NewGuid();
                lbItem.Name = item.Name;
                lbItem.UnitPrice = item.UnitPrice;
                lbItem.Unit = item.Unit;
                lbItem.Quantity = 1;
                //lbItem.SubTotal = item.UnitPrice;
            }
            return lb;
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
        public bool IsBtnKzwc
        {
            get
            {
                if (Model.Kzg==(Csla.ApplicationContext.User.Identity as CustomIdentity).User.Name)
                    return true;
                return false;
            }
        }
        public bool IsBtnSgwc
        {
            get
            {
                if (Model.Sgy == (Csla.ApplicationContext.User.Identity as CustomIdentity).User.Name)
                    return true;
                return false;
            }
        }
       
        public void btnKzwcClick()
        {
            Model.KzSj = DateTime.Today.ToString();
        }
        public void btnSgwcClick()
        {
            Model.SgSj = DateTime.Today.ToString();
        }
        public LbBxNVL LbBxList
        {
            get
            {
                return LbBxNVL.GetLbBxNVL();
            }
        }

        public UserNameNVL UserNameList
        {
            get
            {
                return UserNameNVL.GetUserNameNVL();
            }
        }

        public List<BwSzListItemView> BwSzItems
        {
            get
            {
                if (Model == null) return null;
                List<BwSzListItemView> list = new List<BwSzListItemView>();
                foreach (var item in Model.BwSzs)
                {
                        BwSzListItemView vm = (BwSzListItemView)Utils.GetBindingView(new BwSzListItemViewModel(item,IsEdit));
                    list.Add(vm);
                }
                //if (list.Count > 0) { SzGroupBoxHeader = "逝者信息"; } else { SzGroupBoxHeader = ""; }
                return list;
            }
        }

        public override void NotifyOfPropertyChange(string propertyName)
        {
            base.NotifyOfPropertyChange(propertyName);
            if (propertyName == "IsEdit")
            {
                base.NotifyOfPropertyChange("BwSzItems");
            }
        }
        #endregion

        #region 命令处理

        //public void Handle(AddBusinessLbMessage message)
        //{
        //    Model = _businessListVM.Model.AddNew();
        //    Model.MxID = _currentMxID;
        //    IsEdit = true;
        //}

        public void Handle(ModifyBusinessLbMessage message)
        {
            base.CMModify();
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        public void Handle(SaveBusinessLbMessage message)
        {
            base.CMSave();
            //add invoiceItem and save
            AddOrUpdateInvoiceItem(Model);
            //更新墓穴状态，更新形状显示，更新全局缓存,自动更新Graphy
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        public void Handle(CancelBusinessLbMessage message)
        {
            base.CMCancel();
            if (Model.IsNew)
            {
                base.CMDelete();
            }
        }
        public void Handle(DeleteLbMessage message)
        {
                base.CMDelete();
                IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        //public void Handle(DisLbInvoiceItemMessage message)
        //{
        //    //BusinessInvoiceViewModel um = new BusinessInvoiceViewModel(IoC.Get<IGlobalData>().CurrentMx.MxID);
        //    BusinessInvoiceViewModel um = new BusinessInvoiceViewModel(IoC.Get<IGlobalData>().CurrentMx, 5, Model.BusinessID);
        //    Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
        //    IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
       
        //}
        public void Handle(PrintLbMessage message)
        {
            BwReportDataHelper dataHelper = new BwReportDataHelper(this);
            PrintDialogViewModel um = new PrintDialogViewModel(dataHelper);
            Dictionary<string, object> settings = new Dictionary<string, object> { {"Height",300 },{"Width",300},{ "WindowState", WindowState.Normal }};
            IoC.Get<IWindowManager>().ShowWindow(um, null, settings);

        }
        #endregion 

        private void AddOrUpdateInvoiceItem(BusinessLb model)
        {
            InvoiceItemEditList itemList = InvoiceItemEditList.GetInvoiceItemEditListByMxID(model.MxID);
            InvoiceItemEdit item = itemList.FindInvoiceItemEditByBusinessID(model.BusinessID);
            if (item == null )
            {
                item = itemList.AddNew();
            }

            //这里只增加发票子项目，不管发票，在收费窗口中，发票子项目的invoceID为空，自动增加发票
            //InvoiceList il = InvoiceList.GetInvoiceListByMxID(model.MxID);
            //Invoice invoice = il.FindInvoiceByInvoiceID(item.InvoiceID?? Guid.Empty);
            //if (invoice == null)
            //{
            //    invoice = il.AddNew();
            //}

            decimal totalprice = 0;
            foreach (var lbitem in model.LbItems)
            {
                totalprice += lbitem.SubTotal;
            }

            item.BusinessName = "立碑";
            item.Price = totalprice;
            item.Quantity=1;
            item.ItemTypeID= 5;  //5-立碑
            item.BusinessID = model.BusinessID;
            item.MxID= model.MxID;
            item.PayFlag= false;
            item.InvoiceID = Guid.Empty;

            
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
