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

namespace Dalworth.Server.MainForm.ReportConstructionSummary
{
    public class ReportConstructionSummaryController : NestedReportController<ReportConstructionSummaryModel, ReportConstructionSummaryView>
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
        }

        #endregion

        #region OnDateRangeChanged

        private void OnDateRangeChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region ProjectType

        public ProjectTypeEnum ProjectType
        {
            get { return Model.ProjectType; }
            set { Model.ProjectType = value; }
        }

        #endregion

        #region PassTitleToReport

        private void PassTitleToReport(ReportDocument report)
        {
            string reportTitle = "";
            if (ProjectType == ProjectTypeEnum.Construction)
                reportTitle = "Construction Summary Report";
            else if (ProjectType == ProjectTypeEnum.Content)
                reportTitle = "Content Summary Report";

            ParameterFieldDefinitions reportDefinitions;
            ParameterFieldDefinition reportDefinition;
            ParameterValues reportParamValues;
            ParameterDiscreteValue reportParamValue = new ParameterDiscreteValue();

            reportParamValue.Value = reportTitle;
            reportDefinitions = report.DataDefinition.ParameterFields;
            reportDefinition = reportDefinitions["reportTitle"];
            reportParamValues = reportDefinition.CurrentValues;

            reportParamValues.Clear();
            reportParamValues.Add(reportParamValue);
            reportDefinition.ApplyCurrentValues(reportParamValues);
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
                    View.m_gridConstructionSummary.DataSource = Model.ConstructionSummaries;
                }
            }
        }

        #endregion

        #region GenerateReport

        private ReportConstructionSummary GenerateReport()
        {
            ReportConstructionSummary report;

            using (new WaitCursor())
            {
                report = new ReportConstructionSummary();
                report.SetDataSource(Model.ConstructionSummaries);
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;

                PassTitleToReport(report);
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
            ReportConstructionSummary report = GenerateReport();

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
            ReportConstructionSummaryXls report;

            using (new WaitCursor())
            {
                report = new ReportConstructionSummaryXls();
                report.SetDataSource(Model.ConstructionSummaries);
 
                PassTitleToReport(report);
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
            string reportName;

            if (Model.ProjectType == ProjectTypeEnum.Construction)
                reportName = "ConstructonSummaryReport-";
            else
                reportName = "ContentSummaryReport-";

            reportName +=  String.Format("{0:yyyy-MM-dd}", Model.StartDate) + "-" + String.Format("{0:yyyy-MM-dd}", Model.EndDate);

            return reportName;
        }

        #endregion
    }
}
