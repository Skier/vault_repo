using System;
using System.Collections;
using System.Globalization;
using System.Text;
using DPI.ClientComp;
using DPI.Components;
using DPI.Interfaces;
using DPI.Services;

namespace Dpi.Central.Web.Account.Wireless.Processes.Rsp
{
    public class RechargeServicePlanModel
    {
        private class ProductComparer : IComparer 
        {
            public int Compare(object x, object y) 
            {
                return ((IWireless_Products) x).Price.CompareTo(((IWireless_Products) y).Price);
            }
        }

        private IMap m_imap;
        private int m_accountNumber;
        private IWireless_Custdata m_customerData;
        private IWirelessDeviceData m_deviceData;
        
        private IWireless_Products[] cachedOptionalProducts;
        private string cachedOptionalProductsPlanName;

        private IWireless_Products[] m_rechargedProducts;
        private IWireless_Products m_finalProduct;
        private IWirelessOrderSum m_orderSummary;
        private PaymentType m_paymentType;
        private CreditCard m_creditCard;
        private BankCheck m_bankCheck;
        private PaymentResult m_paymentResult;
        private ICellPhoneReceipt m_receipt;
        private readonly bool m_isRecharge;
        private readonly bool m_isInternational;
        private readonly IUser m_user;

        public RechargeServicePlanModel(IMap imap, int accountNumber)
        {
            m_imap = imap;

            m_accountNumber = accountNumber;
            m_isRecharge = true;
            m_isInternational = false;

            m_user = new User();
            m_user.ClerkId = "Web-User";
            m_user.LoginStoreCode = "DPI-Web";
            m_user.Role = "Agent";
        }

        public IWireless_Products[] GetMainProducts()
        {
            return DpiWirelessSvc.GetDpiWLMainProds(m_imap, DeviceData.Provider, m_isRecharge, m_isInternational);
        }

        public IWireless_Products[] GetOptionalProducts(string planName)
        {
            if (cachedOptionalProducts != null) {
                if (planName == cachedOptionalProductsPlanName) {
                    return cachedOptionalProducts;
                }
            }

            IWireless_Products[] products = DpiWirelessSvc.GetDpiWLOptionalProds(m_imap, DeviceData.Provider);

            ArrayList result = new ArrayList();
            foreach (IWireless_Products product in products) {
                if (product.Vendor_Name == planName) {
                    result.Add(product);
                }
            }

            cachedOptionalProducts = (IWireless_Products[]) result.ToArray(typeof (IWireless_Products));
            cachedOptionalProductsPlanName = planName;

            return cachedOptionalProducts;
        }

        public decimal GetSelectedOptionalProductsPrice()
        {
            decimal price = decimal.Zero;

            foreach (IWireless_Products product in m_selectedOptionalProducts) {
                price += product.Price;
            }

            return price;
        }

        private IWireless_Products m_selectedMainProduct;

        public IWireless_Products SelectedMainProduct
        {
            get { return m_selectedMainProduct; }
            set { m_selectedMainProduct = value; }
        }

        private ArrayList m_selectedOptionalProducts;

        public ArrayList SelectedOptionalProducts
        {
            get { return m_selectedOptionalProducts; }
            set { m_selectedOptionalProducts = value; }
        }

        public Hashtable GetUserPlans()
        {
            IWireless_Products[] products = GetMainProducts();

            Hashtable result = new Hashtable();
            if (products != null) {
                foreach (IWireless_Products product in products) {
                    if (!result.ContainsKey(product.Vendor_Name)) {
                        result.Add(product.Vendor_Name, new ArrayList());
                    }

                    ((ArrayList) result[product.Vendor_Name]).Add(product);

                }
            }

            foreach (ArrayList array in result.Values) {
                if (array != null) {
                    array.Sort(new ProductComparer());
                }
            }

            return result;
        }

        #region Order Summary

        public bool CheckFinalProductExistence()
        {
            IList selectedProducts = GetSelectedProducts();
            
            string productIdList = GetProductIdList(selectedProducts);

            IWireless_Products[] productList = DpiWirelessSvc.GetPackageByProdList(m_imap, productIdList);

            return productList.Length != 0;
        }

        public IWireless_Products[] LoadCustomerRechargedProducts()
        {
            ISvcPlanDataResp servicePlan = DpiWirelessSvc.GetAvailableBalanceResp(DeviceData.Provider, string.Empty, CustomerData.ESN);
            IWireless_Products[] products =  DpiWirelessSvc.GetProdsBySoc(m_imap, DpiWirelessSvc.GetSoc(servicePlan.ControlNumber), false);

            string productIdList = GetProductIdList(products);
            
            return LoadRechargedProducts(productIdList);
        }

        public IWireless_Products[] LoadSelectedRechargedProducts()
        {          
            IList selectedProducts = GetSelectedProducts();

            string productIdList = GetProductIdList(selectedProducts);

            return LoadRechargedProducts(productIdList);
        }

        public IWirelessOrderSum CreateOrderSummary(string workflow) 
        {
            if (m_rechargedProducts == null) {
                throw new ApplicationException("Recharged product is not loaded.");
            }

            m_orderSummary = CustSvc.GetWLOrderSummary(m_imap, m_rechargedProducts, m_user.LoginStoreCode, DemandType.DpiWireless.ToString());

            IDemand demand = m_orderSummary.Demand;
			
            demand.StoreCode = m_user.LoginStoreCode;
            demand.ConsumerAgent = m_user.ClerkId;
            demand.Status = DemandStatus.Submited.ToString();
            demand.IsUnderWF = true;
            demand.WFStep = "Order Summary";
            demand.Workflow = workflow;

            return m_orderSummary;
        }

