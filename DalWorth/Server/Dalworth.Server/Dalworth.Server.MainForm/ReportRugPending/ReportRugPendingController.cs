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
using Dalworth.Server.Domain.Reports;
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

namespace Dalworth.Server.MainForm.ReportRugPending
{
    public class ReportRugPendingController : NestedReportController<ReportRugPendingModel, ReportRugPendingView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {            
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            RefreshData();
        }

        #endregion        
        
        #region RefreshData

        public void RefreshData()
        {
            Model.RefreshReportData();

            View.m_lblInWorkOrders.Text = Model.RugPending.InWorkOrderQty.ToString();
            View.m_lblInWorkRugs.Text = Model.RugPending.InWorkRugQty.ToString();
            View.m_lblInWorkTotalAmount.Text = Model.RugPending.InWorkAmt.ToString("C");
            View.m_lblReadyOrders.Text = Model.RugPending.ReadyOrderQty.ToString();
            View.m_lblReadyTotalAmount.Text = Model.RugPending.ReadyAmt.ToString("C");
            View.m_lblTotalAmount.Text = Model.RugPending.TotalAmt.ToString("C");
        }

        #endregion

        #region GenerateReport

        private ReportRugPending GenerateReport()
        {
            ReportRugPending report;

            using (new WaitCursor())
            {
                report = new ReportRugPending();
                List<RugPending> dataSource = new List<RugPending>();
                dataSource.Add(Model.RugPending);
                report.SetDataSource(dataSource);
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
            ReportRugPending report = GenerateReport();

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
            try
            {
                GenerateReport().ExportToDisk(ExportFormatType.Excel, path);
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
