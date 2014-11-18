using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class AddressSvc
	{
		public static IAddress_StreetType[] GetStreetTypes(IMap imap)
		{
			UOW uow = null;
	
			try
			{
				uow = new UOW(imap); 	
				uow.Service = "AddressSvc.GetStreetTypes()";
				IAddress_StreetType[] addressStreetTypes = Address_StreetType.getAll(uow);
				return addressStreetTypes;
			}
			finally
			{	
				uow.close();
				imap.ClearDomainObjs();	
			}
		}
	}
}