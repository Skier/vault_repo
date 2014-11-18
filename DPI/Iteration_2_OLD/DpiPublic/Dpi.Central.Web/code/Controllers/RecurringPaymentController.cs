using System;
using System.Collections;
using System.Web;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Controllers
{
    public class RecurringPaymentController : ControllerBase
    {
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

        #region Static Members

        private static RecurringPaymentController _instance;

        public static RecurringPaymentController Instance
        {
            get
            {
                lock (typeof (RecurringPaymentController)) {
                    if (_instance == null) {
                        _instance = new RecurringPaymentController();
                    }
                }

                return _instance;
            }
        }

        #endregion

        protected RecurringPaymentController() : base()
        {
        }

        public void EditPayment(int paymentId)
        {
            ICustomerRecurringPayment payment = RetrievePayment(paymentId);

            Mode = EditorMode.Editor;
            PaymentId = paymentId;

            if (payment.AccountTypeId == (int) PaymentType.Credit) {
                HttpContext.Current.Response.Redirect(
                    UrlDictionary.CC_RECURRING_PAYMENT_URL);
            } else if (payment.AccountTypeId == (int) PaymentType.Check) {
                HttpContext.Current.Response.Redirect(
                    UrlDictionary.BANK_RECURRING_PAYMENT_URL);
            }
        }

        public ICustomerRecurringPayment[] RetrieveAllPayments()
        {
            object value = HttpContext.Current.Session[PAYMENTS_KEY];

            if (value == null) {
                ICustomerRecurringPayment[] payments;
                payments = CustSvc.GetCustROPByAccount(Map, AccountNumber);

                ArrayList paymentList = new ArrayList(payments);
                paymentList.Sort(new RecurringPaymentStatusComparer());

                paymentList.CopyTo(payments);

                value = HttpContext.Current.Session[PAYMENTS_KEY] = payments;
            }

            return (ICustomerRecurringPayment[]) value;
        }

        public void DeactivatePayment(int paymentId)
        {
            ICustomerRecurringPayment payment = RetrievePayment(paymentId);

            payment.Active = false;
            CustSvc.PreSave(Map);

            HttpContext.Current.Session[PAYMENTS_KEY] = null;
        }

        public ICustomerRecurringPayment RetrievePayment(int paymentId)
        {
            ICustomerRecurringPayment[] payments = RetrieveAllPayments();

            foreach (ICustomerRecurringPayment payment in payments) {
                if (payment.Id == paymentId) {
                    return payment;
                }
            }

            throw new ApplicationException("Payment with '" + paymentId
                + "' Id was not found in the specified collection.");
        }

        public ICustomerRecurringPayment GetCCPayment(PaymentType type)
        {
            ICustomerRecurringPayment payment;

            if (Mode == EditorMode.Creator) {
                payment = CustSvc.GetCustROP(Map);

                payment.DateInserted = DateTime.Now;
                payment.UserId = "DPI Central";
                payment.AccNumber = AccountNumber;
                payment.AccountTypeId = (int) type;
            } else {
                payment = RetrievePayment(PaymentId);
            }

            return payment;
        }

        public void SavePayment(ICustomerRecurringPayment payment)
        {
            CustSvc.PreSave(Map);
            HttpContext.Current.Session[PAYMENTS_KEY] = null;
            SendConfirmation(payment.EmailAddress);
            HttpContext.Current.Response.Redirect(UrlDictionary.RECURRING_PAYMENT_MANAGER_URL);
        }

        private void SendConfirmation(string toAddress)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(toAddress);
            msg.EmailFrom = "inquiry@dpiteleconnect.com";
            msg.EmailSubject = "Recurring payment confirmation";
            msg.EmailMessage = "Thank you for using DPI Teleconnect Online. This is the confirmation that you have setup new recurring payment using bank account.";

            msg.SendMail();
        }

        public EditorMode Mode
        {
            get { return (EditorMode) GetValue(EDITOR_MODE_KEY); }
            set { SetValue(EDITOR_MODE_KEY, value); }
        }

        public int PaymentId
        {
            get { return (int) GetValue(RECURRING_PAYMENT_ID_KEY); }
            set { SetValue(RECURRING_PAYMENT_ID_KEY, value); }
        }
    }
}