using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class CustDataTests
    {		
        /*		Data		*/
        int accNumber = 30010099;
        string confNum = "confNum";
        string nameLast = "nameLast";
        string nameFirst = "nameFirst";
        string ctNumber1 = "ctNumber1";
        string ctNumber2 = "ctNumber2";
        string adrNum = "adrNum";
        string adrNumSfx = "adrNumSfx";
        string adrPfx = "adrPfx";
        string adrStreet = "adrStreet";
        string adrStreetType = "adrStreetType";
        string adrSfx = "adrSfx";
        string adrUnitDesc = "adrUnitDesc";
        string adrUnit = "adrUnit";
        string adrElevation = "adrElevation";
        string adrFloor = "adrFloor";
        string adrStructureDesc = "adrStructureDesc";
        string adrStructureNum = "adrStructureNum";
        string adrCity = "adrCity";
        string adrState = "adrState";
        string adrZip = "adrZip";
        string mail_AdrNum = "mail_AdrNum";
        string mail_AdrNumSfx = "mail_AdrNumSfx";
        string mail_AdrPfx = "mail_AdrPfx";
        string mail_AdrStreet = "mail_AdrStreet";
        string mail_adrStreetType = "mail_adrStreetType";
        string mail_AdrSfx = "mail_AdrSfx";
        string mail_AdrUnitDesc = "mail_AdrUnitDesc";
        string mail_AdrUnit = "mail_AdrUnit";
        string mail_AdrElevation = "mail_AdrElevation";
        string mail_AdrFloor = "mail_AdrFloor";
        string mail_AdrStructureDesc = "mail_AdrStructureDesc";
        string mail_AdrStructureNum = "mail_AdrStructureNum";
        string mail_AdrCity = "mail_AdrCity";
        string mail_AdrState = "mail_AdrState";
        string mail_AdrZip = "mail_AdrZip";
        string complex = "complex";
        string prevIlec = "prevIlec";
        string prevPHNum = "prevPHNum";
        string storeCode = "storeCode";
        DateTime payDate = DateTime.Now;
        DateTime payTime = DateTime.Now;
        string priceCode = "priceCode";
        string ilec = "ilec";
        string phNumber = "phNumber";
        DateTime activDate = DateTime.Now;
        DateTime sDiscoDate = DateTime.Now;
        DateTime aDiscoDate = DateTime.Now;
        string nOrder = "nOrder";
        string dOrder = "dOrder";
        string status1 = "status1";
        string status3 = "status3";
        decimal nxtPymnt = 52.5M;
        decimal balance = 53.5M;
        decimal lstPymnt = 54.5M;
        DateTime lstPayDate = DateTime.Now;
        decimal totalPymnts = 56.5M;
        int grace = 58;
        int reminder = 59;
        int dayCredit = 60;
        int permCredit = 61;
        bool bill_Initial = true;
        bool bill_One = true;
        bool bill_Two = true;
        string taxCode = "taxCode";
        string wU_SwiftPay_ID = "wU_SwiftPay_ID";
        string language = "language";
        string birthday = "birthday";
        int service_Month = 69;
        bool uNEP = true;
        DateTime due_Date = DateTime.Now;
        byte bill_Cycle = 72;
        string pIC = "pIC";
        string lPIC = "lPIC";
        string email = "email";
        
        /*		Constructors		*/
        public CustDataTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CustDataTests test = new CustDataTests();
            
            // UOW Tests
           // test.addCustData();
            //test.findCustData();
            //test.saveCustData();
            //test.findAllCustDatas();

            //try
            //{
             //   test.delCustData();
           // }
            //catch(ArgumentException ae)
           // {
            //    Console.WriteLine("Expected exception: delCustData:" + ae.Message);
           // }
           // catch(Exception e)
            //{
              //  Console.WriteLine("Error: delCustData: " + e.Message);
            //}
            
        }
