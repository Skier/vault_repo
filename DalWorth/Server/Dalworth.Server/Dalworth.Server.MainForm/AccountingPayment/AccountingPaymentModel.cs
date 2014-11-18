using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.AccountingPayment
{
    class AccountingPaymentModel : IModel
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

        #region QbAccount

        private QbAccount m_qbAccount;
        public QbAccount QbAccount
        {
            get { return m_qbAccount; }
        }

        #endregion

        #region QbPaymentMethod

        private QbPaymentMethod m_qbPaymentMethod;
        public QbPaymentMethod QbPaymentMethod
        {
            get { return m_qbPaymentMethod; }
        }

        #endregion

        #endregion

        #region Init

        public void Init()
        {               
            QbCustomer qbCustomer = QbCustomer.FindByPrimaryKey(QbTransaction.QbPayment.QbCustomerId);
            if (qbCustomer.ProjectId.HasValue)
                m_project = Project.FindByPrimaryKey(qbCustomer.ProjectId.Value);

            m_customer = Customer.FindByPrimaryKey(qbCustomer.CustomerId);            
            m_qbAccount = QbAccount.FindByPrimaryKey(m_qbTransaction.QbPayment.QbAccountListId);
            if (!string.IsNullOrEmpty(m_qbTransaction.QbPayment.QbPaymentMethodListId))
                m_qbPaymentMethod = QbPaymentMethod.FindByPrimaryKey(m_qbTransaction.QbPayment.QbPaymentMethodListId);
        }

        #endregion
    }
    
   
}
