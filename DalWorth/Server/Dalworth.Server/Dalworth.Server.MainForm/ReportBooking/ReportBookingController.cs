using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.ReportPreview;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.ReportBooking
{
    public class ReportBookingController : NestedReportController<ReportBookingModel, ReportBookingView>
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
            if (View.m_ctlDateRange.EditValue.IsNull)
                View.m_ctlDateRange.EditValue = new DateRange(DateTime.Now.AddDays(-14), DateTime.Now);

            RefreshData();
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
                    View.m_gridBooking.DataSource = Model.Bookings;
                }
            }
        }

        #endregion

        #region GenerateReport

        private ReportBooking GenerateReport()
        {
            ReportBooking report;

            using (new WaitCursor())
            {
                report = new ReportBooking();
                report.SetDataSource(Model.Bookings);
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
            try
            {
                ReportBooking report = GenerateReport();

                using (ReportPreviewController controller
                    = Prepare<ReportPreviewController>(report))
                {
                    controller.Execute(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Unable to preview report",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            ReportBooking report;

            try
            {
                using (new WaitCursor())
                {
                    report = new ReportBooking();
                    report.SetDataSource(Model.Bookings);
                }

                report.ExportToDisk(ExportFormatType.Excel, path);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Unable to export report",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #endregion

        #region "GetReportFileName"

            public string GetReportFileName()
            {
                return "BookingReport-" + String.Format("{0:yyyy-MM-dd}", Model.StartDate) + "-" + String.Format("{0:yyyy-MM-dd}", Model.EndDate);
            }

        #endregion
    }
}
