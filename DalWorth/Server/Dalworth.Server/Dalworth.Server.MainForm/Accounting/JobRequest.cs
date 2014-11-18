using System;
using System.ComponentModel;
using System.Collections.Generic;

using Dalworth.Server.Windows;
using Dalworth.Server.Domain;
using Dalworth.Server.Data;

namespace Dalworth.Server.MainForm.Accounting
{
    public partial class JobRequest :BaseControl
    {
        #region Private 

        private QbCustomer m_qbCustomer;
        private QbCustomer m_qbProject;
        private List<QbCustomer> m_existingQbProjects;
        private ProcessQbCustomerSyncRequest m_processQbSyncRequest;
        private QbSyncRequest m_syncRequest;
        private QbSyncActionEnum m_qbSyncAction;

        #endregion 

        #region Constructor

        public JobRequest()
        {
            InitializeComponent();
            m_txtJobNumber.Validating += OnValidating;
            m_txtCompanyName.Validating += OnValidating;
            m_txtFirstName.Validating += OnValidating;
            m_txtLastName.Validating += OnValidating;
            m_txtPhone.Validating += OnValidating;
            m_txtAltPhone.Validating += OnValidating;
            m_txtEmail.Validating += OnValidating;

            m_txtShippingAddress1.Validating += OnValidating;
            m_txtShippingAddress2.Validating += OnValidating;
            m_txtShippingCity.Validating += OnValidating;
            m_txtShippingState.Validating += OnValidating;
            m_txtShippingZip.Validating += OnValidating;

            m_txtBillingAddress1.Validating += OnValidating;
            m_txtBillingAddress2.Validating += OnValidating;
            m_txtBillingCity.Validating += OnValidating;
            m_txtBillingState.Validating += OnValidating;
            m_txtBillingZip.Validating += OnValidating;
            Validating += OnValidating;
            m_btnSave.Click += OnSaveClick;
            m_btnSkip.Click += OnSkipClick;
            m_btnDontSync.Click += OnDontSyncClick;
        }

        #endregion 

        #region Initialize

        internal void Initialize(QbSyncRequest  syncRequest, QbCustomer qbProject, 
            ProcessQbCustomerSyncRequest processQbSyncRequest)
        {
            m_syncRequest = syncRequest;
            switch (m_syncRequest.QbSyncActionId)
            {
                case (int)QbSyncActionEnum.JobAdd:
                    m_lblActionName.Text = "Create New Job";
                    m_qbSyncAction = QbSyncActionEnum.JobAdd;
                    break;
                case (int)QbSyncActionEnum.JobMod:
                    m_lblActionName.Text = "Modify Job";
                    m_qbSyncAction = QbSyncActionEnum.JobMod;
                    break;
            }

            m_syncRequest = syncRequest;
            m_qbProject = qbProject;
            m_processQbSyncRequest = processQbSyncRequest;

            m_qbCustomer = QbCustomer.FindParent(qbProject.CustomerId,null);

            try
            {
                m_qbProject.ProjectInsurance = ProjectInsurance.FindByPrimaryKey(m_qbProject.ProjectId.Value);
            }
            catch (DataNotFoundException) { }
           
            m_ctrlAdSourceSalesRep.Initialize(m_qbProject.QbCustomerTypeListId, m_qbProject.QbSalesRepListId);
            LoadDataToUI();
        }

        #endregion 

        #region LoadDataToUI

        private void LoadDataToUI()
        {
            m_txtJobNumber.Text = m_qbProject.Name;
            m_txtCompanyName.Text = m_qbProject.CompanyName;
            m_txtFirstName.Text = m_qbProject.FirstName;
            m_txtLastName.Text = m_qbProject.LastName;
            m_txtPhone.Text = Utils.FormatPhone(m_qbProject.Phone1);
            m_txtAltPhone.Text = Utils.FormatPhone(m_qbProject.Phone2);
            m_txtEmail.Text = m_qbProject.Email;

            m_txtShippingAddress1.Text = m_qbProject.ShippingAddressAddr1;
            m_txtShippingAddress2.Text = m_qbProject.ShippingAddressAddr2;
            m_txtShippingCity.Text = m_qbProject.ShippingAddressCity;
            m_txtShippingState.Text = m_qbProject.ShippingAddressState;
            m_txtShippingZip.Text = m_qbProject.ShippingAddressPostalCode;

            m_txtBillingAddress1.Text = m_qbProject.BillingAddressAddr1;
            m_txtBillingAddress2.Text = m_qbProject.BillingAddressAddr2;
            m_txtBillingCity.Text = m_qbProject.BillingAddressCity;
            m_txtBillingState.Text = m_qbProject.BillingAddressState;
            m_txtBillingZip.Text = m_qbProject.BillingAddressPostalCode;
           
            m_txtCustomerAddress1.Text = m_qbCustomer.BillingAddressAddr1;
            m_txtCustomerAddress2.Text = m_qbCustomer.BillingAddressAddr2;
            m_txtCustomerCity.Text = m_qbCustomer.BillingAddressCity;
            m_txtCustomerState.Text = m_qbCustomer.BillingAddressState;
            m_txtCustomerZip.Text = m_qbCustomer.BillingAddressPostalCode;

            m_txtCustomerCompanyName.Text = m_qbCustomer.CompanyName;
            m_txtCustomerEmail.Text = m_qbCustomer.Email;
            m_txtCustomerFirstName.Text = m_qbCustomer.FirstName;
            m_txtCustomerLastName.Text = m_qbCustomer.LastName;
            m_txtCustomerName.Text = m_qbCustomer.FullName;
            m_txtCustomerPhone.Text = Utils.FormatPhone(m_qbCustomer.Phone1);

            if (!string.IsNullOrEmpty(m_qbCustomer.QbSalesRepListId))
            {
                QbSalesRep salesRep = QbSalesRep.FindByPrimaryKey(m_qbCustomer.QbSalesRepListId);
                m_txtSalesRep.Text = salesRep.DisplayName;
            }

            m_existingQbProjects = QbCustomer.FindProjects(m_qbProject.CustomerId, null);
            m_existingQbProjects.RemoveAll(delegate(QbCustomer qbCustomer) { return string.IsNullOrEmpty(qbCustomer.ListId); });
            m_gridExistingCustomers.DataSource = new BindingList<QbCustomer>(m_existingQbProjects);
            m_ctlProjectInsuranceEdit.ProjectInsurance = m_qbProject.ProjectInsurance;
        }

