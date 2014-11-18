using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;
 
namespace DPI.Components
{
    public struct PromiseToPay
    {
        public decimal Amount;
        public DateTime Date;
        public DateTime CreatedDate;
    }

    [Serializable]
    public class CustData : DomainObj, IId, ICustDataValidation
    {
        /*        Data        */
        static string iName = "CustData";
        int accNumber;
        string confNum;
        string nameLast;
        string nameFirst;
        string ctNumber1;
        string ctNumber2;
        string adrNum;
        string adrNumSfx;
        string adrPfx;
        string adrStreet;
        string adrStreetType;
        string adrSfx;
        string adrUnitDesc;
        string adrUnit;
        string adrElevation;
        string adrFloor;
        string adrStructureDesc;
        string adrStructureNum;
        string adrCity;
        string adrState;
        string adrZip;
        string mail_AdrNum;
        string mail_AdrNumSfx;
        string mail_AdrPfx;
        string mail_AdrStreet;
        string mail_adrStreetType;
        string mail_AdrSfx;
        string mail_AdrUnitDesc;
        string mail_AdrUnit;
        string mail_AdrElevation;
        string mail_AdrFloor;
        string mail_AdrStructureDesc;
        string mail_AdrStructureNum;
        string mail_AdrCity;
        string mail_AdrState;
        string mail_AdrZip;
        string complex;
        string prevIlec;
        string prevPHNum;
        string storeCode;
        DateTime payDate;
        DateTime payTime;
        string priceCode;
        string ilec;
        string phNumber;
        DateTime activDate;
        DateTime sDiscoDate;
        DateTime aDiscoDate;
        string nOrder;
        string dOrder;
        string status1;
        string status3;
        decimal nxtPymnt;
        decimal balance;
        decimal lstPymnt;
        DateTime lstPayDate;
        decimal totalPymnts;
        int grace;
        int reminder;
        int dayCredit;
        int permCredit;
        bool bill_Initial;
        bool bill_One;
        bool bill_Two;
        string taxCode;
        string wU_SwiftPay_ID;
        string language;
        string birthday;
        int service_Month;
        bool uNEP;
        DateTime due_Date;
        byte bill_Cycle;
        string pIC;
        string lPIC;
        string email;
		int sourceCode;
		string webPassword;
        bool isWebPasswordTemporal;
        
        /*        Properties        */
		public int Id {	get { return accNumber; }}
		public ICustInfoExt CustInfoExtended { get {return new CustInfoExt(CustomerInfo, MailingAddr, ServiceAddr); }}
		public ICustInfoExt2 CustInfoExtended2 { get {return new CustInfoExt2(CustomerInfo, MailingAddr, ServiceAddr, this.ActivDate); }}
		public ICustInfo2 CustomerInfo
		{
			get 
			{
				CustInfo ci = new CustInfo();
				
				ci.Contact   = this.ctNumber1;
				ci.Contact2  = this.ctNumber2;
				ci.Email     = this.email;
				ci.FirstName = this.nameFirst;
				ci.LastName  = this.NameLast;
				ci.PhNumber  = this.phNumber;
				
				return ci;
			}
		}
		public IAddr2 ServiceAddr
		{
			get
			{
				CustAddress sa =  new CustAddress();//(AddressType.Service);//  CustFactory.getServAddr();;

				sa.StreetNum    = this.adrNum;
				sa.StreetPrefix = this.adrPfx;
				sa.Street       = this.adrStreet;
				sa.StreetType   = this.adrStreetType;
				sa.StreetSuffix = this.adrSfx;
				sa.Unit         = this.adrUnit;
				sa.City         = this.adrCity;
				sa.Zipcode      = this.adrZip;
				sa.State        = this.adrState;
				
				return sa;
			}
		}

