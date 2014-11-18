using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using Dalworth.Server.Data;

namespace Dalworth.Server.MainForm.AccountingCreditMemo
{
    class CreditMemoModel : IModel
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

        #region QbTerm

        private QbInvoiceTerm m_qbTerm;
        public QbInvoiceTerm QbTerm
        {
            get { return m_qbTerm; }
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

        #region CreditMemoLines

        private BindingList<QbCreditMemoLine> m_creditMemoLines;
        public BindingList<QbCreditMemoLine> CreditMemoLines
        {
            get { return m_creditMemoLines; }
        }

        #endregion

        #endregion

        #region Init

        public void Init()
        {               
            QbCustomer qbCustomer = QbCustomer.FindByPrimaryKey(QbTransaction.QbCreditMemo.QbCustomerId);
            m_project = Project.FindByPrimaryKey(qbCustomer.ProjectId.Value);
            m_customer = Customer.FindByPrimaryKey(m_project.CustomerId.Value);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbCreditMemo.QbClassListId))
                m_qbClass = QbClass.FindByPrimaryKey(m_qbTransaction.QbCreditMemo.QbClassListId);
            m_qbAccount = QbAccount.FindByPrimaryKey(m_qbTransaction.QbCreditMemo.QbAccountListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbCreditMemo.QbTemplateListId))
                m_qbTemplate = QbTemplate.FindByPrimaryKey(m_qbTransaction.QbCreditMemo.QbTemplateListId);
            m_shipAddress = Address.FindByPrimaryKey(m_project.ServiceAddressId.Value);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbCreditMemo.TermsRefListId))
                m_qbTerm = QbInvoiceTerm.FindByPrimaryKey(m_qbTransaction.QbCreditMemo.TermsRefListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbCreditMemo.SalesRepRefListId))
                m_qbSalesRep = QbSalesRep.FindByPrimaryKey(m_qbTransaction.QbCreditMemo.SalesRepRefListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbCreditMemo.ItemSalesTaxRef))
                m_qbItemSalesTax = QbItem.FindByPrimaryKey(m_qbTransaction.QbCreditMemo.ItemSalesTaxRef);
            m_creditMemoLines = new BindingList<QbCreditMemoLine>(
                QbCreditMemoLine.FindByCreditMemoId(m_qbTransaction.QbCreditMemo.TxnID));
        }

        #endregion
    }
    
   
}
