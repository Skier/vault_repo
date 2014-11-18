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
using DPI.Services;
using DPI.Ordering;
using DPI.Interfaces;

namespace DPI.Ordering
{
	public class NO_ServiceAddr : BasePage
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
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		/* customer info */
		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.TextBox txtBirthday;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.TextBox txtCnNum1;
		protected System.Web.UI.WebControls.TextBox txtCnNum2;
		protected System.Web.UI.WebControls.TextBox txtCnNum3;
		protected System.Web.UI.WebControls.TextBox txtCnNumSnd3;
		protected System.Web.UI.WebControls.TextBox txtCnNumSnd2;
		protected System.Web.UI.WebControls.TextBox txtCnNumSnd1;
		protected System.Web.UI.WebControls.TextBox txtPrevPhon1;
		protected System.Web.UI.WebControls.TextBox txtPrevPhon2;
		protected System.Web.UI.WebControls.TextBox txtPrevPhon3;
		protected System.Web.UI.WebControls.DropDownList ddlPhoneComp;

		/* service address */
		protected System.Web.UI.WebControls.TextBox txtStreetNum;		
		protected System.Web.UI.WebControls.DropDownList txtStreetPfx;
		protected System.Web.UI.WebControls.TextBox txtStreetName;
		protected System.Web.UI.WebControls.DropDownList ddlStreetType;
		protected System.Web.UI.WebControls.DropDownList ddlUnitType;
		protected System.Web.UI.WebControls.TextBox TxtUnit;
		protected System.Web.UI.WebControls.TextBox TxtCity;
		protected System.Web.UI.WebControls.Label TxtZip;

		/* labels */
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblLastname;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblBirthday;
		protected System.Web.UI.WebControls.Label lblAddyWarn;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.Label lblFirstName;

		/* images */
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.Image Image6;
		protected System.Web.UI.WebControls.Image Image5;
		protected System.Web.UI.WebControls.Image Image9;
		protected System.Web.UI.WebControls.Image Image4;

		/* web controls */
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.RequiredFieldValidator errFirstName;
		protected System.Web.UI.WebControls.RequiredFieldValidator errLastName;
		protected System.Web.UI.WebControls.RequiredFieldValidator errNum;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldStreet;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldStrtName;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldCity;
		protected System.Web.UI.WebControls.RegularExpressionValidator vldEmail;
		protected System.Web.UI.WebControls.CheckBox ckbxSame;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.DropDownList txtState;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.DropDownList ddlSfx;
		protected System.Web.UI.WebControls.RegularExpressionValidator vldCityAlpha;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator1;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator4;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator5;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator6;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator7;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator8;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator9;
		protected System.Web.UI.WebControls.CheckBox Checkbox1;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.ImageButton btnNext;

