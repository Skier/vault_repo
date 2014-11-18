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

using DPI.Interfaces;
using DPI.Services;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class PromoRegForm : BasePage
	{
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		private void InitializeComponent()
		{    
			this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label9;
		protected System.Web.UI.WebControls.Label Label10;
		protected System.Web.UI.WebControls.Label Label11;
		protected System.Web.UI.WebControls.Label Label12;
		protected System.Web.UI.WebControls.Label Label13;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.DropDownList ddlTitle;
		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.TextBox txtContact;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.TextBox txtAddr1;
		protected System.Web.UI.WebControls.TextBox txtCity;
		protected System.Web.UI.WebControls.CheckBox chkAgree;
		protected System.Web.UI.WebControls.ImageButton btnSubmit;
		protected System.Web.UI.WebControls.ImageButton btnCancel;
		protected System.Web.UI.WebControls.TextBox TxtZipCode;
		protected System.Web.UI.WebControls.TextBox txtAddr2;
		protected System.Web.UI.WebControls.DropDownList ddlState;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.Label lblFNameErr;
		protected System.Web.UI.WebControls.Label lblLNameErr;
		protected System.Web.UI.WebControls.Label lblContactErr;
		protected System.Web.UI.WebControls.Label lblCityErr;
		protected System.Web.UI.WebControls.Label lblStateErr;
		protected System.Web.UI.WebControls.Label lblZipcodeErr;
		protected System.Web.UI.WebControls.Label lblCheckErr;
		protected System.Web.UI.WebControls.Label lblAddressErr;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		#endregion
	
	#region  Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (IsPostBack)
					return;				

				// state DDLs
				ddlState.DataSource = DropDownListDate.GetStatesShort(true);
				ddlState.DataTextField = "DDLValue";
				ddlState.DataValueField  = "DDLValue";
				ddlState.DataBind();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}		
		}	
		void CustomInit()
		{
			try
			{	
				if (wipper == null)
					throw new ApplicationException("Wipper is null");

				if (wipper.Wip == null)
					throw new ApplicationException("Wip is missing");

				CheckAddress();
				CheckRegistration();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{	
			try
			{			
				if (!ValidateInput())				
					return;

				ToBusObj();
				AgentSvc.SaveRegistration(wipper.IMap);
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion

	#region Implementation
		void CheckRegistration()
		{	
			if (wipper.Wip["AgentRegistration"] != null)
				return;

			wipper.Wip["AgentRegistration"] = AgentSvc.GetNewAgentReg(wipper.IMap);
		}
		void CheckAddress()
		{
			if (wipper.Wip["Address"] != null)
				return;

			wipper.Wip["Address"] =  CustFactory.GetAddress(wipper.IMap);
		}
		void ToBusObj()
		{
			SetAddr();
			SetRegi();
		}
		void SetAddr()
		{
			if (wipper.Wip["Address"] == null)
				throw new ArgumentNullException("Address");
            
			IAddr2 addr = (IAddr2)wipper.Wip["Address"];

			addr.City = txtCity.Text.Trim();
			addr.State = ddlState.SelectedValue;
			addr.Zipcode = TxtZipCode.Text.Trim();
			
			addr.Street = txtAddr1.Text.Trim();

			if (txtAddr2.Text.Trim().Length > 0)
				addr.StreetSuffix = txtAddr2.Text.Trim();
		}
		void SetRegi()
		{
			if (wipper.Wip["AgentRegistration"] == null)
				throw new ArgumentNullException("AgentRegistration");
			
			IAgentRegistration reg = (IAgentRegistration)wipper.Wip["AgentRegistration"];
			
			if (wipper.Wip["Address"] == null)
				throw new ArgumentNullException("Address");

			reg.Address = (IAddr2)wipper.Wip["Address"]; // link with address

			reg.StoreCode = ((IUser)Session["User"]).LoginStoreCode;
			reg.CorpId = StoreSvc.GetCorporation(reg.StoreCode).CorpID;
			reg.RegType = "WebCentral";
			reg.Status = "Active";

			reg.RegDate = DateTime.Now;
			reg.EffStartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
			reg.EffEnddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

			reg.IsAgreed = chkAgree.Checked;		
			reg.Title = ddlTitle.SelectedIndex;
			reg.FirstName = txtFirstName.Text.Trim();
			reg.LastName = txtLastName.Text.Trim();
			
			reg.Phone = txtContact.Text.Trim();

			if (txtEmail.Text.Trim().Length > 0)
				reg.Email = txtEmail.Text.Trim();
		}

		bool ValidateInput()
		{
			ClearAllError();

			if (! Validator.ReqField(txtFirstName.Text))
			{
				lblErrMsg.Text = "First Name is required";
				lblFNameErr.Visible = true;
				return false;
			}
			if (! Validator.ReqField(txtLastName.Text))
			{
				lblErrMsg.Text = "Last Name is required";
				lblLNameErr.Visible = true;
				return false;
			}
			if (! Validator.ReqField(txtContact.Text))
			{
				lblErrMsg.Text = "Contact phone number is required";
				lblContactErr.Visible = true;
				return false;
			}

			if (! Validator.ReqField(txtAddr1.Text))
			{
				lblErrMsg.Text = "Address is required";
				lblAddressErr.Visible = true;
				return false;
			}

			if (! Validator.ReqField(txtCity.Text))
			{
				lblErrMsg.Text = "City is required";
				lblCityErr.Visible = true;
				return false;
			}

			if (ddlState.SelectedIndex == 0) // no selection made
			{
				lblErrMsg.Text = "Please select State";
				lblStateErr.Visible = true;
				return false;
			}
			if (! Validator.ReqField(TxtZipCode.Text))
			{
				lblErrMsg.Text = "Zipcode is required";
				lblZipcodeErr.Visible = true;
				return false;
			}
			if (! Validator.ReqField(txtAddr1.Text))
			{
				lblErrMsg.Text = "Address is required";
				lblAddressErr.Visible = true;
				return false;
			}
			if (!chkAgree.Checked)
			{
				lblErrMsg.Text = "Please check you have Read and Agree to Contest Terms and Conditions";
				lblCheckErr.Visible = true;
				return false;
			}
			return true;
		}
		void ClearAllError()
		{
			lblErrMsg.Text = "";
	
			lblFNameErr.Visible   = 
			lblLNameErr.Visible   = 
			lblContactErr.Visible = 
			lblCityErr.Visible    = 
			lblStateErr.Visible   = 
			lblZipcodeErr.Visible = 
			lblAddressErr.Visible = 
			lblCheckErr.Visible   = false;
		}
		void ErrorHandler(Exception ex)
		{	
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
		}
	}
	#endregion
}	