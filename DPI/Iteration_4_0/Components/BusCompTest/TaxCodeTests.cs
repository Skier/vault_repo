using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework; 

using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class TaxCodeTests
    {
        /*		Data		*/
        string taxCode = "ZZ";
        string description = "description";
        int billSoftTran = 7;
        int billSoftServ = 5;
        
        /*		Constructors		*/
        public TaxCodeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            TaxCodeTests test = new TaxCodeTests();
            
            // UOW Tests
            test.addTaxCode();
            test.findTaxCode();
            test.saveTaxCode();
            test.findAllTaxCodes();
            
            try
            {
                test.delTaxCode();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delTaxCode:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delTaxCode: " + e.Message);
            }
            
        }
        [Test]
        public void addTaxCode()
        {
            UOW uow = new UOW();
            uow.Service = "addTaxCode";
            TaxCode cls = new TaxCode(uow);
            
            cls.TxCode = this.taxCode;
            cls.Description = this.description;
            cls.BillSoftTran = this.billSoftTran;
            cls.BillSoftServ = this.billSoftServ;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addTaxCode - assert";
            cls = TaxCode.find(uow, this.taxCode);
            Assertion.Assert(cls.BillSoftTran == this.billSoftTran);
            uow.close();
        }
        [Test]
        public void findTaxCode()
        {
            UOW uow = new UOW();
            uow.Service = "findTaxCode";
            
            TaxCode cls = TaxCode.find(uow, this.taxCode);
            Assertion.Assert(cls.TxCode.Trim() == this.taxCode.Trim());
            uow.close();
        }
        [Test]
        public void saveTaxCode()
        {
            UOW uow = new UOW();
            uow.Service = "saveTaxCode";
            TaxCode cls = TaxCode.find(uow, this.taxCode);
            
            cls.TxCode = this.taxCode;
            cls.Description = this.description;
            cls.BillSoftTran = this.billSoftTran;
            cls.BillSoftServ = this.billSoftServ;
            cls.Description += " saved";
            this.description = cls.Description;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveTaxCode - assert";
            
            cls = TaxCode.find(uow, this.taxCode);
            Assertion.Assert(cls.Description == this.description);
            uow.close();
        }
        [Test]
        public void findAllTaxCodes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllTaxCodes";
            TaxCode[] objs = TaxCode.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delTaxCode()
        {
            UOW uow = new UOW();
            uow.Service = "delTaxCode";
            TaxCode cls = TaxCode.find(uow, this.taxCode);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delTaxCode - assert";
            cls = TaxCode.find(uow, this.taxCode);
            Assertion.Assert((cls.TxCode ==  null));
            uow.close();
        }
    }
}
