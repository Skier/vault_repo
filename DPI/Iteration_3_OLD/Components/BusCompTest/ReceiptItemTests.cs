using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.ComponentsTests
{
    [TestFixture]
    public class ReceiptItemTests
    {
        /*		Data		*/
        int id;
        int receiptId = 1;
        string itemType = "itemType";
        string itemOrder = "itemOrder";
        string text = "text";
        string overrideFontFamilty = "overrideFontFamilty";
        string overrideFontStyle = "overrideFontStyle";
        int overrideFontSize = 8;
        
        /*		Constructors		*/
        public ReceiptItemTests()
        {
            // try { cleanup(); } 	catch {}
            // Console.WriteLine("Cleanup completed");
        }
        
        /*		Methods		*/
        public static void Main()
        {
            ReceiptItemTests test = new ReceiptItemTests();
			test. GetBody();            
            // UOW Tests
/*
			test.addReceiptItem();
            test.findReceiptItem();

        test.saveReceiptItem();
            test.findAllReceiptItems();
            
            try
            {
                test.delReceiptItem();
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine("Expected exception: delReceiptItem:" + ae.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: delReceiptItem: " + e.Message);
            }
      */   
        }
		[Test]
		public void GetBody()
		{
			UOW uow = new UOW();

			int rId = 1;
			Receipt2 rct = (Receipt2)Receipt2.find(uow, rId);
			IReceiptItem[] b1 = rct.GetFirst(ReceiptItemType.Body );
			IReceiptItem[] b2 = rct.GetNext(b1[0]);
			IReceiptItem[] b3 = rct.GetNext(b2[0]);
			IReceiptItem[] b4 = rct.GetNext(b3[0]);

			for (int i = 0; i < b1.Length; i++)
				if (b1[i].ItemGroup != b1[0].ItemGroup)
					Console.WriteLine("Multiple groups are not allowed");

			if (b2[0].ItemGroup == b1[0].ItemGroup)
				Console.WriteLine("Groups with the same group number are disallowed");

			for (int i = 0; i < b2.Length; i++)
			{
				if (b2[i].ItemGroup != b2[0].ItemGroup)
					Console.WriteLine("Multiple groups are not allowed");

			}
		}
		
		[Test]
        public void addReceiptItem()
        {
            UOW uow = new UOW();
            uow.Service = "addReceiptItem";
            ReceiptItem cls = new ReceiptItem(uow);
            
            cls.ReceiptId = this.receiptId;
            cls.ItemType = this.itemType;
            cls.ItemOrder = this.itemOrder;
            cls.Text = this.text;
            cls.OverrideFontFamilty = this.overrideFontFamilty;
            cls.OverrideFontStyle = this.overrideFontStyle;
            cls.OverrideFontSize = this.overrideFontSize;
        
            uow.commit();
            this.id = cls.Id;
            
            uow = new UOW();
            uow.Service = "addReceiptItem - assert";
            cls = ReceiptItem.find(uow, this.id);
            Assertion.Assert(cls.ItemType == this.itemType);
            uow.close();
        }
        [Test]
        public void findReceiptItem()
        {
            UOW uow = new UOW();
            uow.Service = "findReceiptItem";
            
            ReceiptItem cls = ReceiptItem.find(uow, this.id);
            Assertion.Assert(cls.Id == this.id);
            uow.close();
        }
        [Test]
        public void saveReceiptItem()
        {
            UOW uow = new UOW();
            uow.Service = "saveReceiptItem";
            ReceiptItem cls = ReceiptItem.find(uow, this.id);
            
            cls.ReceiptId = this.receiptId;
            cls.ItemType = this.itemType;
            cls.ItemOrder = this.itemOrder;
            cls.Text = this.text;
            cls.OverrideFontFamilty = this.overrideFontFamilty;
            cls.OverrideFontStyle = this.overrideFontStyle;
            cls.OverrideFontSize = this.overrideFontSize;
            cls.ReceiptId += 2;
            this.receiptId = cls.ReceiptId;
                
            uow.commit();
            
            uow = new UOW();
            uow.Service = "saveReceiptItem - assert";
            
            cls = ReceiptItem.find(uow, this.id);
            Assertion.Assert(cls.ReceiptId == this.receiptId);
            uow.close();
        }
        [Test]
        public void findAllReceiptItems()
        {
            UOW uow = new UOW();
            uow.Service = "findAllReceiptItems";
            ReceiptItem[] objs = ReceiptItem.getAll(uow);
            Assertion.Assert(objs.Length > 0);
            uow.close();
        }
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void delReceiptItem()
        {
            UOW uow = new UOW();
            uow.Service = "delReceiptItem";
            ReceiptItem cls = ReceiptItem.find(uow, this.id);
            cls.delete();
            
            uow.commit();
            
            uow = new UOW();
            uow.Service = "delReceiptItem - assert";
            cls = ReceiptItem.find(uow, this.id);
            Assertion.Assert((cls.Id == 0));
            uow.close();
        }
    }
}
