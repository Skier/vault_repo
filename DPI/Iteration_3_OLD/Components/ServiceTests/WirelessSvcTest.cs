using System;
using System.Collections;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;
using DPI.ClientComp;
using DPI.Services;

namespace DPI.Services
{
	/// <summary>
	/// Summary description for WirelessSvcTest.
	/// </summary>
	[TestFixture]
	public class WirelessSvcTest
	{
		public WirelessSvcTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public static void Main()
		{
			WirelessSvcTest test = new WirelessSvcTest(); 
			test.testGetVendorList();
			test.testGetProductList();

			Console.ReadLine();
		}

		[Test]
		public void testGetVendorList()
		{		
			

			DictionaryEntry[] vendorlist = WirelessSvc.GetWirelessVendors(LoginSvc.GetUser("Barbara"));
			for (int x=0; x<vendorlist.Length; x++)
			{
				Console.WriteLine("{0} - {1}", vendorlist[x].Key, vendorlist[x].Value);
			}
		}

		[Test]
		public void testGetProductList()
		{		

			WirelessProduct[] prodlist=WirelessSvc.GetProducts("TXHC1136RW", "123", "121");
			for (int x=0; x<prodlist.Length; x++)
			{
				Console.WriteLine("{0} : {1} = {2}", prodlist[x].wireless_product_id, prodlist[x].product_name, prodlist[x].price);
			}
		}
	
		// method included just to test compile.  don't run!  No test system at PreSol.
		private void testOrderProduct()
		{
			if (false)  // make sure this doesn't run!
			{			

				WirelessOrderResult[] outlist = WirelessSvc.OrderProduct("TXHC1136RW", "123", "1001", "123");
				for (int x=0; x<outlist.Length; x++)
				{
					Console.WriteLine("Pin={0}  {1}.  commission= {2}", outlist[x].pin, outlist[x].receipt_text, outlist[x].commission);
				}
			}
		}		
	}	
}
