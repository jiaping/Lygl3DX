using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows;

namespace Lygl.UI.Framework.CommonFunction
{
    public class CommonFunction
    {

        /// <summary>
        /// 添加文本列到datagrid控件中
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="header"></param>
        /// <param name="dataName"></param>
        /// <param name="visibility"></param>
        /// <param name="format">表示对绑定数据的显示格式字符串</param>
        static public  void AddDataGridTextColumn(DataGrid dg,string header, string dataName, bool visibility = true,string format="")
        {
            DataGridTextColumn co = new DataGridTextColumn();
            co.Header = header;

            co.Binding = new Binding(dataName);
            if (format!=string.Empty) 
            co.Binding.StringFormat = format;
            if (!visibility)
                co.Visibility = Visibility.Collapsed;
            co.Width = new DataGridLength(50, DataGridLengthUnitType.Star);

            dg.Columns.Add(co);
        }
    }
}
