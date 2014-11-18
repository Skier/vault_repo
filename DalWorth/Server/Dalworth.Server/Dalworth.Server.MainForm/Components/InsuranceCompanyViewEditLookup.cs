using System;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CustomerLookup;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class InsuranceCompanyViewEditLookup : BaseControl
    {
        public delegate void InsuranceCompanyChangedHandler(InsuranceCompany company, Address address);
        public event InsuranceCompanyChangedHandler InsuranceCompanyChanged;
        
        #region Constructor

        public InsuranceCompanyViewEditLookup()
        {
            InitializeComponent();
            m_btnLookup.Click += OnLookupClick;
            m_btnClear.Click += OnClearClick;
        }

        #endregion

        #region Address

        private Address m_address;
        public Address Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        #endregion  

        #region InsuranceCompany

        private InsuranceCompany m_insuranceCompany;
        public InsuranceCompany InsuranceCompany
        {
            get { return m_insuranceCompany; }
            set
            {
                m_insuranceCompany = value;
                UpdateLabels();
            }
        }

        #endregion

        #region OnClearClick

        private void OnClearClick(object sender, EventArgs e)
        {
            InsuranceCompany = null;
            Address = null;
            if (InsuranceCompanyChanged != null)
                InsuranceCompanyChanged.Invoke(InsuranceCompany, Address);
        }

        #endregion

        #region UpdateLabels

        private void UpdateLabels()
        {
            if (m_insuranceCompany != null)
            {
                m_lblInsuranceCompanyName.Text = m_insuranceCompany.Name;
                m_lblPhone1.Text = m_insuranceCompany.Phone1;
                m_lblPhone2.Text = m_insuranceCompany.Phone2;
                m_lblContactPerson.Text = m_insuranceCompany.ContactPerson;
            }
            else
            {
                m_lblInsuranceCompanyName.Text = string.Empty;
                m_lblPhone1.Text = string.Empty;
                m_lblPhone2.Text = string.Empty;
                m_lblContactPerson.Text = string.Empty;
            }

            m_btnClear.Enabled = m_insuranceCompany != null;
        }

        #endregion

        #region OnLookupClick

        private void OnLookupClick(object sender, EventArgs e)
        {
            InsuranceCompanyAndAddress companyAndAddress = null;

            if (InsuranceCompany != null)
                companyAndAddress = new InsuranceCompanyAndAddress(InsuranceCompany, Address);

            using (InsuranceCompanyLookupController controller = Controller.Prepare<InsuranceCompanyLookupController>(companyAndAddress))
            {
                controller.Execute(false);
                if (controller.IsCompanySelected)
                {
                    InsuranceCompany = controller.Company.InsuranceCompany;
                    Address = controller.Company.Address;
                    if (InsuranceCompanyChanged != null)
                        InsuranceCompanyChanged.Invoke(InsuranceCompany, Address);
                }
            }
        }

        #endregion
    }
}

