using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dpi.Central.Web.Account.Payment
{
    public class AccountInfoControl : UserControl
    {
        #region Web Form Designer generated code

        protected Label lblPhoneNumber;
        protected TextBox txtFirstName;
        protected TextBox txtLastName;
        protected TextBox txtAddr;
        protected TextBox txtCity;
        protected DropDownList lstState;
        protected TextBox txtZip;
        protected TextBox txtEmail;
        protected RequiredFieldValidator vldRfFirstName;
        protected RequiredFieldValidator vldRfLastName;
        protected RequiredFieldValidator vldRfAddr;
        protected RequiredFieldValidator vldRfCity;
        protected RequiredFieldValidator vldRfZip;
        protected RequiredFieldValidator vldRfEmail;
        protected RegularExpressionValidator vldReEmail;
        protected RegularExpressionValidator vldReZip;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReNpa;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReNxx;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfNpa;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfNxx;
        protected Dpi.Central.Web.Controls.PhoneNumberBox phnPhoneNumber;
        protected System.Web.UI.WebControls.CustomValidator vldCstPhoneNumber;
        protected System.Web.UI.HtmlControls.HtmlImage Img2;
        protected System.Web.UI.HtmlControls.HtmlImage Img3;
        protected System.Web.UI.HtmlControls.HtmlImage Img4;
        protected System.Web.UI.HtmlControls.HtmlImage Img5;
        protected System.Web.UI.HtmlControls.HtmlImage Img6;
        protected System.Web.UI.WebControls.TextBox txtAcctNumber;
        protected System.Web.UI.HtmlControls.HtmlGenericControl rowAccountNumber;
        protected System.Web.UI.HtmlControls.HtmlGenericControl rowPhoneNumber;
        protected RequiredFieldValidator vldRfState;

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vldCstPhoneNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.PhoneNumberServerValidate);

        }

        #endregion

        private void PhoneNumberServerValidate(object source, ServerValidateEventArgs args) 
        {
            args.IsValid = phnPhoneNumber.IsValid;
        }

        #region Properties

        public short FirstTabIndex
        {
            set
            {
                txtAcctNumber.TabIndex = value++;
                phnPhoneNumber.TabIndex = value;
                value += 3;
                txtFirstName.TabIndex = value++;
                txtLastName.TabIndex = value++;
                txtAddr.TabIndex = value++;
                txtCity.TabIndex = value++;
                lstState.TabIndex = value++;
                txtZip.TabIndex = value++;
                txtEmail.TabIndex = value++;
            }
        }

        public short LastTabIndex
        {
            get { return txtEmail.TabIndex; }
        }

        public bool VisibilityForAccountNumber
        {
            set { rowAccountNumber.Visible = value; }
        }

        public bool VisibilityForPhoneNumber
        {
            set { rowPhoneNumber.Visible = value; }
        }

        public bool EnabledPhoneNumber
        {
            set { phnPhoneNumber.Enabled = value; }
        }

        public string AccountNumber
        {
            get { return txtAcctNumber.Text; }
            set { txtAcctNumber.Text = value; }
        }

        public string PhoneNumber
        {
            get { return phnPhoneNumber.PhoneNumber; }
            set { phnPhoneNumber.PhoneNumber = value; }
        }

        public string FirstName
        {
            get { return txtFirstName.Text; }
            set { txtFirstName.Text = value; }
        }

        public string LastName
        {
            get { return txtLastName.Text; }
            set { txtLastName.Text = value; }
        }

        public string StreetAddress
        {
            get { return txtAddr.Text; }
            set { txtAddr.Text = value; }
        }

        public string City
        {
            get { return txtCity.Text; }
            set { txtCity.Text = value; }
        }

        public string State
        {
            get { return lstState.SelectedValue; }
            set { lstState.SelectedValue = value; }
        }

        public string Zip
        {
            get { return txtZip.Text; }
            set { txtZip.Text = value; }
        }

        public string Email
        {
            get { return txtEmail.Text; }
            set { txtEmail.Text = value; }
        }

        #endregion
    }
}