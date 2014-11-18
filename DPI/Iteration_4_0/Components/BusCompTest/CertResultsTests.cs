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
    public class CertResultTests
    {
        /*		Data		*/
        int id;
        string type = "type";
        string storeCode = "storeCode";
        string coworker = "coworker";
        string name = "name";
        DateTime certDate = DateTime.Now;
        string status = "status";
        
        /*		Constructors		*/
        public CertResultTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            CertResultTests test = new CertResultTests();
            
            // UOW Tests
            test.addCertResult();
            test.findCertResult();
            test.saveCertResult();
            test.findAllCertResultes();
            
            try
            {
                test.delCertResult();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delCertResult:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delCertResult: " + e.Message);
            }
            
        }
        [Test]
        public void addCertResult()
        {
            UOW uow = new UOW();
            uow.Service = "addCertResult";
            CertResult cls = new CertResult(uow);
            
            cls.Type = this.type;
            cls.StoreCode = this.storeCode;
            cls.Coworker = this.coworker;
            cls.Name = this.name;
            cls.CertDate = this.certDate;
            cls.Status = this.status;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addCertResult - assert";
            cls = CertResult.find(uow, this.id);
            Assertion.Assert(cls.StoreCode == this.storeCode);
            uow.close();
        }
        [Test]
        public void findCertResult()
        {
            UOW uow = new UOW();
            uow.Service = "findCertResult";
            
            CertResult cls = CertResult.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveCertResult()
        {
            UOW uow = new UOW();
            uow.Service = "saveCertResult";
            CertResult cls = CertResult.find(uow, this.id);
            
            cls.Type = this.type;
            cls.StoreCode = this.storeCode;
            cls.Coworker = this.coworker;
            cls.Name = this.name;
            cls.CertDate = this.certDate;
            cls.Status = this.status;
            cls.Type += " saved";
            this.type = cls.Type;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveCertResult - assert";
            
            cls = CertResult.find(uow, this.id);
            Assertion.Assert(cls.Type == this.type);
            uow.close();
        }
        [Test]
        public void findAllCertResultes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllCertResultes";
            CertResult[] objs = CertResult.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delCertResult()
        {
            UOW uow = new UOW();
            uow.Service = "delCertResult";
            CertResult cls = CertResult.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delCertResult - assert";
            cls = CertResult.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
