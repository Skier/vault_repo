using System;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using Servman;
using Servman.Data;
using Servman.SDK;
using Servman.Service;

namespace Dalworth.LeadCentral.Intuit.Sync
{
    partial class LeadCentralSyncService : ServiceBase
    {
        private readonly Timer Timer;

        public LeadCentralSyncService()
        {
            InitializeComponent();
            
            Timer = new Timer();
            Timer.Elapsed += OnTimerElapsed;
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
            Host.LogFileWriter = File.AppendText(Host.GetPath("lead_centrtal.log"));
            Host.Trace("LeadCentralSyncService", "Service Started");

            Timer.Interval = Configuration.SyncInterval;
            OnTimerElapsed(null, null);
            Timer.Start();

        }

        protected override void OnStop()
        {
            Host.Trace("LeadCentralSyncService", "Service Stopped");
            Host.LogFileWriter.Close();
            Timer.Stop();
        }

        public void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {               
            try
            {
                Timer.Stop();
                SyncService.SyncCustomers();
                Host.Trace("LeadCentralSyncService", "Sync done.");
            }
            catch (Exception ex)
            {
                Host.Trace("LeadCentralSyncService", "Unhandled exception: " + ex.Message + ex.StackTrace);
                Database.Close();
            }
            finally
            {
                Timer.Start();
            }            
        }
    }
}
