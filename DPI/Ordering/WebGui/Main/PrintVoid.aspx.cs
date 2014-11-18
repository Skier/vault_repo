using System;
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
	public class PrintVoid : System.Web.UI.Page 
	{	 	
		#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm form_ProductL1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrint;
		protected System.Web.UI.WebControls.Image Image1;
		protected System.Web.UI.WebControls.Button btnClose;
		protected System.Web.UI.WebControls.PlaceHolder phVoidedTran;
		
		override protected void OnInit(EventArgs ea)
		{
			InitializeComponent();
			base.OnInit(ea);
		}

		private void InitializeComponent()
		{    
			this.btnPrint.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void Page_Load(object sender, System.EventArgs e)
		{
			btnPrint.Attributes.Add("onClick", "window.print()");
			btnClose.Attributes.Add("onClick", "window.close()");
			try
			{
				IMap imap = (IMap)Session["IMap"];

				if (imap == null)
					throw new ApplicationException("No WIP");

				WIP wip = (WIP)imap.find(WIP.IKeyS);

				phVoidedTran.Controls.Add(VoidManager.PrintVoidedTran(imap, 
					(VoidTranType)wip["TranType"], (int)wip["Tran"]));
//					
//				ILocalTransactionInfo tinfo = LocalTransactionSvc.FindTran(imap, (int)wip["Tran"]);
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
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}

		void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}		
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( (IMap)Session["IMap"], ex, Session["User"], this.ToString());
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
	}
}