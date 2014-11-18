using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Services
{
	public class PostPymtFactory
	{
		public static IPymtPostProvider GetProvider(string storeCode)
		{
			if (Corporation.IsRentway(storeCode))
				return new RentwayWSGateway();

			throw new ArgumentException("No PymtPost provider for Store Code: " + storeCode);

		}
	}
}