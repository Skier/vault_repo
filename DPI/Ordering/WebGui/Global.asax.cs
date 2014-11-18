using System;
using System.Web;
using System.Web.Security;
using System.Security.Principal;

using DPI.Interfaces;
using DPI.ClientComp;
using DPI.Components;
using DPI.Services;


namespace DPI.Ordering 
{
	public class Global : System.Web.HttpApplication
	{
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		protected void Application_Start(Object sender, EventArgs ea)
		{
			OperSvc.StartThreads("Global.Application_Start");
			OperSvc.RefreshData("Global.Application_Start");
			SvcFactory.Start();
			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Global.ToString(), "", "Application_Started");
		}
		protected void Session_Start(Object sender, EventArgs e)
		{
			Session.Timeout = 1440;
			Session["Start"] = DateTime.Today;
		}
		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
		}
		protected void Application_EndRequest(Object sender, EventArgs e)
		{
		}
		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			if (HttpContext.Current.User != null)
				if (HttpContext.Current.User.Identity.IsAuthenticated)
					if (HttpContext.Current.User.Identity is FormsIdentity)
						HttpContext.Current.User = 
							new GenericPrincipal((FormsIdentity)HttpContext.Current.User.Identity,
							new Roles(((FormsIdentity)HttpContext.Current.User.Identity)
							.Ticket.UserData).GetRoles());
		}
		protected void Application_Error(Object sender, EventArgs e)
		{
//			if (Session["User"] == null)
//			{
//				Context.ClearError();
//				Response.Redirect("Timeout.aspx", false);
//			}
//			else
//			{
//				Context.ClearError();
//				Response.Redirect("../GenericError.aspx");		
//			}
		}
		protected void Session_End(Object sender, EventArgs e)
		{
			object o = Session["Start"];
		}
		protected void Application_End(Object sender, EventArgs e)
		{
			OperSvc.DisposeEazyTax();
			OperSvc.StopThreads("Global.Application_End");
			ProdSvc.TaxWrapperDispose();
			DPI_Err_Log.AddLogEntry(ErrLogSubSystems.Global.ToString(), "", "Application_Ended");
		}
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
