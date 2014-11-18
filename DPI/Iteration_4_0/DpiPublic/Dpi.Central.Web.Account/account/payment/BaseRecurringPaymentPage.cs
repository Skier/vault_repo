using System;
using System.Collections;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Payment
{
    public class BaseRecurringPaymentPage : BasePaymentPage
    {
        #region Classes

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

        #endregion

        #region Override Methods

        protected override void OnLoad(EventArgs e)
        {
            if (Session["SELECTED_PAYMENT_ID_KEY"] != null && Session["SELECTED_PAYMENT_ID_KEY"] is int) {
                SelectedPaymentId = (int) Session["SELECTED_PAYMENT_ID_KEY"];
            }

            base.OnLoad(e);
        }

        #endregion

        #region Implementation

        protected ICustomerRecurringPayment GetPayment(int paymentId, PaymentType paymentType) 
        {
            ICustomerRecurringPayment payment;

            if (paymentId != -1) {
                payment = CustSvc.GetCustROP(Map, paymentId);
            } else {
                payment = CustSvc.GetCustROP(Map);
                payment.Active = true;
                payment.DateInserted = DateTime.Now;
                payment.UserId = "DPI Central";
                payment.AccNumber = GetAccountNumber();
                payment.AccountTypeId = (int) paymentType;

                ICustomerRecurringPayment activePayment = GetActiveRecurringPayment();
                if (activePayment != null) {
                    activePayment.Active = false;
                }
            }

            payment.DateModified = DateTime.Now;
            payment.Priority = 1;

            return payment;
        }

        protected void SavePayment(ICustomerRecurringPayment payment, PaymentType paymentType) 
        {
            CustSvc.PreSave(Map);

            EmailSender.SendRecurringPaymentConfirmation(payment.EmailAddress, NameFormatter.Format(payment.BillingFirstName, payment.BillingLastName), paymentType == PaymentType.Credit);

            ICustInfo ci = base.Cust.CustInfo;
            
            if (ci.Email.ToUpper() != payment.EmailAddress.ToUpper()) {
                EmailSender.SendRecurringPaymentConfirmation(ci.Email, NameFormatter.Format(ci), paymentType == PaymentType.Credit);                
            }
            
            Response.Redirect(SiteMap.REC_PAYMENTS_URL);
        }

        protected ICustomerRecurringPayment[] LoadRecurringPayments() 
        {
            int accountNumber = GetAccountNumber();
            ICustomerRecurringPayment[] payments = CustSvc.GetCustROPByAccount(Map, accountNumber);

            ArrayList paymentList = new ArrayList(payments);
            paymentList.Sort(new RecurringPaymentStatusComparer());
            paymentList.CopyTo(payments);

            return payments;
        }

        protected ICustomerRecurringPayment GetActiveRecurringPayment() 
        {
            ICustomerRecurringPayment[] payments = LoadRecurringPayments();
            foreach (ICustomerRecurringPayment payment in payments) {
                if (payment.Active) {
                    return payment;
                }
            }

            return null;
        }

        protected void ChangeRecurringPaymentStatus(int paymentId)
        {
            ICustomerRecurringPayment payment = CustSvc.GetCustROP(Map, paymentId);
            ChangeRecurringPaymentStatus(payment);
        }

        protected void ChangeRecurringPaymentStatus(ICustomerRecurringPayment payment) 
        {
            payment.Active = !payment.Active;
            CustSvc.PreSave(Map);
        }

        #endregion

        #region Protected Methods

        protected void UpdateAccountInfoControl(ref AccountInfoControl ctrlAccountInfo, ICustomerRecurringPayment payment) 
        {
            ctrlAccountInfo.AccountNumber = base.GetAccountNumber().ToString();
            ctrlAccountInfo.PhoneNumber = payment.PhNumber;
            ctrlAccountInfo.FirstName = payment.BillingFirstName;
            ctrlAccountInfo.LastName = payment.BillingLastName;
            ctrlAccountInfo.StreetAddress = payment.BillingAddress;
            ctrlAccountInfo.City = payment.BillingCity;
            ctrlAccountInfo.State = payment.BillingState;
            ctrlAccountInfo.Zip = payment.BillingZip;
            ctrlAccountInfo.Email = payment.EmailAddress;
        }

        protected void UpdatePayment(ref ICustomerRecurringPayment payment, AccountInfoControl ctrlAccountInfo) 
        {
            payment.BillingFirstName = ctrlAccountInfo.FirstName;
            payment.BillingLastName = ctrlAccountInfo.LastName;
            payment.BillingAddress = ctrlAccountInfo.StreetAddress;
            payment.BillingCity = ctrlAccountInfo.City;
            payment.BillingState = ctrlAccountInfo.State;
            payment.BillingZip = ctrlAccountInfo.Zip;
            payment.PhNumber = ctrlAccountInfo.PhoneNumber;
            payment.EmailAddress = ctrlAccountInfo.Email;
        }

        #endregion

        #region Properties

        protected bool IsEditMode
        {
            get { return SelectedPaymentId != -1; }
        }

        protected int SelectedPaymentId
        {
            get
            {
                object value = ViewState["SELECTED_PAYMENT_ID_KEY"];
                if (value != null && value is int) {
                    return (int) value;
                }

                return -1;
            }

            set { ViewState["SELECTED_PAYMENT_ID_KEY"] = value; }
        }

        #endregion
    }
}