using System;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
	public class TaxFactory
	{
		public static ITaxSvc getTaxSvc(string version)
		{

//			switch(version)
//			{
//				case "V" :
					return new TaxSvc();
//				
//				default :
//					return new TaxSvc();
//			}
		}
		public static IChargeDto getChargeDto()
		{
			return new ChargeDto();
		}
	}
}