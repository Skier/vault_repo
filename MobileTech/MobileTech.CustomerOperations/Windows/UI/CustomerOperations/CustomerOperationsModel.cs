using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.ServiceLayer;
using MobileTech.Data;
using MobileTech.Windows.UI.SelectItem;
using MobileTech.Windows.UI.Odometer;

namespace MobileTech.Windows.UI.CustomerOperations
{
	public abstract class CustomerOperationsModel
    {
        #region Fields

        #region CustomerVisit

        private CustomerVisit m_customerVisit;

        protected CustomerVisit CustomerVisit
        {
            get { return m_customerVisit; }
            set { m_customerVisit = value; }
        }

        #endregion

        #region Transactions

        private Dictionary<CustomerTransactionTypeEnum, CustomerTransaction> m_transactions;

        public Dictionary<CustomerTransactionTypeEnum, CustomerTransaction> Transactions
        {
            get { return m_transactions; }
            set { m_transactions = value; }
        }

        #endregion

        protected CustomerOperationsCommonData m_commonData;

        public CustomerOperationsCommonData CommonData
        {
            get
            {
                return m_commonData;
            }
        }


        #endregion

        #region Init
        /// <summary>
        ///  If exeption will be rised then Rollback must be called
        /// </summary>
        /// <param name="routeScheduleQueue"></param>
        /// <exception cref="MobileTech.Data.DataNotFoundException"></exception>
        /// <exception cref="System.Exception"></exception>
        public virtual void Init(CustomerOperationsCommonData commonData)
        {
            m_commonData = commonData;

            m_transactions = new Dictionary<CustomerTransactionTypeEnum, CustomerTransaction>();

            if (Database.UnderTransaction)
            {
                m_customerVisit = CustomerVisit.FindCurrent();

                CustomerTransaction salesTransaction = CustomerTransaction.Find(CustomerTransactionTypeEnum.Sales, 
                    m_customerVisit);

                if(salesTransaction != null)
                    m_transactions.Add(CustomerTransactionTypeEnum.Sales, salesTransaction);
            }
            else
            {
                Database.Begin();

                m_customerVisit = CustomerVisit.Prepare();
                m_customerVisit.CustomerId = m_commonData.RouteScheduleQueue.Customer.CustomerId;

                Counter.Assign(m_customerVisit);

                CustomerVisit.Insert(m_customerVisit);
            }


        }

        #endregion

        #region Rollback

        public void Rollback()
		{
			Database.Rollback();
        }

        #endregion

        #region IsTransactionExists

        public bool IsTransactionExists(CustomerTransactionTypeEnum type)
		{
            return m_transactions.ContainsKey(type);
		}

		#endregion

        #region AddTransaction

        public void AddTransaction(CustomerTransactionTypeEnum type)
        {
            if (IsTransactionExists(type))
                throw new MobileTechException("Transaction already exist");

            CustomerTransaction transaction = CustomerTransaction.Prepare(Route.Current,
                m_customerVisit, type);

            Counter.Assign(transaction.BusinessTransaction);


            BusinessTransaction.Insert(transaction.BusinessTransaction);

            transaction.BusinessTransactionId = transaction.BusinessTransaction.BusinessTransactionId;

            CustomerTransaction.Insert(transaction);

            m_transactions.Add(type, transaction);
        }

        #endregion


        #region RemoveTransaction
        public void RemoveTransaction(CustomerTransactionTypeEnum type)
        {
            CustomerTransaction transaction = 
                m_transactions[CustomerTransactionTypeEnum.Sales];

            CustomerTransaction.Delete(transaction);
            BusinessTransaction.Delete(transaction.BusinessTransaction);

            m_transactions.Remove(CustomerTransactionTypeEnum.Sales);
        }
        #endregion
    }
}
