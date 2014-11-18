using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class VendorSvc
	{
		public static bool IsNpaNxxReq(IMap imap, string id)
		{
			UOW uow = null;
			try
			{
				
				uow = new UOW(imap, "VendorSvc.IsNpaNxxReq");

				if (!ValidateVendor(id))
					return false;

				return Vendors.find(uow, int.Parse(id)).IsNpaNxxReq;		
			}
			finally
			{
				uow.close();
			}
		}
		static bool ValidateVendor(string id)
		{
			if (id == null)
				return false;

			if (id.Trim().Length == 0)
				return false;

			return true;
		}
		public static IVendors[] GetNpaNxxVendors(IMap imap)
		{
			UOW uow = null;
			try
			{
				
				uow = new UOW(imap, "VendorSvc.GetNpaNxxVendors");

				return Vendors.GetNpaNxxVendors(uow);		
			}
			finally
			{
				uow.close();
			}
		}
	}
}