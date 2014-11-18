using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class DmdTaxTests
    {
        /*		Data		*/
        int id;
        DmdItem dmdItem;
        string taxId = "T1";
        decimal taxAmount = 3.5M;
		string taxCode = "777";
        
        /*		Constructors		*/
        public DmdTaxTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DmdTaxTests test = new DmdTaxTests();
            
            // UOW Tests
            test.addDmdTax();
            test.findDmdTax();
            test.saveDmdTax();
            test.findAllDmdTaxs();
            
            try
            {
                test.delDmdTax();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDmdTax:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDmdTax: " + e.Message);
            }
            
        }
        [Test]
        public void addDmdTax()
        {
            UOW uow = new UOW();
            uow.Service = "addDmdTax";
            DmdTax cls = new DmdTax(uow);
            
			this.dmdItem = DmdItem.find(uow, 1590);

            cls.DmdItm = this.dmdItem;
            cls.TaxId = this.taxId;
            cls.TaxAmount = this.taxAmount;
			cls.TaxCode = this.taxCode;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addDmdTax - assert";
            cls = DmdTax.find(uow, this.id);
			Assertion.Assert(cls.TaxId == this.taxId);
			Assertion.Assert(cls.TaxCode == this.taxCode);
			
			uow.close();
        }
        [Test]
        public void findDmdTax()
        {
            UOW uow = new UOW();
            uow.Service = "findDmdTax";
            
            DmdTax cls = DmdTax.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
			//Assertion.Assert(cls.TaxCode == this.taxCode);
            uow.close();
        }
        [Test]
        public void saveDmdTax()
        {
            UOW uow = new UOW();
            uow.Service = "saveDmdTax";
            DmdTax cls = DmdTax.find(uow, this.id);
            
            cls.DmdItm = this.dmdItem;
            cls.TaxId = this.taxId;
            cls.TaxAmount = ++this.taxAmount;
            this.dmdItem = (DmdItem)cls.DmdItm;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDmdTax - assert";
            
            cls = DmdTax.find(uow, this.id);
            Assertion.Assert(cls.TaxAmount == this.taxAmount);
			Assertion.Assert(cls.TaxCode == this.taxCode);
            uow.close();
        }
        [Test]
        public void findAllDmdTaxs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDmdTaxs";
            DmdTax[] objs = DmdTax.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDmdTax()
        {
            UOW uow = new UOW();
            uow.Service = "delDmdTax";
            DmdTax cls = DmdTax.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDmdTax - assert";
            cls = DmdTax.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
