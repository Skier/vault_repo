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

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering; 
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class RegFormResponse : BasePage
	{

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}

		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Label lblName;
		protected System.Web.UI.WebControls.Label lblNameTitle;
		protected System.Web.UI.WebControls.Label lblPhone;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.ImageButton btnPromo;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Label lblCityStZip;
		protected System.Web.UI.WebControls.Label lblAddr1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		#endregion
	
	#region  Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				Pre_Load();
				if (IsPostBack)
					return;

				CustomInit();
				

			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion

	#region Implementation
		void Pre_Load()
		{
		}
		void CustomInit()
		{
			btnPrint.Attributes.Add("onClick", "window.print()");
			ToPage();
		}

		void txtState_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		void ToPage()
		{
			SetLabels();
		}
		
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
		}
		void SetLabels()
		{
			lblName.Text = ((IAgentRegistration)wipper.Wip["AgentRegistration"]).FirstName 
				+ " " + ((IAgentRegistration)wipper.Wip["AgentRegistration"]).LastName;
			
			lblNameTitle.Text =	((IAgentRegistration)wipper.Wip["AgentRegistration"]).FirstName 
				+ " " + ((IAgentRegistration)wipper.Wip["AgentRegistration"]).LastName 
				+ ", " + GetTitle();
			
			lblAddr1.Text = ((IAddr2)wipper.Wip["Address"]).FormattedStreetAddress;
			lblCityStZip.Text = ((IAddr2)wipper.Wip["Address"]).FormattedCityStateZip;
			lblPhone.Text = ((IAgentRegistration)wipper.Wip["AgentRegistration"]).Phone;
			lblEmail.Text = ((IAgentRegistration)wipper.Wip["AgentRegistration"]).Email;
		}
		string GetTitle()
		{
			switch (((IAgentRegistration)wipper.Wip["AgentRegistration"]).Title)
			{
				case 0 :
					return "Sales Associate";

				case 1 :
					return "Store Manager";

				case 2 :
					return "Owner";
				
				default :
					return "Other";
			}
//			return ((AgentTitle)title).ToString();
		}
	#endregion

	}

}	