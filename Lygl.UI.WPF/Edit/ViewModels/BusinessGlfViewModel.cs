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
using Lygl.DalLib.Invoice;
using System.Configuration;
using System.Data.SqlClient;
using Csla.Data;
using Lygl.DalLib.Util;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    /// <summary>
    /// 显示于墓穴窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispBusinessGlfMessage : MxBusinessCMBase
    {
        public DispBusinessGlfMessage() : base() { Name = "DispBusinessGlf"; Label = "管理费"; ToolTip = "显示管理费业务处理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }

        //public override bool IsCanExecute()
        //{
        //    return true;// MxID == Guid.Empty ? false : true;
        //}
    }
    /// <summary>
    /// 显示于点灯业务窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class SaveBusinessGlfMessage : SaveCommandMessageBase
    {
        public SaveBusinessGlfMessage() : base() { Name = "SaveBusinessGlf"; Label = "保存"; ToolTip = "保存修改的管理费业务"; Group = "BusinessGlf"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelBusinessGlfMessage : CancelCommandMessageBase
    {

        public CancelBusinessGlfMessage() : base() { Name = "CancelBusinessGlf"; Label = "取消"; ToolTip = "不保存修改的管理费业务"; Group = "BusinessGlf"; }
    }

    

    [Export(typeof(ICommandMessage))]
    public class AddBusinessGlfMessage : AddCommandMessageBase
    {
        public AddBusinessGlfMessage() : base() { Name = "AddBusinessGlf"; Label = "添加"; ToolTip = "添加新管理费业务"; Group = "BusinessGlf"; Category = "管理费"; }

       
    }
   [Export(typeof(ICommandMessage))]
    public class DeleteBusinessGlfMessage : DeleteCommandMessageBase
    {
       public DeleteBusinessGlfMessage() : base() { Name = "DeleteBusinessGlf"; Label = "删除"; ToolTip = "删除管理费业务"; Group = "BusinessGlf"; Category = "管理费"; }

    }
    [Export(typeof(ICommandMessage))]
    public class ModifyBusinessGlfMessage : ModifyCommandMessageBase
    {

        public ModifyBusinessGlfMessage() : base() { Name = "ModifyBusinessGlf"; Label = "修改"; ToolTip = "修改选择的管理费业务"; Group = "BusinessGlf"; Category = "管理费"; }

    }
    //[Export(typeof(ICommandMessage))]
    //public class DisGlfInvoiceItemMessage : NeedMxCommandMessageBase
    //{
    //    public DisGlfInvoiceItemMessage() : base() { Name = "GlfInvoiceItem"; Label = "打印发票"; ToolTip = "管理费收费"; Group = "BusinessGlf"; }
    //}
    #endregion 

    [Export(typeof(BusinessGlfViewModel))]
    class BusinessGlfViewModel : BusinessWithMainListViewModel<BusinessGlfList, BusinessGlf>,
        IHandle<AddBusinessGlfMessage>,
        IHandle<ModifyBusinessGlfMessage>,
        IHandle<SaveBusinessGlfMessage>,
        IHandle<CancelBusinessGlfMessage>,
        IHandle<DeleteBusinessGlfMessage>
    {
        /// <summary>
        /// 用于正常浏览业务用
        /// 
        /// </summary>
        /// <param name="MxID"></param>
        /// <param name="isEdit"></param>
        public BusinessGlfViewModel(MxRO mx)
            : base(mx, "管理费", new string[] { "BusinessGlf", "InvoiceShare" })
        {
        }

        #region Window Event handle
        //在这里实现基类没有实现的窗口响应事件
        #endregion 

        #region Binding view
        //在这里实现基类没有实现的绑定属性
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

        public void Handle(AddBusinessGlfMessage message)
        {
            base.CMListAddNew();
        }
        public void Handle(DeleteBusinessGlfMessage message)
        {
            base.CMListDeleteItem(Model);
        }
        public override void InitNewItem(BusinessGlf newModel)
        {
            base.InitNewItem(newModel);
            newModel.MxID = _currentMx.MxID;
            newModel.OperatorCode = SecurityHelper.GetCurrentUserCode();// IoC.Get<IGlobalData>().CurrentUser.Code;
            newModel.BusinessName = "管理费";
            newModel.OperateTime = DateTime.Now.ToShortDateString();
            newModel.StartDate = DateTime.Now.ToShortDateString();
            newModel.EndDate = DateTime.Now.ToShortDateString();
        }
       
        public void Handle(ModifyBusinessGlfMessage message)
        {
            base.CMModifyItem();
        }
        public void Handle(SaveBusinessGlfMessage message)
        {
            base.CMSaveList();
            AddInvoiceItem(Model);
        }

        public void Handle(CancelBusinessGlfMessage message)
        {
            base.CMCancelList();

        }
        #endregion 
        private void AddInvoiceItem(BusinessGlf model)
        {
            InvoiceItemEditList itemList = InvoiceItemEditList.GetInvoiceItemEditListByMxID(model.MxID);
            InvoiceItemEdit item = itemList.FindInvoiceItemEditByBusinessID(model.BusinessID);
            if (item == null)
            {
                item = itemList.AddNew();
            }

            //InvoiceList il = InvoiceList.GetInvoiceListByMxID(model.MxID);
            //Invoice invoice = il.AddNew();


            item.Price = model.Price;
            item.Quantity = 1;
            item.ItemTypeID = 900;  //900-管理费
            item.BusinessID = model.BusinessID;
            item.BusinessName = "管理费";
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
