using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Diagnostics;
using MobileTech.Domain;
using MobileTech.Data;
using System.Data;
using MobileTech.ServiceLayer;
#if WINCE
using System.Data.SqlServerCe;
#else
    using System.Data.SqlClient;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.SqlServer.Replication;
    using Microsoft.SqlServer;
#endif

namespace MobileTech.Windows.UI.TComm
{
    public delegate void SyncProgressEvent(int percentComplete);
    public delegate void SyncMessageEvent(String message);

    public class SyncData
    {
        public event SyncMessageEvent Messages;
        public event SyncProgressEvent Progress;
        //private bool m_executing = false;
        private int m_percenteComplete;

        public enum SyncTransactionType { Push, Pull };

        string dbLocalFile = Host.Configuration.GetString(ConfigurationKey.DbLocalFile);
        string replInternetUrl = Host.Configuration.GetString(ConfigurationKey.ReplInternetUrl);
        string replInternetLogin = Host.Configuration.GetString(ConfigurationKey.ReplInternetLogin);
        string replInternetPassword = Host.Configuration.GetString(ConfigurationKey.ReplInternetPassword);
        string replMasterLogin = Host.Configuration.GetString(ConfigurationKey.ReplMasterLogin);
        string replMasterPassword = Host.Configuration.GetString(ConfigurationKey.ReplMasterPassword);
        string replMasterPublisher = Host.Configuration.GetString(ConfigurationKey.ReplMasterPublisher);
        string replMasterPublisherDb = Host.Configuration.GetString(ConfigurationKey.ReplMasterPublisherDb);
        string replMasterPublication = Host.Configuration.GetString(ConfigurationKey.ReplMasterPublication);
        string replTransLogin = Host.Configuration.GetString(ConfigurationKey.ReplTransLogin);
        string replTransPassword = Host.Configuration.GetString(ConfigurationKey.ReplTransPassword);
        string replTransPublisher = Host.Configuration.GetString(ConfigurationKey.ReplTransPublisher);
        string replTransPublisherDb = Host.Configuration.GetString(ConfigurationKey.ReplTransPublisherDb);
        string replTransPublication = Host.Configuration.GetString(ConfigurationKey.ReplTransPublication);
        string replOtherPublication = Host.Configuration.GetString(ConfigurationKey.ReplOtherPublication);
#if !WINCE

        string subServer = @".\SQLEXPRESS";
        //string pubServer = @"10.0.0.49";
        //string replMasterPublication = @"MTS_Master";
        string pubDB = "";
        string subDB = @"MTSMobile";

