using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
    public sealed class PaymentSvc
    {
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
        public static PaymentResult MakeCreditCardPayment(IMap imap, int accountNumber, 
			CreditCard creditCard, decimal paymentAmount, decimal dueAmount, string application, 
			string storeCode, string clerkId)
        {
            ValidateParameters(imap, accountNumber, creditCard, paymentAmount);

            UOW uow = null;
			ICustInfo2 customerInfo;
			IDemand demand;
			Payment payment;
			int priority = 0;
			string noteText = null;
			int confNum = 0;
			PaymentResult result = new PaymentResult(PaymentResultCode.Rejected, "", "");
			IVerifoneResult verifoneResult;
			IPayInfoLocal payInfoLocal;

			ValidatePreconditions(paymentAmount);

			try 
			{
				uow = new UOW(imap, "PaymentSvc.MakeCreditCardPayment");	

				priority = 0;
				customerInfo = CreateCustomerInfo(uow, creditCard, accountNumber, ref priority);
				demand = CreateDemand(uow, customerInfo, storeCode, ref priority);
				payInfoLocal = CreatePayInfo(imap, demand,dueAmount, PaymentType.Credit, paymentAmount, ref priority);
				payment = CreatePayment(uow, payInfoLocal, creditCard, demand, dueAmount, paymentAmount, ref priority, application);

				uow.commit();
			}
			finally
			{
				 uow.close();
			}

			try
			{

				uow = new UOW(imap, "PaymentSvc.MakeCreditCardPayment");

				try
				{
					uow.BeginTransaction();
					verifoneResult = VerifoneWrapper.SubmitMonthlyXact(uow, storeCode,
							clerkId, payment.Id.ToString(), customerInfo.PhNumber, paymentAmount, 
							0,"Monthly Payment"); 
					confNum = verifoneResult.ConfNum; 
				}
				catch (TransactionException ex)
				{
						uow.Rollback();
						// TODO:  Create another rejected status
						demand.Status = "VFTRejected";
						payInfoLocal.Status = "Cancelled";
						string message = ex.Message.Replace("\\n", ". ");
						result = new PaymentResult(PaymentResultCode.Rejected, "", message);
						noteText = "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + message;
				}

				if (noteText == null)  // Verifone wrapper success
				{

					PaymentServiceProvider provider = new PaymentServiceProvider();

					result = provider.MakeCreditCardPayment(accountNumber, creditCard, paymentAmount);
					result.Payment = payment;

					noteText = string.Empty;

					if (result.Code == PaymentResultCode.Completed) 
					{
						uow.commit();
                   
						// Enforce transaction object to be saved before saving the payment object.
						payment.Priority = priority++;
						demand.Status = "Completed";
						payInfoLocal.Status = "Paid";
					

						noteText = "Credit Card payment Approved on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Approval/Confirmation Number " + confNum;
					} 
					else if (result.Code == PaymentResultCode.Rejected) 
					{
						uow.Rollback();
						demand.Status = "Rejected";
						payInfoLocal.Status = "Error";
						noteText = "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + result.Description;
					} 
					else if (result.Code == PaymentResultCode.UnableToComplete) 
					{
						uow.Rollback();
						demand.Status = "UnableToComplete";
						payInfoLocal.Status = "Error";
						noteText = "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Cannot connect";
					} 
					else if (result.Code == PaymentResultCode.NeedVerification) 
					{
						uow.Rollback();
						demand.Status = "NeedVerification";
						payInfoLocal.Status = "Error";
						noteText = "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Reply not received";
					}
				}
			}
			finally
			{
				uow.close();
			}



			try{

				uow = new UOW(imap, "PaymentSvc.MakeCreditCardPayment");
                CreateAccountNote(uow, accountNumber, noteText, ref priority);
				uow.commit();
				if(result.Payment != null) result.Payment.ConfNum = confNum;
                return result;
            } finally {
                uow.close();
            }
        }

        public static PaymentResult MakePaymentByCheck(IMap imap, int accountNumber, BankCheck bankCheck, 
			decimal paymentAmount, decimal dueAmount, string application, string storeCode, string clerkId)
        {
            ValidateParameters(imap, accountNumber, bankCheck, paymentAmount);

			UOW uow = null;
			ICustInfo2 customerInfo;
			IDemand demand;
			Payment payment;
			int priority = 0;
			string noteText = null;
			int confNum = 0;
			PaymentResult result = new PaymentResult(PaymentResultCode.Rejected, "", "");
			IVerifoneResult verifoneResult;
			IPayInfoLocal payInfoLocal;

			ValidatePreconditions(paymentAmount);

			try 
			{
				uow = new UOW(imap, "PaymentSvc.MakePaymentByCheck");	

				priority = 0;

				customerInfo = CreateCustomerInfo(uow, bankCheck, accountNumber, ref priority);
				demand = CreateDemand(uow, customerInfo,storeCode, ref priority);
				payInfoLocal = CreatePayInfo(imap, demand,dueAmount, PaymentType.Check, paymentAmount, ref priority);
				payment = CreatePayment(uow, payInfoLocal, bankCheck, demand, dueAmount,  paymentAmount, ref priority, application);
				uow.commit();
			}
			finally
			{
				uow.close();
			}

			try
			{
				uow = new UOW(imap, "PaymentSvc.MakeCreditCardPayment");

				try
				{
					uow.BeginTransaction();
					verifoneResult = VerifoneWrapper.SubmitMonthlyXact(uow, storeCode, clerkId, 
						payment.Id.ToString(), customerInfo.PhNumber, paymentAmount, 0,"Monthly Payment"); 					

					confNum = verifoneResult.ConfNum;
				}
				catch (TransactionException ex)
				{
					uow.Rollback();
					// TODO:  Create another rejected status
					demand.Status = "VFTRejected";
					payInfoLocal.Status = "Cancelled";
					string message = ex.Message.Replace("\\n", ". ");
					result = new PaymentResult(PaymentResultCode.Rejected, "", message);
					noteText = "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + message;
				}

				if (noteText == null)  // Verifone wrapper success
				{
					PaymentServiceProvider provider = new PaymentServiceProvider();
					result = provider.MakeCheckPayment(accountNumber, bankCheck, paymentAmount);
					result.Payment = payment;

					noteText = string.Empty;

					if (result.Code == PaymentResultCode.Completed) 
					{
						uow.commit();
                   
						// Enforce transaction object to be saved before saving the payment object.
						payment.Priority = priority++;
						demand.Status = "Completed";
						payInfoLocal.Status = "Paid";
						noteText = "Check payment Approved on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Approval/Confirmation Number " + confNum;
					} 
					else if (result.Code == PaymentResultCode.Rejected) 
					{
						uow.Rollback();
						demand.Status = "Rejected";
						payInfoLocal.Status = "Error";
						noteText = "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + result.Description;
					} 
					else if (result.Code == PaymentResultCode.UnableToComplete) 
					{
						uow.Rollback();
						demand.Status = "UnableToComplete";
						payInfoLocal.Status = "Error";
						noteText = "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Cannot connect";
					} 
					else if (result.Code == PaymentResultCode.NeedVerification) 
					{
						uow.Rollback();
						demand.Status = "NeedVerification";
						payInfoLocal.Status = "Error";
						noteText = "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Reply not received";
					}
				}
			}
			finally
			{
				uow.close();
			}

			try
			{
				uow = new UOW(imap, "PaymentSvc.MakeCreditCardPayment");
                CreateAccountNote(uow, accountNumber, noteText, ref priority);
                uow.commit();
				if(result.Payment != null) result.Payment.ConfNum = confNum;
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
            decimal min, max;
            Payment.GetMinMaxPaymentAmount(out min, out max);

            if (paymentAmount < min) {
                throw new ArgumentException("paymentAmount", "Payment amount can not be less than minimum payment amount: " + min.ToString("c"));
            }
            
            if (paymentAmount > max) {
                throw new ArgumentException("paymentAmount", "Payment amount can not be bigger than maximum payment amount: " + max.ToString("c"));
            }
        }

        #endregion

		private static IPayInfoLocal CreatePayInfo(IMap imap, IDemand demand, 
			decimal amtDue, PaymentType paymentType, decimal paymentAmount,ref int priority)
		{
			IPayInfoLocal payInfo = (IPayInfoLocal) PaySvc.GetNewPayInfo(imap,demand,PayInfoClass.PayInfoLocal);
			payInfo.SetAmts(paymentAmount, amtDue, 0, paymentAmount);
			payInfo.PaymentType = paymentType;
			payInfo.Status = "Pend";

			payInfo.Priority = priority++;
			

			return payInfo;
		}
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

        private static IDemand CreateDemand(UOW uow, ICustInfo2 customerInfo, string storeCode, ref int priority)
        {
            IDemand demand = DmdFactory.GetDemand(uow, DemandType.Monthly.ToString());

            demand.Consumer = customerInfo;
            demand.Status = "Pending";
            demand.BillPayer = customerInfo.AccNumber;
            demand.Priority = priority++;
			demand.StoreCode = storeCode;


            return demand;
        }

        private static Payment CreatePayment(UOW uow, IPayInfoLocal paymentInfo, CreditCard creditCard, IDemand demand, decimal dueAmount, 
			decimal paymentAmount, ref int priority, string application)
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
            payment.NameFirst = creditCard.FirstName;
            payment.NameLast = creditCard.LastName;
            payment.Address = creditCard.StreetAddress;
            payment.Zip = creditCard.Zip;
            payment.PaymentInfo = paymentInfo;

            return payment;
        }

        private static Payment CreatePayment(UOW uow, IPayInfoLocal paymentInfo, BankCheck bankCheck, IDemand demand, decimal dueAmount, decimal paymentAmount, ref int priority, string application)
        {
            BankCheckPayment payment = new BankCheckPayment(uow);

            payment.Demand = demand;
            payment.Amount = paymentAmount;
            payment.BankAccountNumber = bankCheck.BankAccountNumber;
            payment.BankRoutingNumber = bankCheck.BankRoutingNumber;
            payment.DriverLicenseState = bankCheck.DriverLicenseState;
            payment.DriverLicenseNumber = bankCheck.DriverLicenseNumber;
            payment.Address = bankCheck.StreetAddress;
            payment.Application = application;
            payment.PaymentInfo = paymentInfo;
			payment.NameFirst = bankCheck.FirstName;
			payment.NameLast = bankCheck.LastName;
			payment.Zip = bankCheck.Zip;
            payment.Priority = priority++;

            return payment;
        }

        private static PaymentTransaction CreatePaymentTransaction(UOW uow, int accountNumber, decimal paymentAmount, ref int priority)
        {
            PaymentTransaction transaction = new PaymentTransaction(uow);

            transaction.AccNumber = accountNumber;
            transaction.LocalAmount = paymentAmount;
            transaction.PayDate = DateTime.Now;
            transaction.TransactionMethodId = 1;
            transaction.TransactionTypeId = 2;
            transaction.StoreCode = "DPI1234567";
            transaction.Priority = priority++;

            return transaction;
        }

        private static Account_Notes CreateAccountNote(UOW uow, int accountNumber, string noteText, ref int priority)
        {            
            string department = "Internet";

            Account_Notes notes = new Account_Notes(uow, accountNumber, "WebAccess", noteText, department);

            notes.Priority = priority++;

            return notes;
        }

        #endregion
    }
}