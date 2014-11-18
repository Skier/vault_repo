using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CustomerEdit
{
    public class CustomerEditController : Controller<CustomerEditModel, CustomerEditView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region Customer

        public CustomerAndAddress Customer
        {
            get { return Model.Customer; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data == null || data.Length == 0 || data[0] == null)
            {
                Address address = new Address();
                address.State = "TX";
                Model.Customer = new CustomerAndAddress(new Customer(), address);
            }
            else
            {
                Model.Customer = (CustomerAndAddress) data[0];
                if (data.Length == 2)
                  Model.QbCustomer = (QbCustomer) data[1];
            }
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_txtLastName.Validating += OnLastNameValidating;

            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;

            if (Model.QbCustomer == null)
                View.m_grpAdsourceSalesRep.Visible = false;
            else
                View.m_ctrlAdSourceSalesRep.Initialize(Model.QbCustomer.QbCustomerTypeListId, Model.QbCustomer.QbSalesRepListId);
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtFirstName.Text = Model.Customer.Customer.FirstName;
            View.m_txtLastName.Text = Model.Customer.Customer.LastName;
            View.m_txtHomePhone.Text = Model.Customer.Customer.Phone1;
            View.m_txtWorkPhone.Text = Model.Customer.Customer.Phone2;
            View.m_txtEmail.Text = Model.Customer.Customer.Email;

            if (!Model.Customer.Customer.CustomerTypeId.HasValue)
                View.m_cmbStatus.SelectedIndex = 1;
            else
                View.m_cmbStatus.SelectedIndex = Model.Customer.Customer.CustomerTypeId.Value - 1;

            View.m_ctrlAddressEdit.Address = Model.Customer.Address;
            
            if (Model.Customer.Customer.ID == 0)
                View.Text = "New Customer";
            
            View.m_txtFirstName.Focus();
        }

        #endregion

        #region Controls Validation

        private void OnLastNameValidating(object sender, CancelEventArgs e)
        {
            if (View.m_txtLastName.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtLastName, "Please enter Last Name");
            else
                View.m_errorProvider.SetError(View.m_txtLastName, string.Empty);
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (Model.QbCustomer != null)
                View.m_ctrlAdSourceSalesRep.ValidateChildren();

            View.ValidateChildren();

            if (View.m_errorProvider.HasErrors || View.m_ctrlAddressEdit.HasErrors)
                return;

            if (Model.QbCustomer != null && View.m_ctrlAdSourceSalesRep.HasErrors)
                return;

            Model.Customer.Customer.FirstName = View.m_txtFirstName.Text;
            Model.Customer.Customer.LastName = View.m_txtLastName.Text;
            Model.Customer.Customer.Phone1 = (string)View.m_txtHomePhone.EditValue;
            Model.Customer.Customer.Phone2 = (string)View.m_txtWorkPhone.EditValue;
            Model.Customer.Customer.Email = (string)View.m_txtEmail.EditValue;
            Model.Customer.Customer.CustomerTypeId = (byte)(View.m_cmbStatus.SelectedIndex + 1);

            Model.Customer.Address.Block = View.m_ctrlAddressEdit.Address.Block;
            Model.Customer.Address.Prefix = View.m_ctrlAddressEdit.Address.Prefix;
            Model.Customer.Address.Street = View.m_ctrlAddressEdit.Address.Street;
            Model.Customer.Address.Suffix = View.m_ctrlAddressEdit.Address.Suffix;
            Model.Customer.Address.Unit = View.m_ctrlAddressEdit.Address.Unit;
            Model.Customer.Address.Address2 = View.m_ctrlAddressEdit.Address.Address2;
            Model.Customer.Address.City = View.m_ctrlAddressEdit.Address.City;
            Model.Customer.Address.State = View.m_ctrlAddressEdit.Address.State;
            Model.Customer.Address.Zip = View.m_ctrlAddressEdit.Address.Zip;
            Model.Customer.Address.MapLetter = View.m_ctrlAddressEdit.Address.MapLetter;
            Model.Customer.Address.MapPage = View.m_ctrlAddressEdit.Address.MapPage;

            if (Model.QbCustomer != null)
            {
                Model.QbCustomer.QbSalesRepListId = View.m_ctrlAdSourceSalesRep.QbSalesRepListId;
                Model.QbCustomer.QbCustomerTypeListId = View.m_ctrlAdSourceSalesRep.QbCustomerTypeListId;
            }

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion
    }
}