        #endregion

        #region LoadDataFromUI

        private void LoadDataFromUI()
        {
            m_qbProject.Name = m_txtJobNumber.Text;
            m_qbProject.CompanyName = m_txtCompanyName.Text;
            m_qbProject.Email = m_txtEmail.Text;
            m_qbProject.FirstName = m_txtFirstName.Text;
            m_qbProject.LastName = m_txtLastName.Text;
            m_qbProject.Phone1 = m_txtPhone.Text;
            m_qbProject.Phone2 = m_txtAltPhone.Text;
            m_qbProject.ShippingAddressAddr1 = m_txtShippingAddress1.Text;
            m_qbProject.ShippingAddressAddr2 = m_txtShippingAddress2.Text;
            m_qbProject.ShippingAddressCity = m_txtShippingCity.Text;
            m_qbProject.ShippingAddressState = m_txtShippingState.Text;
            m_qbProject.ShippingAddressPostalCode = m_txtShippingZip.Text;
            m_qbProject.BillingAddressAddr1 = m_txtBillingAddress1.Text;
            m_qbProject.BillingAddressAddr2 = m_txtBillingAddress2.Text;
            m_qbProject.BillingAddressCity = m_txtBillingCity.Text;
            m_qbProject.BillingAddressState = m_txtBillingState.Text;
            m_qbProject.BillingAddressPostalCode = m_txtBillingZip.Text;

            m_qbProject.QbCustomerTypeListId = m_ctrlAdSourceSalesRep.QbCustomerTypeListId;
            m_qbProject.QbSalesRepListId = m_ctrlAdSourceSalesRep.QbSalesRepListId;
            ProjectInsurance insurance = m_ctlProjectInsuranceEdit.ProjectInsurance;

            if (insurance.IsFilled())
                m_qbProject.ProjectInsurance = insurance;

        }

        #endregion

        #region HasErrors

        public bool HasErrors
        {
            get
            {
                return m_errorProvider.HasErrors || m_ctrlAdSourceSalesRep.HasErrors;
            }
        }

        #endregion

        #region OnValidating

        private void OnValidating(object sender, EventArgs e)
        {
        m_ctrlAdSourceSalesRep.ValidateChildren();

            if (string.IsNullOrEmpty(m_txtJobNumber.Text))
            {
                m_errorProvider.SetError(m_txtCustomerName, "Required");

            }
            else
            {
                ValidateOptinalField(m_txtJobNumber, 41);
                foreach (QbCustomer existingQbProject in m_existingQbProjects)
                {
                    if (existingQbProject.Name.ToUpper() == m_txtJobNumber.Text.ToUpper())
                    {
                        m_errorProvider.SetError(m_txtJobNumber, "Already Exists");
                        break;
                    }
                }

            }

            ValidateOptinalField(m_txtJobNumber, 41);
            ValidateOptinalField(m_txtLastName, 25);
            ValidateOptinalField(m_txtFirstName, 25);
            ValidateOptinalField(m_txtPhone, 21);
            ValidateOptinalField(m_txtAltPhone, 21);
            ValidateOptinalField(m_txtEmail, 1023);

            ValidateOptinalField(m_txtBillingAddress1, 41);
            ValidateOptinalField(m_txtBillingAddress2, 41);
            ValidateOptinalField(m_txtBillingCity, 31);
            ValidateOptinalField(m_txtBillingState, 21);
            ValidateOptinalField(m_txtBillingZip, 13);

            ValidateOptinalField(m_txtShippingAddress1, 41);
            ValidateOptinalField(m_txtShippingAddress2, 41);
            ValidateOptinalField(m_txtShippingCity, 31);
            ValidateOptinalField(m_txtShippingState, 21);
            ValidateOptinalField(m_txtShippingZip, 13);
        }

        #endregion

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs e)
        {
           m_ctrlAdSourceSalesRep.ClearError();
           m_errorProvider.ClearErrors();
           m_ctrlAdSourceSalesRep.ValidateChildren();
           ValidateChildren();
           if (m_ctrlAdSourceSalesRep.HasErrors || m_errorProvider.HasErrors)
               return;

            LoadDataFromUI();

            m_processQbSyncRequest(m_qbSyncAction, m_qbProject);
        }

        #endregion 

        #region OnSkipClick
        
        void OnSkipClick(object sender, EventArgs e)
        {
            m_processQbSyncRequest(QbSyncActionEnum.SkipJob, m_qbProject);
        }

        #endregion 

        #region OnSkipClick

        void OnDontSyncClick(object sender, EventArgs e)
        {
            m_processQbSyncRequest(QbSyncActionEnum.DontSyncJob, m_qbProject);
        }

        #endregion 

        #region ValidateOptinalField

        private void ValidateOptinalField(System.Windows.Forms.Control control, int maxSize)
        {
            if (!string.IsNullOrEmpty(control.Text) && control.Text.Length > maxSize)
            {
                m_errorProvider.SetError(control, "Too Long");           
            }
        }

        #endregion 
        }
}
