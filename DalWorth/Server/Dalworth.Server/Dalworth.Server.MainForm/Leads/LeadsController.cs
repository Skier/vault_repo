using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.LeadEdit;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.Leads
{
    public class LeadsController : NestedController<LeadsModel, LeadsView>
    {
        private bool m_disableAutoFilter = false;
        private MainFormController m_mainFormController;

        #region FocusedLeadWrapper

        private LeadWrapper FocusedLeadWrapper
        {
            get
            {
                if (View.m_gridViewLeads.FocusedRowHandle < 0)
                    return null;

                return (LeadWrapper)View.m_gridViewLeads.GetRow(
                    View.m_gridViewLeads.FocusedRowHandle);
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            m_mainFormController = (MainFormController)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_gridViewLeads.FocusedRowChanged += OnLeadsFocusedRowChanged;
            View.m_gridViewLeads.DoubleClick += OnLeadsDoubleClick;
            View.m_gridViewLeads.RowUpdated += OnLeadsRowUpdated;

            View.m_txtLeadId.KeyDown += OnFieldsKeyDown;
            View.m_txtPhoneNo.KeyDown += OnFieldsKeyDown;
            View.m_txtCustomer.KeyDown += OnFieldsKeyDown;
            View.m_txtAddress.KeyDown += OnFieldsKeyDown;

            View.m_cmbStatus.SelectedIndexChanged += OnFiltersChanged;
            View.m_dateRange.DateRangeValueChanged += OnFiltersChanged;

            View.m_btnClear.Click += OnClearClick;
            View.m_btnRefresh.Click += OnRefreshClick;

            View.m_gridViewLeads.KeyDown += OnGridVisitsKeyDown;

            View.m_gridLeads.DataSource = Model.LeadWrappers;
            View.m_txtCustomer.Focus();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            //
        }

        #endregion

        #region OpenLead

        protected void OpenLead(LeadWrapper leadWrapper)
        {
            using (LeadEditController controller = Prepare<LeadEditController>(leadWrapper))
            {
                controller.Execute(false);
                if (!controller.IsCancelled)
                    TryToSelectLead(controller.AffectedLead);
            }
        }

        #endregion

        #region OnGridVisitsKeyDown

        private void OnGridVisitsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridLeads.Focused)
            {
                LeadWrapper leadWrapper = FocusedLeadWrapper;
                if (leadWrapper != null)
                    OpenLead(leadWrapper);
            }
        }

        #endregion

        #region TryToSelectLead

        private void TryToSelectLead(Lead lead)
        {
            for (int rowIndex = 0; rowIndex < Model.LeadWrappers.Count; rowIndex++)
            {
                if (Model.LeadWrappers[rowIndex].Lead.ID == lead.ID)
                {
                    int rowHandle = View.m_gridViewLeads.GetRowHandle(rowIndex);
                    if (rowHandle >= 0)
                    {
                        View.m_gridViewLeads.ClearSelection();
                        View.m_gridViewLeads.FocusedRowHandle = rowHandle;
                        View.m_gridViewLeads.SelectRow(rowHandle);
                        return;
                    }
                }
            }

            if (Model.LeadWrappers.Count > 0)
                TryToSelectLead(Model.LeadWrappers[0].Lead);
        }

        #endregion

        #region OnLeadsFocusedRowChanged

        private void OnLeadsFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            LeadWrapper leadWrapper = FocusedLeadWrapper;
            if (leadWrapper != null)
            {
                //refresh details if need
            }
        }

        #endregion

        #region OnLeadsDoubleClick

        private void OnLeadsDoubleClick(object sender, EventArgs e)
        {
            LeadWrapper leadWrapper = FocusedLeadWrapper;
            if (leadWrapper != null)
                OpenLead(leadWrapper);
        }

        #endregion

        #region OnLeadsRowUpdated

        private void OnLeadsRowUpdated(object sender, RowObjectEventArgs e)
        {
            LeadWrapper leadWrapper = (LeadWrapper) e.Row;
            if (leadWrapper != null)
            {
                try
                {
                    Database.Begin();
                    Model.SaveLead(leadWrapper.Lead);
                    Database.Commit();
                }
                catch (Exception)
                {
                    Database.Rollback();
                    throw;
                }
            }
        }

        #endregion

        #region OnFieldsKeyDown

        private void OnFieldsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            else
                OnFiltersChanged(sender, e);
        }

        #endregion

        #region OnFiltersChanged

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            if (m_disableAutoFilter)
                return;

            int? leadId = null;
            if (View.m_txtLeadId.Text != null && View.m_txtLeadId.Text != string.Empty)
            {
                leadId = int.Parse(View.m_txtLeadId.Text);
                ClearFilters();
                View.m_txtLeadId.Text = leadId.ToString();
            }

            LeadWrapper selectedLeadWrapper = FocusedLeadWrapper;
            RefreshData(leadId);
            if (selectedLeadWrapper != null)
                TryToSelectLead(selectedLeadWrapper.Lead);
        }

        #endregion

        #region RefreshData

        public void RefreshData()
        {
            RefreshData(null);
        }

        private void RefreshData(int? exactLeadId)
        {
            string firstName = Utils.ParseFirstName(View.m_txtCustomer.Text);
            string lastName = Utils.ParseLastName(View.m_txtCustomer.Text);

            string city = Utils.ParseCity(View.m_txtAddress.Text);
            string zip = Utils.ParseZip(View.m_txtAddress.Text);
            string block = Utils.ParseBlock(View.m_txtAddress.Text);
            string street = Utils.ParseStreet(View.m_txtAddress.Text);

            string phoneNo = Utils.ExtractDigits(View.m_txtPhoneNo.Text);

            int statusValue = (int)View.m_cmbStatus.EditValue;

            Model.UpdateLeadWrappers(exactLeadId,
                firstName, lastName, phoneNo,
                city, zip, street, block,
                statusValue,
                View.m_dateRange.EditValue);

            View.m_gridLeads.DataSource = Model.LeadWrappers;
            OnLeadsFocusedRowChanged(null, null);
        }

        #endregion

        #region OnRefreshClick

        private void OnRefreshClick(object sender, EventArgs e)
        {
            LeadWrapper selectedLeadWrapper = FocusedLeadWrapper;
            RefreshData(null);
            if (selectedLeadWrapper != null)
                TryToSelectLead(selectedLeadWrapper.Lead);
        }

        #endregion

        #region OnClearClick

        private void ClearFilters()
        {
            m_disableAutoFilter = true;
            View.m_txtLeadId.Text = string.Empty;
            View.m_txtPhoneNo.Text = string.Empty;
            View.m_txtCustomer.Text = string.Empty;
            View.m_txtAddress.Text = string.Empty;
            View.m_cmbStatus.SelectedIndex = 0;
            View.m_dateRange.Clear();
            m_disableAutoFilter = false;
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            ClearFilters();
            RefreshData(null);
        }

        #endregion
    }
}
