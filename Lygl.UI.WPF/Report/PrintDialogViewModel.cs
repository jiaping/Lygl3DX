using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.UI.Framework.ViewModelBase;
using System.Windows;
using Caliburn.Micro;
using System.Windows.Threading;
using CodeReason.Reports;
using System.IO;
using System.Data;
using System.Windows.Xps.Packaging;
using CodeReason.Reports.Controls;
using System.Windows.Controls;

namespace Lygl.UI.Report
{
    public class PrintDialogViewModel : Screen
    {
        private bool _firstActivated = true;

        IReportData ReportDataHelper;
        public PrintDialogViewModel(IReportData reportDataHelper)
        {
            DisplayName = reportDataHelper.GetDisplayName();
            ReportDataHelper = reportDataHelper;
            this.Activated+=new EventHandler<ActivationEventArgs>(PrintDialogViewModel_Activated);
        }

       
        void PrintDialogViewModel_Activated(object sender, EventArgs e)
        {
            if (!_firstActivated) return;
            _firstActivated = false;
            Execute.OnUIThread(delegate
            {
                //Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new System.Action(delegate
                //{
                try
                {
                    ReportDocument reportDocument = new ReportDocument();
                    reportDocument.XamlData = ReportDataHelper.GetReportXaml();
                    reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Reports\");

                    DateTime dateTimeStart = DateTime.Now; // start time measure here

                    //List<ReportData> listData = new List<ReportData>();
                    //for (int i = 0; i < 2; i++) // generates multiple reports
                    //{
                    //    ReportData data = new ReportData();
                        
                    //    // set constant document values
                    //    data.ReportDocumentValues.Add("PrintDate", dateTimeStart); // print date is now
                    //    data.ReportDocumentValues.Add("ReportNumber", (i + 1)); // report number

                    //    // sample table "Ean"
                    //    DataTable table = new DataTable("Ean");
                    //    table.Columns.Add("Position", typeof(string));
                    //    table.Columns.Add("Item", typeof(string));
                    //    table.Columns.Add("EAN", typeof(string));
                    //    table.Columns.Add("Count", typeof(int));
                    //    Random rnd = new Random(1234 + i);
                    //    int count = rnd.Next(10) * (rnd.Next(2) + 1);
                    //    for (int j = 1; j <= count; j++)
                    //    {
                    //        // randomly create some articles
                    //        table.Rows.Add(new object[] { j, "Item " + (j + (1000 * (i + 1))).ToString("0000"), "123456790123", rnd.Next(9) + 1 });
                    //    }
                    //    data.DataTables.Add(table);
                    //    data.ReportDocumentValues.Add("MxName", "墓穴名"+i.ToString());
                    //    listData.Add(data);
                    //}


                    //XpsDocument xps = reportDocument.CreateXpsDocument(listData);
                    //XpsDocument xps = reportDocument.CreateXpsDocument(SetupLbFlowDocumentData());
                    //_documentViewer.Document = xps.GetFixedDocumentSequence();
                    reportDocument.XamlData = ReportDataHelper.GetReportXaml();
                    reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Reports\");

                    XpsDocument xps = reportDocument.CreateXpsDocument(ReportDataHelper.GetReportData());
                    _documentViewer.Document = xps.GetFixedDocumentSequence();

                    // show the elapsed time in window title
                    //Title += " - generated in " + (DateTime.Now - dateTimeStart).TotalMilliseconds + "ms";
                }
                catch (Exception ex)
                {
                    // show exception
                    MessageBox.Show(ex.Message + "\r\n\r\n" + ex.GetType() + "\r\n" + ex.StackTrace, ex.GetType().ToString(), MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                finally
                {
                     _busyDecorator.IsBusyIndicatorHidden = true;
                }
            });

        }

        //private XpsDocument SetupLbFlowDocumentData(ReportDocument reportDocument)
        //{
        //    reportDocument.XamlData = ReportDataHelper.GetReportXaml();
        //    reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Templates\");

        //    XpsDocument xps = reportDocument.CreateXpsDocument(ReportDataHelper.GetReportData());
            
        //    return xps;
        //}

        private XpsDocument SetupInvoiceFlowDocumentData(ReportDocument reportDocument)
        {
            StreamReader reader = new StreamReader(new FileStream(@"Templates\Bw.xaml", FileMode.Open, FileAccess.Read));
            reportDocument.XamlData = reader.ReadToEnd();
            reportDocument.XamlImagePath = Path.Combine(Environment.CurrentDirectory, @"Templates\");
            reader.Close();

            ReportData data = new ReportData();
            BwReportDataHelper bwDataHelper = ReportDataHelper as BwReportDataHelper;

            data.ReportDocumentValues.Add("MxName", bwDataHelper.MxName);
            data.ReportDocumentValues.Add("OperatorCode", bwDataHelper.OperatorCode);
            data.ReportDocumentValues.Add("OperateTime", bwDataHelper.OperateTime);
            data.ReportDocumentValues.Add("Bx", bwDataHelper.Bx);
            data.ReportDocumentValues.Add("Drawee", bwDataHelper.Drawee);
            data.ReportDocumentValues.Add("PayFlag", bwDataHelper.PayFlag.ToString());

            data.ReportDocumentValues.Add("LbrText", bwDataHelper.LbrText);
            data.ReportDocumentValues.Add("LbsjText", bwDataHelper.LbsjText);

            data.ReportDocumentValues.Add("Ch1", bwDataHelper.Ch1);
            data.ReportDocumentValues.Add("Ch2", bwDataHelper.Ch2);
            data.ReportDocumentValues.Add("Sheng1", bwDataHelper.Sheng1);
            data.ReportDocumentValues.Add("Sheng2", bwDataHelper.Sheng2);
            data.ReportDocumentValues.Add("Gu1", bwDataHelper.Gu1);
            data.ReportDocumentValues.Add("Gu2", bwDataHelper.Gu2);

            data.DataTables.Add(bwDataHelper.GetProductLbItems());

            XpsDocument xps = reportDocument.CreateXpsDocument(data);
            return xps;
        }

        
        private CodeReason.Reports.Controls.BusyDecorator  _busyDecorator;
        private System.Windows.Controls.DocumentViewer _documentViewer;

        protected  override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            _busyDecorator = (BusyDecorator)(view as FrameworkElement).FindName("busyDecorator");
            _documentViewer = (DocumentViewer)(view as FrameworkElement).FindName("documentViewer");
            
        }

        //protected override void OnViewLoaded(object view)
        //{
        //    base.OnViewLoaded(view);
            

            

        //}
    }
}
