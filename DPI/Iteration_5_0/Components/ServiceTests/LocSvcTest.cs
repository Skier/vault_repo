using System;
using NUnit.Framework;

using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class LocSvcTest
	{
		/*		Data		*/
		IMap imap;
		
		/*		Constructors	*/
		public LocSvcTest()
		{
		}
		/*		Methods		*/
		public static void Main()
		{
			LocSvcTest test = new LocSvcTest(); 
			test.GetState();
		}
		[Test]
		public void GetState()
		{
			string zip = "75001";
			imap = new IdentityMap();
			string state = LocSvc.FindState(imap,zip);
			Assertion.AssertNotNull(state);
		}
	}
}