		public IAddr2 MailingAddr
		{
			get
			{
				CustAddress ma = new CustAddress();//  MailAddress(AddressType.Mailing);
	
				ma.StreetNum = this.mail_AdrNum;
				ma.StreetPrefix = this.mail_AdrPfx;
				ma.Street = this.mail_AdrStreet;
				ma.StreetType = this.mail_adrStreetType;
				ma.StreetSuffix = this.mail_AdrSfx;
				ma.Unit = this.mail_AdrUnit;
				ma.City = this.mail_AdrCity;
				ma.Zipcode = this.mail_AdrZip;
				ma.State = this.mail_AdrState;

				return ma;
			}
		}
		public override IDomKey IKey 
		{
			get { return new Key(iName, accNumber.ToString()); }
		}
        public int AccNumber
        {
            get { return accNumber; }
            set
            {
                setState();
                accNumber = value;
            }
        }
        public string ConfNum
        {
            get { return confNum; }
            set
            {
                setState();
                confNum = value;
            }
        }
        public string NameLast
        {
            get { return nameLast; }
            set
            {
                setState();
                nameLast = value;
            }
        }
        public string NameFirst
        {
            get { return nameFirst; }
            set
            {
                setState();
                nameFirst = value;
            }
        }
        public string CtNumber1
        {
            get { return ctNumber1; }
            set
            {
                setState();
                ctNumber1 = value;
            }
        }
        public string CtNumber2
        {
            get { return ctNumber2; }
            set
            {
                setState();
                ctNumber2 = value;
            }
        }
        public string AdrNum
        {
            get { return adrNum; }
            set
            {
                setState();
                adrNum = value;
            }
        }
        public string AdrNumSfx
        {
            get { return adrNumSfx; }
            set
            {
                setState();
                adrNumSfx = value;
            }
        }
        public string AdrPfx
        {
            get { return adrPfx; }
            set
            {
                setState();
                adrPfx = value;
            }
        }
        public string AdrStreet
        {
            get { return adrStreet; }
            set
            {
                setState();
                adrStreet = value;
            }
        }
        public string AdrStreetType
        {
            get { return adrStreetType; }
            set
            {
                setState();
                adrStreetType = value;
            }
        }
        public string AdrSfx
        {
            get { return adrSfx; }
            set
            {
                setState();
                adrSfx = value;
            }
        }
        public string AdrUnitDesc
        {
            get { return adrUnitDesc; }
            set
            {
                setState();
                adrUnitDesc = value;
            }
        }
        public string AdrUnit
        {
            get { return adrUnit; }
            set
            {
                setState();
                adrUnit = value;
            }
        }
        public string AdrElevation
        {
            get { return adrElevation; }
            set
            {
                setState();
                adrElevation = value;
            }
        }
        public string AdrFloor
        {
            get { return adrFloor; }
            set
            {
                setState();
                adrFloor = value;
            }
        }
        public string AdrStructureDesc
        {
            get { return adrStructureDesc; }
            set
            {
                setState();
                adrStructureDesc = value;
            }
        }
        public string AdrStructureNum
        {
            get { return adrStructureNum; }
            set
            {
                setState();
                adrStructureNum = value;
            }
        }
        public string AdrCity
        {
            get { return adrCity; }
            set
            {
                setState();
                adrCity = value;
            }
        }
        public string AdrState
        {
            get { return adrState; }
            set
            {
                setState();
                adrState = value;
            }
        }
        public string AdrZip
        {
            get { return adrZip; }
            set
            {
                setState();
                adrZip = value;
            }
        }
        public string Mail_AdrNum
        {
            get { return mail_AdrNum; }
            set
            {
                setState();
                mail_AdrNum = value;
            }
        }
        public string Mail_AdrNumSfx
        {
            get { return mail_AdrNumSfx; }
            set
            {
                setState();
                mail_AdrNumSfx = value;
            }
        }
        public string Mail_AdrPfx
        {
            get { return mail_AdrPfx; }
            set
            {
                setState();
                mail_AdrPfx = value;
            }
        }
        public string Mail_AdrStreet
        {
            get { return mail_AdrStreet; }
            set
            {
                setState();
                mail_AdrStreet = value;
            }
        }
        public string Mail_adrStreetType
        {
            get { return mail_adrStreetType; }
            set
            {
                setState();
                mail_adrStreetType = value;
            }
        }
        public string Mail_AdrSfx
        {
            get { return mail_AdrSfx; }
            set
            {
                setState();
                mail_AdrSfx = value;
            }
        }
        public string Mail_AdrUnitDesc
        {
            get { return mail_AdrUnitDesc; }
            set
            {
                setState();
                mail_AdrUnitDesc = value;
            }
        }
        public string Mail_AdrUnit
        {
            get { return mail_AdrUnit; }
            set
            {
                setState();
                mail_AdrUnit = value;
            }
        }
        public string Mail_AdrElevation
        {
            get { return mail_AdrElevation; }
            set
            {
                setState();
                mail_AdrElevation = value;
            }
        }
        public string Mail_AdrFloor
        {
            get { return mail_AdrFloor; }
            set
            {
                setState();
                mail_AdrFloor = value;
            }
        }
        public string Mail_AdrStructureDesc
        {
            get { return mail_AdrStructureDesc; }
            set
            {
                setState();
                mail_AdrStructureDesc = value;
            }
        }
        public string Mail_AdrStructureNum
        {
            get { return mail_AdrStructureNum; }
            set
            {
                setState();
                mail_AdrStructureNum = value;
            }
        }
        public string Mail_AdrCity
        {
            get { return mail_AdrCity; }
            set
            {
                setState();
                mail_AdrCity = value;
            }
        }
        public string Mail_AdrState
        {
            get { return mail_AdrState; }
            set
            {
                setState();
                mail_AdrState = value;
            }
        }
        public string Mail_AdrZip
        {
            get { return mail_AdrZip; }
            set
            {
                setState();
                mail_AdrZip = value;
            }
        }
        public string Complex
        {
            get { return complex; }
            set
            {
                setState();
                complex = value;
            }
        }
        public string PrevIlec
        {
            get { return prevIlec; }
            set
            {
                setState();
                prevIlec = value;
            }
        }
        public string PrevPHNum
        {
            get { return prevPHNum; }
            set
            {
                setState();
                prevPHNum = value;
            }
        }
        public string StoreCode
        {
            get { return storeCode; }
            set
            {
                setState();
                storeCode = value;
            }
        }
        public DateTime PayDate
        {
            get { return payDate; }
            set
            {
                setState();
                payDate = value;
            }
        }
        public DateTime PayTime
        {
            get { return payTime; }
            set
            {
                setState();
                payTime = value;
            }
        }
        public string PriceCode
        {
            get { return priceCode; }
            set
            {
                setState();
                priceCode = value;
            }
        }
        public string Ilec
        {
            get { return ilec; }
            set
            {
                setState();
                ilec = value;
            }
        }
        public string PhNumber
        {
            get { return phNumber; }
            set
            {
                setState();
                phNumber = value;
            }
        }
        public DateTime ActivDate
        {
            get { return activDate; }
            set
            {
                setState();
                activDate = value;
            }
        }
        public DateTime SDiscoDate
        {
            get { return sDiscoDate; }
            set
            {
                setState();
                sDiscoDate = value;
            }
        }
        public DateTime ADiscoDate
        {
            get { return aDiscoDate; }
            set
            {
                setState();
                aDiscoDate = value;
            }
        }
        public string NOrder
        {
            get { return nOrder; }
            set
            {
                setState();
                nOrder = value;
            }
        }
        public string DOrder
        {
            get { return dOrder; }
            set
            {
                setState();
                dOrder = value;
            }
        }
        public string Status1
        {
            get { return status1; }
            set
            {
                setState();
                status1 = value;
            }
        }
        public string Status3
        {
            get { return status3; }
            set
            {
                setState();
                status3 = value;
            }
        }
        public decimal NxtPymnt
        {
            get { return nxtPymnt; }
            set
            {
                setState();
                nxtPymnt = Decimal.Round(value, 2);
            }
        }
        public decimal Balance
        {
            get { return balance; }
            set
            {
                setState();
                balance = Decimal.Round(value, 2);
            }
        }
        public decimal LstPymnt
        {
            get { return lstPymnt; }
            set
            {
                setState();
                lstPymnt = Decimal.Round(value, 2);
            }
        }
        public DateTime LstPayDate
        {
            get { return lstPayDate; }
            set
            {
                setState();
                lstPayDate = value;
            }
        }
        public decimal TotalPymnts
        {
            get { return totalPymnts; }
            set
            {
                setState();
                totalPymnts = Decimal.Round(value, 2);
            }
        }
        public int Grace
        {
            get { return grace; }
            set
            {
                setState();
                grace = value;
            }
        }
        public int Reminder
        {
            get { return reminder; }
            set
            {
                setState();
                reminder = value;
            }
        }
        public int DayCredit
        {
            get { return dayCredit; }
            set
            {
                setState();
                dayCredit = value;
            }
        }
        public int PermCredit
        {
            get { return permCredit; }
            set
            {
                setState();
                permCredit = value;
            }
        }
        public bool Bill_Initial
        {
            get { return bill_Initial; }
            set
            {
                setState();
                bill_Initial = value;
            }
        }
        public bool Bill_One
        {
            get { return bill_One; }
            set
            {
                setState();
                bill_One = value;
            }
        }
        public bool Bill_Two
        {
            get { return bill_Two; }
            set
            {
                setState();
                bill_Two = value;
            }
        }
        public string TaxCode
        {
            get { return taxCode; }
            set
            {
                setState();
                taxCode = value;
            }
        }
        public string WU_SwiftPay_ID
        {
            get { return wU_SwiftPay_ID; }
            set
            {
                setState();
                wU_SwiftPay_ID = value;
            }
        }
        public string Language
        {
            get { return language; }
            set
            {
                setState();
                language = value;
            }
        }
        public string Birthday
        {
            get { return birthday; }
            set
            {
                setState();
                birthday = value;
            }
        }
        public int Service_Month
        {
            get { return service_Month; }
            set
            {
                setState();
                service_Month = value;
            }
        }
        public bool UNEP
        {
            get { return uNEP; }
            set
            {
                setState();
                uNEP = value;
            }
        }
        public DateTime Due_Date
        {
            get { return due_Date; }
            set
            {
                setState();
                due_Date = value;
            }
        }
        public byte Bill_Cycle
        {
            get { return bill_Cycle; }
            set
            {
                setState();
                bill_Cycle = value;
            }
        }
        public string PIC
        {
            get { return pIC; }
            set
            {
                setState();
                pIC = value;
            }
        }
        public string LPIC
        {
            get { return lPIC; }
            set
            {
                setState();
                lPIC = value;
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                setState();
                email = value;
            }
        }
		public int SourceCode
		{
			get { return sourceCode; }
			set 
			{ 
				setState();
				sourceCode = value;
			}
		}

		public string WebPassword
		{
			get { return this.webPassword;}
			set 
			{	setState();
				this.webPassword = value;
			}
		}

        public bool IsWebPasswordTemporal
        {
            get { return this.isWebPasswordTemporal; }
            set
            {
                setState();
                this.isWebPasswordTemporal = value;
            }
        }
        
