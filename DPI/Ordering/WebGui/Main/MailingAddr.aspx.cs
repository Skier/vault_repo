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
	public class MailingAddr : BasePage
	{
	#region Data
		protected System.Web.UI.WebControls.TextBox txtStreetNum;
		protected System.Web.UI.WebControls.TextBox txtStreetName;
		protected System.Web.UI.WebControls.TextBox TxtUnit;
		protected System.Web.UI.WebControls.TextBox TxtCity;
		protected System.Web.UI.WebControls.TextBox TxtZip;
		protected System.Web.UI.WebControls.DropDownList txtState;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.DropDownList ddlStreetType;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator2;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator3;
		protected System.Web.UI.WebControls.Image Image9;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldState;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Image Image2;
		protected System.Web.UI.WebControls.Image Image3;
		protected System.Web.UI.WebControls.Image Image4;
		protected System.Web.UI.WebControls.Image Image5;
		protected System.Web.UI.WebControls.DropDownList ddlUnitType;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.DropDownList ddlSfx;
		protected System.Web.UI.WebControls.DropDownList txtStreetPfx;
		protected System.Web.UI.WebControls.Label lblFirstName;
		protected System.Web.UI.WebControls.Label lblLastName;
		protected System.Web.UI.WebControls.Label lblBDay;
		protected System.Web.UI.WebControls.Label lblEmail;
		protected System.Web.UI.WebControls.Label lblContact;
		protected System.Web.UI.WebControls.Label lblContact2;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.RegularExpressionValidator vldCityAlpha;
	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			//		CustomInit();
		}
		
		private void InitializeComponent()
		{  
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion	

	#region Event Handlers
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

		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if(!IsValid)
					return;

				SaveServAddr();
				
				if (!ValidateNext())
					return;

				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Response.Redirect(wipper.Wip.Prev(), false);
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
		void CustomInit()
		{
			btnNext.Visible = wipper.Wip.HasNext; 
			btnPrevious.Visible = wipper.Wip.HasPrev;

			if (wipper.Wip["MailAddr"] == null)
				wipper.Wip["MailAddr"] = CustFactory.GetAddress(wipper.IMap);

			LoadCustInfo(); 
			LoadMailAddr();
			LoadStreetTypes();
			
			lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
			lblZipCode.Text = (string)wipper.Wip["zip"];
		}
		void LoadCustInfo()
		{
			lblFirstName.Text =	((ICustInfo)wipper.Wip["CustInfo"]).FirstName;		
			lblLastName.Text =	((ICustInfo)wipper.Wip["CustInfo"]).LastName;

			lblBDay.Text = Birthday.Show(((ICustInfo)wipper.Wip["CustInfo"]).Birthday);
		
			lblEmail.Text =	((ICustInfo)wipper.Wip["CustInfo"]).Email;
			lblContact.Text = ((ICustInfo)wipper.Wip["CustInfo"]).Contact;  
			lblContact2.Text =	((ICustInfo)wipper.Wip["CustInfo"]).Contact2;  
		}
		void LoadMailAddr()
		{
			txtStreetNum.Text					=	((IAddr2)wipper.Wip["MailAddr"]).StreetNum ;    
			txtStreetName.Text					=	((IAddr2)wipper.Wip["MailAddr"]).Street	 ;  
			txtStreetPfx.SelectedValue			=	((IAddr2)wipper.Wip["MailAddr"]).StreetPrefix;
			ddlSfx.SelectedValue				=	((IAddr2)wipper.Wip["MailAddr"]).StreetSuffix;
			ddlStreetType.SelectedValue			=	((IAddr2)wipper.Wip["MailAddr"]).StreetType  ;
			TxtUnit.Text						=	((IAddr2)wipper.Wip["MailAddr"]).Unit     ;   
			ddlUnitType.SelectedValue 			=	((IAddr2)wipper.Wip["MailAddr"]).UnitType  ;  
			TxtCity.Text						=	((IAddr2)wipper.Wip["MailAddr"]).City       ; 
			txtState.SelectedValue				=	((IAddr2)wipper.Wip["MailAddr"]).State       ;
			TxtZip.Text							=	((IAddr2)wipper.Wip["MailAddr"]).Zipcode       ;
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

		void SaveServAddr()
		{
			((IAddr2)wipper.Wip["MailAddr"]).StreetNum    = txtStreetNum.Text; 
			((IAddr2)wipper.Wip["MailAddr"]).Street	     = txtStreetName.Text;
			((IAddr2)wipper.Wip["MailAddr"]).StreetPrefix = txtStreetPfx.SelectedValue;
			((IAddr2)wipper.Wip["MailAddr"]).StreetSuffix = ddlSfx.SelectedValue;
			((IAddr2)wipper.Wip["MailAddr"]).StreetType   = ddlStreetType.SelectedValue;
			((IAddr2)wipper.Wip["MailAddr"]).Unit         = TxtUnit.Text;
			((IAddr2)wipper.Wip["MailAddr"]).UnitType     = ddlUnitType.SelectedValue ;
			((IAddr2)wipper.Wip["MailAddr"]).City         = TxtCity.Text;
			((IAddr2)wipper.Wip["MailAddr"]).State        = txtState.SelectedValue.ToString();
			((IAddr2)wipper.Wip["MailAddr"]).Zipcode      = TxtZip.Text;
		}

		bool ValidateNext()
		{
			if(!IsValid)
				return false;
			
			if((ddlUnitType.SelectedIndex != 0) && (TxtUnit.Text == ""))
			{
				lblError.Visible = true;
				lblError.Text = "Please enter a unit number if you have a unit type.";
				return false;
			}
			return true;
		}
		void ErrorHandler(Exception ex)
		{
			ErrLogSvc.LogError(
				wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, ex.Message + ", " + ex.StackTrace);

			btnNext.Visible = false;
			lblError.Visible = true;
			lblError.Text = Const.GENERAL_ERROR;
		}
	#endregion
	}
}