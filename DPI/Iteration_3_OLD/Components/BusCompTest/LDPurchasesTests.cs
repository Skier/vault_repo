using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class LDPurchasesTests
    {
        /*		Data		*/
		/*p_id = 239279
		accNumber = 50391855
		amount = 27
		ld_type = p
		 ld_Product = unld*/

        int lDPurchases_ID = 239279;
        int accNumber = 50391855;
        decimal amount = 67M;
        DateTime purchase_Date = DateTime.Now;
        bool ordered = true;
        string lD_Type = "F";
        string lD_Product = "UNLD";
        
        /*		Constructors		*/
        public LDPurchasesTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            LDPurchasesTests test = new LDPurchasesTests();
            
            // UOW Tests
       //     test.addLDPurchases();
        //    test.findLDPurchases();
          //  test.saveLDPurchases();
            test.findAllLDPurchaseses();
            
            try
            {
                test.delLDPurchases();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delLDPurchases:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delLDPurchases: " + e.Message);
            }
            
        }
        [Test]
        public void addLDPurchases()
        {
            UOW uow = new UOW();
            uow.Service = "addLDPurchases";
            LDPurchases cls = new LDPurchases(uow);
            
            cls.AccNumber = this.accNumber;
            cls.Amount = this.amount;
            cls.Purchase_Date = this.purchase_Date;
            cls.Ordered = this.ordered;
            cls.LD_Type = this.lD_Type;
            cls.LD_Product = this.lD_Product;
        
            uow.commit();
            this.lDPurchases_ID = cls.LDPurchases_ID;
            
            uow = new UOW();
            uow.Service = "addLDPurchases - assert";
            cls = LDPurchases.find(uow, this.lDPurchases_ID);
            Assertion.Assert(cls.Amount == this.amount);
            uow.close();
        }
        [Test]
        public void findLDPurchases()
        {
            UOW uow = new UOW();
            uow.Service = "findLDPurchases";
            
            LDPurchases cls = LDPurchases.find(uow, this.lDPurchases_ID);
            Assertion.Assert(cls.LDPurchases_ID == this.lDPurchases_ID);
            uow.close();
        }
        [Test]
        public void saveLDPurchases()
        {
            UOW uow = new UOW();
            uow.Service = "saveLDPurchases";
            LDPurchases cls = LDPurchases.find(uow, this.lDPurchases_ID);
            
            cls.AccNumber = this.accNumber;
            cls.Amount = this.amount;
            cls.Purchase_Date = this.purchase_Date;
            cls.Ordered = this.ordered;
            cls.LD_Type = this.lD_Type;
            cls.LD_Product = this.lD_Product;
            //cls.AccNumber += 2;
            this.accNumber = cls.AccNumber;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveLDPurchases - assert";
            
            cls = LDPurchases.find(uow, this.lDPurchases_ID);
            Assertion.Assert(cls.AccNumber == this.accNumber);
            uow.close();
        }
        [Test]
        public void findAllLDPurchaseses()
        {
            UOW uow = new UOW();
            uow.Service = "findAllLDPurchaseses";
            LDPurchases[] objs = LDPurchases.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delLDPurchases()
        {
            UOW uow = new UOW();
            uow.Service = "delLDPurchases";
            LDPurchases cls = LDPurchases.find(uow, this.lDPurchases_ID);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delLDPurchases - assert";
            cls = LDPurchases.find(uow, this.lDPurchases_ID);
            Assertion.Assert((cls.LDPurchases_ID == 0));
            uow.close();
        }
    }
}
