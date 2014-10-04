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
    public class BwReportDataHelper : ReportDataHelperBase<BusinessLbViewModel>,IReportData
    {
        public BwReportDataHelper(BusinessLbViewModel vm)
            : base(vm, "Bw.xaml")
        {
            //MxName = vm.MxName;
            //OperatorCode = vm.Model.OperatorCode;
            //OperateTime = vm.Model.OperateTime;
            //Bx = vm.Model.Bx;
            //Drawee = vm.Model.Drawee;
            PayFlag = ViewModel.Model.PayFlag.ToString();
            //LbrText = vm.Model.LbrText;
            //LbsjText = vm.Model.LbsjText;
            //Ch1 = vm.Model.BwSzs[0].Ch;
            //Ch2 = vm.Model.BwSzs[1].Ch;
            //Sheng1 = vm.Model.BwSzs[0].Sheng;
            //Sheng2 = vm.Model.BwSzs[1].Sheng;
            //Gu1 = vm.Model.BwSzs[0].Gu;
            //Gu2 = vm.Model.BwSzs[1].Gu;
        }

        public string MxName;
        public string OperatorCode;
        public DateTime OperateTime;
        public string Bx;
        public string Drawee;
        public string PayFlag;

        public string LbrText;
        public string LbsjText;

        public string Ch1;
        public string Ch2;
        public string Sheng1;
        public string Sheng2;
        public string Gu1;
        public string Gu2;

        public DataTable GetProductLbItems()
        {
            DataTable dt = new DataTable("ProductLbTable");
            dt.Columns.Add("Name",typeof(string));
            dt.Columns.Add("UnitPrice", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("SubTotal", typeof(string));

            foreach (var item in ViewModel.Model.LbItems)
            {
                dt.Rows.Add(new object[] { item.Name, item.UnitPrice.ToString("F"), item.Quantity.ToString("D"), item.SubTotal.ToString("F") });
            }
            return dt;
        }


        #region IReportData
        public string GetReportXaml()
        {
            StreamReader reader = new StreamReader(new FileStream(@"Reports\Bw.xaml", FileMode.Open, FileAccess.Read));
            string result= reader.ReadToEnd();
            reader.Close();
            return result;
        }
        public ReportData GetReportData()
        {
            ReportData data = new ReportData();

            data.ReportDocumentValues.Add("MxName", MxName);
            data.ReportDocumentValues.Add("OperatorCode", OperatorCode);
            data.ReportDocumentValues.Add("OperateTime", OperateTime);
            data.ReportDocumentValues.Add("Bx", Bx);
            data.ReportDocumentValues.Add("Drawee", Drawee);
            data.ReportDocumentValues.Add("PayFlag", PayFlag.ToString());

            data.ReportDocumentValues.Add("LbrText", LbrText);
            data.ReportDocumentValues.Add("LbsjText", LbsjText);

            data.ReportDocumentValues.Add("Ch1", Ch1);
            data.ReportDocumentValues.Add("Ch2", Ch2);
            data.ReportDocumentValues.Add("Sheng1", Sheng1);
            data.ReportDocumentValues.Add("Sheng2", Sheng2);
            data.ReportDocumentValues.Add("Gu1", Gu1);
            data.ReportDocumentValues.Add("Gu2", Gu2);

            data.DataTables.Add(GetProductLbItems());

            
            return data;
        }

        #endregion


    }
}
