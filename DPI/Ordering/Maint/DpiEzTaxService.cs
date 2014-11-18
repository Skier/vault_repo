using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration;



namespace DPI.Maint
{
	[RunInstaller(true)]
	public class RemAgentExporterService_Installer : 
		System.Configuration.Install.Installer
	{
		protected ServiceProcessInstaller processInstaller;
		protected ServiceInstaller installer;
		public RemAgentExporterService_Installer()
		{
			processInstaller = new ServiceProcessInstaller();
			processInstaller.Account = ServiceAccount.LocalSystem;

			installer = new ServiceInstaller();
			installer.ServiceName = "DpiEzTaxService";
			installer.DisplayName = "DpiEzTaxService";
			
			installer.StartType = ServiceStartMode.Automatic;

			Installers.Add(processInstaller);
			Installers.Add(installer);
		}
	}

	public class TaxService : System.ServiceProcess.ServiceBase
	{
		private System.ComponentModel.Container components = null;

		public TaxService()
		{
			InitializeComponent();
		}

		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new TaxService() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "DpiEzTaxService";		
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		protected override void OnStart(string[] args)
		{
			DpiEzTaxer.CreateChannel();
		}
		protected override void OnStop()
		{
			DpiEzTaxer.StopChannel();
		}
	}
}
