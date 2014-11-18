using System;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering
{
	public class BasePage : System.Web.UI.Page
	{
		protected internal Wipper wipper; //do not remove internal or protected

		protected void OnInit(EventArgs ea, bool underWF)
		{
//			if (Session["IMap"] == null)
//				timeOut();
			
			if (Session["Start"] == null)
				Session["Start"] = DateTime.Today;

			if (((DateTime)Session["Start"]).ToShortDateString() != DateTime.Today.ToShortDateString())
				timeOut();
			
			wipper = new Wipper(this, underWF);
		}
		protected override void OnInit(EventArgs e)
		{  
			base.OnInit(e);
			OnInit(e, true);
		}
		void timeOut()
		{
			ErrLogSvc.LogError(null, this.ToString(), ((IUser)Session["User"]).ClerkId, 
				"Timeout had occurred. Session started: " + ((DateTime)Session["Start"]).ToLongDateString());  

			Session.Abandon();
			Response.Redirect("TimeOut.aspx");
		}
	}
}