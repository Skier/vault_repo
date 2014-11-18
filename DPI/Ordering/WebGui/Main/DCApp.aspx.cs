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
using System.Text.RegularExpressions;

using DPI.Services;
using DPI.Interfaces;
using DPI.Ordering;
using DPI.ClientComp;

namespace DPI.Ordering.Main
{
	public class DCApp : BasePage
	{
	#region Data
		protected System.Web.UI.WebControls.Image 							imgWorkflow;
		protected System.Web.UI.WebControls.Image 							Image1;
		protected System.Web.UI.WebControls.Image 							Image2;
		protected System.Web.UI.WebControls.Image 							Image3;
		protected System.Web.UI.WebControls.Image 							Image4;
		protected System.Web.UI.WebControls.Image 							Image5;
		protected System.Web.UI.WebControls.Image 							Image6;
		protected System.Web.UI.WebControls.Image 							Image7;
		protected System.Web.UI.WebControls.Image 							Image8;
		protected System.Web.UI.WebControls.Image 							Image9;
		protected System.Web.UI.WebControls.Image 							Image16;
		protected System.Web.UI.WebControls.Label 							Label1;
		protected System.Web.UI.WebControls.Label 							Label2;
		protected System.Web.UI.WebControls.Label 							Label3;
		protected System.Web.UI.WebControls.Label 							Label10;
		protected System.Web.UI.WebControls.Label 							Label12;
		protected System.Web.UI.WebControls.Label 							Label13;
		protected System.Web.UI.WebControls.Label 							Label14;
		protected System.Web.UI.WebControls.Label 							Label15;
		protected System.Web.UI.WebControls.Label 							Label16;
		protected System.Web.UI.WebControls.Label 							Label11;
		protected System.Web.UI.WebControls.Label 							Label17;
		protected System.Web.UI.WebControls.Label 							Label18;
		protected System.Web.UI.WebControls.Label 							Label19;
		protected System.Web.UI.WebControls.RequiredFieldValidator			RequiredFieldValidator9;
		protected System.Web.UI.WebControls.RequiredFieldValidator			RequiredFieldValidator10;
		protected System.Web.UI.WebControls.RequiredFieldValidator			RequiredFieldValidator12;
		protected System.Web.UI.WebControls.RequiredFieldValidator			RequiredFieldValidator13;
		protected System.Web.UI.WebControls.RequiredFieldValidator			RequiredFieldValidator14;
		protected System.Web.UI.WebControls.RequiredFieldValidator			Requiredfieldvalidator16;
		protected System.Web.UI.WebControls.RequiredFieldValidator			vldStreet;
		protected System.Web.UI.WebControls.RegularExpressionValidator		RegularExpressionValidator1;
		protected System.Web.UI.WebControls.RegularExpressionValidator		RegularExpressionValidator3;
		protected System.Web.UI.WebControls.ValidationSummary				ValidationSummary1;
		protected System.Web.UI.WebControls.DropDownList					ddlState;
		protected System.Web.UI.WebControls.TextBox							TxtFirstName;
		protected System.Web.UI.WebControls.TextBox							TxtLastName;
		protected System.Web.UI.WebControls.TextBox							TxtCity;
		protected System.Web.UI.WebControls.TextBox							TxtStreetName;
		protected System.Web.UI.WebControls.DropDownList					ddlStreetType;
		protected System.Web.UI.WebControls.DropDownList					txtStreetPfx;
		protected System.Web.UI.WebControls.TextBox							TxtStreetNumber;
		protected System.Web.UI.WebControls.TextBox							txtAccNo4;
		protected System.Web.UI.WebControls.TextBox							txtAccNo3;
		protected System.Web.UI.WebControls.TextBox							txtAccNo2;
		protected System.Web.UI.WebControls.TextBox							txtAccNo1;
		protected System.Web.UI.WebControls.RegularExpressionValidator 		Regularexpressionvalidator4;
		protected System.Web.UI.WebControls.RegularExpressionValidator 		RegularExpressionValidator2;
		protected System.Web.UI.WebControls.RegularExpressionValidator 		Regularexpressionvalidator5;
		protected System.Web.UI.WebControls.RequiredFieldValidator			errNum;
		protected System.Web.UI.WebControls.TextBox							txtCnNum3;
		protected System.Web.UI.WebControls.TextBox							txtCnNum2;
		protected System.Web.UI.WebControls.TextBox							txtCnNum1;
		protected System.Web.UI.WebControls.Label							Label20;
		protected System.Web.UI.WebControls.TextBox							txtEmail;
		protected System.Web.UI.WebControls.RegularExpressionValidator		RegularExpressionValidator6;
		protected System.Web.UI.WebControls.RequiredFieldValidator 			RequiredFieldValidator2;
		protected System.Web.UI.WebControls.RequiredFieldValidator 			RequiredFieldValidator8;
		protected System.Web.UI.WebControls.RequiredFieldValidator 			RequiredFieldValidator11;
		protected System.Web.UI.WebControls.RequiredFieldValidator 			RequiredFieldValidator15;
		protected System.Web.UI.WebControls.Label 							Label5;
		protected System.Web.UI.WebControls.Image 							Image10;
		protected System.Web.UI.WebControls.TextBox 						txtSSN1;
		protected System.Web.UI.WebControls.TextBox 						txtSSN2;
		protected System.Web.UI.WebControls.TextBox 						txtSSN3;
		protected System.Web.UI.WebControls.RequiredFieldValidator			RequiredFieldValidator3;
		protected System.Web.UI.WebControls.RegularExpressionValidator 		RegularExpressionValidator7;
		protected System.Web.UI.WebControls.RegularExpressionValidator 		RegularExpressionValidator8;
		protected System.Web.UI.WebControls.RegularExpressionValidator 		RegularExpressionValidator9;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Image Image11;
		protected System.Web.UI.WebControls.DropDownList ddlDOBMM;
		protected System.Web.UI.WebControls.DropDownList ddlDOBDD;
		protected System.Web.UI.WebControls.DropDownList ddlDOBYY;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator4;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator20;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary2;
		protected System.Web.UI.WebControls.PlaceHolder phError;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.TextBox							TxtZipCode;				

