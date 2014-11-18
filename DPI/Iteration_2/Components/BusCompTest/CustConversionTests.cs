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
    public class CustConversionTests
    {
        /*		Data		*/
        string convName = "convName";
        int exclCorp = 2;
        string exclAgent = "exclAgent";
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        string status = "status";
        string description = "description";
        
        /*		Constructors		*/
        public CustConversionTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CustConversionTests test = new CustConversionTests();
            
            // UOW Tests
            test.addCustConversion();
            test.findCustConversion();
            test.saveCustConversion();
            test.findAllCustConversions();
            
            try
            {
                test.delCustConversion();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCustConversion:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCustConversion: " + e.Message);
            }
            
        }
        [Test]
        public void addCustConversion()
        {
            UOW uow = new UOW();
            uow.Service = "addCustConversion";
            CustConversion cls = new CustConversion(uow);
            
            cls.ConvName = this.convName;
            cls.ExclCorp = this.exclCorp;
            cls.ExclAgent = this.exclAgent;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.Status = this.status;
            cls.Description = this.description;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addCustConversion - assert";
            cls = CustConversion.find(uow, this.convName);
            Assertion.Assert(cls.ExclAgent == this.exclAgent);
            uow.close();
        }
        [Test]
        public void findCustConversion()
        {
            UOW uow = new UOW();
            uow.Service = "findCustConversion";
            
            CustConversion cls = CustConversion.find(uow, this.convName);
            Assertion.Assert(cls.ConvName.Trim() == this.convName.Trim());
            uow.close();
        }
        [Test]
        public void saveCustConversion()
        {
            UOW uow = new UOW();
            uow.Service = "saveCustConversion";
            CustConversion cls = CustConversion.find(uow, this.convName);
            
            cls.ConvName = this.convName;
            cls.ExclCorp = this.exclCorp;
            cls.ExclAgent = this.exclAgent;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.Status = this.status;
            cls.Description = this.description;
            cls.ExclCorp += 2;
            this.exclCorp = cls.ExclCorp;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCustConversion - assert";
            
            cls = CustConversion.find(uow, this.convName);
            Assertion.Assert(cls.ExclCorp == this.exclCorp);
            uow.close();
        }
        [Test]
        public void findAllCustConversions()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCustConversions";
            CustConversion[] objs = CustConversion.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCustConversion()
        {
            UOW uow = new UOW();
            uow.Service = "delCustConversion";
            CustConversion cls = CustConversion.find(uow, this.convName);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCustConversion - assert";
            cls = CustConversion.find(uow, this.convName);
            Assertion.Assert((cls.ConvName ==  null));
            uow.close();
        }
    }
}
