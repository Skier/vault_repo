using System;
using System.Web.UI;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;

using DPI.Components;
using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering
{
	public class Wipper
	{
		protected IMap imap;
		protected WIP wip;
		protected Page parent; 
		
		/*		Properties		*/
		public IMap IMap		{ get { return imap; }}
		public WIP Wip			
		{ 
			get { return wip; }
			set 
			{ 
				imap.remove(WIP.IKeyS);
				wip = value; 
				imap.add(wip);
			}
		}
		public int WFSteps		{ get { return wip.StepCount; }}
		public int WFStep		{ get { return wip.StepNumber; }}
		public string Workflow	{ get { return wip.Workflow.Name; }}

		/*		Constructors		*/

		public Wipper(Page parent, bool underWF)
		{
			this.parent = parent;

			imap = (IMap)parent.Session["IMap"];
			
			if (imap != null)
			{
				wip = (WIP)imap.find(WIP.IKeyS);
				if (underWF)
					Checkout();
			}
		}
		/*		Methods		*/
		public void NewMap()
		{
			imap = IMapFactory.getIMap();
			parent.Session["IMap"]= imap;
		}
		void Clearout()
		{
			wip  = null;
			imap = null;
			parent.Session["IMap"]= null;
		}
		void Checkout()
		{
			string url = 
				parent.Request.Url.AbsolutePath.Substring(parent.Request.Url.AbsolutePath.LastIndexOf(@"/") + 1);

			if (wip == null)
			{
				ErrLogSvc.LogError(null, 
								GetOwner(), 
								HttpContext.Current.User.Identity.Name, 
								"Wip is null. Workflow terminated");  
				
				Clearout();	
				parent.Response.Redirect(Const.SIGN_OUT_PAGE);
			}

			if (imap == null)
			{
				ErrLogSvc.LogError(null, 
								GetOwner(),
								HttpContext.Current.User.Identity.Name, 
								"IMap is null. Workflow terminated");  
				
				parent.Response.Redirect(Const.SIGN_OUT_PAGE, false);
			}

			if (wip.Current() != url)
			{
				DPI_Err_Log.AddLogEntry(ErrLogSubSystems.WQSpinner.ToString(), HttpContext.Current.User.Identity.Name, 
								"Warning: possible out of sequence transfer from '"
								+  wip.Current() + "' to '" + url + "'. Redirected to " + wip.Current());  

				parent.Response.Redirect(wip.Current());
			}
		}
		string GetOwner()
		{
			return parent.ToString() + " : " + this.ToString();
		}
	}
}