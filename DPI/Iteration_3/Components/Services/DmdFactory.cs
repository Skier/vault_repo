using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class DmdFactory
	{
		static string[] dtypes 
			= new string[] {
							   DemandType.New.ToString(), DemandType.NewPymt.ToString(), 
							   DemandType.Monthly.ToString(), DemandType.PriceLU.ToString(), DemandType.DebCardNew.ToString(), 
							   DemandType.DebCardReload.ToString(), DemandType.Reversal.ToString(), DemandType.Internet.ToString(),
							   DemandType.ReversalVoid.ToString(), DemandType.Wireless.ToString(), DemandType.Satellite.ToString(), 
							   DemandType.DpiWireless.ToString()
						   };

		public static IDemand GetDemand(UOW uow, OrderType otype) 
		{
			if (otype == OrderType.New)
				return new Demand(uow, DemandType.New.ToString());

			return new Demand(uow, DemandType.Monthly.ToString());
		}
		public static IDemand GetDemand(UOW uow, string dmdType)
		{
			return new Demand(uow,  ValidateDmdType(dmdType));
		}
		public static IDemand GetDemand(IMap imap, string dmdType, bool marker) 
		{
			IDemand dmd = GetDemand(dmdType);
			imap.add((IMapObj)dmd);
			return dmd;
		}
		public static IDemand GetDemand(string dmdType) 
		{
			Demand dmd = new Demand();
			dmd.DmdType = ValidateDmdType(dmdType);
			return dmd;
		}
		static string ValidateDmdType(string dmdType)
		{
			for (int i = 0; i < dtypes.Length; i++)
				if (dtypes[i].ToLower() == dmdType.Trim().ToLower())
					return dtypes[i];

			throw new ArgumentException("Uknown Demand type: " + dmdType);
		}
	}
}