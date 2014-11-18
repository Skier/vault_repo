using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Servman;
using Servman.SDK;

namespace Dalworth.LeadCentral.Intuit.Sync
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine("Please enter any key to stop service");
                //Host.LogFileWriter = File.AppendText(Host.GetPath("log.txt"));
                Configuration.RemoveConfigReadOnlyAttribute();
                Configuration.LoadGlobalConfiguration();

                var syncService = new LeadCentralSyncService();

                syncService.StartService();

                Console.ReadLine();

                syncService.StopService();

                Host.Trace("Main", "DONE!!");
                return;
            }

            var servicesToRun = new ServiceBase[] { new LeadCentralSyncService() };

            Configuration.RemoveConfigReadOnlyAttribute();
            Configuration.LoadGlobalConfiguration();

            ServiceBase.Run(servicesToRun);
        }
    }
}
