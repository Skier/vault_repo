using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class AccountSellingPutInfo : Page
    {        
        #region Web Form Designer generated code

        protected RequiredFieldValidator vldRfAccountNumber;
        protected RegularExpressionValidator vldReAccountNumber;
        protected RequiredFieldValidator vldRfEmail;
        protected RegularExpressionValidator vldReEmail;
        protected RequiredFieldValidator vldRfPassword;
        protected CompareValidator vldCmpConfirmPassword;
        protected RequiredFieldValidator vldRfAccountLastName;
        protected ImageButton m_btnSubmit;
        protected System.Web.UI.WebControls.Label m_lblCaption;
        protected System.Web.UI.WebControls.Label m_lblName;
        protected System.Web.UI.WebControls.TextBox m_txtName;
        protected System.Web.UI.WebControls.Label m_lblCompany;
        protected System.Web.UI.WebControls.TextBox m_txtCompany;
        protected System.Web.UI.WebControls.Label m_lblAddress;
        protected System.Web.UI.WebControls.TextBox m_txtAddress;
        protected System.Web.UI.WebControls.Label m_lblMobilePhone;
        protected System.Web.UI.WebControls.TextBox m_txtMobilePhone;
        protected System.Web.UI.WebControls.Label m_lblOfficePhone;
        protected System.Web.UI.WebControls.TextBox m_txtOfficePhone;
        protected System.Web.UI.WebControls.Label m_lblEmail;
        protected System.Web.UI.WebControls.TextBox m_txtEmail;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);            
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.OnSubmitClick);
            this.Load += new EventHandler(OnPageLoad);
        }

        #endregion

        #region OnPageLoad

        private void OnPageLoad(object sender, EventArgs e) {
            m_btnSubmit.Attributes.Add("onclick", "return checkValues();");
        }

        #endregion

        #region OnSubmitClick

        private void OnSubmitClick(object sender, ImageClickEventArgs e) {
            SendEmail();   
            Response.Redirect("account_selling_get_doc.aspx");
        }

        #endregion

        #region SendEmail

        private void SendEmail()
        {
            string emailBody = string.Empty;
                        
            emailBody += string.Format("Name: {0} \n", m_txtName.Text);
            emailBody += string.Format("Company: {0} \n", m_txtCompany.Text);
            emailBody += string.Format("Address: {0} \n", m_txtAddress.Text);
            emailBody += string.Format("Mobile Phone: {0} \n", m_txtMobilePhone.Text);
            emailBody += string.Format("Office Phone: {0} \n", m_txtOfficePhone.Text);
            emailBody += string.Format("Email: {0} \n", m_txtEmail.Text);
            
            MailMessage message = new MailMessage();
            //TODO: email address goes here
            message.AddEmailTo(ConfigurationSettings.AppSettings["DpiEmail"]);
            message.EmailFrom = m_txtEmail.Text;
            message.EmailSubject = "This person viewed document \"10 Things You Need to Know About Selling Your Telecom Accounts.\"";
            message.EmailMessage = emailBody;

            message.SendMail();
            
        }

        #endregion
        
    }
}