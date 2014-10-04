using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeReason.Reports;

namespace Lygl.UI.Report
{
    public interface IReportData
    {
        /// <summary>
        /// 获取报表模版文档
        /// </summary>
        /// <returns></returns>
        string GetReportXaml();
        /// <summary>
        /// 获取报表数据
        /// </summary>
        /// <returns></returns>
        ReportData GetReportData();
        /// <summary>
        /// 打印预览窗口显示标题
        /// </summary>
        /// <returns></returns>
        string GetDisplayName();

    }
}
