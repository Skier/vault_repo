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
using DPI.ClientComp;

namespace DPI.Ordering
{
	public class SatelliteSchedular : BasePage
	{
		protected System.Web.UI.WebControls.PlaceHolder phFrame;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnSelVendor;

	#region Data
		protected System.Web.UI.HtmlControls.HtmlForm Form1;			
	#endregion

	#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			//base.OnInit(e);
			CustomInit();
		}
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
	#endregion

	#region Event Handlers		
		void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
//				IUser user = (IUser)Session["User"];
//
//				IUserAccountExtension userExt = UserAccountSvc.GetExtendedUser(((IUser)Session["User"]).AcctId, "DirectNetwork");
//				string s = UserAccountSvc.GetServicePwrFullUrl(((IUser)Session["User"]).AcctId, "DirectNetwork");
//				string p = s;
				
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
		void CustomInit()
		{
			try
			{
				phFrame.Controls.Add(new TableScheduleInstallation(GetSvcPwrFrameUrl()));
			}
			catch (Exception ex)
			{
				ErrorHandler(ex);
			}
		}
	#endregion
		
	#region Implementations	
		void ErrorHandler(Exception ex)
		{
			FatalError.ShowErr(phFrame, ex.Message);
			FatalError.SaveErr(ex, (IUser)Session["User"], this.ToString());
		}			
		
		string GetSvcPwrFrameUrl()
		{
			IUserAccountExtension userExt = UserAccountSvc.GetExtendedUser(((IUser)Session["User"]).AcctId, hdnSelVendor.Value);
			//@"<iframe src='http://fssdev.servicepower.com/AW/LoginAction.do?fromRW=Y&loginName=dvd00001&loginPassword=dvdtlaad' width='663' height='650' align='left' scrolling='yes'>";
			return "<iframe src='" + userExt.Url + "&loginName=" + userExt.UserName + "&loginPassword=" + userExt.Password + "' width='663' height='650' align='left' scrolling='yes'>";
		}
	#endregion
		
	}
}