using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CodeReason.Reports;

namespace Lygl.UI.Report
{
    /// <summary>
    /// 报表数据帮助器的基类
    /// </summary>
    /// <typeparam name="T">具体报表对应的viewmodel</typeparam>
    public class ReportDataHelperBase<T>:IReportData
    {
        public ReportDataHelperBase(T vm,string reportFileName)
        {
            ViewModel = vm;
            FileName = reportFileName;
        }
        //调用打印的模块
        public T ViewModel;
        //报表设计文件名
        public string FileName;
        /// <summary>
        /// 打印预览窗口显示标题
        /// </summary>
        public string DisplayName;
        /// <summary>
        /// 获取报表数据
        /// </summary>
        protected  ReportData _reportData;
        /// <summary>
        /// 组装报表数据
        /// 子类中必须实现，否则数据为空
        /// </summary>
        protected virtual void PopulateData()
        {
        }
        public virtual string GetReportXaml()
        {
            StreamReader reader = new StreamReader(new FileStream(string.Format(@"Reports\{0}",FileName), FileMode.Open, FileAccess.Read));
            string result = reader.ReadToEnd();
            reader.Close();
            return result;
        }
        public ReportData GetReportData()
        {
            if (_reportData == null)
            {
                _reportData = new ReportData();
                PopulateData();
            }
            return _reportData;
        }
        public string GetDisplayName()
        {
            return  DisplayName;
        }
    }
}
