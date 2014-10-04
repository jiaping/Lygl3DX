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
using Lygl.UI.Seek;
using Csla.Core;
using Lygl.DalLib.Statistic;

namespace Lygl.UI.Report
{
    public class ReportSeekMxDataHelper:ReportDataHelperBase<SeekMxViewModel>//,IReportData
    {
        public ReportSeekMxDataHelper(SeekMxViewModel vm)
            :base(vm,"ReportSeekMx.xaml")
        {
            ViewModel = vm;
            DisplayName = "打印预览－管理费到期查询结果";
        }

        protected override void PopulateData()
        {
            base.PopulateData();
            _reportData.ReportDocumentValues.Add("Title", "管理费到期查询结果");
            _reportData.DataTables.Add(GetSeekItemTable());
        }
        public DataTable GetSeekItemTable()
        {
            DataTable dt = new DataTable("SeekMxItemTable");
            switch (ViewModel.SeekTypeIndex)
            {
                case 0:
                    break;
                case 4:
                    dt.Columns.Add("MxName",typeof(string));
                    dt.Columns.Add("StartDate", typeof(string));
                    dt.Columns.Add("EndDate" ,typeof(string));
                         foreach (var item in ViewModel.dgMain as ObservableBindingList<SeekGlfDq>)
                    {
                        dt.Rows.Add(new object[] { item.MxName,item.StartDate,item.EndDate});
                    }
                    break;
                default:
                    break;
            }
            
            
            return dt;
        }
    }
}
