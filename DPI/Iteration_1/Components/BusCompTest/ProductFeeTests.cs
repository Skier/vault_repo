using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class ProductFeeTests
    {
        /*		Data		*/
/*        int id;
        string orderType = "orderType";
        int orderedProduct = 3;
        int fee = 4;
 */       
        /*		Constructors		*/
        public ProductFeeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ProductFeeTests test = new ProductFeeTests();
            
            // UOW Tests
   //         test.addProductFee();
   //         test.findProductFee();
   //         test.saveProductFee();
   //         test.findAllProductFees();
/*            
            try
            {
                test.delProductFee();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delProductFee:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delProductFee: " + e.Message);
            }
 */           
        }
		[Test]
		public void testGetFee()
		{
			UOW uow = new UOW();
			uow.Service = "testGetFee";
			ProdPrice[] fees;

			try
			{
				fees = ProductFee.getFeesForProd(uow, 188, "75080", "SWB", OrderType.New);
				Assertion.Assert("No fees?!? (prod 188 zip 75080)",fees.Length!=0); 
			}
			catch(Exception e)
			{
				Assertion.Assert(e.Message, false);
			}
			finally
			{
				uow.close();
			}

		}
        //[Test]
/*        public void addProductFee()
        {
            UOW uow = new UOW();
            uow.Service = "addProductFee";
            ProductFee cls = new ProductFee(uow);
            
            cls.OrderType = this.orderType;
            cls.OrderedProduct = this.orderedProduct;
            cls.Fee = this.fee;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addProductFee - assert";
            cls = ProductFee.find(uow, this.id);
            Assertion.Assert(cls.OrderedProduct == this.orderedProduct);
            uow.close();
        }
        //[Test]
        public void findProductFee()
        {
            UOW uow = new UOW();
            uow.Service = "findProductFee";
            
            ProductFee cls = ProductFee.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        //[Test]
        public void saveProductFee()
        {
            UOW uow = new UOW();
            uow.Service = "saveProductFee";
            ProductFee cls = ProductFee.find(uow, this.id);
            
            cls.OrderType = this.orderType;
            cls.OrderedProduct = this.orderedProduct;
            cls.Fee = this.fee;
            cls.OrderType += " saved";
            this.orderType = cls.OrderType;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveProductFee - assert";
            
            cls = ProductFee.find(uow, this.id);
            Assertion.Assert(cls.OrderType == this.orderType);
            uow.close();
        }
        [Test]
        public void findAllProductFees()
        {
            UOW uow = new UOW();
            uow.Service = "findAllProductFees";
            ProductFee[] objs = ProductFee.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        //[Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delProductFee()
        {
            UOW uow = new UOW();
            uow.Service = "delProductFee";
            ProductFee cls = ProductFee.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delProductFee - assert";
            cls = ProductFee.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
		*/
    }
}
