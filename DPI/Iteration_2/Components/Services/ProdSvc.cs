using System;
using System.Collections;
using DPI.Components;
using DPI.Interfaces;

namespace DPI.Services
{
	public class ProdSvc 
	{
		public static IProdPrice[] GetTopProd(IMap imap, IILECInfo ilec, string zipcode, string role, 
			                                  string storeCode, string criteria) 
		{
			UOW uow = null; 
  
			try
			{
				uow = new UOW(imap, "ProdSvc.GetTopProd"); 

				if (ilec == null)
					throw new ApplicationException("ILEC is required");
				
				IProdPrice[] prods 
					= ProdQual.FilterTopProducts(uow, AvailProds.GetAvailProds(uow, ilec.OrgId, zipcode, role, storeCode));
				
				return ProdQual.FilterRestProds(uow, prods, storeCode, criteria);
			}
			finally
			{	
				imap.ClearDomainObjs();
				uow.close();
			}					
		}
		public static bool ValidZip(IMap imap, string zip)
		{
			UOW uow = null;

			try
			{
				uow = new UOW(imap, "ProdSvc.ValidZip");
				return DmaZip.checkZip(uow, zip);
			}
			finally
			{
				uow.close();
			}
		}
		public static bool ValidZip(IMap imap, int zip)
		{
			UOW uow = null;
			Location loc = null;
			try
			{
				uow = new UOW(imap, "ProdSvc.ValidZip");
				
				try { loc = Location.find(uow, zip); }
				catch (Exception) { return false; }
				
				return DmaZip.checkZip(uow, loc.Name);
			}
			finally
			{
				uow.close();
			}
		}
		public static IProdPrice[] GetDependentProds(IMap imap, IProdPrice basicLine, IILECInfo ilec,
			                                         string zipcode, string role, string storeCode, string dmdType) 
		{
			UOW uow = null;
		
			try
			{
				uow = new UOW(imap, "ProdSvc.GetDependentProds");

				if (basicLine == null)
					throw new ArgumentException("Basic Services is required");

				return ProdProcessor.GetDependentProds(uow, basicLine, ilec, zipcode, role, storeCode, dmdType);
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
			}	
		}
		public static IProdPrice[] AddProd (IMap imap, IProdPrice[] prods, IProdPrice added) 
		{
			UOW uow = null;	
			try
			{
				uow = new UOW(imap, "ProdSvc.AddProd");

				if (added == null)
					throw new ArgumentException("Added product is required");

				return ProdProcessor.AddProd(uow, prods, added);
			}
			finally
			{		
				imap.ClearDomainObjs();
				uow.close();
			}	
		}
		public static IProdPrice[] RemoveProd(IMap imap, IProdPrice[] prods, IProdPrice removed)
		{
			UOW uow = null;	

			try
			{
				uow = new UOW(imap, "ProdSvc.RemoveProd");	
			
				if (removed == null)
					throw new ArgumentException("Removed product is required");

				return ProdProcessor.RemoveProd(uow, prods, removed);
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
			}	
		}
			public static IDmdItem AddDmdItem(IMap imap, IDemand dmd,  IProdPrice pp, OrderType ot)
		{
			UOW uow = null;	
			try
			{
				uow = new UOW(imap, "ProdSvc.AddDmdItem");
				return Demand.BuildIt(uow, (Demand)dmd, pp, ot);
			}
			finally
			{		
				uow.close();
			}
		}

		public static void AddDmdItem(IMap imap, IDemand dmd,  IProdPrice[] pPrice, OrderType ot)
		{
			UOW uow = null;	
			try
			{
				uow = new UOW(imap, "ProdSvc.AddDmdItem");
			
				for (int i =0; i < pPrice.Length; i++)
					Demand.BuildIt(uow, (Demand)dmd, pPrice[i], ot);
			}
			finally
			{		
				uow.close();
			}
		}
	
		public static void TaxWrapperDispose()
		{
			TaxWrapper.Dispose();
		}
		public static string ValidateOrder(IMap imap, IProdPrice[] pPrice, string dmdType)
		{
			UOW uow = null;	

			try
			{
				uow = new UOW(imap, "ProdSvc.ValidateOrder");	

				return ProdProcessor.ValidateOrder(uow, pPrice, dmdType);
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
			}	
		}
		
		public static IKeyVal[] GetAllProdDiscounts(IMap imap, string orderType)
		{
			UOW uow = null;	

			try
			{
				uow = new UOW(imap, "ProdSvc.GetAllProdDiscounts");	

				return ProdPrice.GetAllProdDiscounts(uow, orderType);
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
			}	
		}
		
		public static IDmdTax[] ComputeTax(IMap imap, int prod, decimal priceAmt, string zip, DateTime date)
		{
			UOW uow = null;
			IBillSoftTax btax = null;

			try
			{
				uow = new UOW(imap, "ProdSvc.ComputeTax");	

				btax = new BillSoftTax(uow);
				
				return btax.ComputeTax(uow, prod, priceAmt, zip, date);				
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
				
				if (btax != null)
					btax.ReleaseSession();
			}
		}
		public static ISource[] GetSources(IMap imap)

		{
			UOW uow = null;	

			try
			{
				uow = new UOW(imap, "ProdSvc.GetSources");	

				return  SourceCode.AddEmpty(SourceCode.getActive(uow));
			}
			finally
			{
				imap.ClearDomainObjs();
				uow.close();
			}	
		}
	}
}