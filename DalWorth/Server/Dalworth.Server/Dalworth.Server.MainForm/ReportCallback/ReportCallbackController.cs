using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
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
using Dalworth.Server.MainForm.CallbackProcess;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.MainForm.ReportEquipmentDetails;
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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.ReportCallback
{
    public class ReportCallbackController : NestedReportController<ReportCallbackModel, ReportCallbackView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_gridCallbackView.RowStyle += OnGridCallbackViewRowStyle;
            View.m_btnProcess.Click += OnProcessClick;
            View.m_gridCallbackView.DoubleClick += OnCallbackViewDoubleClick;              
            View.m_linkProcess.Click += OnLinkProcessClick;
            View.m_gridCallbackView.KeyDown += OnGridCallbackViewKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            RefreshData();
        }

        #endregion

        #region OnGridCallbackViewRowStyle

        private void OnGridCallbackViewRowStyle(object sender, RowStyleEventArgs e)
        {
            CallbackReportWrapper row = (CallbackReportWrapper)View.m_gridCallbackView.GetRow(e.RowHandle);

            if (row != null && row.IsProcessed)
                e.Appearance.BackColor = Color.LightGray;
        }

        #endregion

        #region OnProcessClick

        private void OnCallbackViewDoubleClick(object sender, EventArgs e)
        {            
            GridHitInfo hitInfo = View.m_gridCallbackView.CalcHitInfo(
                View.m_gridCallback.PointToClient(Cursor.Position));

            if (hitInfo.InRow)
                OnProcessClick(null, null);
        }

        private void OnLinkProcessClick(object sender, EventArgs e)
        {
            OnProcessClick(null, null);
        }

        private void OnGridCallbackViewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridCallback.Focused && Model.CallbackList.Count > 0)
                OnProcessClick(null, null);
        }

        private void OnProcessClick(object sender, EventArgs e)
        {
            if (View.m_gridCallbackView.SelectedRowsCount <= 0)
            {
                XtraMessageBox.Show("Please select record(s) to process", "No record(s) selected",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<CallbackReportWrapper> selectedRecords = new List<CallbackReportWrapper>();
            foreach (int handle in View.m_gridCallbackView.GetSelectedRows())
                selectedRecords.Add((CallbackReportWrapper) View.m_gridCallbackView.GetRow(handle));
            

            if (selectedRecords.Count > 1)
            {
                foreach (CallbackReportWrapper record in selectedRecords)
                {
                    if (record.IsProcessed)
                    {
                        if (XtraMessageBox.Show("Selected record set contains processed entities. Do you want to continue?", "Processed records selected",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        } 

                        break;
                    }                        
                }
            }


            using (CallbackProcessController controller = Prepare<CallbackProcessController>(selectedRecords))
            {
                controller.Execute(false);   
                if (!controller.IsCancelled)
                    Model.CallbackList.ResetBindings();
            }
        }

        #endregion

        
        #region RefreshData

        public void RefreshData()
        {
            using (new WaitCursor())
            {
                Model.RefreshReportData();
                View.m_gridCallback.DataSource = Model.CallbackList;                
            }
        }

        #endregion

        #region Overriden methods

        public override bool IsPreviewImplemented()
        {
            return false;
        }

        public override void OnPreview()
        {
            throw new NotImplementedException();
        }

        public override bool IsPrintImplemented()
        {
            return false;
        }

        public override void OnPrint()
        {
            throw new NotImplementedException();
        }

        public override bool IsXlsExportImplemented()
        {
            return false;
        }

        public override void ExportXls(string path)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
