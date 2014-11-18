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
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using SmartSchedule.Service.Optimize.WcfServiceClient;

namespace SmartSchedule.Service.Optimize
{
    public partial class OptimizeWindowsService : ServiceBase
    {
        private Timer m_timerKeepAlive;

        static void Main(string[] args)
        {
            try
            {
                FileInfo logFile = new FileInfo(Host.GetPath("log.txt"));
                if (logFile.Length > 10000000)
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

                    using (OptimizeWindowsService service = new OptimizeWindowsService())
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

        public OptimizeWindowsService()
        {
            InitializeComponent();

            m_timerKeepAlive = new Timer();
            m_timerKeepAlive.Elapsed += OnTimerKeepAliveElapsed;
        }

        protected override void OnStart(string[] args)
        {
            m_timerKeepAlive.Interval = 120000;
            m_timerKeepAlive.Start();
            WcfClient.WcfClient.Instance.KeepAliveDummy();
        }

        protected override void OnStop()
        {
            m_timerKeepAlive.Stop();
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

    [CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class CallbackListener : IWcfServiceCallback
    {
        public void OnOptimizationRequested(Schedule schedule)
        {
            try
            {
                Optimizer optimizer = new Optimizer(schedule);
                optimizer.IterationDone += OnOptimizerIterationDone;

                Schedule optimizedSchedule = optimizer.Optimize();
                Host.Trace("OPTIMIZER", "Optimized schedule ready to send");
                WcfClient.WcfClient.Instance.ApplyOptimizationResult(optimizedSchedule);
                Host.Trace("OPTIMIZER", "Optimized schedule sent");
            }
            catch (Exception e)
            {
                Host.Trace("OPTIMIZER::OnOptimizationRequested", "Unhandled exception: : " + e.Message + e.StackTrace);
            }            
        }

        public void ForceSync(SyncTypeEnum syncType)
        {
            
        }

        private void OnOptimizerIterationDone()
        {
            try
            {
                WcfClient.WcfClient.Instance.KeepAliveDummy();
            }
            catch (Exception e)
            {
                Host.Trace("OPTIMIZER::OnOptimizerIterationDone", "Unhandled exception: : " + e.Message + e.StackTrace);
            }            
        }

        public void OnViewModelChanged(CallbackInfo info){}
    }

}
