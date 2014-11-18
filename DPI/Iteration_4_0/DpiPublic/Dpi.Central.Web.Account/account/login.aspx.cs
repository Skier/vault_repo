using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;

namespace Dpi.Central.Web.Account
{
    public class LoginPage : BaseLoginPage
    {
        #region Web Form Designer generated code

        protected System.Web.UI.WebControls.TextBox txtAccountNumber;
        protected System.Web.UI.WebControls.TextBox txtPassword;
        protected RegularExpressionValidator anRegExpValidator;
        protected RequiredFieldValidator pwdReqFldValidator;
        protected Dpi.Central.Web.Controls.PhoneNumberBox phnPhoneNumber;
        protected System.Web.UI.WebControls.RequiredFieldValidator vldRfPassword;
        protected System.Web.UI.WebControls.ImageButton btnSubmit;
        protected System.Web.UI.WebControls.CustomValidator vldCstPhoneNumber;
        protected System.Web.UI.WebControls.RegularExpressionValidator Regularexpressionvalidator1;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldReAccountNumber;
        protected System.Web.UI.WebControls.CustomValidator vldCstIdentity;
        protected System.Web.UI.WebControls.RegularExpressionValidator vldRePassword;

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
            this.vldCstPhoneNumber.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstPhoneNumber_ServerValidate);
            this.vldCstIdentity.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.vldCstIdentity_ServerValidate);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        #endregion

        #region EventHandlers

        private void Page_Load(object sender, System.EventArgs e) 
        {
            if (!IsPostBack) {
                if (Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"] != null) {
                    txtAccountNumber.Text = Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"].ToString();
                    Session["NEW_ACC_SIGNUP_TO_LOGIN_REDIRECT_ACCT_ID"] = null;
                }
            }
        }

        private void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e) 
        {
            if (!Page.IsValid) {
                return;
            }
                        
            try {
                LoginResult result = Login(txtAccountNumber.Text.Trim(), phnPhoneNumber.PhoneNumber, txtPassword.Text);
                string message = GetUIMessage(result);
                SetErrorMessage(message);
            } catch (Exception ex) {
                SetErrorMessage(ex);
            }
        }

        #endregion

        private void vldCstPhoneNumber_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            PhoneNumberBox phoneNumberBox = (PhoneNumberBox) Page.FindControl(((BaseValidator) source).ControlToValidate);
            args.IsValid = phoneNumberBox.IsValid;
        }

        private void vldCstIdentity_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args) 
        {
            args.IsValid = txtAccountNumber.Text.Trim().Length != 0 || phnPhoneNumber.PhoneNumber.Trim().Length != 0;
        }

        protected override void InitLayout() 
        {
            // TODO: it duplicates code from BaseImagedAccountPage. Remove it.

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

        protected override Label AddErrorMessageLabel() 
        {
            Label errorMessageLabel = new Label();
            errorMessageLabel.CssClass = ERROR_MSG_CSS_CLASS;

            Form.Controls.AddAt(0, errorMessageLabel);

            return errorMessageLabel;
        }
    }
}