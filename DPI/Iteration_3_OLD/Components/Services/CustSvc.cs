using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class RemindWebPasswordResult
	{
		public WebValidationResultStatus Status;
		public string FirstName;
		public string EmailAddress;
		public string WebPassword;

	}
    public class WebValidationResult
    {
        private int accNumber;
        private WebValidationResultStatus status;
        private bool isPasswordTemporal;

        public int AccNumber
        {
            get { return accNumber; }
            set { accNumber = value; }
        }

        public WebValidationResultStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public bool IsPasswordTemporal
        {
            get { return isPasswordTemporal; }
            set { isPasswordTemporal = value; }
        }
    }

	public class ChangeAccountSettingsResult
	{
		public string FirstName;
		public string OldEmailAddress;
		public string EmailAddress;
	}

    public enum WebValidationResultStatus
    {
        ValidCustomer,
        InvalidCustomer,
        CustomerNotSetupYet
    }

	public enum EnableWebAccessStatus
	{
		Success,
		AccountNumberInvalid,
		LastNameDoesNotMatch
	}

    public class CustSvc
    {
        #region Notes

        public static IAcctNotes GetNotes(IMap imap, string user, string note, string dept) {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetAccNotes");

                return new Notes(uow, user, note);
            }
            finally {
                uow.close();
            }
        }
        public static IAcctNotes GetNotes(IMap imap, string text, string clerkid) {
            UOW uow = null;
            try {
                uow = new UOW(imap);
                return new Notes(uow, text, clerkid);
            }
            finally {
                uow.close();

            }
        }

        #endregion

        #region New Orders

        public static IReceipt SubmitNewXact(IMap imap, IDemand dmd, int ilec, string storeCode, IUser user, string transnum,
                                             IPayInfo payInfo, string commPort, IAcctNotes notes)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap);
                uow.Service = "CustSvc.SubmitNewXact() - Create Account";
                uow.BeginTransaction();

                IReceipt receipt
                    = YonixNewTran.WriteTransaction(uow, dmd, ilec, storeCode, user, transnum, payInfo, null, notes);

                //payInfo.UpdateFromVerifone(uow, receipt);

                WebServSvc.PresetPymt(uow, user, payInfo, receipt.ConfNum);

                uow.commit();
                return receipt;
            } finally {
                if (uow != null) {
                    uow.close();
                }
                imap.ClearDomainObjs();
            }
        }

        public static IOrderSum GetOrderSummary // new methods
            (IMap imap, IProdPrice[] prods, string zipcode, IILECInfo ilec, string dmdType, OrderType ot)
        {
            UOW uow = null;

            if (prods == null) {
                throw new ArgumentException("Products are required");
            }

            if (zipcode == null) {
                throw new ArgumentException("Zipcode is required");
            }

            if (ilec == null) {
                throw new ArgumentException("IlecCode is required");
            }

            try {
                uow = new UOW(imap);
                return Demand.BuildDmd(uow, prods, zipcode, ilec, dmdType, ot).OrderSummary(uow);
            } finally {
                imap.ClearDomainObjs();
                if (uow != null) {
                    uow.close();
                }
            }
        }

        public static IOrderSum GetOrderSummary(IMap imap, IDemand demand)
        {
            if (demand == null) {
                throw new ArgumentException("Demand is required.");
            }

            UOW uow = null;
            try {
                uow = new UOW("CustSvc.GetOrderSummary");
                return demand.OrderSummary(uow);
            } finally {
                uow.close();
            }
        }

		public static IWirelessOrderSum GetWLOrderSummary
			(IMap imap, IWireless_Products[] prods, string storeCode, string dmdType)
		{
			UOW uow = null;
		
			if (prods == null) 
				throw new ArgumentException("Products are required");
					
			try 
			{
				uow = new UOW(imap);
				StoreLocation sl = StoreLocation.find(uow, storeCode);

				return Demand.BuildDmd(uow, prods, sl.Zip, dmdType).WirelessOrderSum(uow);
			} 
			finally 
			{
				imap.ClearDomainObjs();
				if (uow != null) 
					uow.close();				
			}
		}
        public static IReceipt SubmitNewOrder2(IMap imap, IUser user, INewOrderDTO dto, IAcctNotes notes)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap);
                uow.Service = "CustSvc.SubmitNewOrder()";

                uow.BeginTransaction();

                IReceipt recpt = YonixNewTran.SubmitNewOrder2(uow, dto, notes);

                //pymt.UpdateFromVerifone(uow, recpt);

                //WebServSvc.PresetPymt(uow, user, pymt, recpt.ConfNum);

                uow.commit();

                return recpt;
            } finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        public static IReceipt SubmitNewOrder2(IMap imap, IUser user, INewOrderDTO dto)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap);
                uow.Service = "CustSvc.SubmitNewOrder()";

                uow.BeginTransaction();

                IReceipt recpt = YonixNewTran.SubmitNewOrder2(uow, dto, null);

                //pymt.UpdateFromVerifone(uow, recpt);

                //WebServSvc.PresetPymt(uow, user, pymt, recpt.ConfNum);

                uow.commit();

                return recpt;
            } finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        public static INewOrderDTO GetNewOrderDTO() {
            return new NewOrderDTO();
        }

        #endregion		

        #region Order Status
        
        public static ActivationWorkLog[] GetOrderStatus(IMap imap, int accountNumber) {
            if (imap == null) {
                throw new ArgumentException("imap is required");
            }

            if (accountNumber <= 0) {
                throw new ArgumentException("accNumber Required");
            }
            
            UOW uow = new UOW(imap, "CustSvc.GetOrderStatus");
            try {
                ActivationWorkLog[] recs = ActivationWorkLog.getByAccountNumber(uow, accountNumber);

                return recs;
            } finally {
                uow.close();
            }
        }

        #endregion Order Status

        #region Pending Payments 

        public static void CancelPendPayment(IMap imap, int payInfoId) // Omar
        {
            UOW uow = null;

            try {
                uow = new UOW(imap);
                uow.BeginTransaction();
                PendingCancelation.PaymentInfo(uow, payInfoId);
                uow.commit();
            } finally {
                uow.close();
            }
        }

        public static void ConfirmPendPayment(IMap imap, string trConf, int payInfoId) // Donna
        {
            UOW uow = null;

            try {
                uow = new UOW(imap);

                uow.BeginTransaction();

                PendingXact.ConfirmPendPayment(uow, trConf, payInfoId);

                if (Demand.find(uow, PayInfo.find(uow, payInfoId).DmdId).DmdType.ToLower()
                    != DemandType.New.ToString().ToLower()
                    && Demand.find(uow, PayInfo.find(uow, payInfoId).DmdId).DmdType.ToLower()
                       != DemandType.LocalConv.ToString().ToLower()) {
                    uow.commit();
                    return;
                }

                if (PendingXact.CheckPendPayInfos(uow, payInfoId)) {
                    PendingXact.SubmitOrder(uow, payInfoId);
                }

                uow.commit();
            } catch (Exception ex) {
                uow.Rollback();
                throw new ApplicationException(ex.Message);
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Monthly Payment

        public static IReceipt SubmitMonthlyXact(
            IMap imap,
            string phNumber,
            IUser user,
            string transNum,
            IPayInfo payInfo,
            string commPort)
        {
            UOW uow = new UOW(imap);
            uow.Service = "CustSvc.SubmitMonthlyXact() - Create Monthly Payment";

            try {
                uow.BeginTransaction();

                if (!(payInfo is IPayInfoLocal)) {
                    throw new ArgumentException("Local PayInfo is expected. Found: " + payInfo.PayClass);
                }

                IVerifoneResult verifoneResult = VerifoneWrapper.SubmitMonthlyXact(
                    uow,
                    user.LoginStoreCode,
                    payInfo.ParDemand.ConsumerAgent,
                    transNum,
                    phNumber,
                    ((IPayInfoLocal) payInfo).LocalAmountPaid,
                    ((IPayInfoLocal) payInfo).LdAmount,
                    commPort);

                IReceipt receipt = new Receipt(verifoneResult.ConfNum.ToString(), verifoneResult.AccNumber);
                //payInfo.UpdateFromVerifone(uow, receipt);
                WebServSvc.PresetPymt(uow, user, payInfo, receipt.ConfNum);

                uow.commit();
                return receipt;
            } finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        #endregion		

        #region Customer Info

        public static IAcctInfo GetCustNamePend(IMap imap, IDemand dmd)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetCustNamePend");

                if (dmd.ConsId > 0) {
                    return CustInfo.find(uow, dmd.ConsId);
                }

                return Account.GetDummyAccount();
            } finally {
                uow.close();
            }
        }

        public static IAcctInfo GetCustName(IMap imap, IDemand dmd)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetCustName");

                if (dmd.BillPayer > 0) {
                    return Account.GetAccountInfo(uow, dmd.BillPayer);
                }

                if (dmd.ConsId > 0) {
                    return CustInfo.find(uow, dmd.ConsId);
                }

                return Account.GetDummyAccount();
            } finally {
                uow.close();
            }
        }

        public static IAcctInfo GetAcctInfo(IMap imap, string phNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetAcctInfo(PhNumber)");
                return Account.GetAccountInfo(uow, phNumber);
            } finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        public static IAcctInfo GetAcctInfo(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetAcctInfo(AccNumber)");
                return Account.GetAccountInfo(uow, accNumber);
            } finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        public static IAcctInfo[] GetAcctInfos(IMap imap, ICustomerFilter customerFilter)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetAcctInfos(IMap, ICustomerFilter)");

                IAcctInfo[] acctInfos = FindCustomer.FindCustomers(uow, customerFilter);

                return acctInfos;
            } finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        public static ICustInfo2 GetCustInfo(IMap imap, string custInfoType)
        {
            return new CustInfo(imap, custInfoType);
        }

        public static ICustInfo2 GetCustInfoExtPend(IMap imap, IDemand dmd)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetCustInfoExtPend(IMap imap, IDemand dmd)");
                if (dmd.BillPayer < 1) {
                    return null;
                }

                ICustInfo2[] cis = CustData.GetCustDataByAcctNumber(uow, dmd.BillPayer);
                if (cis.Length < 1) {
                    return null;
                }

                return cis[0];
            } finally {
                uow.close();
            }
        }

        public static ICustInfoExt GetCustInfoExt(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetCustInfo(AccNumber)");
                return CustData.find(uow, accNumber).CustInfoExtended;
            } finally {
                uow.close();
            }
        }

        public static ICustInfoExt2 GetCustInfoExt2(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetCustInfo(AccNumber)");
                return CustData.find(uow, accNumber).CustInfoExtended2;
            } finally {
                uow.close();
            }
        }

        public static IProdPrice[] GetProdsForAcct(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetCustInfo(AccNumber)");
                return spOrder_GetProductsForAccountWrapper.GetProducts(uow, accNumber);
            } finally {
                uow.close();
            }
        }

        public static IPastReminderNotice GetReminderNotice(IMap imap, int accNumber)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetLastReminderNotice(AccNumber)");
                return PastReminderNotice.GetReminderNotice(uow, accNumber);
            } finally {
                uow.close();
            }
        }

        public static bool IsEligibleForPromiseToPay(IMap imap, int accNumber)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.IsEligibleForPromiseToPay(AccNumber)");
                return PromiseToPay.IsEligibleForPromiseToPay(uow, accNumber);
            } finally {
                uow.close();
            }
        }

        public static bool DoesPromiseToPayExist(IMap imap, int accNumber)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.DoesPromiseToPayExist");
                IAcctInfo acctInfo = Account.GetAccountInfo(uow, accNumber);
                return PromiseToPay.DoesPromiseToPayExist(
                    uow, accNumber, acctInfo.DueDate);
            } finally {
                uow.close();
            }
        }

        public static PromiseToPay GetPromiseToPay(IMap imap, int accNumber) 
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetPromiseToPay");
                IAcctInfo acctInfo = Account.GetAccountInfo(uow, accNumber);
                return PromiseToPay.GetPromiseToPay(
                    uow, accNumber, acctInfo.DueDate);
            } finally {
                uow.close();
            }
        }

        public static DateTime GetPromiseToPayDate(IMap imap, int accNumber)
        {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetPromiseToPayDate");
                return PromiseToPay.GetPromiseToPayDate(uow, accNumber);
            } finally {
                uow.close();
            }   
        }

        public static void ClearOrderSummaryPageData(IMap imap, IDemand dmd, IPayInfo pi)
        {
            //			if (dmd == null)
            //				return;		

            UOW uow = new UOW(imap);
            uow.Service = "CustSvc.ClearDemandData";

            try {
                if (dmd != null) {
                    ((Demand) dmd).removeFromIMap(uow);
                }

                if (pi != null) {
                    if (pi is DomainObj) {
                        ((DomainObj) pi).removeFromIMap(uow);
                    }
                }
            } finally {
                imap.ClearDomainObjs();
                uow.close();
            }
        }

        public static int ReserveAccNumber(IMap imap)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap);
                uow.Service = "CustSvc.ReserveAccNumber()";
                return CustData.ReserveAccNumber(uow);
            } finally {
                uow.close();
            }
        }

        //		public static void SaveModified(IMap imap)
        //		{
        //			UOW uow = null;
        //
        //			try
        //			{
        //				uow = new UOW(imap); 
        //				uow.Service = "CustSvc.SaveDmd() - Save demand group";
        //				uow.BeginTransaction(); // this saves Demand, DmdItems, and DmdTaxes
        //				uow.commit();
        //			}
        //			finally
        //			{	
        //				uow.close();
        //			}
        //		}
        public static IReceipt GetReceipt(IDemand dmd)
        {
            return new Receipt(dmd.Id, null, dmd.BillPayer);
        }

        public static IReceipt GetReceipt(int dmd, string confNumber, int accNumber)
        {
            return new Receipt(dmd, confNumber, accNumber);
        }

        #endregion

        #region Pre -Save	

        public static void PreSave(IMap imap)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.PreSave");

                uow.BeginTransaction();
                uow.commit(); // pre-saves Bus objects.
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Customer Page

        public static ICustomerPageDTO GetCustomerPage(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetCustomerPage(IMap imap, int accNumber)");
                CustData custData = CustData.find(uow, accNumber);

                if (custData == null) {
                    return null;
                }

                CustomerPageDTO customerPageDTO = new CustomerPageDTO();

                customerPageDTO.AccNumber = accNumber;
                customerPageDTO.Birthday = custData.Birthday;
                customerPageDTO.Contact = custData.CtNumber1;
                customerPageDTO.Contact2 = custData.CtNumber2;
                customerPageDTO.Email = custData.Email;
                customerPageDTO.FirstName = custData.NameFirst;
                customerPageDTO.LastName = custData.NameLast;
                customerPageDTO.PrevILEC = custData.PrevIlec;
                customerPageDTO.PrevPhone = custData.PrevPHNum;
                customerPageDTO.Status = Account.GetStatus(custData.Status1);

                StoreLocation storeLocation = StoreLocation.find(uow, custData.StoreCode);
                if (storeLocation != null) {
                    customerPageDTO.StoreName = storeLocation.Name;
                }

                return customerPageDTO;
            } finally {
                uow.close();
            }
        }

        public static void SaveCustomerPage(IMap imap, ICustomerPageDTO customerPageDTO)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.SaveCustomerPage(IMap imap, ICustomerPage customerPage)");

                CustData custData = CustData.find(uow, customerPageDTO.AccNumber);

                // Check custData existance.
                if (custData == null) {
                    return;
                }

                uow.BeginTransaction();

                custData.Birthday = customerPageDTO.Birthday;
                custData.CtNumber1 = customerPageDTO.Contact;
                custData.CtNumber2 = customerPageDTO.Contact2;
                custData.Email = customerPageDTO.Email;
                custData.NameFirst = customerPageDTO.FirstName;
                custData.NameLast = customerPageDTO.LastName;
                custData.PrevIlec = customerPageDTO.PrevILEC;
                custData.PrevPHNum = customerPageDTO.PrevPhone;

                custData.save();
                uow.commit();
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Customer Page

        public static IBillPageDTO GetBillPage(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetBillPage(IMap imap, int accNumber)");

                CustData custData = CustData.find(uow, accNumber);
                if (custData == null) {
                    return null;
                }

                Account account = new Account(uow, accNumber);
                if (account == null) {
                    return null;
                }

                BillPageDTO customerPageDTO = new BillPageDTO();

                customerPageDTO.AccNumber = accNumber;
                customerPageDTO.DueDate = account.DueDate;
                customerPageDTO.DiscoDate = account.DiscoDate;
                customerPageDTO.BillCycle = custData.Bill_Cycle;
                customerPageDTO.PastDueAmt = account.PastDueAmt;
                customerPageDTO.CurrDueAmt = account.CurrDueAmt;
                customerPageDTO.Balance = account.CustDataBal;
                customerPageDTO.NextPymtAmount = account.NextPymtAmt;

                return customerPageDTO;
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Address Page

        public static IAddressPageDTO GetAddressPage(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetAddressPage(IMap imap, int accNumber)");
                CustData custData = CustData.find(uow, accNumber);

                if (custData == null) {
                    return null;
                }

                AddressPage addressPage = new AddressPage();
                addressPage.MailAddress = custData.MailingAddr;

                return addressPage;
            } finally {
                uow.close();
            }
        }

        public static void SaveAddressPage(IMap imap, IAddressPageDTO addressPageDTO)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.SaveCustomerPage(IMap imap, ICustomerPage customerPage)");

                CustData custData = CustData.find(uow, addressPageDTO.AccNumber);

                // Check custData existance.
                if (custData == null) {
                    return;
                }

                uow.BeginTransaction();

                custData.Mail_AdrPfx = addressPageDTO.MailAddress.StreetPrefix;
                custData.Mail_AdrNum = addressPageDTO.MailAddress.StreetNum;
                custData.Mail_AdrStreet = addressPageDTO.MailAddress.Street;
                custData.Mail_AdrSfx = addressPageDTO.MailAddress.StreetSuffix;
                custData.Mail_AdrUnit = addressPageDTO.MailAddress.Unit;
                custData.Mail_AdrCity = addressPageDTO.MailAddress.City;
                custData.Mail_AdrPfx = addressPageDTO.MailAddress.StreetPrefix;
                custData.Mail_AdrState = addressPageDTO.MailAddress.State;
                custData.Mail_AdrZip = addressPageDTO.MailAddress.Zipcode;

                custData.save();
                uow.commit();

            } finally {
                uow.close();
            }
        }

        #endregion

        #region PaymentLog Page

        public static IPaymentLogPageDTO GetPaymentLogPage(IMap imap, int accNumber)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetAccountPaymentLogPage(IMap imap, int accNumber)");
                Account_PaymentLog[] accountPaymentLogs = Account_PaymentLog.getAccount(uow, accNumber);

                if (accountPaymentLogs == null
                    || accountPaymentLogs.Length == 0) {
                    return null;
                }

                PaymentLogPageDTO paymentLogPageDTO = new PaymentLogPageDTO();

                paymentLogPageDTO.AccNumber = accNumber;
                paymentLogPageDTO.AccountPaymentLogs = new IAccountPaymentLog[accountPaymentLogs.Length];
                accountPaymentLogs.CopyTo(paymentLogPageDTO.AccountPaymentLogs, 0);

                return paymentLogPageDTO;
            } finally {
                uow.close();
            }
        }

        public static void SavePaymentLog(IMap imap, IPaymentLogPageDTO paymentLogPageDTO)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.SavePaymentLog(IMap imap, ICustomerPage customerPage)");

                CustData custData = CustData.find(uow, paymentLogPageDTO.AccNumber);

                // Check custData existance.
                if (custData == null) {
                    return;
                }

                uow.BeginTransaction();

                // If payment log does not exists add new record
                if (paymentLogPageDTO.IsNew) {
                    Account_PaymentLog newAccount_PaymentLog = new Account_PaymentLog(uow);

                    newAccount_PaymentLog.AccNumber = paymentLogPageDTO.AccNumber;
                    newAccount_PaymentLog.Date = paymentLogPageDTO.Date;
                    newAccount_PaymentLog.Description = paymentLogPageDTO.Description;
                    newAccount_PaymentLog.Amount = paymentLogPageDTO.Amount;
                    newAccount_PaymentLog.Balance = paymentLogPageDTO.Balance;
                    newAccount_PaymentLog.add();
                } else {
                    Account_PaymentLog account_PaymentLog = Account_PaymentLog.find(uow, paymentLogPageDTO.AccountPaymentLogId);

                    // Save Existing record
                    account_PaymentLog.Date = paymentLogPageDTO.Date;
                    account_PaymentLog.Description = paymentLogPageDTO.Description;
                    account_PaymentLog.Amount = paymentLogPageDTO.Amount;
                    account_PaymentLog.Balance = paymentLogPageDTO.Balance;
                    account_PaymentLog.save();
                }

                uow.commit();
            } finally {
                uow.close();
            }
        }

        public static void DeletePaymentLog(IMap imap, int paymentLogId)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.DeletePaymentLog(IMap imap, int paymentLogId");

                Account_PaymentLog paymentLog = Account_PaymentLog.find(uow, paymentLogId);

                // Check paymentLog existance.
                if (paymentLog == null) {
                    return;
                }

                uow.BeginTransaction();

                paymentLog.delete();

                uow.commit();
            } finally {
                uow.close();
            }
        }

        public static void SaveBillPage(IMap imap, IBillPageDTO customerPageDTO)
        {
            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.SaveCustomerPage(IMap imap, ICustomerPage customerPage)");

                CustData custData = CustData.find(uow, customerPageDTO.AccNumber);

                // Check custData existance.
                if (custData == null) {
                    return;
                }

                uow.BeginTransaction();

                custData.Bill_Cycle = customerPageDTO.BillCycle;

                custData.save();
                uow.commit();
            } finally {
                uow.close();
            }
        }

        #endregion

        #region Public Web Access

        public static WebValidationResult ValidateWebAccessByPhNumber(
            IMap imap, string phNumber, string webPassword)
        {
            WebValidationResult result = new WebValidationResult();
            result.Status = WebValidationResultStatus.InvalidCustomer;

            if (imap == null) {
                throw new ArgumentException("imap is required");
            }

            if (phNumber == null || phNumber.Length != 10) {
                throw new ArgumentException("phone is required");
            }

            if (webPassword == null || webPassword.Length == 0) {
                throw new ArgumentException("webPassword is required");
            }

            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.ValidateWebAccess");

                ICustDataValidation[] custArr = CustData.ValidateCustDataByPhNumber(uow, phNumber);

                if (custArr.Length == 0) {
                    result.Status = WebValidationResultStatus.InvalidCustomer;
                    return result;
                } 

                CustData custData = CustData.find(uow, custArr[0].AccNumber);
                
                if (custData.WebPassword == null || custData.WebPassword.Length == 0) {
                    result.Status = WebValidationResultStatus.CustomerNotSetupYet;
                    return result;
                } else if (custData.WebPassword != webPassword.Trim()) {
                    result.Status = WebValidationResultStatus.InvalidCustomer;
                    return result;
                } else {
                    CustomerWebLog webLog = new CustomerWebLog(uow);

                    uow.BeginTransaction();
                    webLog.AcctNumber = custArr[0].AccNumber;
                    uow.commit();

                    result.AccNumber = custArr[0].AccNumber;
                    result.Status = WebValidationResultStatus.ValidCustomer;
                    result.IsPasswordTemporal = custData.IsWebPasswordTemporal;
                    
                    return result;
                }

            } finally {
                uow.close();
            }

        }

        public static WebValidationResult ValidateWebAccessByAccountNumber(
            IMap imap, int accountNumber)
        {
            WebValidationResult result = new WebValidationResult();
            if (imap == null) {
                throw new ArgumentException("imap is required");
            }

            if (accountNumber <= 0) {
                throw new ArgumentException("accountNumber is required");
            }

            UOW uow = null;
            try {
                // TODO : Check pwd 
                uow = new UOW(imap, "CustSvc.ValidateWebAccess");

                ICustDataValidation[] custArr = CustData.ValidateCustDataByAcctNumber(uow, accountNumber);

                if (custArr.Length == 0) {
                    result.Status = WebValidationResultStatus.InvalidCustomer;
                    return result;
                } else if (custArr[0].WebPassword == null || custArr[0].WebPassword.Length == 0) {
                    result.Status = WebValidationResultStatus.CustomerNotSetupYet;
                    return result;
                } else {
                    result.Status = WebValidationResultStatus.ValidCustomer;
                    result.AccNumber = custArr[0].AccNumber;
                    return result;
                }

            } finally {
                uow.close();
            }
        }

        public static WebValidationResult ValidateWebAccessByAccountNumber(
            IMap imap, int accountNumber, string webPassword)
        {
            WebValidationResult result = new WebValidationResult();
            if (imap == null) {
                throw new ArgumentException("imap is required");
            }

            if (accountNumber <= 0) {
                throw new ArgumentException("accountNumber is required");
            }

            if (webPassword == null || webPassword.Length == 0) {
                throw new ArgumentException("webPassword is required");
            }

            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.ValidateWebAccess");

                ICustDataValidation[] custArr = CustData.ValidateCustDataByAcctNumber(uow, accountNumber);

                if (custArr.Length == 0) {
                    result.Status = WebValidationResultStatus.InvalidCustomer;
                    return result;
                } 

                CustData custData = CustData.find(uow, custArr[0].AccNumber);
                
                if (custData.WebPassword == null || custData.WebPassword.Length == 0) {
                    result.Status = WebValidationResultStatus.CustomerNotSetupYet;
                    return result;
                } else if (custData.WebPassword != webPassword.Trim()) {
                    result.Status = WebValidationResultStatus.InvalidCustomer;
                    return result;
                } else {
                    CustomerWebLog webLog = new CustomerWebLog(uow);

                    uow.BeginTransaction();
                    webLog.AcctNumber = custArr[0].AccNumber;
                    uow.commit();

                    result.Status = WebValidationResultStatus.ValidCustomer;
                    result.AccNumber = custArr[0].AccNumber;
                    result.IsPasswordTemporal = custData.IsWebPasswordTemporal;

                    return result;
                }

            } finally {
                uow.close();
            }
        }
        
        public static ChangeAccountSettingsResult ChangeAccountSettings(IMap imap, int accNumber, string email, string webPassword)
        {
			ChangeAccountSettingsResult result;
            if (imap == null) {
                throw new ArgumentException("imap is required");
            }

            if (accNumber <= 0) {
                throw new ArgumentException("accNumber Required");
            }
            
            if (email == null || email.Length == 0) {
                throw new ArgumentException("Email address is required"); 
            }

            if (webPassword != null) {
                webPassword = webPassword.Trim();
                if(webPassword.Length == 0) {
                    throw new ArgumentException("Password is required"); 
                }
            } 
            
            UOW uow = new UOW(imap, "CustSvc.ChangeAccountSettings");
            try {
                CustData custData = CustData.find(uow, accNumber);
                if (custData == null) {
                    throw new ArgumentException("Account Number is invalid.");
                }

                string oldEmail = custData.Email;
                
                custData.Email = email;
                if (webPassword != null) {
                    custData.WebPassword = webPassword;
                    custData.IsWebPasswordTemporal = false;
                }
                
				result = new ChangeAccountSettingsResult();
				
				result.EmailAddress = custData.Email;
				result.OldEmailAddress = oldEmail;
				result.FirstName = custData.CustomerInfo.FirstName;

                uow.commit();
            } finally {
                uow.close();
            }

			return result;
        }

        public static EnableWebAccessStatus EnableWebAccess(IMap imap, int accNumber, string accLastName, string email, string webPassword)
        {
			EnableWebAccessStatus result = EnableWebAccessStatus.Success;

            if (imap == null) {
                throw new ArgumentException("imap is required");
            }

            if (accNumber <= 0) {
                throw new ArgumentException("accNumber Required");
            }

            if (accLastName == null || accLastName.Length == 0) {
                throw new ArgumentException("last name is required");
            }

            if (email == null || email.Length == 0) {
                throw new ArgumentException("email required");
            }

            if (webPassword == null || webPassword.Length == 0) {
                throw new ArgumentException("web Password required");
            }

            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.EnableWebAccess");

                CustData custData = CustData.find(uow, accNumber);
                if (custData == null) {
                    result = EnableWebAccessStatus.AccountNumberInvalid;
					return result;
                }

                IAcctInfo accountInfo = Account.GetAccountInfo(uow, accNumber);
                if (accountInfo.LastName == null || (
					accountInfo.LastName.Trim().ToUpper() != accLastName.Trim().ToUpper())) {
					result = EnableWebAccessStatus.LastNameDoesNotMatch;
					return result;
                }

                custData.Email = email;
                custData.WebPassword = webPassword;

                uow.commit();
            } finally {
                uow.close();
            }

			return result;
        }

        public static RemindWebPasswordResult RemindWebPassword(IMap imap, int accNumber)
        {
			RemindWebPasswordResult result = new RemindWebPasswordResult();

            UOW uow = null;

            try {
                uow = new UOW(imap, "CustSvc.GetCustInfo(AccNumber)");
                CustData custData = CustData.find(uow, accNumber);
                if (custData != null) {
                    if (custData.WebPassword != null && custData.WebPassword.Length > 0) {
                        custData.IsWebPasswordTemporal = true;
                        custData.WebPassword = string.Empty;

                        Random rnd = new Random((int)DateTime.Now.Ticks);
                        for (int i = 0; i < 7; i++) {
                            custData.WebPassword += rnd.Next().ToString().Substring(0, 1);
                        }

                        uow.commit();

						result.FirstName = custData.CustomerInfo.FirstName;
						result.WebPassword = custData.WebPassword;
						result.EmailAddress = custData.Email;
						result.Status = WebValidationResultStatus.ValidCustomer;

                    } else {
                        result.Status = WebValidationResultStatus.CustomerNotSetupYet;

                    }
				
					

                }
            } finally {
                uow.close();
            }

          return result;

        }

        #endregion

        #region Payments

        public static void MakePromiseToPay(
            IMap imap, int accountNumber, DateTime payDate, decimal payAmount, string userId)
        {
            if (imap == null) {
                throw new ArgumentNullException("imap");
            }

            if (payDate.Date <= DateTime.Now.Date) {
                throw new ArgumentException("Pay date must be bigger than " 
                                            + DateTime.Now.Date.ToShortDateString() + ".", "payDate");
            }

            if (payAmount < Account.MIN_PTP_PAY_AMOUNT || 
                payAmount > Account.MAX_PTP_PAY_AMOUNT) {
                throw new ArgumentException("Pay amount must be in range between "
                                            + Account.MIN_PTP_PAY_AMOUNT + " and " 
                                            + Account.MAX_PTP_PAY_AMOUNT + ".", "payAmount");
            }

            if (userId == null || userId == string.Empty) {
                throw new ArgumentNullException("userId");
            }

            UOW uow = null;

            try {
                uow = new UOW(imap, "MakePromiseToPay");
                PromiseToPay.MakeIvrRecord(uow, accountNumber, payDate, payAmount, userId);
            } finally {
                uow.close();
            }
        }

        #region Recurring Payment

        public static ICustomerRecurringPayment[] GetCustROPByAccount(IMap imap, int accNumber) {
            UOW uow = null;
            try {
                uow = new UOW(imap);
                return CustomerRecurringPayment.GetCustROPByAccount(uow, accNumber);
            }
            finally {
                uow.close();

            }
        }
        public static ICustomerRecurringPayment GetCustROP(IMap imap) {
            UOW uow = null;
            try {
                uow = new UOW(imap);
                return new CustomerRecurringPayment(uow);
            }
            finally {
                uow.close();

            }
        }

        #endregion

        #endregion

        #region Wireless CustData
        public static IWireless_Custdata[] GetWirelessCustData(IMap imap, string phoneOrEsn) {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetWirelessCustData");

                return Wireless_Custdata.GetByPhoneOrEsn(uow, phoneOrEsn);
            }
            finally {
                uow.close();
                //imap.ClearDomainObjs();
            }
        }
        public static IWireless_Custdata GetWirelessCustData(IMap imap) {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetWirelessCustData");

                return new Wireless_Custdata(uow);
            }
            finally {
                uow.close();
                //imap.ClearDomainObjs();
            }
        }
        public static IWireless_Custdata GetWirelessCustData(IMap imap, int id) {
            UOW uow = null;
            try {
                uow = new UOW(imap, "CustSvc.GetWirelessCustData");

                return Wireless_Custdata.find(uow, id);
            }
            finally {
                uow.close();
                imap.ClearDomainObjs();
            }
        }

        #endregion
    }
}
