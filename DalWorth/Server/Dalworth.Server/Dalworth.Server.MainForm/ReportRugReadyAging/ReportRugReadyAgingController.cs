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

namespace Dalworth.Server.MainForm.ReportRugReadyAging
{
    public class ReportRugReadyAgingController : NestedReportController<ReportRugReadyAgingModel, ReportRugReadyAgingView>
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

            View.m_lbl5DaysCount.Text = Model.RugReadyAging[0].RugsCount.ToString();
            View.m_lbl5DaysAmount.Text = Model.RugReadyAging[0].EstimatedClosedAmount.ToString("C");
            View.m_lbl20DaysCount.Text = Model.RugReadyAging[1].RugsCount.ToString();
            View.m_lbl20DaysAmount.Text = Model.RugReadyAging[1].EstimatedClosedAmount.ToString("C");
            View.m_lblMore20DaysCount.Text = Model.RugReadyAging[2].RugsCount.ToString();
            View.m_lblMore20DaysAmount.Text = Model.RugReadyAging[2].EstimatedClosedAmount.ToString("C");
        }

        #endregion

        #region GenerateReport

        private ReportRugReadyAging GenerateReport()
        {
            ReportRugReadyAging report;

            using (new WaitCursor())
            {
                report = new ReportRugReadyAging();
                List<RugReadyAgingSingle> datasource = new List<RugReadyAgingSingle>();
                datasource.Add(new RugReadyAgingSingle(Model.RugReadyAging));
                report.SetDataSource(datasource);
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
            ReportRugReadyAging report = GenerateReport();

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
