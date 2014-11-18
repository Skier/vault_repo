using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.AccountSetup
{
	public class AccountSetupModel
	{
        private const string CONVERSATION_PHONE_NOTE = "Conversion Account Phone Number: {0}. {1} has agreed to the Conversion Verification (LOA). Birthday is {2}";
        private const string TPV_AGREEMENT_BIRTHDAY_NOTE = "{0} has agreed to the Conversion Verification (LOA). Birthday is {1}";

	    private IMap m_map;
	    private IILECInfo[] m_providers;
	    private string m_providersZip;
	    private IProdPrice[] m_currentServices;
	    private Hashtable m_currentServicesTable;
	    private ArrayList m_productComposition;
	    
	    //Key - productId, value (bool) - initial status
	    //contains packages only
	    private Hashtable m_initialPackageStatuses; 	    
	    private Hashtable m_initialSelectedProducts; 	    
	    private bool[] m_visitedGroups;
	    
	    #region Properties

	    #region Info

	    private AccountSetupInfo m_accountSetupInfo;
	    public AccountSetupInfo Info
	    {
	        get { return m_accountSetupInfo; }
	    }

	    #endregion

	    #endregion	    	    

        #region Constructor

	    public AccountSetupModel(IMap map)
	    {
	        m_accountSetupInfo = new AccountSetupInfo();
	        m_map = map;
	        m_providers = new IILECInfo[0];
	    }

	    #endregion

	    #region GetProviders
	    
	    public IILECInfo[] GetProviders(string zip)
	    {
	        //Get cashed providers
	        if (m_providers.Length > 0 && zip == m_providersZip)
	        {
	            return m_providers;
	        } else
	        {
                m_providersZip = zip;
	        
                m_providers = OrgSvc.GetILECsAtZip(m_map, zip);
                if (m_providers == null)
                    m_providers = new IILECInfo[0];	            
	        }
	        
	        return m_providers;	     	        
	    }
	    
	    public string GetProvidersErrorMessage(string zip)
	    {
            if (OrgSvc.DoesZipExist(m_map, zip)) {
                return " We currently do not service your zip code " + zip;
            }
				
            return "Zip " + zip + " is not found";
	    }

	    #endregion
	    	    	    	    
        #region Order Summary

        public virtual void EnsureOrderSummary()
        {
            if (Info.OrderSummary == null) {
                FillSelectedServices();
                Info.OrderSummary = CustSvc.GetOrderSummary(m_map, Info.SelectedServices, Info.Zip, Info.Provider, DemandType.New.ToString(CultureInfo.InvariantCulture), Info.IsMoveExistingPhone ? OrderType.Conv : OrderType.New);
            }
        }

        private void InvalidateOrderSummary() 
        {
            Info.OrderSummary = null;
        }

        #endregion

        #region Customer & Address

        public virtual void EnsureCustomerInfo()
        {
            EnsureOrderSummary();

            if (Info.CustomerInfo == null) {
                Info.CustomerInfo = CustFactory.GetCustInfo(m_map);
                Info.OrderSummary.Demand.Consumer = Info.CustomerInfo;
                Info.CustomerInfo.PrevPhone = Info.PhoneFirst3 + Info.PhoneSecond3 + Info.PhoneLast4;
                Info.CustomerInfo.PrevILEC = Info.Provider.ILECCode;
            }
        }

        public virtual void EnsureServiceAddressInfo() 
        {
            if (Info.ServiceAddress == null) {
                Info.ServiceAddress = CustFactory.GetAddress(m_map);
                ((CustAddress)Info.ServiceAddress).AddrType = AddressType.Service;
                Info.ServiceAddress.Zipcode = Info.Zip;
                Info.ServiceAddress.State = LocSvc.FindState(m_map, Info.Zip);
            }
        }

        public virtual void EnsureMailAddressInfo() 
        {
            if (Info.MailAddress == null) {
                Info.MailAddress = CustFactory.GetAddress(m_map);
                ((CustAddress)Info.MailAddress).AddrType = AddressType.Mailing;
            }
        }

	    #endregion

	    #region ResetServiceList

        public void ResetServiceList() {
            m_currentServices = null;
            Info.CurrentServiceGroup = 0;
            Info.SelectedServices = null;
            m_visitedGroups = null;
        }

        #endregion

	    #region ResetVisitedGroups

	    public void ResetVisitedGroups()
	    {
	        for (int i = 0; i < m_visitedGroups.Length; i++)
	            m_visitedGroups[i] = false;	        
	    }

	    #endregion

	    #region FillSelectedServices

	    public void FillSelectedServices()
	    {
            ArrayList selectedServices = new ArrayList();
            foreach (IProdPrice service in GetServices()) {
                if (service.ProdSelState == ProdSelectionState.Selected)
                    selectedServices.Add(service);
            }
                
            Info.SelectedServices = (IProdPrice[]) selectedServices.ToArray(typeof (IProdPrice));	        
	    }

	    #endregion
	    
        #region Services - Test Implementation
	    	    	    	    
//	    public IProdPrice[] GetServices()
//	    {		        	        	        
//	        if (m_currentServices != null)
//	            return m_currentServices;
//	        
//            Info.Package = new ServicePackageInfo();
//            Info.Package.Package = new TestProdPrice(1, ProdSelectionState.Selected, false, 15, "1233", "sdfs", "2343");
//        
//            string[] features = new string[3]{"Feature 1", "Feature 2", "Feature 3"};
//            Info.Package.Features = features;
//            Info.Package.PackageName = "My Test Name";
//            Info.Package.Price = 30; 
//	        
//	        
//	        ArrayList result = new ArrayList();
//	        for (int i = 0; i <= 3; i++) //Group 1
//	        {
//	            string description =
//	                "President Bush sent a $2.9 trillion spending plan to a Democratic-controlled Congress on Monday, proposing a big increase in military spending to fight the war in Iraq while squeezing other spending to meet his goal of eliminating the deficit in five years.";
//	            
//	            TestProdPrice prodPrice = new TestProdPrice(i,
//	                ProdSelectionState.Available, false, 10, "Product G1 bla bla bla bla bla bla bla bla bla bla bla" + i.ToString(), 
//	                description, "Group 1");
//	            result.Add(prodPrice);
//	        }	        	        
//	        
//	        for (int i = 0; i <= 3; i++) //Group 2
//	        {
//	            TestProdPrice prodPrice = new TestProdPrice(i + 4,
//	                ProdSelectionState.Selected, false, 10, "Product G2 " + i.ToString(), 
//	                "Description " + i.ToString(), "Group 2");
//	            result.Add(prodPrice);
//	        }	        	        
//	        
//	        for (int i = 0; i <= 3; i++) //Group 3
//	        {
//	            TestProdPrice prodPrice = new TestProdPrice(i + 8,
//	                ProdSelectionState.Available, false, 10, "Product G3 " + i.ToString(), 
//	                "Description " + i.ToString(), "Group 3");
//	            result.Add(prodPrice);
//	        }
//	        
//	        m_currentServices = (IProdPrice[]) result.ToArray(typeof (TestProdPrice));
//	        	        
//	        m_visitedGroups = new bool[m_currentServices.Length];
//	        return m_currentServices;
//	    }
//	    
//	    public string[] GetServiceGroupNames()
//	    {
//	        IProdPrice[] products = GetServices();	        
//	        ArrayList groups = new ArrayList();	        
//	        string currentGroupName = string.Empty;	        	        
//	        
//	        foreach (IProdPrice product in products)
//	        {
//	            if (product.ProdType != currentGroupName)
//	            {
//	                currentGroupName = product.ProdType;
//	                groups.Add(currentGroupName);
//	            }
//	        }
//	        
//	        return (string[]) groups.ToArray(typeof (string));
//	    }
//	    
//	    public IProdPrice[] AddService(int serviceId)
//	    {		        
//	        Thread.Sleep(2000);
//	        foreach (IProdPrice prodPrice in m_currentServices)
//	        {
//	            if (prodPrice.ProdId == serviceId)
//	            {
//	                prodPrice.ProdSelState = ProdSelectionState.Selected;	                
//	                break;
//	            }	                
//	        }
//	        
//	        ((TestProdPrice)m_currentServices[0]).ProdSelState = ProdSelectionState.Unavailable;
//	        return m_currentServices;
//	    }
//	    
//	    public IProdPrice[] RemoveService(int serviceId)	    
//	    {	        	       
//	        Thread.Sleep(2000);
//	        foreach (IProdPrice prodPrice in m_currentServices) {
//	            if (prodPrice.ProdId == serviceId) {
//	                prodPrice.ProdSelState = ProdSelectionState.Available;
//	                break;
//	            }
//	                
//	        }
//	        
//	        return m_currentServices;	        
//	    }
//	    
//	    //true if at least 1 item in this group could be changed
//	    public bool IsGroupEnabled(string groupName)
//	    {
//            foreach (IProdPrice product in GetServices()) {
//                if (product.ProdType == groupName 
//                    && !product.Locked 
//                    && product.ProdSelState != ProdSelectionState.Unavailable)
//                {
//                    return true;
//                }
//            }
//            
//	        return false;
//	    }
//	    
//	    public string GetProductGroupName(int productId)
//	    {
//            foreach (IProdPrice product in GetServices()) {
//                if (product.ProdId == productId) {
//                    return product.ProdType;
//                }
//            }
//            
//            return string.Empty;	        
//	    }
//	    
//	    public int GetProductGroupNumber(string groupName)
//	    {
//	        string[] groups = GetServiceGroupNames();
//
//	        for (int i = 0; i < groups.Length; i++)
//	        {
//	            if (groups[i] == groupName)
//	                return i;
//	        }
//	        
//            return 0;	        
//	    }
//	    
//	    public int GetProductGroupNumber(int productId)
//	    {
//	        return GetProductGroupNumber(GetProductGroupName(productId));
//	    }
//	    
//	    public void MarkGroupAsVisited(int group)
//	    {
//	        m_visitedGroups[group] = true;
//	    }
//	    
//	    public bool IsGroupVisited(int group)
//	    {
//	        return m_visitedGroups[group];
//	    }
//	    
//	    public decimal GetQuoteTotal()
//	    {
//	        return 10;
//	    }
//	    
//	    public bool IsProductEnabledOnUI(int prodId)
//	    {
//	        return true;
//	    }
//	    
//	    public bool IsProductLifeLine(IProdPrice prod)
//	    {
//	        return false;
//	    }
//	    
//        public bool IsProductVisibleInList(IProdPrice prod) {
//            return true;
//        }

	    #endregion
	    
        #region Services - Real Implementation
	    
	    public bool IsProductLifeLine (IProdPrice product)
	    {
	        if (product.ProdName.ToLower().IndexOf("life") == -1)
	            return false;
	        else
	            return true;
	    }

        public IProdPrice[] GetServices() {	        
            if (m_currentServices != null)
                return m_currentServices;
            
            
            m_currentServices = ProdSvc.GetDependentProds(m_map, Info.Package.Package,
            	                        Info.Provider,
            	                        Info.Zip,
            	                        "Agent",
            	                        "DPI-Web",
            	                        "");

            m_currentServicesTable = new Hashtable();
            foreach (ProdPrice service in m_currentServices)
            {
                m_currentServicesTable.Add(service.ProdId, service);
            }
            
	        m_visitedGroups = new bool[m_currentServices.Length];
            
            //Init product composition            
            ProdComposition[] prodComposition = ProdSvc.GetProductsComposition(m_map);
            
            //Filter only used products
            m_productComposition = new ArrayList();
            foreach (ProdComposition composition in prodComposition)
            {
                if (m_currentServicesTable.ContainsKey(composition.Prod)
                    || m_currentServicesTable.ContainsKey(composition.SubProd))
                {
                    m_productComposition.Add(composition);
                }                    
            }
	        
            m_initialPackageStatuses = new Hashtable();
            foreach (IProdPrice prod in m_currentServices)
            {                                   
                if (GetSubProducts(prod.ProdId).Length > 0) //Is Package
                {
                    bool isPackageEnabled;
                    
                    if (prod.ProdSelState == ProdSelectionState.Available
                        || prod.ProdSelState == ProdSelectionState.Selected)
                    {
                        isPackageEnabled = true;
                    } else
                    {
                        isPackageEnabled = false;
                    }
                                            
                    m_initialPackageStatuses.Add(prod.ProdId, isPackageEnabled);
                }
            }

            //Fill products witch were initialy selected
            m_initialSelectedProducts = new Hashtable();
            foreach (IProdPrice product in m_currentServices)
            {
                if (product.ProdSelState == ProdSelectionState.Selected)
                    m_initialSelectedProducts.Add(product.ProdId, product.ProdId);
            }
            
            return m_currentServices;
        }
	    
	    private ProdComposition[] GetSubProducts(int productId)
	    {
            ArrayList result = new ArrayList();

	        foreach (ProdComposition composition in m_productComposition)
	        {
	            if (composition.Prod == productId)
	                result.Add(composition);
	        }
	        
	        return (ProdComposition[]) result.ToArray(typeof (ProdComposition));
	    }
	    
	    private ProdComposition[] GetParentProducts(int productId)
	    {
            ArrayList result = new ArrayList();

            foreach (ProdComposition composition in m_productComposition) {
                if (composition.SubProd == productId)
                    result.Add(composition);
            }
	        
            return (ProdComposition[]) result.ToArray(typeof (ProdComposition));	        
	    }
	    
	    private ArrayList GetMutuallyExclusiveProducts(IProdPrice product)
	    {
	        ArrayList result = new ArrayList();
	        foreach (IProdPrice prod in m_currentServices)
	        {
	            
	            if (prod.ProdSubclass == product.ProdSubclass 
	                && prod.ProdType == product.ProdType 	                
	                && IsProductVisibleInList(prod)
	                && product != prod)
	            {
	                result.Add(prod);
	            }	                
	        }
	        
	        return result;
	    }
	    
	    public bool IsProductVisibleInList(IProdPrice product)
	    {
	        if ((product.UnitPrice != decimal.Zero)
	            || (product.UnitPrice == decimal.Zero 
	                && product.ProdSelState == ProdSelectionState.Selected)
                || (product.UnitPrice == decimal.Zero 
                    && m_initialSelectedProducts.ContainsKey(product.ProdId)))
	            return true;
	        
	        
	        return false;	        
	    }
	    
	    public bool IsProductEnabledOnUI(int productId)
	    {
	        if (!m_currentServicesTable.ContainsKey(productId))
	            return false;
	        
	        IProdPrice product = (IProdPrice) m_currentServicesTable[productId];
	        
	        if (product.ProdType == "Local Service")
	            return false;
	        
	        if (product.ProdSelState != ProdSelectionState.Unavailable && !product.Locked)
	            return true;
	        
	        //Check Mutually Exclusive
	        ArrayList analogs = GetMutuallyExclusiveProducts(product);
	        foreach (IProdPrice prod in analogs)
	        {
	            if (prod.ProdSelState == ProdSelectionState.Selected)
	                return true;
	        }
	        
	        //Check sub products
	        ProdComposition[] subProds = GetSubProducts(productId);
	        if (subProds.Length > 0 && ((bool)m_initialPackageStatuses[productId]) == false)
	            return false;
	            
	        
	        foreach (ProdComposition sub in subProds)
	        {
	            if (m_currentServicesTable.ContainsKey(sub.SubProd)	                
	                && ((IProdPrice)m_currentServicesTable[sub.SubProd]).ProdSelState == ProdSelectionState.Selected)
	                return true;
	        }
	        	        	        
	        return false;	        
	    }
	    
        public string[] GetServiceGroupNames() {
            IProdPrice[] products = GetServices();	        
            ArrayList groups = new ArrayList();	
            string currentGroupName = string.Empty;
	        
            foreach (IProdPrice product in products) {
                if (product.ProdType != currentGroupName) {
                    currentGroupName = product.ProdType;
                    groups.Add(currentGroupName);
                }
            }
	        
            return (string[]) groups.ToArray(typeof (string));
        }
	    
        public IProdPrice[] AddService(int serviceId) {                        
            IProdPrice prodToAdd = null;
            
            foreach (IProdPrice prod in m_currentServices)
            {
                if (prod.ProdId == serviceId)
                {
                    prodToAdd = prod;
                    break;
                }
            }
            
            if (prodToAdd != null) {
                //First Need to remove all Mutually Exclusive products
                foreach (IProdPrice prod in GetMutuallyExclusiveProducts(prodToAdd))
                {
                    if (prod.ProdSelState == ProdSelectionState.Selected)
                        RemoveService(prod.ProdId);
                }
                
                //Then remove all subproducts if it is package                
                foreach (ProdComposition composition in GetSubProducts(prodToAdd.ProdId))
                {   
                    if (!m_currentServicesTable.ContainsKey(composition.SubProd))
                        continue;
                    
                    IProdPrice childProduct = (IProdPrice) m_currentServicesTable[composition.SubProd];
                    
                    if (childProduct.ProdSelState == ProdSelectionState.Selected)
                        RemoveService(childProduct.ProdId);
                }
                                
                m_currentServices = ProdSvc.AddProd(m_map, m_currentServices, prodToAdd); 
                InvalidateOrderSummary();
            }
                	       	        	        	        
            return m_currentServices;
        }
	    
        public IProdPrice[] RemoveService(int serviceId) {
            IProdPrice prodToRemove = null;
            
            foreach (IProdPrice prod in m_currentServices) {
                if (prod.ProdId == serviceId) {
                    prodToRemove = prod;
                    break;
                }
            }
            
            if (prodToRemove != null) {
                m_currentServices = ProdSvc.RemoveProd(m_map, m_currentServices, prodToRemove); 
                InvalidateOrderSummary();
            }
                	       	        	        	        
            return m_currentServices;
        }	    	    

        //true if at least 1 item in this group could be changed
        public bool IsGroupEnabled(string groupName) {
            foreach (IProdPrice product in GetServices()) {
                if (product.ProdType == groupName 
                    && IsProductVisibleInList(product)
                    && IsProductEnabledOnUI(product.ProdId)) {
                    return true;
                }
            }
            
            return false;
        }
	    
        public string GetProductGroupName(int productId) {
            foreach (IProdPrice product in GetServices()) {
                if (product.ProdId == productId) {
                    return product.ProdType;
                }
            }
            
            return string.Empty;	        
        }
	    
        public int GetProductGroupNumber(string groupName) {
            string[] groups = GetServiceGroupNames();

            for (int i = 0; i < groups.Length; i++) {
                if (groups[i] == groupName)
                    return i;
            }
	        
            return 0;	        
        }
	    
        public int GetProductGroupNumber(int productId) {
            return GetProductGroupNumber(GetProductGroupName(productId));
        }
	    
        public void MarkGroupAsVisited(int group) {
            m_visitedGroups[group] = true;
        }
	    
        public bool IsGroupVisited(int group) {
            return m_visitedGroups[group];
        }
	    
	    public decimal GetQuoteTotal()
	    {
            decimal quoteTotal = decimal.Zero;
	                    	        	        
            foreach (IProdPrice prod in GetServices()) {	                            
                if (prod.ProdType != "Local Service" 
                    && prod.ProdSelState == ProdSelectionState.Selected
                    && prod.StartServMon <= 1) 
                {
                    quoteTotal += prod.UnitPrice;
                }                
            }
	        
	        return quoteTotal;	        
	    }

	    	    
        #endregion	    	    

	    #region CreateAccount

	    public void CreateAccount()
	    {
	        //TODO: Need to make sure which user to use
	        IUser user = new User();
	        user.ClerkId = "Web-User";
	        user.LoginStoreCode = "DPI-Web";
	        user.Role = "Agent";
	        	        
	        IAcctNotes notes = null;  
	        if (!Info.IsMoveExistingPhoneNull && Info.IsMoveExistingPhone)
	            notes = CustSvc.GetNotes(m_map, user.ClerkId, string.Format(CONVERSATION_PHONE_NOTE, 
	                    Info.PhoneFirst3 + Info.PhoneSecond3 + Info.PhoneLast4, 
	                    Info.CustomerInfo.FirstName + " " + Info.CustomerInfo.LastName,
	                    Info.TpvBirthday.ToString("MM/dd/yyyy")));

            int transNum = new Random().Next(1, 1000000);
	        
	        IDemand demand = Info.OrderSummary.Demand;	        	        
	        demand.Status = DemandStatus.PendConf.ToString();
	        demand.ConsumerAgent = user.ClerkId;
	        demand.StoreCode = user.LoginStoreCode;
	        demand.IsUnderWF = true;
	        demand.WFStep = "Final Step";
	        demand.Workflow = "New Account from Web";
	        	    
	        //IPayInfoLocal
	        IPayInfoLocal payInfo = (IPayInfoLocal)PaySvc.GetNewPayInfo(m_map, demand, PayInfoClass.PayInfoLocal);	        
	        payInfo.PaymentType = Info.PaymentType;
	        payInfo.SetAmts(Info.PaymentAmount, Info.OrderSummary.GetTotalAmtDue(1), 
	                        0, Info.PaymentAmount);	        	        
	        payInfo.Status = PaymentStatus.Incomplete.ToString();
	        
	        PaymentResult paymentResult;
	        
	        IReceipt receipt = CustSvc.SubmitNewXactWithPayment(
	            m_map,
	            demand,
	            Info.Provider.OrgId,
	            user.LoginStoreCode,
	            user,
	            transNum.ToString(),
	            payInfo,
	            null,	        
	            notes,
	            Info.PaymentType,
	            Info.CreditCard,
	            Info.BankCheck,
	            Info.PaymentAmount,
	            Info.ServiceAddress,
	            Info.MailAddress,
	            Info.CustomerInfo,
	            out paymentResult);
	        Info.PaymentResult = paymentResult;
	        	        	        
	        INewOrderDTO order = CustSvc.GetNewOrderDTO();
	        if (!Info.IsMoveExistingPhoneNull && Info.IsMoveExistingPhone)
	            order.OrderType = OrderType.Conv;
	        else
	            order.OrderType = OrderType.New;
	        
	        order.MailAddr = Info.MailAddress;
	        order.SvcAddr = Info.ServiceAddress;
	        order.Cust = Info.CustomerInfo;
	        order.Dmd = demand;
	        order.Zipcode = Info.Zip;
	        order.ILEC = Info.Provider.ILECCode;
	        order.TransNum = transNum.ToString();	        
	        order.PayInfo = payInfo;
	        order.Receipt = receipt;
	        	                    
	        if (notes == null)
	            Info.CreatedAccount = CustSvc.SubmitNewOrder2(m_map, null, order);
	        else
	            Info.CreatedAccount = CustSvc.SubmitNewOrder2(m_map, null, order, notes);	            
	        
	        CustSvc.ChangeAccountSettings(m_map, Info.CreatedAccount.AccNumber, Info.CustomerInfo.Email,
	                                      Info.WebAccessPassword);

            SaveVerbatumAddress();
	        	        	        
	        if (Info.SetupRecurringPayments) {
	            SetupRecurringPayments();
	        }

            if (Info.IsTpvAgreement) {
                InsertAccountTpvNotes(user);
            }

            InsertCustomerWebLogEntry();
	        SendCreateAccountNotification();
        }

        private void SaveVerbatumAddress()
        {
            UOW uow = null;
            try {
                uow = new UOW(m_map);

                CustData custData = CustData.find(uow, Info.CreatedAccount.AccNumber);
                Debug.Assert(custData != null);

                custData.VerbatumServiceAddress = Info.VerbatumServiceAddress;
                custData.VerbatumMailingAddress = Info.VerbatumMailingAddress;
                
                uow.commit();
            } finally {
                uow.close();
            }
        }

	    private void InsertAccountTpvNotes(IUser user)
	    {
            UOW uow = null;
            try {
                uow = new UOW(m_map);

                new Notes(uow, user.ClerkId, string.Format(TPV_AGREEMENT_BIRTHDAY_NOTE, Info.CustomerInfo.FirstName + " " + Info.CustomerInfo.LastName, Info.TpvBirthday.ToString("MM/dd/yyyy")));
                
                uow.commit();
            } finally {
                uow.close();
            }
        }

	    private void SendCreateAccountNotification() 
        {
            string accountNumber;
            string payorName;
            string streetAddress;
            string cityStateZip;
            string email;
            string paymentType;
            string paymentCaption;
            string paymentMeanNumber;
            string paymentAmount;
            string paymentDate;
            string confirmationNumber;

            try {
                accountNumber = Info.CreatedAccount.AccNumber.ToString();

                if (Info.PaymentType == PaymentType.Credit) {
                    payorName = NameFormatter.Format(Info.CreditCard);
                    streetAddress = Info.CreditCard.StreetAddress;
                    cityStateZip = (Info.CreditCard.City + " " + Info.CreditCard.State + " " + Info.CreditCard.Zip).ToUpper();
                    email = Info.CreditCard.Email;
                    paymentType = "Credit Card - " + MapCardTypeToString(Info.CreditCard.Type);
                    paymentMeanNumber = "*************" + Info.CreditCard.Number.Substring(Info.CreditCard.Number.Length - 4, 4);
                    paymentCaption = "Credit Card Number";
                } else if (Info.PaymentType == PaymentType.Check) {
                    payorName = NameFormatter.Format(Info.BankCheck);
                    streetAddress = Info.BankCheck.StreetAddress;
                    cityStateZip = (Info.BankCheck.City + " " + Info.BankCheck.State + " " + Info.BankCheck.Zip).ToUpper();
                    email = Info.BankCheck.Email;
                    paymentType = "Check";

                    if (Info.BankCheck.BankAccountNumber.Length > 4) {
                        paymentMeanNumber = "*************" + Info.BankCheck.BankAccountNumber.Substring(Info.BankCheck.BankAccountNumber.Length - 4, 4);
                    } else {
                        paymentMeanNumber = Info.BankCheck.BankAccountNumber;
                    }

                    paymentCaption = "Bank Account Number ";
                } else {
                    throw new ApplicationException("Payment type is unknown: " + Info.PaymentType + ".");
                }

                paymentAmount = Info.PaymentAmount.ToString("C");
                paymentDate = Info.PaymentResult.Payment.PaymentDate.ToString("MM/dd/yyyy hh:mm");
                confirmationNumber = Info.CreatedAccount.ConfNum.ToString();

                ArrayList products = GetProductsForReceipt();

                string productInstruction = GetProductInstruction();

                if (Info.CustomerInfo.Email.ToUpper() == email.ToUpper()) {
                    EmailSender.SendCreateAccountNotification(
                        Info.CustomerInfo.Email,
                        NameFormatter.Format(Info.CustomerInfo),
                        accountNumber,
                        payorName,
                        streetAddress,
                        cityStateZip,
                        email,
                        paymentType,
                        paymentCaption,
                        paymentMeanNumber,
                        paymentAmount,
                        paymentDate,
                        confirmationNumber, 
                        productInstruction, products, IsInternetProductExist());
                } else {
                    EmailSender.SendCreateAccountNotificationShort(
                        Info.CustomerInfo.Email,
                        NameFormatter.Format(Info.CustomerInfo),
                        accountNumber,
                        payorName,
                        streetAddress,
                        cityStateZip,
                        email,
                        paymentType,
                        paymentCaption,
                        paymentMeanNumber,
                        paymentAmount,
                        paymentDate,
                        confirmationNumber, 
                        productInstruction, products, IsInternetProductExist());

                    EmailSender.SendCreateAccountNotificationToPayor(
                        email,
                        NameFormatter.Format(Info.CustomerInfo),
                        accountNumber,
                        payorName,
                        streetAddress,
                        cityStateZip,
                        email,
                        paymentType,
                        paymentCaption,
                        paymentMeanNumber,
                        paymentAmount,
                        paymentDate,
                        confirmationNumber);
                }
            } catch (Exception ex) {
                throw new ApplicationException("Sending new account creation email notification failed.", ex);
            }
        }

        public bool IsInternetProductExist() 
        {
            IProdPrice[] products = Info.OrderSummary.Products;
            foreach (IProdPrice product in products) {
                if (product.ProdType == ProdCategory.Internet.ToString()) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// TODO: move it out of here.
        /// </summary>
        /// <returns></returns>
        private string GetProductInstruction()
        {
            string productInstruction = string.Empty;

            foreach (IProdPrice product in Info.SelectedServices) {
                if (IsThisLongDistance(product)) {
                    productInstruction += @"
                        <p>Long Distance Instructions:<br>
                        To begin making Long Distance calls please dial 1.877.260.2763. A prompt
                        will ask you to enter your PIN number (which is your home phone number;
                        area code first).<br>
                        You will then be prompted to enter the destination number which
                        represents the telephone number you are calling. You will be provided
                        with your remaining available minutes for the current billing cycle. At
                        the beginning of each new billing cycle your minutes will replenish.</p>";
                } else if (IsThisDpiClub(product)) {
                    productInstruction += @"
                        <p>The dPi Club Program includes:<br><br>
                        Involuntary Unemployment Insurance:<br>
                        If you become involuntarily unemployed dPi TeleConnect will waive your
                        monthly payments for up to 3 months subject to the provisions of the
                        program (1-888-600-4436).<br><br>
                        Grocery Coupon Savings Book:<br>
                        Get valuable coupons on the products you buy every day. Use them at any
                        grocery store and save over $500 every year. Select from more than 
                        1,000 brand name items.<br><br>
                        Debt and Credit Counseling Services:<br>
                        If you are currently living paycheck to paycheck or if credit cards
                        bills are weighing you down? Then speak with one of our Credit
                        Counselors and we will be happy to assist you (1-800-285-8546 ID Code:
                        dPI).</p>";
                } else if (IsThisDPiClubGold(product)) {
                    productInstruction += @"
                        <p>The dPi Club Program, Gold Package includes:<br><br>
                        Involuntary Unemployment Insurance:<br>
                        If you become involuntarily unemployed dPi TeleConnect will waive your
                        monthly payments for up to 3 months subject to the provisions of the
                        program (1-888-600-4436).<br><br>
                        Grocery Coupon Savings Book:<br>
                        Get valuable coupons. Use them at any grocery store and save over $500
                        every year.<br><br>
                        Debt and Credit Counseling Services:<br>
                        If you are currently living paycheck to paycheck or if credit cards
                        bills are weighing you down? Then speak with one of our Credit
                        Counselors and we will be happy to assist you (1-800-285-8546 ID Code:
                        dPI).<br><br>
                        Inside Wiring Maintenance:<br>
                        Inside Wiring is located inside the end user's premises or building. It
                        starts at the Point of Demarcation and extends to the telephone, 
                        itself.<br>
                        This is only applicable after service becomes active and the Customer
                        has had the product for 60 days (paid for the product) before it can be
                        used.<br><br>
                        Grace Days:<br>
                        This allows a Customer 5 extra day after their due date each month to
                        make a payment with no risk of being disconnected.</p>";
                } else if (IsGraceDays(product)) {
                    productInstruction += @"<p>Grace Days:<br>This allows a Customer 5 extra day after their due date each month to make a payment with no risk of being disconnected.</p>";
                } else if (IsInsideWireMaintenance(product)) {
                    productInstruction += @"<p>Inside Wiring Maintenance:<br>Inside Wiring is located inside the end user's premises or building. It starts at the Point of Demarcation and extends to the telephone, itself. This is only applicable after service becomes active and the Customer has had the product for 60 days (paid for the product) before it can be used.</p>";
                }
            }

            return productInstruction;
        }

        public ArrayList GetProductsForReceipt()
        {
            IProdPrice[] allProducts = Info.OrderSummary.Products;

            ArrayList products = new ArrayList(allProducts.Length);
            for (int i = 0; i < allProducts.Length; i++) {
                if (allProducts[i].SuppressOnWebReceipt) {
                    continue;
                }

                // TODO: remove after I4 finish
//                if (allProducts[i].StartServMon != 1) {
//                    continue;
//                }

                if (allProducts[i].UnitPrice == 0m) {
                    if (allProducts[i].SuppressZeroPriceProd) {
                        continue;
                    }
                }

                // TODO: remove after I4 finish
//                if (allProducts[i].BillText.ToLower().IndexOf("fee") != -1) {
//                    continue;
//                }

                if (allProducts[i].ProdType.ToLower() == "fee" && allProducts[i].StartServMon == 2) {
                    continue;
                }

                if (allProducts[i].PackageId == 0) {
                    products.Add(new DictionaryEntry(GetBillText(allProducts[i]), GetUnitPriceForReceipt(allProducts[i]).ToString("C")));
                }
            }

            return products;
        }

        public static string GetBillText(IProdPrice product)
        {
            if (product == null) {
                throw new ArgumentNullException("product");
            }

            string billText;
            
            if (product.StartServMon != 1) {
                int numOfFreeMonth = product.StartServMon - 1;
                if (numOfFreeMonth > 1) {
                    if (product.BillText.IndexOf(string.Format("({0} months free)", numOfFreeMonth)) != -1) {
                        billText = string.Format("({0} months free) ", numOfFreeMonth) + product.BillText.Substring(0, product.BillText.IndexOf(string.Format("({0} months free)", numOfFreeMonth)));
                    } else {
                        billText = string.Format("({0} months free) ", numOfFreeMonth) + product.BillText;
                    }
                } else {
                    billText = "(1st month free) " + product.BillText;
                }

                billText += "<br>&nbsp;&nbsp;(" + product.UnitPrice.ToString("C") + " Monthly)";
            } else {
                billText = product.BillText;
            }

            return billText;
        }

        public static decimal GetUnitPriceForReceipt(IProdPrice product)
        {
            if (product == null) {
                throw new ArgumentNullException("product");
            }

            if (product.StartServMon != 1) {
                return 0.0m;
            }

            return product.UnitPrice;
        }

        private bool IsThisLongDistance(IProdPrice product)
        {
            return product.BillText.ToLower().IndexOf("long distance") != -1;
        }

        private bool IsThisDpiClub(IProdPrice product)
        {
            return product.BillText.ToLower().IndexOf("dpi club") != -1 && product.BillText.ToLower().IndexOf("gold package") == -1;
        }

        private bool IsThisDPiClubGold(IProdPrice product)
        {
            return product.BillText.ToLower().IndexOf("dpi club") != -1 && product.BillText.ToLower().IndexOf("gold package") != -1;
        }

        private bool IsInsideWireMaintenance(IProdPrice product)
        {
            return product.BillText.ToLower().IndexOf("inside wire maintenance") != -1;
        }

        private bool IsGraceDays(IProdPrice product)
        {
            return product.BillText.ToLower().IndexOf("grace days") != -1;
        }

        private static string MapCardTypeToString(CreditCardType type) 
        {
            switch (type) {
                case CreditCardType.VISA:
                    return "VISA";
                case CreditCardType.MasterCard:
                    return "MASTER CARD";
                case CreditCardType.AmericanExpress:
                    return "AMERICAN EXPRESS";
                case CreditCardType.DiscoverCard:
                    return "DISCOVER";
                default:
                    throw new ApplicationException("Credit Card type is unknown: " + type + ".");
            }
        }

        private void SetupRecurringPayments()
        {
            try {
                ICustomerRecurringPayment payment = CustSvc.GetCustROP(m_map);

                payment.Active = true;
                payment.DateInserted = DateTime.Now;
                payment.UserId = "DPI Central";
                payment.AccNumber = Info.CreatedAccount.AccNumber;
                payment.AccountTypeId = (int) Info.PaymentType;
                payment.DateModified = DateTime.Now;
                payment.Priority = 1;

                if (Info.PaymentType == PaymentType.Credit) {
                    payment.BillingFirstName = Info.CreditCard.FirstName;
                    payment.BillingLastName = Info.CreditCard.LastName;
                    payment.BillingAddress = Info.CreditCard.StreetAddress;
                    payment.BillingCity = Info.CreditCard.City;
                    payment.BillingState = Info.CreditCard.State;
                    payment.BillingZip = Info.CreditCard.Zip;
                    payment.PhNumber = Info.CreditCard.PhoneNumber;
                    payment.EmailAddress = Info.CreditCard.Email;

                    payment.BAccNumber = Info.CreditCard.Number;
                    payment.ExpirationMonthYear = Info.CreditCard.ExpMonth.ToString() + Info.CreditCard.ExpYear.ToString();
                    payment.CVV2 = Info.CreditCard.CvNumber;
                } else {
                    payment.BillingFirstName = Info.BankCheck.FirstName;
                    payment.BillingLastName = Info.BankCheck.LastName;
                    payment.BillingAddress = Info.BankCheck.StreetAddress;
                    payment.BillingCity = Info.BankCheck.City;
                    payment.BillingState = Info.BankCheck.State;
                    payment.BillingZip = Info.BankCheck.Zip;
                    payment.PhNumber = Info.BankCheck.PhoneNumber;
                    payment.EmailAddress = Info.BankCheck.Email;

                    payment.BAccNumber = Info.BankCheck.BankAccountNumber;
                    payment.BRouteNumber = Info.BankCheck.BankRoutingNumber;
                    payment.DLStateNumber = Info.BankCheck.DriverLicenseState + Info.BankCheck.DriverLicenseNumber;
                }

                CustSvc.PreSave(m_map);
            } catch (Exception ex) {
                throw new ApplicationException("Setup recurring payments failed.", ex);
            }
        }

        private void InsertCustomerWebLogEntry()
        {
            UOW uow = new UOW(m_map, "AccountSetupModel.CreateAccount");

            try {
                CustomerWebLog webLog = new CustomerWebLog(uow);

                uow.BeginTransaction();
                webLog.AcctNumber = Info.CreatedAccount.AccNumber;
                uow.commit();
            } finally {
                uow.close();
            }
        }

	    #endregion
	}

    #region TestProdPrice

    public class TestProdPrice : IProdPrice
    {
        private ProdSelectionState prodSelState;
        private bool suppressOnWebReceipt = false;
        private bool displayUnclickMessage = true;
        private bool locked;
        private bool isPreselectedWebOrderL2 = true;
        private bool suppressZeroPriceProd = false;
        private string ordSumryStartMon2 = string.Empty;
        private string taxCode = string.Empty;
        private string priceRule = string.Empty;
        private decimal unitPrice;
        private int endServMon = 12;
        private int startServMon = 1;
        private string billText;
        private string prodName = string.Empty;
        private string description;
        private string prodSubclass = string.Empty;
        private string prodType;
        private int packageId;
        private int prodId;

        public TestProdPrice(int prodId, ProdSelectionState prodSelState, bool locked, decimal unitPrice, string billText, string description, string prodType)
        {
            this.prodSelState = prodSelState;
            this.locked = locked;
            this.unitPrice = unitPrice;
            this.billText = billText;
            this.description = description;
            this.prodType = prodType;
            this.prodId = prodId;
        }

        public int ProdId
        {
            get { return prodId; }
        }

        public int PackageId
        {
            get { return packageId; }
            set { packageId = value; }
        }

        public string ProdType
        {
            get { return prodType; }
        }

        public string ProdSubclass
        {
            get { return prodSubclass; }
        }

        public string Description
        {
            get { return description; }
        }

        public string ProdName
        {
            get { return prodName; }
        }

        public string BillText
        {
            get { return billText; }
            set {billText = value;}
        }

        public int StartServMon
        {
            get { return startServMon; }
        }

        public int EndServMon
        {
            get { return endServMon; }
        }

        public decimal UnitPrice
        {
            get { return unitPrice; }
        }

        public string PriceRule
        {
            get { return priceRule; }
        }

        public string TaxCode
        {
            get { return taxCode; }
        }

        public string OrdSumryStartMon2
        {
            get { return ordSumryStartMon2; }
        }

        public bool SuppressZeroPriceProd
        {
            get { return suppressZeroPriceProd; }
        }

        public bool IsPreselectedWebOrderL2
        {
            get { return isPreselectedWebOrderL2; }
        }

        public bool Locked
        {
            get { return locked; }
        }

        public bool DisplayUnclickMessage
        {
            get { return displayUnclickMessage; }
        }

        public bool SuppressOnWebReceipt
        {
            get { return suppressOnWebReceipt; }
        }

        public ProdSelectionState ProdSelState
        {
            get { return prodSelState; }
            set { prodSelState = value; }
        }
    }

    #endregion

}
