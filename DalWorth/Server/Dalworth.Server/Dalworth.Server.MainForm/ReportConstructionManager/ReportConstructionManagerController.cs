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

namespace Dalworth.Server.MainForm.ReportConstructionManager
{
    public class ReportConstructionManagerController : NestedReportController<ReportConstructionManagerModel, ReportConstructionManagerView>
    {        
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_dtpMonth.DateTimeChanged += OnMonthChanged;
            View.m_dtpMonth.ButtonClick += OnMonthButtonClick;

            View.m_gridView.CustomSummaryCalculate += OnSummaryCalculate;            
            View.m_gridView.CustomColumnDisplayText += OnGridDisplayText;
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

        #region OnGridDisplayText

        private void OnGridDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if ((e.Column.Name == View.m_colJobCost.Name && e.Value == null)
                || (e.Column.Name == View.m_colProfit.Name && e.Value == null)
                || (e.Column.Name == View.m_colProfitPercentage.Name && e.Value == null))
            {
                e.DisplayText = "N/A";
            }
        }

        #endregion


        #region OnSummaryCalculate

        private decimal? m_jobCostTotal;
        private decimal? m_profitTotal;

        private void OnSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {   
            GridSummaryItem item = (GridSummaryItem)e.Item;
                
            if (item.FieldName == View.m_colJobCost.FieldName)
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                    m_jobCostTotal = decimal.Zero;
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (e.FieldValue == null)
                    {
                        m_jobCostTotal = null;
                        e.TotalValueReady = true;
                    } else
                        m_jobCostTotal += (decimal)e.FieldValue;                            
                } 
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (m_jobCostTotal == null)
                        e.TotalValue = "N/A";
                    else
                        e.TotalValue = m_jobCostTotal.Value;
                }                    
            }
            else if (item.FieldName == View.m_colProfit.FieldName)
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                    m_profitTotal = decimal.Zero;
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    if (e.FieldValue == null)
                    {
                        m_profitTotal = null;
                        e.TotalValueReady = true;
                    }
                    else
                        m_profitTotal += (decimal)e.FieldValue;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (m_profitTotal == null)
                        e.TotalValue = "N/A";
                    else
                        e.TotalValue = m_profitTotal.Value;
                }                                    
            }
            else if (item.FieldName == View.m_colProfitPercentage.FieldName)
            {
                if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    if (e.IsTotalSummary)
                    {
                        object totalProfit = View.m_gridView.Columns["ProfitAmount"].SummaryItem.SummaryValue;
                        decimal totalBilled = (decimal)View.m_gridView.Columns["BilledAmount"].SummaryItem.SummaryValue;

                        if (totalProfit is string)
                            e.TotalValue = "N/A";
                        else if (totalBilled != 0)
                            e.TotalValue = (decimal)totalProfit / totalBilled;
                        else
                            e.TotalValue = decimal.Zero;

                    }
                    else if (e.IsGroupSummary)
                    {
                        object totalProfit = View.m_gridView.GetGroupSummaryValue(e.GroupRowHandle,
                            (GridGroupSummaryItem)View.m_gridView.GroupSummary[3]);
                        decimal totalBilled = (decimal)View.m_gridView.GetGroupSummaryValue(e.GroupRowHandle,
                            (GridGroupSummaryItem)View.m_gridView.GroupSummary[0]);

                        if (totalProfit is string)
                            e.TotalValue = "N/A";
                        else if (totalBilled != 0)
                            e.TotalValue = (decimal)totalProfit / totalBilled;
                        else
                            e.TotalValue = decimal.Zero;
                    }

                } else
                    e.TotalValueReady = true;
            }
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
                View.m_grid.DataSource = Model.ConstructionManagers;
                View.m_gridView.ExpandAllGroups();
            }
        }

        #endregion

        #region GenerateReport

        private ReportConstructionManager GenerateReport()
        {
            ReportConstructionManager report;

            using (new WaitCursor())
            {                
                report = new ReportConstructionManager();
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
            if (Model.ConstructionManagers.Count == 0)
            {
                XtraMessageBox.Show("Report has no data", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }                

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
            if (Model.ConstructionManagers.Count == 0)
            {
                XtraMessageBox.Show("Report has no data", "Preview", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }                

            ReportConstructionManager report = GenerateReport();

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
            ReportConstructionManagerXls report;

            using (new WaitCursor())
            {
                report = new ReportConstructionManagerXls();
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
