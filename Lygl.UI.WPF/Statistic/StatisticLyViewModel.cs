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

namespace Lygl.UI.Statistic
{
    public class StatisticLyViewModel:Screen
    {
        private DataGrid _dgMain;
        //private string _statisticType;

        public StatisticLyViewModel()
        {
            DisplayName = "墓区统计";
            dgMain = new List<StatisticObject>();
            StatisticLy sa = StatisticLy.GetStatisticLy();
            dgMain.Add(new StatisticObject("总墓区数", "",sa.TotalMqNum.ToString()));
            dgMain.Add(new StatisticObject("总墓数", "", sa.TotalMxNum.ToString()));
            dgMain.Add(new StatisticObject("", "", ""));
            AreaROL ar = AreaROL.GetAreaROL();
            foreach (var item in ar)
            {
                sa = StatisticLy.GetMqInfo(item.AreaID);
                dgMain.Add(new StatisticObject(string.Format("墓区:{0}", item.Name), "墓穴总数", sa.MqMxNum.ToString()));
                dgMain.Add(new StatisticObject("", "待售墓穴数", sa.MqDsMxNum.ToString()));
                dgMain.Add(new StatisticObject("", "已预定墓穴数", sa.MqYdMxNum.ToString()));
                dgMain.Add(new StatisticObject("", "已出售墓穴数", sa.MqYsMxNum.ToString()));
            }
        }

        public IList<StatisticObject> dgMain { get; set; }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _dgMain = (view as StatisticLyView).dgMain;  
            PopulateDataGrid();
        }

        private void PopulateDataGrid()
        {
            CommonFunction.AddDataGridTextColumn(_dgMain, "大类", "Name1");
            CommonFunction.AddDataGridTextColumn(_dgMain, "子类", "Name2");
            CommonFunction.AddDataGridTextColumn(_dgMain, "统计值", "Value");
        }
    }
    public class StatisticObject
    {
        public StatisticObject(string name1,string name2,string value)
        {
            this.Name1 = name1;
            this.Name2 = name2;
            this.Value = value;
        }

        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Value { get; set; }
    }
}
