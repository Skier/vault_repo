using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class Energy_CustDataTests
    {
        /*		Data		*/
        int iD;
        string siteCode = "siteCode";
        string quoteId = "quoteId";
        string pin = "pin";
        string address1 = "address1";
        string address2 = "address2";
        string city = "city";
        string state = "state";
        string zip = "zip";
        string zip4 = "zip4";
        string nameFirst = "nameFirst";
        string nameLast = "nameLast";
        string nameMiddle = "nameMiddle";
        string ph1 = "ph1";
        string ph2 = "ph2";
        string email = "email";
        string fax = "fax";
        string preferedContactMethod = "preferedContactMethod";
        string ssn = "ssn";
        string dL = "dL";
        string dlState = "dlState";
        DateTime dOB = DateTime.Now;
        string permitName = "permitName";
        string customerNumberRef = "customerNumberRef";
        string doingBusAs = "doingBusAs";
        bool specialNeedsReq = true;
        bool lowIncomeCustomer = true;
        string language = "language";
        string status = "status";
        DateTime dateInserted = DateTime.Now;
        DateTime dateModified = DateTime.Now;
        
        /*		Constructors		*/
        public Energy_CustDataTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            Energy_CustDataTests test = new Energy_CustDataTests();
            
            // UOW Tests
            test.addEnergy_CustData();
            test.findEnergy_CustData();
            test.saveEnergy_CustData();
            test.findAllEnergy_CustDatas();
            
            try
            {
                test.delEnergy_CustData();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delEnergy_CustData:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delEnergy_CustData: " + e.Message);
            }
            
        }
        [Test]
        public void addEnergy_CustData()
        {
            UOW uow = new UOW();
            uow.Service = "addEnergy_CustData";
            Energy_CustData cls = new Energy_CustData(uow);
            
            cls.SiteCode = this.siteCode;
            cls.QuoteId = this.quoteId;
            cls.Pin = this.pin;
            cls.Address1 = this.address1;
            cls.Address2 = this.address2;
            cls.City = this.city;
            cls.State = this.state;
            cls.Zip = this.zip;
            cls.Zip4 = this.zip4;
            cls.NameFirst = this.nameFirst;
            cls.NameLast = this.nameLast;
            cls.NameMiddle = this.nameMiddle;
            cls.Ph1 = this.ph1;
            cls.Ph2 = this.ph2;
            cls.Email = this.email;
            cls.Fax = this.fax;
            cls.PreferedContactMethod = this.preferedContactMethod;
            cls.Ssn = this.ssn;
            cls.DL = this.dL;
            cls.DlState = this.dlState;
            cls.DOB = this.dOB;
            cls.PermitName = this.permitName;
            cls.CustomerNumberRef = this.customerNumberRef;
            cls.DoingBusAs = this.doingBusAs;
            cls.SpecialNeedsReq = this.specialNeedsReq;
            cls.LowIncomeCustomer = this.lowIncomeCustomer;
            cls.Language = this.language;
            cls.Status = this.status;
            cls.DateInserted = this.dateInserted;
            cls.DateModified = this.dateModified;
        
            uow.commit();
            this.iD = cls.ID;
            
            uow = new UOW();
            uow.Service = "addEnergy_CustData - assert";
            cls = Energy_CustData.find(uow, this.iD);
            Assertion.Assert(cls.QuoteId == this.quoteId);
            uow.close();
        }
        [Test]
        public void findEnergy_CustData()
        {
            UOW uow = new UOW();
            uow.Service = "findEnergy_CustData";
            
            Energy_CustData cls = Energy_CustData.find(uow, this.iD);
            Assertion.Assert(cls.ID == this.iD);
            uow.close();
        }
        [Test]
        public void saveEnergy_CustData()
        {
            UOW uow = new UOW();
            uow.Service = "saveEnergy_CustData";
            Energy_CustData cls = Energy_CustData.find(uow, this.iD);
            
            cls.SiteCode = this.siteCode;
            cls.QuoteId = this.quoteId;
            cls.Pin = this.pin;
            cls.Address1 = this.address1;
            cls.Address2 = this.address2;
            cls.City = this.city;
            cls.State = this.state;
            cls.Zip = this.zip;
            cls.Zip4 = this.zip4;
            cls.NameFirst = this.nameFirst;
            cls.NameLast = this.nameLast;
            cls.NameMiddle = this.nameMiddle;
            cls.Ph1 = this.ph1;
            cls.Ph2 = this.ph2;
            cls.Email = this.email;
            cls.Fax = this.fax;
            cls.PreferedContactMethod = this.preferedContactMethod;
            cls.Ssn = this.ssn;
            cls.DL = this.dL;
            cls.DlState = this.dlState;
            cls.DOB = this.dOB;
            cls.PermitName = this.permitName;
            cls.CustomerNumberRef = this.customerNumberRef;
            cls.DoingBusAs = this.doingBusAs;
            cls.SpecialNeedsReq = this.specialNeedsReq;
            cls.LowIncomeCustomer = this.lowIncomeCustomer;
            cls.Language = this.language;
            cls.Status = this.status;
            cls.DateInserted = this.dateInserted;
            cls.DateModified = this.dateModified;
            cls.SiteCode += " saved";
            this.siteCode = cls.SiteCode;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveEnergy_CustData - assert";
            
            cls = Energy_CustData.find(uow, this.iD);
            Assertion.Assert(cls.SiteCode == this.siteCode);
            uow.close();
        }
        [Test]
        public void findAllEnergy_CustDatas()
        {
            UOW uow = new UOW();
            uow.Service = "findAllEnergy_CustDatas";
            Energy_CustData[] objs = Energy_CustData.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delEnergy_CustData()
        {
            UOW uow = new UOW();
            uow.Service = "delEnergy_CustData";
            Energy_CustData cls = Energy_CustData.find(uow, this.iD);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delEnergy_CustData - assert";
            cls = Energy_CustData.find(uow, this.iD);
            Assertion.Assert((cls.ID == 0));
            uow.close();
        }
    }
}
