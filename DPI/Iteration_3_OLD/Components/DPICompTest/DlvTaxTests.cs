using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Interfaces;


namespace DPI.Components
{
    [TestFixture]
    public class DlvTaxTests
    {
        /*		Data		*/
        int id;
        int ver = 0;
        int dlv = 16;
        string taxId = "S";
        decimal taxAmount = 4.5M;
        
        /*		Constructors		*/
        public DlvTaxTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DlvTaxTests test = new DlvTaxTests();
            
            // UOW Tests
            test.addDlvTax();
            test.findDlvTax();
            test.saveDlvTax();
            test.findAllDlvTaxs();
            
            try
            {
                test.delDlvTax();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDlvTax:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDlvTax: " + e.Message);
            }
            
        }
        [Test]
        public void addDlvTax()
        {
            UOW uow = new UOW();
            uow.Service = "addDlvTax";
            DlvTax cls = new DlvTax(uow);
            
            cls.Dlv = this.dlv;
            cls.TaxId = this.taxId;
            cls.TaxAmount = this.taxAmount;
        
            uow.commit();
            this.id = cls.Id;
       //     this.ver = cls.Ver;
            
            uow = new UOW();
            uow.Service = "addDlvTax - assert";
            cls = DlvTax.find(uow, this.id);
            Assertion.Assert(cls.Dlv == this.dlv);
            uow.close();
        }
        [Test]
        public void findDlvTax()
        {
            UOW uow = new UOW();
            uow.Service = "findDlvTax";
            
            DlvTax cls = DlvTax.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveDlvTax()
        {
            UOW uow = new UOW();
            uow.Service = "saveDlvTax";

            DlvTax cls = DlvTax.find(uow, this.id);

			this.taxId = "K";
			cls.TaxId = this.taxId;
			this.taxAmount += 1.7M;
            cls.TaxAmount = this.taxAmount;
                
            uow.commit();

      //      this.ver++;

            uow = new UOW();
            uow.Service = "saveDlvTax - assert";
            
            cls = DlvTax.find(uow, this.id);
			Assertion.Assert(cls.TaxId == this.taxId);
			Assertion.Assert(cls.TaxAmount == this.taxAmount);
			Assertion.Assert(cls.Ver == this.ver);
			uow.close();
        }
        [Test]
        public void findAllDlvTaxs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDlvTaxs";
            DlvTax[] objs = DlvTax.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDlvTax()
        {
            UOW uow = new UOW();
            uow.Service = "delDlvTax";
            DlvTax cls = DlvTax.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDlvTax - assert";
            cls = DlvTax.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
