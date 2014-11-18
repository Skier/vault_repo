using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.XtraEditors.Controls;
using Dalworth.Server.Windows;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Accounting
{
    public partial class CustomerRequest :BaseControl
    {
        #region Constructor 

        public CustomerRequest()
        {
            InitializeComponent();
            m_btnSave.Click += OnSaveClick;
            m_btnSkip.Click += OnSkipClick;
            m_btnDontSync.Click += OnDontSyncClick;
            m_gridExistingCustomers.DoubleClick += OnExistingCustomersDoubleClick;
            m_btnRestoreNewCustomer.Click += OnRestoreNewCustomerClick;
            m_txtCustomerName.Validating += OnValidating;
            m_txtCompanyName.Validating += OnValidating;
            m_txtEmail.Validating += OnValidating;
            m_txtFirstName.Validating += OnValidating;
            m_txtLastName.Validating += OnValidating;
            m_txtPhone.Validating += OnValidating;
            m_txtAltPhone.Validating += OnValidating;
            m_cmbSalesReps.SelectedIndexChanged += OnQbSalesRepChanged;
        }

        #endregion 

        #region Properties 

        private QbCustomer m_newQbCustomer;
        private QbCustomer m_selectedQbCustomer;
        private BindingList<QbCustomer> m_similarQbCustomers;
        private ProcessQbCustomerSyncRequest m_processQbSyncRequest;
        private List<QbSalesRep> m_salesReps;
        private QbSyncRequest m_syncRequest;
        private QbSyncActionEnum m_syncAction;

        #endregion 

        #region Initialize 

        internal void Initialize(QbSyncRequest syncRequest, 
            QbCustomer qbCustomer, List<QbCustomer> similarCustomers, 
            ProcessQbCustomerSyncRequest processQbSyncRequest)
        {
            m_syncRequest = syncRequest;
            switch (m_syncRequest.QbSyncActionId)
            {
                case (int)QbSyncActionEnum.CustomerAdd:
                    m_syncAction = QbSyncActionEnum.CustomerAdd;
                    m_lblActionName.Text = "Create New Customer";
                    break;
                case (int)QbSyncActionEnum.CustomerMod:
                    m_syncAction = QbSyncActionEnum.CustomerMod;
                    m_lblActionName.Text = "Modify Customer";
                    break;
            }

            m_newQbCustomer = qbCustomer;
            m_similarQbCustomers = new BindingList<QbCustomer>(similarCustomers);
            m_processQbSyncRequest = processQbSyncRequest;

            m_selectedQbCustomer = m_newQbCustomer;
            LoadDataToUI();
            if (similarCustomers == null || m_similarQbCustomers.Count == 0)
                m_grpExistingCustomers.Visible = false;
            else
            {
                m_grpExistingCustomers.Visible = true;
                m_gridExistingCustomers.DataSource = m_similarQbCustomers;
            }

            m_salesReps = QbSalesRep.FindActive(null);

            foreach (QbSalesRep salesRep in m_salesReps)
            {
                m_cmbSalesReps.Properties.Items.Add(
                    new ImageComboBoxItem(salesRep.DisplayName, salesRep.ListId));
            }

            if (!string.IsNullOrEmpty(m_selectedQbCustomer.QbSalesRepListId))
                m_cmbSalesReps.EditValue = m_selectedQbCustomer.QbSalesRepListId;
            
            m_selectedQbCustomer = m_newQbCustomer;
        }

        #endregion 

        #region LoadDataToUI

        private void LoadDataToUI()
        {
            m_txtCustomerName.Text = m_selectedQbCustomer.Name;
            m_txtCompanyName.Text = m_selectedQbCustomer.CompanyName;
            m_txtEmail.Text = m_selectedQbCustomer.Email;
            m_txtFirstName.Text = m_selectedQbCustomer.FirstName;
            m_txtLastName.Text = m_selectedQbCustomer.LastName;
            m_txtPhone.Text = Utils.FormatPhone(m_selectedQbCustomer.Phone1);
            m_txtAltPhone.Text = Utils.FormatPhone(m_selectedQbCustomer.Phone2);
            m_txtShippingAddress1.Text = m_selectedQbCustomer.ShippingAddressAddr1;
            m_txtShippingAddress2.Text = m_selectedQbCustomer.ShippingAddressAddr2;
            m_txtShippingCity.Text = m_selectedQbCustomer.ShippingAddressCity;
            m_txtShippingState.Text = m_selectedQbCustomer.ShippingAddressState;
            m_txtShippingZip.Text = m_selectedQbCustomer.ShippingAddressPostalCode;
            m_txtBillingAddress1.Text = m_selectedQbCustomer.BillingAddressAddr1;
            m_txtBillingAddress2.Text = m_selectedQbCustomer.BillingAddressAddr2;
            m_txtBillingCity.Text = m_selectedQbCustomer.BillingAddressCity;
            m_txtBillingState.Text = m_selectedQbCustomer.BillingAddressState;
            m_txtBillingZip.Text = m_selectedQbCustomer.BillingAddressPostalCode;
        }

        #endregion

        #region LoadDataFromUI

        private void LoadDataFromUI()
        {
            m_selectedQbCustomer.Name = m_txtCustomerName.Text;
            m_selectedQbCustomer.CompanyName = m_txtCompanyName.Text;
            m_selectedQbCustomer.Email = m_txtEmail.Text;
            m_selectedQbCustomer.FirstName = m_txtFirstName.Text;
            m_selectedQbCustomer.LastName = m_txtLastName.Text;
            m_selectedQbCustomer.Phone1 = m_txtPhone.Text;
            m_selectedQbCustomer.Phone2 = m_txtAltPhone.Text;
            m_selectedQbCustomer.ShippingAddressAddr1 = m_txtShippingAddress1.Text;
            m_selectedQbCustomer.ShippingAddressAddr2 = m_txtShippingAddress2.Text;
            m_selectedQbCustomer.ShippingAddressCity = m_txtShippingCity.Text;
            m_selectedQbCustomer.ShippingAddressState = m_txtShippingState.Text;
            m_selectedQbCustomer.ShippingAddressPostalCode = m_txtShippingZip.Text;
            m_selectedQbCustomer.BillingAddressAddr1 = m_txtBillingAddress1.Text;
            m_selectedQbCustomer.BillingAddressAddr2 = m_txtBillingAddress2.Text;
            m_selectedQbCustomer.BillingAddressCity = m_txtBillingCity.Text;
            m_selectedQbCustomer.BillingAddressState = m_txtBillingState.Text;
            m_selectedQbCustomer.BillingAddressPostalCode = m_txtBillingZip.Text;
        }

        #endregion 

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs e)
        {
            m_dxErrorProvider.ClearErrors();
            ValidateChildren();
            if (m_dxErrorProvider.HasErrors)
                return;

            LoadDataFromUI();

            if (m_processQbSyncRequest != null)
                m_processQbSyncRequest(m_syncAction, m_selectedQbCustomer);  
        }

        #endregion

        #region OnDontSyncClick

        private void OnDontSyncClick(object sender, EventArgs e)
        {
            m_dxErrorProvider.ClearErrors();

            LoadDataFromUI();

            if (m_processQbSyncRequest != null)
                m_processQbSyncRequest(QbSyncActionEnum.DontSyncCustomer, m_selectedQbCustomer);
        }

        #endregion

        #region OnSkipClick

        private void OnSkipClick(object sender, EventArgs e)
        {
            m_dxErrorProvider.ClearErrors();

            LoadDataFromUI();

            if (m_processQbSyncRequest != null)
                m_processQbSyncRequest(QbSyncActionEnum.SkipCustomer, m_selectedQbCustomer);  
        }

        #endregion 

        #region OnExistingCustomersDoubleClick

        private void OnExistingCustomersDoubleClick(object sender, System.EventArgs e)
        {
            m_dxErrorProvider.ClearErrors();
            m_selectedQbCustomer =
               (QbCustomer) m_gridExistingCustomersView.GetRow(m_gridExistingCustomersView.FocusedRowHandle);
            m_selectedQbCustomer.InitializeNonQbFields(m_newQbCustomer);
            LoadDataToUI();
            OnValidating(null, null);
        }

        #endregion 

        #region OnRestoreNewCustomerClick

        private void OnRestoreNewCustomerClick(object sender, System.EventArgs e)
        {
            m_dxErrorProvider.ClearErrors();
            m_selectedQbCustomer = m_newQbCustomer;
            LoadDataToUI();
            ValidateChildren();
        }

        #endregion 

        #region OnValidating

        private void OnValidating(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_txtCustomerName.Text))
            {
                m_dxErrorProvider.SetError(m_txtCustomerName, "Required");
                
            }
            else
            {
                ValidateOptinalField(m_txtCustomerName, 41);
                foreach (QbCustomer similarCustomer in m_similarQbCustomers)
                {
                    if (similarCustomer.FullName.ToUpper() == m_txtCustomerName.Text.ToUpper() 
                        && m_selectedQbCustomer.ListId != similarCustomer.ListId)
                    {
                        m_dxErrorProvider.SetError(m_txtCustomerName, "Already Exists, Double Click on Existing customer or change Name");
                        break;
                    }
                }
                
            }

            ValidateOptinalField(m_txtCompanyName, 41);
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

        #region OnQbSalesRepChanged

        private void OnQbSalesRepChanged(object sender, EventArgs e)
        {
            m_dxErrorProvider.ClearErrors();
            ValidateChildren();
        }

        #endregion 

        #region ValidateOptinalField

        private void ValidateOptinalField (System.Windows.Forms.Control control, int maxSize)
        {
            if (!string.IsNullOrEmpty(control.Text) && control.Text.Length > maxSize)
            {
                m_dxErrorProvider.SetError(control, "Too Long");           
            }
        }

        #endregion 
    }
}
