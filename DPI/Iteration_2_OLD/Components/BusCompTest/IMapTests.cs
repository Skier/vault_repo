using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class IMapTests
	{
		/*		Data		*/

		IdentityMap imap;
		IMapObj[] objs;

		public static void Main()
		{
			IMapTests tests = new IMapTests();
			
			tests.RemoveDomObj();
			tests.load();
			tests.remove();
			tests.findRemoved();
			tests.findKept();
			tests.addDuplicate();
			tests.findDuplicates();
			tests.findDuplicates2();
		}
		[Test]
		public void RemoveDomObj()
		{
			imap = new IdentityMap();
			UOW uow = new UOW(imap);

			try
			{
				DlvTax.getAll(uow);
				Assertion.Assert(imap.Count > 0);
				imap.ClearDomainObjs();

				Assertion.Assert(imap.Count == 0);
			}
			finally
			{
				uow.close();
			}
		}

		[Test]
		public void load()
		{
			imap = new IdentityMap();
			UOW uow = new UOW(imap);
			DlvTax[] dtaxes = DlvTax.getAll(uow);

			Assertion.Assert(dtaxes.Length == imap.Count);
			uow.close();
		}

		[Test]
		public void remove()
		{
			objs = imap.getObjets();
			
			int k, removed = 0;

			for (int i = 0; i < objs.Length; i++)
			{
				Math.DivRem(i, 3, out k);
				if (k == 0)
				{
					imap.remove(objs[i].IKey); 
					removed++;
				}
			}
			Assertion.Assert(objs.Length == imap.Count + removed);
		}
		[Test]
		public void findRemoved()
		{
			if (objs == null)
				return;
			Assertion.AssertNull(imap.find(objs[0].IKey));
		}
		[Test]
		public void findKept()
		{
			if (objs == null)
				return;

			if (objs.Length < 2)
				return;
			DlvTax dtax = (DlvTax)imap.find(objs[1].IKey);
			
			Assertion.Assert(dtax.Id == ((DlvTax)objs[1]).Id);
		}
		[Test]
		public void addDuplicate()
		{
			if (objs.Length < 2)
				return;
			
			int cnt = imap.Count;
			DlvTax dtax = (DlvTax)imap.find(objs[1].IKey);
			imap.add(dtax);

			Assertion.Assert(cnt == imap.Count);
		}
		[Test]
		public void findDuplicates()
		{
			imap.clear();
			UOW uow = new UOW(imap);
			DlvTax[] dtaxes = DlvTax.getAll(uow);
			
			for (int i = 0; i < dtaxes.Length; i++)
				dtaxes[i] = DlvTax.find(uow, dtaxes[i].Id);

			uow.close();		
			Assertion.Assert(imap.Count == imap.Found);
		}
		[Test]
		public void findDuplicates2()
		{
			UOW uow  = null;
	
			imap.clear();
			uow = new UOW(imap);
			DlvTax[] dtaxes = DlvTax.getAll(uow);
	
			//	Console.WriteLine("Find dlvs: " + dtaxes.Length.ToString() );
				
			int k;
			imap.clearCnts();
			

			for (int i = 0; i < dtaxes.Length; i++)
			{
				Math.DivRem(i, 3, out k);
				if (k == 0)
				{
					//			Console.WriteLine("Removing " + i.ToString() );
					imap.remove(objs[i].IKey); 
				}
			}

			for (int i = 0; i < dtaxes.Length; i++)
			{
				//		Console.WriteLine("Second loop " + i.ToString() );
				dtaxes[i] = DlvTax.find(uow, dtaxes[i].Id);
			}
			uow.close();		
			Assertion.Assert(imap.Added == imap.Removed);
		}
	
		DlvTax[] conv(IMapObj[] recs)
		{
			DlvTax[] dtaxes = new DlvTax[recs.Length];
			for (int i = 0; i < dtaxes.Length; i++)
				dtaxes[i] = (DlvTax)recs[i];
			
			return dtaxes;
		}
	}
}