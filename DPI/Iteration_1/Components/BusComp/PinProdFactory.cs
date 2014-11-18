using System;

using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class WSProviderFactory 
	{
		public static bool HasWSProvider(UOW uow, IWireless_Products wp)
		{
			if (HasOverrideWSProvider(uow, wp))
				return true;

			return HasDefaultWSProvider(uow, wp);

		}
		static bool HasOverrideWSProvider(UOW uow, IWireless_Products wp)
		{	
			if (wp.OverrideWSProvider == null)
				return false;

			return wp.OverrideWSProvider.Trim().Length > 0;
		}

		static bool HasDefaultWSProvider(UOW uow, IWireless_Products wp)
		{	
			IVendors ven =  Vendors.find(uow, wp.Vendor_id);

			if (ven.DefaultWSProvider == null)
				return false;

			return ven.DefaultWSProvider.Trim().Length > 0;
		}
		public static IWebSvcProvider GetProvider(UOW uow, IWireless_Products wp)
		{
			IVendors ven =  Vendors.find(uow, wp.Vendor_id);
			
			if (wp.OverrideWSProvider == null)		
				return GetGateway(ven.DefaultWSProvider);
			
			if (wp.OverrideWSProvider.Trim().Length == 0)
				return GetGateway(ven.DefaultWSProvider);

			return GetGateway(wp.OverrideWSProvider);
		}
		public static IWebSvcProvider GetProvider(UOW uow, IPinProduct prod)
		{
			IWireless_Products wp = Wireless_Products.find(uow, prod.Product_Id);
			IVendors ven =  Vendors.find(uow, wp.Vendor_id);
			
			if (wp.OverrideWSProvider == null)		
				return GetGateway(ven.DefaultWSProvider);
			
			if (wp.OverrideWSProvider.Trim().Length == 0)
				return GetGateway(ven.DefaultWSProvider);

			return GetGateway(wp.OverrideWSProvider);
		}
		public static IWebSvcProvider GetProvider(UOW uow, IProdInfo prod)
		{
			return GetGateway(Vendors.find(uow, prod.Vendor).DefaultWSProvider);
		}
		public static IWebSvcProvider GetProvider(UOW uow, int vendorId)
		{
			IVendors ven =  Vendors.find(uow, vendorId);
			
			return GetGateway(ven.DefaultWSProvider);
		}
		public static IWebSvcProvider GetGateway(string provider)
		{	
			if (provider == null)
				throw new ApplicationException("Pin Product Provider is missing");
			
			switch (provider.Trim().ToLower())
			{
				case Const.SLINGSHOT : 
					return new SlingShotWSGateway(Const.SLINGSHOT);

				case Const.DPI_COLDFUSION :
					return new ColdFusionWSGateway(Const.DPI_COLDFUSION);

				case Const.INFINITY_MOBILE  :
					return new InfinityMobileWSGateway(Const.INFINITY_MOBILE);
				
				case Const.PRESOLUTIONS :
					return new PreSolutionsWSGateway(Const.PRESOLUTIONS);

				case Const.PHONETEC :
					return new PhoneTecWSGateway(Const.PHONETEC);
				
				default :
					throw new ArgumentException("Uknown Pin Product web services provider: " + provider);
			}
		}
	}
}