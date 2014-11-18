using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account
{
    /// <summary>
    /// Base page for all account pages.  Should be abstract, but designer does not like it.
    /// </summary>
    public class BaseAccountPage : Page
    {
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
                ctx.Response.Redirect(UrlDictionary.ACCOUNT_SUMMARY_URL);
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

			if (this.User != null && this.User.Identity != null && (this.User.Identity is FormsIdentity) && (! IsLoginPage()))
			{
				FormsIdentity  id = (FormsIdentity)this.User.Identity;
				if (id.Ticket.Expired)
				{
					Logout(true);
				}
			}
			
        }

        protected void Logout()
        {
            Logout(true);
        }

        protected void CreateFormAuthenticationTicket(string accountNumber)
        {

            // Create the authentication ticket and store the roles in the
            // custom UserData property of the authentication ticket.

			// TODO:  Put timing (10) from web config
			  FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                accountNumber,
				false, 10);



            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            // Create a cookie and add the encrypted 
            // ticket to the cookie as data.
            HttpCookie authCookie = new HttpCookie(
                FormsAuthentication.FormsCookieName, encryptedTicket);

            // Add the cookie to the outgoing cookies collection.
            Response.Cookies.Add(authCookie);
        }

		protected virtual bool IsLoginPage()
		{
			return false;
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
    }
}