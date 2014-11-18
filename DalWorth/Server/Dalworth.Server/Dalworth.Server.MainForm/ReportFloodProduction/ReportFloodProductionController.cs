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
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.ReportFloodProduction
{
    public class ReportFloodProductionController : NestedReportController<ReportFloodProductionModel, ReportFloodProductionView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_ctlDateRange.DateRangeValueChanged += OnDateRangeChanged;
            View.m_gridFloodProductionView.CustomSummaryCalculate += OnSummaryCalculate;            
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

        #region OnSummaryCalculate

        private void OnSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridSummaryItem item = (GridSummaryItem)e.Item;

            if (item.FieldName == View.m_columnSoldPct.FieldName)
            {
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (e.IsTotalSummary)
                    {
                        decimal floodsScheduled = (decimal)View.m_columnFloodScheduledQty.SummaryItem.SummaryValue;
                        decimal floodsSold = (decimal)View.m_columnFloodsSoldQty.SummaryItem.SummaryValue;

                        if (floodsScheduled != 0)
                            e.TotalValue = floodsSold / floodsScheduled;
                        else
                            e.TotalValue = decimal.Zero;

                    }
                }
                else
                    e.TotalValueReady = true;
            }
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
                    View.m_gridFloodProduction.DataSource = Model.FloodProductions;
                }                
            }
        }

        #endregion

        #region GenerateReport

        private ReportFloodProduction GenerateReport()
        {
            ReportFloodProduction report;

            using (new WaitCursor())
            {                
                report = new ReportFloodProduction();
                report.SetDataSource(Model.FloodProductions);
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
            ReportFloodProduction report = GenerateReport();

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
            ReportFloodProductionXls report;

            using (new WaitCursor())
            {
                report = new ReportFloodProductionXls();
                report.SetDataSource(Model.FloodProductions);                
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
            return "FloodProductionReport-" + String.Format("{0:yyyy-MM-dd}", Model.StartDate) + "-" + String.Format("{0:yyyy-MM-dd}", Model.EndDate);
        }

        #endregion
    }
}