/*        [Test]
        public void addCustData()
        {
            UOW uow = new UOW();
            uow.Service = "addCustData";				
            CustData cls = new CustData(uow);
            
            cls.AccNumber = this.accNumber;
            cls.ConfNum = this.confNum;
            cls.NameLast = this.nameLast;
            cls.NameFirst = this.nameFirst;
            cls.CtNumber1 = this.ctNumber1;
            cls.CtNumber2 = this.ctNumber2;
            cls.AdrNum = this.adrNum;
            cls.AdrNumSfx = this.adrNumSfx;
            cls.AdrPfx = this.adrPfx;
            cls.AdrStreet = this.adrStreet;
            cls.AdrStreetType = this.adrStreetType;
            cls.AdrSfx = this.adrSfx;
            cls.AdrUnitDesc = this.adrUnitDesc;
            cls.AdrUnit = this.adrUnit;
            cls.AdrElevation = this.adrElevation;
            cls.AdrFloor = this.adrFloor;
            cls.AdrStructureDesc = this.adrStructureDesc;
            cls.AdrStructureNum = this.adrStructureNum;
            cls.AdrCity = this.adrCity;
            cls.AdrState = this.adrState;
            cls.AdrZip = this.adrZip;
            cls.Mail_AdrNum = this.mail_AdrNum;
            cls.Mail_AdrNumSfx = this.mail_AdrNumSfx;
            cls.Mail_AdrPfx = this.mail_AdrPfx;
            cls.Mail_AdrStreet = this.mail_AdrStreet;
            cls.Mail_adrStreetType = this.mail_adrStreetType;
            cls.Mail_AdrSfx = this.mail_AdrSfx;
            cls.Mail_AdrUnitDesc = this.mail_AdrUnitDesc;
            cls.Mail_AdrUnit = this.mail_AdrUnit;
            cls.Mail_AdrElevation = this.mail_AdrElevation;
            cls.Mail_AdrFloor = this.mail_AdrFloor;
            cls.Mail_AdrStructureDesc = this.mail_AdrStructureDesc;
            cls.Mail_AdrStructureNum = this.mail_AdrStructureNum;
            cls.Mail_AdrCity = this.mail_AdrCity;
            cls.Mail_AdrState = this.mail_AdrState;
            cls.Mail_AdrZip = this.mail_AdrZip;
            cls.Complex = this.complex;
            cls.PrevIlec = this.prevIlec;
            cls.PrevPHNum = this.prevPHNum;
            cls.StoreCode = this.storeCode;
            cls.PayDate = this.payDate;
            cls.PayTime = this.payTime;
            cls.PriceCode = this.priceCode;
            cls.Ilec = this.ilec;
            cls.PhNumber = this.phNumber;
            cls.ActivDate = this.activDate;
            cls.SDiscoDate = this.sDiscoDate;
            cls.ADiscoDate = this.aDiscoDate;
            cls.NOrder = this.nOrder;
            cls.DOrder = this.dOrder;
            cls.Status1 = this.status1;
            cls.Status3 = this.status3;
            cls.NxtPymnt = this.nxtPymnt;
            cls.Balance = this.balance;
            cls.LstPymnt = this.lstPymnt;
            cls.LstPayDate = this.lstPayDate;
            cls.TotalPymnts = this.totalPymnts;
            cls.Grace = this.grace;
            cls.Reminder = this.reminder;
            cls.DayCredit = this.dayCredit;
            cls.PermCredit = this.permCredit;
            cls.Bill_Initial = this.bill_Initial;
            cls.Bill_One = this.bill_One;
            cls.Bill_Two = this.bill_Two;
            cls.TaxCode = this.taxCode;
            cls.WU_SwiftPay_ID = this.wU_SwiftPay_ID;
            cls.Language = this.language;
            cls.Birthday = this.birthday;
            cls.Service_Month = this.service_Month;
            cls.UNEP = this.uNEP;
            cls.Due_Date = this.due_Date;
            cls.Bill_Cycle = this.bill_Cycle;
            cls.PIC = this.pIC;
            cls.LPIC = this.lPIC;
            cls.Email = this.email;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addCustData - assert";
            cls = CustData.find(uow, this.accNumber);
            Assertion.Assert(cls.NameLast == this.nameLast);
            uow.close();
        }
		*/
        [Test]
        public void findCustData()
        {
            UOW uow = new UOW();
            uow.Service = "findCustData";
            
            CustData cls = CustData.find(uow, this.accNumber);
            Assertion.Assert(cls.AccNumber == this.accNumber);
            uow.close();
        }
	
 /*       [Test]
        public void saveCustData()
        {
            UOW uow = new UOW();
            uow.Service = "saveCustData";
            CustData cls = CustData.find(uow, this.accNumber);
            
            cls.AccNumber = this.accNumber;
            cls.ConfNum = this.confNum;
            cls.NameLast = this.nameLast;
            cls.NameFirst = this.nameFirst;
            cls.CtNumber1 = this.ctNumber1;
            cls.CtNumber2 = this.ctNumber2;
            cls.AdrNum = this.adrNum;
            cls.AdrNumSfx = this.adrNumSfx;
            cls.AdrPfx = this.adrPfx;
            cls.AdrStreet = this.adrStreet;
            cls.AdrStreetType = this.adrStreetType;
            cls.AdrSfx = this.adrSfx;
            cls.AdrUnitDesc = this.adrUnitDesc;
            cls.AdrUnit = this.adrUnit;
            cls.AdrElevation = this.adrElevation;
            cls.AdrFloor = this.adrFloor;
            cls.AdrStructureDesc = this.adrStructureDesc;
            cls.AdrStructureNum = this.adrStructureNum;
            cls.AdrCity = this.adrCity;
            cls.AdrState = this.adrState;
            cls.AdrZip = this.adrZip;
            cls.Mail_AdrNum = this.mail_AdrNum;
            cls.Mail_AdrNumSfx = this.mail_AdrNumSfx;
            cls.Mail_AdrPfx = this.mail_AdrPfx;
            cls.Mail_AdrStreet = this.mail_AdrStreet;
            cls.Mail_adrStreetType = this.mail_adrStreetType;
            cls.Mail_AdrSfx = this.mail_AdrSfx;
            cls.Mail_AdrUnitDesc = this.mail_AdrUnitDesc;
            cls.Mail_AdrUnit = this.mail_AdrUnit;
            cls.Mail_AdrElevation = this.mail_AdrElevation;
            cls.Mail_AdrFloor = this.mail_AdrFloor;
            cls.Mail_AdrStructureDesc = this.mail_AdrStructureDesc;
            cls.Mail_AdrStructureNum = this.mail_AdrStructureNum;
            cls.Mail_AdrCity = this.mail_AdrCity;
            cls.Mail_AdrState = this.mail_AdrState;
            cls.Mail_AdrZip = this.mail_AdrZip;
            cls.Complex = this.complex;
            cls.PrevIlec = this.prevIlec;
            cls.PrevPHNum = this.prevPHNum;
            cls.StoreCode = this.storeCode;
            cls.PayDate = this.payDate;
            cls.PayTime = this.payTime;
            cls.PriceCode = this.priceCode;
            cls.Ilec = this.ilec;
            cls.PhNumber = this.phNumber;
            cls.ActivDate = this.activDate;
            cls.SDiscoDate = this.sDiscoDate;
            cls.ADiscoDate = this.aDiscoDate;
            cls.NOrder = this.nOrder;
            cls.DOrder = this.dOrder;
            cls.Status1 = this.status1;
            cls.Status3 = this.status3;
            cls.NxtPymnt = this.nxtPymnt;
            cls.Balance = this.balance;
            cls.LstPymnt = this.lstPymnt;
            cls.LstPayDate = this.lstPayDate;
            cls.TotalPymnts = this.totalPymnts;
            cls.Grace = this.grace;
            cls.Reminder = this.reminder;
            cls.DayCredit = this.dayCredit;
            cls.PermCredit = this.permCredit;
            cls.Bill_Initial = this.bill_Initial;
            cls.Bill_One = this.bill_One;
            cls.Bill_Two = this.bill_Two;
            cls.TaxCode = this.taxCode;
            cls.WU_SwiftPay_ID = this.wU_SwiftPay_ID;
            cls.Language = this.language;
            cls.Birthday = this.birthday;
            cls.Service_Month = this.service_Month;
            cls.UNEP = this.uNEP;
            cls.Due_Date = this.due_Date;
            cls.Bill_Cycle = this.bill_Cycle;
            cls.PIC = this.pIC;
            cls.LPIC = this.lPIC;
            cls.Email = this.email;
            cls.ConfNum += " saved";
            this.confNum = cls.ConfNum;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCustData - assert";
            
            cls = CustData.find(uow, this.accNumber);
            Assertion.Assert(cls.ConfNum == this.confNum);
            uow.close();
        }

        [Test]
        public void findAllCustDatas()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCustDatas";
            CustData[] objs = CustData.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCustData()
        {
            UOW uow = new UOW();
            uow.Service = "delCustData";
            CustData cls = CustData.find(uow, this.accNumber);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCustData - assert";
            cls = CustData.find(uow, this.accNumber);
            Assertion.Assert((cls.AccNumber == 0));
            uow.close();
        }
		*/
    }
}
