using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web
{
    internal enum UpdateDirection
    {
        ToPage, FromPage
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

        public static ICustomerRecurringPayment FindRecurringPayment(int paymentId)
        {
            Controller controller = Controller.Instance;

            foreach (ICustomerRecurringPayment payment in controller.Payments) {
                if (payment.Id == paymentId) {
                    return payment;
                }
            }

            throw new ApplicationException("Payment with '" + paymentId 
                + "' Id was not found in the specified collection.");
        }
    }

    internal class RecurringPaymentStatusComparer : IComparer 
    {
        public int Compare(object x, object y) 
        {
            ICustomerRecurringPayment xp = x as ICustomerRecurringPayment;
            ICustomerRecurringPayment yp = y as ICustomerRecurringPayment;

            if (xp == null && yp == null) {
                return 0;
            }

            if (xp == null) {
                return -1;
            }

            if (yp == null) {
                return 1;
            }

            if ((xp.Active && yp.Active) || (!xp.Active && !yp.Active)) {
                return 0;
            }

            if (xp.Active) {
                return -1;
            }

            return 1;
        }
    }

    internal sealed class Utilities
    {
        private Utilities()
        {
        }

        public static void SendRecurringPaymentConfirmation(string toAddress)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(toAddress);
            msg.EmailFrom = "inquiry@dpiteleconnect.com";
            msg.EmailSubject = "Recurring payment confirmation";
            msg.EmailMessage = "Thank you for using DPI Teleconnect Online. This is the confirmation that you have setup new recurring payment using bank account.";

            msg.SendMail();
        }

        public static void SendSingUpConfirmation(string toAddress, string name)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(toAddress);
            msg.EmailFrom = "inquiry@dpiteleconnect.com";
            msg.EmailSubject = "DPI Teleconnect Web Access";
            msg.EmailMessage = string.Format("Hello {0}. This email confirms that you have setup web access to your DPI Teleconnect account. Thank you.", name);

            msg.SendMail();
        }
    }
}