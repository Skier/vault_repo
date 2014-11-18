using System;
using System.Configuration;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
    public sealed class PaymentSvc
    {
        private const string PAYMENT_FAILED = "Payment processing failed.";

        #region Constructors

        private PaymentSvc()
        {
        }

        #endregion

        #region Public Methods

        #region Home Phone Service Payments

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

        public static Payment[] GetPaymentsByAccNumber(IMap imap, int accNumber)
        {
            ValidateParameters(imap, accNumber);

            UOW uow = null;

            try {
                uow = new UOW(imap, "PaymentSvc.GetPaymentsByAccNumber");
                Payment[] payments = Payment.GetPaymentsByAccNumber(uow, accNumber);

                // load payment info entity.
                foreach (Payment currentPayment in payments) {
                    currentPayment.Demand = Demand.find(uow, currentPayment.DemandId);
                }

                return payments;
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Wireless Service Payments

        public static PaymentResult MakeWirelessCreditCardPayment(IMap imap, IUser user, int accountNumber, IWireless_Products product, IWirelessOrderSum orderSum, CreditCard creditCard, decimal paymentAmount, out ICellPhoneReceipt receipt)
        {
            return MakeWirelessPayment(imap, user, accountNumber, product, orderSum, creditCard, null, paymentAmount, out receipt);
        }

        public static PaymentResult MakeWirelessCheckPayment(IMap imap, IUser user, int accountNumber, IWireless_Products product, IWirelessOrderSum orderSum, BankCheck bankCheck, decimal paymentAmount, out ICellPhoneReceipt receipt)
        {
            return MakeWirelessPayment(imap, user, accountNumber, product, orderSum, null, bankCheck, paymentAmount, out receipt);
        }

        private static PaymentResult MakeWirelessPayment(IMap imap, IUser user, int accountNumber, IWireless_Products product, IWirelessOrderSum orderSum, CreditCard creditCard, BankCheck bankCheck, decimal paymentAmount, out ICellPhoneReceipt receipt)
        {
            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            if (user == null) {
                throw new ArgumentNullException("user");
            }

            if (product == null) {
                throw new ArgumentNullException("product");
            }

            if (orderSum == null) {
                throw new ArgumentNullException("orderSum");
            }

            if (creditCard == null && bankCheck == null) {
                throw new ArgumentException("Both creditCard and bankCheck parameters can not be null.");
            }

            if (creditCard != null && bankCheck != null) {
                throw new ArgumentException("Both creditCard and bankCheck parameters can not be not null.");
            }

            PaymentType paymentType = creditCard != null ? PaymentType.Credit : PaymentType.Check;

            IWireless_Custdata customerData = null;
            IWirelessDeviceData deviceData = null;
            IPayInfo payInfo = null;
            ICellPhoneInfo ci = null;
            Payment payment = null;

            int priority = 0;

            try {
                customerData = CustSvc.GetWirelessCustData(imap, accountNumber);
                deviceData = DpiWirelessSvc.GetWLDeviceDataResp(customerData.ESN);

                orderSum.Demand.Priority = priority++;
                orderSum.Demand.BillPayer = accountNumber;
            
                payInfo = CreatePayInfo(imap, false, orderSum.Demand, paymentType, paymentAmount, ref priority);
                ci = CreateCellPhoneInfo(customerData, product, ref priority);

                if (paymentType == PaymentType.Credit) {
                    payment = CreatePayment(imap, payInfo, creditCard, orderSum.Demand, paymentAmount, "Wireless", ref priority);
                } else {
                    payment = CreatePayment(imap, payInfo, bankCheck, orderSum.Demand, paymentAmount, "Wireless", ref priority);
                }

                CustSvc.PreSave(imap);
            } catch (Exception ex) {
                CancelWirelessPayment(imap, user, accountNumber, orderSum.Demand, payInfo, PAYMENT_FAILED + " " + ex.Message);
                throw new PaymentException(payment, PAYMENT_FAILED, ex);
            }

            try {
                receipt = DpiWirelessSvc.Replenish(imap, user, deviceData.Provider, customerData, payInfo, orderSum.TaxAmt, ci, product);

                int attemptCount = int.Parse(ConfigurationSettings.AppSettings["DpiWLNumOfTimesToRetry"]);
                for (int attempts = 0; !receipt.Pass && attempts < attemptCount; attempts++) {
                    receipt = DpiWirelessSvc.CheckPlanStatus(imap, user, deviceData.Provider, customerData, payInfo, orderSum.TaxAmt, ci, product);
                }
            
                receipt.Pin = ci.Pin;
                receipt.ControlNumber = ci.ControlNumber;
                receipt.ConfNum = payInfo.Id.ToString();

                CheckCustomerEmail(customerData, receipt);

                payInfo.Status = PaymentStatus.Paid.ToString();
                orderSum.Demand.Status = DemandStatus.Approved.ToString();
                
                // Invoke it so as to get IWireless_Transactions.Wireless_Transaction_ID (payInfo.Tran.TranNumber)
                CustSvc.PreSave(imap); 

                payInfo.TranNumber = payInfo.Tran.TranNumber;

                // Invoke it so as to save IWireless_Transactions.Wireless_Transaction_ID
                CustSvc.PreSave(imap);

            } catch (Exception ex) {
                CancelWirelessPayment(imap, user, accountNumber, orderSum.Demand, payInfo, PAYMENT_FAILED + " " + ex.Message);
                throw new PaymentException(payment, PAYMENT_FAILED, ex);
            }

            PaymentResult result;

            try {
                PaymentServiceProvider provider = new PaymentServiceProvider();

                if (paymentType == PaymentType.Credit) {
                    result = provider.MakeCreditCardPayment(accountNumber, creditCard, paymentAmount);
                } else {
                    result = provider.MakeCheckPayment(accountNumber, bankCheck, paymentAmount);
                }

                if (result.Code == PaymentResultCode.Rejected || result.Code == PaymentResultCode.UnableToComplete) {
                    CancelWirelessPayment(imap, user, accountNumber, orderSum.Demand, payInfo, string.Empty);
                }
            } catch (Exception ex) {
                CancelWirelessPayment(imap, user, accountNumber, orderSum.Demand, payInfo, PAYMENT_FAILED + " " + ex.Message);
                throw new PaymentException(payment, PAYMENT_FAILED, ex);
            }

            result.Payment = payment;

            string noteText = GetNoteText(paymentType, result, paymentAmount, receipt.ConfNum);
            DpiWirelessSvc.SetAccountNotes(imap, accountNumber, user.ClerkId, noteText);

            return result;
        }

        private static string GetNoteText(PaymentType paymentType, PaymentResult result, decimal paymentAmount, string confirmationNumber)
        {
            if (paymentType == PaymentType.Credit) {
                if (result.Code == PaymentResultCode.Completed) {
                    return "Credit Card payment Approved on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Approval/Confirmation Number " + confirmationNumber;
                } else if (result.Code == PaymentResultCode.Rejected) {
                    return "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + result.Description + ". Approval/Confirmation Number " + confirmationNumber;
                } else if (result.Code == PaymentResultCode.UnableToComplete) {
                    return "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Cannot connect. Approval/Confirmation Number " + confirmationNumber;
                } else if (result.Code == PaymentResultCode.NeedVerification) {
                    return "Credit Card payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Reply not received. Approval/Confirmation Number " + confirmationNumber;
                } else {
                    throw new ApplicationException("Payment result code is unknown: " + result.Code + ".");
                }
            } else if (paymentType == PaymentType.Check) {
                if (result.Code == PaymentResultCode.Completed) {
                    return "Check payment Approved on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Approval/Confirmation Number " + confirmationNumber;
                } else if (result.Code == PaymentResultCode.Rejected) {
                    return "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: " + result.Description + ". Approval/Confirmation Number " + confirmationNumber;
                } else if (result.Code == PaymentResultCode.UnableToComplete) {
                    return "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Cannot connect. Approval/Confirmation Number " + confirmationNumber;
                } else if (result.Code == PaymentResultCode.NeedVerification) {
                    return "Check payment Declined on " + DateTime.Now.ToShortDateString() + " in the amount of " + paymentAmount.ToString("C") + ". Declined Reason: Reply not received. Approval/Confirmation Number " + confirmationNumber;
                } else {
                    throw new ApplicationException("Payment result code is unknown: " + result.Code + ".");
                }
            }
            
            throw new ApplicationException("Payment type is unknow: " + paymentType + ".");
        }

        private static void CheckCustomerEmail(IWireless_Custdata customerData, ICellPhoneReceipt rct) 
        {
            if (rct.Mdn == null || rct.Mdn == "") {
                return;
            }

            if (customerData.Email != null && customerData.Email.Length > 6) {
                return;
            }

            customerData.Email = rct.Mdn + "@messaging.sprintpcs.com";
        }

        private static ICellPhoneInfo CreateCellPhoneInfo(IWireless_Custdata customerData, IWireless_Products product, ref int priority) 
        {
            ICellPhoneInfo pi = PinSvc.GetCellInfo();

            pi.NewESN = customerData.ESN;
            pi.Zip = customerData.Zip;
            pi.PhoneNumber = customerData.PhNumber;
            pi.WireleesProduct = product.Wireless_product_id;
            pi.ControlNumber = product.Soc;
            pi.ActivationCharge = 0m;
            pi.Priority = priority++;

            return pi;
        }

        private static IPayInfo CreatePayInfo(IMap imap, bool isConfReq, IDemand demand, PaymentType paymentType, decimal paymentAmount, ref int priority)
        {
            IPayInfo payInfo = PaySvc.GetNewPayInfo(imap, demand, PayInfoClass.PayInfo);
			
            payInfo.Status = PaymentStatus.Incomplete.ToString();			
            payInfo.IsConfReq = isConfReq;
            payInfo.PaymentType = paymentType;
            payInfo.TotalAmountDue = payInfo.TotalAmountPaid = payInfo.AmountTendered = paymentAmount;
            payInfo.Priority = priority++;

            return payInfo;
        }

        private static void CancelWirelessPayment(IMap imap, IUser user, int accountNumber, IDemand demand, IPayInfo payInfo, string noteText)
        {
            try {
                if (demand != null) {
                    demand.Status = DemandStatus.Cancelled.ToString();
                }

                if (payInfo != null) {
                    payInfo.Status = PaymentStatus.Cancelled.ToString();
                }

                if (payInfo != null && payInfo.Tran != null) {
                    bool manualTransactionCancelling = false;

                    Wireless_Transactions transaction = (Wireless_Transactions)payInfo.Tran;
                    if (transaction.RowState == RowState.New) {
                        manualTransactionCancelling = true;

                        UOW uow = null;

                        try {
                            uow = new UOW(imap, "PaymentSvc.CancelWirelessPayment()");
                            AOL_PINs.ResetPin(uow, transaction.Pin);
                            uow.commit();
                        } finally {
                            uow.close();
                        }
                    } else {
                        manualTransactionCancelling = !DpiWirelessSvc.CancelWLTransaction(imap, user.ClerkId, transaction.Wireless_Transaction_ID);

                        // Do not reset PIN!

                        ErrLogging.LogError("PaymentSvc.CancelWirelessPayment()", user.ClerkId, "Resetting " + transaction.Pin + " pin failed. Approval/Confirmation number is " + payInfo.Id + ".");
                    }

                    if (manualTransactionCancelling) {
                        transaction.Status = PaymentStatus.Cancelled.ToString();
                    }
                }

                CustSvc.PreSave(imap);

                if (noteText != string.Empty) {
                    if (payInfo != null && ((DomainObj)payInfo).RowState != RowState.New) {
                        noteText += " Approval/Confirmation number is " + payInfo.Id + ".";
                    }

                    DpiWirelessSvc.SetAccountNotes(imap, accountNumber, user.ClerkId, noteText);
                }
            } catch (Exception ex) {
                string message = "Cancelling of wireless payment failed. " + ex.Message;
                
                if (payInfo != null && ((DomainObj)payInfo).RowState != RowState.New) {
                    message += " Approval/Confirmation number is " + payInfo.Id + ".";
                }

                message += " Payment failure reason is: " + noteText;

                try {
                    DpiWirelessSvc.SetAccountNotes(imap, accountNumber, user.ClerkId, message);
                } catch (Exception) {
                }
            }
        }

        #endregion

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

        private static Payment CreatePayment(IMap imap, IPayInfo paymentInfo, CreditCard creditCard, IDemand demand, 
            decimal paymentAmount, string application, ref int priority) 
        {
            CreditCardPayment payment = new CreditCardPayment(imap);

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

        private static Payment CreatePayment(IMap imap, IPayInfo paymentInfo, BankCheck bankCheck, IDemand demand, decimal paymentAmount, string application, ref int priority) 
        {
            BankCheckPayment payment = new BankCheckPayment(imap);

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