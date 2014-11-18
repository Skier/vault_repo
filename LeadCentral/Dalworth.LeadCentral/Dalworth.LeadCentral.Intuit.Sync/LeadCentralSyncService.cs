using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using Dalworth.Common;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Intuit.Sync
{
    partial class LeadCentralSyncService : ServiceBase
    {
        private readonly Timer m_syncTimer;

        public LeadCentralSyncService()
        {
            InitializeComponent();

            m_syncTimer = new Timer();
            m_syncTimer.Elapsed += OnSyncTimerElapsed;
        }

        public void StartService()
        {
            OnStart(null);
        }

        public void StopService()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            var logFile = Host.GetPath("lead_centrtal.log");
            var writer = File.AppendText(logFile);
            Host.LogFileWriter = writer;
            Host.Trace("LeadCentralSyncService", "Service Started");

            if (Configuration.Sync.SyncInterval > 0)
            {
                m_syncTimer.Interval = Configuration.Sync.SyncInterval;
                m_syncTimer.Start();
            }
        }

        protected override void OnStop()
        {
            Host.Trace("LeadCentralSyncService", "Service Stopped");
            Host.LogFileWriter.Close();
            m_syncTimer.Stop();
        }

        public void OnSyncTimerElapsed(object sender, ElapsedEventArgs e)
        {
            m_syncTimer.Stop();
            try
            {
                try
                {
                    PhoneCallService.ProcessPhoneCalls();
                }
                catch (Exception ex)
                {

                    Host.Trace("LeadCentralSyncService", "Unhandled exception: " + ex.Message + ex.StackTrace);
                }
            }
            finally
            {
                m_syncTimer.Start();
            }
        }
    }
}
