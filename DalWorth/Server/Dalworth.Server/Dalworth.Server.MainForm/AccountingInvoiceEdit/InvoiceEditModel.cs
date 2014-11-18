using System.ComponentModel;

using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.AccountingInvoiceEdit
{
    class InvoiceEditModel : IModel
    {
        #region Properties 

        #region QbTransaction

        private QbTransaction m_qbTransaction;
        public QbTransaction QbTransaction
        {
            get { return m_qbTransaction; }
            set { m_qbTransaction = value; }
        }

        #endregion 

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion 

        #region Project

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
        }

        #endregion

        #region QbClass

        private QbClass m_qbClass;
        public QbClass QbClass
        {
            get { return m_qbClass; }
        }

        #endregion

        #region QbAccount

        private QbAccount m_qbAccount;
        public QbAccount QbAccount
        {
            get { return m_qbAccount; }
        }

        #endregion

        #region QbTemplate

        private QbTemplate m_qbTemplate;
        public QbTemplate QbTemplate
        {
            get { return m_qbTemplate; }
        }

        #endregion

        #region ShipAddress

        private Address m_shipAddress;
        public Address ShipAddress
        {
            get { return m_shipAddress; }
        }

        #endregion

        #region BillAddress

        private Address m_billAddress;
        public Address BillAddress
        {
            get { return m_billAddress; }
        }

        #endregion

        #region QbInvoiceTerm

        private QbInvoiceTerm m_qbInvoiceTerm;
        public QbInvoiceTerm QbInvoiceTerm
        {
            get { return m_qbInvoiceTerm; }
        }

        #endregion

        #region QbSalesRep

        private QbSalesRep m_qbSalesRep;
        public QbSalesRep QbSalesRep
        {
            get { return m_qbSalesRep; }
        }

        #endregion

        #region QbItemSalesTax

        private QbItem m_qbItemSalesTax;
        public QbItem QbItemSalesTax
        {
            get { return m_qbItemSalesTax; }
        }

        #endregion

        #region InvoiceLines

        private BindingList<QbInvoiceLine> m_invoiceLines;
        public BindingList<QbInvoiceLine> InvoiceLines
        {
            get { return m_invoiceLines; }
        }

        #endregion

        #endregion

        #region Init

        public void Init()
        {               
            QbCustomer qbCustomer = QbCustomer.FindByPrimaryKey(QbTransaction.QbInvoice.QbCustomerId);
            m_project = Project.FindByPrimaryKey(qbCustomer.ProjectId.Value);
            m_customer = Customer.FindByPrimaryKey(m_project.CustomerId.Value);            
            if (!string.IsNullOrEmpty(m_qbTransaction.QbInvoice.QbClassListId))
                m_qbClass = QbClass.FindByPrimaryKey(m_qbTransaction.QbInvoice.QbClassListId);
            m_qbAccount = QbAccount.FindByPrimaryKey(m_qbTransaction.QbInvoice.QbAccountListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbInvoice.QbTemplateListId))
                m_qbTemplate = QbTemplate.FindByPrimaryKey(m_qbTransaction.QbInvoice.QbTemplateListId);
            m_shipAddress = Address.FindByPrimaryKey(m_project.ServiceAddressId.Value);
            m_billAddress = Address.FindByPrimaryKey(m_customer.AddressId.Value);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbInvoice.QbInvoiceTermListId))
                m_qbInvoiceTerm = QbInvoiceTerm.FindByPrimaryKey(m_qbTransaction.QbInvoice.QbInvoiceTermListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbInvoice.QbSalesRepRefListId))
                m_qbSalesRep = QbSalesRep.FindByPrimaryKey(m_qbTransaction.QbInvoice.QbSalesRepRefListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbInvoice.ItemSalesTaxRef))
                m_qbItemSalesTax = QbItem.FindByPrimaryKey(m_qbTransaction.QbInvoice.ItemSalesTaxRef);
            m_invoiceLines = new BindingList<QbInvoiceLine>(
                QbInvoiceLine.FindByInvoiceId(m_qbTransaction.QbInvoice.ID, null));
        }

        #endregion
    }
    
   
}
