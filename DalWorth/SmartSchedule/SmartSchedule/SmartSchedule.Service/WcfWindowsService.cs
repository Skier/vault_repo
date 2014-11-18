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
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;

namespace SmartSchedule.Service
{
    public partial class WcfWindowsService : ServiceBase
    {
        ServiceHost host;

        static void Main(string[] args)
        {
            try
            {
                FileInfo logFile = new FileInfo(Host.GetPath("log.txt"));
                if (DateTime.Now.Date > logFile.CreationTime.Date)
                {
                    if (!Directory.Exists(Host.GetPath(@"Log\")))
                        Directory.CreateDirectory(Host.GetPath(@"Log\"));
                    DateTime arcivedLogDate = logFile.CreationTime.Date;
                    logFile.CreationTime = DateTime.Now;
                    logFile.MoveTo(Host.GetPath(@"Log\") + string.Format("log{0}-{1}-{2}.txt",
                        arcivedLogDate.Year, arcivedLogDate.Month.ToString("00"), arcivedLogDate.Day.ToString("00")));
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

                    using (WcfWindowsService service = new WcfWindowsService())
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

        public WcfWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Type serviceType = typeof(WcfService);
            host = new ServiceHost(serviceType);                                                
            host.Open();
        }

        protected override void OnStop()
        {
            if (host != null)
                host.Close();
        }


        static void OnEvents(IEventInfo eventInfo)
        {
            if (eventInfo is Exception)
            {
                Exception e = ((Exception)eventInfo);

                Host.Trace("Program::OnEvents",
                    e.Message);

                StringBuilder stringBuilder = new StringBuilder();

                while (e != null)
                {
                    stringBuilder.Append(e.Message);
                    stringBuilder.Append(":\n");
                    stringBuilder.Append(e.StackTrace);
                    stringBuilder.Append(new String('-', 20));

                    e = e.InnerException;
                }

                Host.Trace("Program::OnEvents",
                    stringBuilder.ToString());
            }
        }
    }
}
