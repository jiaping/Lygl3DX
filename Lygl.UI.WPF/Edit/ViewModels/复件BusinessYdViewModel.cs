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
using Lygl.DalLib.UserManager;
using Lygl.UI.Framework;
using Lygl.UI.Framework.ViewModelBase;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message

    [Export(typeof(ICommandMessage))]
    public class AddBusinessYdMessage : AddCommandMessageBase
    {
        public AddBusinessYdMessage() : base() { Name = "AddBusinessYd"; Label = "添加"; ToolTip = "添加新的预定业务"; Category = "预定"; }
        public override bool IsCanExecute()
        {
            //这里要求该命令不显示在工具栏中，所以总为false;
            //因为它不以获得参数通知
            return false;
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyBusinessYdMessage : ModifyCommandMessageBase
    {
        public ModifyBusinessYdMessage() : base() { Name = "ModifyBusinessYd"; Label = "修改"; ToolTip = "修改预定业务"; Group = "BusinessYd"; Category = "预定"; }
        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
            if ((ViewModel as IHaveModel).Model == null) return false;
            if (((ViewModel as IHaveModel).Model as BusinessYd).PayFlag ) return false;
            //if  (ROMx == null || ROMx.MxStatusID > 0) return false;
            return base.IsCanExecute();
        }
    }
    /// <summary>
    /// 保存预定业务的修改
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class SaveBusinessYdMessage : SaveCommandMessageBase
    {
        public SaveBusinessYdMessage() : base() { Name = "SaveBusinessYd"; Label = "保存"; ToolTip = "保存预定业务的修改"; Group = "BusinessYd"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelBusinessYdMessage : CancelCommandMessageBase
    {
        public CancelBusinessYdMessage() : base() { Name = "CancelBusinessYd"; Label = "取消"; ToolTip = "不保存预定业务的修改"; Group = "BusinessYd"; }
    }
    
    

    #endregion 

    //[Export(typeof(BusinessYdViewModel))]
   public  class BusinessYdViewModel : BusinessSimpleViewModel<BusinessYd>,
        IHandle<ModifyBusinessYdMessage>,
        IHandle<SaveBusinessYdMessage>,
        IHandle<CancelBusinessYdMessage>
    {
        public BusinessYdViewModel(MxRO currentMx)
            : base(currentMx, "墓穴预定",new string [] { "BusinessYd","InvoiceShare"})
        {
            //if Yd is empty,return default
            Model = BusinessYd.GetBusinessYdByMxID(_currentMx.MxID, _currentMx.MxID);
            //如果为空，则新建
            if (Model.BusinessID == Guid.Empty)
            {
                if ((Csla.ApplicationContext.User.Identity as CustomIdentity).HavePermission((new AddBusinessYdMessage()).Name)){
                Model = BusinessYd.NewBusinessYd();
                Model.BusinessName = "预定";
                Model.MxID = currentMx.MxID;
                Model.Price = currentMx.Price??0;
                Model.OperatorCode =( Csla.ApplicationContext.User.Identity as CustomIdentity).User.Code;// IoC.Get<IGlobalData>().CurrentUser.Code;
                Model.OperateTime = DateTime.Today;
                Model.YDDate = DateTime.Today;
                Model.PayFlag = false;
                IsEdit = true;
                } else{
                    MessageBox.Show("");
                }
            }
        }
        #region Window Event handle
        protected override void OnDeactivate(bool close)
        {
            //需要订阅和取消订阅
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }
        public override void CanClose(Action<bool> callback)
        {
            if (Model != null && Model.IsDirty)
            {
                System.Windows.MessageBox.Show("正在修改数据，请先保存或取消修改", "提示！", MessageBoxButton.OK);
                callback(false);
            }else     callback(true);
            
        }
        #endregion 

        #region Binding view

        //public bool PayFlag
        //{
        //    get
        //    {
        //        InvoiceItemEditList ItemList = InvoiceItemEditList.GetInvoiceItemEditListByMxInvoiceTypeBusinessID(_currentMx.MxID, businessType, businessID);
        //        if (ItemList.Count != 1) throw new Exception("未找到发票项，或预定超过1次");

        //        InvoiceItemEdit ii = ItemList.First();
        //        if ((ii.InvoiceID == Guid.Empty))
        //        {
        //            Model = InvoiceList.GetInvoiceListByMxID(_currentMx.MxID).AddNew();
        //            IsEdit = true;
        //        }
        //        else
        //        {
        //            Model = InvoiceList.GetInvoiceListByMxID(_currentMx.MxID).First();
        //            IsEdit = false;
        //        }
        //    }
        //}
        public void SelectContact()
        {
            EditContactViewModel um = new EditContactViewModel(_currentMx);
            IoC.Get<IGlobalData>().ShowDialog<EditContactViewModel>(um);
            if (um.Model != null)
            {
                Model.Drawee = um.Model.Name;
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
        public void Handle(ModifyBusinessYdMessage message)
        {
            CMModify();
        }
        public void Handle(SaveBusinessYdMessage message)
        {
            #region not use businessinvoicitemsimple 
            CMSave();
            //add invoiceItem and save
            AddInvoiceItem(Model);
            #endregion 

            //更新墓穴状态，更新形状显示，更新全局缓存,自动更新Graphy
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        public void Handle(CancelBusinessYdMessage message)
        {
            Model.CancelEdit();
            if (Model.IsNew)
            {
                Model.Delete();
                Model = null;
            }
            IsEdit = false;
        }
       
        #endregion 

        private void AddInvoiceItem(BusinessYd model)
        {
            InvoiceItemEditList itemList = InvoiceItemEditList.GetInvoiceItemEditListByMxID(model.MxID);
            InvoiceItemEdit item = itemList.FindInvoiceItemEditByBusinessID(model.BusinessID);
            if (item == null)
            {
                item = itemList.AddNew();
            }

            //InvoiceList il = InvoiceList.GetInvoiceListByMxID(model.MxID);
            //Invoice invoice = il.AddNew();


            item.Price = model.DownPayment;
            item.Quantity = 1;
            item.ItemTypeID = 1;  //1-预定
            item.BusinessID = model.BusinessID;
            item.BusinessName = "预定";
            item.MxID = model.MxID;
            item.PayFlag = false;

            //invoice.InvoiceAccount = item.Price;
            //invoice.MxID = model.MxID;
            //invoice.InvoiceTime = DateTime.Today;

            using (var ctx = ConnectionManager<SqlConnection>.GetManager(ConfigurationManager.ConnectionStrings["LyglDB"].ConnectionString, false))
            {
                using (SqlTransaction st = ctx.Connection.BeginTransaction())
                {
                    try
                    {
                        //InvoiceList ilTemp = il.Clone();
                        //il = ilTemp.Save();
                        item.InvoiceID = Guid.Empty;// invoice.InvoiceID;
                        item.BusinessID = model.BusinessID;
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
