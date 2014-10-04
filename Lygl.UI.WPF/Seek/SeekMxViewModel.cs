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
using Lygl.DalLib.Business;
using Lygl.DalLib.Statistic;
using Lygl.UI.Report;

namespace Lygl.UI.Seek
{

    [Export(typeof(ICommandMessage))]
    public class PrintSeekMxMessage : CommandMessageBusinessBase
    {
        public PrintSeekMxMessage() : base() { Name = "PrintSeekMx"; Label = "打印"; ToolTip = "打印查询结果"; Group = "SeekMx"; Category = "墓穴查询"; }

        public override bool IsCanExecute()
        {
            return true;
        }
    }
    //public struct GlfStruct
    //{
    //    public Guid MxID;
    //    public string MxName;
    //    public DateTime StartDate;
    //    public DateTime EndDate;
    //}
  public class SeekMxCommand:CommandBase<SeekMxCommand>
{
      public string MxName;
      public SeekMxCommand(string mxName)
      {
          MxName = mxName;
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

    public partial class SeekMxViewModel : ScreenWithModelBase<AreaRO>,
        IHandle<PrintSeekMxMessage>
    {
        /// <summary>
        /// Default constructor is protected so callers must use one with a parent.
        /// </summary>
        public SeekMxViewModel()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            DisplayName = "墓穴查找";
            _commandGroupNames = new string[] { "SeekMx" };
            //System.Threading.ThreadPool.QueueUserWorkItem(o =>
            //{
            //    GC.GetTotalMemory(true);
            //});
        }



        public int SeekTypeIndex { get; set; }
        public String SeekText { get; set; }

        private string[] _commandGroupNames; 
        public IList<ICommandMessage> toolBar
        {
            get
            {
                foreach (var item in _commandGroupNames)
                {
                    IoC.Get<ICommandMessageAggregator>().SetGroupViewModel(item, this);
                }
                return IoC.Get<ICommandMessageAggregator>().GetGroup(_commandGroupNames);
            }
        }
        public void Handle(PrintSeekMxMessage message)
        {
            ReportSeekMxDataHelper dataHelper = new ReportSeekMxDataHelper(this);
            PrintDialogViewModel um = new PrintDialogViewModel(dataHelper);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "Height", 300 }, { "Width", 300 }, { "WindowState", WindowState.Normal } };
            IoC.Get<IWindowManager>().ShowWindow(um, null, settings);
        }

        public void Seek()
        {
            PopulateDataGrid();
            string text = string.Format("%{0}", SeekText);
            switch (SeekTypeIndex)
            {
                case 0:  //墓穴名
                    MxROL mxList = MxROL.GetMxROLByMxName(text);
                    dgMain = mxList;
                    this.Refresh();
                    break;
                case 1: //逝者
                    MxSzEditList szList = MxSzEditList.GetMxSzEditListBySzName(text);
                    dgMain = szList;
                    this.Refresh();
                    break;
                case 2:  //姓名查找联系人
                    ContactEditList contactList = ContactEditList.GetContactEditListByName(text);
                    dgMain = contactList;
                    this.Refresh();
                    break;
                case 3:  //电话查找联系人
                    ContactEditList contactList1 = ContactEditList.GetContactEditListByPhoneNum(text,1);
                    dgMain = contactList1;
                    this.Refresh();
                    break;
                case 4:  //已欠管理费
                    ObservableBindingList<SeekGlfDq> glfList = new ObservableBindingList<SeekGlfDq>();
                    using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
                    {
                        using (var cmd = new SqlCommand("dbo.GetGlfListByDate", ctx.Connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DqDate", SeekText).DbType = DbType.DateTime;
                            //var args = new DataPortalHookArgs(cmd, dqDate);
                            using (var dr = new SafeDataReader(cmd.ExecuteReader()))
                            {
                                while (dr.Read())
                                {
                                    SeekGlfDq re = new SeekGlfDq(dr.GetGuid("MxID"), dr.GetString("MxName"), dr.GetDateTime("StartDate"), dr.GetDateTime("EndDate"));
                                    
                                    //re.MxID = dr.GetGuid("MxID");
                                    //re.MxName = dr.GetString("MxName");
                                    //re.StartDate = dr.GetDateTime("StartDate");
                                    //re.EndDate = dr.GetDateTime("EndDate");
                                    glfList.Add(re);
                                }
                            }
                        }
                    }    
                     
                    //BusinessGlfList glfList = BusinessGlfList.GetBusinessGlfListByDate(SmartDate.StringToDate(SeekText));
                    dgMain = glfList;
                    this.Refresh();
                    break;
                default:
                    break;
            }
        }

        public IObservableBindingList dgMain { get; set; }
        private DataGrid _dgMain;
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (view is Window)
            {
                _dgMain = ((DataGrid)((view as Window).Content as FrameworkElement).FindName("dgMain"));
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
            
            switch (SeekTypeIndex)
            {
                case 0:
                    mxID = (_dgMain.SelectedItem as MxRO).MxID;
                    break;
                case 1:
                    mxID = (_dgMain.SelectedItem as MxSzEdit).MxID ?? Guid.Empty;
                    break;
                case 2:
                case 3:
                    mxID = (_dgMain.SelectedItem as ContactEdit).MxID ;
                    break;
                case 4:
                    mxID = (_dgMain.SelectedItem as SeekGlfDq).MxID;
                    break;
            }
            IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(mxID);
            Lygl.UI.Edit.ViewModels.EditMxViewModel um = new Lygl.UI.Edit.ViewModels.EditMxViewModel(mxID);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }
        private void PopulateDataGrid()
        {
            _dgMain.Columns.Clear();
            switch (SeekTypeIndex)
            {
                   
                case 0:
                    CommonFunction.AddDataGridTextColumn(_dgMain, "ID", "MxID", false);
                    CommonFunction.AddDataGridTextColumn(_dgMain, "墓穴名", "MxName");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "状态", "MxStatus");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "类型", "MxType");
                    break;
                case 1:
                    CommonFunction.AddDataGridTextColumn(_dgMain, "ID", "MxID", false);
                    CommonFunction.AddDataGridTextColumn(_dgMain, "逝者名", "Name");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "性别", "Sex");
                    break;
                case 2:
                case 3:
                    CommonFunction.AddDataGridTextColumn(_dgMain, "ID", "MxID", false);
                    CommonFunction.AddDataGridTextColumn(_dgMain, "联系人", "Name");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "电话", "Phone");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "手机", "Mobile");
                    break;
                case 4:
                    CommonFunction.AddDataGridTextColumn(_dgMain, "ID", "MxID", false);
                    CommonFunction.AddDataGridTextColumn(_dgMain, "墓穴名", "MxName");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "开始日期", "StartDate");
                    CommonFunction.AddDataGridTextColumn(_dgMain, "到期日期", "EndDate");
                    break;
                default:
                    break;
            }
            
            
        }


        //private void AddDataGridTextColumn(string header, string binding,bool visibility=true)
        //{
        //    DataGridTextColumn co = new DataGridTextColumn();
        //    co.Header = header;
           
        //    co.Binding = new Binding(binding);
        //    if (!visibility)
        //    co.Visibility = Visibility.Collapsed;
        //    co.Width = new DataGridLength(50,DataGridLengthUnitType.Star);
            
        //    _dgMain.Columns.Add(co);
        //}
    }
}
