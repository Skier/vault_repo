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

namespace DPI.Ordering.Main
{
	public class DebCardRedeem : BasePage
	{
		
	#region Web Form Designer generated code
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label lblNoVoids;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.PlaceHolder PlaceHolder1;
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}

		private void InitializeComponent()
		{    
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion	

	#region Event Handlers	
		void CustomInit()
		{
			try
			{
				btnNext.Visible = wipper.Wip["Tran"] != null;
				imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();
				CreateTable();
			}
			catch(Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				MultiBlockAndAtt();
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void CbCheckChanged(object sender, EventArgs ea)
		{
			try
			{
				wipper.Wip["Tran"] = WirelessTranSvc.GetWSTran(wipper.IMap, int.Parse(((WebControl)sender).ID));
				this.btnNext.Visible = true;
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
				wipper.Wip = GetWip();

				BuildDmdGroup();
				
				if (wipper.Wip is DCRedeemWip)
				{
					Response.Redirect(wipper.Wip.Next(), false); 
					return;
				}
				Response.Redirect(wipper.Wip.Current(), false); // for reload need to go to the first page
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		
		void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("MenuScreen.aspx", false);		
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		
	#endregion

	#region Implementation
		WIP GetWip()
		{
			
			IWireless_Transactions tran = (IWireless_Transactions)wipper.Wip["tran"];
			IUser user = (IUser)Session["User"];
			int vendor = WirelessTranSvc.GetWirelessProd(wipper.IMap, tran.Wireless_product_ID).Vendor_id;
	
			switch(vendor)
			{
				case 127 :
					return wipper.Wip; // keep existing

				case 128 :
				{
					WIP wip = new DebCardReloadWip(user.DisplayName, user.ClerkId, tran.StoreCode); 
					wip["Tran"] = tran;
					return wip;
				}
				default :
					throw new ArgumentException("Unknown vendor: " + vendor.ToString());
			}
		}

		void CreateTable()
		{
			IWireless_Transactions[] xacts = WirelessTranSvc.GetNonRedeemedTrans(wipper.IMap);
			if (xacts.Length == 0)
			{
				lblErrMsg.Visible = true;
				lblErrMsg.Text = "No Redeemable tranctions";
				return;
			}

			PlaceHolder1.Controls.Add(
				new TableDebCardNotRedeemed(wipper.IMap, new EventHandler(CbCheckChanged), xacts,  GetTranId()));
		}
		int GetTranId()
		{
			if (wipper.Wip["Tran"] ==  null)
				return 0;

            return ((IWireless_Transactions)wipper.Wip["Tran"]).Wireless_Transaction_ID;
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr((IMap)Session["IMap"], ex, Session["User"], this.ToString());
			btnNext.Visible = false;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
		}	

		void MultiBlockAndAtt()
		{
			//	btnNext.Attributes.Add("onclick","return confirmButton();");
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, ImageButton1);
		}

		void BuildDmdGroup()
		{ 
			if (FindDemandGroup())
				return;

			((IWireless_Transactions)wipper.Wip["tran"]).Status = "Pend";
			SetupDemand();
			SetupDmdItem();
			SetupPayInfo();
			SetupDebCardApp();
		}
		bool FindDemandGroup()
		{
			return false;
		}
		void SetupDemand()
		{	
			wipper.Wip["Demand"] =	MakeDmd(); 

			IWireless_Transactions tran = (IWireless_Transactions)wipper.Wip["Tran"];

			((IDemand)wipper.Wip["Demand"]).ConsumerAgent = tran.Clerkid;     //  This uses Wireless Xact StoreCode &
			((IDemand)wipper.Wip["Demand"]).StoreCode     = tran.StoreCode;   //  ClerkId instead of IUser 

			((IDemand)wipper.Wip["Demand"]).Loc           = LocSvc.FindStoreZipId(wipper.IMap, tran.StoreCode);
			((IDemand)wipper.Wip["Demand"]).Status        = DemandStatus.Pend.ToString();
		}
		IDemand MakeDmd()
		{
			if (wipper.Wip["Demand"] != null)
				((IDemand)wipper.Wip["Demand"]).delete();

			if (wipper.Wip is DCRedeemWip )
				return DmdFactory.GetDemand(wipper.IMap, DemandType.DebCardNew.ToString(), true); 

			if (wipper.Wip is DebCardReloadWip)
				return DmdFactory.GetDemand(wipper.IMap, DemandType.DebCardReload.ToString(), true);

			throw new ArgumentException("Unknown Wip type: " +  wipper.Wip.ToString());

		}
		void SetupDmdItem()
		{	
			IWireless_Transactions tran = (IWireless_Transactions)wipper.Wip["Tran"];
			wipper.Wip["DebCard"] = DebCardSvc.GetDebCardProd(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode);
			
			wipper.Wip["DItem"] 
				= ProdSvc.AddDmdItem(wipper.IMap, (IDemand)wipper.Wip["Demand"],
				                    (IProdPrice)wipper.Wip["DebCard"], 
				                    GetOrderType()); 

			((IDmdItem)wipper.Wip["DItem"])
				.AdjustPrice(tran.Tran_Amount - GetFeeAmount((IDmdItem)wipper.Wip["DItem"]));
		}
		OrderType GetOrderType()
		{
			if (((IDemand)wipper.Wip["Demand"]).DmdType.Trim().ToLower() 
				== DemandType.DebCardReload.ToString().Trim().ToLower()) 
					return OrderType.Add;		
			
			if (((IDemand)wipper.Wip["Demand"]).DmdType.Trim().ToLower() 
				== DemandType.DebCardNew.ToString().Trim().ToLower()) 
					return OrderType.New;

			throw new ArgumentException("Unknown Debit Card Demand Type: " 
				                         +  ((IDemand)wipper.Wip["Demand"]).DmdType);
		}
		void SetupPayInfo()
		{
			IWireless_Transactions tran = (IWireless_Transactions)wipper.Wip["Tran"];
			
			if (wipper.Wip["PayInfo"] != null)
				((IPayInfo)wipper.Wip["PayInfo"]).delete();

			wipper.Wip["PayInfo"] 
				= PaySvc.GetNewPayInfo(wipper.IMap,(IDemand)wipper.Wip["Demand"], PayInfoClass.PayInfo);
			
			((IPayInfo)wipper.Wip["PayInfo"]).ParDemand       = (IDemand)wipper.Wip["Demand"];
			((IPayInfo)wipper.Wip["PayInfo"]).TotalAmountPaid = tran.Tran_Amount; 
			((IPayInfo)wipper.Wip["PayInfo"]).AmountTendered  = tran.Tran_Amount;
			((IPayInfo)wipper.Wip["PayInfo"]).PaymentType     = PaymentType.TurboCash;
			((IPayInfo)wipper.Wip["PayInfo"]).Status          = PaymentStatus.Pend.ToString();
			((IPayInfo)wipper.Wip["PayInfo"]).TranNumber	  = tran.Wireless_Transaction_ID;
		}
		decimal GetFeeAmount(IDmdItem di)
		{
			decimal fees = 0m;
			
			for (int i = 0; i <	di.TagAlongs.Length; i++)
				fees +=   di.TagAlongs[i].PriceAmt;

			return fees;
		}
		void SetupDebCardApp()
		{
			if ((ICardApp)wipper.Wip["DebCardApp"] != null)
				((ICardApp)wipper.Wip["DebCardApp"]).delete();

			wipper.Wip["DebCardApp"] = DebCardSvc.GetDebitCardApp(wipper.IMap, (IDemand)wipper.Wip["Demand"]);
			((ICardApp)wipper.Wip["DebCardApp"]).Status = CardAppStatus.Pend.ToString();
			((ICardApp)wipper.Wip["DebCardApp"]).Verified = false;

		}
	#endregion
	}
}