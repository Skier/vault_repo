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
	public class IlecInfo : BasePage
	{
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.Label lblZipcode;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.RadioButtonList RadioButtonList1;
	#endregion

	#region Event Handlers

		void Page_Load(object sender, System.EventArgs ea)
		{			
			try
			{
				Pre_Load();

				if(IsPostBack)
					return;

				CustomInit();
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
				lblErrMsg.Text = Const.GENERAL_ERROR;
				lblErrMsg.Visible = true;
			}
		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["SelectedIlec"] = ((IILECInfo[])wipper.Wip["AvaIlecs"])[RadioButtonList1.SelectedIndex];
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				lblErrMsg.Text = Const.GENERAL_ERROR;
				lblErrMsg.Visible = true;
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["SelectedIlec"] = null;
				wipper.Wip["AvaIlecs"]= null;
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
				lblErrMsg.Text = Const.GENERAL_ERROR;
				lblErrMsg.Visible = true;
			}
		}
	#endregion

	#region Implementation
		void Pre_Load()
		{	
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
			
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);

		}
		void ValidateIlecs()
		{
			if(((IILECInfo[])wipper.Wip["avaIlecs"]).Length == 0)
			{	
				lblErrMsg.Text = OrgSvc.ZipNotFound(wipper.IMap, (string)wipper.Wip["Zip"]);
				lblErrMsg.Visible = true;
				return;
			}

			if(((IILECInfo[])wipper.Wip["avaIlecs"]).Length == 1) // Only 1 ilec to choose from
			{
				wipper.Wip["SelectedIlec"] = ((IILECInfo[])wipper.Wip["avaIlecs"])[0];  
				wipper.Wip["AvaIlecs"] = null;
				Response.Redirect(wipper.Wip.Next(), false);
			}	
				
			RadioButtonList1.DataSource = wipper.Wip["avaIlecs"];
			RadioButtonList1.DataBind();

			for( int i = 0; i < RadioButtonList1.Items.Count; i++)
				RadioButtonList1.Items[i].Selected = ((IILECInfo[])wipper.Wip["avaIlecs"])[i].IsDefault;
		}
		void GetIlecs()
		{
			wipper.Wip["avaIlecs"] = OrgSvc.GetILECsAtZip(wipper.IMap, (string)wipper.Wip["Zip"]);  
		}
		void CustomInit()
		{	
			lblZipcode.Text = (string)wipper.Wip["zip"];
			GetIlecs();
			ValidateIlecs();
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
		}
	#endregion
	}
}