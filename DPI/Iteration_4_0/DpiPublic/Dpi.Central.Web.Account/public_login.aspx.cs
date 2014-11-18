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
using Dpi.Central.Web.Account;

namespace Dpi.Central.Web
{
	public class PublicLogin : BaseLoginPage
	{
        protected System.Web.UI.WebControls.TextBox txtAccountNumber;
        protected System.Web.UI.WebControls.TextBox txtPassword;
        protected System.Web.UI.WebControls.TextBox txtNpa;
        protected System.Web.UI.WebControls.TextBox txtNxx;
        protected System.Web.UI.WebControls.TextBox txtNumber;
        protected System.Web.UI.WebControls.Label m_lblErrorMessage;
        protected HyperLink lnkForgotPwd;
        protected HyperLink lnkSignUp;
        protected Dpi.Central.Web.Controls.Footer _footer;
        protected ImageButton btnSubmit;
        protected HyperLink m_lnkNewAccount1;
        protected HyperLink m_lnkNewAccount2;	    	    
        protected HyperLink m_lnkNewAccount3;	    	    
	    
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.OnSubmitClick);
            this.Load += new System.EventHandler(this.OnPageLoad);

        }
		#endregion

	    #region OnPageLoad

	    private void OnPageLoad(object sender, EventArgs e) {
            if (!IsPostBack) {
                
                bool isComeFromMainPage = false;
                
                if (Request.Form["txtNpa"] != null)
                {
                    txtNpa.Text = Request.Form["txtNpa"];
                    isComeFromMainPage = true;
                }
                
                if (Request.Form["txtNxx"] != null)
                {
                    txtNxx.Text = Request.Form["txtNxx"];
                    isComeFromMainPage = true;
                }
                
                if (Request.Form["txtNumber"] != null)
                {
                    txtNumber.Text = Request.Form["txtNumber"];
                    isComeFromMainPage = true;
                }
                    
                if (Request.Form["txtAccountNumber"] != null)
                {
                    txtAccountNumber.Text = Request.Form["txtAccountNumber"];
                    isComeFromMainPage = true;
                }
                    
                if (Request.Form["txtPassword"] != null)
                {
                    txtPassword.Text = Request.Form["txtPassword"];
                    isComeFromMainPage = true;
                }
                
                if (isComeFromMainPage)
                    OnSubmitClick(this, null);
            }

            lnkForgotPwd.NavigateUrl = SiteMap.PASSWORD_REMINDER_URL;
            lnkSignUp.NavigateUrl = SiteMap.SIGN_UP_URL;
            m_lnkNewAccount1.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;
            m_lnkNewAccount2.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;			
            m_lnkNewAccount3.NavigateUrl = SiteMap.NEW_ACC_SELECT_PROVIDER_URL;			
	    }

	    #endregion

	    #region OnSubmitClick

	    private void OnSubmitClick(object sender, ImageClickEventArgs e) {
                        
	        try {
	            LoginResult result = Login(
	                txtAccountNumber.Text.Trim(), txtNpa.Text.Trim() + txtNxx.Text.Trim() + txtNumber.Text.Trim(),
	                txtPassword.Text.Trim());

	            m_lblErrorMessage.Text = GetUIMessage(result);
                
	        } catch (Exception ex) {
	            // This is only for unexpected.  No business logic should end up here.
	            m_lblErrorMessage.Text = "Error: " + ex.Message;
	        }
	    }

	    #endregion
	    

	}
}
