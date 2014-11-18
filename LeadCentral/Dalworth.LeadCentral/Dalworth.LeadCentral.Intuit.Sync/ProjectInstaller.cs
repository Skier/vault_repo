using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;


namespace Dalworth.LeadCentral.Intuit.Sync
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        private ServiceInstaller m_serviceInstaller;

        public ProjectInstaller()
        {
            InitializeComponent();

            var serviceProcessInstaller = new ServiceProcessInstaller();
            m_serviceInstaller = new ServiceInstaller();

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            m_serviceInstaller.DisplayName = "LeadCentral Sync";
            m_serviceInstaller.StartType = ServiceStartMode.Automatic;

            m_serviceInstaller.ServiceName = "LeadCentral Sync";

            Installers.Add(serviceProcessInstaller);
            Installers.Add(m_serviceInstaller);            
        }
    }
}
