namespace Dpi.Central.Web
{
    internal sealed class SiteMap
    {
        public const string INDEX_URL = "~/index.aspx";
        public const string SIGN_UP_URL = "~/signup.aspx";
        public const string LOGIN_URL = "~/account/login.aspx";
        public const string PASSWORD_REMINDER_URL = "~/PasswordReminder.aspx";
        public const string ACCOUNT_SUMMARY_URL = "~/account/summary.aspx";
        public const string ACCOUNT_SETTINGS_URL = "~/account/account_settings.aspx";
        public const string ORDER_STATUS_URL = "~/account/order_status.aspx";
        public const string CHANGE_PASSWORD_URL = "~/account/change_password.aspx";
        public const string RECURRING_PAYMENT_MANAGER_URL = "~/account/payment/rec_payment_manager.aspx";
        public const string CC_RECURRING_PAYMENT_URL = "~/account/payment/cc_rec_payment_editor.aspx";
        public const string BANK_RECURRING_PAYMENT_URL = "~/account/payment/bank_rec_payment_editor.aspx";
        public const string PROMISE_TO_PAY_URL = "~/account/payment/promise_to_pay.aspx";
        public const string PAYMENT_SELECTION_URL = "~/account/payment/payment_selection.aspx";
        public const string CC_PAYMENT_URL = "~/account/payment/cc_payment.aspx";
        public const string CHECK_PAYMENT_URL = "~/account/payment/check_payment.aspx";
        public const string PAYMENT_RECIEPT_URL = "~/account/payment/reciept.aspx";

        private SiteMap()
        {
        }
    }
}
