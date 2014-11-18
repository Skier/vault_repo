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
    public class PermissionTests
    {
        /*		Data		*/
        string permsName = "permsName";
        string description = "description";
        
        /*		Constructors		*/
        public PermissionTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            PermissionTests test = new PermissionTests();
            
            // UOW Tests
            test.addPermission();
            test.findPermission();
            test.savePermission();
            test.findAllPermissions();
            
            try
            {
                test.delPermission();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delPermission:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delPermission: " + e.Message);
            }
            
        }
        [Test]
        public void addPermission()
        {
            UOW uow = new UOW();
            uow.Service = "addPermission";
            Permission cls = new Permission(uow);
            
            cls.PermsName = this.permsName;
            cls.Description = this.description;
        
            uow.commit();
            
            uow = new UOW();
            uow.Service = "addPermission - assert";
            cls = Permission.find(uow, this.permsName);
            Assertion.Assert(cls.Description == this.description);
            uow.close();
        }
        [Test]
        public void findPermission()
        {
            UOW uow = new UOW();
            uow.Service = "findPermission";
            
            Permission cls = Permission.find(uow, this.permsName);
            Assertion.Assert(cls.PermsName.Trim() == this.permsName.Trim());
            uow.close();
        }
        [Test]
        public void savePermission()
        {
            UOW uow = new UOW();
            uow.Service = "savePermission";
            Permission cls = Permission.find(uow, this.permsName);
            
            cls.PermsName = this.permsName;
            cls.Description = this.description;
            cls.Description += " saved";
            this.description = cls.Description;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "savePermission - assert";
            
            cls = Permission.find(uow, this.permsName);
            Assertion.Assert(cls.Description == this.description);
            uow.close();
        }
        [Test]
        public void findAllPermissions()
        {
            UOW uow = new UOW();
            uow.Service = "findAllPermissions";
            Permission[] objs = Permission.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delPermission()
        {
            UOW uow = new UOW();
            uow.Service = "delPermission";
            Permission cls = Permission.find(uow, this.permsName);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delPermission - assert";
            cls = Permission.find(uow, this.permsName);
            Assertion.Assert((cls.PermsName ==  null));
            uow.close();
        }
    }
}
