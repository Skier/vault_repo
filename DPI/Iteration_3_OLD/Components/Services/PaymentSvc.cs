using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
    public sealed class PaymentSvc
    {
        #region Constants

        public const decimal MINIMUM_PAYMENT_AMOUNT = 10.0m;
        public const decimal MAXIMUM_PAYMENT_AMOUNT = 200.0m;

        #endregion

        #region Constructors

        private PaymentSvc()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imap"></param>
        /// <param name="accountNumber"></param>
        /// <param name="creditCard"></param>
        /// <param name="paymentAmount"></param>
        /// <returns></returns>
        public static PaymentResult MakeCreditCardPayment(IMap imap, int accountNumber, CreditCard creditCard, decimal paymentAmount, string application)
        {
            ValidateParameters(imap, accountNumber, creditCard, paymentAmount);

            UOW uow = null;

            try {
                uow = new UOW(imap, "PaymentSvc.MakeCreditCardPayment");

                ValidatePreconditions(paymentAmount);

                int priority = 0;

                ICustInfo2 customerInfo = CreateCustomerInfo(uow, creditCard, accountNumber, ref priority);
                IDemand demand = CreateDemand(uow, customerInfo, ref priority);
                Payment payment = CreatePayment(uow, creditCard, demand, paymentAmount, ref priority, application);

                PaymentServiceProvider provider = new PaymentServiceProvider();

                PaymentResult result = provider.MakeCreditCardPayment(accountNumber, creditCard, paymentAmount);
                result.Payment = payment;

                string noteText = string.Empty;

                if (result.Code == PaymentResultCode.Completed) {
                    PaymentTransaction transaction = CreatePaymentTransaction(uow, accountNumber, paymentAmount, ref priority);

                    // Link the payment with the transaction of the external payment service.
                    payment.PaymentTransaction = transaction;

                    // Enforce transaction object to be saved before saving the payment object.
                    payment.Priority = transaction.Priority + 1;

                    demand.Status = "Completed";

                    noteText = "Yonix Approved: Credit Card payment Approved on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Approval Number " + result.Payment.Id;
                } else if (result.Code == PaymentResultCode.Rejected) {
                    demand.Status = "Rejected";
                    noteText = "Yonix Declined: Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + result.Description;
                } else if (result.Code == PaymentResultCode.UnableToComplete) {
                    demand.Status = "UnableToComplete";
                    noteText = "Yonix Declined: Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Cannot connect";
                } else if (result.Code == PaymentResultCode.NeedVerification) {
                    demand.Status = "NeedVerification";
                    noteText = "Yonix Declined: Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Reply not received";
                }

                CreateAccountNote(uow, accountNumber, noteText, ref priority);

                uow.commit();

                return result;
            } finally {
                uow.close();
            }
        }

        public static PaymentResult MakePaymentByCheck(IMap imap, int accountNumber, BankCheck bankCheck, decimal paymentAmount, string application)
        {
            ValidateParameters(imap, accountNumber, bankCheck, paymentAmount);

            UOW uow = null;

            try {
                uow = new UOW(imap, "PaymentSvc.MakePaymentByCheck");

                ValidatePreconditions(paymentAmount);

                int priority = 0;

                ICustInfo2 customerInfo = CreateCustomerInfo(uow, bankCheck, accountNumber, ref priority);
                IDemand demand = CreateDemand(uow, customerInfo, ref priority);
                Payment payment = CreatePayment(uow, bankCheck, demand, paymentAmount, ref priority, application);

                PaymentServiceProvider provider = new PaymentServiceProvider();
                PaymentResult result = provider.MakeCheckPayment(accountNumber, bankCheck, paymentAmount);
                result.Payment = payment;

                string noteText = string.Empty;

                if (result.Code == PaymentResultCode.Completed) {
                    PaymentTransaction transaction = CreatePaymentTransaction(uow, accountNumber, paymentAmount, ref priority);

                    // Link the payment with the transaction of the external payment service.
                    payment.PaymentTransaction = transaction;

                    // Enforce transaction object to be saved before saving the payment object.
                    payment.Priority = transaction.Priority + 1;

                    demand.Status = "Completed";

                    noteText = "Yonix Approved: Check payment Approved on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Approval Number " + result.Payment.Id;
                } else if (result.Code == PaymentResultCode.Rejected) {
                    demand.Status = "Rejected";
                    noteText = "Yonix Declined: Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + result.Description;
                } else if (result.Code == PaymentResultCode.UnableToComplete) {
                    demand.Status = "UnableToComplete";
                    noteText = "Yonix Declined: Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Cannot connect";
                } else if (result.Code == PaymentResultCode.NeedVerification) {
                    demand.Status = "NeedVerification";
                    noteText = "Yonix Declined: Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Reply not received";
                }

                CreateAccountNote(uow, accountNumber, noteText, ref priority);

                uow.commit();

                return result;
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Private Methods

        #region Validation Methods

        private static void ValidateParameters(IMap imap, int accountNumber)
        {
            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            if (accountNumber <= 0) {
                throw new ArgumentException("accountNumber");
            }
        }

        private static void ValidateParameters(IMap imap, int accountNumber, CreditCard creditCard, decimal paymentAmount)
        {
            ValidateParameters(imap, accountNumber);

            if (creditCard == null) {
                throw new ArgumentNullException("creditCard");
            }

            if (paymentAmount <= 0) {
                throw new ArgumentException("paymentAmount");
            }
        }

        private static void ValidateParameters(IMap imap, int accountNumber, BankCheck bankCheck, decimal paymentAmount)
        {
            ValidateParameters(imap, accountNumber);

            if (bankCheck == null) {
                throw new ArgumentNullException("bankCheck");
            }

            if (paymentAmount <= 0) {
                throw new ArgumentException("paymentAmount");
            }
        }

        private static void ValidatePreconditions(decimal paymentAmount)
        {
            if (paymentAmount < MINIMUM_PAYMENT_AMOUNT) {
                throw new ArgumentException("paymentAmount", "Payment amount can not be less than minimum payment amount: " + MINIMUM_PAYMENT_AMOUNT.ToString("c"));
            }

            if (paymentAmount > MAXIMUM_PAYMENT_AMOUNT) {
                throw new ArgumentException("paymentAmount", "Payment amount can not be bigger than maximum payment amount: " + MAXIMUM_PAYMENT_AMOUNT.ToString("c"));
            }
        }

        #endregion

        private static ICustInfo2 CreateCustomerInfo(UOW uow, BankCheck bankCheck, int accountNumber, ref int priority)
        {
            CustAddress mailAddress = new CustAddress(uow);

            mailAddress.Zipcode = bankCheck.Zip;
            mailAddress.State = bankCheck.State;
            mailAddress.City = bankCheck.City;
            mailAddress.Street = bankCheck.StreetAddress;
            mailAddress.Priority = priority++;

            CustAddress servAddress = new CustAddress(uow);

            servAddress.Zipcode = bankCheck.Zip;
            servAddress.State = bankCheck.State;
            servAddress.City = bankCheck.City;
            servAddress.Street = bankCheck.StreetAddress;
            servAddress.Priority = priority++;

            CustInfo customerInfo = new CustInfo(uow);

            customerInfo.LastName = bankCheck.LastName;
            customerInfo.FirstName = bankCheck.FirstName;
            customerInfo.Email = bankCheck.Email;
            customerInfo.AccNumber = accountNumber;
            // TODO: reformat phone number (803) 46-5 -> 803465
            customerInfo.PhNumber = bankCheck.PhoneNumber;
            customerInfo.MailAddr = mailAddress;
            customerInfo.ServAddr = servAddress;
            customerInfo.Priority = priority++;

            return customerInfo;
        }

        private static ICustInfo2 CreateCustomerInfo(UOW uow, CreditCard creditCard, int accountNumber, ref int priority)
        {
            CustAddress mailAddress = new CustAddress(uow);

            mailAddress.Zipcode = creditCard.Zip;
            mailAddress.State = creditCard.State;
            mailAddress.City = creditCard.City;
            mailAddress.Street = creditCard.StreetAddress;
            mailAddress.Priority = priority++;

            CustAddress servAddress = new CustAddress(uow);

            servAddress.Zipcode = creditCard.Zip;
            servAddress.State = creditCard.State;
            servAddress.City = creditCard.City;
            servAddress.Street = creditCard.StreetAddress;
            servAddress.Priority = priority++;

            CustInfo customerInfo = new CustInfo(uow);

            customerInfo.LastName = creditCard.LastName;
            customerInfo.FirstName = creditCard.FirstName;
            customerInfo.Email = creditCard.Email;
            customerInfo.AccNumber = accountNumber;
            // TODO: reformat phone number (803) 46-5 -> 803465
            customerInfo.PhNumber = creditCard.PhoneNumber;
            customerInfo.MailAddr = mailAddress;
            customerInfo.ServAddr = servAddress;
            customerInfo.Priority = priority++;

            return customerInfo;
        }

        private static IDemand CreateDemand(UOW uow, ICustInfo2 customerInfo, ref int priority)
        {
            IDemand demand = DmdFactory.GetDemand(uow, DemandType.Internet.ToString());

            demand.Consumer = customerInfo;
            demand.Status = "Pending";
            demand.BillPayer = customerInfo.AccNumber;
            demand.Priority = priority++;

            return demand;
        }

        private static Payment CreatePayment(UOW uow, CreditCard creditCard, IDemand demand, decimal paymentAmount, ref int priority, string application)
        {
            CreditCardPayment payment = new CreditCardPayment(uow);

            payment.Demand = demand;
            payment.Amount = paymentAmount;
            payment.CcType = creditCard.Type;
            payment.CcNumber = creditCard.Number;
            payment.CvNumber = creditCard.CvNumber;
            payment.ExpYear = creditCard.ExpYear;
            payment.ExpMonth = creditCard.ExpMonth;
            payment.Priority = priority++;
            payment.Application = application;

            return payment;
        }

        private static Payment CreatePayment(UOW uow, BankCheck bankCheck, IDemand demand, decimal paymentAmount, ref int priority, string application)
        {
            BankCheckPayment payment = new BankCheckPayment(uow);

            payment.Demand = demand;
            payment.Amount = paymentAmount;
            payment.BankAccountNumber = bankCheck.BankAccountNumber;
            payment.BankRoutingNumber = bankCheck.BankRoutingNumber;
            payment.DriverLicenseState = bankCheck.DriverLicenseState;
            payment.DriverLicenseNumber = bankCheck.DriverLicenseNumber;
            payment.Application = application;

            payment.Priority = priority++;

            return payment;
        }

        private static PaymentTransaction CreatePaymentTransaction(UOW uow, int accountNumber, decimal paymentAmount, ref int priority)
        {
            PaymentTransaction transaction = new PaymentTransaction(uow);

            transaction.AccNumber = accountNumber;
            transaction.LocalAmount = paymentAmount;
            transaction.PayDate = DateTime.Now;
            transaction.TransactionMethodId = GetTransactionMethodId();
            transaction.TransactionTypeId = GetTransactionTypeId();
            transaction.StoreCode = GetStoreCode();
            transaction.Priority = priority++;

            return transaction;
        }

        private static int GetTransactionMethodId()
        {
            // TODO: clarify with Boris.
            return 1;
        }

        private static int GetTransactionTypeId()
        {
            // TODO: clarify with Boris.
            return 2;
        }

        private static string GetStoreCode()
        {
            // TODO: clarify with Boris.
            return "DPI1234567";
        }

        private static Account_Notes CreateAccountNote(UOW uow, int accountNumber, string noteText, ref int priority)
        {
            // TODO: clarify with Boris.
            string department = "Customer Service";

            Account_Notes notes = new Account_Notes(uow, accountNumber, "Public", noteText, department);

            notes.Priority = priority++;

            return notes;
        }

        #endregion
    }
}