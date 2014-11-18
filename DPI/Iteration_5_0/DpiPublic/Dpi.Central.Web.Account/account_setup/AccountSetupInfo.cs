using System;
using DPI.Components;
using DPI.Interfaces;

namespace Dpi.Central.Web.Account.AccountSetup
{
	public class AccountSetupInfo
	{
        #region Constructor

	    public AccountSetupInfo()
	    {
	        m_isMoveExistingPhoneNull = true;
	        m_isQualifyForLowIncomeNull = true;
	    }

	    #endregion

	    #region IsMoveExistingPhone

	    private bool m_isMoveExistingPhone;
	    public bool IsMoveExistingPhone
	    {
	        get { return m_isMoveExistingPhone; }
	        set { m_isMoveExistingPhone = value; }
	    }

	    #endregion
	    
        #region IsMoveExistingPhoneNull

        private bool m_isMoveExistingPhoneNull;
        public bool IsMoveExistingPhoneNull {
            get { return m_isMoveExistingPhoneNull; }
            set { m_isMoveExistingPhoneNull = value; }
        }

        #endregion

	    #region PhoneNumber

	    private string m_phoneFirst3;	    	   
	    public string PhoneFirst3
	    {
	        get { return m_phoneFirst3; }
	        set { m_phoneFirst3 = value; }
	    }

	    private string m_phoneSecond3;
	    public string PhoneSecond3
	    {
	        get { return m_phoneSecond3; }
	        set { m_phoneSecond3 = value; }
	    }

	    private string m_phoneLast4;
	    public string PhoneLast4
	    {
	        get { return m_phoneLast4; }
	        set { m_phoneLast4 = value; }
	    }

	    #endregion

	    #region IsQualifyForLowIncome

	    private bool m_isQualifyForLowIncome;
	    public bool IsQualifyForLowIncome
	    {
	        get { return m_isQualifyForLowIncome; }
	        set { m_isQualifyForLowIncome = value; }
	    }

	    #endregion
	    
        #region IsQualifyForLowIncomeNull

        private bool m_isQualifyForLowIncomeNull;
        public bool IsQualifyForLowIncomeNull {
            get { return m_isQualifyForLowIncomeNull; }
            set { m_isQualifyForLowIncomeNull = value; }
        }

        #endregion

	    #region Provider

	    private IILECInfo m_provider;
	    public IILECInfo Provider
	    {
	        get { return m_provider; }
	        set { m_provider = value; }
	    }

	    #endregion

	    #region Zip

	    private string m_zip;
	    public string Zip
	    {
	        get { return m_zip; }
	        set { m_zip = value; }
	    }

	    #endregion

	    #region Package

	    private ServicePackageInfo m_package;
	    public ServicePackageInfo Package
	    {
	        get { return m_package; }
	        set { m_package = value; }
	    }

	    #endregion	    

	    #region CurrentServiceGroup

	    private int m_currentServiceGroup;
	    public int CurrentServiceGroup
	    {
	        get { return m_currentServiceGroup; }
	        set { m_currentServiceGroup = value; }
	    }

	    #endregion
	    
        #region SelectedServices

        private IProdPrice[] m_selectedServices;
        public IProdPrice[] SelectedServices {
            get { return m_selectedServices; }
            set { m_selectedServices = value; }
        }

        #endregion

        #region OrderSummary

        IOrderSum m_orderSummary;
        public IOrderSum OrderSummary 
        {
            get { return m_orderSummary; }
            set { m_orderSummary = value; }
        }

        #endregion

        #region CustomerInfo

        private ICustInfo2 m_customerInfo;
        public ICustInfo2 CustomerInfo
        {
            get { return m_customerInfo; }
            set { m_customerInfo = value; }
        }

        #endregion

        #region AddressInfo

        private IAddr2 m_serviceAddress;
        public IAddr2 ServiceAddress
        {
            get { return m_serviceAddress; }
            set { m_serviceAddress = value; }
        }

        private IAddr2 m_mailAddress;
        public IAddr2 MailAddress 
        {
            get { return m_mailAddress; }
            set { m_mailAddress = value; }
        }

        private bool m_isMailAddressTheSame = true;
        public bool IsMailAddressTheSame
        {
            get { return m_isMailAddressTheSame; }
            set { m_isMailAddressTheSame = value; }
        }

	    private string _verbatumServiceAddress;
	    public string VerbatumServiceAddress
	    {
	        get { return _verbatumServiceAddress; }
	        set { _verbatumServiceAddress = value; }
	    }

	    private string _verbatumMailingAddress;
	    public string VerbatumMailingAddress
	    {
	        get { return _verbatumMailingAddress; }
	        set { _verbatumMailingAddress = value; }
	    }

	    #endregion

	    #region Web Access Password

        private string m_webAccessPassword;
        public string WebAccessPassword
        {
            get { return m_webAccessPassword; }
            set { m_webAccessPassword = value; }
        }

	    #endregion

	    #region CreatedAccount

	    private IReceipt m_createdAccount;
	    public IReceipt CreatedAccount
	    {
	        get { return m_createdAccount; }
	        set { m_createdAccount = value; }
	    }

	    #endregion

	    #region PaymentType

	    private PaymentType m_paymentType;
	    public PaymentType PaymentType
	    {
	        get { return m_paymentType; }
	        set { m_paymentType = value; }
	    }

	    #endregion

	    #region CreditCard

	    private CreditCard m_creditCard;
	    public CreditCard CreditCard
	    {
	        get { return m_creditCard; }
	        set { m_creditCard = value; }
	    }

	    #endregion

	    #region BankCheck

	    private BankCheck m_bankCheck;
	    public BankCheck BankCheck
	    {
	        get { return m_bankCheck; }
	        set { m_bankCheck = value; }
	    }

	    #endregion

	    #region PaymentAmount

	    private decimal m_paymentAmount;
	    public decimal PaymentAmount
	    {
	        get { return m_paymentAmount; }
	        set { m_paymentAmount = value; }
	    }

	    #endregion	    

	    #region PaymentResult

	    private PaymentResult m_paymentResult;
	    public PaymentResult PaymentResult
	    {
	        get { return m_paymentResult; }
	        set { m_paymentResult = value; }
	    }

	    #endregion	    

	    #region SetupRecurringPayments

	    private bool _setupRecurringPayments = false;
	    public bool SetupRecurringPayments
	    {
	        get { return _setupRecurringPayments; }
	        set { _setupRecurringPayments = value; }
	    }

	    #endregion

	    #region IsDoNotResetModelOnFirstStep

	    private bool m_isDoNotResetModelOnFirstStep;
	    public bool IsDoNotResetModelOnFirstStep
	    {
	        get { return m_isDoNotResetModelOnFirstStep; }
	        set { m_isDoNotResetModelOnFirstStep = value; }
	    }

	    #endregion

        #region TpvAgreement

        private bool _isTpvAgreement = false;
        public bool IsTpvAgreement
        {
            get { return _isTpvAgreement; }
            set { _isTpvAgreement = value; }
        }

	    #endregion

        #region TpvBirthday

        private DateTime _tpvBirthday = DateTime.MinValue;
        public DateTime TpvBirthday
        {
            get { return _tpvBirthday; }
            set { _tpvBirthday = value; }
        }
        #endregion
	}
}
