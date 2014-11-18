using System.Configuration;

namespace Dpi.Central.Web.Account
{
    public sealed class SiteMap
    {
        public static string INDEX_URL = PublicSiteUrl + "/index.aspx";
        public static string SIGN_UP_URL = AccountSiteUrl + "/signup.aspx";
        public static string LOGIN_URL = AccountSiteUrl + "/account/login.aspx";
        public static string PASSWORD_REMINDER_URL = PublicSiteUrl + "/PasswordReminder.aspx";
        public static string ACCOUNT_SUMMARY_URL = AccountSiteUrl + "/account/summary.aspx";
        public static string ACCOUNT_SETTINGS_URL = AccountSiteUrl + "/account/account_settings.aspx";
        public static string ORDER_STATUS_URL = AccountSiteUrl + "/account/order_status.aspx";
        public static string CHANGE_PASSWORD_URL = AccountSiteUrl + "/account/change_password.aspx";
        public static string RECURRING_PAYMENT_MANAGER_URL = AccountSiteUrl + "/account/payment/rec_payment_manager.aspx";
        public static string CC_RECURRING_PAYMENT_URL = AccountSiteUrl + "/account/payment/cc_rec_payment_editor.aspx";
        public static string BANK_RECURRING_PAYMENT_URL = AccountSiteUrl + "/account/payment/bank_rec_payment_editor.aspx";
        public static string PROMISE_TO_PAY_URL = AccountSiteUrl + "/account/payment/promise_to_pay.aspx";
        public static string PAYMENT_SELECTION_URL = AccountSiteUrl + "/account/payment/payment_selection.aspx";
        public static string CC_PAYMENT_URL = AccountSiteUrl + "/account/payment/cc_payment.aspx";
        public static string CHECK_PAYMENT_URL = AccountSiteUrl + "/account/payment/check_payment.aspx";
        public static string PAYMENT_RECIEPT_URL = AccountSiteUrl + "/account/payment/reciept.aspx";

        private SiteMap()
        {
        }

        public static string AccountSiteUrl
        {
            get
            {
                return ConfigurationSettings.AppSettings["AccountSiteUrl"];
            }
        }

        public static string PublicSiteUrl 
        {
            get 
            {
                return ConfigurationSettings.AppSettings["PublicSiteUrl"];
            }
        }
    }
}
