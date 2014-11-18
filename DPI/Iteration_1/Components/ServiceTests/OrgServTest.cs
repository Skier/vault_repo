using System;
using NUnit.Framework;
using DPI.Interfaces;
using DPI.Components;
using DPI.Services;

namespace DPI.Services
{
	[TestFixture]
	public class OrgServTest
	{
		/*		Data		*/
		
		/*		Constructors	*/
		public OrgServTest()
		{
		}
		/*		Methods		*/
		public static void Main()
		{
			OrgServTest test = new OrgServTest();
			test.getILECSatZip();
		}
		[Test]
		public void getILECSatZip()
		{
		}
	}
}