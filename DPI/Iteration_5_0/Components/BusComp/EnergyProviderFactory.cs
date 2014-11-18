using System;

using DPI.Components;
using DPI.Components.EPSolutions;
using DPI.Interfaces;

namespace DPI.Components
{
	public class EnergyProviderFactory
	{
		public static EnergyProvider GetProvider(UOW uow, IWireless_Products wp)
		{
			IVendors ven =  Vendors.find(uow, wp.Vendor_id);
			
			if (wp.OverrideWSProvider == null)		
				return GetGateway(ven.DefaultWSProvider);
			
			if (wp.OverrideWSProvider.Trim().Length == 0)
				return GetGateway(ven.DefaultWSProvider);

			return GetGateway(wp.OverrideWSProvider);
		}
		public static EnergyProvider GetProvider(UOW uow, int vendorId)
		{
			IVendors ven =  Vendors.find(uow, vendorId);
			
			return GetGateway(ven.DefaultWSProvider);
		}
		public static EnergyProvider GetProvider(UOW uow, IVendors vendor)
		{
			return GetGateway(vendor.DefaultWSProvider);
		}
		public static EnergyProvider GetGateway(string provider)
		{	
			if (provider == null)
				throw new ApplicationException("Product Provider is missing");
			
			switch (provider.Trim().ToLower())
			{
				case Const.EPSOLUTIONS :
					return new EPSolutionsWSGateway(Const.EPSOLUTIONS);

				default :
					throw new ArgumentException("Uknown Product web services provider: " + provider);
			}
		}
	}
}