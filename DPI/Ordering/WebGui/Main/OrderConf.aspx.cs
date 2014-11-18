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
	public class OrderConf : BasePage
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
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.PlaceHolder placehldrOrderedProducts;
		protected System.Web.UI.WebControls.Label lblCustName;
		protected System.Web.UI.WebControls.Label lblMerch;
		protected System.Web.UI.WebControls.Label lblDate;
		protected System.Web.UI.WebControls.Label lblPType;
		protected System.Web.UI.WebControls.Label lblTaxes;
		protected System.Web.UI.WebControls.Label lblPaid;
		protected System.Web.UI.WebControls.Label lblOrderTotal;
		protected System.Web.UI.WebControls.Label lblZipCode;
		protected System.Web.UI.WebControls.Label lblIlec;
		protected System.Web.UI.WebControls.Label lblSubheader;
		protected System.Web.UI.WebControls.Label lblPayType;
		protected System.Web.UI.WebControls.Label lblTotAmntPaid;
		protected System.Web.UI.WebControls.Label lblLDAmount;
		protected System.Web.UI.WebControls.Label lblProdSubTotal;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		#endregion

	#region Event Handlers
		void Page_Load(object sender, System.EventArgs e) 
		{
			if (IsPostBack)
				return;

			try
			{
				CustomInit();				
				loadConfirmationData();
				ToPage();
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
				bool isHighTouch = IsHighTouch();
				LinkObjects(isHighTouch);

				if (!isHighTouch)
					if (!SaveOrder())
						return;

				if (isHighTouch)
					HighTouchConf();

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
		void ToPage()
		{
			lblMerch.Text = DPI.ClientComp.User.GetUser(this).ClerkId;
			lblTaxes.Text = ((IOrderSum)wipper.Wip["OrderSummary"]).GetTaxAmt(1).ToString("C");
			lblProdSubTotal.Text = ((IOrderSum)wipper.Wip["OrderSummary"]).GetProdSubTotal(1).ToString("C");

			lblOrderTotal.Text = 
				(((IOrderSum)wipper.Wip["OrderSummary"]).GetTotalAmtDue(1) 
				+ ((IPayInfoLocal)wipper.Wip["PayInfo"]).LdAmount).ToString("C");
			
			lblPaid.Text = 
				(((IPayInfoLocal)wipper.Wip["PayInfo"]).LocalAmountPaid 
				+ ((IPayInfoLocal)wipper.Wip["PayInfo"]).LdAmount).ToString("C");
			
			lblDate.Text = ((IPayInfo)wipper.Wip["PayInfo"]).PayDate.ToShortDateString();
			lblLDAmount.Text = ((IPayInfoLocal)wipper.Wip["PayInfo"]).LdAmount.ToString("C");

			IProdPrice[] orderedProds = ((IOrderSum)wipper.Wip["OrderSummary"]).Products;
			placehldrOrderedProducts.Controls.Add(new TableProdOrdConfirm(orderedProds));	

			lblIlec.Text = ((IILECInfo)wipper.Wip["selectedilec"]).ILECName;
			lblZipCode.Text = (string)wipper.Wip["zip"];
		}
		void loadConfirmationData()
		{	
			if (wipper.Wip["CustInfo"] == null)
				wipper.Wip["CustInfo"] = CustFactory.GetCustInfo(wipper.IMap);

			if ( ((ICustInfo)wipper.Wip["CustInfo"]).FirstName != null)
				lblCustName.Text =	((ICustInfo)wipper.Wip["CustInfo"]).FirstName 
					+ " " + ((ICustInfo)wipper.Wip["CustInfo"]).LastName;		
	
			lblPType.Text= ((IPayInfo)wipper.Wip["PayInfo"]).PaymentType.ToString();	
			// setup other contols ...
		}

		bool SaveOrder()
		{
			if (wipper.Wip["AcctNotes"] == null)
			{
				wipper.Wip["Receipt"] = CustSvc.SubmitNewOrder2(wipper.IMap, (IUser)Session["User"], SetupDto());
				return true;
			}

			wipper.Wip["Receipt"] = CustSvc.SubmitNewOrder2(wipper.IMap, (IUser)Session["User"], SetupDto(), (IAcctNotes)wipper.Wip["AcctNotes"]);
			return true;
		}


		INewOrderDTO SetupDto()
		{
			INewOrderDTO dto = CustSvc.GetNewOrderDTO();
			
			dto.OrderType = (DPI.Interfaces.OrderType)wipper.Wip["OrderType"];
			dto.MailAddr = (IAddr)wipper.Wip["MailAddr"];
			dto.SvcAddr = (IAddr)wipper.Wip["ServAddr"];
			dto.Cust = (ICustInfo2)wipper.Wip["CustInfo"];
			dto.Dmd = (IDemand)wipper.Wip["Demand"];
			dto.Zipcode = (string)wipper.Wip["Zip"];
			dto.ILEC = ((IILECInfo)wipper.Wip["SelectedIlec"]).ILECCode;
			dto.TransNum = ((int)wipper.Wip.WipId).ToString();
			dto.PayInfo = (IPayInfo)wipper.Wip["PayInfo"];
			dto.Receipt = (IReceipt)wipper.Wip["receipt"];

			return dto;
		}
		void HighTouchConf()
		{
			IReceipt receipt = CustSvc.GetReceipt((IDemand)wipper.Wip["Demand"]);
			wipper.Wip["receipt"] = receipt;
			wipper.Wip.BusObjId = ((IDemand)wipper.Wip["Demand"]).Id;
			wipper.Wip.BusObjType = "Demand";
		}
		bool IsHighTouch()
		{
			if ((bool)wipper.Wip["IsHighTouchDisallowed"])
				return false;

			return StoreSvc.IsRac_WF((IUser)Session["User"]);
		}

		void LinkObjects(bool isHighTouch)
		{
			wipper.IMap.add((IMapObj)wipper.Wip["ServAddr"]);
			wipper.IMap.add((IMapObj)wipper.Wip["MailAddr"]);
			wipper.IMap.add((IMapObj)wipper.Wip["PayInfo"]);			
			
			if (isHighTouch)
				((IPayInfo)wipper.Wip["PayInfo"]).Status = PaymentStatus.PendConfirm.ToString();

			CustSvc.PreSave(wipper.IMap);
			
			wipper.IMap.add((IMapObj)wipper.Wip["CustInfo"]);
			wipper.IMap.add((IMapObj)wipper.Wip["Demand"]);
			
			ICustInfo2 ci = (ICustInfo2)wipper.Wip["CustInfo"];
			ci.AccNumber = ((IReceipt)wipper.Wip["Receipt"]).AccNumber;
			ci.ServAddID  = ((IAddr2)wipper.Wip["ServAddr"]).AddressID;
			ci.MailAddID  = ((IAddr2)wipper.Wip["MailAddr"]).AddressID;

            ((IDemand)wipper.Wip["Demand"]).BillPayer = ci.AccNumber;
			((IDemand)wipper.Wip["Demand"]).ConsId = ci.CustInfoID;

			CustSvc.PreSave(wipper.IMap);
		}

		void CustomInit()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);

			btnNext.Visible = wipper.Wip.HasNext;
			btnPrevious.Visible = wipper.Wip.HasPrev; // && (wipper.Wip["Receipt"] == null);
			
			lblSubheader.Text = "Order Confirmation";

			if(HighTouch.IsHighTouch(wipper.Wip, (IUser)Session["User"]))
			{
				lblSubheader.Text = "Order Review";
				lblTotAmntPaid.Visible = false;
				lblPayType.Visible = false;
				lblPType.Visible = false;
				lblPaid.Visible = false;
			}
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());

			btnNext.Visible = false;
			lblError.Visible = true;
			lblError.Text = Const.GENERAL_ERROR;
		}
//		void SaveInternetPin()
//		{
//			if (wipper.Wip["Pin"] == null || ((string)wipper.Wip["Pin"]).Trim().Length == 0)
//				return;
//
//			((IReceipt)wipper.Wip["Receipt"]).Pin = (string)wipper.Wip["Pin"];
//		}

	#endregion
	}
}