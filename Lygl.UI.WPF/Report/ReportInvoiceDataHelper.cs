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
using Lygl.UI.Edit.ViewModels;
using System.Data;
using CodeReason.Reports;
using System.IO;

namespace Lygl.UI.Report
{
    public class ReportInvoiceDataHelper:ReportDataHelperBase<BusinessInvoiceViewModel>,IReportData
    {
        public ReportInvoiceDataHelper(BusinessInvoiceViewModel vm)
            :base(vm,"ReportInvoice.xaml")
        {
            ViewModel = vm;
            MxName = vm.MxName;
            OperatorCode = vm.Model.OperatorCode;
            OperateTime = vm.Model.InvoiceTime;
            DisplayName = "打印预览－发票打印";
        }

        public string MxName;
        public string OperatorCode;
        public DateTime OperateTime;

        protected override void PopulateData()
        {
            base.PopulateData();
            _reportData.ReportDocumentValues.Add("MxName", MxName);
            _reportData.ReportDocumentValues.Add("OperatorCode", OperatorCode);
            _reportData.ReportDocumentValues.Add("OperateTime", OperateTime.ToString("d"));

            _reportData.DataTables.Add(GetInvoiceItemTable());
        }
        public DataTable GetInvoiceItemTable()
        {
            DataTable dt = new DataTable("InvoiceItemTable");
            dt.Columns.Add("Name",typeof(string));
            dt.Columns.Add("UnitPrice", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("SubTotal", typeof(string));
            //InvoiceItemEditList itemList= InvoiceItemEditList.GetInvoiceItemEditListByMxInvoice(ViewModel.Model.InvoiceID,ViewModel.Model.MxID);
            foreach (var item in ViewModel.ItemList)
            {
                dt.Rows.Add(new object[] { item.BusinessName, item.Price.ToString("F"), item.Quantity.ToString("D"), (item.Price * item.Quantity).ToString("F") });
            }
            return dt;
        }

    }
}
