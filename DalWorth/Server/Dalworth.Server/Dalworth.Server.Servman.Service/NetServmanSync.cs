using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.ServmanSync;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Servman.Service
{
    public partial class NetServmanSync : ServiceBase
    {
        private readonly Timer m_timer;
        private DateTime m_currentDate;
        private DateTime m_lastCustomerImportDate;
        private DateTime m_lastExportDate;        
        private DateTime m_lastTechnicianImportDate;
        private DateTime m_lastRugRemiderSentDate;

        public NetServmanSync()
        {
            InitializeComponent();
            m_lastExportDate = DateTime.MinValue;
            m_lastTechnicianImportDate = DateTime.MinValue;
            m_lastRugRemiderSentDate = DateTime.MinValue;
            m_timer = new Timer();
            m_timer.Elapsed += OnTimerElapsed;
        }

        public void Start()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            m_currentDate = DateTime.Now;
            Host.LogFileWriter = File.AppendText(Host.GetPath("log.txt"));

            Host.Trace("NetServmanSync", "Service Started");

            m_timer.Interval = Configuration.TicketImportInterval;
            OnTimerElapsed(null, null);
            m_timer.Start();
        }

        protected override void OnStop()
        {
            Host.Trace("NetServmanSync", "Service Stopped");
            Host.LogFileWriter.Close();
            m_timer.Stop();
        }

        public void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {               
            try
            {
                m_timer.Stop();

                BackgroundJobPendingModel.ProcessPendingJobs();

                if (m_lastRugRemiderSentDate != DateTime.Now.Date)
                {
                    BackgroundJobPendingModel.ProcessCustomerEmailReminders();
                    m_lastRugRemiderSentDate = DateTime.Now;
                }

                if (IsInHoursOfOperation())
                    BackgroundJobPendingModel.ProcessLateLeadNotification();

                if (DateTime.Now.TimeOfDay < Configuration.WorkingTimeStart.TimeOfDay
                    || DateTime.Now.TimeOfDay > Configuration.WorkingTimeEnd.TimeOfDay)
                {
                    Database.CloseConnection(ConnectionKeyEnum.Servman);
                    m_timer.Start();
                    return;
                }

                ImportModel.ImportOrders();
                DigiumLogItem.Import();

                if (DateTime.Now.Subtract(m_lastCustomerImportDate).TotalMilliseconds > Configuration.CustomerImportInterval)
                {
                    ImportModel.ImportCustomers();
                    NetServmanSyncModel.PrintTomorrowsVisits();
                    m_lastCustomerImportDate = DateTime.Now;
                }

                if (DateTime.Now.Subtract(m_lastExportDate).TotalMilliseconds > Configuration.ExportInterval)
                {
                    DalworthExportModel.ExportCustomers();
                    DalworthExportModel.ExportOrders();
                    m_lastExportDate = DateTime.Now;
                }

                if (m_lastTechnicianImportDate.Date != DateTime.Now.Date)
                {
                    PartnerSummaryReportItem.GenerateWeeklyReport();
                    ImportModel.ImportUpdateAllTechnicians();                    
                    Order.UpdateCurrentOrders(DateTime.Now.AddDays(-32));
                    m_lastTechnicianImportDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                Host.Trace("NetServmanSync", "Unhandled exception: " + ex.Message + ex.StackTrace);
                Host.SendErrorEmail("Unhandled exception: " + ex.Message + ex.StackTrace);

                Database.Close();

                if (ex.Message.Contains("Alias is not found"))                
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"\ServiceRestart.bat");
            }
            finally
            {
                ArchiveLog();
                m_timer.Start();
            }            
        }

        private static bool IsInHoursOfOperation ()
        {
            DateTime now = DateTime.Now;

            if (now.DayOfWeek == DayOfWeek.Sunday)
                return false;
            if (now.DayOfWeek == DayOfWeek.Saturday && now.TimeOfDay.Hours < 9 && now.TimeOfDay.Hours > 16)
                return false;
            if (now.TimeOfDay.Hours < 8 && now.TimeOfDay.Hours > 17)
                return false;

            return true;
        }

        private void ArchiveLog()
        {
            try
            {
                if (DateTime.Now.Date != m_currentDate.Date)
                {
                    Host.LogFileWriter.Close();

                    string archiveFileName = m_currentDate.Year + "-"
                        + m_currentDate.Month.ToString("00") + "-" + m_currentDate.Day.ToString("00") + ".txt";
                    string archiveFilePath = Host.GetPath("Logs\\" + archiveFileName);

                    if (File.Exists(archiveFilePath))
                        File.Delete(archiveFilePath);

                    File.Move(Host.GetPath("log.txt"), archiveFilePath);
                    Host.LogFileWriter = File.AppendText(Host.GetPath("log.txt"));
                    m_currentDate = DateTime.Now;                    
                }
            }
            catch (Exception){}
        }

    }
}
