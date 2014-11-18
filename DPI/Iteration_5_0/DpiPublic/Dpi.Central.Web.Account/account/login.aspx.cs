using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class LoginPage : BaseLoginPage
    {
        #region Web Form Designer generated code

        protected TextBox txtAccountNumber;
        protected TextBox txtPassword;
        protected RegularExpressionValidator anRegExpValidator;
        protected RequiredFieldValidator pwdReqFldValidator;
        protected PhoneNumberBox phnPhoneNumber;
        protected RequiredFieldValidator vldRfPassword;
        protected ImageButton btnSubmit;
        protected CustomValidator vldCstPhoneNumber;
        protected RegularExpressionValidator Regularexpressionvalidator1;
        protected RegularExpressionValidator vldReAccountNumber;
        protected CustomValidator vldCstIdentity;
        protected RegularExpressionValidator vldRePassword;

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
            this.vldCstPhoneNumber.ServerValidate += new ServerValidateEventHandler(this.vldCstPhoneNumber_ServerValidate);
            this.vldCstIdentity.ServerValidate += new ServerValidateEventHandler(this.vldCstIdentity_ServerValidate);
            this.btnSubmit.Click += new ImageClickEventHandler(this.btnSubmit_Click);
            this.Load += new EventHandler(this.Page_Load);

        }

        #endregion

        #region EventHandlers

        private void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                if (Request.QueryString.HasKeys()) {
                    if (Request.QueryString[PHONE_NUMBER_PARAMETER] != null) {
                        phnPhoneNumber.PhoneNumber = Request.QueryString[PHONE_NUMBER_PARAMETER];
                    }

                    if (Request.QueryString[ACCOUNT_NUMBER_PARAMETER] != null) {
                        txtAccountNumber.Text = Request.QueryString[ACCOUNT_NUMBER_PARAMETER];
                    }

                    if (Request.QueryString[PASSWORD_PARAMETER] != null) {
                        string encodedPassword = Request.QueryString[PASSWORD_PARAMETER];
                        txtPassword.Text = base.DecodePassword(encodedPassword);
                    }

                    Validate();

                    btnSubmit_Click(this, null);
                } else if (Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"] != null) {
                    txtAccountNumber.Text = Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"].ToString();
                    Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"] = null;
                }
            }
        }

        private void vldCstPhoneNumber_ServerValidate(object source, ServerValidateEventArgs args)
        {
            PhoneNumberBox phoneNumberBox = (PhoneNumberBox) Page.FindControl(((BaseValidator) source).ControlToValidate);
            args.IsValid = phoneNumberBox.IsValid;
        }

        private void vldCstIdentity_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = txtAccountNumber.Text.Trim().Length != 0 || phnPhoneNumber.PhoneNumber.Trim().Length != 0;
        }

        private void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (!Page.IsValid) {
                return;
            }

            try {
                AuthenticationResult result = Authenticate(IMapFactory.getIMap(), txtAccountNumber.Text.Trim(), phnPhoneNumber.PhoneNumber, txtPassword.Text);
                if (result.Status == AuthenticationStatus.Success) {
                    if (result.Role == UserRole.HomePhoneAccount) {
                        CustData custData = ((CustData)result.Data);
                        Login(custData);
                    } else if (result.Role == UserRole.WirelessAccount) {
                        IWireless_Custdata custData = ((IWireless_Custdata)result.Data);
                        Login(custData);
                    } else {
                        throw new ApplicationException("User role is unknown: " + result.Role + ".");
                    }
                }

                string message = GetUIMessage(result.Status, phnPhoneNumber.PhoneNumber.Length > 0);
                SetErrorMessage(message);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        #region Private Methods

        private void Login(CustData custData)
        {
            string accountNumber = custData.AccNumber.ToString(CultureInfo.InvariantCulture);
            AuthenticationHelper.SetAuthCookie(accountNumber, new string[] {AuthenticationHelper.HOME_PHONE_ACCOUNT}, false);

            if (custData.IsWebPasswordTemporal) {
                Response.Redirect(SiteMap.CHANGE_PASSWORD_URL, true);
            } else {
                CustSvc.InsertCustomerWebLogEntry(IMapFactory.getIMap(), custData.AccNumber);
                Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL, true);
            }
        }

        private void Login(IWireless_Custdata custData)
        {
            string accountNumber = custData.ID.ToString(CultureInfo.InvariantCulture);
            AuthenticationHelper.SetAuthCookie(accountNumber, new string[] {AuthenticationHelper.WIRELESS_ACCOUNT}, false);

            if (custData.IsWebPasswordTemporal) {
                Response.Redirect(SiteMap.CHANGE_PASSWORD_URL, true);
            } else {
                CustSvc.InsertCustomerWebLogEntry(IMapFactory.getIMap(), custData.ID);
                Response.Redirect(SiteMap.WRLS_SERVICE_INFO_URL, true);
            }
        }

        #endregion

        #region TODO: it duplicates code from BaseImagedAccountPage. Remove it.

        protected override void InitLayout()
        {
            HtmlTable cxtTable = new HtmlTable();

            cxtTable.CellPadding = 0;
            cxtTable.CellSpacing = 0;
            cxtTable.Border = 0;
            cxtTable.Height = "100%";

            HtmlTableCell cell = CreateCell(cxtTable, "page_header");
            cell.ColSpan = 2;
            cell.Controls.Add(CreateImage(HEADER_IMG_PATH));
            cell.Controls.Add(Menu);

            HtmlTableRow row = new HtmlTableRow();
            cell = CreateCell(row, "page_left_side_bar_cxt");
            cell.Controls.Add(CreateImage("~/images/about_side_top.jpg"));
            cell = CreateCell(row, "page_cxt");
            cell.Controls.Add(CreateImage("~/images/ppc_top.jpg"));
            HtmlForm form = ControlHelper.GetHtmlForm();
            cell.Controls.Add(form);
            cxtTable.Rows.Add(row);

            row = new HtmlTableRow();
            cell = CreateCell(row, "page_left_side_bar_footer");
            cell.Controls.Add(CreateImage("~/images/about_side_bottom.jpg"));
            cell = CreateCell(row, "page_footer");
            cell.Controls.Add(new Footer());
            cxtTable.Rows.Add(row);

            int cxtTableIndex = Controls.IndexOf(form);
            Controls.AddAt(cxtTableIndex, cxtTable);
        }

        protected override void AddErrorControlToPage() 
        {
            Form.Controls.AddAt(0, ErrorControl);
        }

        #endregion
    }
}