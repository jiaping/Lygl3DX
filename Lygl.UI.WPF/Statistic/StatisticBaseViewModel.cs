using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Data;
using Lygl.DalLib.Browse;

namespace Lygl.UI.Statistic
{
    public class StatisticBaseViewModel:Screen
    {
        private DataGrid _dgMain;
        private string _statisticType;

        public StatisticBaseViewModel(string statisticType)
        {
            _statisticType = statisticType;
            dgMain = MxROL.GetMxROL();
        }

        public MxROL dgMain { get; set; }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //_dgMain = (view as StatisticBaseView).dgMain;  
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            DataGridTextColumn co = new DataGridTextColumn();
            co.Header = "新增列";

            co.Binding = new Binding("MxName");
            //co.Binding=new System.Windows.Data.b
            _dgMain.Columns.Add(co);
        }
    }
}
