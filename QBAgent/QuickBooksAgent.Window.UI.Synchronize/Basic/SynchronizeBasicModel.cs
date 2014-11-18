using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.QBSDK;
using System.Xml;
using QuickBooksAgent.QBSDK.Domain;
using QuickBooksAgent.Domain;
using System.Diagnostics;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Windows.UI.Synchronize.Basic
{
    public enum TaskStateEnum
    { 
        Wait,
        Working,
        Error,
        Done
    }

    public enum TaskNameEnum
    { 
        LicenseCheck,
        CreateDatabase,
        RecievingSessionKey,
        ReceivingServerTime,        
        Connecting,
        PerformingData,
        Uploading,
        WaitingResponse,
        Downloading,
        ProcessRequest,
        ProcessCompany,
        ProcessTerms,
        ProcessCustomers,
        ProcessItems,
        ProcessInvoices,
        ProcessEmployees,
        ProcessVendors,
        ProcessTimeTrackings,
        ProcessAccounts,
        ProcessChecks,
        ProcessCreditCardsCharges,
        ProcessCreditCardsCredits,
        DeletingOutOfDateTransactions,
    }
    
    public enum SyncResultEnum
    {        
        LicenseFailed,
        Other
    }

    public delegate void SynchronizeBasicTaskChangedHandler (int index, int percentComplete);

    public class SynchronizeBasicModel:ITableModel,IModel
    {
        List<Synchronizer> m_synchronizers = new List<Synchronizer>();

        public List<Synchronizer> Synchronizers
        {
            get { return m_synchronizers; }
        }
        
        public class Task
        {
            public TaskStateEnum State = TaskStateEnum.Wait;
            public TaskNameEnum TaskName;
            public String FriendlyName;

            public Task(TaskNameEnum task)
            {
                TaskName = task;
                FriendlyName = task.ToString();
            }
            public Task(TaskNameEnum task,String friendlyName)
            {
                TaskName = task;
                FriendlyName = friendlyName;
            }
        }

        public class TaskCollection:List<Task>
        {
            public void Reset() 
            {
                Clear();

                m_currentTaskIndex = 0;
            }

            public void Change(TaskNameEnum taskName, TaskStateEnum taskState)
            {

                for (int i = m_currentTaskIndex; i < Count; i++)
                {
                    if (this[i].TaskName == taskName)
                    {
                        this[i].State = taskState;
                        m_currentTaskIndex = i;
                        m_currentTaskName = taskName;
                        break;
                    }
                    else
                    {
                        this[i].State = TaskStateEnum.Done;
                    }
                }



                    Host.Trace("SychronizeBaseModel::ChangeTask", this[m_currentTaskIndex].FriendlyName);

            }

            private int m_currentTaskIndex;

            public int CurrentTaskIndex
            {
                get { return m_currentTaskIndex; }
            }

            TaskNameEnum m_currentTaskName;

            public TaskNameEnum CurrentTaskName
            {
                get { return m_currentTaskName; }
            }

        }

        public event SynchronizeBasicTaskChangedHandler TaskChanged;

        TaskCollection m_tasks = new TaskCollection();

        #region QBRequest
        QBRequest m_request;

        public QBRequest Request
        {
            get { return m_request; }
        }
        #endregion

        QBConnection m_connection;

        public QBConnection Connection
        {
            get { return m_connection; }
        }

        #region ITableModel Members

        public int GetRowCount()
        {
            return m_tasks.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "State";

            return "Task";
        }

        public Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            Task task = m_tasks[rowIndex];

            if (columnIndex == 0)
            {
                return task.State.ToString();
            }
            else
            {

                if (task.TaskName == TaskNameEnum.Uploading
                    && task.State != TaskStateEnum.Wait)
                    return String.Format("Uploading, sent:{0}, left {1}",
                        m_bytesUploaded,
                        m_bytesUploadLeft);
                else if (task.TaskName == TaskNameEnum.Downloading
                    && task.State != TaskStateEnum.Wait)
                {
                    return String.Format("Downloading, recieved:{0}",
                            m_bytesDownloaded);
                }



                return task.FriendlyName;
            }
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return null;
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region InitTasks

        void InitTasks()
        {
            m_tasks.Reset();

            m_tasks.Add(new Task(TaskNameEnum.LicenseCheck, "Checking license"));

            if (!Database.IsDatabaseExist())
                m_tasks.Add(new Task(TaskNameEnum.CreateDatabase, "Create database"));

            if (m_connection.Type == QBConnectionType.Secure
                && m_connection.SessionTicket.IsExpired)
            {
                m_tasks.Add(new Task(TaskNameEnum.RecievingSessionKey,"Recieving session key"));
            }

            m_tasks.Add(new Task(TaskNameEnum.Connecting, "Connecting"));
            m_tasks.Add(new Task(TaskNameEnum.PerformingData,"Performing data"));
            m_tasks.Add(new Task(TaskNameEnum.Uploading, "Uploading"));
            m_tasks.Add(new Task(TaskNameEnum.WaitingResponse,"Waiting response"));
            m_tasks.Add(new Task(TaskNameEnum.Downloading, "Downloading"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessRequest, "Processing request"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessCompany, "Processing company"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessTerms, "Processing terms"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessCustomers, "Processing customers"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessItems, "Processing items"));            
            m_tasks.Add(new Task(TaskNameEnum.ProcessEmployees, "Processing employees"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessVendors, "Processing vendors"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessTimeTrackings, "Processing time trackings"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessAccounts, "Processing accounts"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessInvoices, "Processing invoices"));            
            m_tasks.Add(new Task(TaskNameEnum.ProcessChecks, "Processing checks"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessCreditCardsCharges, "Processing CC charges"));
            m_tasks.Add(new Task(TaskNameEnum.ProcessCreditCardsCredits, "Processing CC credits"));
            if (Database.IsDatabaseExist() && Configuration.QuickBooks.TransactionFreshnessDays.HasValue
                && Configuration.QuickBooks.LastSyncDate.HasValue)
                m_tasks.Add(new Task(TaskNameEnum.DeletingOutOfDateTransactions, "Deleting old transactions"));

            foreach (Synchronizer synchronizer in m_synchronizers)
                synchronizer.Reset();
            
        }

        #endregion

        #region IModel Members

        public void Init()
        {            
            m_connection = new QBConnection(Configuration.QuickBooks.AppLogin,
                           new QBConnectionTicket(Configuration.QuickBooks.AppCode,
                           Configuration.QuickBooks.ConnectionTicket));            

            m_request = new QBRequest();

            m_request.Process += new QBRequestProcessRequest(OnRequestProcess);

            m_connection.StateChanged += new QBConnectionStateChangedHandler(OnConnectionStateChanged);
            m_connection.UploadProgress += new QBConnectionProgressHandler(OnUploadProgress);
            m_connection.DownloadProgress += new QBConnectionProgressHandler(OnDonwloadProgress);
            m_synchronizers.Add(new CompanySynchronizer());
            m_synchronizers.Add(new TermsSynchronizer());
            m_synchronizers.Add(new CustomerSynchronizer());
            m_synchronizers.Add(new ItemSynchronizer());            
            m_synchronizers.Add(new EmployeeSynchronizer());
            m_synchronizers.Add(new VendorSynchronizer());
            m_synchronizers.Add(new TimeTrackingSynchronizer());
            m_synchronizers.Add(new AccountSynchronizer());
            m_synchronizers.Add(new InvoiceSynchronizer());
            m_synchronizers.Add(new CheckSynchronizer());
            m_synchronizers.Add(new CreditCardChargeSynchronizer());
            m_synchronizers.Add(new CreditCardCreditSynchronizer());

            InitTasks();
        }


        #endregion

        #region OnDonwloadProgress
        int m_bytesDownloaded;
        int m_bytesDownloadLeft;

        void OnDonwloadProgress(int bytesProcessed, int bytesLeft)
        {
            m_bytesDownloaded = bytesProcessed;
            m_bytesDownloadLeft = bytesLeft;

            if (Change != null)
                Change.Invoke();
        }
        #endregion

        #region OnUploadProgress
        int m_bytesUploaded;
        int m_bytesUploadLeft;

        void OnUploadProgress(int bytesProcessed, int bytesLeft)
        {
            m_bytesUploaded = bytesProcessed;
            m_bytesUploadLeft = bytesLeft;


            if (Change != null)
                Change.Invoke();
        }
        #endregion

        #region OnRequestProcess
        void OnRequestProcess(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement("QBXMLMsgsRq");
            xmlWriter.WriteAttributeString("onError", "continueOnError");//"rollbackOnError"

                foreach (Synchronizer synchronizer in m_synchronizers)
                    synchronizer.ProcessRequest(xmlWriter);

            xmlWriter.WriteEndElement();
        }
        #endregion

        #region OnConnectionStateChanged
        void OnConnectionStateChanged(QBConnectionStateEnum previousState, 
            QBConnectionStateEnum currentState, QBConnectionStateEnum prevLatestState)
        {
            if (currentState <= prevLatestState)
                return;
            
            switch(currentState)
            {
                case QBConnectionStateEnum.Connecting:
                    ChangeTask(TaskNameEnum.Connecting);
                    break;
                case QBConnectionStateEnum.PerformingData:
                    ChangeTask(TaskNameEnum.PerformingData);
                    break;
                case QBConnectionStateEnum.Uploading:
                    ChangeTask(TaskNameEnum.Uploading);
                    break;
                case QBConnectionStateEnum.WaitingResponse:
                    ChangeTask(TaskNameEnum.WaitingResponse);
                    break;
                case QBConnectionStateEnum.Downloading:
                    ChangeTask(TaskNameEnum.Downloading);
                    break;
            }
        }
        #endregion

        private void ChangeTask(TaskNameEnum taskName)
        {

            m_tasks.Change(taskName, TaskStateEnum.Working);


            if (TaskChanged != null)
                TaskChanged.Invoke(m_tasks.CurrentTaskIndex,(int)(((double)m_tasks.CurrentTaskIndex / (double)m_tasks.Count) * 100));

            if (Change != null)
                Change.Invoke();
        }

        private void ChangeTask(TaskNameEnum taskName,TaskStateEnum state)
        {
            m_tasks.Change(taskName, state);

            if (Change != null)
                Change.Invoke();
        }

        #region Start
        public SyncResultEnum Start()
        {
            InitTasks();

            m_bytesDownloaded
                = m_bytesDownloadLeft
                = m_bytesUploaded
                = m_bytesUploadLeft
                = 0;

            return Synchronize();
        }

        private void SaveRegValue(DateTime dateTime)
        {
            string value = Crypto.Encrypt("3f6he", dateTime.ToString("yyyy-MM-dd"));            
                                    
            Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Apps\\Affilia Software Q-Agent", "Package",
                ASCIIEncoding.ASCII.GetBytes(value), RegistryValueKind.Binary);            
        }
        
        private SyncResultEnum Synchronize()
        {
            try
            {
                ChangeTask(TaskNameEnum.LicenseCheck);
                Host.Trace("SynchronizeBasicModel::Synchronize", "Cheking license");

                QBRequest timeRequest = new QBRequest();
                QBConnection connection = new QBConnection(Configuration.QuickBooks.AppLogin,
                               new QBConnectionTicket(Configuration.QuickBooks.AppCode,
                               Configuration.QuickBooks.ConnectionTicket));
                connection.ConnectionTicket.Ticket = Crypto.Decrypt(
                    SessionModel.Instance.LoginInfo.Password,
                    Configuration.QuickBooks.ConnectionTicket);

                QBResponse qbTimeResponse = connection.Send(timeRequest);
                List<QBResponseReader> timeReaders = new List<QBResponseReader>();
                qbTimeResponse.Process(timeReaders);
                Configuration.QuickBooks.CurrentSyncTime = qbTimeResponse.SyncDate;
                
                if (!Configuration.App.IsLicensed())
                {
                    object regValue = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Apps\\Affilia Software Q-Agent", "Package", null);
                    DateTime? evalFirstSyncDate = null;
                    if (regValue == null)
                    {
                        SaveRegValue(Configuration.QuickBooks.CurrentSyncTime);
                    }
                    else
                    {
                        try
                        {
                            string s = ASCIIEncoding.ASCII.GetString((byte[])regValue, 0, ((byte[])regValue).Length);
                            evalFirstSyncDate = DateTime.Parse(Crypto.Decrypt("3f6he", s));
                        }
                        catch (Exception)
                        {
                            SaveRegValue(Configuration.QuickBooks.CurrentSyncTime);
                        }
                    }
                    
                    if (evalFirstSyncDate == null)
                    {
                        MessageBox.Show("Evaluation: 30 day(s) left");
                    } else
                    {
                        int daysLeft = 30 - (Configuration.QuickBooks.CurrentSyncTime - evalFirstSyncDate.Value).Days;
                        
                        if (daysLeft > 0)                    
                            MessageBox.Show(string.Format("Evaluation: {0} day(s) left", daysLeft));                    
                        else
                        {
                            ChangeTask(m_tasks.CurrentTaskName, TaskStateEnum.Error);
                            MessageBox.Show("Evaluation period is expired. Please register your application.");                        
                            return SyncResultEnum.LicenseFailed;
                        }                                                
                    }
                }
                
                Host.Trace("SynchronizeBasicModel::Synchronize", "");                
                if (!Database.IsDatabaseExist())
                {
                    ChangeTask(TaskNameEnum.CreateDatabase);

                    Database.CreateDatabase(true);
                    Configuration.QuickBooks.IsNewlyCreatedDB = true;
                    InsertInitialRecords();
                } else
                    Configuration.QuickBooks.IsNewlyCreatedDB = false;
                                
                if (Connection.Type == QBConnectionType.Secure
                    && Connection.SessionTicket.IsExpired)
                {
                    ChangeTask(TaskNameEnum.RecievingSessionKey);

                    Connection.SessionTicket.Recieve(SessionModel.Instance.LoginInfo);
                }
                                
                Database.Begin();

                QBResponse qbResponse = m_connection.Send(Request);
                ChangeTask(TaskNameEnum.ProcessRequest);
                
                List<QBResponseReader> readers = new List<QBResponseReader>();

                foreach (Synchronizer synchronizer in m_synchronizers)
                    readers.Add(synchronizer.QBReader);                    
                    
                qbResponse.ReaderChanged += new QBResponse.ReaderChangedHandler(OnReaderChanged);

                qbResponse.Process(readers);                

                if (!Configuration.QuickBooks.IsNewlyCreatedDB && Configuration.QuickBooks.TransactionFreshnessDays.HasValue
                    && Configuration.QuickBooks.LastSyncDate.HasValue)
                {
                    ChangeTask(TaskNameEnum.DeletingOutOfDateTransactions);
                    RemoveTransactionsOlderThan(Configuration.QuickBooks.LastSyncDate.Value.AddDays(-Configuration.QuickBooks.TransactionFreshnessDays.Value));
                }                                    
                
                ChangeTask(TaskNameEnum.ProcessItems, TaskStateEnum.Done);

                if (!qbResponse.IsSuccessResultCode)
                    throw new QuickBooksAgentException(qbResponse.StatusMessage);

                if (qbResponse.IsSuccessResultCode && !IsErrorsExist())
                {
                    if (Configuration.QuickBooks.TransactionFreshnessDays != null)
                        Configuration.QuickBooks.LastSyncDate = Configuration.QuickBooks.CurrentSyncTime;
                    else
                        Configuration.QuickBooks.LastSyncDate = qbResponse.SyncDate;
                    
                    Configuration.QuickBooks.TransactionFreshnessDaysLastSync = Configuration.QuickBooks.TransactionFreshnessDays;
                    Configuration.Save();                    
                }

                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();
                ChangeTask(m_tasks.CurrentTaskName, TaskStateEnum.Error);

                if (e is WebException)
                {
                    MessageBox.Show(
                        "Internet connection error occurred. Check your connection settings and start sync again.\nDetails: " +
                        e.Message,
                        "Connection error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                } else if (e is QuickBooksHeaderException)
                {
                    QuickBooksHeaderException exception = (QuickBooksHeaderException) e;

                    if (exception.Code == 2020) //Require session Authentication
                    {
                        MessageBox.Show(
                            "Your connection ticket requires session authentication. Q-Agent doesn't support session authentication tickets. Please set ticket login security to 'No' or create new ticket.\n\n" +
                            "QuickBooks message: " + e.Message,
                            "Request processing error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);                                                                    
                    } else
                    {
                        MessageBox.Show(
                            "QuickBooks Online encountered an error: " +
                            e.Message,
                            "Request processing error", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);                                            
                    }
                    
                }
                else
                {
                    throw e;
                }
                                
            }
            return SyncResultEnum.Other;
        }

        private void OnReaderChanged(QBResponseReader reader)
        {
            if (reader is QBResponseCompanyReader)
                ChangeTask(TaskNameEnum.ProcessCompany);
            else if (reader is QBResponseTermsReader)
                ChangeTask(TaskNameEnum.ProcessTerms);
            else if (reader is QBResponseCustomerReader)
                ChangeTask(TaskNameEnum.ProcessCustomers);
            else if (reader is QBResponseItemReader)
                ChangeTask(TaskNameEnum.ProcessItems);
            else if (reader is QBResponseEmployeeReader)
                ChangeTask(TaskNameEnum.ProcessEmployees);
            else if (reader is QBResponseVendorReader)
                ChangeTask(TaskNameEnum.ProcessVendors);
            else if (reader is QBResponseTimeTrackingReader)
                ChangeTask(TaskNameEnum.ProcessTimeTrackings);
            else if (reader is QBResponseAccountReader)
                ChangeTask(TaskNameEnum.ProcessAccounts);
            else if (reader is QBResponseInvoiceReader)
                ChangeTask(TaskNameEnum.ProcessInvoices);
            else if (reader is QBResponseCheckReader)
                ChangeTask(TaskNameEnum.ProcessChecks);
            else if (reader is QBResponseCreditCardChargeReader)
                ChangeTask(TaskNameEnum.ProcessCreditCardsCharges);
            else if (reader is QBResponseCreditCardCreditReader)
                ChangeTask(TaskNameEnum.ProcessCreditCardsCredits);
        }

        #endregion
        
        private void RemoveTransactionsOlderThan(DateTime date)
        {            
            Check.DeleteOlderThan(date);
            CreditCard.DeleteOlderThan(date);
            Invoice.DeleteOlderThan(date);
            TimeTracking.DeleteOlderThan(date);
        }

        #region InsertInitialRecords

        private void InsertInitialRecords()
        {
            EntityState.Insert(EntityState.Synchronized);
            EntityState.Insert(EntityState.Modified);
            EntityState.Insert(EntityState.Created);
            EntityState.Insert(EntityState.Deleted);
            
            QBEntityType.Insert(QBEntityType.Employee);
            QBEntityType.Insert(QBEntityType.Vendor);
            QBEntityType.Insert(QBEntityType.Customer);

            CreditCardType.Insert(CreditCardType.Credit);
            CreditCardType.Insert(CreditCardType.Charge);
            
            AccountType.Insert(AccountType.AccountsPayable       );  
            AccountType.Insert(AccountType.AccountsReceivable    );  
            AccountType.Insert(AccountType.Bank                  );  
            AccountType.Insert(AccountType.CostOfGoodsSold       );  
            AccountType.Insert(AccountType.CreditCard            );  
            AccountType.Insert(AccountType.Equity                );  
            AccountType.Insert(AccountType.Expense               );  
            AccountType.Insert(AccountType.FixedAsset            );  
            AccountType.Insert(AccountType.Income                );  
            AccountType.Insert(AccountType.LongTermLiability     );  
            AccountType.Insert(AccountType.NonPosting            );  
            AccountType.Insert(AccountType.OtherAsset            );
            AccountType.Insert(AccountType.OtherCurrentAsset     );
            AccountType.Insert(AccountType.OtherCurrentLiability );
            AccountType.Insert(AccountType.OtherExpense          );
            AccountType.Insert(AccountType.OtherIncome           );
            
            DetailAccountType.Insert(DetailAccountType.AP                                   );
            DetailAccountType.Insert(DetailAccountType.AR                                   );
            DetailAccountType.Insert(DetailAccountType.AccumulatedAdjustment                );
            DetailAccountType.Insert(DetailAccountType.AccumulatedAmortization              );
            DetailAccountType.Insert(DetailAccountType.AccumulatedAmortizationOfOtherAssets );
            DetailAccountType.Insert(DetailAccountType.AccumulatedDepletion                 );
            DetailAccountType.Insert(DetailAccountType.AccumulatedDepreciation              );
            DetailAccountType.Insert(DetailAccountType.AdvertisingOrPromotional             );
            DetailAccountType.Insert(DetailAccountType.AllowanceForBadDebts                 );
            DetailAccountType.Insert(DetailAccountType.Amortization                         );
            DetailAccountType.Insert(DetailAccountType.Auto                                 );
            DetailAccountType.Insert(DetailAccountType.BadDebts                             );
            DetailAccountType.Insert(DetailAccountType.BankCharges                          );
            DetailAccountType.Insert(DetailAccountType.Buildings                            );
            DetailAccountType.Insert(DetailAccountType.CashOnHand                           );
            DetailAccountType.Insert(DetailAccountType.CharitableContributions              );
            DetailAccountType.Insert(DetailAccountType.Checking                             );
            DetailAccountType.Insert(DetailAccountType.CommonStock                          );
            DetailAccountType.Insert(DetailAccountType.CostOfLabor                          );
            DetailAccountType.Insert(DetailAccountType.CostOfLaborCOS                       );
            DetailAccountType.Insert(DetailAccountType.CreditCard                           );
            DetailAccountType.Insert(DetailAccountType.DepletableAssets                     );
            DetailAccountType.Insert(DetailAccountType.Depreciation                         );
            DetailAccountType.Insert(DetailAccountType.DevelopmentCosts                     );
            DetailAccountType.Insert(DetailAccountType.DiscountsOrRefundsGiven              );
            DetailAccountType.Insert(DetailAccountType.DividendIncome                       );
            DetailAccountType.Insert(DetailAccountType.DuesAndSubscriptions                 );
            DetailAccountType.Insert(DetailAccountType.EmployeeCashAdvances                 );
            DetailAccountType.Insert(DetailAccountType.Entertainment                        );
            DetailAccountType.Insert(DetailAccountType.EntertainmentMeals                   );
            DetailAccountType.Insert(DetailAccountType.EquipmentRental                      );
            DetailAccountType.Insert(DetailAccountType.EquipmentRentalCOS                   );
            DetailAccountType.Insert(DetailAccountType.FederalIncomeTaxPayable              );
            DetailAccountType.Insert(DetailAccountType.FurnitureAndFixtures                 );
            DetailAccountType.Insert(DetailAccountType.Goodwill                             );
            DetailAccountType.Insert(DetailAccountType.Insurance                            );
            DetailAccountType.Insert(DetailAccountType.InsurancePayable                     );
            DetailAccountType.Insert(DetailAccountType.IntangibleAssets                     );
            DetailAccountType.Insert(DetailAccountType.InterestEarned                       );
            DetailAccountType.Insert(DetailAccountType.InterestPaid                         );
            DetailAccountType.Insert(DetailAccountType.Inventory                            );
            DetailAccountType.Insert(DetailAccountType.InvestmentMortgageOrRealEstateLoans  );
            DetailAccountType.Insert(DetailAccountType.InvestmentOther                      );
            DetailAccountType.Insert(DetailAccountType.InvestmentTaxExemptSecurities        );
            DetailAccountType.Insert(DetailAccountType.InvestmentUSGovObligations           );
            DetailAccountType.Insert(DetailAccountType.Land                                 );
            DetailAccountType.Insert(DetailAccountType.LeaseBuyout                          );
            DetailAccountType.Insert(DetailAccountType.LeaseholdImprovements                );
            DetailAccountType.Insert(DetailAccountType.LegalAndProfessionalFees             );
            DetailAccountType.Insert(DetailAccountType.Licenses                             );
            DetailAccountType.Insert(DetailAccountType.LineOfCredit                         );
            DetailAccountType.Insert(DetailAccountType.LoanPayable                          );
            DetailAccountType.Insert(DetailAccountType.LoansToOfficers                      );
            DetailAccountType.Insert(DetailAccountType.LoansToOthers                        );
            DetailAccountType.Insert(DetailAccountType.LoansToStockholders                  );
            DetailAccountType.Insert(DetailAccountType.MachineryAndEquipment                );
            DetailAccountType.Insert(DetailAccountType.MoneyMarket                          );
            DetailAccountType.Insert(DetailAccountType.NonProfitIncome                      );
            DetailAccountType.Insert(DetailAccountType.NotesPayable                         );
            DetailAccountType.Insert(DetailAccountType.OfficeOrGeneralAdministrativeExpenses);
            DetailAccountType.Insert(DetailAccountType.OpeningBalanceEquity                 );
            DetailAccountType.Insert(DetailAccountType.OrganizationalCosts                  );
            DetailAccountType.Insert(DetailAccountType.OtherCostsOfServiceCOS               );
            DetailAccountType.Insert(DetailAccountType.OtherCurrentAssets                   );
            DetailAccountType.Insert(DetailAccountType.OtherCurrentLiab                     );
            DetailAccountType.Insert(DetailAccountType.OtherFixedAssets                     );
            DetailAccountType.Insert(DetailAccountType.OtherInvestmentIncome                );
            DetailAccountType.Insert(DetailAccountType.OtherLongTermAssets                  );
            DetailAccountType.Insert(DetailAccountType.OtherLongTermLiab                    );
            DetailAccountType.Insert(DetailAccountType.OtherMiscExpense                     );
            DetailAccountType.Insert(DetailAccountType.OtherMiscIncome                      );
            DetailAccountType.Insert(DetailAccountType.OtherMiscServiceCost                 );
            DetailAccountType.Insert(DetailAccountType.OtherPrimaryIncome                   );
            DetailAccountType.Insert(DetailAccountType.OwnersEquity                         );
            DetailAccountType.Insert(DetailAccountType.PaidInCapitalOrSurplus               );
            DetailAccountType.Insert(DetailAccountType.PartnerContributions                 );
            DetailAccountType.Insert(DetailAccountType.PartnerDistributions                 );
            DetailAccountType.Insert(DetailAccountType.PartnersEquity                       );
            DetailAccountType.Insert(DetailAccountType.PayrollClearing                      );
            DetailAccountType.Insert(DetailAccountType.PayrollExpenses                      );
            DetailAccountType.Insert(DetailAccountType.PayrollTaxPayable                    );
            DetailAccountType.Insert(DetailAccountType.PenaltiesAndSettlements              );
            DetailAccountType.Insert(DetailAccountType.PreferredStock                       );
            DetailAccountType.Insert(DetailAccountType.PrepaidExpenses                      );
            DetailAccountType.Insert(DetailAccountType.PrepaidExpensesPayable               );
            DetailAccountType.Insert(DetailAccountType.PromotionalMeals                     );
            DetailAccountType.Insert(DetailAccountType.RentOrLeaseOfBuildings               );
            DetailAccountType.Insert(DetailAccountType.RentsHeldInTrust                     );
            DetailAccountType.Insert(DetailAccountType.RentsInTrustLiab                     );
            DetailAccountType.Insert(DetailAccountType.RepairAndMaintenance                 );
            DetailAccountType.Insert(DetailAccountType.Retainage                            );
            DetailAccountType.Insert(DetailAccountType.RetainedEarnings                     );
            DetailAccountType.Insert(DetailAccountType.SalesOfProductIncome                 );
            DetailAccountType.Insert(DetailAccountType.SalesTaxPayable                      );
            DetailAccountType.Insert(DetailAccountType.Savings                              );
            DetailAccountType.Insert(DetailAccountType.SecurityDeposits                     );
            DetailAccountType.Insert(DetailAccountType.ServiceOrFeeIncome                   );
            DetailAccountType.Insert(DetailAccountType.ShareholderNotesPayable              );
            DetailAccountType.Insert(DetailAccountType.ShippingFreightAndDelivery           );
            DetailAccountType.Insert(DetailAccountType.ShippingFreightAndDeliveryCOS        );
            DetailAccountType.Insert(DetailAccountType.StateOrLocalIncomeTaxPayable         );
            DetailAccountType.Insert(DetailAccountType.SuppliesAndMaterials                 );
            DetailAccountType.Insert(DetailAccountType.SuppliesAndMaterialsCOGS             );
            DetailAccountType.Insert(DetailAccountType.TaxExemptInterest                    );
            DetailAccountType.Insert(DetailAccountType.TaxesPaid                            );
            DetailAccountType.Insert(DetailAccountType.Travel                               );
            DetailAccountType.Insert(DetailAccountType.TravelMeals                          );
            DetailAccountType.Insert(DetailAccountType.TreasuryStock                        );
            DetailAccountType.Insert(DetailAccountType.TrustAccounts                        );
            DetailAccountType.Insert(DetailAccountType.TrustAccountsLiab                    );
            DetailAccountType.Insert(DetailAccountType.UndepositedFunds                     );
            DetailAccountType.Insert(DetailAccountType.Utilities                            );
            DetailAccountType.Insert(DetailAccountType.Vehicles                             );
            
            TransactionType.Insert(TransactionType.ARRefundCreditCard   ); 
            TransactionType.Insert(TransactionType.Bill                 );
            TransactionType.Insert(TransactionType.BillPaymentCheck     );
            TransactionType.Insert(TransactionType.BillPaymentCreditCard);
            TransactionType.Insert(TransactionType.BuildAssembly        );
            TransactionType.Insert(TransactionType.Charge               );
            TransactionType.Insert(TransactionType.Check                );
            TransactionType.Insert(TransactionType.CreditCardCharge     );
            TransactionType.Insert(TransactionType.CreditCardCredit     );
            TransactionType.Insert(TransactionType.CreditMemo           );
            TransactionType.Insert(TransactionType.Deposit              );
            TransactionType.Insert(TransactionType.Estimate             );
            TransactionType.Insert(TransactionType.InventoryAdjustment  );
            TransactionType.Insert(TransactionType.Invoice              );
            TransactionType.Insert(TransactionType.ItemReceipt          );
            TransactionType.Insert(TransactionType.JournalEntry         );
            TransactionType.Insert(TransactionType.LiabilityAdjustment  );
            TransactionType.Insert(TransactionType.Paycheck             );
            TransactionType.Insert(TransactionType.PayrollLiabilityCheck);
            TransactionType.Insert(TransactionType.PurchaseOrder        );
            TransactionType.Insert(TransactionType.ReceivePayment       );
            TransactionType.Insert(TransactionType.SalesOrder           );
            TransactionType.Insert(TransactionType.SalesReceipt         );
            TransactionType.Insert(TransactionType.SalesTaxPaymentCheck );
            TransactionType.Insert(TransactionType.Transfer             );
            TransactionType.Insert(TransactionType.VendorCredit         );
            TransactionType.Insert(TransactionType.YTDAdjustment        );
            TransactionType.Insert(TransactionType.Payment              );
        }

        #endregion

        internal bool IsErrorsExist()
        {
            foreach (Synchronizer synchronizer in m_synchronizers)
            {
                foreach (QBResponseStatus status in synchronizer.QBReader.ResponseStatus)
                {
                    if (status.IsError)
                        return true;
                }
            }

            return false;
        }
    }
}
