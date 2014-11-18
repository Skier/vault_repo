using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using SmartSchedule.Service.Sync.WcfServiceClient;

namespace SmartSchedule.Service.Sync
{
    public partial class SyncWindowsService : ServiceBase
    {
        private Timer m_timerSync;
        private Timer m_timerRecommendations;
        private Timer m_timerKeepAlive;

        static void Main(string[] args)
        {
            try
            {
                FileInfo logFile = new FileInfo(Host.GetPath("log.txt"));
                if (logFile.Length > 3000000)
                {
                    if (!Directory.Exists(Host.GetPath(@"Log\")))
                        Directory.CreateDirectory(Host.GetPath(@"Log\"));

                    DirectoryInfo directoryInfo = new DirectoryInfo(Host.GetPath(@"Log\"));
                    string maxFileInfo = directoryInfo.GetFiles("log?????.txt").Max(fileInfo => fileInfo.Name);

                    int maxFileNumber = 0;
                    if (!string.IsNullOrEmpty(maxFileInfo))
                        maxFileNumber = int.Parse(maxFileInfo.Replace("log", string.Empty).Replace(".txt", string.Empty));
                    logFile.MoveTo(Host.GetPath(@"Log\") + string.Format("log{0}.txt", 
                        (++maxFileNumber).ToString("00000")));
                }
                    
            }
            catch (Exception) { }

            using (StreamWriter streamWriter = File.AppendText(Host.GetPath("log.txt")))
            {
                Host.LogFileWriter = streamWriter;

                try
                {
                    Configuration.RemoveConfigReadOnlyAttribute();
                    Configuration.LoadGlobalConfiguration();
                    Host.Trace("Program::Main", "Application started");

                    using (SyncWindowsService service = new SyncWindowsService())
                    {
                        if (Environment.UserInteractive)
                        {
                            service.OnStart(args);
                            Console.WriteLine("Press Enter to stop service...");
                            Console.ReadLine();
                            service.OnStop();
                        }
                        else
                            ServiceBase.Run(service);                        
                    }

                    Host.Trace("Program::Main", "Application stoped");
                }
                catch (Exception e)
                {
                    Host.Trace("Program::Main", "Application crashed " + e);
                    throw;                    
                }
                finally
                {
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
        }

        public SyncWindowsService()
        {
            InitializeComponent();

            m_timerKeepAlive = new Timer();
            m_timerKeepAlive.Elapsed += OnTimerKeepAliveElapsed;
            m_timerSync = new Timer();
            m_timerRecommendations = new Timer();
            m_timerSync.Elapsed += OnTimerSyncElapsed;
            m_timerRecommendations.Elapsed += OnTimerRecommendationsElapsed;
        }

        protected override void OnStart(string[] args)
        {
            m_timerKeepAlive.Interval = 120000;
            m_timerKeepAlive.Start();
            WcfClient.WcfClient.Instance.KeepAliveDummy();

            m_timerSync.Interval = Configuration.TicketImportInterval;
            if (Configuration.IsSyncEnabled)
                m_timerSync.Start();
            m_timerRecommendations.Interval = 500;
            m_timerRecommendations.Start();
        }

        protected override void OnStop()
        {
            m_timerKeepAlive.Stop();
            m_timerSync.Stop();
            m_timerRecommendations.Stop();
        }

        public void OnTimerKeepAliveElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                m_timerKeepAlive.Stop();
                WcfClient.WcfClient.Instance.KeepAliveDummy();
            }
            catch (Exception ex)
            {
                Host.Trace("OnTimerKeepAliveElapsed", "Unhandled exception: " + ex.Message + ex.StackTrace);
            }
            finally
            {
                m_timerKeepAlive.Start();
            }
        }

        public static void DoImport()
        {
            List<string> correctTicketList = Domain.Sync.Sync.FindTicketNumbersCheck();
            List<Order> orders = Domain.Sync.Sync.FindImportOrders();            
            WcfClient.WcfClient.Instance.ApplyServmanData(orders);   
         
            if (correctTicketList.Count == orders.Count)
                return;

            List<string> queryTicketList = new List<string>();
            foreach (var order in orders)
                queryTicketList.Add(order.TicketNumber);

            bool isMissingTickets = false;
            foreach (var correctTicketNumber in correctTicketList)
            {
                if (!queryTicketList.Contains(correctTicketNumber))
                {
                    Host.Trace("SmartScheduleServmanSync ERROR",
                        string.Format("Sync query should contain {0} but it doesn't", correctTicketNumber));
                    isMissingTickets = true;
                }
            }

            if (isMissingTickets)
                WcfClient.WcfClient.Instance.SendErrorEmail();   
        }

        public void OnTimerSyncElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                m_timerSync.Stop();
                DoImport();
                Host.Trace("SmartScheduleServmanSync", "Import Done");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Error reading file"))
                    Host.Trace("SmartScheduleServmanSync", "DBF access closed");
                else
                    Host.Trace("SmartScheduleServmanSync", "Unhandled exception: " + ex.Message + ex.StackTrace);
                Database.Close();
            }
            finally
            {
                m_timerSync.Start();
            }
        }

        private void OnTimerRecommendationsElapsed(object sender, ElapsedEventArgs args)
        {
            try
            {
                m_timerRecommendations.Stop();
                
                using (IDbConnection connection = Connection.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
                {
                    connection.Open();
                    List<RecommendationRequest> requests 
                        = RecommendationRequest.FindUnprocessedRequests(connection);

                    //TODO: uncomment this to enable optimizer
//                    if (requests.Count == 0)
//                    {
//                        WcfClient.WcfClient.Instance.ProcessPendingOptimizations();
//                        return;
//                    }                        

                    foreach (RecommendationRequest request in requests)
                    {
                        RecommendationRequest.DeleteRequest(request, connection);
                        Host.Trace("SmartScheduleRecommendations",
                            string.Format("Request {0} found", request.Guid));
                    }

                    foreach (RecommendationRequest request in requests)
                    {
                        List<RecommendationResponseItem> responses
                            = WcfClient.WcfClient.Instance.GetRecommendations(request);
                        Host.Trace("SmartScheduleRecommendations",
                            string.Format("Response for request {0} received", request.Guid));

                        foreach (RecommendationResponseItem responseItem in responses)
                            RecommendationResponseItem.Insert(responseItem, connection);
                        Host.Trace("SmartScheduleRecommendations",
                            string.Format("Response for request {0} exported to DBF", request.Guid));
                    }                                       
                }                
            }
            catch (Exception ex)
            {
                Host.Trace("SmartScheduleRecommendations", "Unhandled exception: " + ex.Message + ex.StackTrace);
            }
            finally
            {
                m_timerRecommendations.Start();
            }
        }
    }
}
