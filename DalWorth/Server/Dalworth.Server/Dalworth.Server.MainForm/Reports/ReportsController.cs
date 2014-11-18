using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.ReportAdSourceByYear;
using Dalworth.Server.MainForm.ReportBooking;
using Dalworth.Server.MainForm.ReportConstructionLead;
using Dalworth.Server.MainForm.ReportConstructionManager;
using Dalworth.Server.MainForm.ReportConstructionSummary;
using Dalworth.Server.MainForm.ReportConstructionTimeLine;
using Dalworth.Server.MainForm.ReportEquipmentSummary;
using Dalworth.Server.MainForm.ReportFloodProduction;
using Dalworth.Server.MainForm.ReportInvoice;
using Dalworth.Server.MainForm.ReportRevenue;
using Dalworth.Server.MainForm.ReportRugPending;
using Dalworth.Server.MainForm.ReportRugProduction;
using Dalworth.Server.MainForm.ReportRugReadyAging;
using Dalworth.Server.MainForm.ReportTechnicianProduction;
using Dalworth.Server.MainForm.TaskEdit;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.Reports
{
    public class ReportsController : NestedController<ReportsModel, ReportsView>
    {
        private Controller m_currentReportController;        

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_cmbReport.SelectedIndexChanged += OnReportChanged;
            View.m_btnPreview.Click += OnPreviewClick;
            View.m_btnPrint.Click += OnPrintClick;            
            View.m_btnExportXls.Click += OnExportXlsClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
        }

        #endregion

        #region RefreshData

        public void RefreshData()
        {
            OnReportChanged(null, null);
        }

        #endregion

        #region OnReportChanged

        private void OnReportChanged(object sender, EventArgs e)
        {
            View.m_pnlReportContent.Controls.Clear();

            if (View.m_cmbReport.SelectedIndex == 0) //Equipment Summary Report
            {
                ReportEquipmentSummaryController controller
                    = Prepare<ReportEquipmentSummaryController>();                
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }
            else if (View.m_cmbReport.SelectedIndex == 1) //Daily Flood Production
            {
                ReportFloodProductionController controller
                    = Prepare<ReportFloodProductionController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }
            else if (View.m_cmbReport.SelectedIndex == 2) //Daily Rug Production
            {
                ReportRugProductionController controller
                    = Prepare<ReportRugProductionController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }
            else if (View.m_cmbReport.SelectedIndex == 3) //Daily Technician Production
            {
                ReportTechnicianProductionController controller
                    = Prepare<ReportTechnicianProductionController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }
            else if (View.m_cmbReport.SelectedIndex == 4) //Pending Rug Report
            {
                ReportRugPendingController controller
                    = Prepare<ReportRugPendingController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);                
            }
            else if (View.m_cmbReport.SelectedIndex == 5) //Ready Rug Order Aging
            {
                ReportRugReadyAgingController controller
                    = Prepare<ReportRugReadyAgingController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);                
            }
            else if (View.m_cmbReport.SelectedIndex == 6) //Construction TimeLine
            {
                ReportConstructionTimeLineController controller
                    = Prepare<ReportConstructionTimeLineController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);                
            }
            else if (View.m_cmbReport.SelectedIndex == 7) //Construction Manager
            {
                ReportConstructionManagerController controller
                    = Prepare<ReportConstructionManagerController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);                
            }
            else if (View.m_cmbReport.SelectedIndex == 8) //Construction Lead
            {
                ReportConstructionLeadController controller
                    = Prepare<ReportConstructionLeadController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);                
            }
            else if (View.m_cmbReport.SelectedIndex == 9) //Booking
            {
                ReportBookingController controller
                    = Prepare<ReportBookingController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }

            else if (View.m_cmbReport.SelectedIndex == 10) //Construction Summary
            {
                ReportConstructionSummaryController controller
                    = Prepare<ReportConstructionSummaryController>();
                controller.ProjectType = ProjectTypeEnum.Construction; // set project type as Construction
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }

            else if (View.m_cmbReport.SelectedIndex == 11) //Content Summary
            {
                ReportConstructionSummaryController controller
                    = Prepare<ReportConstructionSummaryController>();
                controller.ProjectType = ProjectTypeEnum.Content; // set project type as Content
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }

            else if (View.m_cmbReport.SelectedIndex == 12) //Ad source by Year
            {
                ReportAdSourceByYearController controller
                    = Prepare<ReportAdSourceByYearController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }

            else if (View.m_cmbReport.SelectedIndex == 13) //Revenue
            {
                ReportRevenueController controller
                    = Prepare<ReportRevenueController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }

            else if (View.m_cmbReport.SelectedIndex == 14) //Invoices
            {
                ReportInvoiceController controller
                    = Prepare<ReportInvoiceController>();
                m_currentReportController = controller;
                View.m_pnlReportContent.Controls.Clear();
                View.m_pnlReportContent.Controls.Add(controller.View);
            }

            View.m_btnPreview.Enabled =
                (bool)m_currentReportController.GetType().GetMethod("IsPreviewImplemented").Invoke(
                    m_currentReportController, null);

            View.m_btnPrint.Enabled =
                (bool)m_currentReportController.GetType().GetMethod("IsPrintImplemented").Invoke(
                    m_currentReportController, null);

            View.m_btnExportXls.Enabled = 
                (bool)m_currentReportController.GetType().GetMethod("IsXlsExportImplemented").Invoke(
                    m_currentReportController, null);
        }

        #endregion

        #region Print & Preview

        private void OnPrintClick(object sender, EventArgs e)
        {
            m_currentReportController.GetType().GetMethod("OnPrint").Invoke(m_currentReportController, null);
        }

        private void OnPreviewClick(object sender, EventArgs e)
        {
            m_currentReportController.GetType().GetMethod("OnPreview").Invoke(m_currentReportController, null);
        }

        #endregion

        #region OnExportXlsClick

        private void OnExportXlsClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (m_currentReportController.GetType().GetMethod("GetReportFileName") != null)
                dialog.FileName = (string)m_currentReportController.GetType().GetMethod("GetReportFileName").Invoke(m_currentReportController, null);
            else
                dialog.FileName = "Report1";
            
            dialog.AddExtension = true;
            dialog.Filter = "Microsoft Office Excel Workbook (*.xls)|*.xls";
            if (dialog.ShowDialog() == DialogResult.OK)
            {                                
                m_currentReportController.GetType().GetMethod("ExportXls").Invoke(
                    m_currentReportController, new string[1] { dialog.FileName });

                Process.Start(dialog.FileName);
            }                
        }

        #endregion

    }
}