        private IList GetSelectedProducts()
        {
            if (SelectedMainProduct == null) {
                throw new ApplicationException("Main product must be selected.");
            }

            ArrayList products = new ArrayList();

            if (SelectedOptionalProducts != null) {
                products.AddRange(SelectedOptionalProducts);
            }

            products.Add(SelectedMainProduct);

            return products;
        }

        private string GetProductIdList(IList products) 
        {
            StringBuilder sb = new StringBuilder();

            foreach (IWireless_Products product in products) {
                if (sb.Length != 0) {
                    sb.Append(",");   
                }

                sb.Append(product.Wireless_product_id.ToString(CultureInfo.InvariantCulture));
            }

            return sb.ToString();
        }

        private IWireless_Products[] LoadRechargedProducts(string productIdList) 
        {
            IWireless_Products[] productList = DpiWirelessSvc.GetPackageByProdList(m_imap, productIdList);
            if (productList.Length == 0) {
                throw new ApplicationException("The products: " + productIdList + " can not be combined together.");
            }

            ClearOrderSummary();

            m_finalProduct = productList[0];

            m_rechargedProducts = DpiWirelessSvc.GetProdsBySoc(m_imap, m_finalProduct.Soc, false);

            return m_rechargedProducts;
        }

        private void ClearOrderSummary() 
        {
            m_rechargedProducts = null;
            m_finalProduct = null;
            m_orderSummary = null;
        }

        #endregion

        #region Payments

        public bool IsPinAvailable()
        {
            UOW uow = null;

            try {
                uow = new UOW(m_imap, "RechargeServicePlanModel.IsPinAvailable");

                IList selectedProducts = GetSelectedProducts();

                string productIdList = GetProductIdList(selectedProducts);

                IWireless_Products[] productList = DpiWirelessSvc.GetPackageByProdList(m_imap, productIdList);
                if (productList.Length == 0) {
                    throw new ApplicationException("The products: " + productIdList + " can not be combined together.");
                }

                IWireless_Products finalProduct = productList[0];

                return AOL_PINs.IsPinAvailable(uow, finalProduct.Wireless_product_id);
            } finally {
                uow.close();
            }
        }

        public decimal GetPaymentAmount()
        {
            

            return m_orderSummary.TotalAmtDue;
        }

        public decimal GetTaxAmount()
        {
            if (m_orderSummary == null) {
                throw new ApplicationException("Order summary is not created.");
            }

            return m_orderSummary.TaxAmt;
        }

        // TODO: SR replace with Replanish
        public PaymentResult MakePayment(CreditCard creditCard)
        {
            return MakePayment(creditCard, m_orderSummary.TotalAmtDue);
        }

        public PaymentResult MakePayment(CreditCard creditCard, decimal paymentAmount)
        {
            if (creditCard == null) {
                throw new ArgumentNullException("creditCard");
            }

            if (m_finalProduct == null) {
                throw new ApplicationException("Composite product is not loaded.");
            }

            if (m_orderSummary == null) {
                throw new ApplicationException("Order summary is not created.");
            }

            m_paymentType = PaymentType.Credit;
            m_creditCard = creditCard;

            m_paymentResult = PaymentSvc.MakeWirelessCreditCardPayment(m_imap, m_user, m_accountNumber, m_finalProduct, m_orderSummary, m_creditCard, paymentAmount, out m_receipt);

            return m_paymentResult;
        }

        // TODO: SR replace with Replanish
        public PaymentResult MakePayment(BankCheck bankCheck) 
        {
            return MakePayment(bankCheck, m_orderSummary.TotalAmtDue);
        }

        public PaymentResult MakePayment(BankCheck bankCheck, decimal paymentAmount) 
        {
            if (bankCheck == null) {
                throw new ArgumentNullException("bankCheck");
            }

            if (m_finalProduct == null) {
                throw new ApplicationException("Composite product is not loaded.");
            }

            if (m_orderSummary == null) {
                throw new ApplicationException("Order summary is not created.");
            }

            m_paymentType = PaymentType.Check;
            m_bankCheck = bankCheck;

            m_paymentResult = PaymentSvc.MakeWirelessCheckPayment(m_imap, m_user, m_accountNumber, m_finalProduct, m_orderSummary, m_bankCheck, paymentAmount, out m_receipt);

            return m_paymentResult;
        }
        
        #endregion

        #region Properties

        public IWireless_Custdata CustomerData 
        {
            get
            {
                if (m_customerData == null) {
                    m_customerData = CustSvc.GetWirelessCustData(m_imap, m_accountNumber);
                }

                return m_customerData;
            }
        }

        public PaymentType PaymentType
        {
            get { return m_paymentType; }
        }

        public CreditCard CreditCard 
        {
            get { return m_creditCard; }
        }

        public BankCheck BankCheck 
        {
            get { return m_bankCheck; }
        }

        public PaymentResult PaymentResult
        {
            get { return m_paymentResult; }
        }

        public IWirelessOrderSum OrderSummary
        {
            get
            {
                if (m_orderSummary == null) {
                    throw new ApplicationException("Order summary is not created.");
                }

                return m_orderSummary;
            }
        }

        public ICellPhoneReceipt Receipt 
        {
            get
            {
                if (m_receipt == null) {
                    throw new ApplicationException("Receipt is not created.");
                }

                return m_receipt;
            }
        }

        public string Provider
        {
            get { return DeviceData.Provider; }
        }

        private IWirelessDeviceData DeviceData 
        {
            get
            {
                if (m_deviceData == null) {
                    m_deviceData = DpiWirelessSvc.GetWLDeviceDataResp(CustomerData.ESN);
                }

                return m_deviceData;
            }
        }

        #endregion
    }
}