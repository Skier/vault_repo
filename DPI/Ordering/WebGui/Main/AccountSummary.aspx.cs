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
	public class AccountSummary : BasePage
	{ 
	#region Web Form Designer generated code
		
	#region Data
		bool firstTime;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblAddress;
		protected System.Web.UI.WebControls.Label lblPhoneNumber;
		protected System.Web.UI.WebControls.Label lblDueDate;
		protected System.Web.UI.WebControls.Label lblLastDay;
		protected System.Web.UI.WebControls.Label lblBalForward;
		protected System.Web.UI.WebControls.ImageButton btnChangeDue;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.Label lblCurrCharges;
		protected System.Web.UI.WebControls.PlaceHolder phOrderSummary;
		protected System.Web.UI.WebControls.TextBox txtLDsrvc;
		protected System.Web.UI.WebControls.Label lblAccNumber;
		protected System.Web.UI.WebControls.DropDownList ddlPayMethod;
		protected System.Web.UI.WebControls.ImageButton imgPastReminderNotice;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label lblCustomerName;
		protected System.Web.UI.WebControls.Label lblCityStateZip;
		protected System.Web.UI.WebControls.Label lblLocalAmountDue;
		protected System.Web.UI.WebControls.DropDownList ddlLdAmount;
		protected System.Web.UI.WebControls.Label lblTotalAmountDue;
		protected System.Web.UI.WebControls.TextBox txtAmountTendered;
		protected System.Web.UI.WebControls.Label lblChangeDue;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label txtNonRefund;
		protected System.Web.UI.WebControls.Label lblStatus;
		protected System.Web.UI.WebControls.Label lblPaymethod;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotAmntColl;
		protected System.Web.UI.WebControls.Label lblAmountPaid;
		protected System.Web.UI.WebControls.Image imgSignal;
		protected System.Web.UI.WebControls.TextBox txtAmountPaid;
		protected System.Web.UI.WebControls.Label lblChangeDueTxt;
		protected System.Web.UI.WebControls.Label lblPaidError;
		protected System.Web.UI.WebControls.Label lblAmtColError;
		protected System.Web.UI.WebControls.CheckBox chkLocalInFull;
		protected IAccSum acctSum;
	#endregion

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		private void InitializeComponent()
		{  
			this.ddlLdAmount.SelectedIndexChanged += new System.EventHandler(this.ddlLdAmount_SelectedIndexChanged);
			this.chkLocalInFull.CheckedChanged += new System.EventHandler(this.chkLocalInFull_CheckedChanged);
			this.btnChangeDue.Click += new System.Web.UI.ImageClickEventHandler(this.btnChangeDue_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

	#endregion	
		/*		Properties		*/
//		public Wipper Wipper { get { return wipper; }}

	#region EventHandlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try 
			{
				txtAmountPaid.ReadOnly = chkLocalInFull.Checked;
				RefreshIt();
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
				DestroyBusObj();
				Response.Redirect(wipper.Wip.Prev(), false);
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
				if (!ValidatePI())
					return;

				acctSum.Btn_Next();
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);				
			}
		}
		void chkLocalInFull_CheckedChanged(object sender, System.EventArgs e)
		{	
			try
			{
				//txtAmountPaid.ReadOnly = chkLocalInFull.Checked;
				if (!chkLocalInFull.Checked)
					return;
			
				PayInFull();
				RefreshIt();
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}
		}
		void btnChangeDue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				RefreshIt();
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}
		}
		void ddlLdAmount_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				((IPayInfoLocal)wipper.Wip["PayInfo"]).LdAmountDue = Money.Truncate(ddlLdAmount.SelectedValue);
				lblTotalAmountDue.Text = ((IPayInfoLocal)wipper.Wip["PayInfo"]).TotalAmountDue.ToString("C");
			
				RefreshIt();
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}
		}
	#endregion
	
	#region Implementation
		void DestroyBusObj()
		{
			if (wipper.Wip["Demand"] != null)
			{
				wipper.IMap.remove(((IDomObj)wipper.Wip["Demand"]).IKey);
				wipper.Wip["Demand"] = null;
			}

			if (wipper.Wip["PayInfo"] != null)
			{
				wipper.IMap.remove(((IDomObj)wipper.Wip["PayInfo"]).IKey);
				wipper.Wip["PayInfo"] = null;
			}

			wipper.Wip["CustInfoExt"] = null;
			wipper.Wip["AcctInfo"]    = null;
		}
		bool RefreshIt()
		{
			//AmtTenderedOverride(); // if non cash payment method selected, amt tendered is set to total amt due 
		
			SetPayInfoAttrs();
			ShowAcctInfo();	
			CalculateChange();
			return ValidatePI();
		}
		void PayInFull()
		{
			IPayInfoLocal pi = (IPayInfoLocal)wipper.Wip["payinfo"];
			
			pi.PayInFull(Money.Truncate(lblLocalAmountDue.Text),    // local due
						Money.Truncate(ddlLdAmount.SelectedValue),  //ld due
						Money.Truncate(this.txtAmountTendered.Text));  

			txtAmountPaid.Text = pi.TotalAmountPaid.ToString("C");
		}
		bool ValidatePI()
		{
			lblErrMsg.Visible = lblPaidError.Visible = false;

			if (firstTime) // suppress error the first time
				return true;

			lblErrMsg.Text = ((IPayInfo)wipper.Wip["payinfo"]).Validate();
			if (lblErrMsg.Text != string.Empty)
			{
				lblErrMsg.Visible = true;
				return false;
			}
			if (this.chkLocalInFull.Checked)
				if (((IPayInfo)wipper.Wip["payinfo"]).TotalAmountDue 
					!= ((IPayInfo)wipper.Wip["payinfo"]).TotalAmountPaid)
				{
					lblErrMsg.Text = "When Pay In Full is checked, the amount paid has to be equal to Total Amount Due";
					lblErrMsg.Visible = lblPaidError.Visible = true;
					return false;
				}	
			return true;
//    Amount Paid can exceed Total Due 4/17/05 Barbara
		}
		void CalculateChange()
		{
			lblChangeDue.Text = string.Empty;

			if (((IPayInfo)wipper.Wip["PayInfo"]).ChangeAmount > 0m)
				lblChangeDue.Text = ((IPayInfo)wipper.Wip["PayInfo"]).ChangeAmount.ToString("C");
		}
