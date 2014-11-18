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
	public class DebitCardFactory
	{
		public static IDebitCardResponse GetRespObj(IDemand dmd, XmlNode node)
		{
			int vendor = GetProviderId(dmd);
			switch(vendor)
			{
				case 25 :
					return new PurposeDCResponse(node);
			}	
			throw new ArgumentException("Debit Card product Unknown Debit Card Provider id: " + vendor.ToString());

		}
		public static IDebCardProvider GetProvider(IDemand dmd)
		{
			int vendor = GetProviderId(dmd);
			switch(vendor)
			{
				case 25 :
					return new PurposeDebitCard();
			}	
			throw new ArgumentException("Debit Card product Unknown Debit Card Provider id: " + vendor.ToString());
		}
		public static ICardApp GetCardApp(IMap imap, IDemand dmd)
		{
			int vendor = GetProviderId(dmd);
			switch(vendor)
			{
				case 25 :
					return new CardApp(imap, dmd);
			}	
			throw new ArgumentException("Debit Card product Unknown Debit Card Provider id: " + vendor.ToString());
		}
		public static int GetProviderId(IDemand dmd)
		{
			return ProdInfoCol.GetProd((dmd.GetDmdItems()[0].Prod)).Vendor;
		}
	}
}