		#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		//	CustomInit();
		}

		private void InitializeComponent()
		{    
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion	
	
	#region Event Handlers	
		void CustomInit()
		{
			try
			{
				imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
				btnNext.Visible = btnPrevious.Visible = false;

				if (wipper.Wip.HasNext)
				{
					btnNext.Visible = true;
					btnNext.Attributes.Add("onClick","clickedButton=true; ");
					MultiClickBlocker.Block(this, btnNext);
				}

				if (wipper.Wip.HasPrev)
				{
					btnPrevious.Visible = true;
					btnPrevious.Attributes.Add("onClick","clickedButton=true; ");
					MultiClickBlocker.Block(this, btnPrevious);
				}
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}

		void Page_Load(object sender, System.EventArgs e)
		{	
			Session.Timeout = 20;		
			
			if (IsPostBack)
				return;
	
			CustomInit();	
			
			try
			{	

				// DOB
				ddlDOBMM.DataSource = DropDownListDate.GetMonthsLong();
				ddlDOBMM.DataTextField = "DDLText";
				ddlDOBMM.DataValueField  = "DDLValue";
				ddlDOBMM.DataBind();

				ddlDOBDD.DataSource = DropDownListDate.GetDays(true);	
				ddlDOBDD.DataTextField = "DDLText";
				ddlDOBDD.DataValueField  = "DDLValue";
				ddlDOBDD.DataBind();

				ddlDOBYY.DataSource = DropDownListDate.GetBirthYears(true, 18);
				ddlDOBYY.DataTextField = "DDLText";
				ddlDOBYY.DataValueField  = "DDLValue";
				ddlDOBYY.DataBind(); 

				// state DDLs
				ddlState.DataSource = DropDownListDate.GetStatesShort(true);
				ddlState.DataTextField = "DDLText";
				ddlState.DataValueField  = "DDLValue";
				ddlState.DataBind();

				LoadStreetTypes();		
				GetCustInfo();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				if(!this.IsValid)
					return;

				ToBusObj();

				if (!ValidateData())
					return;

				//	ToPage();
				DebCardSvc.PreSave(wipper.IMap);
				Continue();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}

		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{	
				Destroyer.Destroy(((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr);
				((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr = null;
				
				Destroyer.Destroy(wipper.Wip["CustInfo"]);
				wipper.Wip["CustInfo"] = null;
			}
			catch {}

			Response.Redirect(wipper.Wip.Prev(), false);
		}
	#endregion 

	#region Implementation
		void Continue()
		{			
			IWireless_Transactions tran = null;
			if (wipper.Wip["Tran"] != null)
				tran = (IWireless_Transactions)wipper.Wip["Tran"];

			wipper.Wip["receipt"]  = DebCardSvc.NewCard (wipper.IMap, 
				(IPayInfo)wipper.Wip["PayInfo"],  
				(ICardApp)wipper.Wip["DebCardApp"],
				(IUser)HttpContext.Current.Session["User"],
				tran
				);
				
			Response.Redirect(wipper.Wip.Next(), false);
			
		}
		void RefreshData()
		{
			ToBusObj();
			ValidateData();
			ToPage();
		}

		void ToBusObj()
		{
			SaveCustInfo();
			((ICardApp)wipper.Wip["DebCardApp"]).CardNum 
				= txtAccNo1.Text + txtAccNo2.Text + txtAccNo3.Text + txtAccNo4.Text;
		}
		void ToPage()
		{
			LoadCustInfo();
		}
		bool ValidateData()
		{		
			phError.Visible = false;
			ArrayList ar = new ArrayList();

			if(CheckBday(ar))
				return true;

			if (ar.Count == 0)
				return true;

			phError.Controls.Add(new ErrorTable(ar));
			phError.Visible = true;
			return false;
		}			
		bool CheckBday(ArrayList ar)
		{
			try
			{
				DateTime strBDay = new DateTime(int.Parse(ddlDOBYY.SelectedValue.ToString()), int.Parse(ddlDOBMM.SelectedValue.ToString()), int.Parse(ddlDOBDD.SelectedValue.ToString()) );	

				int age = 18;
				// Hardcoded policy 
				if (ddlState.SelectedValue.ToUpper().Trim() == "AL")
					age = 19;

				if (ddlState.SelectedValue.ToUpper().Trim() == "NE")
					age = 19;

				DateTime theshhold = new DateTime(DateTime.Now.Year - age, DateTime.Now.Month, DateTime.Now.Day);
				if (theshhold < strBDay)
				{
					ar.Add("Customer must be " + age.ToString() + " years of age or older.");
					return false;
				}
			}
			catch
			{
				ar.Add("Invalid Date Of Birth");
			}
			return true;
		}
		void SaveCustInfo()
		{
			if (wipper.Wip["CustInfo"] == null)
				return;

			SaveName();
			SaveDOB();
			SavePhone();
			SaveSSN();
			SaverAddr();
		}
		void SavePhone()
		{
			string phone = txtCnNum1.Text.Trim() + txtCnNum2.Text.Trim() + txtCnNum3.Text.Trim();

			if (phone.Length != 10)
				return;

			((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber = phone;
		}
		void SaveSSN()
		{
			string ssn = txtSSN1.Text.Trim() + txtSSN2.Text.Trim() + txtSSN3.Text.Trim();	
			if (ssn.Length != 9)
				return;
		
			((ICustInfo2)wipper.Wip["CustInfo"]).Ssn = ssn;
		}
		void SaveName()
		{
			((ICustInfo2)wipper.Wip["CustInfo"]).FirstName = TxtFirstName.Text.Trim();
			((ICustInfo2)wipper.Wip["CustInfo"]).LastName  = TxtLastName.Text.Trim();
		}
		void SaverAddr()
		{
			if (((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr == null)
				return;

			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.City = TxtCity.Text.Trim();
			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.Zipcode = TxtZipCode.Text.Trim();
			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.State = ddlState.SelectedValue;
			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.Street = TxtStreetName.Text.Trim();
			
			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.StreetType = ddlStreetType.SelectedValue;
			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.StreetPrefix = txtStreetPfx.SelectedValue;
			((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.StreetNum = TxtStreetNumber.Text.Trim();	
		}
		void SaveDOB()
		{
			if (ddlDOBYY.SelectedIndex == 0)
				return;
				
			if (ddlDOBMM.SelectedIndex == 0)
				return;

			((ICustInfo2)wipper.Wip["CustInfo"]).Dob = 
					new DateTime(int.Parse(ddlDOBYY.SelectedValue),
					int.Parse(ddlDOBMM.SelectedValue), 
					int.Parse(ddlDOBDD.SelectedValue));
		}
		void LoadCustInfo()
		{
			if (wipper.Wip["CustInfo"] == null)
				return;

			ShowName();
			ShowDOB();
			ShowPhoneNumber();
			ShowSSN();
			ShowAddress();
		}
		void ShowName()
		{
			TxtFirstName.Text =	((ICustInfo2)wipper.Wip["CustInfo"]).FirstName;
			TxtLastName.Text =	((ICustInfo2)wipper.Wip["CustInfo"]).LastName;
		}
		void ShowAddress()
		{
			if (((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr == null)
				return;

			TxtCity.Text		        = ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.City;
			TxtZipCode.Text	            = ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.Zipcode;
			ddlState.SelectedValue	    = ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.State;
			TxtStreetName.Text          = ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.Street;
			ddlStreetType.SelectedValue	= ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.StreetType;
			txtStreetPfx.SelectedValue	= ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.StreetPrefix;
			TxtStreetNumber.Text		= ((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr.StreetNum;			
		}
		void ShowDOB()
		{
			if (((ICustInfo2)wipper.Wip["CustInfo"]).Dob == DateTime.MinValue)
				return;

			ddlDOBMM.SelectedValue = ((ICustInfo2)wipper.Wip["CustInfo"]).Dob.Month.ToString("d2");
			ddlDOBDD.SelectedValue = ((ICustInfo2)wipper.Wip["CustInfo"]).Dob.Day.ToString("d2");
			ddlDOBYY.SelectedValue = ((ICustInfo2)wipper.Wip["CustInfo"]).Dob.Year.ToString("d4");
		}
		void ShowSSN()
		{
			if (((ICustInfo2)wipper.Wip["CustInfo"]).Ssn == null)
				return;

			if (((ICustInfo2)wipper.Wip["CustInfo"]).Ssn.Length != 9)
				return;
		
			txtSSN1.Text = ((ICustInfo2)wipper.Wip["CustInfo"]).Ssn.ToString().Substring(0,3);
			txtSSN2.Text = ((ICustInfo2)wipper.Wip["CustInfo"]).Ssn.ToString().Substring(3,2);
			txtSSN3.Text = ((ICustInfo2)wipper.Wip["CustInfo"]).Ssn.ToString().Substring(5,4);
		}
		void ShowPhoneNumber()
		{
			if (((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber == null)
				return;

			if (((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber.Length != 10) 
				return;
		
			txtCnNum1.Text =			((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber.ToString().Substring(0,3);
			txtCnNum2.Text =			((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber.ToString().Substring(3,3);
			txtCnNum3.Text =			((ICustInfo2)wipper.Wip["CustInfo"]).PhNumber.ToString().Substring(6,4);
		}
		void GetCustInfo()
		{
			if (wipper.Wip["CustInfo"] == null)
				wipper.Wip["CustInfo"] = CustSvc.GetCustInfo(wipper.IMap, CustInfoTypes.CardApp.ToString());

			if (((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr == null)
				((ICustInfo2)wipper.Wip["CustInfo"]).MailAddr = CustFactory.GetAddress(wipper.IMap);
			
			((ICustInfo2)wipper.Wip["CustInfo"]).Status = CustInfoStatus.Prospect.ToString();

			((IDemand)wipper.Wip["Demand"]).Consumer = (ICustInfo2)wipper.Wip["CustInfo"];
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
		#endregion
	}
}