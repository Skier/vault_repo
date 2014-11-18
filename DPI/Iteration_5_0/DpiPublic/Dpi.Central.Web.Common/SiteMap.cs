using System.Configuration;

namespace Dpi.Central.Web
{
    public sealed class SiteMap
    {
        private static string _publicSiteUrl = string.Empty;
        private static string _accountSiteUrl = string.Empty;

        public static string INDEX_URL = PublicSiteUrl + "/index.aspx";
        public static string SIGN_UP_URL = AccountSiteUrl + "/signup.aspx";
        public static string LOGIN_URL = AccountSiteUrl + "/account/login.aspx";
        public static string PUBLIC_LOGIN_URL = AccountSiteUrl + "/public_login.aspx";
        public static string PASSWORD_REMINDER_URL = AccountSiteUrl + "/password_reminder.aspx";
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
        public static string PAYMENT_HISTORY_URL = AccountSiteUrl + "/account/payment/payment_history.aspx";
        public static string CHECK_PAYMENT_URL = AccountSiteUrl + "/account/payment/check_payment.aspx";
        public static string PAYMENT_RECIEPT_URL = AccountSiteUrl + "/account/payment/reciept.aspx";
        public static string ACCOUNT_IMAGES_URL = AccountSiteUrl + "/images/";        
        public static string PAYMENT_FORECAST_URL = AccountSiteUrl + "/account/payment_forecast.aspx";

        // Recurring payments
        public static string REC_PAYMENTS_URL = AccountSiteUrl + "/account/payment/rec_payments.aspx";
        public static string REC_DEACTIVATE_CONFIRMATION_URL = AccountSiteUrl + "/account/payment/rec_deactivate_confirmation.aspx";
        public static string REC_CC_PAYMENT_URL = AccountSiteUrl + "/account/payment/rec_cc_payment.aspx";
        public static string REC_CHECK_PAYMENT_URL = AccountSiteUrl + "/account/payment/rec_check_payment.aspx";

        // New account setup
        public static string NEW_ACC_SELECT_PROVIDER_URL = AccountSiteUrl + "/account_setup/select_provider.aspx";
        public static string NEW_ACC_SELECT_PACKAGE_URL = AccountSiteUrl + "/account_setup/select_package.aspx";
        public static string NEW_ACC_SELECT_SERVICES_URL = AccountSiteUrl + "/account_setup/select_services.aspx";
        public static string NEW_ACC_ORDER_SUMMARY_URL = AccountSiteUrl + "/account_setup/order_summary.aspx";
        public static string NEW_ACC_TPV_AGREEMENT_URL = AccountSiteUrl + "/account_setup/tpv_agreement.aspx";
        public static string NEW_ACC_TPV_DISAGREEMENT_URL = AccountSiteUrl + "/account_setup/tpv_disagreement.aspx";
        public static string NEW_ACC_SERVICE_ADDRESS_URL = AccountSiteUrl + "/account_setup/service_address.aspx";
        public static string NEW_ACC_SUMMARY_URL = AccountSiteUrl + "/account_setup/account_summary.aspx";        
        public static string NEW_ACC_PAY_CHECK_URL = AccountSiteUrl + "/account_setup/check_payment.aspx";
        public static string NEW_ACC_PAY_CREDIT_CARD_URL = AccountSiteUrl + "/account_setup/cc_payment.aspx";
        
        // Wireless
        public static string PUBLIC_WIRELESS_PRODUCTS_URL = PublicSiteUrl + "/ppc.aspx";
        public static string WRLS_LOGIN_URL = AccountSiteUrl + "/wireless/login.aspx";
        public static string WRLS_CUSTOMER_INFO_URL = AccountSiteUrl + "/wireless/customer_info.aspx";
        public static string WRLS_SERVICE_INFO_URL = AccountSiteUrl + "/wireless/service_info.aspx";

        // Recharge Differnet Plan Process
        public static string RDP_SELECT_PLAN_URL = AccountSiteUrl + "/wireless/rsp/select_plan.aspx";
        public static string RDP_SELECT_PRODUCTS_URL = AccountSiteUrl + "/wireless/rsp/select_products.aspx";
        public static string RDP_ORDER_SUMMARY_URL = AccountSiteUrl + "/wireless/rsp/order_summary.aspx";
        public static string RDP_PAY_CHECK_URL = AccountSiteUrl + "/wireless/rsp/check_payment.aspx";
        public static string RDP_PAY_CREDIT_CARD_URL = AccountSiteUrl + "/wireless/rsp/cc_payment.aspx";
        public static string RDP_RECEIPT_URL = AccountSiteUrl + "/wireless/rsp/receipt.aspx";

        // Replanish Wireless Account Process
        public static string RWA_CUSTOMER_INFO_URL = AccountSiteUrl + "/rwa/customer_info.aspx";
        public static string RWA_ORDER_SUMMARY_URL = AccountSiteUrl + "/rwa/order_summary.aspx";
        public static string RWA_PAYMENT_URL = AccountSiteUrl + "/rwa/payment.aspx";
        public static string RWA_RECEIPT_URL = AccountSiteUrl + "/rwa/receipt.aspx";

        static SiteMap()
        {
            _publicSiteUrl = ConfigurationSettings.AppSettings["PublicSiteUrl"];
            _accountSiteUrl = ConfigurationSettings.AppSettings["AccountSiteUrl"];
        }

        internal static string PublicSiteUrl
        {
            get
            {
                // TODO:?????????????????????
                _publicSiteUrl = ConfigurationSettings.AppSettings["PublicSiteUrl"];
                return _publicSiteUrl;
            }
        }

        internal static string AccountSiteUrl
        {
            get
            {
                // TODO:?????????????????????
                _accountSiteUrl = ConfigurationSettings.AppSettings["AccountSiteUrl"];
                return _accountSiteUrl;
            }
        }

        private SiteMap() 
        {
        }
    }
}
