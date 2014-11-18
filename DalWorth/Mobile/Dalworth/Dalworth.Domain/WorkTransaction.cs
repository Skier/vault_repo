using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading;
using Dalworth.Data;
using Dalworth.Domain.Package;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class WorkTransaction : ICounterField
    {
        private static Timer m_timer;
        private static DateTime m_lastSuccessSendDate;

        public delegate void SendAttemptThresholdExceededHandler();
        public static event SendAttemptThresholdExceededHandler SendAttemptThresholdExceeded;

        public WorkTransaction(){ }

        #region WorkTransactionType

        public WorkTransactionTypeEnum WorkTransactionType
        {
            get { return (WorkTransactionTypeEnum)m_workTransactionTypeId; }
            set { m_workTransactionTypeId = (int)value; }
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "WorkTransaction"; }
        }

        #endregion        

        #region FindNotSentTransactions

        private const string SqlFindNotSentTransactions =
            @"SELECT *
            FROM WorkTransaction
                WHERE IsSent = 0";

        public static List<WorkTransaction> FindNotSentTransactions(IDbTransaction transaction)
        {
            List<WorkTransaction> transactions = new List<WorkTransaction>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindNotSentTransactions, transaction))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        transactions.Add(Load(dataReader));
                    }
                }
            }
            return transactions;
        }

        public static List<WorkTransaction> FindNotSentTransactions()
        {
            return FindNotSentTransactions(null);
        }


        #endregion

        #region Send

        public static void Send()
        {
            Send(0);
        }

        public static void Send(int dueTime)
        {
            Host.Trace("WorkTransaction:Send", "Method executed");
            if (m_timer == null)
            {
                m_timer = new Timer(OnSendAttempt, null, dueTime, Configuration.SendAttemptPeriod);
                Host.Trace("WorkTransaction:Send", "New timer created");
                m_lastSuccessSendDate = DateTime.Now;
            }                
        }

        #endregion

        #region SendTransaction

        private void SendTransaction()
        {            
            Work work = Work.FindByPrimaryKey(WorkId);
            DalworthSyncService service = new DalworthSyncService();

            if (WorkTransactionType == WorkTransactionTypeEnum.VisitDeclined)
            {
                service.DeclineVisit(Configuration.ConnectionKey, work.TechnicianEmployeeId, VisitId.Value, Notes);

            } else if (WorkTransactionType == WorkTransactionTypeEnum.NoGo)
            {
                service.NoGo(Configuration.ConnectionKey, work.TechnicianEmployeeId, VisitId.Value);

            } else if (WorkTransactionType == WorkTransactionTypeEnum.VisitAccepted)
            {
                service.AcceptVisit(Configuration.ConnectionKey, work.TechnicianEmployeeId, VisitId.Value);

            } else if (WorkTransactionType == WorkTransactionTypeEnum.VisitCompleted)
            {
                SendCompleteVisit(work, service);

            } else if (WorkTransactionType == WorkTransactionTypeEnum.SubmitETC)
            {
                WorkTransactionEtc etc = WorkTransactionEtc.FindByPrimaryKey(ID);
                service.Etc(Configuration.ConnectionKey, work.TechnicianEmployeeId, VisitId.Value, 
                            etc.SaleAmount, etc.Hours, etc.Minutes, etc.Notes);
                
            } else if (WorkTransactionType == WorkTransactionTypeEnum.VisitArrived)
            {
                service.ArrivedToVisit(Configuration.ConnectionKey, work.TechnicianEmployeeId, VisitId.Value);
            } else if (WorkTransactionType == WorkTransactionTypeEnum.GPS)
            {
                WorkTransactionGps gps = WorkTransactionGps.FindByPrimaryKey(ID);
                service.Gps(Configuration.ConnectionKey, work.ID, (float)gps.Latitude, (float)gps.Longitude, gps.GpsTime);
            }
        }

        #endregion

        #region OnSendAttempt

        private static void OnSendAttempt(object state)
        {
            IDbConnection dbConnection = Connection.Instance.GetDbConnection(ConnectionKeyEnum.Temporary2);            


            m_timer.Change(Timeout.Infinite, Timeout.Infinite);

            Host.Trace("WorkTransaction:OnSendAttempt", "Method executed");

            IDbTransaction dbTransaction = dbConnection.BeginTransaction();
            List<WorkTransaction> transactions = FindNotSentTransactions(dbTransaction);
            dbTransaction.Rollback();

            if (transactions.Count == 0)
            {
                Host.Trace("WorkTransaction:OnSendAttempt", "Thread destroyed");
                m_timer.Dispose();
                m_timer = null;
                return;
            } else if (Configuration.ConnectionKey == string.Empty)
            {
                Host.Trace("WorkTransaction:OnSendAttempt", "Connection key in not decrypted yet, will try later");
                return;
            }

            foreach (WorkTransaction transaction in transactions)
            {
                IDbTransaction dbTransaction2 = null;

                try
                {                    
                    Host.Trace("WorkTransaction:OnSendAttempt", "Attempt to send transaction, ID = " + transaction.ID);
                    transaction.SendTransaction();

                    dbTransaction2 = dbConnection.BeginTransaction();
                    transaction.IsSent = true;
                    Update(transaction, dbTransaction2);
                    dbTransaction2.Commit();

                    Host.Trace("WorkTransaction:OnSendAttempt", "Transaction ID = " + transaction.ID + " has been sent");                    
                    m_lastSuccessSendDate = DateTime.Now;
                }
                catch (WebException)
                {
                    Host.Trace("WorkTransaction:OnSendAttempt", "Attempt to send transaction, ID = " + transaction.ID + " has failed. Was trying " + DateTime.Now.Subtract(m_lastSuccessSendDate).TotalMilliseconds + " miliseconds");
                    
                    if (DateTime.Now.Subtract(m_lastSuccessSendDate).TotalMilliseconds > Configuration.SendAttemptFailThreshold)
                    {                                                
                        m_timer.Dispose();                                                
                        m_timer = null;

                        if (SendAttemptThresholdExceeded != null)
                            SendAttemptThresholdExceeded.Invoke();
                    } else
                    {
                        m_timer.Change(Configuration.SendAttemptPeriod, Configuration.SendAttemptPeriod);                        
                    }

                    return;
                }                
                catch (Exception ex)
                {
                    if (dbTransaction2 != null)
                        dbTransaction2.Rollback();

                    Host.Trace("WorkTransaction:OnSendAttempt", "Unknown error while attempting to send transaction" 
                                                                + ex.Message + ex.StackTrace);
                    m_timer.Dispose();
                    m_timer = null;
                    throw;
                }                
            }

            m_timer.Change(0, Configuration.SendAttemptPeriod);
        }

        #endregion

        #region SaveGpsInfo

        public static void SaveGpsInfo(float latitude, float longitude, DateTime gpsTime)
        {
            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = ApplicationPackage.GetApplicationPackage().Work.ID;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.GPS;
            workTransaction.TransactionDate = DateTime.Now;
            Insert(workTransaction);

            WorkTransactionGps gps = new WorkTransactionGps(workTransaction.ID, latitude, longitude, gpsTime);
            WorkTransactionGps.Insert(gps);
        }

        #endregion

        #region SendCompleteVisit

        private void SendCompleteVisit(Work work, DalworthSyncService service)
        {
            WorkTransactionPayment paymentServer = null;
            if (AmountCollected != decimal.Zero) //Contains payment
            {
                paymentServer = new WorkTransactionPayment();
                paymentServer.WorkTransactionPaymentTypeId = (int)WorkTransactionPaymentTypeEnum.Cash;
                paymentServer.PaymentAmount = AmountCollected;
            }

            List<WorkTransactionTask> workTransactionTasksLocal = WorkTransactionTask.FindBy(ID);
            List<TaskPackage> taskPackagesServer = new List<TaskPackage>();
            foreach (WorkTransactionTask workTransactionTaskLocal in workTransactionTasksLocal)
            {
                Task taskLocal = Task.FindByPrimaryKey(workTransactionTaskLocal.TaskId);
                TaskPackage packageServer = new TaskPackage();
                taskPackagesServer.Add(packageServer);

                packageServer.Task = new SyncService.Task();
                packageServer.Task.TaskTypeId = taskLocal.TaskTypeId;
                packageServer.Task.TaskStatusId = taskLocal.TaskStatusId;
                packageServer.Task.Number = taskLocal.Number;

                List<WorkTransactionTaskItem> workTransactionTaskItemsLocal = WorkTransactionTaskItem.FindBy(workTransactionTaskLocal.ID);
                List<SyncService.Item> itemsServer = new List<SyncService.Item>();
                foreach (WorkTransactionTaskItem workTransactionTaskItemLocal in workTransactionTaskItemsLocal)
                {
                    Item itemLocal = Item.FindByPrimaryKey(workTransactionTaskItemLocal.ItemId);

                    SyncService.Item itemServer = new SyncService.Item();
                    itemServer.ID = itemLocal.ServerId ?? 0;
                    itemServer.ItemTypeId = itemLocal.ItemTypeId;
                    itemServer.SerialNumber = itemLocal.SerialNumber;
                    itemServer.ItemShapeId = itemLocal.ItemShapeId;
                    itemServer.Width = itemLocal.Width;
                    itemServer.Height = itemLocal.Height;
                    itemServer.Diameter = itemLocal.Diameter;
                    itemServer.IsProtectorApplied = itemLocal.IsProtectorApplied;
                    itemServer.IsPaddingApplied = itemLocal.IsPaddingApplied;
                    itemServer.IsMothRepelApplied = itemLocal.IsMothRepelApplied;
                    itemServer.IsRapApplied = itemLocal.IsRapApplied;
                    itemServer.CleanCost = itemLocal.CleanCost;
                    itemServer.ProtectorCost = itemLocal.ProtectorCost;
                    itemServer.PaddingCost = itemLocal.PaddingCost;
                    itemServer.MothRepelCost = itemLocal.MothRepelCost;
                    itemServer.RapCost = itemLocal.RapCost;
                    itemServer.OtherCost = itemLocal.OtherCost;
                    itemServer.SubTotalCost = itemLocal.SubTotalCost;
                    itemServer.TaxCost = itemLocal.TaxCost;
                    itemServer.TotalCost = itemLocal.TotalCost;

                    itemsServer.Add(itemServer);
                }

                packageServer.Items = itemsServer.ToArray();
            }
            
            service.CompleteVisit(Configuration.ConnectionKey, work.TechnicianEmployeeId, 
                VisitId.Value, taskPackagesServer.ToArray(), paymentServer);                            
        }

        #endregion
    }
}
      