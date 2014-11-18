using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Domain;
using MobileTech.Data;

namespace MobileTech.Windows.UI.CustomerOperations.Commit
{
    internal class CustomerOperationsCommitModel:CustomerOperationsModel
    {

        #region Commit

        public void Commit()
        {

            if (Transactions.ContainsKey(CustomerTransactionTypeEnum.Sales))
            {
                List<CustomerTransactionDetail> customerTransactionDetailList
                    = CustomerTransactionDetail.FindBy(
                    Transactions[CustomerTransactionTypeEnum.Sales]);



                foreach (CustomerTransactionDetail detail in customerTransactionDetailList)
                {
                    RouteInventory routeInventory = RouteInventory.Prepare(
                        StorageTypeEnum.Store);

                    routeInventory.Item = detail.Item;

                    routeInventory.ModifySale(detail.Quantity);
                }
            }


            foreach (CustomerTransaction transaction in Transactions.Values)
            {
                transaction.BusinessTransaction.DateCommited = DateTime.Now;
                transaction.BusinessTransaction.Status = BusinessTransactionStatusEnum.Commited;

                BusinessTransaction.Update(transaction.BusinessTransaction);
            }

            m_commonData.RouteScheduleQueue.Status = RouteScheduleQueueStatusEnum.Serviced;

            CustomerVisit.DateFinished = DateTime.Now;

            RouteScheduleQueue.Update(m_commonData.RouteScheduleQueue);

            CustomerVisit.Update(CustomerVisit);

            Database.Commit();
        }

        #endregion

        #region AssignDocumentNumbers

        public void AssignDocumentNumbers()
        {
            foreach (CustomerTransaction transaction in Transactions.Values)
            {
                BusinessTransaction.AssignDocumentNumber(transaction.BusinessTransaction);
            }
        }

        #endregion
    }
}
