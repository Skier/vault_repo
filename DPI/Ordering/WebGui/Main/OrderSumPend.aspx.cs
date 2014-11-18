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
	public class OrderSumPend : BasePage
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
		protected System.Web.UI.WebControls.Label lblOrderTotalM2;
		protected System.Web.UI.WebControls.Label lblFeesM2;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label lblAmountDue;
		protected System.Web.UI.WebControls.Label lblLocalAmountDue;
		protected System.Web.UI.WebControls.DropDownList ddlLdAmount;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label lblLocalAmountDue2;
		protected System.Web.UI.WebControls.Label lblReminder;
		protected System.Web.UI.WebControls.Label Label5;
		protected System.Web.UI.WebControls.ImageButton btnMonthChart;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Label Label6;
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
			((IPayInfoLocal)wipper.Wip["PayInfo"]).SetAmts(
				GetSumry().GetTotalAmtDue(1) + decimal.Parse(ddlLdAmount.SelectedValue), 
				GetSumry().GetTotalAmtDue(1),
				decimal.Parse(ddlLdAmount.SelectedValue),
				GetSumry().GetTotalAmtDue(1) + decimal.Parse(ddlLdAmount.SelectedValue));	
			
			CheckNotes();				
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

			// month 2
			lblOrderTotalM2.Text    = GetSumry().GetProdSubTotal(2).ToString("C");
			lblLocalAmountDue2.Text = GetSumry().GetTotalAmtDue(2).ToString("C");
			lblFeesM2.Text          = GetSumry().GetTaxAmt(2).ToString("C");

			// the rest
			
			if (wipper.Wip["AcctNotes"] != null)
				txtOrderNotes.Text = ((IAcctNotes)wipper.Wip["AcctNotes"]).Text;	
			
			SetMouseOver();
		}
		bool ValidateIt()
		{
			return true;
		}

		void CustomInit()
		{			
			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev;
			
			CheckOrderSummary();
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
					= CustSvc.GetNotes(wipper.IMap, wipper.Wip.ClerkId, txtOrderNotes.Text.Trim(), Const.SUBSYSTEM);
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
			((IPayInfo)wipper.Wip["PayInfo"]).IsConfReq  = (bool)wipper.Wip["IsConfReq"];	
		}
		
		void SetAttrs()
		{
			btnMonthChart.Attributes.Add("onClick", "window.open('MonthChart.aspx', '_blank' ,"
				+ "'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,"
				+ " menubar=no, scrollbars=yes,resizable=yes')");
			ddlLdAmount.Attributes.Add("onClick","clickedButton=true; ");		
			
			btnPrint.Attributes.Add("onClick", "window.open('SummaryPrint.aspx', '_blank' ,'height= 630, width=700 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			//btnMonthChart.Attributes.Add("onClick", "window.open('MonthChart.aspx', '_blank' ,'height= 630, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
            btnNext.Attributes.Add("onClick", "Submit(" + ((IProdPrice)wipper.Wip["SelectedBasicService"]).ProdId + ");");
		}
		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnMonthChart);
		}
		void StoreOrder()
		{	
			CustSvc.PreSave(wipper.IMap);
			wipper.IMap.add((IMapObj)wipper.Wip["Demand"]);	
			wipper.IMap.add((IMapObj)wipper.Wip["PayInfo"]);
			CustSvc.PreSave(wipper.IMap);

			((IDemand)wipper.Wip["Demand"]).Source = (int)wipper.Wip["Source"];
			((IDemand)wipper.Wip["Demand"]).BillPayer = CustSvc.ReserveAccNumber(wipper.IMap);

			wipper.Wip["Receipt"] = CustSvc.GetReceipt((IDemand)wipper.Wip["Demand"]);

			wipper.Wip.BusObjType = "Demand";		
			wipper.Wip.BusObjId = ((IDemand)wipper.Wip["Demand"]).Id;		
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