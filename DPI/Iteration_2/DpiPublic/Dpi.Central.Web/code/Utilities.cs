using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Interfaces;

namespace Dpi.Central.Web
{
    internal sealed class UrlDictionary
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

        private UrlDictionary()
        {
        }
    }

    internal enum UpdateDirection
    {
        ToPage,
        FromPage
    }

    internal sealed class Convertor
    {
        private Convertor()
        {
        }

        public static string Convert(PaymentType pType)
        {
            switch (pType) {
                case PaymentType.Cash:
                    return "Cash";

                case PaymentType.Check:
                    return "Check";

                case PaymentType.Credit:
                    return "Credit/Debit";

                case PaymentType.Debit:
                    return "Credit/Debit";

                default:
                    return pType.ToString();
            }
        }

        public static string MakeFriendlyName(string name) 
        {
            if (name == null || name == string.Empty) {
                return string.Empty;
            }

            if (name.Length == 1) {
                return name.ToUpper();
            }

            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        } 
        
        public static string FormatPhoneNumber(string value)
        {
            if (value == null) 
            {
                throw new ArgumentNullException("value");
            }
            
            string result = value;

            if (value.Length >= 4) 
            {
                result.Insert(3, "-");
                if (value.Length >= 7) 
                {
                    result.Insert(7, "-");
                }
            }
            
            return result;
        }
    }

    internal sealed class Finder
    {
        private Finder()
        {
        }

        public static int FindFirstIndex(string headerText, DataGrid dataGrid)
        {
            for (int i = 0; i < dataGrid.Columns.Count; i++) {
                DataGridColumn column = dataGrid.Columns[i];
                if (column.HeaderText == headerText) {
                    return i;
                }
            }

            throw new ApplicationException("Column '" + headerText
                + "' was not found in '" + dataGrid.ID + "' DataGrid control.");
        }

        public static Control FindFirstControl(Type controlType, ControlCollection controls)
        {
            foreach (Control control in controls) {
                if (control.GetType().IsSubclassOf(controlType) || controlType.IsInstanceOfType(control)) {
                    return control;
                }
            }

            throw new ApplicationException("Control of '" + controlType
                + "' type was not found in the specified collection.");
        }
    }
}
