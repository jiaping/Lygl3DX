using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Xml;
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Lygl.UI.Framework.ViewModelBase;
using Lygl.DalLib.Browse;
using System.Windows.Controls;
using Csla;
using Csla.Data;
using System.Data.SqlClient;
using System.Data;
using Caliburn.Micro;
using System.Collections.Generic;
using Lygl.DalLib.Edit;
using Csla.Core;
using System.Windows.Input;
using Lygl.UI.Framework.CommonFunction;
using Lygl.DalLib.Statistic;

namespace Lygl.UI.Seek
{
    public class SeekGlfCommand : CommandBase<SeekGlfCommand>
{
      public string MxName;
      public SeekGlfCommand()
      {
          
      }

      protected override void DataPortal_Execute()
      {
          using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
          {
              using (var cmd = new SqlCommand("select * from Mx where MxName like @MxName", ctx.Connection))
              {
                  try
                  {
                      cmd.CommandType = CommandType.Text;
                      cmd.Parameters[0].Value = MxName;
                      cmd.ExecuteReader();
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }

              }
          }
      }
}

    public partial class SeekGlfViewModel : ScreenWithModelBase<AreaRO>
    {
        /// <summary>
        /// Default constructor is protected so callers must use one with a parent.
        /// </summary>
        public SeekGlfViewModel()
        {
            DisplayName = "管理费统计查询";
            //System.Threading.ThreadPool.QueueUserWorkItem(o =>
            //{
            //    GC.GetTotalMemory(true);
            //});
        }
        public DateTime SeekStartDate { get; set; }
        public DateTime SeekEndDate { get; set; }
        
        public void Seek()
        {
            PopulateDataGrid();
            SeekGlfList glfList = SeekGlfList.GetSeekGlfList(SeekStartDate, SeekEndDate);
           
            //BusinessGlfList glfList = BusinessGlfList.GetBusinessGlfListByDate(SmartDate.StringToDate(SeekText));
            dgMain = glfList;
            //this.Refresh();
            RaisePropertyChangedEventImmediately("dgMain");
        }

        public SeekGlfList dgMain { get; set; }
        private DataGrid _dgMain;
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
            
           
        }

        //public void DGMain_SelectionChanged(SelectionChangedEventArgs e)
        //{
        //    Guid mxID = Guid.Empty;
        //    if (e.AddedItems.Count != 1) return;
        //    switch (SeekTypeIndex)
        //    {
        //        case 0:
        //            mxID = (e.AddedItems[0] as MxRO).MxID;
        //            break;
        //        case 1:
        //            mxID = (e.AddedItems[0] as MxSzEdit).MxID ?? Guid.Empty;
        //            break;
        //    }
        //    IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(mxID);
        //    Lygl.UI.Edit.ViewModels.EditMxViewModel um = new Lygl.UI.Edit.ViewModels.EditMxViewModel(mxID);
        //    Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
        //    IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        //}
        public void DGMain_MouseDoubleClick(MouseButtonEventArgs e)
        {
            Guid mxID = Guid.Empty;
            if (_dgMain.SelectedItem == null) return;
           
            mxID = (_dgMain.SelectedItem as SeekGlfItem).MxID;
            IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(mxID);
            Lygl.UI.Edit.ViewModels.EditMxViewModel um = new Lygl.UI.Edit.ViewModels.EditMxViewModel(mxID);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }
        private void PopulateDataGrid()
        {
            _dgMain.Columns.Clear();
            CommonFunction.AddDataGridTextColumn(_dgMain, "ID", "MxID", false);
            CommonFunction.AddDataGridTextColumn(_dgMain, "墓穴名", "MxName");
            CommonFunction.AddDataGridTextColumn(_dgMain, "开始日期", "StartDate",true,"d");
            CommonFunction.AddDataGridTextColumn(_dgMain, "到期日期", "EndDate", true, "d");
            CommonFunction.AddDataGridTextColumn(_dgMain, "业务ID", "BusinessID",false);
        }
       
    }
}
