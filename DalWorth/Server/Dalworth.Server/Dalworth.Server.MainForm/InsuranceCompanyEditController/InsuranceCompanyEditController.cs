using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CustomerEdit
{
    public class InsuranceCompanyEditController : Controller<InsuranceCompanyEditModel, InsuranceCompanyEditView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region Company

        public InsuranceCompanyAndAddress Company
        {
            get { return Model.Company; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data == null || data.Length == 0 || data[0] == null)
            {
                Address address = new Address();
                address.State = "TX";
                Model.Company = new InsuranceCompanyAndAddress(new InsuranceCompany(), address);
            }                
            else
                Model.Company = (InsuranceCompanyAndAddress)data[0];            
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_txtName.Validating += OnNameValidting;
            View.m_txtPhone1.Validating += OnPhone1Validating;
            View.m_txtPhone2.Validating += OnPhone2Validating;
            View.m_txtContactPerson.Validating += OnContactPersonValidating;

            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtName.Text = Model.Company.InsuranceCompany.Name;
            View.m_txtPhone1.Text = Model.Company.InsuranceCompany.Phone1;
            View.m_txtPhone2.Text = Model.Company.InsuranceCompany.Phone2;
            View.m_txtContactPerson.Text = Model.Company.InsuranceCompany.ContactPerson;
            View.m_ctrlAddressEdit.Address = Model.Company.Address;

            if (Model.Company.InsuranceCompany.ID == 0)
                View.Text = "New Insurance Company";
            
            View.m_txtName.Focus();
        }

        #endregion

        #region Controls Validation

        private void OnNameValidting(object sender, CancelEventArgs e)
        {
            if (View.m_txtName.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtName, "Please enter Name");
            else
                View.m_errorProvider.SetError(View.m_txtName, string.Empty);
        }

        private void OnPhone1Validating(object sender, CancelEventArgs e)
        {
            if (View.m_txtPhone1.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtPhone1, "Please enter Phone 1");
            else
                View.m_errorProvider.SetError(View.m_txtPhone1, string.Empty);
        }

        private void OnPhone2Validating(object sender, CancelEventArgs e)
        {
            if (View.m_txtPhone2.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtPhone2, "Please enter Phone 2");
            else
                View.m_errorProvider.SetError(View.m_txtPhone2, string.Empty);
        }

        private void OnContactPersonValidating(object sender, CancelEventArgs e)
        {
            if (View.m_txtContactPerson.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtContactPerson, "Please enter Contact Person");
            else
                View.m_errorProvider.SetError(View.m_txtContactPerson, string.Empty);
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors || View.m_ctrlAddressEdit.HasErrors)
                return;

            Model.Company.InsuranceCompany.Name = View.m_txtName.Text;
            Model.Company.InsuranceCompany.ContactPerson = View.m_txtContactPerson.Text;
            Model.Company.InsuranceCompany.Phone1 = View.m_txtPhone1.Text;
            Model.Company.InsuranceCompany.Phone2 = View.m_txtPhone2.Text;

            Model.Company.Address.Address1 = View.m_ctrlAddressEdit.Address.Address1;
            Model.Company.Address.Address2 = View.m_ctrlAddressEdit.Address.Address2;
            Model.Company.Address.City = View.m_ctrlAddressEdit.Address.City;
            Model.Company.Address.State = View.m_ctrlAddressEdit.Address.State;
            Model.Company.Address.Zip = View.m_ctrlAddressEdit.Address.Zip;
            Model.Company.Address.Map = View.m_ctrlAddressEdit.Address.Map;
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