		#endregion
		void CustomInit()
		{
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
			SetupCustInfo();

			LoadStreetTypes();
			//EnableValidators(false);
		}
		void SetupCustInfo()
		{
			if (wipper.Wip["ServAddr"] == null)
			{
				IAddr sa = CustFactory.GetAddress(wipper.IMap);
				sa.Zipcode = (string)wipper.Wip["Zip"];
				wipper.Wip["ServAddr"] = sa; 
			}

			if (wipper.Wip["CustInfo"] == null)
			{
				ICustInfo2 ci =  CustFactory.GetCustInfo(wipper.IMap);

				if (wipper.Wip["Receipt"] != null)
					ci.AccNumber = ((IReceipt)wipper.Wip["Receipt"]).AccNumber;
				
				wipper.Wip["CustInfo"] = ci;
				((IDemand)wipper.Wip["Demand"]).Consumer = ci;
			}		
		}
		private void Page_Load(object sender, System.EventArgs e) 
		{
			if(!IsPostBack)
			{
				LoadCustInfo(); 
				LoadServAddr();
			}
			lblError.Visible = false;
			lblError.Text = "";

			lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
			lblZipCode.Text = (string)wipper.Wip["zip"];

			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnNext);
		}
		private void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{	
			if(!this.IsValid)
				return;

			if((ddlUnitType.SelectedIndex != 0) && (TxtUnit.Text == ""))
			{
				lblError.Visible = true;
				lblError.Text = "Please enter a unit number if you have a unit type.";
			}
			
			if((txtBirthday.Text.Length > 0) && (!Birthday.IsValid(txtBirthday.Text)))
			{
				lblError.Visible = true;
				lblError.Text = "Please enter a correct birthday format.";
			}
			else
			{
				if (!SaveCustInfo())
				{
					// set error message
					return;
				}

				if (!SaveServAddr())
				{
					// set error message
					return;
				}

				if (((IAddr)wipper.Wip["ServAddr"]).Zipcode.ToString() !=  wipper.Wip["zip"].ToString())
				{
					// display a pop-up advising that the order will have to start from GetSipCode
				
					//	Response.Redirect(wipper.Wip.Rework());
				}
				if (!Checkbox1.Checked)
				{
					Response.Redirect(wipper.Wip.Next(), false);
					return;
				}
				wipper.Wip["MailAddr"] = wipper.Wip["ServAddr"];  // mail address same as service address
				Response.Redirect(wipper.Wip.Skip(), false);			
			}
		}
		//validator
		void LoadCustInfo()
		{
			txtFirstName.Text =	((ICustInfo)wipper.Wip["CustInfo"]).FirstName;		
			txtLastName.Text =	((ICustInfo)wipper.Wip["CustInfo"]).LastName;	
			
			txtBirthday.Text = Birthday.Show(((ICustInfo)wipper.Wip["CustInfo"]).Birthday);
			txtEmail.Text =	((ICustInfo)wipper.Wip["CustInfo"]).Email;
			
			txtCnNum1.Text = FormatPhone.ShowPhone(PhoneNumParts.Npa, ((ICustInfo)wipper.Wip["CustInfo"]).Contact); 
			txtCnNum2.Text = FormatPhone.ShowPhone(PhoneNumParts.Nxx, ((ICustInfo)wipper.Wip["CustInfo"]).Contact);
			txtCnNum3.Text = FormatPhone.ShowPhone(PhoneNumParts.Line, ((ICustInfo)wipper.Wip["CustInfo"]).Contact); 
			if(((ICustInfo)wipper.Wip["CustInfo"]).Contact2 != null)
				if(((ICustInfo)wipper.Wip["CustInfo"]).Contact2.Trim().Length > 0)
				{
					txtCnNumSnd1.Text = FormatPhone.ShowPhone(PhoneNumParts.Npa, ((ICustInfo)wipper.Wip["CustInfo"]).Contact2); 
					txtCnNumSnd2.Text = FormatPhone.ShowPhone(PhoneNumParts.Nxx, ((ICustInfo)wipper.Wip["CustInfo"]).Contact2);
					txtCnNumSnd3.Text = FormatPhone.ShowPhone(PhoneNumParts.Line, ((ICustInfo)wipper.Wip["CustInfo"]).Contact2); 
				}	
		}

		void LoadServAddr()
		{	
			// Populate controls using values from wipper.Wip:
			if(((IAddr2)wipper.Wip["ServAddr"]).State == null)
				((IAddr2)wipper.Wip["ServAddr"]).State 
					= LocSvc.FindState(wipper.IMap, ((IAddr2)wipper.Wip["ServAddr"]).Zipcode);
			
			if(((IAddr2)wipper.Wip["ServAddr"]).State.Trim().Length == 0)
				((IAddr2)wipper.Wip["ServAddr"]).State 
					= LocSvc.FindState(wipper.IMap, ((IAddr2)wipper.Wip["ServAddr"]).Zipcode);

			txtState.SelectedValue = ((IAddr2)wipper.Wip["ServAddr"]).State ;
			txtStreetNum.Text		= ((IAddr2)wipper.Wip["ServAddr"]).StreetNum;
			txtStreetName.Text	= ((IAddr2)wipper.Wip["ServAddr"]).Street;
			txtStreetPfx.SelectedValue	= ((IAddr2)wipper.Wip["ServAddr"]).StreetPrefix;
			ddlSfx.SelectedValue = ((IAddr2)wipper.Wip["ServAddr"]).StreetSuffix;
			ddlStreetType.SelectedValue = ((IAddr2)wipper.Wip["ServAddr"]).StreetType;
			TxtUnit.Text		= ((IAddr2)wipper.Wip["ServAddr"]).Unit;
			ddlUnitType.SelectedValue = ((IAddr2)wipper.Wip["ServAddr"]).UnitType ;
			TxtCity.Text		= ((IAddr2)wipper.Wip["ServAddr"]).City; 
			TxtZip.Text			= ((IAddr2)wipper.Wip["ServAddr"]).Zipcode; 
		}
		void LoadStreetTypes()
		{			
			IAddress_StreetType[] addressStreetTypes = AddressSvc.GetStreetTypes(wipper.IMap);
			ddlStreetType.Items.Add(new ListItem("", ""));
			foreach (IAddress_StreetType addressStreetType in addressStreetTypes)
			{
				ddlStreetType.Items.Add(new ListItem(addressStreetType.StreetType, addressStreetType.StreetTypeAbbr));			
			}
		}
		bool SaveCustInfo()
		{
			if((ICustInfo)wipper.Wip["CustInfo"]== null)
				wipper.Wip["CustInfo"] = CustFactory.GetCustInfo(wipper.IMap);

			((ICustInfo)wipper.Wip["CustInfo"]).FirstName = txtFirstName.Text;
			((ICustInfo)wipper.Wip["CustInfo"]).LastName  = txtLastName.Text;
			((ICustInfo)wipper.Wip["CustInfo"]).Birthday  = Birthday.Store(txtBirthday.Text);
			((ICustInfo)wipper.Wip["CustInfo"]).Email     = txtEmail.Text;
			((ICustInfo)wipper.Wip["CustInfo"]).Contact   = string.Concat(txtCnNum1.Text, txtCnNum2.Text, txtCnNum3.Text);
			((ICustInfo)wipper.Wip["CustInfo"]).Contact2  = string.Concat(txtCnNumSnd1.Text, txtCnNumSnd2.Text, txtCnNumSnd3.Text);  
			((ICustInfo)wipper.Wip["CustInfo"]).PrevPhone = string.Concat(txtPrevPhon1.Text, txtPrevPhon2.Text, txtPrevPhon3.Text);
			((ICustInfo)wipper.Wip["CustInfo"]).PrevILEC  =	ddlPhoneComp.SelectedValue;
			if (wipper.Wip["PhNumber"] != null)
				((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber = (string)wipper.Wip["PhNumber"];
			
			return true;
		}
		bool SaveServAddr()
		{
			// Replace this hardcoded values with data from controls + validate them
			((IAddr2)wipper.Wip["ServAddr"]).StreetNum    = txtStreetNum.Text; 
			((IAddr2)wipper.Wip["ServAddr"]).Street       = txtStreetName.Text;
			((IAddr2)wipper.Wip["ServAddr"]).StreetPrefix = txtStreetPfx.SelectedValue;
			((IAddr2)wipper.Wip["ServAddr"]).StreetType   = ddlStreetType.SelectedValue;
			((IAddr2)wipper.Wip["ServAddr"]).StreetSuffix = ddlSfx.SelectedValue;
			((IAddr2)wipper.Wip["ServAddr"]).Unit         = TxtUnit.Text;
			((IAddr2)wipper.Wip["ServAddr"]).UnitType     = ddlUnitType.SelectedValue ;
			((IAddr2)wipper.Wip["ServAddr"]).City         = TxtCity.Text;
			((IAddr2)wipper.Wip["ServAddr"]).State         = txtState.SelectedValue.ToString();
			
			return true;
		}

		public void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(wipper.Wip.Prev(), false);		
		}


	}
}