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
    public class DPI_Err_LogTests
    {
        /*		Data		*/
        int id;
        string subsys = "subsys";
        string dPI_User = "dPI_User";
        DateTime dateTime = DateTime.Now;
        string message = "message";
        
        /*		Constructors		*/
        public DPI_Err_LogTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            DPI_Err_LogTests test = new DPI_Err_LogTests();
            
            // UOW Tests
            test.addDPI_Err_Log();
            test.findDPI_Err_Log();
            test.saveDPI_Err_Log();
            test.findAllDPI_Err_Logs();
            
            try
            {
                test.delDPI_Err_Log();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delDPI_Err_Log:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delDPI_Err_Log: " + e.Message);
            }
            
        }
        [Test]
        public void addDPI_Err_Log()
        {
            UOW uow = new UOW();
            uow.Service = "addDPI_Err_Log";
            DPI_Err_Log cls = new DPI_Err_Log(uow);
            
            cls.Subsys = this.subsys;
            cls.DPI_User = this.dPI_User;
            cls.DateTime = this.dateTime;
            cls.Message = this.message;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addDPI_Err_Log - assert";
            cls = DPI_Err_Log.find(uow, this.id);
            Assertion.Assert(cls.DPI_User == this.dPI_User);
            uow.close();
        }
        [Test]
        public void findDPI_Err_Log()
        {
            UOW uow = new UOW();
            uow.Service = "findDPI_Err_Log";
            
            DPI_Err_Log cls = DPI_Err_Log.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveDPI_Err_Log()
        {
            UOW uow = new UOW();
            uow.Service = "saveDPI_Err_Log";
            DPI_Err_Log cls = DPI_Err_Log.find(uow, this.id);
            
            cls.Subsys = this.subsys;
            cls.DPI_User = this.dPI_User;
            cls.DateTime = this.dateTime;
            cls.Message = this.message;
            cls.Subsys += " saved";
            this.subsys = cls.Subsys;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveDPI_Err_Log - assert";
            
            cls = DPI_Err_Log.find(uow, this.id);
            Assertion.Assert(cls.Subsys == this.subsys);
            uow.close();
        }
        [Test]
        public void findAllDPI_Err_Logs()
        {
            UOW uow = new UOW();
            uow.Service = "findAllDPI_Err_Logs";
            DPI_Err_Log[] objs = DPI_Err_Log.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delDPI_Err_Log()
        {
            UOW uow = new UOW();
            uow.Service = "delDPI_Err_Log";
            DPI_Err_Log cls = DPI_Err_Log.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delDPI_Err_Log - assert";
            cls = DPI_Err_Log.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
