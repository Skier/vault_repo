using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.ReportPreview;
using Dalworth.Server.MainForm.Reports;
using Dalworth.Server.MainForm.TaskEdit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.ReportTechnicianProduction
{
    public class ReportTechnicianProductionController : NestedReportController<ReportTechnicianProductionModel, ReportTechnicianProductionView>
    {
        #region CurrentTechnician

        private Employee CurrentTechnician
        {
            get { return (Employee) ((ImageComboBoxItem) View.m_cmbTechnician.SelectedItem).Value; }
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_ctlDateRange.DateRangeValueChanged += OnDateRangeChanged;
            View.m_cmbTechnician.SelectedIndexChanged += OnTechnicianChanged;

        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (View.m_ctlDateRange.EditValue.IsNull)
                View.m_ctlDateRange.EditValue = new DateRange(DateTime.Now.AddDays(-14), DateTime.Now);

            if (View.m_cmbTechnician.Properties.Items.Count == 0)
            {
                foreach (Employee technician in Model.Technicians)
                {
                    View.m_cmbTechnician.Properties.Items.Add(
                        new ImageComboBoxItem(technician.DisplayName, technician));
                }

                View.m_cmbTechnician.SelectedIndex = 0;
            } else
                RefreshData();
        }

        #endregion


        #region OnDateRangeChanged

        private void OnDateRangeChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region OnTechnicianChanged

        private void OnTechnicianChanged(object sender, EventArgs e)
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
                    && View.m_ctlDateRange.EditValue.EndDate.HasValue
                    && View.m_cmbTechnician.SelectedIndex >= 0)
                {
                    Model.RefreshReportData(
                        CurrentTechnician,
                        View.m_ctlDateRange.EditValue.StartDate.Value,
                        View.m_ctlDateRange.EditValue.EndDate.Value);
                    View.m_gridTechnicianProduction.DataSource = Model.TechnicianProductions;
                }                
            }
        }

        #endregion

        #region GenerateReport

        private ReportTechnicianProduction GenerateReport()
        {
            ReportTechnicianProduction report;

            using (new WaitCursor())
            {                
                report = new ReportTechnicianProduction();
                report.SummaryInfo.ReportTitle =
                    CurrentTechnician.FirstName + " " + CurrentTechnician.LastName;
                report.SetDataSource(Model.GetPrintedData());
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
            ReportTechnicianProduction report = GenerateReport();

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
            ReportTechnicianProductionXls report;

            using (new WaitCursor())
            {
                report = new ReportTechnicianProductionXls();
                report.SummaryInfo.ReportTitle =
                    CurrentTechnician.FirstName + " " + CurrentTechnician.LastName;
                report.SetDataSource(Model.GetPrintedData());                
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
    }
}
