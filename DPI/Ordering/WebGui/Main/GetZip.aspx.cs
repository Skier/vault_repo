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
	public class NOGetZip : BasePage
	{
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{    
			this.chkConversion.CheckedChanged += new System.EventHandler(this.chkConversion_CheckedChanged);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.TextBox txtSalesId;
		protected System.Web.UI.WebControls.TextBox txtZipcode;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.TextBox txtNpa;
		protected System.Web.UI.WebControls.TextBox txtNxx;
		protected System.Web.UI.WebControls.TextBox txtNumber;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator4;
		protected System.Web.UI.WebControls.CheckBox chkConversion;
		protected System.Web.UI.WebControls.Label lblCurSvcPh;
		protected System.Web.UI.WebControls.DropDownList ddlSource;
		protected System.Web.UI.WebControls.Label Label1;
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
		void chkConversion_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				lblCurSvcPh.Visible = txtNpa.Visible = txtNxx.Visible = txtNumber.Visible = chkConversion.Checked;

				bool isAllowedLocalConv = (bool)wipper.Wip["AllowLocalConv"];
				
				if (!chkConversion.Checked)
				{
					wipper.Wip = new NewOrderWip((IUser)Session["User"]);
					wipper.Wip["AllowLocalConv"] = isAllowedLocalConv;
					return;
				}

				wipper.Wip = new NewOrderConversionWip((IUser)Session["User"]);
				wipper.Wip["AllowLocalConv"] = isAllowedLocalConv;
				wipper.Wip["Criteria"] = "Local Conversion";
				wipper.Wip["AcctNotes"]  = CustSvc.GetNotes(wipper.IMap, ((IUser)Session["User"]).ClerkId, 
					"Conversion Account Phone #: ");
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			lblErrMsg.Visible = false;

			try
			{
				if (!Page.IsValid)
					return;

				if (!(ValidateSalesId()))
						return;
				
				if (!ValidatePhone())
					return;

				if (!ValidateZip())
					return;

				DoAcctNotes();

				SetSource(); //Not required as per Don 

				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}	

		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				wipper.Wip["Zip"] = null;
				wipper.Wip["AcctNotes"] = null;
				wipper.Wip["PhNumber"] = null;
				Response.Redirect(wipper.Wip.Prev(), false);
			}
			catch {}
		}
		void ddlSource_SelectionChanged(object sender, System.EventArgs e)
				{
					try
					{
						wipper.Wip["Source"] = int.Parse(((DropDownList)sender).SelectedValue);
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
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);			
		}
		void SetConvCriteria()
		{
			wipper.Wip["AllowLocalConv"] = StoreSvc.IsAllowedLocalConv(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode);
		}
		void CustomInit()
		{
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
	
			string label = wipper.Workflow;
			imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
			SetConvCriteria();
			LoadSources();			
		}
		
		bool ValidateZip()
		{
			if (txtZipcode.Text.Trim().Length == 0)
			{
				lblErrMsg.Text = "Please enter zip";
				lblErrMsg.Visible = true;
				return false;
			}

			IILECInfo[] ilecs = OrgSvc.GetILECsAtZip(wipper.IMap, txtZipcode.Text);  
					
			if(ilecs.Length > 0)
			{
				wipper.Wip["Zip"] = txtZipcode.Text;
				return true;
			}

			lblErrMsg.Text = OrgSvc.ZipNotFound(wipper.IMap, txtZipcode.Text);
			lblErrMsg.Visible = true;
			return false;
		}

		bool ValidateSalesId()
		{
			if (!Sales.SalesIdReq((IUser)Session["User"], wipper.Wip))
				return true;
			
			if (txtSalesId.Text.Trim().Length == 0)
			{
				lblErrMsg.Text = "Please enter Co-Worker Id";
				lblErrMsg.Visible = true;
				return false;
			}

			wipper.Wip.ClerkId = txtSalesId.Text.Trim();
			return true;
		}
		void ErrorHandler(Exception ex)
		{
			btnNext.Visible = false;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
		}
		void DoAcctNotes()
		{
			if (wipper.Wip["AcctNotes"] != null)
				((IAcctNotes)wipper.Wip["AcctNotes"]).Text += FormatPhone.Format((string)wipper.Wip["PhNumber"]);
		}
		void SetSource()
		{
			if (!StoreSvc.ShowSource(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode))
				return;
			
			if (ddlSource.SelectedIndex < 1)
				return;

			wipper.Wip["Source"] = int.Parse(ddlSource.SelectedItem.Value);
	}
		bool ValidatePhone()
		{
			if (!chkConversion.Checked)
				return true;

			if (ValAndSetPhoneFields())
				return true;

			lblErrMsg.Text = "Please enter Current Service Phone Number";
			return false;
		}

		bool ValAndSetPhoneFields()
		{
			if (txtNpa.Text.Trim().Length != 3)
				return false;
			
			if (txtNxx.Text.Trim().Length != 3)
				return false;

			if (txtNumber.Text.Trim().Length != 4)
				return false;
			
			wipper.Wip["PhNumber"] = txtNpa.Text + txtNxx.Text + txtNumber.Text;

			return true;
		}
		void LoadSources()
		{	
			if (!StoreSvc.ShowSource(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode))
				return;

			ISource[] sources = ProdSvc.GetSources(wipper.IMap);
			
			for (int i = 0; i < sources.Length; i++)
				ddlSource.Items.Add(new ListItem(sources[i].Name, sources[i].Id.ToString()));
		}

		
	}
	#endregion
}	