using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;
using DPI.Components;
using DPI.Interfaces;


namespace DPI.ComponentsTests
{
	[TestFixture]
	public class CustInfoExtTests
	{
		/*		Data		*/
		//ICustInfo custInfo;
//		int mailAddID = 3;
//		int accNumber = 5;
		int custInfoID = 11;

		/*		Constructors		*/
		public CustInfoExtTests()
		{
			
		}
        
		/*		Methods		*/
		public static void Main()
		{
			CustInfoExtTests test = new CustInfoExtTests();
            
			// UOW Tests

            
			try
			{
				test.FindCustInfoById();
			}
			catch(ArgumentException ae)
			{
				Console.WriteLine("Expected exception: delCustInfo:" + ae.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Error: delCustInfo: " + e.Message);
			}
            
		}
		
		[Test]
		public void FindCustInfoById()
		{
			UOW uow = new UOW();
			uow.Service = "FindCustInfoById";
            
			CustInfo ci = CustInfo.find(uow, this.custInfoID);
			 
//			CustAddress madr = (CustAddress)CustAddress.find(uow, ci.MailAddID);
//			//IAddr madr = CustAddress.find(uow, ci.MailAddID);
//			// IAddr sadr = CustAddress.find(uow, ci.ServAddID);
//			CustAddress sadr = (CustAddress)CustAddress.find(uow, ci.ServAddID);
			IAddr2 madr = null;
			IAddr2 sadr = null;

			//ICustInfoExt cie = new CustInfoExt(ci, (IMailAddr)madr, sadr);		
			ICustInfoExt cie = new CustInfoExt(ci, madr, sadr);		
		

			Assertion.Assert(ci.CustInfoID == this.custInfoID);
			uow.close();
		}
		
		
		
	}
}
