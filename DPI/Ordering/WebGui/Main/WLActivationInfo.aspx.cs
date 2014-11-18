using System;

using DPI.Services;
using DPI.Interfaces;
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class WLActivationInfo : BasePage
	{

	#region Web Form Designer generated code

		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Label lblErrMsg;
		protected System.Web.UI.WebControls.ImageButton btnPrevious;
		protected System.Web.UI.WebControls.ImageButton btnNext;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label3;
		protected System.Web.UI.WebControls.Label Label4;
		protected System.Web.UI.WebControls.Label Label2;
		
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
			CustomInit();
		}
		void InitializeComponent()
		{    
			this.btnNext.Click += new System.Web.UI.ImageClickEventHandler(this.btnNext_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion		

	#region Event Handlers
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				btnNext.Visible = true;				
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
				Response.Redirect(wipper.Wip.Next(), false);
			} 
			catch (Exception ex) {ErrorHandler(ex); }		
		}		
		void CustomInit()
		{
			try
			{
				btnPrevious.Visible = btnNext.Visible = false;
				MultiBlock();		
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion
	
	#region Implementation

		void MultiBlock()
		{
			MultiClickBlocker.Block(this, btnNext);
			MultiClickBlocker.Block(this, btnPrevious);						
		}		
		void ErrorHandler(Exception ex)
		{
			FatalError.SaveErr( wipper.IMap, ex, Session["User"], this.ToString());				
			lblErrMsg.Text = Const.GENERAL_ERROR;
			lblErrMsg.Visible = true;
		}
	#endregion
	}
}