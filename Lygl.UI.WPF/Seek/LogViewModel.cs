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
using Lygl.DalLib.Seek;

namespace Lygl.UI.Seek
{
//    [Serializable]
//  public class GetLogCommand:CommandBase<GetLogCommand>
//{
//        public IObservableBindingList DBRecords;
//      public GetLogCommand()
//      {

//      }

//      protected override void DataPortal_Execute()
//      {
//          using (var ctx = ConnectionManager<SqlConnection>.GetManager("LyglDB"))
//          {
//              using (var cmd = new SqlCommand("select * from Log" , ctx.Connection))
//              {
//                  try
//                  {
//                      cmd.CommandType = CommandType.Text;
//                      //cmd.Parameters[0].Value = MxName;
//                      cmd.
//                      var result=cmd.ExecuteReader();

//                      using (var dr = new SafeDataReader(cmd.ExecuteReader()))
//            {
//                if (dr.Read())
//                {
//                    Fetch(dr);
//                }
//                BusinessRules.CheckRules();
//            }
//                      LoadProperty(AreaIDProperty, dr.GetGuid("AreaID"));


//                  }
//                  catch (Exception ex)
//                  {
//                      throw ex;
//                  }

//              }
//          }
//      }
//}

    public partial class LogViewModel : Screen
    {
        /// <summary>
        /// Default constructor is protected so callers must use one with a parent.
        /// </summary>
        public LogViewModel()
        {
            DisplayName = "查看日志";
            LogDate = DateTime.Today.Date;
            //PopulateDataGrid();
            //System.Threading.ThreadPool.QueueUserWorkItem(o =>
            //{
            //    GC.GetTotalMemory(true);
            //});
        }



        public int SeekTypeIndex { get; set; }
        public SmartDate LogDate { get; set; }

        
        public void Seek()
        {
            LogROL mxList = LogROL.GetLogROL(LogDate);
            dgMain = mxList;
            this.Refresh();
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

        private void PopulateDataGrid()
        {
                    AddDataGridTextColumn("ID", "MxID", false);
                    AddDataGridTextColumn("墓穴名", "MxName");
                    AddDataGridTextColumn("状态", "MxStatus");
                    AddDataGridTextColumn("类型", "MxType");
        }


        private void AddDataGridTextColumn(string header, string binding,bool visibility=true)
        {
            DataGridTextColumn co = new DataGridTextColumn();
            co.Header = header;
            co.Binding = new Binding(binding);
            if (!visibility)
            co.Visibility = Visibility.Collapsed;
            _dgMain.Columns.Add(co);
        }
    }
}
