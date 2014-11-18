using System;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using Dalworth.Server.MainForm.ReportPreview;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.ReportInvoice
{
    public class ReportInvoiceController : NestedReportController<ReportInvoiceModel, ReportInvoiceView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_ctlDateRange.DateRangeValueChanged += OnDateRangeChanged;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
        }

        #endregion

        #region OnDateRangeChanged

        private void OnDateRangeChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion
        
        #region RefreshData

        public void RefreshData()
        {
            using (new WaitCursor())
            {
                if (View.m_ctlDateRange.EditValue.StartDate.HasValue
                    && View.m_ctlDateRange.EditValue.EndDate.HasValue)
                {
                    Model.RefreshReportData(
                        View.m_ctlDateRange.EditValue.StartDate.Value,
                        View.m_ctlDateRange.EditValue.EndDate.Value);
                    View.m_gridInvoices.DataSource = Model.InvoiceWrappers;
                }                
            }
        }

        #endregion

        #region GenerateReport

        private ReportInvoice GenerateReport()
        {
            ReportInvoice report;

            using (new WaitCursor())
            {
                report = new ReportInvoice();
                report.SetDataSource(Model.InvoiceWrappers);
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;
            }

            return report;
        }
       
        #endregion

        #region OnPrint

        public override bool IsPrintImplemented()
        {
            return true;
        }

        public override void OnPrint()
        {
            try
            {
                GenerateReport().PrintToPrinter(0, false, 1, 0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Unable to print report",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion        

        #region OnPreview

        public override bool IsPreviewImplemented()
        {
            return true;
        }

        public override void OnPreview()
        {
            ReportInvoice report = GenerateReport();

            using (ReportPreviewController controller
                = Prepare<ReportPreviewController>(report))
            {
                controller.Execute(false);
            }                                                       
        }

        #endregion     
  
        #region Export

        public override bool IsXlsExportImplemented()
        {
            return true;
        }

        public override void ExportXls(string path)
        {
            ReportInvoiceXls report;

            using (new WaitCursor())
            {
                report = new ReportInvoiceXls();
                report.SetDataSource(Model.InvoiceWrappers);                
            }

            try
            {
                report.ExportToDisk(ExportFormatType.Excel, path);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Unable to export report",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region GetReportFileName

        public string GetReportFileName()
        {
            return "InvoiceReport-" + String.Format("{0:yyyy-MM-dd}", Model.StartDate) + "-" + String.Format("{0:yyyy-MM-dd}", Model.EndDate);
        }

        #endregion
    }
}
