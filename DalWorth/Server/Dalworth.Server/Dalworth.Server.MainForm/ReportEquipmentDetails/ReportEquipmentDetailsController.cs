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
using Dalworth.Server.MainForm.ReportEquipmentDetails;
using Dalworth.Server.MainForm.ReportEquipmentSummary;
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

namespace Dalworth.Server.MainForm.ReportEquipmentDetails
{
    public class ReportEquipmentDetailsController : Controller<ReportEquipmentDetailsModel, ReportEquipmentDetailsView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Location = (Location) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnPreview.Click += OnPreviewClick;
            View.m_btnPrint.Click += OnPrint;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblLocation.Text = Model.LocationPrint.Line1;
            View.m_lblLocationAddressRow1.Text = Model.LocationPrint.Line2;
            View.m_lblLocationAddressRow2.Text = Model.LocationPrint.Line3;

            View.m_gridEquipmentDetails.DataSource = Model.EquipmentNumbers;
        }

        #endregion

        #region GenerateReport

        private ReportEquipmentDetails GenerateReport()
        {
            ReportEquipmentDetails report;

            using (new WaitCursor())
            {                
                report = new ReportEquipmentDetails();
                Model.PreparePrintData();
                report.SetDataSource(Model.EquipmentsPrint);
                List<LocationPrint> locationPrints = new List<LocationPrint>();
                locationPrints.Add(Model.LocationPrint);
                report.Subreports[0].SetDataSource(locationPrints);
                report.PrintOptions.PrinterName = Configuration.ReportPrinter;
            }

            return report;
        }

        #endregion

        #region OnPrint

        private void OnPrint(object sender, EventArgs e)
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

        #region OnPreviewClick

        private void OnPreviewClick(object sender, EventArgs e)
        {
            ReportEquipmentDetails report = GenerateReport();

            using (ReportPreviewController controller
                = Prepare<ReportPreviewController>(report))
            {
                controller.Execute(false);
            }
        }

        #endregion
    }
}
