using System.Web;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    /// <summary>
    /// Summary description for AccountSettingsController.
    /// </summary>
    public class AccountSettingsController : ControllerBase
    {
        #region Constants
        
        private const string CUST_INFO_EXT_KEY = "CurrentAccountCustInfoExt";

        #endregion Constants

        #region Static Members

        private static AccountSettingsController _instance;

        public static AccountSettingsController Instance {
            get {
                if (_instance == null) {
                    lock (typeof (AccountSettingsController)) {
                        if (_instance == null) {
                            _instance = new AccountSettingsController();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        protected AccountSettingsController() : base() {
        }
        
        /// <summary>
        /// Changes account settings namely e-mail and web access password
        /// </summary>
        /// 
        /// <remarks>If we will have to change something else shall we overload this method
        /// or implement it somehow else?</remarks>
        /// 
        /// <param name="email">E-Mail address to be set for the currently logged on account</param>
        /// <param name="webPassword">New web access password for the account</param>
        public void ChangeAccountSettings(string email, string webPassword) {
            CustSvc.ChangeAccountSettings(Map, AccountNumber, email, webPassword);
        }
        
        public ICustInfoExt GetCustInfoExt() {
            return CustSvc.GetCustInfoExt(Map, AccountNumber);
        }
    }
}