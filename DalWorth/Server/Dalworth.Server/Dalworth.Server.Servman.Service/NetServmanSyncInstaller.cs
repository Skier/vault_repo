using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using System.Text;

namespace Dalworth.Server.Servman.Service
{
    [RunInstaller(true)]
    public class NetServmanSyncInstaller : Installer
    {
        private ServiceInstaller m_serviceInstaller;

        public override void Install(IDictionary stateSaver)
        {
            if (Context != null && Context.Parameters["servicename"] != null
                    && Context.Parameters["servicename"] != string.Empty)
            {
                m_serviceInstaller.DisplayName = Context.Parameters["servicename"];
                m_serviceInstaller.ServiceName = Context.Parameters["servicename"];
            }

            base.Install(stateSaver);
        }


        public override void Uninstall(IDictionary savedState)
        {
            if (Context != null && Context.Parameters["servicename"] != null
                    && Context.Parameters["servicename"] != string.Empty)
            {
                m_serviceInstaller.DisplayName = Context.Parameters["servicename"];
                m_serviceInstaller.ServiceName = Context.Parameters["servicename"];
            }

            base.Uninstall(savedState);
        }

        public NetServmanSyncInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller =
                                  new ServiceProcessInstaller();
            m_serviceInstaller = new ServiceInstaller();

            //# Service Account Information

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information

            m_serviceInstaller.DisplayName = "NET Servman Sync Service";
            m_serviceInstaller.StartType = ServiceStartMode.Automatic;
            
            //# This must be identical to the WindowsService.ServiceBase name

            //# set in the constructor of WindowsService.cs

            m_serviceInstaller.ServiceName = "NET Servman Sync Service";

            Installers.Add(serviceProcessInstaller);
            Installers.Add(m_serviceInstaller);            
        }
    }
}
