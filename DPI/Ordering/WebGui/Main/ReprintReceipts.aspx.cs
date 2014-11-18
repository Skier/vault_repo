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
	public class ReprintReceipts : BasePage
	{
	
	#region Web Form Designer generated code
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton1;
		protected System.Web.UI.WebControls.RadioButton RadioButton2;
		protected System.Web.UI.WebControls.RadioButton RadioButton3;
		protected System.Web.UI.WebControls.RadioButton RadioButton4;
		protected System.Web.UI.WebControls.RadioButton RadioButton5;
		protected System.Web.UI.WebControls.RadioButton RadioButton6;
		protected System.Web.UI.WebControls.RadioButton RadioButton7;
		protected System.Web.UI.WebControls.Label lblNoReceipts;
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
		
		void Page_Load(object sender, System.EventArgs e)
		{
			SetAttAndMultiBlocker();
			Validate();

			try
			{
			    GetOrders();
			 	PlaceHolder1.Controls.Add(new ReprintReceipt(new EventHandler(CbCheckChanged), ((IOrder[])wipper.Wip["Orders"])));
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);		
		}
		void CbCheckChanged(object sender, EventArgs ea)
		{
			IOrder[] orders = (IOrder[])wipper.Wip["Orders"];
			for ( int i = 0; i < orders.Length; i++)
				if (orders[i].Id.ToString() + orders[i].PayInfoId.ToString() == ((WebControl)sender).ID.ToString())
					wipper.Wip["Order"] =  orders[i];

			btnNext.Visible = wipper.Wip.HasNext;
		}		
		void btnNext_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{		
			try
			{
				SetBusObj();

				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	
	#endregion

	#region Implementation
		void CustomInit()
		{
			EstNextButton();
			lblNoReceipts.Visible = false;
		}		
		void EstNextButton()
		{
			btnNext.Visible = false; //set to false for web control

			if (((IDemand)wipper.Wip["demand"] != null)
				&& (wipper.Wip.HasNext))
					btnNext.Visible = true;
		}
		void SetBusObj()
		{
			IOrder order = (IOrder)wipper.Wip["Order"];
			wipper.Wip["Demand"] = StoreSvc.FindDemand(wipper.IMap, order.Id);
			wipper.Wip["PayInfo"] = PaySvc.FindPayInfo(wipper.IMap, order.PayInfoId);
			wipper.Wip["IsHightouch"] = ((IPayInfo)wipper.Wip["payinfo"]).IsConfReq;
			wipper.Wip["Receipt"] = CustSvc.GetReceipt(
				((IDemand)wipper.Wip["demand"]).Id, 
				order.ConfNumber, 
				order.AccNumber
				);
			SetBusObj((IDemand)wipper.Wip["demand"]);			
		}
		void SetBusObj(IDemand demand)
		{
			if (demand.DmdType.ToLower() == DemandType.Monthly.ToString().ToLower())
			{
				wipper.Wip["CustInfoExt"] = CustSvc.GetCustInfoExt(wipper.IMap, demand.BillPayer);
				wipper.Wip["acctinfo"]    = CustSvc.GetAcctInfo   (wipper.IMap, demand.BillPayer);
				return;
			}

			if (demand.DmdType.ToLower() == DemandType.NewPymt.ToString().ToLower())
			{
				wipper.Wip["OrderSummary"] = CustSvc.GetOrderSummary(wipper.IMap, demand);
				return;
			}

			if (((IDemand)wipper.Wip["demand"]).DmdType.ToLower() == DemandType.New.ToString().ToLower())
			{
				wipper.Wip["CustInfo"] = CustSvc.GetCustInfoExtPend(wipper.IMap, (IDemand)wipper.Wip["demand"]);
				wipper.Wip["OrderSummary"] = CustSvc.GetOrderSummary(wipper.IMap, (IDemand)wipper.Wip["demand"] );
				return;
			}

			throw new ArgumentException("reprint Order Type '" +  demand.DmdType + "' is unavailable.");
		}
		void GetOrders()
		{
			if (wipper.Wip["Orders"] == null)
				wipper.Wip["Orders"]  = StoreSvc.GetOrderByStore(
										wipper.IMap, 
										(IUser)Session["User"],
										(DateTime)wipper.Wip["fromDate"],
										(DateTime)wipper.Wip["toDate"]);	
				
			if (((IOrder[])wipper.Wip["Orders"]).Length == 0)
			{
				lblNoReceipts.Text = "No orders found for " + ((DateTime)wipper.Wip["fromDate"]).ToShortDateString();
				lblNoReceipts.Visible = true;
			}	
		}

		void ValidateIt()
		{
			if(HttpContext.Current.Session["User"] == null)
			{
				ErrLogSvc.LogError(
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, "There is no IUser.");

				Response.Redirect( "Timeout.htm", false);
			}

			if(((IUser)HttpContext.Current.Session["User"]).LoginStoreCode == null)																	 
			{
				ErrLogSvc.LogError(
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, "The account has no storecode");

				Response.Redirect( "Timeout.htm", false);
			}
			
			if(((IUser)HttpContext.Current.Session["User"]).LoginStoreCode.Trim().Length == 0 )																	 
			{
				ErrLogSvc.LogError(
					wipper.IMap, this.ToString(), HttpContext.Current.User.Identity.Name, "The account has no storecode");

				Response.Redirect( "Timeout.htm", false);
			}
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			btnNext.Visible = false;
			lblErrMsg.Visible = true;
			lblErrMsg.Text = Const.GENERAL_ERROR;
		}
		void SetAttAndMultiBlocker()
		{
			btnNext.Attributes.Add("onclick","return confirmButton();");
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, ImageButton1);
		}
	#endregion
	}
}