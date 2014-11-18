namespace DPI.Ordering.control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.Security;
	using System.Security.Principal;
	using System.Web.SessionState;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	
	
	using DPI.ClientComp;
	using DPI.Interfaces;
	using DPI.Services;

	public class SideControl2 :  System.Web.UI.UserControl
	{

		
	#region Web Form Designer generated code
		#region Data 
		protected System.Web.UI.WebControls.ImageButton btnNewPayment;
		protected System.Web.UI.WebControls.ImageButton btnMonthlyPayment;
		protected System.Web.UI.WebControls.ImageButton btnCustomerInquiry;
		protected System.Web.UI.WebControls.ImageButton btnProductLookup;
		protected System.Web.UI.WebControls.ImageButton btnCellRecharge;
		protected System.Web.UI.WebControls.ImageButton btnInternet;
		protected System.Web.UI.WebControls.ImageButton btnLDCallingCard;
		protected System.Web.UI.WebControls.ImageButton btnNewOrder;
		protected System.Web.UI.WebControls.ImageButton btnOperations;
		protected System.Web.UI.WebControls.ImageButton btnReporting;
		protected System.Web.UI.WebControls.ImageButton btnDebitCard;
		protected System.Web.UI.WebControls.ImageButton btnReprintConfirm;
		protected System.Web.UI.WebControls.ImageButton btnReprintReceipt;
		protected System.Web.UI.WebControls.ImageButton btnDebitCardRedeem;
		protected System.Web.UI.WebControls.ImageButton btnDCTest;
		protected System.Web.UI.WebControls.ImageButton btnPendOrders;
		protected System.Web.UI.WebControls.ImageButton btnSatellite;
		protected System.Web.UI.WebControls.ImageButton btnReversalVoid;
		#endregion
	
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{
			this.btnNewOrder.Click += new System.Web.UI.ImageClickEventHandler(this.btnNewOrder_Click);
			this.btnNewPayment.Click += new System.Web.UI.ImageClickEventHandler(this.btnNewPayment_Click);
			this.btnMonthlyPayment.Click += new System.Web.UI.ImageClickEventHandler(this.btnMonthlyPayment_Click);
			this.btnCustomerInquiry.Click += new System.Web.UI.ImageClickEventHandler(this.btnCustomerInquiry_Click);
			this.btnProductLookup.Click += new System.Web.UI.ImageClickEventHandler(this.btnProductLookup_Click);
			this.btnCellRecharge.Click += new System.Web.UI.ImageClickEventHandler(this.btnCellRecharge_Click);
			this.btnInternet.Click += new System.Web.UI.ImageClickEventHandler(this.btnInternet_Click);
			this.btnLDCallingCard.Click += new System.Web.UI.ImageClickEventHandler(this.btnLDCallingCard_Click);
			this.btnDebitCard.Click += new System.Web.UI.ImageClickEventHandler(this.btnDebitCard_Click);
			this.btnDebitCardRedeem.Click += new System.Web.UI.ImageClickEventHandler(this.btnDebitCardRedeem_Click);
			this.btnReprintReceipt.Click += new System.Web.UI.ImageClickEventHandler(this.btnReprintReceipt_Click);
			this.btnReprintConfirm.Click += new System.Web.UI.ImageClickEventHandler(this.btnReprintReceipt_Click);
			this.btnReversalVoid.Click += new System.Web.UI.ImageClickEventHandler(this.btnReversalVoid_Click);
			this.btnPendOrders.Click += new System.Web.UI.ImageClickEventHandler(this.btnPendOrders_Click);
			this.btnReporting.Click += new System.Web.UI.ImageClickEventHandler(this.btnReporting_Click);
			this.btnOperations.Click += new System.Web.UI.ImageClickEventHandler(this.btnOps_Click);
			this.btnDCTest.Click += new System.Web.UI.ImageClickEventHandler(this.btnDCTest_Click);
			this.btnSatellite.Click += new System.Web.UI.ImageClickEventHandler(this.btnSatellite_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion

	#region Events Handlers
		
		void Page_Load(object sender, System.EventArgs e)
		{
		
		}		
		void CustomInit()
		{
			try
			{
				foreach(object ctrl in Controls)
					if (ctrl is System.Web.UI.WebControls.ImageButton || ctrl is System.Web.UI.WebControls.Button)
					{
						((System.Web.UI.WebControls.ImageButton)ctrl).Visible
							= HttpContext.Current.User.IsInRole(((System.Web.UI.WebControls.ImageButton)ctrl).ID.Substring(3));

						((System.Web.UI.WebControls.ImageButton)ctrl).Attributes.Add("onClick", "clickedButton=true; ");
					}

				btnDCTest.Visible = false;
				btnPendOrders.Visible = false;
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnNewOrder_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				SaveAndTransfer((ImageButton)sender, new NewOrderWip((IUser)Session["User"]));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnMonthlyPayment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				SaveAndTransfer((ImageButton)sender, new MonthlyPaymentWip((IUser)Session["User"]));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnReporting_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("ReportMain.aspx", false);
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnNewPayment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try 
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new NewPaymentWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}

		void btnCustomerInquiry_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new CustomerInquiryWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}

		void btnProductLookup_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new PriceLookupWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}

		void btnInternet_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new InternetWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnSatellite_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new SatelliteWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		
		void btnCellRecharge_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new WirelessWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));				
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnLDCallingCard_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new LongDistanceWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode));				
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnOps_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("Operations.aspx", false);
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnReversalVoid_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new ReversalVoidWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnDebitCard_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Response.Redirect("DCMain.aspx", false);
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}

		void btnReprintReceipt_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new ReprintReceiptWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnDebitCardRedeem_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new DCRedeemWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnDCTest_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				IUser user = (IUser)Session["User"];
				SaveAndTransfer((ImageButton)sender, new DCTestWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));
			}
			catch (Exception Ex)
			{
				ErrorHandler(Ex);
			}
		}
		void btnPendOrders_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
//			try
//			{
//				IUser user = (IUser)Session["User"];
//				SaveAndTransfer((ImageButton)sender, new PendOrdersWIP(user.DisplayName, user.ClerkId, user.LoginStoreCode));
//			}
//			catch (Exception Ex)
//			{
//				ErrorHandler(Ex);
//			}		
		}

	#endregion

	#region Implementation 
		void SaveAndTransfer(ImageButton ib, WIP wip)
		{
			IMap imap = IMapFactory.getIMap();
			imap.add(wip);
			ib.Page.Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr(ex, Session["User"], this.ToString());			
		}
	#endregion			

		
	}
}