        /*        Constructors			*/
        public CustData()
        {
            sql = new CustDataSQL();
            rowState = RowState.New;
        }
        public CustData(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new CustDataSQL();
        }
        public override void checkExists()
        {
            if ((AccNumber < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
//		public static ICustInfo2 GetActive(UOW uow)
//		{
//
//		}
		public static ICustInfo2 GetDummyCustData()
		{
			CustData cd = new CustData();
			cd.AccNumber = 0;
			cd.nameFirst ="Unknown";
			cd.nameLast = "";
			cd.phNumber = "000-000-0000";
			cd.uow = null;

			return cd.CustomerInfo;

		}
		public static ICustInfo GetBalanceInfo(UOW uow, int accNumber)
		{			
/*			Since ICustBalanceInfo is removed...
			CustData custData = find(uow, accNumber);
			CustBalanceInfo custBalanceInfo = new CustBalanceInfo ();
			custBalanceInfo.AdrCity = custData.AdrCity;
			custBalanceInfo.AdrNum = custData.AdrNum;
			custBalanceInfo.AdrPfx = custData.AdrPfx;
			custBalanceInfo.AdrSfx = custData.AdrSfx;
			custBalanceInfo.AdrState = custData.AdrState;
			custBalanceInfo.AdrStreet = custData.AdrStreet;
			custBalanceInfo.AdrStreetType = custData.AdrStreetType;
			custBalanceInfo.AdrUnit = custData.AdrUnit;
			custBalanceInfo.AdrZip = custData.AdrZip;
			custBalanceInfo.Balance = custData.Balance;
			custBalanceInfo.NameFirst = custData.NameFirst;
			custBalanceInfo.NameLast = custData.NameLast;
			return custBalanceInfo;
	*/
			return null;
		}
        public static CustData find(UOW uow, int accNumber)
        {
            if (uow.Imap.keyExists(CustData.getKey(accNumber)))
                return (CustData)uow.Imap.find(CustData.getKey(accNumber));
            
            CustData cls = new CustData();
            cls.uow = uow;
            cls.accNumber = accNumber;
            cls = (CustData)DomainObj.addToIMap(uow, getOne(((CustDataSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static bool IsEligibleForPromiseToPay(UOW uow, int accNumber)
        {
            CustDataSQL ds = new CustDataSQL();
            return ds.IsEligibleForPromiseToPay(uow, accNumber);
        }
        public static bool DoesPromiseToPayExist(UOW uow, int accNumber, DateTime dueDate)
        {
            CustDataSQL ds = new CustDataSQL();
            return ds.DoesPromiseToPayExist(uow, accNumber, dueDate);
        }
        public static PromiseToPay GetPromiseToPay(UOW uow, int accNumber, DateTime dueDate)
        {
            CustDataSQL ds = new CustDataSQL();
            return ds.GetPromiseToPay(uow, accNumber, dueDate);
        }
        public static DateTime GetPromiseToPayDate(UOW uow, int acctNumber)
        {
            CustDataSQL ds = new CustDataSQL();
            return ds.GetPromiseToPayDate(uow, acctNumber);
        }
        public static void MakeIvrRecord(UOW uow, int accNumber, 
            DateTime payDate, decimal payAmount, string userId) {

            CustDataSQL ds = new CustDataSQL();
            ds.MakeIvrRecord(uow, accNumber, payDate, payAmount, userId);
        }
		public static CustData[] getPhone(UOW uow, string phNumber)
		{
			if (phNumber == null)
				throw new ArgumentException("Phone number is required");
			
			if (phNumber.Trim().Length != 10)
				throw new ArgumentException("Phone number must be 10 digits");
			try
			{
				Int64.Parse(phNumber.Trim());
			}
			catch (Exception)
			{
				throw new ArgumentException("Phone number must be numeric");
			}
			CustData[] objs = (CustData[])DomainObj.addToIMap(uow, (new CustDataSQL()).getPhone(uow, phNumber));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}

        public static CustData[] getAll(UOW uow)
        {
            CustData[] objs = (CustData[])DomainObj.addToIMap(uow, (new CustDataSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int accNumber)
        {
            return new Key(iName, accNumber.ToString());
        }
		public static CustData GetCustByPayInfo(UOW uow, int payInfo)
		{
			return new CustDataSQL().getByPayInfo(uow, payInfo);
		}
		public static int ReserveAccNumber(UOW uow)
		{
			return new CustDataSQL().ReserveAcct(uow);
		}
		public static ICustInfo2[] GetCustDataByAcctNumber(UOW uow, int acctNumber)
		{
			return new CustDataSQL().GetCustDataByAcctNumber(uow, acctNumber);
		}

		public static ICustDataValidation [] ValidateCustDataByAcctNumber(UOW uow, int acctNumber)
		{
			return new CustDataSQL().ValidateCustDataByAcctNumber(uow, acctNumber);
		}

		public static ICustDataValidation [] ValidateCustDataByPhNumber(UOW uow, string phNumber)
		{
			return new CustDataSQL().ValidateCustDataByPhNumber(uow, phNumber);
		}



		/*		Implementation		*/
        static CustData getOne(CustData[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        public static void copyAttrs(CustData src, CustData tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.accNumber = src.accNumber;
            tar.confNum = src.confNum;
            tar.nameLast = src.nameLast;
            tar.nameFirst = src.nameFirst;
            tar.ctNumber1 = src.ctNumber1;
            tar.ctNumber2 = src.ctNumber2;
            tar.adrNum = src.adrNum;
            tar.adrNumSfx = src.adrNumSfx;
            tar.adrPfx = src.adrPfx;
            tar.adrStreet = src.adrStreet;
            tar.adrStreetType = src.adrStreetType;
            tar.adrSfx = src.adrSfx;
            tar.adrUnitDesc = src.adrUnitDesc;
            tar.adrUnit = src.adrUnit;
            tar.adrElevation = src.adrElevation;
            tar.adrFloor = src.adrFloor;
            tar.adrStructureDesc = src.adrStructureDesc;
            tar.adrStructureNum = src.adrStructureNum;
            tar.adrCity = src.adrCity;
            tar.adrState = src.adrState;
            tar.adrZip = src.adrZip;
            tar.mail_AdrNum = src.mail_AdrNum;
            tar.mail_AdrNumSfx = src.mail_AdrNumSfx;
            tar.mail_AdrPfx = src.mail_AdrPfx;
            tar.mail_AdrStreet = src.mail_AdrStreet;
            tar.mail_adrStreetType = src.mail_adrStreetType;
            tar.mail_AdrSfx = src.mail_AdrSfx;
            tar.mail_AdrUnitDesc = src.mail_AdrUnitDesc;
            tar.mail_AdrUnit = src.mail_AdrUnit;
            tar.mail_AdrElevation = src.mail_AdrElevation;
            tar.mail_AdrFloor = src.mail_AdrFloor;
            tar.mail_AdrStructureDesc = src.mail_AdrStructureDesc;
            tar.mail_AdrStructureNum = src.mail_AdrStructureNum;
            tar.mail_AdrCity = src.mail_AdrCity;
            tar.mail_AdrState = src.mail_AdrState;
            tar.mail_AdrZip = src.mail_AdrZip;
            tar.complex = src.complex;
            tar.prevIlec = src.prevIlec;
            tar.prevPHNum = src.prevPHNum;
            tar.storeCode = src.storeCode;
            tar.payDate = src.payDate;
            tar.payTime = src.payTime;
            tar.priceCode = src.priceCode;
            tar.ilec = src.ilec;
            tar.phNumber = src.phNumber;
            tar.activDate = src.activDate;
            tar.sDiscoDate = src.sDiscoDate;
            tar.aDiscoDate = src.aDiscoDate;
            tar.nOrder = src.nOrder;
            tar.dOrder = src.dOrder;
            tar.status1 = src.status1;
            tar.status3 = src.status3;
            tar.nxtPymnt = src.nxtPymnt;
            tar.balance = src.balance;
            tar.lstPymnt = src.lstPymnt;
            tar.lstPayDate = src.lstPayDate;
            tar.totalPymnts = src.totalPymnts;
            tar.grace = src.grace;
            tar.reminder = src.reminder;
            tar.dayCredit = src.dayCredit;
            tar.permCredit = src.permCredit;
            tar.bill_Initial = src.bill_Initial;
            tar.bill_One = src.bill_One;
            tar.bill_Two = src.bill_Two;
            tar.taxCode = src.taxCode;
            tar.wU_SwiftPay_ID = src.wU_SwiftPay_ID;
            tar.language = src.language;
            tar.birthday = src.birthday;
            tar.service_Month = src.service_Month;
            tar.uNEP = src.uNEP;
            tar.due_Date = src.due_Date;
            tar.bill_Cycle = src.bill_Cycle;
            tar.pIC = src.pIC;
            tar.lPIC = src.lPIC;
            tar.email = src.email;
			tar.sourceCode = src.sourceCode;
			tar.webPassword = src.webPassword;
            tar.isWebPasswordTemporal = src.isWebPasswordTemporal;

            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class CustDataSQL : SqlGateway
        {
            public CustData[] getKey(CustData rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustData_Get_Id";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
                return convert(execReader(cmd));
            }
			public ICustInfo2[] GetCustDataByAcctNumber(UOW uow, int acctNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spGetCustDataByAcctNumber";
				cmd.Parameters.Add("@AccNumber", SqlDbType.Char, 10).Value = acctNumber;
				return execReader1(cmd);
			}

			public ICustDataValidation [] ValidateCustDataByAcctNumber(UOW uow, int acctNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustData_Validate_By_AcctNum";
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = acctNumber;
				return execReader2(cmd);
			}

			public ICustDataValidation [] ValidateCustDataByPhNumber(UOW uow, string phNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustData_Validate_By_PhNumber";
				cmd.Parameters.Add("@PhNumber", SqlDbType.Char, 10).Value = phNumber;
				return execReader2(cmd);
			}

            public bool IsEligibleForPromiseToPay(UOW uow, int accNumber) 
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran; 
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [dbo].[fnPtp_IsEligible](@AccNumber)";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                object value = cmd.ExecuteScalar();
                return ((string)value == "T");
            }

            public bool DoesPromiseToPayExist(UOW uow, int accNumber, DateTime dueDate) 
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AccountCheckPTP";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = dueDate;
                object value = cmd.ExecuteScalar();
                return ((int)value > 0);
            }

            public PromiseToPay GetPromiseToPay(UOW uow, int accNumber, DateTime dueDate) 
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_AccountGetPTP";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = dueDate;

                PromiseToPay ptp;
                using (SqlDataReader rdr = cmd.ExecuteReader()) 
                {
                    if (!rdr.Read()) {
                        throw new ApplicationException("The method 'GetPromiseToPay' must return value.");
                    }

                    ptp.Amount = (decimal)rdr["PTP_Amount"];
                    ptp.Date = (DateTime)rdr["PTP_Date"];
                    ptp.CreatedDate = (DateTime)rdr["Date_Created"];
                }
                
                return ptp;
            }

            public DateTime GetPromiseToPayDate(UOW uow, int accNumber)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran; 
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [dbo].[fnPtp_IVRDate](@accnumber, getdate())";
                cmd.Parameters.Add("@accnumber", SqlDbType.Int).Value = accNumber;
                object value = cmd.ExecuteScalar();
                return (DateTime)value;
            }

            public void MakeIvrRecord(UOW uow, int accNumber, 
                DateTime payDate, decimal payAmount, string userId)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran; 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spIVR_AddPTP";
                cmd.Parameters.Add("@accnumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@ptpdate", SqlDbType.DateTime).Value = payDate;
                cmd.Parameters.Add("@ptpamount", SqlDbType.SmallMoney).Value = payAmount;
                cmd.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = userId;
                cmd.ExecuteNonQuery();
            }

//			public ICustInfo2[] GetActive(UOW uow)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.CommandText = "[spCustData_Get_Active";
//				return execReader2(cmd);
//			}

			public CustData[] getPhone(UOW uow, string phNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustData_Get_Phone";
				cmd.Parameters.Add("@PhNumber", SqlDbType.Char, 10).Value = phNumber;
				return convert(execReader(cmd));
			}
			ICustInfo2[] execReader1(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader1(rdr));
			
					ICustInfo2[] cis = new ICustInfo2[ar.Count];
					ar.CopyTo(cis);
					return cis;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}

			ICustDataValidation [] execReader2(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader2(rdr));
			
					ICustDataValidation[] cds = new ICustDataValidation[ar.Count];
					ar.CopyTo(cds);
					return cds;
				}
				catch (Exception e)
				{
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}

			ICustInfo2 reader1(SqlDataReader rdr)
			{
				CustInfo ci = new CustInfo();
			
				if ( rdr["NameLast"] != DBNull.Value)
					ci.LastName = (string) rdr["NameLast"];
							
				if (rdr["NameFirst"] != DBNull.Value)
					ci.FirstName = (string) rdr["NameFirst"];

				if (rdr["PhNumber"] != DBNull.Value)
					ci.PhNumber = (string) rdr["PhNumber"];
							
				return ci; 
			}


			ICustDataValidation reader2(SqlDataReader rdr)
			{
				CustData cd = new CustData();
			
				if ( rdr["AccNumber"] != DBNull.Value)
					cd.AccNumber = (int) rdr["AccNumber"];

				if ( rdr["WebPassword"] != DBNull.Value)
					cd.webPassword = (string) rdr["WebPassword"];
							
				return cd;

			}


			
			public CustData getByPayInfo(UOW uow, int payInfo)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustData_Get_By_PayInfo";
				cmd.Parameters.Add("@PayInfo", SqlDbType.Int, 0).Value = payInfo;
				CustData[] res = convert(execReader(cmd));
				
				if (res.Length == 0)
					return null;

				return res[0];
			}
			public int ReserveAcct(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spGetNewAccount";
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0);
                cmd.Parameters["@AccNumber"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				return (int)cmd.Parameters["@AccNumber"].Value;
			}
            public override void insert(DomainObj obj)
            {
                CustData rec = (CustData)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustData_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CustData rec = (CustData)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustData_Del_Id";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CustData rec = (CustData)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustData_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CustData[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCustData_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, CustData rec)
            {
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
                if (rec.confNum == null)
                    cmd.Parameters.Add("@ConfNum", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.ConfNum.Length == 0)
                        cmd.Parameters.Add("@ConfNum", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ConfNum", SqlDbType.VarChar, 20).Value = rec.confNum;
                }
 
                if (rec.nameLast == null)
                    cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.NameLast.Length == 0)
                        cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 25).Value = rec.nameLast;
                }
 
                if (rec.nameFirst == null)
                    cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.NameFirst.Length == 0)
                        cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 25).Value = rec.nameFirst;
                }
 
                if (rec.ctNumber1 == null)
                    cmd.Parameters.Add("@CtNumber1", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.CtNumber1.Length == 0)
                        cmd.Parameters.Add("@CtNumber1", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@CtNumber1", SqlDbType.VarChar, 10).Value = rec.ctNumber1;
                }
 
                if (rec.ctNumber2 == null)
                    cmd.Parameters.Add("@CtNumber2", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.CtNumber2.Length == 0)
                        cmd.Parameters.Add("@CtNumber2", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@CtNumber2", SqlDbType.VarChar, 10).Value = rec.ctNumber2;
                }
 
                if (rec.adrNum == null)
                    cmd.Parameters.Add("@AdrNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrNum.Length == 0)
                        cmd.Parameters.Add("@AdrNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrNum", SqlDbType.VarChar, 10).Value = rec.adrNum;
                }
 
                if (rec.adrNumSfx == null)
                    cmd.Parameters.Add("@AdrNumSfx", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrNumSfx.Length == 0)
                        cmd.Parameters.Add("@AdrNumSfx", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrNumSfx", SqlDbType.VarChar, 10).Value = rec.adrNumSfx;
                }
 
                if (rec.adrPfx == null)
                    cmd.Parameters.Add("@AdrPfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.AdrPfx.Length == 0)
                        cmd.Parameters.Add("@AdrPfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrPfx", SqlDbType.VarChar, 2).Value = rec.adrPfx;
                }
 
                if (rec.adrStreet == null)
                    cmd.Parameters.Add("@AdrStreet", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.AdrStreet.Length == 0)
                        cmd.Parameters.Add("@AdrStreet", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrStreet", SqlDbType.VarChar, 50).Value = rec.adrStreet;
                }
 
                if (rec.adrStreetType == null)
                    cmd.Parameters.Add("@AdrStreetType", SqlDbType.VarChar, 4).Value = DBNull.Value;
                else
                {
                    if (rec.AdrStreetType.Length == 0)
                        cmd.Parameters.Add("@AdrStreetType", SqlDbType.VarChar, 4).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrStreetType", SqlDbType.VarChar, 4).Value = rec.adrStreetType;
                }
 
                if (rec.adrSfx == null)
                    cmd.Parameters.Add("@AdrSfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.AdrSfx.Length == 0)
                        cmd.Parameters.Add("@AdrSfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrSfx", SqlDbType.VarChar, 2).Value = rec.adrSfx;
                }
 
                if (rec.adrUnitDesc == null)
                    cmd.Parameters.Add("@AdrUnitDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrUnitDesc.Length == 0)
                        cmd.Parameters.Add("@AdrUnitDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrUnitDesc", SqlDbType.VarChar, 10).Value = rec.adrUnitDesc;
                }
 
                if (rec.adrUnit == null)
                    cmd.Parameters.Add("@AdrUnit", SqlDbType.VarChar, 8).Value = DBNull.Value;
                else
                {
                    if (rec.AdrUnit.Length == 0)
                        cmd.Parameters.Add("@AdrUnit", SqlDbType.VarChar, 8).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrUnit", SqlDbType.VarChar, 8).Value = rec.adrUnit;
                }
 
                if (rec.adrElevation == null)
                    cmd.Parameters.Add("@AdrElevation", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrElevation.Length == 0)
                        cmd.Parameters.Add("@AdrElevation", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrElevation", SqlDbType.VarChar, 10).Value = rec.adrElevation;
                }
 
                if (rec.adrFloor == null)
                    cmd.Parameters.Add("@AdrFloor", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrFloor.Length == 0)
                        cmd.Parameters.Add("@AdrFloor", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrFloor", SqlDbType.VarChar, 10).Value = rec.adrFloor;
                }
 
                if (rec.adrStructureDesc == null)
                    cmd.Parameters.Add("@AdrStructureDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrStructureDesc.Length == 0)
                        cmd.Parameters.Add("@AdrStructureDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrStructureDesc", SqlDbType.VarChar, 10).Value = rec.adrStructureDesc;
                }
 
                if (rec.adrStructureNum == null)
                    cmd.Parameters.Add("@AdrStructureNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.AdrStructureNum.Length == 0)
                        cmd.Parameters.Add("@AdrStructureNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrStructureNum", SqlDbType.VarChar, 10).Value = rec.adrStructureNum;
                }
 
                if (rec.adrCity == null)
                    cmd.Parameters.Add("@AdrCity", SqlDbType.VarChar, 28).Value = DBNull.Value;
                else
                {
                    if (rec.AdrCity.Length == 0)
                        cmd.Parameters.Add("@AdrCity", SqlDbType.VarChar, 28).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrCity", SqlDbType.VarChar, 28).Value = rec.adrCity;
                }
 
                if (rec.adrState == null)
                    cmd.Parameters.Add("@AdrState", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.AdrState.Length == 0)
                        cmd.Parameters.Add("@AdrState", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrState", SqlDbType.VarChar, 2).Value = rec.adrState;
                }
 
                if (rec.adrZip == null)
                    cmd.Parameters.Add("@AdrZip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                else
                {
                    if (rec.AdrZip.Length == 0)
                        cmd.Parameters.Add("@AdrZip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@AdrZip", SqlDbType.VarChar, 5).Value = rec.adrZip;
                }
 
                if (rec.mail_AdrNum == null)
                    cmd.Parameters.Add("@Mail_AdrNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrNum.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrNum", SqlDbType.VarChar, 10).Value = rec.mail_AdrNum;
                }
 
                if (rec.mail_AdrNumSfx == null)
                    cmd.Parameters.Add("@Mail_AdrNumSfx", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrNumSfx.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrNumSfx", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrNumSfx", SqlDbType.VarChar, 10).Value = rec.mail_AdrNumSfx;
                }
 
                if (rec.mail_AdrPfx == null)
                    cmd.Parameters.Add("@Mail_AdrPfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrPfx.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrPfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrPfx", SqlDbType.VarChar, 2).Value = rec.mail_AdrPfx;
                }
 