        ServerConnection subscriberConn;// = new ServerConnection(subServer);
        //Subscriber Connection
        //SqlConnection subscriberSQLConn;
        ServerConnection publisherConn;// = new ServerConnection(subServer);
        //Subscriber Connection
        //SqlConnection publisherSQLConn;

#endif
        public void ReplTransSync()
        {
#if WINCE
            //MGSUtils utils = new Utils();
            SqlCeReplication repl = new SqlCeReplication();


            repl.InternetUrl = replInternetUrl;
            repl.InternetLogin = replInternetLogin;
            repl.InternetPassword = replInternetPassword;

            /// Set Publisher properties.
            ///
            repl.Publisher = replTransPublisher;
            repl.PublisherDatabase = replTransPublisherDb;
            repl.Publication = replTransPublication;

            /// Set Publisher security properties.
            ///
            repl.PublisherSecurityMode = SecurityType.NTAuthentication;
            repl.PublisherLogin = replTransLogin;
            repl.PublisherPassword = replTransPassword;

            /// Set Subscriber properties.
            ///
            repl.SubscriberConnectionString = Configuration.ConnectionString;
            repl.Subscriber = "Driver1";

            ///  Add dynamic filter (filter by Driver IDs).
            ///
            repl.HostName = "1";

            /// Bidirectional or upload only?
            ///
            repl.ExchangeType = ExchangeType.Upload;

            try
            {
                repl.Synchronize();
            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
            finally
            {
                /// Dispose of the Replication object.
                ///
                //repl.Dispose();
            }
#endif

        }
        public bool ReplMasterSync()
        {
#if WINCE
            //MGSUtils utils = new Utils();
            SqlCeReplication repl = new SqlCeReplication();


            repl.InternetUrl = replInternetUrl;
            repl.InternetLogin = replInternetLogin;
            repl.InternetPassword = replInternetPassword;

            /// Set Publisher properties.
            ///
            repl.Publisher = replMasterPublisher;
            repl.PublisherDatabase = replMasterPublisherDb;
            repl.Publication = replMasterPublication;

            /// Set Publisher security properties.
            ///
            repl.PublisherSecurityMode = SecurityType.NTAuthentication;
            repl.PublisherLogin = replMasterLogin;
            repl.PublisherPassword = replMasterPassword;

            /// Set Subscriber properties.
            ///
            repl.SubscriberConnectionString = Configuration.ConnectionString;
            repl.Subscriber = Configuration.Route.ToString().Trim();

            ///  Add dynamic filter (filter by Driver IDs).
            ///
            repl.HostName = Configuration.Route.ToString().Trim();


            /// Bidirectional or upload only?
            ///
            repl.ExchangeType = ExchangeType.BiDirectional;

            try
            {
                repl.Synchronize();
            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
            finally
            {
                /// Dispose of the Replication object.
                ///
                repl.Dispose();
            }
#endif
            return true;
        }
        public bool ReplSync(bool init)
        {
            bool bSync = false;

            string appDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            if (!Directory.Exists(appDir + @"\Database"))
            {
                Directory.CreateDirectory(appDir + @"\Database");
            }
            if (!Directory.Exists(appDir + @"\Database\Temp"))
            {
                Directory.CreateDirectory(appDir + @"\Database\Temp");
            }            //Check to see if we need to sync
            try
            {
                if (init == true)
                {
                    if (File.Exists(MobileTech.Configuration.DBFullPath))
                    {
                        File.Delete(MobileTech.Configuration.DBFullPath);
                    }

                    bSync = true;

                }
                else
                {
                    if (File.Exists(MobileTech.Configuration.DBFullPath))
                    {
                        if (!Route.IsContainsData())
                        {
                            File.Delete(MobileTech.Configuration.DBFullPath);
                        }
                    }
                    bSync = true;
                }

            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
            // Return if we don't need to initialize the Database
            if (!bSync)
            {
                return false;
            }
            // Go ahead with Initial Sync
#if WINCE
            SqlCeReplication repl = new SqlCeReplication();

            repl.InternetUrl = replInternetUrl;
            repl.InternetLogin = replInternetLogin;
            repl.InternetPassword = replInternetPassword;

            /// Set Publisher properties.
            ///
            repl.Publisher = replMasterPublisher;
            repl.PublisherDatabase = replMasterPublisherDb;
            repl.Publication = replMasterPublication;

            /// Set Publisher security properties.
            ///
            repl.PublisherSecurityMode = SecurityType.NTAuthentication;
            repl.PublisherLogin = replMasterLogin;
            repl.PublisherPassword = replMasterPassword;

            /// Set Subscriber properties.
            ///
            repl.SubscriberConnectionString = Configuration.ConnectionString;
            repl.Subscriber = "MobiletechMobile";


            ///  Add dynamic filter ().
            ///
            repl.HostName = Configuration.Route.ToString().Trim();

            /// Bidirectional or upload only?
            ///
            repl.ExchangeType = ExchangeType.BiDirectional;
            try
            {
                /// Create the local database subscription.
                ///
                repl.AddSubscription(AddOption.CreateDatabase);
                //repl.ReinitializeSubscription(false);
                repl.Synchronize();
            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }

            repl.Publication = replOtherPublication;
            try
            {
                repl.AddSubscription(AddOption.ExistingDatabase);
                /// Synchronize to the SQL Server 2000 database to populate the local subscription database.
                ///
                repl.Synchronize();
            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
            /////// Set Publisher properties.
            ///////1
            repl.Publisher = replTransPublisher;
            repl.PublisherDatabase = replTransPublisherDb;
            repl.Publication = replTransPublication;

            /// Set Publisher security properties.
            ///
            repl.PublisherLogin = replTransLogin;
            repl.PublisherPassword = replTransPassword;

            try
            {
                repl.LoginTimeout = 30;
                repl.AddSubscription(AddOption.ExistingDatabase);
                repl.Synchronize();

            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }


            /// Dispose of the Replication object.
            ///
            //repl.Dispose();
#endif
            if (File.Exists(MobileTech.Configuration.DBFullPath))
            {
                if (!Route.IsContainsData())
                {
                    //if (File.Exists(appDir + @"\Database\Temp\MobiletechClean.sdf"))
                    //{
                    //    File.Copy(appDir + @"\Database\Temp\MobiletechClean.sdf", dbLocalFile);
                    //}   

                    return false;
                }
            }
            else
            {
                //if (File.Exists(appDir + @"\Database\Temp\MobiletechOld.sdf"))
                //{
                //    File.Copy(appDir + @"\Database\Temp\MobiletechOld.sdf", dbLocalFile);
                //}   
                return false;
            }
            //if (File.Exists(appDir + @"\Database\Temp\MobiletechOld.sdf"))
            //{
            //    File.Delete(appDir + @"\Database\Temp\MobiletechOld.sdf");
            //}

            return true;
        }
        public bool ClearTransactions()
        {
            try
            {
                SetPercent(0);
                AddMessage(Resources.Message_Sync_Clearing_Trans_Detail + " CustomerTransactionDetail");
                CustomerTransactionDetail.Clear();
                SetPercent(25);
                AddMessage(Resources.Message_Sync_Clearing_Trans_Detail + " CustomerTransaction");
                CustomerTransaction.Clear();
                SetPercent(50);
                AddMessage(Resources.Message_Sync_Clearing_Trans_Detail + " PeriodTransaction");
                PeriodTransaction.Clear();
                SetPercent(75);
                //AddMessage(Resources.Message_Sync_Clearing_Trans_Detail + " InventoryTransaction");
                //InventoryTransaction.Clear();
                //SetPercent(25);
                AddMessage(Resources.Message_Sync_Clearing_Trans_Detail + " BusinessTransaction");
                BusinessTransaction.Clear();
                SetPercent(100);
                
            }
            catch
            {
                SetPercent(100);
                return false;
            }
            return true;
        }
#if !WINCE
        public bool ReplSyncWin(bool init)
        {
            pubDB = replMasterPublisherDb;
            //public Form1()
            //{
            //InitializeComponent();
            //}

            //private void button1_Click(object sender, EventArgs e)

            //{
            //Cursor.Current = Cursors.WaitCursor;
            if (_synchronizeMergePullSubscription())
            {
                Configuration.AppStart = false;
                return true;
            }
            else
            {
                return false;
            }
 
        }
        [STAThread]
        private bool _synchronizeMergePullSubscription()
        {
            try
            {
                //localConn = "Server=localhost;Integrated Security=True;Database=C:\\ROUTENET\\MobileTech.UI.WinCE\\DatabaseMobileTech.sdf";
                //subscriberSQLConn = new SqlConnection(localConn);
                //subscriberSQLConn.Open();
                subscriberConn = new ServerConnection(subServer);

                subscriberConn.Connect();
                MergePullSubscription mPullSub = new MergePullSubscription();
                mPullSub.ConnectionContext = subscriberConn;
                mPullSub.DatabaseName = subDB;
                mPullSub.PublisherName = replMasterPublisher;
                mPullSub.PublicationDBName = pubDB;
                mPullSub.PublicationName = replMasterPublication;
                mPullSub.InternetLogin = replInternetLogin;
                mPullSub.InternetPassword = replInternetPassword;
                mPullSub.InternetSecurityMode = AuthenticationMethod.WindowsAuthentication;
                mPullSub.InternetUrl = replInternetUrl;
                //mPullSub.PublisherSecurity = AuthenticationMethod.WindowsAuthentication;
                mPullSub.UseWebSynchronization = true;
                // if pull subscription exists, start the sync

                if (mPullSub.LoadProperties())

                {/*
                    mPullSub.SynchronizationAgent.ExchangeType = MergeExchangeType.Bidirectional;
                    mPullSub.SynchronizationAgent.Synchronize();*/
                }
                else
                {
                        subscriberConn.Disconnect();
                    // create the pull subscription and retry

                    if (_createMergePullSubscription())

                        _synchronizeMergePullSubscription();

                    }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.ToString() + " " + ex.Message);
                return false;
            }
            finally
            {
                subscriberConn.Disconnect();
                
                //Cursor.Current = Cursors.Default;
            }
            return true;
        }

        private bool _createMergePullSubscription()
        {

            bool retVal = false;

            // To create a pull subscription, you need a connection to the Subscriber
            //and Publisher.

            //subscriberSQLConn = new SqlConnection(localConn);
            //subscriberSQLConn.Open();
            subscriberConn = new ServerConnection(subServer);

            //publisherSQLConn = new SqlConnection(localConn);
            //publisherSQLConn.
            //publisherSQLConn.Open();
            publisherConn = new ServerConnection("10.0.0.49");//publisherSQLConn);
            
            try

            {

                subscriberConn.Connect();
                publisherConn.Connect();

                // Register a new Subscriber at the Publisher, unless it is already
                //registered.

                RegisteredSubscriber subscriber;

                subscriber = new RegisteredSubscriber(subServer, publisherConn);

                if (!subscriber.IsExistingObject)
                {
                    subscriber.Create();
                    subscriber.Refresh();
                }

                MergePublication mergePublication = new MergePublication();
                mergePublication.Name = replMasterPublication;
                mergePublication.DatabaseName = pubDB;
                mergePublication.ConnectionContext = publisherConn;

                // If the publication exists, define the subscription at the Subscriber.

                if (mergePublication.LoadProperties())
                {

                    // If the publication does not support pull subscriptions, allow them.

                    // You must do this bitwise because Publication.Attributes is really a
                    //bitmask.

                    if ((mergePublication.Attributes & PublicationAttributes.AllowPull) == 0)

                    {
                        mergePublication.Attributes =
                        mergePublication.Attributes | PublicationAttributes.AllowPull;
                        mergePublication.CommitPropertyChanges();
                        mergePublication.Refresh();
                    }

                    MergePullSubscription mergePullSubscription = new MergePullSubscription();
                    // Define pull subscription properties.
                    mergePullSubscription.ConnectionContext = subscriberConn;
                    mergePullSubscription.DatabaseName = subDB;
                    mergePullSubscription.PublisherName = replMasterPublisher;
                    mergePullSubscription.PublicationDBName = pubDB;
                    mergePullSubscription.PublicationName = replMasterPublication;
                    mergePullSubscription.SubscriberType = MergeSubscriberType.Local;
                    // Specify the Windows account under which the Merge Agent job runs.
                    mergePullSubscription.SynchronizationAgentProcessSecurity.Login = "msmith";
                    mergePullSubscription.SynchronizationAgentProcessSecurity.Password =
                    "Giles1002590";
                    mergePullSubscription.CreateSyncAgentByDefault = true;

                    // Create the subscription.

                    if (!mergePullSubscription.IsExistingObject) // DDS
                    {
                        mergePullSubscription.Create();
                        mergePullSubscription.Refresh();
                        // Register the pull subscription at the Publisher.
                        mergePublication.MakePullSubscriptionWellKnown(subServer,
                        subDB,
                        mergePullSubscription.SyncType,
                        mergePullSubscription.SubscriberType,
                        mergePullSubscription.Priority);
                    }
                    retVal = true;
                }
                else
                {
                    // Do something here if the publication does not exist.
                    retVal = false;

                }

            }
            catch //(Exception ex)
            {
                retVal = false;
                //MessageBox.Show(ex.ToString() + " " + ex.Message);
            }
            finally
            {
                publisherConn.Disconnect();
                subscriberConn.Disconnect();
            }

            return retVal;

        }
#endif

        public bool ExportTransactions()
        {
            string path = Path.GetDirectoryName(MobileTech.Configuration.AppNameFullPath);
            DataTable dtBusinessTransaction = new DataTable();
            dtBusinessTransaction.TableName = "BusinessTransaction";
            DataTable dtCustomerTransaction = new DataTable();
            dtCustomerTransaction.TableName = "CustomerTransaction";
            DataTable dtCustomerDetailTransaction = new DataTable();
            dtCustomerDetailTransaction.TableName = "CustomerDetailTransaction";
            DataTable dtInventoryTransaction = new DataTable();
            dtInventoryTransaction.TableName = "InventoryTranaction";
            DataTable dtPeriodTransaction = new DataTable();
            dtPeriodTransaction.TableName = "PeriodTransaction";
    
            try
            {

                int? btCount = 0;
                btCount = BusinessTransaction.FindAll(dtBusinessTransaction);
                btCount = CustomerTransaction.FindAll(dtCustomerTransaction);
                btCount = CustomerTransactionDetail.FindAll(dtCustomerDetailTransaction);
                //btCount = itStore.FindAll(dtInventoryTransaction);
                btCount = PeriodTransaction.FindAll(dtPeriodTransaction);
                if (!Directory.Exists(path + @"\Export"))
                {
                    Directory.CreateDirectory(path + @"\Export");
                }
                path += @"\Export\";
                if (File.Exists(path + "BusinessTransaction.xml"))
                {
                    File.Delete(path + "BusinessTransaction.xml");
                }
                if (File.Exists(path + "CustomerTransaction.xml"))
                {
                    File.Delete(path + "CustomerTransaction.xml");
                }
                if (File.Exists(path + "CustomerDetailTransaction.xml"))
                {
                    File.Delete(path + "CustomerDetailTransaction.xml");
                }
                if (File.Exists(path + "PeriodTransaction.xml"))
                {
                    File.Delete(path + "PeriodTransaction.xml");
                }
                SetPercent(0);
                AddMessage(Resources.Message_Sync_Export_Detail + " BusinessTransaction");
                dtBusinessTransaction.WriteXml(path + "BusinessTransaction.xml", XmlWriteMode.WriteSchema);
                SetPercent(25);
                AddMessage(Resources.Message_Sync_Export_Detail + " CustomerTransaction");
                dtCustomerTransaction.WriteXml(path + "CustomerTransaction.xml", XmlWriteMode.WriteSchema);
                SetPercent(50);
                AddMessage(Resources.Message_Sync_Export_Detail + " CustomerDetailTransaction");
                dtCustomerDetailTransaction.WriteXml(path + "CustomerDetailTransaction.xml", XmlWriteMode.WriteSchema);
                SetPercent(75);
                AddMessage(Resources.Message_Sync_Export_Detail + " PeriodTransaction");
                dtPeriodTransaction.WriteXml(path + "PeriodTransaction.xml", XmlWriteMode.WriteSchema);
                SetPercent(100);

            }
            catch (Exception ex)
            {
                throw new MobileTechException(ex);
            }
            return true;

        }
        public bool SyncEmulate()
        {
            Import import = new Import(Host.GetPath("Database\\Import"));
            import.Clear = true;

            import.Messages += new TaskMessageEvent(OnTaskMessages);
            import.Progress += new TaskProgressEvent(OnTaskProgress);

            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
#if WINCE
                    if (!Database.IsDatabaseExist())
                        Database.CreateDatabase();
#else
                    if (!MobileTech.Data.Database.IsDatabaseExist() ||
                    !MobileTech.Data.Database.IsSchemaExist())
                    {
                        MobileTech.Data.Database.CreateDatabase();
                    }
#endif

                }

                import.Execute();

               // Route.Reset();
               // Session.Reset();
               // RouteInventory.Reset();


                Item.UpdateIndex();

            }
            catch (Exception ex)
            {
                EventService.AddEvent(
                    new MobileTechException("Error with importing data",
                    ex));
            }
            Configuration.Location = Route.Current.LocationId;
            Configuration.Route = Route.Current.RouteNumber;
            return true;

        }
        void OnTaskProgress(int percentComplete)
        {
            SetPercent(percentComplete);

        }

        void OnTaskMessages(string message)
        {
            AddMessage(message);
            //m_lbProgressMessage.Refresh();
        }
#region SyncEvents

        public int PercentComplete
        {
            get { return m_percenteComplete; }
        }
		protected void SetPercent(int percentComplete)
		{
			m_percenteComplete = percentComplete;

			if (Progress != null)
				Progress.Invoke(percentComplete);
		}

		protected void AddMessage(String message)
		{
			if (Messages != null)
				Messages.Invoke(message);
		}

		protected void AddMessage(String message,int percentComplete)
		{
			if (Messages != null)
				Messages.Invoke(message);

			SetPercent(percentComplete);
		}
        #endregion
    }
}
