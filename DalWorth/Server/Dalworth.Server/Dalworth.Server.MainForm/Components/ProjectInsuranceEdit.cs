using System;

using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class ProjectInsuranceEdit :BaseControl
    {
        public ProjectInsuranceEdit()
        {
            InitializeComponent();
        }

        #region ProjectInsurance

        private ProjectInsurance m_projectInsurance;
        public ProjectInsurance ProjectInsurance
        {
            get 
            {
                LoadDataFromUI();
                return m_projectInsurance; 
            }
            set 
            { 
                m_projectInsurance = value;
                LoadDataToUI();
            }
        }

        #endregion

        #region Private 

        private void LoadDataFromUI()
        {
            if (m_projectInsurance == null)
                m_projectInsurance = new ProjectInsurance();

            m_projectInsurance.Company = m_txtCompany.Text;
            m_projectInsurance.Address1 = m_txtAddress1.Text;
            m_projectInsurance.Address2 = m_txtAddress2.Text;
            m_projectInsurance.Contact = m_txtContact.Text;
            m_projectInsurance.Phone = m_txtPhone.Text;
            m_projectInsurance.Fax = m_txtFax.Text;
            m_projectInsurance.ClaimNumber = m_txtClaimNumber.Text;

            string amount = m_txtDeductibleAmount.Text;
            amount = amount.Replace('$', ' ').Trim();
            m_projectInsurance.DeductibleAmount = !string.IsNullOrEmpty(amount) ? Convert.ToDecimal(amount) : 0;
        }

        private void LoadDataToUI()
        {
            if (m_projectInsurance != null)
            {
                m_txtCompany.Text = m_projectInsurance.Company;
                m_txtAddress1.Text = m_projectInsurance.Address1;
                m_txtAddress2.Text = m_projectInsurance.Address2;
                m_txtContact.Text = m_projectInsurance.Contact;
                m_txtPhone.Text = m_projectInsurance.Phone;
                m_txtFax.Text = m_projectInsurance.Fax;
                m_txtClaimNumber.Text = m_projectInsurance.ClaimNumber;
                m_txtDeductibleAmount.EditValue = m_projectInsurance.DeductibleAmount;
            }
            else
            {
                m_txtCompany.Text = string.Empty;
                m_txtAddress1.Text = string.Empty;
                m_txtAddress2.Text = string.Empty;
                m_txtContact.Text = string.Empty;
                m_txtPhone.Text = string.Empty;
                m_txtFax.Text = string.Empty;
                m_txtClaimNumber.Text = string.Empty;
                m_txtDeductibleAmount.EditValue = string.Empty;
            }
        }

        #endregion 
    }
}
