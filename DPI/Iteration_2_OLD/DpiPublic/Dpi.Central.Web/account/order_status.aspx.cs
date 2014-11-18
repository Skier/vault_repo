using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controllers;
using DPI.Components;

namespace Dpi.Central.Web.Account
{
    public class OrderStatus : Page
    {
        protected DataGrid grdOrderStatuses;
        private int _workFinishColIdx = -1;
        
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

        }

        #endregion

        private void BindEvents() {
            grdOrderStatuses.DataBinding += new EventHandler(grdOrderStatuses_DataBinding);
            grdOrderStatuses.ItemDataBound += new DataGridItemEventHandler(grdOrderStatuses_ItemDataBound);
        }

        private void grdOrderStatuses_DataBinding(object sender, EventArgs e) {
            ActivationWorkLog[] awls = OrderStatusController.Instance.GetOrderStatus();
            if (awls == null || awls.Length == 0) {
                grdOrderStatuses.Visible = false;
                return;
            }

            for (int i = 0; i < grdOrderStatuses.Columns.Count; i++) {
                DataGridColumn column = grdOrderStatuses.Columns[i];
                if (column is BoundColumn && ((BoundColumn)column).DataField == "WorkFinish") {
                    _workFinishColIdx = i;
                    break;
                }
            }
            if (_workFinishColIdx == -1) {
                throw new ApplicationException("The WorkFinish column not exists");
            }

            grdOrderStatuses.DataSource = awls;
        }
        
        private void grdOrderStatuses_ItemDataBound(object sender, DataGridItemEventArgs ea) {
            if (ea.Item.DataItem == null) {
                return;
            }

            ActivationWorkLog awl = (ActivationWorkLog)ea.Item.DataItem;
            if (awl.WorkFinish == DateTime.MinValue) {
                TableCell cell = ea.Item.Cells[_workFinishColIdx];
                cell.Controls.Clear();
                cell.Style["text-align"] = "center";
                cell.Text = "&mdash;";
            }
        }
    }
}