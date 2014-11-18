using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
    [TestFixture]
    public class TempAutologinTests
    {
        /*		Data		*/
        int id = 1;
        string acctName = "acctName";
        string pW = "pW";
        
        /*		Constructors		*/
        public TempAutologinTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            TempAutologinTests test = new TempAutologinTests();
            
            // UOW Tests
            test.addTempAutologin();
            test.findTempAutologin();
            test.saveTempAutologin();
            test.findAllTempAutologins();
            
            try
            {
                test.delTempAutologin();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delTempAutologin:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delTempAutologin: " + e.Message);
            }
            
        }
        [Test]
        public void addTempAutologin()
        {
            UOW uow = new UOW();
            uow.Service = "addTempAutologin";
            TempAutologin cls = new TempAutologin(uow, this.acctName,  this.pW);
            
            uow.commit();
            this.id = cls.Id;

            uow = new UOW();
            uow.Service = "addTempAutologin - assert";
            cls = TempAutologin.find(uow, this.id);
            Assertion.Assert(cls.PW == this.pW);
            uow.close();
        }
        [Test]
        public void findTempAutologin()
        {
            UOW uow = new UOW();
            uow.Service = "findTempAutologin";
            
            TempAutologin cls = TempAutologin.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveTempAutologin()
        {
            UOW uow = new UOW();
            uow.Service = "saveTempAutologin";
            TempAutologin cls = TempAutologin.find(uow, this.id);
            
            cls.Id = this.id;
            cls.AcctName = this.acctName;
            cls.PW = this.pW;
            cls.AcctName += " saved";
            this.acctName = cls.AcctName;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveTempAutologin - assert";
            
            cls = TempAutologin.find(uow, this.id);
            Assertion.Assert(cls.AcctName == this.acctName);
            uow.close();
        }
        [Test]
        public void findAllTempAutologins()
        {
            UOW uow = new UOW();
            uow.Service = "findAllTempAutologins";
            TempAutologin[] objs = TempAutologin.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delTempAutologin()
        {
            UOW uow = new UOW();
            uow.Service = "delTempAutologin";
            TempAutologin cls = TempAutologin.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delTempAutologin - assert";
            cls = TempAutologin.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
