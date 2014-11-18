using System;
using System.IO;
using System.ServiceProcess;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Servman.Service
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Configuration.RemoveConfigReadOnlyAttribute();
                Configuration.LoadGlobalConfiguration();


                if (args[0].StartsWith("--"))
                {
                    Host.LogFileWriter = File.AppendText(Host.GetPath("log.txt"));

                    if (args[0] == "--import-transactions")
                    {
                        var fromDateArr = args[1].Split('-');
                        var year = int.Parse(fromDateArr[0]);
                        var month = int.Parse(fromDateArr[1]);
                        var date = int.Parse(fromDateArr[2]);

                        Transaction.Import(new DateTime(year, month, date));
                    }

                    if (args[0] == "--rematch-calls")
                    {
                        var fromDateArr = args[1].Split('-');
                        var year = int.Parse(fromDateArr[0]);
                        var month = int.Parse(fromDateArr[1]);
                        var date = int.Parse(fromDateArr[2]);

                        DigiumRequestProcessor.MatchTransactionsToCalls(new DateTime(year, month, date));
                    }

                    if (args[0] == "--rematch-workflow")
                    {
                        var workflowId = args[1];
                        DigiumRequestProcessor.MatchTransactionsToCalls(rematchWorkflowId:workflowId);
                    }

                    if (args[0] == "--update-orders")
                    {
                        var fromDateArr = args[1].Split('-');
                        var year = int.Parse(fromDateArr[0]);
                        var month = int.Parse(fromDateArr[1]);
                        var date = int.Parse(fromDateArr[2]);

                        Order.UpdateCurrentOrders(new DateTime(year, month, date));
                    }

                    return;
                }


                Console.WriteLine("Please enter any key to stop service");
                //Host.LogFileWriter = File.AppendText(Host.GetPath("log.txt"));
                
                NetServmanSync sync = new NetServmanSync();

                sync.Start();

                Console.ReadLine();

                sync.Stop();
               
                Host.Trace("Main", "DONE!!");
                return;
            }

            ServiceBase[] servicesToRun = new ServiceBase[] { new NetServmanSync() };
                        
            Configuration.RemoveConfigReadOnlyAttribute();
            Configuration.LoadGlobalConfiguration();

            ServiceBase.Run(servicesToRun);            
        }
    }
}