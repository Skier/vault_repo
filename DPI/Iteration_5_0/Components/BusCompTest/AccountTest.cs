using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Components;
using DPI.Interfaces;
using NUnit.Framework;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class AccountTest
	{
		/*		Methods		*/
		public static void Main()
		{
			AccountTest test = new AccountTest();
            
			test.FindByPhone();
			test.FindByPhone2();
			test.GetBalance();
			test.IsActivePhone();
			test.IsActivePhoneNotFound();
			test.IsActiveAccNum();
			test.IsActiveAccNumNotFound();
		}

		[Test]
		public void GetBalance()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "GetBalance";
				Account acct = new Account(uow, 50343975);
				Assertion.Assert(acct.AccountInfo.IsActive == false);
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void FindByPhone()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "FindByPhone";
				string phone = "9721111111";
				Assertion.Assert(Account.FindByPhNumber(uow, phone) == 0);
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void FindByPhone2()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "FindByPhone";
				string phone = "8433950162";
				Assertion.Assert(Account.FindByPhNumber(uow, phone) > 0);	
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void IsActivePhone()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "IsActive";
				string phone = "8433950162";
				Assertion.Assert(Account.IsActiveAccount(uow, phone));	
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void IsActivePhoneNotFound()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "IsActive";
				string phone = "891919191";
				Assertion.Assert(!Account.IsActiveAccount(uow, phone));	
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void IsActiveAccNum()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "IsActive";
				int acct = 50343979;
				Assertion.Assert(Account.IsActiveAccount(uow, acct));
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void IsActiveAccNumNotFound()
		{
			UOW uow = new UOW();
			try
			{
				uow.Service = "IsActive";
				int acct = 555;
				Assertion.Assert(!Account.IsActiveAccount(uow, acct));
			}
			finally
			{
				uow.close();
			}
		}
	}
}			