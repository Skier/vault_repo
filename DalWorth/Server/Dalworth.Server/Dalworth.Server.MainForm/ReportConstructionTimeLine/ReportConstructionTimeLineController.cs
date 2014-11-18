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

namespace Dalworth.Server.MainForm.ReportConstructionTimeLine
{
    public class ReportConstructionTimeLineController : NestedReportController<ReportConstructionTimeLineModel, ReportConstructionTimeLineView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_dtpMonth.DateTimeChanged += OnMonthChanged;
            View.m_dtpMonth.ButtonClick += OnMonthButtonClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (View.m_dtpMonth.EditValue == null)
                View.m_dtpMonth.DateTime = DateTime.Now;

            RefreshData();
        }

        #endregion

        #region OnMonthButtonClick

        private void OnMonthButtonClick(object sender, ButtonPressedEventArgs e)
        {            
            RefreshData();
        }

        #endregion


        #region OnMonthChanged

        private void OnMonthChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        
        #region RefreshData

        public void RefreshData()
        {
            using (new WaitCursor())
            {
                Model.RefreshReportData(
                    View.m_dtpMonth.DateTime.Month,
                    View.m_dtpMonth.DateTime.Year);
                View.m_grid.DataSource = Model.ConstructionTimeLines;
                View.m_gridView.ExpandAllGroups();
            }
        }

        #endregion

        #region GenerateReport

        private ReportConstructionTimeLine GenerateReport()
        {
            ReportConstructionTimeLine report;

            using (new WaitCursor())
            {                
                report = new ReportConstructionTimeLine();
                report.SummaryInfo.ReportTitle =
                    View.m_dtpMonth.DateTime.ToString("y");                    
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
            ReportConstructionTimeLine report = GenerateReport();

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
            ReportConstructionTimeLineXls report;

            using (new WaitCursor())
            {
                report = new ReportConstructionTimeLineXls();
                report.SummaryInfo.ReportTitle =
                    View.m_dtpMonth.DateTime.ToString("y");                    
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
