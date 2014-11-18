using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.Dashboard;
using Dalworth.Server.Windows;
using DevExpress.XtraScheduler;

namespace Dalworth.Server.MainForm.CallbackProcess
{
    public class CallbackProcessModel : IModel
    {
        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region ItemsToProcess

        private List<CallbackReportWrapper> m_itemsToProcess;
        public List<CallbackReportWrapper> ItemsToProcess
        {
            get { return m_itemsToProcess; }
            set { m_itemsToProcess = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_customer = m_itemsToProcess[0].Customer;

            foreach (CallbackReportWrapper item in m_itemsToProcess)
            {
                if (m_customer.ID != item.Customer.ID)
                {
                    m_customer = null;
                    break;
                }
            }
        }

        #endregion

        #region Process

        public void Process(int? period, DateTime? callbackDate, 
            CallbackProcessTransactionStatusEnum action)
        {
            foreach (CallbackReportWrapper item in m_itemsToProcess)
            {
                try
                {
                    CallbackProcessTransaction transaction = CallbackProcessTransaction.FindByPrimaryKey(
                        item.Customer.ID, DateTime.Now.Date);
                    transaction.CallbackProcessTransactionStatus = action;
                    CallbackProcessTransaction.Update(transaction);
                    item.ProcessTransaction = transaction;
                }
                catch (DataNotFoundException)
                {
                    CallbackProcessTransaction transaction = new CallbackProcessTransaction(
                        item.Customer.ID, DateTime.Now.Date, (int)action);
                    CallbackProcessTransaction.Insert(transaction);
                    item.ProcessTransaction = transaction;
                }

                item.Customer.CallbackInquireDate = DateTime.Now;
                item.Customer.CallbackLastAttemptDate = DateTime.Now;

                if (action == CallbackProcessTransactionStatusEnum.NotIntrested)
                {                    
                    item.Customer.CallbackDaysInterval = null;
                    item.Customer.CallbackExactDate = null;
                    item.Customer.CallbackDoNotCall = false;
                    item.Customer.CallbackLeftMessageCount = 0;
                    item.Customer.CallbackBusyCount = 0;
                } 
                else if (action == CallbackProcessTransactionStatusEnum.DoNotCall)
                {
                    item.Customer.CallbackDaysInterval = null;
                    item.Customer.CallbackExactDate = null;
                    item.Customer.CallbackDoNotCall = true;
                    item.Customer.CallbackLeftMessageCount = 0;
                    item.Customer.CallbackBusyCount = 0;
                }
                else if (action == CallbackProcessTransactionStatusEnum.VisitCreated)
                {
                    item.Customer.CallbackDaysInterval = null;
                    item.Customer.CallbackExactDate = null;
                    item.Customer.CallbackDoNotCall = false;
                    item.Customer.CallbackLeftMessageCount = 0;
                    item.Customer.CallbackBusyCount = 0;
                }
                else if (action == CallbackProcessTransactionStatusEnum.LeftMessage)
                {
                    if (item.Customer.CallbackLeftMessageCount >= 3)
                    {
                        item.Customer.CallbackDaysInterval = null;
                        item.Customer.CallbackExactDate = null;
                        item.Customer.CallbackDoNotCall = false;
                        item.Customer.CallbackLeftMessageCount = 0;
                        item.Customer.CallbackBusyCount = 0;                        
                    } else
                    {
                        item.Customer.CallbackDaysInterval = period;
                        item.Customer.CallbackExactDate = callbackDate;
                        item.Customer.CallbackDoNotCall = false;
                        item.Customer.CallbackLeftMessageCount++;
                        item.Customer.CallbackBusyCount = item.Customer.CallbackBusyCount;                        
                    }
                }
                else if (action == CallbackProcessTransactionStatusEnum.Busy)
                {
                    item.Customer.CallbackDaysInterval = period;
                    item.Customer.CallbackExactDate = callbackDate;
                    item.Customer.CallbackDoNotCall = false;
                    item.Customer.CallbackLeftMessageCount = item.Customer.CallbackLeftMessageCount;
                    item.Customer.CallbackBusyCount++;
                }
                else if (action == CallbackProcessTransactionStatusEnum.CallReschedule)
                {
                    item.Customer.CallbackDaysInterval = period;
                    item.Customer.CallbackExactDate = callbackDate;
                    item.Customer.CallbackDoNotCall = false;
                    item.Customer.CallbackLeftMessageCount = 0;
                    item.Customer.CallbackBusyCount = 0;
                }
                
                Customer.Update(item.Customer);
            }
        }

        #endregion
    }
}
