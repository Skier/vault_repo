using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Account;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class PasswordReminder : BaseImageAccountPage
    {
        #region Constants

        private const string BOTH_VALUES_ARE_BLANK = "Both Phone Number and Account Number cannot be left blank";
        private const string BOTH_VALUES_ARE_ENTERED = "Both Phone Number and Account Number cannot be entered";

        #endregion

        #region Web Form Designer generated code

        protected ImageButton btnSubmit;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReAccountNumber;
        protected Dpi.Central.Web.Controls.PhoneNumberBox phnPhoneNumber;
        protected System.Web.UI.WebControls.CustomValidator vldCstPhoneNumber;
        protected System.Web.UI.WebControls.CustomValidator vldCstIdentity;
        protected System.Web.UI.HtmlControls.HtmlGenericControl divPhoneNumberRow;
        protected System.Web.UI.HtmlControls.HtmlGenericControl divAccountNumberRow;
        protected System.Web.UI.HtmlControls.HtmlGenericControl divButtonsRow;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfPhoneNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfAccountNumber;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkOrdinarySignUp;
        protected System.Web.UI.HtmlControls.HtmlAnchor lnkWirelessSignUp;
        protected System.Web.UI.WebControls.RadioButton rbtnWireless;
        protected System.Web.UI.WebControls.RadioButton rbtnOrdinary;
        protected TextBox txtAccountNumber;

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
            this.vldCstIdentity.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstIdentity_ServerValidate);
            this.vldCstPhoneNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstPhoneNumber_ServerValidate);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.SubmitHandler);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region Event Handler

        private void Page_Load(object sender, System.EventArgs e) 
        {
            rbtnWireless.Attributes.Add("onclick", "ShowPhoneNumber();");
            rbtnOrdinary.Attributes.Add("onclick", "ShowAccountNumber();");

            if (rbtnWireless.Checked) {
                divPhoneNumberRow.Style.Add("display", "visible");
                divAccountNumberRow.Style.Add("display", "none");
                divButtonsRow.Style.Add("display", "visible");
                vldRfPhoneNumber.Enabled = true;
                vldCstPhoneNumber.Enabled = true;
                vldRfAccountNumber.Enabled = false;
                vldReAccountNumber.Enabled = false;
            } else if (rbtnOrdinary.Checked) {
                divPhoneNumberRow.Style.Add("display", "none");
                divAccountNumberRow.Style.Add("display", "visible");
                divButtonsRow.Style.Add("display", "visible");
                vldRfPhoneNumber.Enabled = false;
                vldCstPhoneNumber.Enabled = false;
                vldRfAccountNumber.Enabled = true;
                vldReAccountNumber.Enabled = true;
            } else {
                divPhoneNumberRow.Style.Add("display", "none");
                divAccountNumberRow.Style.Add("display", "none");
                divButtonsRow.Style.Add("display", "none");
                vldRfPhoneNumber.Enabled = false;
                vldCstPhoneNumber.Enabled = false;
                vldRfAccountNumber.Enabled = false;
                vldReAccountNumber.Enabled = false;
            }

            if (!IsPostBack) {
                lnkOrdinarySignUp.Visible = false;
                lnkWirelessSignUp.Visible = false;
            }
        }

        private void vldCstIdentity_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            if (rbtnWireless.Checked || rbtnOrdinary.Checked) {
                return;
            }

            if (txtAccountNumber.Text.Trim() == string.Empty && phnPhoneNumber.PhoneNumber == string.Empty) {
                args.IsValid = false;
                SetErrorMessage(BOTH_VALUES_ARE_BLANK);
            } else if (txtAccountNumber.Text.Trim() != string.Empty && phnPhoneNumber.PhoneNumber != string.Empty) {
                args.IsValid = false;
                SetErrorMessage(BOTH_VALUES_ARE_ENTERED);
            }
        }

        private void vldCstPhoneNumber_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = phnPhoneNumber.IsValid;
        }

        private void SubmitHandler(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                if (rbtnWireless.Checked) {
                    RemindWirelessPhoneServicePassword();
                } else if (rbtnOrdinary.Checked) {
                    RemindOrdinaryPhoneServicePassword();
                }
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private void RemindWirelessPhoneServicePassword()
        {
            IWireless_Custdata custData;
            WebValidationResultStatus status = CustSvc.RemindWebPassword(Map, phnPhoneNumber.PhoneNumber, out custData);

            switch (status) {
                case WebValidationResultStatus.ValidCustomer:
                    EmailSender.SendWirelessPasswordRemider(custData.Email, NameFormatter.Format(custData.NameFirst, custData.NameLast), custData.WebPassword);
                    SetErrorMessage(string.Format("Thank you {0}. A temporary password has been sent to your phone.", NameFormatter.Capitalize(custData.NameFirst)));
                    break;
                case WebValidationResultStatus.CustomerNotSetupYet:
                    SetErrorMessage("Please select the link Web Access Wireless Sign Up to setup access for your account.");
                    lnkWirelessSignUp.Visible = true;
                    break;
                case WebValidationResultStatus.InvalidCustomer:
                    SetErrorMessage("Phone Number provided is invalid. Please verify and enter a valid Phone Number.");
                    break;
            }
        }

        private void RemindOrdinaryPhoneServicePassword()
        {
            CustData custData;
            WebValidationResultStatus status = CustSvc.RemindWebPassword(Map, Int32.Parse(txtAccountNumber.Text.Trim()), out custData);

            switch (status) {
                case WebValidationResultStatus.ValidCustomer:
                    EmailSender.SendPasswordRemider(custData.Email, NameFormatter.Format(custData.NameFirst, custData.NameLast), custData.WebPassword);
                    SetErrorMessage(string.Format("Thank you {0}. A temporary password has been sent to your email address.", NameFormatter.Capitalize(custData.NameFirst)));
                    break;
                case WebValidationResultStatus.CustomerNotSetupYet:
                    SetErrorMessage("Please select the link Web Access Ordinary Sign Up to setup access for your account");
                    lnkOrdinarySignUp.Visible = true;
                    break;
                case WebValidationResultStatus.InvalidCustomer:
                    SetErrorMessage("Account Number provided is invalid. Please verify and enter a valid Account Number");
                    break;
            }
        }

        #endregion
    }
}