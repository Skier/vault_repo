using System;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class CustFactory
	{
		public static IAddr2 GetAddress(IMap imap) 
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap);
				return new CustAddress(uow);
			}
			finally
			{
				uow.close();
			}
		}
		public static ICustInfo2 GetCustInfo(IMap imap) 
		{
			UOW uow = null;
			try
			{
				uow = new UOW(imap);
				return new CustInfo(uow);
			}
			finally
			{
				uow.close();
			}
		}
	}
}