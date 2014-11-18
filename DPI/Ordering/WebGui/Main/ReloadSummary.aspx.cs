using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

using DPI.ClientComp;
using DPI.Services; 
using DPI.Ordering;
using DPI.Interfaces;

namespace DPI.Ordering 
{
	public class ReloadSummary : BasePage
	{
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			//CustomInit();
		}
	
		private void InitializeComponent()
		{    
			this.txtLoadAmount.TextChanged += new System.EventHandler(this.txtLoadAmount_TextChanged);
			this.btnChangeDue.Click += new System.Web.UI.ImageClickEventHandler(this.btnChangeDue_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtOrderNotes;
		protected System.Web.UI.WebControls.ImageButton btn_change;
		protected System.Web.UI.WebControls.Label lblChangeDue;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.DropDownList ddlPayMethod;
		protected System.Web.UI.WebControls.TextBox txtAmountTendered;
		protected System.Web.UI.WebControls.Label txtNonRefund;
		protected System.Web.UI.WebControls.ImageButton btnChangeDue;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblReminder;
		protected System.Web.UI.WebControls.Label lblPayMethod;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotAmtColl;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Label Label8;
		protected System.Web.UI.WebControls.Label Label7;
		protected System.Web.UI.WebControls.Label lblReloadFee;
		protected System.Web.UI.WebControls.TextBox txtLoadAmount;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.TextBox txtAccNo1;
		protected System.Web.UI.WebControls.TextBox txtAccNo2;
		protected System.Web.UI.WebControls.TextBox txtAccNo3;
		protected System.Web.UI.WebControls.TextBox txtAccNo4;
		protected System.Web.UI.WebControls.DropDownList ddlMM;
		protected System.Web.UI.WebControls.DropDownList ddlYY;
		protected System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidator2;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
		protected System.Web.UI.WebControls.Label lblCard;
		protected System.Web.UI.WebControls.PlaceHolder phError;

	#endregion]
		
	#region	Event Handlers

		void Page_Load(object sender, System.EventArgs e)
		{
			if (IsPostBack)
				return;
	
			try
			{
				CustomInit();
				SetupBusObjs();	
				
				ddlMM.DataSource = DropDownListDate.GetCCMonths(true);
				ddlMM.DataTextField = "DDLText";
				ddlMM.DataValueField  = "DDLValue";
				ddlMM.DataBind();

				ddlYY.DataSource = DropDownListDate.GetCCYears(true);
				ddlYY.DataTextField = "DDLText"; 
				ddlYY.DataValueField  = "DDLValue";
				ddlYY.DataBind();	
				
				ToPage();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError,  ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{	
				if (!IsValid)
					return;

				decimal amt = ((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount;

				ToBusObj();
				
				if (!ValidateData())
					return;
				
				ToPage();
				
				if (((IPayInfo)wipper.Wip["payInfo"]).ChangeAmount != amt)
					return; // show new change amount
	
				if (((IPayInfo)wipper.Wip["payInfo"]).TranNumber > 0) // override
					((IPayInfo)wipper.Wip["payInfo"]).PaymentType = PaymentType.TurboCash;

				DebCardSvc.PreSave(wipper.IMap);
				Continue();
			}
			catch (Exception ex)
			{				
				FatalError.ShowErr(phError, ex.Message);	
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}
		}
		void btnPrevious_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{
				Destroyer.Destroy(wipper.Wip["Demand"]);
				wipper.Wip["Demand"] = null;

				Destroyer.Destroy(wipper.Wip["PayInfo"]);
				wipper.Wip["PayInfo"] = null;

				Destroyer.Destroy(wipper.Wip["DebCardApp"]);
				wipper.Wip["DebCardApp"] = null;

				Destroyer.Destroy(wipper.Wip["Receipt"]);
				wipper.Wip["Receipt"] = null;

				Destroyer.Destroy(wipper.Wip["DebCard"]);
				wipper.Wip["DebCard"] = null;
			}
			catch {}
		}

		void btnChangeDue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{	
				RefreshData();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}		
		}	
		void txtLoadAmount_TextChanged(object sender, System.EventArgs e)
		{
			try 
			{
				if (!ValTxtInitLoadAmnt())
					return;

				RefreshData();
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
				FatalError.SaveErr(ex, Session["User"], this.ToString());
			}	
		}
	#endregion

	#region	Implementation
		void CustomInit()
		{	  
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
			
			ddlPayMethod.Attributes.Add("onClick","clickedButton=true; ");
			btnChangeDue.Attributes.Add("onClick","clickedButton=true; ");
			MakeReadOnly();
		}		
		void MakeReadOnly()
		{
			if (wipper.Wip["payInfo"] == null)
				return;

			if (((IPayInfo)wipper.Wip["payInfo"]).TranNumber == 0)
				return;
			
			txtLoadAmount.ReadOnly = txtAmountTendered.ReadOnly = true;
		    ddlPayMethod.Visible = btnChangeDue.Visible = lblPayMethod.Visible = false;
		}
		void Continue()
		{	
			IWireless_Transactions tran = null;
			if (wipper.Wip["Tran"] != null)
				tran = (IWireless_Transactions)wipper.Wip["Tran"];

			try 
			{
				wipper.Wip["Receipt"] = DebCardSvc.Refill(wipper.IMap,
					(IPayInfo)wipper.Wip["payInfo"], 
					(ICardApp)wipper.Wip["DebCardApp"],
					(IUser)Session["User"],
					tran);
				
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				FatalError.ShowErr(phError, ex.Message);
			}
		}
		void RefreshData()
		{
			ToBusObj();
			ValidateData();
			ToPage();
		}
		void ToBusObj()
		{
			((IDemand)wipper.Wip["Demand"]).GetDmdItems()[0].AdjustPrice(0M);

			if (txtLoadAmount.Text != null) // stores load amount
				if (txtLoadAmount.Text.Trim().Length > 0)
				{
					IDmdItem[] itm =  ((IDemand)wipper.Wip["Demand"]).GetDmdItems();
					itm[0].AdjustPrice(Money.Truncate(this.txtLoadAmount.Text));
				}

			decimal amt = ((IDemand)wipper.Wip["Demand"]).OrderSummary(null).GetTotalAmtDue();

			((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered =  Money.Truncate(txtAmountTendered.Text);
			((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountPaid = amt;			
			((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountDue = amt;			

			((IPayInfo)wipper.Wip["PayInfo"]).PaymentType = (PaymentType)int.Parse(ddlPayMethod.SelectedValue);

			((ICardApp)wipper.Wip["DebCardApp"]).Dmd = ((IDemand)wipper.Wip["Demand"]).Id;
			((ICardApp)wipper.Wip["DebCardApp"]).CardNum = GetCardNumber();
			((ICardApp)wipper.Wip["DebCardApp"]).ExpMonth = ddlMM.SelectedValue;
			((ICardApp)wipper.Wip["DebCardApp"]).ExpYear = ddlYY.SelectedValue ;
		}
		void ToPage()
		{
			// Order summary
			IOrderSum sumry = ((IDemand)wipper.Wip["Demand"]).OrderSummary(null);
			
			txtLoadAmount.Text = "";
			if (sumry.GetProdAmt() > 0)
                txtLoadAmount.Text = sumry.GetProdAmt().ToString("C");
			lblReloadFee.Text  = sumry.GetFeeAmt().ToString("C");
			
			lblAmountDue.Text  = "";
			if (sumry.GetProdAmt() > 0)
				lblAmountDue.Text  = sumry.GetTotalAmtDue().ToString("C"); 

			// Payinfo
			IPayInfo payInfo  = (IPayInfo)wipper.Wip["PayInfo"];

			txtAmountTendered.Text = "";
			if (payInfo.AmountTendered > 0)
				txtAmountTendered.Text = payInfo.AmountTendered.ToString("C");

			lblChangeDue.Visible = payInfo.ChangeAmount > 0;
			lblChangeDue.Text    = payInfo.ChangeAmount.ToString("C");

			ddlMM.SelectedValue = ((ICardApp)wipper.Wip["DebCardApp"]).ExpMonth;
			ddlYY.SelectedValue = ((ICardApp)wipper.Wip["DebCardApp"]).ExpYear;
		}
		void SetupBusObjs()
		{	
			if (wipper.Wip["DebCard"] == null)
				wipper.Wip["DebCard"] 
					= DebCardSvc.GetDebCardProd(wipper.IMap, ((IUser)HttpContext.Current.Session["User"]).LoginStoreCode);
	
			if (wipper.Wip["Demand"] == null)
				GetDemand();
			
			if (wipper.Wip["DebCardApp"] == null)
				wipper.Wip["DebCardApp"] = DebCardSvc.GetDebitCardApp(wipper.IMap, (IDemand)wipper.Wip["Demand"]);
		
			if (wipper.Wip["PayInfo"] == null)
			{
				wipper.Wip["PayInfo"] 
					= PaySvc.GetNewPayInfo(wipper.IMap,(IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);
				((IPayInfo)wipper.Wip["PayInfo"]).Status = PaymentStatus.Paid.ToString();
			}
		}
		void GetDemand()
		{
			if (wipper.Wip["Demand"] != null)
				return;

			wipper.Wip["Demand"] = DmdFactory.GetDemand(wipper.IMap, DemandType.DebCardReload.ToString(), true);

			((IDemand)wipper.Wip["Demand"]).ConsumerAgent = wipper.Wip.ClerkId;
			((IDemand)wipper.Wip["Demand"]).StoreCode     = ((IUser)Session["User"]).LoginStoreCode;
			((IDemand)wipper.Wip["Demand"]).Status        = DemandStatus.Pend.ToString();			

			((IDemand)wipper.Wip["Demand"]).Loc    
				= LocSvc.FindStoreZipId(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode);

			AddDmdItems();
		}
		void AddDmdItems()
		{		
			((IDemand)wipper.Wip["Demand"]).ClearDmdItems();

			if (wipper.Wip["DebCard"] == null)
				wipper.Wip["DebCard"] = DebCardSvc.GetDebCardProd(wipper.IMap, 
					((IUser)HttpContext.Current.Session["User"]).LoginStoreCode);

			wipper.Wip["DItem"] = ProdSvc.AddDmdItem(wipper.IMap, 
				(IDemand)wipper.Wip["Demand"], 
				(IProdPrice)wipper.Wip["DebCard"],
				OrderType.Add); 
			
			((IDmdItem)wipper.Wip["DItem"]).DmdItemType = DItemType.Selected.ToString();
		}
		bool ValidateData()
		{		
			phError.Visible = false;
			ArrayList ar = new ArrayList();

            CheckOrderRule(ar);
			CheckAmtTendered(ar);
			CheckCardValid(ar);
			CheckExpDate(ar);

			if (ar.Count == 0)
				return true;

			phError.Controls.Add(new ErrorTable(ar));
			phError.Visible = true;

			return false;
		}
		bool ValTxtInitLoadAmnt()
		{
			phError.Visible = false;
			ArrayList ar = new ArrayList();

			ValRegEx(ar);

			if (ar.Count == 0)
				return true;

			phError.Controls.Add(new ErrorTable(ar));
			phError.Visible = true;
			return false;		
		}
		void ValRegEx(ArrayList ar)
		{
			Regex reg = new Regex(@"^\$?[\d,]*\.?\d{0,2}$");

			if (!reg.IsMatch(txtLoadAmount.Text))
				ar.Add("Please enter valid Load Amount");
		}
		void CheckExpDate(ArrayList ar)
		{
			if (!(new Regex(@"\d{2}").Match(ddlYY.SelectedValue).Success))
			{
				ar.Add("Expiration Date is required");
				return;
			}

			if (!(new Regex(@"\d{2}").Match(ddlMM.SelectedValue).Success))
			{
				ar.Add("Expiration Date is required");
				return;
			}

			int yy = DateTime.Now.Year;
			int mm = DateTime.Now.Month;
			
			if (int.Parse(ddlYY.SelectedValue) > yy)
				return; 
			
			if (int.Parse(ddlMM.SelectedValue) < mm)
			   ar.Add("Invalid Expiration Date");
		}
		void CheckAmtTendered(ArrayList ar)
		{
			if(((IDemand)wipper.Wip["Demand"]).OrderSummary(null).GetTotalAmtDue()
				> ((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered)
					ar.Add("Amount Collected cannot be less than Amount Due");
		}
		void CheckOrderRule(ArrayList ar)
		{
			IProductOrderRule rule = DebCardSvc.GetProductOrderRule(wipper.IMap, 
				                                                   ((IDemand)wipper.Wip["Demand"]).DmdType, 
				                                                   ((IProdPrice)wipper.Wip["debCard"]).ProdId);
			if (rule == null)
				return;
			
			if(Money.Truncate(txtLoadAmount.Text) < rule.MinAmt) 
				ar.Add("Load amount less than  " + rule.MinAmt.ToString("C") + " is not allowed");

			if(Money.Truncate(txtLoadAmount.Text) > rule.MaxAmt)
				ar.Add("Load Amount more than " + rule.MaxAmt.ToString("C") + " is not allowed");
		}
		void CheckCardValid(ArrayList ar)
		{
			if (new Regex(@"\d{16}").Match(GetCardNumber()).Success)
				return;
			
			ar.Add("Master Card Account must be 16 digits");			
		}
		string GetCardNumber()
		{
			return txtAccNo1.Text.Trim() 
				+ txtAccNo2.Text.Trim()
				+ txtAccNo3.Text.Trim()
				+ txtAccNo4.Text.Trim();
		}
	#endregion


	}
}