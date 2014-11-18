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

namespace Dalworth.Server.MainForm.ReportEquipmentSummary
{
    public class ReportEquipmentSummaryController : NestedReportController<ReportEquipmentSummaryModel, ReportEquipmentSummaryView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_gridViewEquipmentSummary.CustomUnboundColumnData += OnGetColumnData;            
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            for (int i = 0; i < Model.EquipmentTypes.Count; i++)
            {
                GridColumn column = new GridColumn();
                column.OptionsColumn.AllowEdit = false;
                column.Caption = Model.EquipmentTypes[i].Type;
                column.VisibleIndex = i + 3;                
                column.Visible = true;
                column.FieldName = "equipmentType" + i;
                column.UnboundType = UnboundColumnType.Integer;
                column.Width = 100;
                column.Tag = i;
                View.m_gridViewEquipmentSummary.Columns.Add(column);
            }

            RefreshData();
        }

        #endregion

        #region OnGetColumnData

        private void OnGetColumnData(object sender, CustomColumnDataEventArgs e)
        {
            Location location = Model.Locations[e.ListSourceRowIndex];            
            e.Value = Model.EquipmentSummary[location][(int)e.Column.Tag];
        }

        #endregion

        #region OnAreaChanged

        private void OnAreaChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        #endregion

        #region RefreshData

        public void RefreshData()
        {
            Model.RefreshReportData();
            View.m_gridEquipmentSummary.DataSource = Model.Locations;                        
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

        #region GenerateReport

        private ReportEquipmentSummary GenerateReport()
        {
            ReportEquipmentSummary report;

            using (new WaitCursor())
            {
                Model.PreparePrintData();
                report = new ReportEquipmentSummary();
                report.SetDataSource(Model.Locations);
                report.Subreports["LocationQuantities"].SetDataSource(Model.PrintQuantities);
                report.Subreports["EquipmentTypes"].SetDataSource(Model.EquipmentTypes);
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;
            }

            return report;
        }

        #endregion

        #region OnPreview

        public override bool IsPreviewImplemented()
        {
            return true;
        }

        public override void OnPreview()
        {
            ReportEquipmentSummary report = GenerateReport();

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
            return false;
        }


        public override void ExportXls(string path)
        {
            
        }

        #endregion
    }
}