                if (rec.mail_AdrStreet == null)
                    cmd.Parameters.Add("@Mail_AdrStreet", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrStreet.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrStreet", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrStreet", SqlDbType.VarChar, 50).Value = rec.mail_AdrStreet;
                }
 
                if (rec.mail_adrStreetType == null)
                    cmd.Parameters.Add("@Mail_adrStreetType", SqlDbType.VarChar, 4).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_adrStreetType.Length == 0)
                        cmd.Parameters.Add("@Mail_adrStreetType", SqlDbType.VarChar, 4).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_adrStreetType", SqlDbType.VarChar, 4).Value = rec.mail_adrStreetType;
                }
 
                if (rec.mail_AdrSfx == null)
                    cmd.Parameters.Add("@Mail_AdrSfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrSfx.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrSfx", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrSfx", SqlDbType.VarChar, 2).Value = rec.mail_AdrSfx;
                }
 
                if (rec.mail_AdrUnitDesc == null)
                    cmd.Parameters.Add("@Mail_AdrUnitDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrUnitDesc.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrUnitDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrUnitDesc", SqlDbType.VarChar, 10).Value = rec.mail_AdrUnitDesc;
                }
 
                if (rec.mail_AdrUnit == null)
                    cmd.Parameters.Add("@Mail_AdrUnit", SqlDbType.VarChar, 8).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrUnit.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrUnit", SqlDbType.VarChar, 8).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrUnit", SqlDbType.VarChar, 8).Value = rec.mail_AdrUnit;
                }
 
                if (rec.mail_AdrElevation == null)
                    cmd.Parameters.Add("@Mail_AdrElevation", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrElevation.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrElevation", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrElevation", SqlDbType.VarChar, 10).Value = rec.mail_AdrElevation;
                }
 
                if (rec.mail_AdrFloor == null)
                    cmd.Parameters.Add("@Mail_AdrFloor", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrFloor.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrFloor", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrFloor", SqlDbType.VarChar, 10).Value = rec.mail_AdrFloor;
                }
 
                if (rec.mail_AdrStructureDesc == null)
                    cmd.Parameters.Add("@Mail_AdrStructureDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrStructureDesc.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrStructureDesc", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrStructureDesc", SqlDbType.VarChar, 10).Value = rec.mail_AdrStructureDesc;
                }
 
                if (rec.mail_AdrStructureNum == null)
                    cmd.Parameters.Add("@Mail_AdrStructureNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrStructureNum.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrStructureNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrStructureNum", SqlDbType.VarChar, 10).Value = rec.mail_AdrStructureNum;
                }
 
                if (rec.mail_AdrCity == null)
                    cmd.Parameters.Add("@Mail_AdrCity", SqlDbType.VarChar, 28).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrCity.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrCity", SqlDbType.VarChar, 28).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrCity", SqlDbType.VarChar, 28).Value = rec.mail_AdrCity;
                }
 
                if (rec.mail_AdrState == null)
                    cmd.Parameters.Add("@Mail_AdrState", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrState.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrState", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrState", SqlDbType.VarChar, 2).Value = rec.mail_AdrState;
                }
 
                if (rec.mail_AdrZip == null)
                    cmd.Parameters.Add("@Mail_AdrZip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                else
                {
                    if (rec.Mail_AdrZip.Length == 0)
                        cmd.Parameters.Add("@Mail_AdrZip", SqlDbType.VarChar, 5).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Mail_AdrZip", SqlDbType.VarChar, 5).Value = rec.mail_AdrZip;
                }
 
                if (rec.complex == null)
                    cmd.Parameters.Add("@Complex", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Complex.Length == 0)
                        cmd.Parameters.Add("@Complex", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Complex", SqlDbType.VarChar, 50).Value = rec.complex;
                }
 
                if (rec.prevIlec == null)
                    cmd.Parameters.Add("@PrevIlec", SqlDbType.VarChar, 3).Value = DBNull.Value;
                else
                {
                    if (rec.PrevIlec.Length == 0)
                        cmd.Parameters.Add("@PrevIlec", SqlDbType.VarChar, 3).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PrevIlec", SqlDbType.VarChar, 3).Value = rec.prevIlec;
                }
 
                if (rec.prevPHNum == null)
                    cmd.Parameters.Add("@PrevPHNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PrevPHNum.Length == 0)
                        cmd.Parameters.Add("@PrevPHNum", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PrevPHNum", SqlDbType.VarChar, 10).Value = rec.prevPHNum;
                }
 
                cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
 
                cmd.Parameters.Add("@PayDate", SqlDbType.DateTime, 0).Value = rec.payDate;
 
                if (rec.payTime == DateTime.MinValue)
                    cmd.Parameters.Add("@PayTime", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@PayTime", SqlDbType.DateTime, 0).Value = rec.payTime;
 
                if (rec.priceCode == null)
                    cmd.Parameters.Add("@PriceCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PriceCode.Length == 0)
                        cmd.Parameters.Add("@PriceCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PriceCode", SqlDbType.VarChar, 10).Value = rec.priceCode;
                }
 
                if (rec.ilec == null)
                    cmd.Parameters.Add("@Ilec", SqlDbType.VarChar, 3).Value = DBNull.Value;
                else
                {
                    if (rec.Ilec.Length == 0)
                        cmd.Parameters.Add("@Ilec", SqlDbType.VarChar, 3).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Ilec", SqlDbType.VarChar, 3).Value = rec.ilec;
                }
 
                if (rec.phNumber == null)
                    cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.PhNumber.Length == 0)
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = rec.phNumber;
                }
 
                if (rec.activDate == DateTime.MinValue)
                    cmd.Parameters.Add("@ActivDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ActivDate", SqlDbType.DateTime, 0).Value = rec.activDate;
 
                if (rec.sDiscoDate == DateTime.MinValue)
                    cmd.Parameters.Add("@SDiscoDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@SDiscoDate", SqlDbType.DateTime, 0).Value = rec.sDiscoDate;
 
                if (rec.aDiscoDate == DateTime.MinValue)
                    cmd.Parameters.Add("@ADiscoDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ADiscoDate", SqlDbType.DateTime, 0).Value = rec.aDiscoDate;
 
                if (rec.nOrder == null)
                    cmd.Parameters.Add("@NOrder", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.NOrder.Length == 0)
                        cmd.Parameters.Add("@NOrder", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@NOrder", SqlDbType.VarChar, 15).Value = rec.nOrder;
                }
 
                if (rec.dOrder == null)
                    cmd.Parameters.Add("@DOrder", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.DOrder.Length == 0)
                        cmd.Parameters.Add("@DOrder", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@DOrder", SqlDbType.VarChar, 15).Value = rec.dOrder;
                }
 
                cmd.Parameters.Add("@Status1", SqlDbType.VarChar, 5).Value = rec.status1;
 
                cmd.Parameters.Add("@Status3", SqlDbType.VarChar, 5).Value = rec.status3;
                cmd.Parameters.Add("@NxtPymnt", SqlDbType.Decimal, 0).Value = rec.nxtPymnt;
                cmd.Parameters.Add("@Balance", SqlDbType.Decimal, 0).Value = rec.balance;
                cmd.Parameters.Add("@LstPymnt", SqlDbType.Decimal, 0).Value = rec.lstPymnt;
 
                cmd.Parameters.Add("@LstPayDate", SqlDbType.DateTime, 0).Value = rec.lstPayDate;
                cmd.Parameters.Add("@TotalPymnts", SqlDbType.Decimal, 0).Value = rec.totalPymnts;
                cmd.Parameters.Add("@Grace", SqlDbType.Int, 0).Value = rec.grace;
                cmd.Parameters.Add("@Reminder", SqlDbType.Int, 0).Value = rec.reminder;
                cmd.Parameters.Add("@DayCredit", SqlDbType.Int, 0).Value = rec.dayCredit;
                cmd.Parameters.Add("@PermCredit", SqlDbType.Int, 0).Value = rec.permCredit;
 
                cmd.Parameters.Add("@Bill_Initial", SqlDbType.Bit, 0).Value = rec.bill_Initial;
 
                cmd.Parameters.Add("@Bill_One", SqlDbType.Bit, 0).Value = rec.bill_One;
 
                cmd.Parameters.Add("@Bill_Two", SqlDbType.Bit, 0).Value = rec.bill_Two;
 
                cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 10).Value = rec.taxCode;
 
                if (rec.wU_SwiftPay_ID == null)
                    cmd.Parameters.Add("@WU_SwiftPay_ID", SqlDbType.VarChar, 16).Value = DBNull.Value;
                else
                {
                    if (rec.WU_SwiftPay_ID.Length == 0)
                        cmd.Parameters.Add("@WU_SwiftPay_ID", SqlDbType.VarChar, 16).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@WU_SwiftPay_ID", SqlDbType.VarChar, 16).Value = rec.wU_SwiftPay_ID;
                }
 
                cmd.Parameters.Add("@Language", SqlDbType.VarChar, 10).Value = rec.language;
 
                if (rec.birthday == null)
                    cmd.Parameters.Add("@Birthday", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Birthday.Length == 0)
                        cmd.Parameters.Add("@Birthday", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Birthday", SqlDbType.VarChar, 10).Value = rec.birthday;
                }
                cmd.Parameters.Add("@Service_Month", SqlDbType.Int, 0).Value = rec.service_Month;
 
                cmd.Parameters.Add("@UNEP", SqlDbType.Bit, 0).Value = rec.uNEP;
 
                if (rec.due_Date == DateTime.MinValue)
                    cmd.Parameters.Add("@Due_Date", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Due_Date", SqlDbType.DateTime, 0).Value = rec.due_Date;
                cmd.Parameters.Add("@Bill_Cycle", SqlDbType.TinyInt, 0).Value = rec.bill_Cycle;
 
                if (rec.pIC == null)
                    cmd.Parameters.Add("@PIC", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.PIC.Length == 0)
                        cmd.Parameters.Add("@PIC", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PIC", SqlDbType.VarChar, 20).Value = rec.pIC;
                }
 
                if (rec.lPIC == null)
                    cmd.Parameters.Add("@LPIC", SqlDbType.VarChar, 20).Value = DBNull.Value;
                else
                {
                    if (rec.LPIC.Length == 0)
                        cmd.Parameters.Add("@LPIC", SqlDbType.VarChar, 20).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@LPIC", SqlDbType.VarChar, 20).Value = rec.lPIC;
                }
 
                if (rec.email == null)
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Email.Length == 0)
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = rec.email;
                }

                if (rec.sourceCode == 0)      
                    cmd.Parameters.Add("@SourceCode", SqlDbType.Int, 0).Value = DBNull.Value;
                else  
                    cmd.Parameters.Add("@SourceCode", SqlDbType.Int, 0).Value = rec.sourceCode;

				if (rec.webPassword == null)
					cmd.Parameters.Add("@WebPassword", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.webPassword.Length == 0)
						cmd.Parameters.Add("@WebPassword", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@WebPassword", SqlDbType.VarChar, 25).Value = 
                            (rec.isWebPasswordTemporal ? "1" : "0") + rec.webPassword;
				}
            }

            protected override DomainObj reader(SqlDataReader rdr)
            {
                CustData rec = new CustData();
                
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["ConfNum"] != DBNull.Value)
                    rec.confNum = (string) rdr["ConfNum"];
 
                if (rdr["NameLast"] != DBNull.Value)
                    rec.nameLast = (string) rdr["NameLast"];
 
                if (rdr["NameFirst"] != DBNull.Value)
                    rec.nameFirst = (string) rdr["NameFirst"];
 
                if (rdr["CtNumber1"] != DBNull.Value)
                    rec.ctNumber1 = (string) rdr["CtNumber1"];
 
                if (rdr["CtNumber2"] != DBNull.Value)
                    rec.ctNumber2 = (string) rdr["CtNumber2"];
 
                if (rdr["AdrNum"] != DBNull.Value)
                    rec.adrNum = (string) rdr["AdrNum"];
 
                if (rdr["AdrNumSfx"] != DBNull.Value)
                    rec.adrNumSfx = (string) rdr["AdrNumSfx"];
 
                if (rdr["AdrPfx"] != DBNull.Value)
                    rec.adrPfx = (string) rdr["AdrPfx"];
 
                if (rdr["AdrStreet"] != DBNull.Value)
                    rec.adrStreet = (string) rdr["AdrStreet"];
 
                if (rdr["AdrStreetType"] != DBNull.Value)
                    rec.adrStreetType = (string) rdr["AdrStreetType"];
 
                if (rdr["AdrSfx"] != DBNull.Value)
                    rec.adrSfx = (string) rdr["AdrSfx"];
 
                if (rdr["AdrUnitDesc"] != DBNull.Value)
                    rec.adrUnitDesc = (string) rdr["AdrUnitDesc"];
 
                if (rdr["AdrUnit"] != DBNull.Value)
                    rec.adrUnit = (string) rdr["AdrUnit"];
 
                if (rdr["AdrElevation"] != DBNull.Value)
                    rec.adrElevation = (string) rdr["AdrElevation"];
 
                if (rdr["AdrFloor"] != DBNull.Value)
                    rec.adrFloor = (string) rdr["AdrFloor"];
 
                if (rdr["AdrStructureDesc"] != DBNull.Value)
                    rec.adrStructureDesc = (string) rdr["AdrStructureDesc"];
 
                if (rdr["AdrStructureNum"] != DBNull.Value)
                    rec.adrStructureNum = (string) rdr["AdrStructureNum"];
 
                if (rdr["AdrCity"] != DBNull.Value)
                    rec.adrCity = (string) rdr["AdrCity"];
 
                if (rdr["AdrState"] != DBNull.Value)
                    rec.adrState = (string) rdr["AdrState"];
 
                if (rdr["AdrZip"] != DBNull.Value)
                    rec.adrZip = (string) rdr["AdrZip"];
 
                if (rdr["Mail_AdrNum"] != DBNull.Value)
                    rec.mail_AdrNum = (string) rdr["Mail_AdrNum"];
 
                if (rdr["Mail_AdrNumSfx"] != DBNull.Value)
                    rec.mail_AdrNumSfx = (string) rdr["Mail_AdrNumSfx"];
 
                if (rdr["Mail_AdrPfx"] != DBNull.Value)
                    rec.mail_AdrPfx = (string) rdr["Mail_AdrPfx"];
 
                if (rdr["Mail_AdrStreet"] != DBNull.Value)
                    rec.mail_AdrStreet = (string) rdr["Mail_AdrStreet"];
 
                if (rdr["Mail_adrStreetType"] != DBNull.Value)
                    rec.mail_adrStreetType = (string) rdr["Mail_adrStreetType"];
 
                if (rdr["Mail_AdrSfx"] != DBNull.Value)
                    rec.mail_AdrSfx = (string) rdr["Mail_AdrSfx"];
 
                if (rdr["Mail_AdrUnitDesc"] != DBNull.Value)
                    rec.mail_AdrUnitDesc = (string) rdr["Mail_AdrUnitDesc"];
 
                if (rdr["Mail_AdrUnit"] != DBNull.Value)
                    rec.mail_AdrUnit = (string) rdr["Mail_AdrUnit"];
 
                if (rdr["Mail_AdrElevation"] != DBNull.Value)
                    rec.mail_AdrElevation = (string) rdr["Mail_AdrElevation"];
 
                if (rdr["Mail_AdrFloor"] != DBNull.Value)
                    rec.mail_AdrFloor = (string) rdr["Mail_AdrFloor"];
 
                if (rdr["Mail_AdrStructureDesc"] != DBNull.Value)
                    rec.mail_AdrStructureDesc = (string) rdr["Mail_AdrStructureDesc"];
 
                if (rdr["Mail_AdrStructureNum"] != DBNull.Value)
                    rec.mail_AdrStructureNum = (string) rdr["Mail_AdrStructureNum"];
 
                if (rdr["Mail_AdrCity"] != DBNull.Value)
                    rec.mail_AdrCity = (string) rdr["Mail_AdrCity"];
 
                if (rdr["Mail_AdrState"] != DBNull.Value)
                    rec.mail_AdrState = (string) rdr["Mail_AdrState"];
 
                if (rdr["Mail_AdrZip"] != DBNull.Value)
                    rec.mail_AdrZip = (string) rdr["Mail_AdrZip"];
 
                if (rdr["Complex"] != DBNull.Value)
                    rec.complex = (string) rdr["Complex"];
 
                if (rdr["PrevIlec"] != DBNull.Value)
                    rec.prevIlec = (string) rdr["PrevIlec"];
 
                if (rdr["PrevPHNum"] != DBNull.Value)
                    rec.prevPHNum = (string) rdr["PrevPHNum"];
 
                if (rdr["StoreCode"] != DBNull.Value)
                    rec.storeCode = (string) rdr["StoreCode"];
 
                if (rdr["PayDate"] != DBNull.Value)
                    rec.payDate = (DateTime) rdr["PayDate"];
 
                if (rdr["PayTime"] != DBNull.Value)
                    rec.payTime = (DateTime) rdr["PayTime"];
 
                if (rdr["PriceCode"] != DBNull.Value)
                    rec.priceCode = (string) rdr["PriceCode"];
 
                if (rdr["Ilec"] != DBNull.Value)
                    rec.ilec = (string) rdr["Ilec"];
 
                if (rdr["PhNumber"] != DBNull.Value)
                    rec.phNumber = (string) rdr["PhNumber"];
 
                if (rdr["ActivDate"] != DBNull.Value)
                    rec.activDate = (DateTime) rdr["ActivDate"];
 
                if (rdr["SDiscoDate"] != DBNull.Value)
                    rec.sDiscoDate = (DateTime) rdr["SDiscoDate"];
 
                if (rdr["ADiscoDate"] != DBNull.Value)
                    rec.aDiscoDate = (DateTime) rdr["ADiscoDate"];
 
                if (rdr["NOrder"] != DBNull.Value)
                    rec.nOrder = (string) rdr["NOrder"];
 
                if (rdr["DOrder"] != DBNull.Value)
                    rec.dOrder = (string) rdr["DOrder"];
 
                if (rdr["Status1"] != DBNull.Value)
                    rec.status1 = (string) rdr["Status1"];
 
                if (rdr["Status3"] != DBNull.Value)
                    rec.status3 = (string) rdr["Status3"];
 
                if (rdr["NxtPymnt"] != DBNull.Value)
                    rec.nxtPymnt = Decimal.Round((decimal)rdr["NxtPymnt"], 2);
 
                if (rdr["Balance"] != DBNull.Value)
                    rec.balance = Decimal.Round((decimal)rdr["Balance"], 2);
 
                if (rdr["LstPymnt"] != DBNull.Value)
                    rec.lstPymnt = Decimal.Round((decimal)rdr["LstPymnt"], 2);
 
                if (rdr["LstPayDate"] != DBNull.Value)
                    rec.lstPayDate = (DateTime) rdr["LstPayDate"];
 
                if (rdr["TotalPymnts"] != DBNull.Value)
                    rec.totalPymnts = Decimal.Round((decimal)rdr["TotalPymnts"], 2);
 
                if (rdr["Grace"] != DBNull.Value)
                    rec.grace = (int) rdr["Grace"];
 
                if (rdr["Reminder"] != DBNull.Value)
                    rec.reminder = (int) rdr["Reminder"];
 
                if (rdr["DayCredit"] != DBNull.Value)
                    rec.dayCredit = (int) rdr["DayCredit"];
 
                if (rdr["PermCredit"] != DBNull.Value)
                    rec.permCredit = (int) rdr["PermCredit"];
 
                if (rdr["Bill_Initial"] != DBNull.Value)
                    rec.bill_Initial = (bool) rdr["Bill_Initial"];
 
                if (rdr["Bill_One"] != DBNull.Value)
                    rec.bill_One = (bool) rdr["Bill_One"];
 
                if (rdr["Bill_Two"] != DBNull.Value)
                    rec.bill_Two = (bool) rdr["Bill_Two"];
 
                if (rdr["TaxCode"] != DBNull.Value)
                    rec.taxCode = (string) rdr["TaxCode"];
 
                if (rdr["WU_SwiftPay_ID"] != DBNull.Value)
                    rec.wU_SwiftPay_ID = (string) rdr["WU_SwiftPay_ID"];
 
                if (rdr["Language"] != DBNull.Value)
                    rec.language = (string) rdr["Language"];
 
                if (rdr["Birthday"] != DBNull.Value)
                    rec.birthday = (string) rdr["Birthday"];
 
                if (rdr["Service_Month"] != DBNull.Value)
                    rec.service_Month = (int) rdr["Service_Month"];
 
                if (rdr["UNEP"] != DBNull.Value)
                    rec.uNEP = (bool) rdr["UNEP"];
 
                if (rdr["Due_Date"] != DBNull.Value)
                    rec.due_Date = (DateTime) rdr["Due_Date"];
 
                if (rdr["Bill_Cycle"] != DBNull.Value)
                    rec.bill_Cycle = (byte) rdr["Bill_Cycle"];
 
                if (rdr["PIC"] != DBNull.Value)
                    rec.pIC = (string) rdr["PIC"];
 
                if (rdr["LPIC"] != DBNull.Value)
                    rec.lPIC = (string) rdr["LPIC"];
 
                if (rdr["Email"] != DBNull.Value)
                    rec.email = (string) rdr["Email"];

				if (rdr["SourceCode"] != DBNull.Value)
					rec.sourceCode = (int) rdr["SourceCode"];

                if (rdr["WebPassword"] != DBNull.Value) {
                    InitPasswordFields(ref rec, (string)rdr["WebPassword"]);
                } else {
                    rec.webPassword = null;
                    rec.isWebPasswordTemporal = false;
                }
 
                rec.rowState = RowState.Clean;
                return rec;
            }

            private void InitPasswordFields(ref CustData rec, string value)
            {
                if (value.Length == 0) {
                    throw new DataException("WebPassword field must be either NULL or " 
                        + "begin with '0' or '1'.");
                }

                string flag = value.Substring(0, 1);
                if (flag != "0" && flag != "1") {
                    throw new DataException("WebPassword field must be either NULL or " 
                        + "begin with '0' or '1'.");
                }

                string password = (value.Length == 1 ? string.Empty
                    : value.Substring(1, value.Length - 1));

                rec.webPassword = password;
                rec.isWebPasswordTemporal = flag == "1";
            }

            CustData[] convert(DomainObj[] objs)
            {
                CustData[] acls  = new CustData[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
		public void setMailAddr(IAddr mailAddr)
		{

			mail_AdrCity = mailAddr.City;
			mail_AdrState = mailAddr.State;
			mail_AdrStreet = mailAddr.Street;
			mail_AdrNum = mailAddr.StreetNum;
			mail_AdrPfx = mailAddr.StreetPrefix;
			mail_AdrSfx = mailAddr.StreetSuffix;
			mail_adrStreetType = mailAddr.StreetType;
			mail_AdrUnit = mailAddr.Unit;
			mail_AdrZip = mailAddr.Zipcode;
		}
		public void setSvcAddr(IAddr svcAddr)
		{
			adrCity = svcAddr.City;
			adrState = svcAddr.State;
			adrStreet = svcAddr.Street;
			adrNum = svcAddr.StreetNum;
			adrPfx = svcAddr.StreetPrefix;
			adrSfx = svcAddr.StreetSuffix;
			adrStreetType = svcAddr.StreetType;
			adrUnit = svcAddr.Unit;
			adrZip = svcAddr.Zipcode;
		}

		// saves to database right away, returns account number
		public void setCustInfo(ICustInfo cust)
		{
			this.nameFirst = cust.FirstName;
			this.nameLast = cust.LastName;
			this.prevIlec = cust.PrevILEC;
			this.prevPHNum = cust.PrevPhone;			
			this.ctNumber1 =	cust.Contact;
			this.ctNumber2 = cust.Contact2;
			this.email = cust.Email;
			this.birthday = cust.Birthday != null?cust.Birthday.ToString():null;
		}
		public void setCustInfo(ICustInfo2 cust)
		{
			this.nameFirst = cust.FirstName;
			this.nameLast = cust.LastName;
			this.prevIlec = cust.PrevILEC;
			this.prevPHNum = cust.PrevPhone;
			this.phNumber = cust.PhNumber;
			this.ctNumber1 =	cust.Contact;
			this.ctNumber2 = cust.Contact2;
			this.email = cust.Email;
			this.birthday = cust.Birthday != null?cust.Birthday.ToString():null;
		}
    }
}
