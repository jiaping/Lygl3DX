using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Data;
using Lygl.DalLib.Browse;
using Lygl.DalLib.Statistic;
using Lygl.UI.Framework.CommonFunction;
using Lygl.DalLib.Business;
using Lygl.DalLib.NVL;
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Lygl.UI.Framework.ViewModelBase;
using System.Windows;


namespace Lygl.UI.Statistic
{

    [Export(typeof(ICommandMessage))]
    public class ExportSrMessage : CommandMessageBase
    {
        public ExportSrMessage() : base() { Name = "ExportSr"; Label = "导出数据"; ToolTip = "导出数据到Excel文件"; Group = "StatisticSr"; Category = "统计"; }

        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            
            return true;
        }
    }

    public class StatisticSrViewModel : ScreenWithModelBase<StatisticSrList>,
        IHandle<ExportSrMessage>
    {
        private DataGrid _dgMain;
        //private string _statisticType;

        public StatisticSrViewModel()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            DisplayName = "业务统计";            
        }

        public StatisticSrList dgMain { get; set; }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
           

            if (view is Window)
            {
                _dgMain = ((DataGrid)((view as Window).Content as FrameworkElement).FindName("dgMain"));
                DatePicker dp = ((DatePicker)((view as Window).Content as FrameworkElement).FindName("SeekStartDate"));
                if (dp != null) dp.SelectedDate = DateTime.Today;
                dp = ((DatePicker)((view as Window).Content as FrameworkElement).FindName("SeekEndDate"));
                if (dp != null) dp.SelectedDate = DateTime.Today;
                //_dgMain.SelectionChanged += new SelectionChangedEventHandler(_dgMain_SelectionChanged);
                //PopulateDataGrid();
            }
            //StatisticSrView sbview = view as StatisticSrView;
            //sbview.SeekStartDate.SelectedDate = DateTime.Today;
            //sbview.SeekEndDate.SelectedDate = DateTime.Today;
            //_dgMain = sbview.dgMain;
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            _dgMain.Columns.Clear();
            CommonFunction.AddDataGridTextColumn(_dgMain, "业务类型", "Name");
            CommonFunction.AddDataGridTextColumn(_dgMain, "小计", "SubTotal",true,"F");
            CommonFunction.AddDataGridTextColumn(_dgMain, "数量", "Num");
        }

        public int SeekTypeIndex { get; set; }
        public DateTime SeekStartDate { get; set; }
        public DateTime SeekEndDate { get; set; }
        public string UserID { get; set; }
        private string[] _commandGroupNames; 

        public UserNameNVL UserNameList
        {
            get
            {
                return UserNameNVL.GetUserNameNVL();
            }
        }

        public IList<ICommandMessage> toolBar
        {
            get
            {
                    IoC.Get<ICommandMessageAggregator>().SetGroupViewModel("StatisticSr", this);
               
                return IoC.Get<ICommandMessageAggregator>().GetGroup(new string[] { "StatisticSr" });
            }
        }

        public void Seek()
        {
            PopulateDataGrid();
            if (UserID == null) 
                dgMain = StatisticSrList.GetStatisticSrList(SeekStartDate, SeekEndDate);
            else 
                dgMain = StatisticSrList.GetStatisticSrList(UserID, SeekStartDate, SeekEndDate);
            RaisePropertyChangedEventImmediately("dgMain");
            
        }
        public void Handle(ExportSrMessage message)
        {
            Export2Excel();
        }
        public void Export2Excel()
        {
            //DataOperation dataop = new DataOperation();

            //DataView dv = dataop.OpertaionsGet(); //获得一个dataview，这是我的程序里的方法，这里可以随便获得任何一个dataview或者其他数据集合sheet

            //dataop.Clear();


            Microsoft.Office.Interop.Excel.Application ac = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbook wb; //这里千万不能使用 new 来实例对象，不然会异常

            Microsoft.Office.Interop.Excel.Worksheet ws;




            wb = ac.Workbooks.Add(System.Reflection.Missing.Value);  //创建工作簿（WorkBook：即Excel文件主体本身）
            ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets.Add(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);        //创建工作表（Worksheet：工作表，即Excel里的子表sheet）

            //设置表名
            ws.Name = "TestXlS";
            
            //将数据导入到工作表的单元格
             int i=1;
            foreach (var item in dgMain)
            {
                ws.Cells[i, 1] = item.Name;
                ws.Cells[i, 2] = item.SubTotal;
                ws.Cells[i, 3] = item.Num;
                i++;
            }
            System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();

            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                wb.SaveAs(saveFileDialog1.FileName, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                wb.Close(System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                System.Windows.Forms.MessageBox.Show(string.Format("数据已导出到{0}中！", saveFileDialog1.FileName));
            } else
                wb.Close(false, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
           
        }
    }
   
}
