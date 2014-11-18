using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;

namespace DPI.ComponentsTests
{
	[TestFixture]
	public class ProdInfoColTest
	{
		public static void Main()
		{
			ProdInfoColTest t = new ProdInfoColTest();
			t.getPackageAllComp();
			t.getPackageAllComp(); // second time data are already there
			t.getPreReqs();
			Console.Read();
		}
		[Test]
		public void getPackageAllComp()
		{	

			int parent = 165;

			UOW uow = new UOW();

			try 
			{
				ProdComposition[] comps = ProdInfoCol.getAllPackageComps(parent);
				Assertion.Assert(comps.Length > 0);	
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				uow.close();
			}
		}
		[Test]
		public void getPreReqs()
		{	

			int parent = 189;

			UOW uow = new UOW();

			try 
			{
				ProdComposition[][] comps = ProdInfoCol.getTopOnlyPreReqs(parent);
				Assertion.Assert(comps.Length > 0);	
			}
			
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				uow.close();
			}
		}
	}
}