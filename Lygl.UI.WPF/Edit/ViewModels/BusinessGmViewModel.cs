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
using Lygl.UI.Framework.ViewModelBase;
using Lygl.DalLib.Invoice;
using System.Configuration;
using Csla.Data;
using System.Data.SqlClient;
using Lygl.DalLib.Util;

namespace Lygl.UI.Edit.ViewModels
{
        #region Message
    [Export(typeof(ICommandMessage))]
    public class AddBusinessGmMessage : AddCommandMessageBase
    {
        public AddBusinessGmMessage() : base() { Name = "AddBusinessGm"; Label = "添加"; ToolTip = "添加新的购买业务"; Category = "购买"; }
        public override bool IsCanExecute()
        {
            //这里要求该命令不显示在工具栏中，所以总为false;
            //因为它不以获得参数通知
            return false;
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyBusinessGmMessage : BusinessModifyCommandMessageBase
    {
        public ModifyBusinessGmMessage() : base() { Name = "ModifyBusinessGm"; Label = "修改"; ToolTip = "修改购买业务"; Group = "BusinessGm"; Category = "购买"; }
        //public override bool IsCanExecute()
        //{
        //    if (ViewModel == null) return false;
        //    if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
        //    if ((ViewModel as IHaveModel).Model == null) return false;
        //    if (((ViewModel as IHaveModel).Model as IBusinessHasPayFlag).PayFlag) return false;
        //    //if  (ROMx == null || ROMx.MxStatusID > 0) return false;
        //    return base.IsCanExecute();
        //}
    }
    [Export(typeof(ICommandMessage))]
    public class SaveBusinessGmMessage : SaveCommandMessageBase
    {
        public SaveBusinessGmMessage() : base() { Name = "SaveBusinessGm"; Label = "保存"; ToolTip = "保存购买业务的修改"; Group = "BusinessGm"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelBusinessGmMessage : CancelCommandMessageBase
    {
        public CancelBusinessGmMessage() : base() { Name = "CancelBusinessGm"; Label = "取消"; ToolTip = "不保存购买业务的修改"; Group = "BusinessGm"; }
    }

   
    #endregion 

    //[Export(typeof(BusinessLbViewModel))]
    public  class BusinessGmViewModel : BusinessSimpleViewModel<BusinessGm>,
        IHandle<ModifyBusinessGmMessage>,
        IHandle<SaveBusinessGmMessage>,
        IHandle<CancelBusinessGmMessage>
    {
        //保存传入的墓穴ID
        private Guid _currentMxID;
        /// <summary>
        /// 用于正常浏览业务用
        /// 
        /// </summary>
        /// <param name="MxID"></param>
        /// <param name="isEdit"></param>
        public BusinessGmViewModel(MxRO currentMx):
            base(currentMx, "墓穴购买", new string[] {"BusinessGm","InvoiceShare"})
        {
            //if Gm is empty,return default
            Model = BusinessGm.GetBusinessGmByMx(_currentMx.MxID, _currentMx.MxID);
            //如果为空，则新建
            if (Model.BusinessID == Guid.Empty)
            {
                if ((Csla.ApplicationContext.User.Identity as CustomIdentity).HavePermission((new AddBusinessGmMessage()).Name))
                {
                    Model = BusinessGm.NewBusinessGm();
                    Model.BusinessName = "购买";
                    Model.MxID = currentMx.MxID;
                    Model.Price = currentMx.Price??0;
                    Model.OperatorCode = SecurityHelper.GetCurrentUserCode();// IoC.Get<IGlobalData>().CurrentUser.Code;
                    Model.OperateTime = DateTime.Today;
                    Model.GmDate = DateTime.Today;
                    Model.PayFlag = false;
                    IsEdit = true;
                }
                else
                {
                    MessageBox.Show("你没有权限，请与管理员联系！");
                }
            }
        }
        #region Window Event handle
     
        #endregion 

        #region Binding view
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

        public void Handle(ModifyBusinessGmMessage message)
        {
            CMModify();
        }
        public void Handle(SaveBusinessGmMessage message)
        {
            #region not use businessinvoicitemsimple
            CMSave();
            //add invoiceItem and save
            AddInvoiceItem(Model);
            #endregion

            //更新墓穴状态，更新形状显示，更新全局缓存,自动更新Graphy
            IoC.Get<IGlobalData>().UpdateMxStatus(_currentMx);
        }
        public void Handle(CancelBusinessGmMessage message)
        {
            CMCancel();
            //Model.CancelEdit();
            //if (Model.IsNew)
            //{
            //    Model.Delete();
            //    Model = null;
            //}
            //IsEdit = false;
        }
        #endregion

        private void AddInvoiceItem(BusinessGm model)
        {
            InvoiceItemEditList itemList = InvoiceItemEditList.GetInvoiceItemEditListByMxID(model.MxID);
            InvoiceItemEdit item = itemList.FindInvoiceItemEditByBusinessID(model.BusinessID);
            if (item == null)
            {
                item = itemList.AddNew();
            }

            item.Price = model.Price;
            item.Quantity = 1;
            item.ItemTypeID = 2;  //2-购买
            item.BusinessID = model.BusinessID;
            item.BusinessName = "购买";
            item.MxID = model.MxID;
            item.PayFlag = false;

            using (var ctx = ConnectionManager<SqlConnection>.GetManager(ConfigurationManager.ConnectionStrings["LyglDB"].ConnectionString, false))
            {
                using (SqlTransaction st = ctx.Connection.BeginTransaction())
                {
                    try
                    {
                        item.InvoiceID = Guid.Empty;
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
