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
    public class SourceCodeTests
    {
        /*		Data		*/
        int id;
        int sortOrder = 2;
        string source = "source";
        string description = "description";
        string status = "status";
        DateTime startDate = DateTime.Now;
        DateTime endDate = DateTime.Now;
        
        /*		Constructors		*/
        public SourceCodeTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            SourceCodeTests test = new SourceCodeTests();
            
            // UOW Tests
            test.addSourceCode();
            test.findSourceCode();
            test.saveSourceCode();
            test.findAllSourceCodes();
            
            try
            {
                test.delSourceCode();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delSourceCode:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delSourceCode: " + e.Message);
            }
            
        }
        [Test]
        public void addSourceCode()
        {
            UOW uow = new UOW();
            uow.Service = "addSourceCode";
            SourceCode cls = new SourceCode(uow);
            
            cls.SortOrder = this.sortOrder;
            cls.Source = this.source;
            cls.Description = this.description;
            cls.Status = this.status;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addSourceCode - assert";
            cls = SourceCode.find(uow, this.id);
            Assertion.Assert(cls.Source == this.source);
            uow.close();
        }
        [Test]
        public void findSourceCode()
        {
            UOW uow = new UOW();
            uow.Service = "findSourceCode";
            
            SourceCode cls = SourceCode.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveSourceCode()
        {
            UOW uow = new UOW();
            uow.Service = "saveSourceCode";
            SourceCode cls = SourceCode.find(uow, this.id);
            
            cls.SortOrder = this.sortOrder;
            cls.Source = this.source;
            cls.Description = this.description;
            cls.Status = this.status;
            cls.StartDate = this.startDate;
            cls.EndDate = this.endDate;
            cls.SortOrder += 2;
            this.sortOrder = cls.SortOrder;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveSourceCode - assert";
            
            cls = SourceCode.find(uow, this.id);
            Assertion.Assert(cls.SortOrder == this.sortOrder);
            uow.close();
        }
        [Test]
        public void findAllSourceCodes()
        {
            UOW uow = new UOW();
            uow.Service = "findAllSourceCodes";
            SourceCode[] objs = SourceCode.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delSourceCode()
        {
            UOW uow = new UOW();
            uow.Service = "delSourceCode";
            SourceCode cls = SourceCode.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delSourceCode - assert";
            cls = SourceCode.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
