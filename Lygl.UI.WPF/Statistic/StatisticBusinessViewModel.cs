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

namespace Lygl.UI.Statistic
{
    public class StatisticBusinessViewModel:Screen
    {
        private DataGrid _dgMain;
        //private string _statisticType;

        public StatisticBusinessViewModel()
        {
            DisplayName = "业务统计";
        }

        public StatisticBusinessList dgMain { get; set; }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            StatisticBusinessView sbview = view as StatisticBusinessView;
            sbview.SeekStartDate.SelectedDate = DateTime.Today;
            sbview.SeekEndDate.SelectedDate = DateTime.Today;
            _dgMain = sbview.dgMain;
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            _dgMain.Columns.Clear();
            CommonFunction.AddDataGridTextColumn(_dgMain, "墓穴名", "MxName");
            CommonFunction.AddDataGridTextColumn(_dgMain, "墓穴ID", "MxID", false);
            CommonFunction.AddDataGridTextColumn(_dgMain, "业务ID", "BusinessID",false);
            CommonFunction.AddDataGridTextColumn(_dgMain, "业务名", "BusinessName");            
        }

        public int SeekTypeIndex { get; set; }
        public DateTime SeekStartDate { get; set; }
        public DateTime SeekEndDate { get; set; }
        public string UserID { get; set; }

        public UserNameNVL UserNameList
        {
            get
            {
                return UserNameNVL.GetUserNameNVL();
            }
        }

        public void Seek()
        {
            PopulateDataGrid();
            if (UserID == null) 
                dgMain = StatisticBusinessList.GetStatisticBusinessList(SeekStartDate, SeekEndDate);
            else 
                dgMain = StatisticBusinessList.GetStatisticBusinessList(UserID, SeekStartDate, SeekEndDate);
            //switch (SeekTypeIndex)
            //{
            //    case 0:  //按日期
            //        dgMain = StatisticBusinessList.GetStatisticBusinessList(SeekStartDate, SeekEndDate);
            //        break;
            //    case 1: //按员工号
            //         dgMain = StatisticBusinessList.GetStatisticBusinessList(UserID,SeekStartDate, SeekEndDate);
            //        break;
            //    default:
            //        break;

            //}


            //RaisePropertyChangedEventImmediately("dgMain");



           // this.Refresh();
        }
    }
   
}
