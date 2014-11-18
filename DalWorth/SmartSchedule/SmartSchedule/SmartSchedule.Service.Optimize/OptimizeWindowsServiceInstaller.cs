using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SmartSchedule.Service.Optimize
{
    [RunInstaller(true)]
    public class OptimizeWindowsServiceInstaller : Installer
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

        public OptimizeWindowsServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            m_serviceInstaller = new ServiceInstaller();

            //# Service Account Information

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information

            m_serviceInstaller.DisplayName = "SmartSchedule Optimizer";
            m_serviceInstaller.StartType = ServiceStartMode.Automatic;
            
            //# This must be identical to the WindowsService.ServiceBase name

            //# set in the constructor of WindowsService.cs

            m_serviceInstaller.ServiceName = "SmartSchedule Optimizer";

            Installers.Add(serviceProcessInstaller);
            Installers.Add(m_serviceInstaller);            
        }

    }
}
