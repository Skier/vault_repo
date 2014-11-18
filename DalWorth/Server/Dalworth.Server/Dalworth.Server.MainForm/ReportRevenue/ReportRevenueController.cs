using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.ReportPreview;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.ReportRevenue
{
    public class ReportRevenueController : NestedReportController<ReportRevenueModel, ReportRevenueView>
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
            DateTime dateEnd = DateTime.Now;
            DateTime dateStart = new DateTime(dateEnd.Year, dateEnd.Month, 1);

            if (View.m_ctlDateRange.EditValue.IsNull)
                View.m_ctlDateRange.EditValue = new DateRange(dateStart, dateEnd);

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
                    View.m_gridRevenue.DataSource = Model.Revenues;
                    View.m_lblGrandTotal.Text = Model.GrandTotal.ToString("c");
                }
            }
        }

        #endregion

        #region GenerateReport

        private ReportRevenue GenerateReport()
        {
            ReportRevenue report;

            using (new WaitCursor())
            {
                report = new ReportRevenue();
                report.SetDataSource(Model.Revenues);
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;

                PassTotalToReport(report);
            }

            return report;
        }

        #endregion

        #region PassTotalToReport

        private void PassTotalToReport(ReportDocument report)
        {
            ParameterFieldDefinitions reportDefinitions;
            ParameterFieldDefinition reportDefinition;
            ParameterValues reportParamValues;
            ParameterDiscreteValue reportParamValue = new ParameterDiscreteValue();

            reportParamValue.Value = Model.GrandTotal;

            reportDefinitions = report.DataDefinition.ParameterFields;
            reportDefinition = reportDefinitions["grandTotal"];
            reportParamValues = reportDefinition.CurrentValues;

            reportParamValues.Clear();
            reportParamValues.Add(reportParamValue);
            reportDefinition.ApplyCurrentValues(reportParamValues);
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
            ReportRevenue report = GenerateReport();

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
            ReportRevenueXls report;

            using (new WaitCursor())
            {
                report = new ReportRevenueXls();
                report.SetDataSource(Model.Revenues);

                PassTotalToReport(report);
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

        #region "GetReportFileName"

        public string GetReportFileName()
        {
            return "RevenueReport-" + String.Format("{0:yyyy-MM-dd}", Model.StartDate) + "-" + String.Format("{0:yyyy-MM-dd}", Model.EndDate);
        }

        #endregion
    }
}
