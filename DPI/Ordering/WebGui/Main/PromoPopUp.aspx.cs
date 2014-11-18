using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using DPI.ClientComp;
using DPI.Interfaces;
using DPI.Services;

namespace DPI.Ordering.Main
{
	public class PromoPopUp : System.Web.UI.Page
	{	
		
	#region Web Form Designer generated code

		#region Data
			protected System.Web.UI.WebControls.ImageButton imgBtnRegister;
		#endregion

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
				
		private void InitializeComponent()
		{    
			this.imgBtnRegister.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnRegister_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			CustomInit();

		}
	#endregion

	#region EventHandlers
		void Page_Load(object sender, System.EventArgs e)
		{
			imgBtnRegister.Attributes.Add("onclick", "gotoWin()");
		}
		void imgBtnRegister_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IMap imap = (IMap)Session["IMap"];
				WIP wip = (WIP)imap.find(WIP.IKeyS);

				Response.Redirect(wip.Current(), false);
			}
			catch {}
		}
		void CustomInit()
		{
			try
			{
				WIP wip = new PromoWip(((IUser)Session["User"]).DisplayName, 
					((IUser)Session["User"]).ClerkId, 
					((IUser)Session["User"]).LoginStoreCode);

				IMap imap = IMapFactory.getIMap();
				imap.add(wip);
				Session["IMap"] = imap;
			}
			catch {}
		}
	#endregion
	#region Implementations

	#endregion

	}
}