//		void AmtTenderedOverride()
//		{
//			if ((PaymentType)(Int32.Parse(ddlPayMethod.SelectedValue)) != PaymentType.Cash)
//				this.txtAmountTendered.Text = this.lblTotalAmountDue.Text;
//		}
		void SetPayInfoAttrs()
		{	
			IPayInfoLocal pi = (IPayInfoLocal)wipper.Wip["payinfo"];
			pi.PaymentType =  (PaymentType)int.Parse(ddlPayMethod.SelectedValue);

			if (chkLocalInFull.Checked)
			{
				PayInFull();
				return;
			} 
			pi.SetAmts(
				Money.Truncate(txtAmountPaid.Text),          // total paid
				((IAcctInfo)wipper.Wip["AcctInfo"]).DueAmt,  // local due
				decimal.Parse(ddlLdAmount.SelectedValue),    // LD due
				Money.Truncate(txtAmountTendered.Text)); 

		
			SyncForHighTouch();
		}
		void CustomInit()
		{
			try
			{
				btnNext.Visible     = wipper.Wip.HasNext;
				btnPrevious.Visible = wipper.Wip.HasPrev;	
				lblPaidError.Visible = 	lblAmtColError.Visible = false;

				BuildBusObjs();
				SetAttributes();
				lblTotAmntColl.Text     = "Total Amount Collected";
				lblChangeDueTxt.Visible = ((IPayInfo)wipper.Wip["PayInfo"]).ChangeAmount > 0;

				PrevBill();
				lblAmountPaid.Visible = true;

				lblAmountPaid.Text 
					= "If the customer is paying an amount other<br> than the total amount due, unclick 'Pay in Full' and " 
					+  " enter amount here.<br><br>If customer is paying total amount due, click 'Proceed to Next Step'";
				PendingPayments();
			}
			catch (Exception ex)
			{	
				ErrLogSvc.LogError(
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, 
					ex.Message + ", Stack trace " + ex.StackTrace);
					
				lblErrMsg.Text = Const.GENERAL_ERROR;
				lblErrMsg.Visible = true;
			}
		}
		void BuildBusObjs()
		{
			GetCustInfo();
			GetDemand();
			GetPayInfo();
		}
		void GetCustInfo()
		{
			if (wipper.Wip["CustInfoExt"] == null)
				wipper.Wip["CustInfoExt"] 
					= CustSvc.GetCustInfoExt(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);	
		}
		void GetDemand()
		{
			if (wipper.Wip["Demand"] == null)
			{
				IDemand dmd = DmdFactory.GetDemand(DemandType.Monthly.ToString());
				IUser user = (IUser)Session["User"];
				dmd.StoreCode = user.LoginStoreCode;
				wipper.IMap.add((IMapObj)dmd);
				wipper.Wip["Demand"] = dmd;
				((IDemand)wipper.Wip["Demand"]).ConsumerAgent = wipper.Wip.ClerkId;
			}
			
			((IDemand)wipper.Wip["Demand"]).BillPayer = ((IAcctInfo )wipper.Wip["AcctInfo"]).AccNumber;
		}
		void GetPayInfo()
		{
			firstTime = false;
			if (wipper.Wip["PayInfo"] != null)
				return;

			chkLocalInFull.Checked = firstTime = true;
			
			wipper.Wip["PayInfo"] = PaySvc.GetNewPayInfo(wipper.IMap,(IDemand)wipper.Wip["Demand"],PayInfoClass.PayInfoLocal);
		    ((IPayInfoLocal)wipper.Wip["payinfo"]).SetAmts(
				((IAcctInfo)wipper.Wip["AcctInfo"]).DueAmt,
				((IAcctInfo)wipper.Wip["AcctInfo"]).DueAmt,
				decimal.Zero,
				((IAcctInfo)wipper.Wip["AcctInfo"]).DueAmt);

			ShowAcctInfo();	
		}
		void PrevBillNotAva()
		{
			imgPastReminderNotice.Attributes.Add("onClick", "window.alert('Previous bill is not available.');");
		}
		void PrevBill()
		{
			IPastReminderNotice notice = CustSvc.GetReminderNotice(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);
			if (notice == null)
			{
				PrevBillNotAva();
				return;
			}
			
			if (notice.Filename == null)
			{
				PrevBillNotAva();
				return;
			}
				
			ViewState["LastBillFilename"] = Const.VIRTUAL_DIR_BILLVIEW + notice.Filename;
			if (((string)ViewState["LastBillFilename"]).Trim().Length == 0)
			{
				PrevBillNotAva();
				return;
			}
			
			imgPastReminderNotice.Attributes.Add("onClick", "window.open('" 
				+ (string)ViewState["LastBillFilename"] 
				+ "',null,'height= 550, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no," 
				+ "scrollbars=no,resizable=yes')");
		}
		void ShowAcctInfo()
		{
			lblAccNumber.Text    = ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber.ToString();
			lblPhoneNumber.Text  = ((IAcctInfo)wipper.Wip["AcctInfo"]).PhNumFormated;
			lblCustomerName.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).CustInfo.FormattedName; 

			lblAddress.Text      = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).ServAddr.FormattedStreetAddress;
			lblCityStateZip.Text = ((ICustInfoExt)wipper.Wip["CustInfoExt"]).ServAddr.FormattedCityStateZip;
			lblStatus.Text		 = ((IAcctInfo)wipper.Wip["AcctInfo"]).Status;

			lblBalForward.Text     = ((IAcctInfo)wipper.Wip["AcctInfo"]).BalForward.ToString("C");
			lblCurrCharges.Text    = ((IAcctInfo)wipper.Wip["AcctInfo"]).CurrCharges.ToString("C");
			lblLocalAmountDue.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).DueAmt.ToString("C");

			if (((IAcctInfo)wipper.Wip["AcctInfo"]).DueDate != DateTime.MinValue)
				lblDueDate.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).DueDate.ToShortDateString();
					
			if (((IAcctInfo)wipper.Wip["AcctInfo"]).DiscoDate > DateTime.MinValue)
				lblLastDay.Text = ((IAcctInfo)wipper.Wip["AcctInfo"]).DiscoDate.ToShortDateString();
		
			lblTotalAmountDue.Text = ((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountDue.ToString("C");
			SyncForHighTouch();
			
			IPastReminderNotice notice = CustSvc.GetReminderNotice(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber);
		}
		void PendingPayments()
		{
			bool NotHighTouch = !HighTouch.IsHighTouch(wipper.Wip, (IUser)Session["User"]);
			acctSum = PendingFactory.GetAccountSummary(!NotHighTouch, this);
			
			lblPaymethod.Visible = NotHighTouch;
			lblChangeDue.Visible = NotHighTouch;
			txtNonRefund.Visible = NotHighTouch;
			ddlPayMethod.Visible = NotHighTouch;
			btnChangeDue.Visible = NotHighTouch;

			txtAmountTendered.Visible = lblTotAmntColl.Visible = NotHighTouch;
			Label5.Text = NotHighTouch ? "Customer Information" : "Customer Information / Review";	
		}
		void SetAttributes() 
		{
			ddlPayMethod.Attributes.Add("OnSelectedIndexChanged","clickedButton=true; ");
			chkLocalInFull.Attributes.Add("OnCheckedChanged","clickedButton=true; ");
			ddlLdAmount.Attributes.Add("OnSelectedIndexChanged","clickedButton=true; ");

			btnNext.Attributes.Add("onClick","clickedButton=true; ");
			btnPrevious.Attributes.Add("onClick","clickedButton=true; ");
			btnChangeDue.Attributes.Add("onClick","clickedButton=true; ");
		}
		void SyncForHighTouch()
		{
			if (!HighTouch.IsHighTouch(wipper.Wip, (IUser)Session["User"]))
				return;

			if (wipper.Wip["payinfo"] == null)
				return;

//			((IPayInfo)wipper.Wip["payinfo"]).SyncForHighTouch();

			this.txtAmountPaid.Text
				= txtAmountTendered.Text 
				= ((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountPaid.ToString("C");
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			btnNext.Visible = false;
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
	#endregion
	}
}