using System;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using Dalworth.Server.Windows;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Accounting
{
    public partial class InvoiceRequest : BaseControl
    {
        #region Constructor

        public InvoiceRequest()
        {
            InitializeComponent();
            m_btnSave.Click += OnSaveClick;
            m_btnSkip.Click += OnSkipClick;
            m_btnDontSync.Click += OnDontSyncClick;
            m_cmbSalesReps.Validating += OnValidating;
            m_txtInvoiceNumber.Validating += OnValidating;
        }

        #endregion 

        #region Properties

        private ProcessQbCustomerSyncRequest m_processQbSyncRequest;
        private QbInvoice m_qbInvoice;
        private List<QbInvoice> m_existingCustomerInvoices;

        #endregion 

        #region Initialize

        internal void Initialize(QbInvoice qbInvoice, ProcessQbCustomerSyncRequest processQbSyncRequest)
        {
            m_qbInvoice = qbInvoice;
            m_processQbSyncRequest = processQbSyncRequest;

            QbCustomer qbProject = QbCustomer.FindByPrimaryKey(m_qbInvoice.QbCustomerId, null);
            m_txtProjectName.Text = qbProject.FullName;
            m_txtInvoiceNumber.Text = m_qbInvoice.RefNumber;

            List<QbSalesRep> salesReps = QbSalesRep.FindActive(null);
            foreach (QbSalesRep salesRep in salesReps)
            {
                m_cmbSalesReps.Properties.Items.Add(
                    new ImageComboBoxItem(salesRep.DisplayName, salesRep.ListId));
            }

            if (m_qbInvoice.QbSalesRepRefListId != null)
                m_cmbSalesReps.EditValue = m_qbInvoice.QbSalesRepRefListId;


            if (m_qbInvoice.TxnDate.HasValue)
                m_dtInvoiceDate.DateTime = m_qbInvoice.TxnDate.Value;

            m_ctrlBillingAddress.HeaderText = "Billing Address";
            m_ctrlBillingAddress.Address1 = m_qbInvoice.BillingAddressAddr1;
            m_ctrlBillingAddress.Address2 = m_qbInvoice.BillingAddressAddr2;
            m_ctrlBillingAddress.City = m_qbInvoice.BillingAddressCity;
            m_ctrlBillingAddress.State = m_qbInvoice.BillingAddresState;
            m_ctrlBillingAddress.Zip = m_qbInvoice.BillingAddresPostalCode;
            m_ctrlBillingAddress.Initialize();

            m_ctrlShippingAddress.HeaderText = "Shipping Address";
            m_ctrlShippingAddress.Address1 = m_qbInvoice.ShipAddressAddr1;
            m_ctrlShippingAddress.Address2 = m_qbInvoice.ShipAddressAddr2;
            m_ctrlShippingAddress.City = m_qbInvoice.BillingAddressCity;
            m_ctrlShippingAddress.State = m_qbInvoice.ShipAddressState;
            m_ctrlShippingAddress.Zip = m_qbInvoice.ShipAddressPostalCode;
            m_ctrlShippingAddress.Initialize();

            List<QbInvoiceLine> invoiceLines = QbInvoiceLine.FindByInvoiceId(m_qbInvoice.ID, null);
            m_gridExistingCustomers.DataSource = new BindingList<QbInvoiceLine>(invoiceLines);

            m_txtTax.Text = m_qbInvoice.TaxAmount.ToString("c");
            m_txtTotal.Text = m_qbInvoice.TotalAmount.ToString("c");

            QbCustomer qbCustomer = QbCustomer.FindParent(qbProject.CustomerId, null);
            m_existingCustomerInvoices = QbInvoice.Find(qbCustomer, null);
        }

        #endregion 

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs e)
        {
            m_errorProvider.ClearErrors();
            m_ctrlBillingAddress.ClearErrors();
            m_ctrlShippingAddress.ClearErrors();

            ValidateChildren();

            if (m_ctrlShippingAddress.HasErrors || m_ctrlBillingAddress.HasErrors || m_errorProvider.HasErrors)
                return;

            LoadDataFromUI();

            if (m_processQbSyncRequest != null)
                m_processQbSyncRequest(QbSyncActionEnum.InvoiceAdd, m_qbInvoice);
        }

        #endregion 

        #region OnSkipClick

        private void OnSkipClick(object sender, EventArgs e)
        {
            if (m_processQbSyncRequest != null)
                m_processQbSyncRequest(QbSyncActionEnum.SkipInvoice, m_qbInvoice);
        }

        #endregion 

        #region OnSkipClick

        private void OnDontSyncClick(object sender, EventArgs e)
        {
            if (m_processQbSyncRequest != null)
                m_processQbSyncRequest(QbSyncActionEnum.DontSyncInvoice, m_qbInvoice);
        }

        #endregion 

        #region LoadDataFromUI

        private void LoadDataFromUI()
        {
            m_ctrlBillingAddress.LoadDataFromUI();
            m_qbInvoice.BillingAddressAddr1 = m_ctrlBillingAddress.Address1;
            m_qbInvoice.BillingAddressAddr2 = m_ctrlBillingAddress.Address2;
            m_qbInvoice.BillingAddressCity = m_ctrlBillingAddress.City;
            m_qbInvoice.BillingAddresState = m_ctrlBillingAddress.State;
            m_qbInvoice.BillingAddresPostalCode = m_ctrlBillingAddress.Zip;

            m_ctrlShippingAddress.LoadDataFromUI();
            m_qbInvoice.ShipAddressAddr1 = m_ctrlShippingAddress.Address1;
            m_qbInvoice.ShipAddressAddr2 = m_ctrlShippingAddress.Address2;
            m_qbInvoice.ShipAddressState = m_ctrlShippingAddress.State;
            m_qbInvoice.ShipAddressCity = m_ctrlShippingAddress.City;
            m_qbInvoice.ShipAddressPostalCode = m_ctrlShippingAddress.Zip;

            m_qbInvoice.QbSalesRepRefListId = (string)m_cmbSalesReps.EditValue;
        }

        #endregion 

        #region OnValidating 

        private void OnValidating(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((string)m_cmbSalesReps.EditValue))
            {
                m_errorProvider.SetError(m_cmbSalesReps, "Required");
            }

            if (string.IsNullOrEmpty(m_txtInvoiceNumber.Text))
            {
                m_errorProvider.SetError(m_txtInvoiceNumber, "Required");
            }

            int idx = m_existingCustomerInvoices.FindIndex(delegate(QbInvoice invoice)
                                                               {
                                                                   return invoice.RefNumber.ToUpper() ==
                                                                          m_txtInvoiceNumber.Text.Trim().ToUpper() &&
                                                                          !string.IsNullOrEmpty(invoice.TxnID);
                                                               });
            if (idx > 0)
                m_errorProvider.SetError(m_txtInvoiceNumber, "Already Exists");
        }
    }

        #endregion 
}
