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

namespace Dalworth.Server.MainForm.ReportAdSourceByYear
{
    public class ReportAdSourceByYearController : NestedReportController<ReportAdSourceByYearModel, ReportAdSourceByYearView>
    {
        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtYear.Text = DateTime.Now.Year.ToString();
            View.m_cmbProjectType.SelectedIndex = 0;

            View.m_txtYear.TextChanged += OnYearChanged;
            View.m_cmbProjectType.SelectedValueChanged += OnProjectTypeChanged;

            RefreshData();
        }

        #endregion

        #region OnYearChanged

        private void OnYearChanged(object sender, EventArgs e)
        {
            string year = Utils.ExtractDigits(View.m_txtYear.Text);

            if (year.Length == 4)
                RefreshData();
        }

        #endregion

        #region OnProjectTypeChanged

        private void OnProjectTypeChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region RefreshData

        public void RefreshData()
        {
            using (new WaitCursor())
            {
                int year = int.Parse(Utils.ExtractDigits(View.m_txtYear.Text));

                int projectTypeId = 0;
                if (View.m_cmbProjectType.EditValue != null)
                    projectTypeId = (int)View.m_cmbProjectType.EditValue;

                Model.RefreshReportData(year, projectTypeId);
                
                View.m_gridAdSourceByYear.DataSource = Model.AdSources;
            }
        }

        #endregion

        #region PassParamsToReport

        private void PassParamsToReport(ReportDocument report)
        {
            string reportYear = View.m_txtYear.Text;
            string projectType = View.m_cmbProjectType.Text;

            ParameterFieldDefinitions reportDefinitions;
            
            ParameterFieldDefinition reportYearDefinition;
            ParameterFieldDefinition projectTypeDefinition;

            ParameterValues reportYearValues;
            ParameterValues projectTypeValues;

            reportDefinitions = report.DataDefinition.ParameterFields;

            reportYearDefinition = reportDefinitions["reportYear"];
            ParameterDiscreteValue reportYearValue = new ParameterDiscreteValue();
            reportYearValue.Value = reportYear;

            reportYearValues = reportYearDefinition.CurrentValues;
            reportYearValues.Clear();
            reportYearValues.Add(reportYearValue);
            reportYearDefinition.ApplyCurrentValues(reportYearValues);

            projectTypeDefinition = reportDefinitions["projectType"];
            ParameterDiscreteValue projectTypeValue = new ParameterDiscreteValue();
            projectTypeValue.Value = projectType;

            projectTypeValues = projectTypeDefinition.CurrentValues;
            projectTypeValues.Clear();
            projectTypeValues.Add(projectTypeValue);
            projectTypeDefinition.ApplyCurrentValues(projectTypeValues);
        }

        #endregion

        #region GenerateReport

        private ReportAdSourceByYear GenerateReport()
        {
            ReportAdSourceByYear report;

            using (new WaitCursor())
            {
                report = new ReportAdSourceByYear();
                report.SetDataSource(Model.AdSources);
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;

                PassParamsToReport(report);
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
            ReportAdSourceByYear report = GenerateReport();

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
            ReportAdSourceByYearXls report;

            using (new WaitCursor())
            {
                report = new ReportAdSourceByYearXls();
                report.SetDataSource(Model.AdSources);

                PassParamsToReport(report);
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
            return "AdSourceByYearReport-"+ Model.Year + "-" + Model.ProjectType;
        }

        #endregion
    }
}
