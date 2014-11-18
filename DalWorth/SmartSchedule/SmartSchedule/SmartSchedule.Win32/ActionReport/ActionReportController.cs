using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.ActionReport
{
    public class ActionReportController : Controller<ActionReportModel, ActionReportView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnRefresh.Click += OnRefreshClick;
            View.m_cmbUser.SelectedIndexChanged += OnRefreshClick;
            View.m_cmbAction.SelectedIndexChanged += OnRefreshClick;
            View.m_cmbTechnician.SelectedIndexChanged += OnRefreshClick;
            View.m_dtpActionRange.DateRangeValueChanged += OnRefreshClick;
            View.m_txtTicket.KeyPress += OnTicketKeyPress;
            View.m_dtpDashboardDate.DateTimeChanged += OnRefreshClick;
            View.m_gridReportView.StartSorting += OnRefreshClick;

            View.m_btnClose.Click += OnCloseClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_cmbTechnician.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            View.m_cmbAction.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            View.m_cmbUser.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));

            foreach (Technician technician in Model.GetTechnicians())
            {
                View.m_cmbTechnician.Properties.Items.Add(
                    new ImageComboBoxItem(technician.Name, (object)technician.TechnicianDefaultId));
            }

            foreach (UserActionTypeEnum actionType in Enum.GetValues(typeof(UserActionTypeEnum)))
                View.m_cmbAction.Properties.Items.Add(
                    new ImageComboBoxItem(UserActionType.GetText(actionType), actionType));

            List<User> users = WcfClient.WcfClient.Instance.FindUsers();
            foreach (var user in users)
            {
                View.m_cmbUser.Properties.Items.Add(
                    new ImageComboBoxItem(user.Login, (object)user.ID));                
            }
        }

        #endregion

        #region OnRefreshClick

        private void OnTicketKeyPress(object sender, KeyPressEventArgs args)
        {
            if (args.KeyChar == '\r')
            {
                OnRefreshClick(null, null);
                args.Handled = true;
            }
        }

        private void OnRefreshClick(object sender, EventArgs eventArgs)
        {
            ImageComboBoxItem userItem = (ImageComboBoxItem) View.m_cmbUser.SelectedItem;
            ImageComboBoxItem actionItem = (ImageComboBoxItem) View.m_cmbAction.SelectedItem;
            ImageComboBoxItem technicianItem = (ImageComboBoxItem) View.m_cmbTechnician.SelectedItem;

            SortField sortField = SortField.ActionDate;
            bool isAscending = true;
            if (View.m_gridReportView.SortInfo.Count > 0)
            {
                isAscending = View.m_gridReportView.SortInfo[0].SortOrder == ColumnSortOrder.Ascending;

                if (View.m_gridReportView.SortInfo[0].Column == View.m_colAction)
                    sortField = SortField.Action;
                else if (View.m_gridReportView.SortInfo[0].Column == View.m_colActionDate)
                    sortField = SortField.ActionDate;
                else if (View.m_gridReportView.SortInfo[0].Column == View.m_colDashboardDate)
                    sortField = SortField.DashboardDate;
                else if (View.m_gridReportView.SortInfo[0].Column == View.m_colTechnician)
                    sortField = SortField.Technician;
                else if (View.m_gridReportView.SortInfo[0].Column == View.m_colTickets)
                    sortField = SortField.Ticket;
                else if (View.m_gridReportView.SortInfo[0].Column == View.m_colUser)
                    sortField = SortField.User;
            }

            List<UserAction> userActions = WcfClient.WcfClient.Instance.FindUserActions(
                userItem.Description == string.Empty ? null : (int?) userItem.Value,
                actionItem.Description == string.Empty ? null : (UserActionTypeEnum?) actionItem.Value,
                technicianItem.Description == string.Empty ? null : (int?) technicianItem.Value,
                View.m_txtTicket.Text,
                View.m_dtpDashboardDate.EditValue == null ? null : (DateTime?)View.m_dtpDashboardDate.DateTime.Date,
                View.m_dtpActionRange.EditValue, sortField, isAscending);            
            View.m_gridReport.DataSource = userActions;
        }

        #endregion

        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion   
    }
}
