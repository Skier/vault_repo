using System;
using System.Collections;
using System.Web;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web
{
    public class Controller
    {
        #region Static Members

        private static Controller _controller;

        public static Controller Instance
        {
            get
            {
                lock (typeof (Controller)) {
                    if (_controller == null) {
                        _controller = new Controller();
                    }
                }

                return _controller;
            }
        }

        #endregion

        #region Constants

        const string CC_REC_PAYMENT_URL = "~/account/payment/cc_rec_payment_editor.aspx";
        const string BANK_REC_PAYMENT_URL = "~/account/payment/bank_rec_payment_editor.aspx";
        
        const int EMPTY_INT_VALUE = -1;

        const string RECURRING_PAYMENT_ID_KEY = "recurring_payment_id_key";
        const string MAP_KEY = "map_key";
        const string PAYMENTS_KEY = "payments_key";
        const string ACCOUNT_INFO_KEY = "account_key";
        const string CUST_INFO_EXT2 = "cust_info_ext2";

        #endregion

        private Controller()
        {
        }

        public int AccountNumber 
        {
            get
            {
                HttpContext ctx = HttpContext.Current;

                if (ctx.User != null && ctx.User.Identity.IsAuthenticated) {
                    return Int32.Parse(HttpContext.Current.User.Identity.Name);
                }

                throw new InvalidOperationException("User must be authenticated before getting account number.");
            }
        }

        public void SwitchToHome()
        {
            HttpContext.Current.Response.Redirect("~/index.aspx");
        }

        public void SwitchToAccountSummary()
        {
            HttpContext.Current.Response.Redirect("~/account/summary.aspx");
        }

        public void SwitchToSignUp()
        {
            HttpContext.Current.Response.Redirect("~/signup.aspx");
        }

        public void SwitchToPasswordRemider()
        {
            HttpContext.Current.Response.Redirect("~/PasswordReminder.aspx");
        }

        #region Payments

        public void SwitchToRecurringPaymentManager()
        {
            HttpContext.Current.Response.Redirect("~/account/payment/rec_payment_manager.aspx");
        }

        public void SwitchToCCRecurringPaymentCreator()
        {
            SetValue(RECURRING_PAYMENT_ID_KEY, EMPTY_INT_VALUE);
            HttpContext.Current.Response.Redirect(CC_REC_PAYMENT_URL);
        }

        public void SwitchToBankRecurringPaymentCreator() 
        {
            SetValue(RECURRING_PAYMENT_ID_KEY, EMPTY_INT_VALUE);
            HttpContext.Current.Response.Redirect(BANK_REC_PAYMENT_URL);
        }

        public void SwitchToCCRecurringPaymentEditor(int recurringPaymentId)
        {
            SetValue(RECURRING_PAYMENT_ID_KEY, recurringPaymentId);
            HttpContext.Current.Response.Redirect(CC_REC_PAYMENT_URL);
        }

        public void SwitchToBankRecurringPaymentEditor(int recurringPaymentId) 
        {
            SetValue(RECURRING_PAYMENT_ID_KEY, recurringPaymentId);
            HttpContext.Current.Response.Redirect(BANK_REC_PAYMENT_URL);
        }

        public bool IsRecurringPaymentCreateMode
        {
            get { return !Exists(RECURRING_PAYMENT_ID_KEY); }
        }

        public int RecurringPaymentId
        {
            get { return (int) GetValue(RECURRING_PAYMENT_ID_KEY); }
        }

        #endregion

        #region Clear Methods

        internal void ClearAll()
        {
            ClearPayments();
            ClearAccount();
            ClearCustInfoExt2();

            HttpContext.Current.Session[MAP_KEY] = null;
        }

        internal void ClearPayments() 
        {
            HttpContext.Current.Session[PAYMENTS_KEY] = null;
        }

        internal void ClearAccount()
        {
            HttpContext.Current.Session[ACCOUNT_INFO_KEY] = null;
        }

        internal void ClearCustInfoExt2() 
        {
            HttpContext.Current.Session[CUST_INFO_EXT2] = null;
        }

        #endregion

        #region Private Methods

        bool Exists(string key)
        {
            try {
                GetValue(key);
                return true;
            } catch {
                return false;
            }
        }

        object GetValue(string key)
        {
            object value = HttpContext.Current.Session[key];
            if (value != null) {
                if (value is int) {
                    if ((int) value != EMPTY_INT_VALUE) {
                        return value;
                    }
                } else {
                    return value;
                }
            }

            throw new ArgumentException("Value with '" + key
                + "' key is empty or does not exist.");
        }

        void SetValue(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        #endregion

        #region Properties

        internal IMap Map 
        {
            get {
                object value = HttpContext.Current.Session[MAP_KEY];
                if (value == null) {
                    value = HttpContext.Current.Session[MAP_KEY] = IMapFactory.getIMap();
                }

                return (IMap)value;
            }
        }

        internal ICustomerRecurringPayment[] Payments 
        {
            get {
                object value = HttpContext.Current.Session[PAYMENTS_KEY];
                if (value == null) {
                    Controller controller = Controller.Instance;
                    
                     ICustomerRecurringPayment[] payments = CustSvc.GetCustROPByAccount(
                         controller.Map, controller.AccountNumber);

                    ArrayList paymentList = new ArrayList(payments);
                    paymentList.Sort(new RecurringPaymentStatusComparer());

                    paymentList.CopyTo(payments);

                    value = HttpContext.Current.Session[PAYMENTS_KEY] = payments;
                }

                return (ICustomerRecurringPayment[])value;
            }
        }

        internal IAcctInfo AccountInfo
        {
            get
            {
                object value = HttpContext.Current.Session[ACCOUNT_INFO_KEY];
                if (value == null) {
                    value = HttpContext.Current.Session[ACCOUNT_INFO_KEY] 
                        = CustSvc.GetAcctInfo(Map, AccountNumber);
                }

                return (IAcctInfo)value;
            }
        }

        internal ICustInfoExt2 CustInfoExt2 
        {
            get {
                object value = HttpContext.Current.Session[CUST_INFO_EXT2];
                if (value == null) {
                    value = HttpContext.Current.Session[CUST_INFO_EXT2] 
                        = CustSvc.GetCustInfoExt2(Map, AccountNumber);
                }

                return (ICustInfoExt2)value;
            }
        }

        #endregion
    }
}