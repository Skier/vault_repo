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
    public class ReceiptTests
    {
        /*		Data		*/
        int id;
        string name = "name";
        string csharpName = "c#Name";
        string comments = "comments";
        
        /*		Constructors		*/
        public ReceiptTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ReceiptTests test = new ReceiptTests();
            
            // UOW Tests
            test.addReceipt();
            test.findReceipt();
            test.saveReceipt();
            test.findAllReceipts();
            
            try
            {
                test.delReceipt();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delReceipt:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delReceipt: " + e.Message);
            }
            
        }
        [Test]
        public void addReceipt()
        {
            UOW uow = new UOW();
            uow.Service = "addReceipt";
            Receipt2 cls = new Receipt2(uow);
            
            cls.Name = this.name;
            cls.CSharpName = this.csharpName;
            cls.Comments = this.comments;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addReceipt - assert";
            cls = (Receipt2)Receipt2.find(uow, this.id);
            Assertion.Assert(cls.CSharpName == this.csharpName);
            uow.close();
        }
        [Test]
        public void findReceipt()
        {
            UOW uow = new UOW();
            uow.Service = "findReceipt";
            
            Receipt2 cls = (Receipt2)Receipt2.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveReceipt()
        {
            UOW uow = new UOW();
            uow.Service = "saveReceipt";
            Receipt2 cls = (Receipt2)Receipt2.find(uow, this.id);
            
            cls.Name = this.name;
            cls.CSharpName= this.csharpName;
            cls.Comments = this.comments;
            cls.Name += " saved";
            this.name = cls.Name;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveReceipt - assert";
            
            cls = (Receipt2)Receipt2.find(uow, this.id);
            Assertion.Assert(cls.Name == this.name);
            uow.close();
        }
        [Test]
        public void findAllReceipts()
        {
            UOW uow = new UOW();
            uow.Service = "findAllReceipts";
            IReceipt2[] objs = Receipt2.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delReceipt()
        {
            UOW uow = new UOW();
            uow.Service = "delReceipt";
            Receipt2 cls = (Receipt2)Receipt2.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delReceipt - assert";
            cls = (Receipt2)Receipt2.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
