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
	public class PendingConf : BasePage
	{
		protected System.Web.UI.WebControls.Image imgWorkflow;
		protected System.Web.UI.WebControls.ValidationSummary ValidationSummary1;
		protected System.Web.UI.WebControls.ImageButton ImageButton1;
		protected System.Web.UI.WebControls.ImageButton btn_GotoMain;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.PlaceHolder phVoidedTran;
		protected System.Web.UI.WebControls.Label lblErrMsg;
	
		private void Page_Load(object sender, System.EventArgs e)
		{	
			btnPrint.Attributes.Add("onClick", "window.open('PrintVoid.aspx', '_blank' ,'height= 600, width=660 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=no,resizable=yes')");
			try
			{
				phVoidedTran.Controls.Add(VoidManager.PrintVoidedTran(wipper.IMap, 
					(VoidTranType)wipper.Wip["TranType"], (int)wipper.Wip["Tran"]));

//				ILocalTransactionInfo tinfo = LocalTransactionSvc.FindTran(wipper.IMap, (int)wipper.Wip["Tran"]);
//				lblTransNum.Text = tinfo.Transaction_Id.ToString();
//				lblAccNum.Text =  tinfo.AccNumber.ToString();
//				lblPayType.Text = tinfo.Transaction_Type_Id.ToString();
//				lblPhoneNum.Text = "N/A";
//				if(tinfo.PhNumber != null)
//					lblPhoneNum.Text = string.Format("{0}-{1}-{2}",tinfo.PhNumber.ToString().Substring(0,3),tinfo.PhNumber.ToString().Substring(3,3), tinfo.PhNumber.ToString().Substring(6,4));
//				
//				lblDate.Text = tinfo.PayDate.ToShortDateString();
//				lblLocAmnt.Text = tinfo.LocalAmount.ToString();
//				lblLDAmnt.Text = tinfo.LDAmount.ToString();	
//				lblCurrDate.Text = DateTime.Now.ToShortDateString() + " : " + DateTime.Now.ToShortTimeString();
				//lbl_VoidText.Text = "You have successfully deleted transaction # <font color='red'>" + wipper.Wip["Tran"]+"</font>";
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);	
			InitializeComponent();
			
			imgWorkflow.ImageUrl = wipper.Wip.Workflow.ImageTag.ToString();

		}

		private void InitializeComponent()
		{    
			this.btnPrint.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
			this.btn_GotoMain.Click += new System.Web.UI.ImageClickEventHandler(this.btn_GotoMain_Click);
			this.ImageButton1.Click += new System.Web.UI.ImageClickEventHandler(this.ImageButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		void SaveAndTransfer(ImageButton ib, WIP wip)
		{
			IMap imap = IMapFactory.getIMap(); // instead call side control button
			imap.add(wip);
			ib.Page.Session["IMap"] = imap;
			Response.Redirect(wip.Current(), false);
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			IUser user = (IUser)Session["User"];
			SaveAndTransfer((ImageButton)sender, new ReversalVoidWip(user.DisplayName, user.ClerkId, user.LoginStoreCode));

		//	SaveAndTransfer((ImageButton)sender, new ReversalVoidWip(HttpContext.Current.User.Identity.Name));
		}

		private void ImageButton2_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);
		}

		private void btn_GotoMain_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["IMap"] = null;
			Response.Redirect("MenuScreen.aspx", false);
		}

		private void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
	}
}
