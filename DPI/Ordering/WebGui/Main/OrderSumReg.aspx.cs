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
	public class OrderSumReg : BasePage
	{
	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.ddlLdAmount.SelectedIndexChanged += new System.EventHandler(this.ddlLdAmount_SelectedIndexChanged);
			this.ddlPayMethod.SelectedIndexChanged += new System.EventHandler(this.ddlPayMethod_SelectedIndexChanged);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.PlaceHolder phldrOrdrDetails;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.TextBox txtOrderNotes;
		protected System.Web.UI.WebControls.Label lblFees;
		protected System.Web.UI.WebControls.ImageButton btn_change;
		protected System.Web.UI.WebControls.Label lblOrderTotal;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label lblChangeDue;
		protected System.Web.UI.WebControls.Label lblOrderTotalM2;
		protected System.Web.UI.WebControls.Label lblFeesM2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.DropDownList ddlPayMethod;
		protected System.Web.UI.WebControls.TextBox txtAmountTendered;
		protected System.Web.UI.WebControls.Label txtNonRefund;
		protected System.Web.UI.WebControls.Label lblLocalAmountDue;
		protected System.Web.UI.WebControls.DropDownList ddlLdAmount;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblLocalAmountDue2;
		protected System.Web.UI.WebControls.Label lblReminder;
		protected System.Web.UI.WebControls.Label lblPayMethod;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.Label lblTotAmtColl;
		protected System.Web.UI.WebControls.ImageButton btnMonthChart;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Label Label6;
		protected System.Web.UI.WebControls.ImageButton btn_ChangeDue;
		protected System.Web.UI.WebControls.TextBox txtAmtPaid;
		protected System.Web.UI.WebControls.Label lblPaidText;
		protected System.Web.UI.WebControls.Label lblErrMsg;
	#endregion
		
	#region	Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			bool loaded = false;

			try
			{	
				loaded = Pre_Load();
			
				if (IsPostBack)
					return;
				
				CustomInit();
							
				if (!loaded)
					LoadTable();
				
				ToPage();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);				
			}
		}	
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs ea)
		{
			try
			{	
				ToBusObj();

				if (!ValidateIt())
					return;

				CustSvc.PreSave(wipper.IMap);

				StoreOrder();
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
				CustSvc.ClearOrderSummaryPageData // add stuff tp clean more (DI & dmdTax)
					(wipper.IMap, (IDemand)wipper.Wip["Demand"], (IPayInfo)wipper.Wip["PayInfo"]);

				wipper.Wip["OrderSummary"] = null;
				wipper.Wip["PayInfo"] = null;
				wipper.Wip["Demand"]  = null;
				
				Response.Redirect(wipper.Wip.Prev(), false);
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
		//		txtAmtPaid.Text =
		//			(Money.Add(ddlLdAmount.SelectedValue, GetSumry().GetTotalAmtDue(1))).ToString("C"); // per barbara 10/10/05
				Refresh();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);				
			}
		}

		void btn_ChangeDue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Refresh();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);				
			}		
		}
		void ddlPayMethod_SelectedIndexChanged(object sender, System.EventArgs ea)
		{
			try
			{
				Refresh();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);				
			}
		}
		#endregion

	#region	Implementation
		void Refresh()
		{
			ToBusObj();
			ValidateIt();
			ToPage();
		}
		void ToBusObj()
		{
			SetAmounts();
			CheckNotes();				
		}
		void SetAmounts()
		{	
			if (wipper.Wip["PayInfo"] == null)
				throw new ApplicationException("PayInfo is null");

			((IPayInfoLocal)wipper.Wip["PayInfo"]).PaymentType = (PaymentType)int.Parse(ddlPayMethod.SelectedValue);

			((IPayInfoLocal)wipper.Wip["PayInfo"]).SetAmts(
				Money.Truncate(txtAmtPaid.Text),
				GetSumry().GetTotalAmtDue(1),
				decimal.Parse(ddlLdAmount.SelectedValue),
				Money.Truncate(txtAmountTendered.Text));	
	
//			((IPayInfoLocal)wipper.Wip["PayInfo"]).SetAmts(
//				GetSumry().GetTotalAmtDue(1) + Money.Truncate(ddlLdAmount.SelectedValue), 
//				GetSumry().GetTotalAmtDue(1),
//				decimal.Parse(ddlLdAmount.SelectedValue),
//				Money.Truncate(txtAmountTendered.Text));	
		}
		void ToPage()
		{
			// Month 1
			lblOrderTotal.Text     = GetSumry().GetProdSubTotal(1).ToString("C"); // ProdSubTotal
			lblFees.Text           = GetSumry().GetTaxAmt(1).ToString("c");       // Fees & taxes
			lblLocalAmountDue.Text = GetSumry().GetTotalAmtDue(1).ToString("C");  // ProdSubTotal +  Fees & taxes		
			

			decimal total =  GetSumry().GetTotalAmtDue(1) 
				+ Money.Truncate(this.ddlLdAmount.SelectedValue);				 // ProdSubTotal +  Fees & taxes + LD	
			lblAmountDue.Text      = decimal.Round(total, 2).ToString("C"); 		

			if (txtAmtPaid.Text.Trim().Length == 0)
				txtAmtPaid.Text =  (GetSumry().GetTotalAmtDue(1) + Money.Truncate(ddlLdAmount.SelectedValue)).ToString("C");  

			// month 2
			lblOrderTotalM2.Text    = GetSumry().GetProdSubTotal(2).ToString("C");
			lblLocalAmountDue2.Text = GetSumry().GetTotalAmtDue(2).ToString("C");
			lblFeesM2.Text          = GetSumry().GetTaxAmt(2).ToString("C");

			// the rest
			FromPayInfo();
			if (wipper.Wip["AcctNotes"] != null)
				txtOrderNotes.Text = ((IAcctNotes)wipper.Wip["AcctNotes"]).Text;	
	
			SetMouseOver();
		}
		void FromPayInfo()
		{
			if (wipper.Wip["PayInfo"] == null)
				return;

			 txtAmountTendered.Text = lblChangeDue.Text = "";
		
			if (((IPayInfoLocal)wipper.Wip["PayInfo"]).AmountTendered != 0m)
                txtAmountTendered.Text = ((IPayInfoLocal)wipper.Wip["PayInfo"]).AmountTendered.ToString("C");

			if (((IPayInfoLocal)wipper.Wip["PayInfo"]).ChangeAmount != 0m)
				lblChangeDue.Text = ((IPayInfoLocal)wipper.Wip["PayInfo"]).ChangeAmount.ToString("C");
		}
		bool ValidateIt()
		{
			lblErrMsg.Visible = false;
			btnNext.Visible = false;
		
			if (Money.Truncate(this.txtAmountTendered.Text) 
				< GetSumry().GetTotalAmtDue(1) + Money.Truncate(ddlLdAmount.SelectedValue))
			{
				lblErrMsg.Text = "Total Amount Collected must be not less than Total Amount Due";
				lblErrMsg.Visible = true;
				return false;
			}

			if (Money.Truncate(txtAmtPaid.Text) 
				< (GetSumry().GetTotalAmtDue(1) + Money.Truncate(ddlLdAmount.SelectedValue)))
			{
				lblErrMsg.Text = "The amount customer is paying must be not less than Total Amount Due";
				lblErrMsg.Visible = true;
				return false;
			}
			
			if (Money.Truncate(txtAmountTendered.Text) < Money.Truncate(txtAmtPaid.Text))
			{
				lblErrMsg.Text = "Total Amount Collected must be not less than amount customer is paying ";
				lblErrMsg.Visible = true;
				return false;
			}

			btnNext.Visible = true;
			return true;
		}

		void CustomInit()
		{			
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
			
			CheckOrderSummary();
			txtAmtPaid.Text = lblAmountDue.Text;
			lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
			lblZipCode.Text = (string)wipper.Wip["zip"];
		}

		void CheckNotes()
		{
			if (txtOrderNotes.Text.Trim().Length == 0)
			{
				wipper.Wip["AcctNotes"] = null;
				return;
			}
	
			if (wipper.Wip["AcctNotes"] == null)
				wipper.Wip["AcctNotes"] 
					= new AcctNotes(txtOrderNotes.Text.Trim(),wipper.Wip.ClerkId);
		}
		void CheckOrderSummary()
		{
			if (wipper.Wip["ordersummary"] != null)
				return;

			wipper.Wip["ordersummary"] = CustSvc.GetOrderSummary(
				wipper.IMap, 
				(IProdPrice[])wipper.Wip["prods"], 
				(string)wipper.Wip["Zip"],
				(IILECInfo)wipper.Wip["SelectedIlec"],
				(string)wipper.Wip["DmdType"], 
				(OrderType)wipper.Wip["ordertype"]);
			
			if (wipper.Wip["ordersummary"] == null)
				throw new ApplicationException("Order summary is not found");
		
			ConfigureDmd();
			CheckPayInfo();
		}
		void ConfigureDmd()
		{
			IDemand dmd = ((IOrderSum)wipper.Wip["ordersummary"]).Demand;
			wipper.Wip["Demand"] = dmd;
			
			dmd.ConsumerAgent	= wipper.Wip.ClerkId;
			dmd.StoreCode		= wipper.Wip.StoreCode;
			dmd.IsUnderWF		= true;
			dmd.WFStep			= wipper.Wip.Url;
			dmd.Workflow		= wipper.Workflow;	
//			dmd.Source          = (int)wipper["Source"];
		}
		
		void SetMouseOver()
		{
			StringBuilder sb = new StringBuilder();
			string[] lines = ((IOrderSum)wipper.Wip["ordersummary"]).GetSumTaxDesc(1);
			
			for (int i = 0; i < lines.Length; i++)
				sb.Append(lines[i] + "<BR>");

			lblFees.Text = 
				"<a href='javascript:void(0);' onmouseover='return escape(\""
				+ sb.ToString()
				+ "\")'>" 
				+ lblFees.Text
				+ "</a>";

			sb = new StringBuilder();
			lines = ((IOrderSum)wipper.Wip["ordersummary"]).GetSumTaxDesc(2);
			
			for (int i = 0; i < lines.Length; i++)
				sb.Append(lines[i] + "<BR>");

			lblFeesM2.Text = 
				"<a href='javascript:void(0);' onmouseover='return escape(\""
				+ sb.ToString()
				+ "\")'>" 
				+ lblFeesM2.Text
				+ "</a>";
	
			Label6.Text = 
				"<a href='javascript:void(0);' onmouseover='return escape(\""
				+ ""
				+ "\")'>" 
				+ " "
				+ "</a>";

		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr(wipper.IMap, ex, Session["User"], this.ToString());
			btnNext.Visible = false;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
		}	
		bool Pre_Load()
		{
			MultiBlock();  // we might not call it every page load
			SetAttrs();    // we might not call it every page load
			return LoadTable();
		}
		bool LoadTable()
		{
			if (wipper.Wip["ordersummary"] == null)
				return false;
				
			phldrOrdrDetails.Controls.Add(new TableProdSum(GetProducts()));	
			return true;
		}
		IOrderSum GetSumry()
		{
			if (wipper.Wip["ordersummary"] == null)
				return null;

			return (IOrderSum)wipper.Wip["ordersummary"];
		}

		IProdPrice[] GetProducts()
		{
			if (wipper.Wip["ordersummary"] == null)
				return new IProdPrice[0];

			return ((IOrderSum)wipper.Wip["ordersummary"]).Products;	
		}
		void CheckPayInfo()
		{
			if (wipper.Wip["PayInfo"] != null)
				return;

			wipper.Wip["PayInfo"] 
				= PaySvc.GetNewPayInfo(wipper.IMap,(IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfoLocal);
			((IPayInfo)wipper.Wip["PayInfo"]).PaymentType = PaymentType.Credit;	
		}
		
		void SetAttrs()
		{
			btnMonthChart.Attributes.Add("onClick", "window.open('MonthChart.aspx', '_blank' ,"
				+ "'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,"
				+ " menubar=no, scrollbars=yes,resizable=yes')");
			ddlLdAmount.Attributes.Add("onClick","clickedButton=true; ");		
			
			btnPrint.Attributes.Add("onClick", "window.open('SummaryPrint.aspx', '_blank' ,'height= 630, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			//btnMonthChart.Attributes.Add("onClick", "window.open('MonthChart.aspx', '_blank' ,'height= 630, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			btn_ChangeDue.Attributes.Add("onClick", "calculate();");
			//ddlLdAmount.Attributes.Add("onselectedindexchanged", "calculateLd();");
			ddlLdAmount.Attributes.Add("onClick", "calculateLd();");

			btnNext.Attributes.Add("onClick", "Submit(" + ((IProdPrice)wipper.Wip["SelectedBasicService"]).ProdId + ");");
		}
		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnMonthChart);
			MultiClickBlocker.Block(this, btn_ChangeDue);
		}
		void StoreOrder()
		{	
			wipper.Wip["receipt"] = GetReceipt();
			
			((IPayInfo)wipper.Wip["PayInfo"]).Status  = PaymentStatus.Paid.ToString();
			((IPayInfo)wipper.Wip["PayInfo"]).VFConf  = ((IReceipt)wipper.Wip["receipt"]).ConfNum;
			
			((IDemand)wipper.Wip["demand"]).BillPayer = ((IReceipt)wipper.Wip["receipt"]).AccNumber;	
			((IDemand)wipper.Wip["Demand"]).Source = (int)wipper.Wip["Source"];

			wipper.Wip.BusObjId = ((IReceipt)wipper.Wip["receipt"]).AccNumber;
			wipper.Wip.BusObjType = "Cust Account";
	
			StoreDmdPayInfo();
		}
		void StoreDmdPayInfo()
		{
			CustSvc.PreSave(wipper.IMap);
			wipper.IMap.add((IMapObj)wipper.Wip["Demand"]);	
			wipper.IMap.add((IMapObj)wipper.Wip["PayInfo"]);
			CustSvc.PreSave(wipper.IMap);
		}
		IReceipt GetReceipt()
		{
			return CustSvc.SubmitNewXact(
				wipper.IMap,
				(IDemand)wipper.Wip["Demand"],
				((IILECInfo)wipper.Wip["SelectedIlec"]).OrgId,
				wipper.Wip.StoreCode,	
				(IUser)Session["User"],
				((int)wipper.Wip.WipId).ToString(), 
				(IPayInfo)wipper.Wip["PayInfo"],
				null,
				(IAcctNotes)wipper.Wip["AcctNotes"]);
		}	
		
	#endregion
	}
}