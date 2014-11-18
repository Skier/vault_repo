using System;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Dpi.Central.Web.Controls;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    public class BaseAccountPage : BasePage
    {
        #region Constants

        protected const string NAME_FORMAT = "{0} {1}";
        protected const string MONEY_FORMAT = "c";
        protected const string MONEY_VALUE_FORMAT = ".00";

        protected const string ACCOUNT_SUMMARY_KEY = "ACCOUNT_SUMMARY_KEY";

        protected const string PAYMENT_AMOUNT_KEY = "PAYMENT_AMOUNT_KEY";

        protected const string PAYMENT_TYPE_KEY = "PAYMENT_TYPE_KEY";
        protected const string PAYMENT_CREDIT_CARD_KEY = "PAYMENT_CREDIT_CARD_KEY";
        protected const string PAYMENT_CHECK_KEY = "PAYMENT_CHECK_KEY";
        
        protected const string SESSION_STATE_IS_INVALID = "Session state is invalid: {0} is missed.";

        protected const string REJECTED_PAYMENT = "Your payment was rejected. Reason: {0}.";
        protected const string UNABLE_TO_COMPLETE_PAYMENT = "We are unable to process your payment at this time. Please attempt to process your payment again within 24 hours. If your services are subject to immediate interruption, please contact our Customer Service team at 1-800-350-4009.";
        protected const string NEED_VERIFICATION = "Please allow 24 hours for verification of payment processing in the amount of {0}. If your services are subject to immediate interruption, please contact our Customer Service team at 1-800-350-4009.";

        #endregion

        #region Fields

        private IMap _imap;
        private ICustInfoExt _cust;
        private IAcctInfo _acct;

        #endregion

        #region Protected Methods

        protected override void InitLayout() 
        {
            base.InitLayout();

            Footer footer = new Footer();
            footer.CssClass = "page_footer_stand_along";
            Form.Controls.Add(footer);

            HtmlTable cxtTable = new HtmlTable();
            cxtTable.CellPadding = 0;
            cxtTable.CellSpacing = 0;
            cxtTable.Border = 0;

            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell = new HtmlTableCell();
            cell.Controls.Add(Form);
            row.Cells.Add(cell);
            cxtTable.Rows.Add(row);

            int cxtTableIndex = Controls.IndexOf(Form);
            Controls.AddAt(cxtTableIndex, cxtTable);
        }

        protected override Label AddErrorMessageLabel() 
        {
            Label errorMessageLabel = new Label();
            errorMessageLabel.CssClass = ERROR_MSG_CSS_CLASS;

            Form.Controls.AddAt(4, errorMessageLabel);

            return errorMessageLabel;
        }

        protected override void OnLoad(EventArgs e)
        {
            ControlHelper.NeedDwcUtils();

            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            base.OnLoad(e);
        }

        protected internal int GetAccountNumber()
        {
            HttpContext ctx = HttpContext.Current;

            if (ctx.User != null && ctx.User.Identity.IsAuthenticated) {
                return Int32.Parse(HttpContext.Current.User.Identity.Name);
            }

            throw new InvalidOperationException("User must be authenticated before getting account number.");
        }

        protected void EnsureOneClickBehaviour(WebControl control)
        {
            StringBuilder sbValid = new StringBuilder();
            sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
            sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
            sbValid.Append(this.Page.GetPostBackEventReference(control));
            sbValid.Append(";");
            sbValid.Append("this.disabled = true;");
            control.Attributes.Add("onclick", sbValid.ToString());
        }

        #endregion

        #region Properties

        protected internal IMap Map
        {
            get
            {
                if (_imap == null) {
                    _imap = IMapFactory.getIMap();
                }

                return _imap;
            }
        }

        protected ICustInfoExt Cust
        {
            get
            {
                if (_cust == null) {
                    _cust = CustSvc.GetCustInfoExt(Map, GetAccountNumber());
                }

                return _cust;
            }
        }

        protected IAcctInfo Acct
        {
            get
            {
                if (_acct == null) {
                    _acct = CustSvc.GetAcctInfo(Map, GetAccountNumber());
                }

                return _acct;
            }
        }

        #endregion
    }
}