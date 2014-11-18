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

	public class CustRecurringPymts : BasePage
	{ 	
	
		#region Web Form Designer generated code
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.PlaceHolder phOrderSummary;
		protected System.Web.UI.WebControls.TextBox txtLDsrvc;
		protected System.Web.UI.WebControls.ImageButton btnGotoMain;
		protected System.Web.UI.WebControls.PlaceHolder phCustAcctInfo;
		protected System.Web.UI.WebControls.LinkButton lbAdd;
		protected System.Web.UI.WebControls.LinkButton lbEdit;
		protected System.Web.UI.WebControls.LinkButton lbDisable;
		protected int selectedPage;
		protected System.Web.UI.WebControls.Label lblAccNumber;
		protected string mode = "";
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		
		private void InitializeComponent()
		{  
			this.lbAdd.Click += new System.EventHandler(this.lbAdd_Click);
			this.lbEdit.Click += new System.EventHandler(this.lbEdit_Click);
			this.lbDisable.Click += new System.EventHandler(this.lbDisable_Click);
			this.btnPrevious.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrevious_Click);
			this.btnGotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btnGotoMain_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
		
		#region Event Handlers
		void CustomInit()
		{
			try
			{
				SetAttrs();
				CreateTable();
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}	
		}	
		void Page_Load(object sender, System.EventArgs e)
		{	
			try 
			{			
				if(IsPostBack)
					return;
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}			
		}
		void btnGotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				Session["IMap"] = null;
				Response.Redirect("MenuScreen.aspx", false);
			}
			catch(Exception ex)
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
		void lbDisable_Click(object sender, System.EventArgs e)
		{
			DisableAll();
			SetAcctActivityLog("Disabled all recurring payments");
			CustSvc.PreSave(wipper.IMap);
			Response.Redirect(wipper.Wip.Prev(), false);
		}
		void lbEdit_Click(object sender, System.EventArgs e)
		{
			try
			{			
				wipper.Wip["OperationMode"] = "edit";
				if ((int)wipper.Wip["RecurringId"] == 0)
				{
					ShowErr("Please select an account");
					return;
				}

				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}		
		void lbAdd_Click(object sender, System.EventArgs e)
		{
			try 
			{
				wipper.Wip["OperationMode"] = "add";
				Response.Redirect(wipper.Wip.Next(), false);
			}
			catch (Exception ex)
			{	
				ErrorHandler(ex);
			}			
		}

		#endregion

		#region Implementation
		void SetAttrs()
		{
			SetDemand();
			lblErrMsg.Visible = false;
			btnPrevious.Visible = true;
			MultiClickBlocker.Block(this, btnPrevious);
			MultiClickBlocker.Block(this, btnGotoMain);
			lblAccNumber.Text   = ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber.ToString();
		}
		void SetDemand()
		{
			((IDemand)wipper.Wip["Demand"]).DmdType = DemandType.RecurringPymts.ToString();
			((IDemand)wipper.Wip["Demand"]).BillPayer = ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber;
		}
		void CreateTable()
		{
			if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length == 0)
			{
				wipper.Wip["OperationMode"] = "add";
				Response.Redirect(wipper.Wip.Next(), false);
			}

			phCustAcctInfo.Controls.Add(new TableRecurringCustInfo(new EventHandler(RecurringSelection), (ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"], (int)wipper.Wip["RecurringId"]));
		}

		void RecurringSelection(object sender, EventArgs e)
		{
			wipper.Wip["RecurringId"] = int.Parse(((WebControl)sender).ID);			
		}
		
		int SelectIndex(int recId)
		{
			for (int i = 0; i <((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length; i++)
				if (((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i].Id == recId)
					return i;

			return 0;
		}

		void ShowErr(string msg)
		{
			lblErrMsg.Text = msg;
			lblErrMsg.Visible = true;
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
		void DisableAll()
		{
			for (int i = 0; i < ((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"]).Length; i++)
				((ICustomerRecurringPayment[])wipper.Wip["CustomerROPs"])[i].Active = false;			
		}
		void SetAcctActivityLog(string msg)
		{
			OperSvc.SetAcctActivityLog(wipper.IMap, ((IAcctInfo)wipper.Wip["AcctInfo"]).AccNumber, msg);
		}
	#endregion		


		
	}
}