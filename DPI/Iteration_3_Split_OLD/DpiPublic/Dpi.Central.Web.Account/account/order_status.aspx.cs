using System;
using System.Web.UI.WebControls;

using DPI.Components;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class OrderStatus : BaseAccountPage
    {
		#region Web Form Designer generated code

        protected override void OnInit(EventArgs e) {
            BindEvents();
            InitializeComponent();
            grdOrderStatuses.DataKeyField = "Id";
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            DataBind();
        }

        private void InitializeComponent() {
            this.lbtnGotoLogin.Click += new System.EventHandler(this.lbtnGotoLogin_Click);
        }

        #endregion

        #region Event handlers

        protected DataGrid grdOrderStatuses;
        protected LinkButton lbtnGotoLogin;
        private int _workFinishColIdx = -1;

        private void BindEvents() {
            grdOrderStatuses.DataBinding += new EventHandler(grdOrderStatuses_DataBinding);
            grdOrderStatuses.ItemDataBound += new DataGridItemEventHandler(grdOrderStatuses_ItemDataBound);
        }

        private void grdOrderStatuses_DataBinding(object sender, EventArgs e) {
            ActivationWorkLog[] activationWorkLogs = CustSvc.GetOrderStatus(Map, GetAccountNumber());

            if (activationWorkLogs == null || activationWorkLogs.Length == 0) {
                grdOrderStatuses.Visible = false;
                return;
            }

            for (int i = 0; i < grdOrderStatuses.Columns.Count; i++) {
                DataGridColumn column = grdOrderStatuses.Columns[i];
                if (column is BoundColumn && ((BoundColumn) column).DataField == "WorkFinish") {
                    _workFinishColIdx = i;
                    break;
                }
            }
            if (_workFinishColIdx == -1) {
                throw new ApplicationException("The WorkFinish column does not exist");
            }

            grdOrderStatuses.DataSource = activationWorkLogs;
        }

        private void grdOrderStatuses_ItemDataBound(object sender, DataGridItemEventArgs ea) {
            if (ea.Item.DataItem == null) {
                return;
            }

            ActivationWorkLog activationWorkLog = (ActivationWorkLog) ea.Item.DataItem;
            if (activationWorkLog.WorkFinish == DateTime.MinValue) {
                TableCell cell = ea.Item.Cells[_workFinishColIdx];
                cell.Controls.Clear();
                cell.Style["text-align"] = "center";
                cell.Text = "&mdash;";
            }
        }

        private void lbtnGotoLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
        }

        #endregion
    }
}