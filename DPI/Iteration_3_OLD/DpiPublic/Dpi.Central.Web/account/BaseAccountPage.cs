using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using DPI.Interfaces;
using DPI.Services;
using System.Text;

namespace Dpi.Central.Web.Account
{
    /// <summary>
    /// Base page for all account pages.  Should be abstract, but designer does not like it.
    /// </summary>
    public class BaseAccountPage : Page
    {

        #region Constants

        protected const string NAME_FORMAT = "{0} {1}";
        protected const string MONEY_FORMAT = "c";
        protected const string MONEY_VALUE_FORMAT = ".00";

        protected const string PAYMENT_AMOUNT_KEY = "PAYMENT_AMOUNT_KEY";

		protected const string PAYMENT_TYPE_KEY = "PAYMENT_TYPE_KEY";
		protected const string PAYMENT_CREDIT_CARD_KEY = "PAYMENT_CREDIT_CARD_KEY";
		protected const string PAYMENT_CHECK_KEY = "PAYMENT_CHECK_KEY";

        // TODO: carify from Boris.
        protected const string INTERNAL_ERROR = "Internal error.";
        protected const string SESSION_STATE_IS_INVALID = "Session state is invalid: {0} is missed.";

		protected const string REJECTED_PAYMENT = "Your payment was rejected. Reason: {0}.";
		protected const string UNABLE_TO_COMPLETE_PAYMENT = "We are unable to process your payment at this time. Please attempt to process your payment again within 24 hours. If your services are subject to immediate interruption, please contact our Customer Service team at 1-800-350-4009.";
		protected const string NEED_VERIFICATION = "Please allow 24 hours for verification of payment processing in the amount of {0}. If your services are subject to immediate interruption, please contact our Customer Service team at 1-800-350-4009.";
        #endregion

        protected HeaderUserControl ctrlHeader;
        protected FooterUserControl ctrlFooter;
        private IMap map;
        private ICustInfoExt _cust;
        private IAcctInfo _acct;

        #region Methods

        #region Static

        public static void Logout(bool redirect)
        {
            FormsAuthentication.SignOut();
            HttpContext ctx = HttpContext.Current;
            ctx.Session.Abandon();
            ctx.Response.Cache.SetNoStore();
            ctx.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            ctx.User = null;
            if (redirect) {
                ctx.Response.Redirect(SiteMap.ACCOUNT_SUMMARY_URL);
            }
        }

        public static bool IsAuthenticated()
        {
            HttpContext ctx = HttpContext.Current;
            return ctx.User != null && ctx.User.Identity.IsAuthenticated;
        }

        public static int GetAccountNumber()
        {
            if (IsAuthenticated()) {
                return Int32.Parse(HttpContext.Current.User.Identity.Name);
            }

            throw new InvalidOperationException("User must be authenticated before getting account number.");
        }

        #endregion Static

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }

        public void btnImgLogout_Click(object sender, ImageClickEventArgs e)
        {
            Logout();
        }

        protected void Logout()
        {
            Logout(true);
        }

        protected void CreateFormAuthenticationTicket(string accountNumber)
        {
            DateTime expDate = DateTime.Now.AddMinutes(10);

            // Create the authentication ticket and store the roles in the
            // custom UserData property of the authentication ticket.
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1,
                accountNumber,
                DateTime.Now,
                expDate,
                false,
                string.Empty);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            // Create a cookie and add the encrypted 
            // ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = expDate;

            // Add the cookie to the outgoing cookies collection.
            Response.Cookies.Add(authCookie);
        }

        #endregion Methods

        #region Properties

        protected IMap Map
        {
            get
            {
                if (map == null) {
                    map = IMapFactory.getIMap();
                }
                return map;
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

		protected string GetDigits(string value)
		{
			StringBuilder sb = new StringBuilder(value.Length);
			for (int i = 0; i < value.Length; i++) 
			{
				char c = value[i];
				if (char.IsDigit(c)) 
				{
					sb.Append(c);
				}
			}

			return sb.ToString();
		}
    }
}