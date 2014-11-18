using System;
using System.ServiceProcess;
using Dalworth.Common;
using Configuration = Dalworth.Common.SDK.Configuration;

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
                
                Configuration.RemoveConfigReadOnlyAttribute();
                Configuration.LoadGlobalConfiguration();

                var syncService = new LeadCentralSyncService();

                syncService.StartService();

                var x = Console.ReadLine();

                Host.Trace("Main", "DONE!!");

                syncService.StopService();
                return;
            }

            var servicesToRun = new ServiceBase[] { new LeadCentralSyncService() };

            Configuration.RemoveConfigReadOnlyAttribute();
            Configuration.LoadGlobalConfiguration();

            ServiceBase.Run(servicesToRun);
        }

    }
}
