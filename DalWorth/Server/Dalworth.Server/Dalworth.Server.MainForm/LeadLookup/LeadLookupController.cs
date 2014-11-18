using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.LeadLookup
{
    public class LeadLookupController : Controller<LeadLookupModel, LeadLookupView>
    {
        #region SelectedLead

        private LeadWrapper m_selectedLead;
        public LeadWrapper SelectedLead
        {
            get { return m_selectedLead; }
        }

        #endregion

        #region FocusedLead

        public LeadWrapper FocusedLead
        {
            get
            {
                if (View.m_gridViewLeads.FocusedRowHandle >= 0)
                    return (LeadWrapper)View.m_gridViewLeads.GetRow(View.m_gridViewLeads.FocusedRowHandle);
                else
                    return null;
            }
        }

        #endregion

        #region IsCancelled

        private Boolean m_isCancelled;
        public Boolean IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length >= 1 && data[0] != null)
                Model.LeadWrappers = (BindingList<LeadWrapper>)data[0];

            if (data != null && data.Length >= 2 && data[1] != null)
                Model.CustomerAndAddress = (CustomerAndAddress)data[1];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnSelect.Click += OnSelectClick;
            View.m_btnIgnore.Click += OnCancelClick;
            View.m_gridViewLeads.DoubleClick += OnGridDoubleClick;
            View.m_gridViewLeads.KeyDown += OnGridKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridLeads.DataSource = Model.LeadWrappers;
            View.m_gridLeads.Select();

            View.m_lblFullName.Text = Model.CustomerAndAddress.Customer.DisplayName;
            View.m_lblHomePhone.Text = Model.CustomerAndAddress.Customer.Phone1Formatted;
            View.m_lblWorkPhone.Text = Model.CustomerAndAddress.Customer.Phone2Formatted;
            View.m_lblEmail.Text = Model.CustomerAndAddress.Customer.Email;
            View.m_lblAddress.Text = Model.CustomerAndAddress.Address.AddressFirstLine;
            View.m_lblAddress2.Text = Model.CustomerAndAddress.Address.AddressSecondLine;
        }

        #endregion

        #region OnSelectClick

        private void OnSelectClick(object sender, EventArgs e)
        {
            m_selectedLead = FocusedLead;
            View.Close();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Close();
        }

        #endregion

        #region OnGridDoubleClick

        private void OnGridDoubleClick(object sender, EventArgs e)
        {
            m_selectedLead = FocusedLead;
            View.Close();
        }

        #endregion

        #region OnGridKeyDown

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && View.m_gridLeads.Focused)
            {
                m_selectedLead = FocusedLead;
                View.Close();
            }
        }

        #endregion

    }
}
