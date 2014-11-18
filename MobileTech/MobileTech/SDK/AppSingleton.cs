
using System;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace MobileTech
{
   public static class SingletonApp 
   {
      static Mutex m_Mutex;
      static Form m_MainForm;
      static IChannel m_IpcChannel;
      const string Uri = "SingletonApp"; 
      public static void Run()
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += OnExit;
            RegisterActivationMonitor();
            Application.Run();
         }
         else
         {
            ActivateFirstInstance();
         }
      }
      public static void Run(ApplicationContext context)
      {
         if(IsFirstInstance())
         {
            MainForm = context.MainForm;
            Application.ApplicationExit += OnExit;
            RegisterActivationMonitor();
            Application.Run(context);
         }
         else
         {
            ActivateFirstInstance();
         }
      }
      public static void Run(Form mainForm)
      {
         if(IsFirstInstance())
         {
            Application.ApplicationExit += OnExit;
            MainForm = mainForm;
            RegisterActivationMonitor();
            Application.Run(mainForm);
         }
         else
         {
            ActivateFirstInstance();
         }
      }
      static bool IsFirstInstance()
      {
         Assembly assembly = Assembly.GetEntryAssembly();
         string name = assembly.FullName;

         m_Mutex = new Mutex(false,name);
         bool owned = false;
         owned = m_Mutex.WaitOne(TimeSpan.Zero,false);
         return owned ;
      }
      static void OnExit(object sender,EventArgs args)
      {
         //Must be done is this order to avoid a race condition 
         ChannelServices.UnregisterChannel(m_IpcChannel);
         m_Mutex.ReleaseMutex();
         m_Mutex.Close();
      }
      static void RegisterActivationMonitor()
      {
         Assembly assembly = Assembly.GetEntryAssembly();
         string name = assembly.FullName;

         //Registering IPC channel
         m_IpcChannel = new IpcChannel(name);
         ChannelServices.RegisterChannel(m_IpcChannel, false);

         Type serverType = typeof(ActivationMonitor);
         RemotingConfiguration.RegisterWellKnownServiceType(serverType,Uri,WellKnownObjectMode.SingleCall);
      }
      static internal Form MainForm
      {
         get
         {
            return m_MainForm;
         }
         set
         {
            m_MainForm = value;
         }
      }
      static void ActivateFirstInstance()
      {
         Assembly assembly = Assembly.GetEntryAssembly();
         string name = assembly.FullName;
         string url = "ipc://" + name + "/" + Uri;
         Type serverType = typeof(ActivationMonitor);

         RemotingConfiguration.RegisterWellKnownClientType(serverType,url);
         ActivationMonitor monitor = new ActivationMonitor();
         monitor.ActivateApplication();
      }
   }
}
