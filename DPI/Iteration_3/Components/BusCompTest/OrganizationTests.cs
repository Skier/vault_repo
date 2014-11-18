using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class OrganizationTests
    {
        /*		Data		*/
        int id;
        string name = "name";
        bool isSupplier = true;
        bool isVendor = true;
        int parent = 5;
        string addrLine1 = "addrLine1";
        string addrLine2 = "addrLine2";
        string city = "city";
        int state = 9;
        int zip = 10;
        string phone = "phone";
        string fax = "fax";
        string uRL = "uRL";
        string contact = "contact";
        string connType = "connType";
        bool isBillableAllowed = true;
        bool isILEC = true;
        
        /*		Constructors		*/
        public OrganizationTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            OrganizationTests test = new OrganizationTests();
            
			Organization[] orgs = Organization.GetILEC_ByCode(new UOW(), "SWB");
				
            // UOW Tests
            test.addOrganization();
            test.findOrganization();
            test.saveOrganization();
            test.findAllOrganizations();
            
            try
            {
                test.delOrganization();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delOrganization:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delOrganization: " + e.Message);
            }
            
        }
        [Test]
        public void addOrganization()
        {
            UOW uow = new UOW();
            uow.Service = "addOrganization";
            Organization cls = new Organization(uow);
            
            cls.Name = this.name;
            cls.IsSupplier = this.isSupplier;
            cls.IsVendor = this.isVendor;
            cls.Parent = this.parent;
            cls.AddrLine1 = this.addrLine1;
            cls.AddrLine2 = this.addrLine2;
            cls.City = this.city;
            cls.State = this.state;
            cls.Zip = this.zip;
            cls.Phone = this.phone;
            cls.Fax = this.fax;
            cls.URL = this.uRL;
            cls.Contact = this.contact;
            cls.ConnType = this.connType;
            cls.IsBillableAllowed = this.isBillableAllowed;
            cls.IsILEC = this.isILEC;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addOrganization - assert";
            cls = Organization.find(uow, this.id);
            Assertion.Assert(cls.IsSupplier == this.isSupplier);
            uow.close();
        }
        [Test]
        public void findOrganization()
        {
            UOW uow = new UOW();
            uow.Service = "findOrganization";
            
            Organization cls = Organization.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveOrganization()
        {
            UOW uow = new UOW();
            uow.Service = "saveOrganization";
            Organization cls = Organization.find(uow, this.id);
            
            cls.Name = this.name;
            cls.IsSupplier = this.isSupplier;
            cls.IsVendor = this.isVendor;
            cls.Parent = this.parent;
            cls.AddrLine1 = this.addrLine1;
            cls.AddrLine2 = this.addrLine2;
            cls.City = this.city;
            cls.State = this.state;
            cls.Zip = this.zip;
            cls.Phone = this.phone;
            cls.Fax = this.fax;
            cls.URL = this.uRL;
            cls.Contact = this.contact;
            cls.ConnType = this.connType;
            cls.IsBillableAllowed = this.isBillableAllowed;
            cls.IsILEC = this.isILEC;
            cls.Name += " saved";
            this.name = cls.Name;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveOrganization - assert";
            
            cls = Organization.find(uow, this.id);
            Assertion.Assert(cls.Name == this.name);
            uow.close();
        }
        [Test]
        public void findAllOrganizations()
        {
            UOW uow = new UOW();
            uow.Service = "findAllOrganizations";
            Organization[] objs = Organization.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delOrganization()
        {
            UOW uow = new UOW();
            uow.Service = "delOrganization";
            Organization cls = Organization.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delOrganization - assert";
            cls = Organization.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
