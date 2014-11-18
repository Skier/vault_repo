using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;

namespace DPI.Components
{
	public class ProdQual
	{
	#region Methods
		public static bool ValidateBeforeAdd(UOW uow, IProdPrice[] prods, IProdPrice toBeAdded)
		{
			for(int i = 0; i < prods.Length; i++)
				if (((ProdPrice)prods[i]).ProdId == toBeAdded.ProdId)
				{
					((ProdPrice)prods[i]).ProdSelState = ProdSelectionState.Available;// make available
					break;
				}

			IProdPrice[] products = ProdQual.SuppressZeroPrice(uow,  
				ProdQual.FilterLockedUnavail(uow,
				ProdQual.FilterVisible(uow, 
				ProdQual.SetupAvailState(uow, (ProdPrice[])prods)))); 

			for(int i = 0; i < products.Length; i++)
				if (((ProdPrice)products[i]).ProdId == toBeAdded.ProdId)
					if (((ProdPrice)prods[i]).ProdSelState == ProdSelectionState.Unavailable) //did not pass validation
						return false;	

			return true; 
		}

		public static IProdPrice[] FilterRestProds(UOW uow, IProdPrice[] prods, string storeCode, string criteria)
		{
			RestrictedProdRule[] rules = GetRestRiles(uow, storeCode, criteria);
			ArrayList ar = new ArrayList(prods.Length);

			for (int i = 0; i < prods.Length; i++)
			{
				if (!Product.AllowRestrictedProd(uow, rules, prods[i].ProdId))
					continue;

				ar.Add(prods[i]);
			}
			return ConvertToPP(ar);
		}

	#endregion
	#region Rules	
		public static ProdFilter[] RemLockedUnavailRules(UOW uow)
		{
			return new ProdFilter[] { new RemoveLockedUnavailable(uow) };
		}
		public static ProdFilter[] AfterUnselectRules(UOW uow)
		{
			return new ProdFilter[] 
				{ new MakeAvailable(uow), new DuplicateSubClass(uow), new DuplicateProduct2(uow) };
		}		
		public static ProdFilter[] GetProvCatRules(UOW uow, string provCat) // remove products from "wrong" prov category
		{
			return new ProdFilter[] { new ProvCategoryProds(uow, provCat) };
		}
		public static ProdFilter[] GetLockRules(UOW uow, int selTopProd) // locks top products and marks dependent as availble
		{
			return new ProdFilter[] 
			{  
				new RemoveUnselectedTopProducts(uow, selTopProd), 
				new LockTopProducts(uow), 
				new EnableNonTopProducts(uow),
				new RemoveProdCategoryTopProducts(uow, selTopProd)
			};
		}
		public static ProdFilter[] GetPreReqRules(UOW uow) // only visible basic service prods/packages
		{
			return new ProdFilter[] { new PreReqRecursive(uow), new PreReqTopOnly(uow)}; 
		}
		public static ProdFilter[] GetTopProductsRules(UOW uow) // only visible basic service prods/packages
		{
			return new ProdFilter[] { new TopProducts(uow)};//, new VisibleProducts(uow) }; 
		}
		public static ProdFilter[] GetVisibleProdRules(UOW uow)
		{
			return new ProdFilter[] { new VisibleProducts(uow) };
		}
		public static ProdFilter[] GetProdAvailRules(UOW uow)
		{
			return new ProdFilter[]	{ new DuplicateSubClass(uow), new DuplicateProduct2(uow) };
		}
		public static ProdFilter[] PreSelectedProdsRules(UOW uow)
		{
			return new ProdFilter[]	{ new PreSelectedProds(uow) };
		}
		public static ProdFilter[] GetSuppressZeroPrice(UOW uow)
		{
			return new ProdFilter[]  
			{ 
                new SuppressZeroPrice(uow),     // product option
				new SuppressOnWebReceipt(uow),  // subclass option
				new SuppressZeroPriceProd(uow)  // subclass option 
			};
		}
		public static ProdFilter[] GetUnselectCurrentRules(UOW uow, IProdPrice selected)
		{
			return new ProdFilter[]  { new UnselectCurrent(uow, selected) };
		}

//GetUnselectCurrentRules
	#endregion
	#region Filters

		public static ProdPrice[] CheckPreReq(UOW uow, ProdPrice[] products)
		{
			if (products == null)
				throw new ArgumentException("Products cannot be null");

			if (products.Length == 0)
				return products;

			ProdPrice[] candidates = new ProdPrice[products.Length];
			products.CopyTo(candidates, 0);

			ProdFilter[] rules = ProdQual.GetPreReqRules(uow);	
			for(int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates);
			
			return candidates;
		}
		
		public static ProdPrice[] GetDependentProds(UOW uow, int ilec, string zipcode, ProdPrice selected, string role, string storeCode)
		{
			if (selected == null)
				throw new ArgumentException("Local Service is required");

			if (ilec == 0)
				throw new ArgumentException("ILEC is required");

			if (zipcode == null)
				throw new ArgumentException("Zipcode is required");

			ProdPrice[] candidates = ProdQual.RemoveDuplicates(AvailProds.GetAvailProds(uow, ilec, zipcode, role, storeCode)); 

			ProdFilter[] rules = new ProdFilter[] 
				{  
					new DuplicateProduct2(uow),
					new VisibleProducts(uow),  
					new DuplicateSubClass(uow),
					new RemoveLockedUnavailable(uow), 
					new EnableNonTopProducts(uow), 
					new ProvCategoryProds(uow, selected.ProvCategory) 
				};		

			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}
		public static ProdPrice[] FilterBySelectedTopProduct(UOW uow, ProdPrice[] prods, ProdPrice selected)
		{
			if (prods == null)
				throw new ArgumentException("Products cannot be null");

			if (selected == null)
				throw new ArgumentException("Selected product cannot be null");

			if (prods.Length == 0)
				return prods;

			ProdPrice[] candidates = new ProdPrice[prods.Length];
			prods.CopyTo(candidates, 0);

			ProdFilter[] rules = ProdQual.GetProvCatRules(uow, ProdInfoCol.GetProd(selected.ProdId).ProvCategory);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
		
			rules = ProdQual.GetLockRules(uow, selected.ProdId);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 

			rules = ProdQual.GetPreReqRules(uow);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 

			return candidates;
		}
		public static ProdPrice[] FilterAfterUnselect(UOW uow, ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Products cannot be null");

			if (prods.Length == 0)
				return prods;

			ProdPrice[] candidates = new ProdPrice[prods.Length];
			prods.CopyTo(candidates, 0);

			ProdFilter[] rules = ProdQual.AfterUnselectRules(uow);

			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
		
			return candidates;
		}
		public static ProdPrice[] DmdFilter(ProdPrice[] products, string dmdType)
		{
			if (products == null)
				throw new ArgumentException("Products cannot be null");

			if (products.Length == 0)
				return products;
	
			ArrayList ar = new ArrayList(products.Length);

			for (int i = 0; i < products.Length; i++)
				if (!DmdProdTypeCol.IsExcluded(products[i].ProdType, dmdType))
					ar.Add(products[i]);

			ProdPrice[] candidates = new ProdPrice[ar.Count];	
			for (int i = 0; i < candidates.Length; i++)
				candidates[i] = (ProdPrice)ar[i];

			return candidates;
		}
		public static ProdPrice[] RemoveDuplicates(ProdPrice[] products)
		{
			if (products == null)
				throw new ArgumentException("Products cannot be null");

			if (products.Length == 0)
				return products;
	
			ArrayList ar = new ArrayList(products.Length);
			Hashtable htable = new Hashtable();
			for (int i = 0; i < products.Length; i++)
				if (!htable.ContainsKey(products[i].ProdId))
				{
					htable.Add(products[i].ProdId, products[i]);
					ar.Add(products[i]);
				}
			ProdPrice[] candidates = new ProdPrice[ar.Count];	
			for (int i = 0; i < candidates.Length; i++)
				candidates[i] = (ProdPrice)ar[i];

			return candidates;
		}
		public static ProdPrice[] FilterTopProducts(UOW uow, ProdPrice[] products)
		{
			if (products == null)
				throw new ArgumentException("Products cannot be null");

			if (products.Length == 0)
				return products;

			ProdPrice[] candidates = new ProdPrice[products.Length];
			products.CopyTo(candidates, 0);

			ProdFilter[] rules = ProdQual.GetTopProductsRules(uow);		// Get rules and mark entries
			for(int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates);

			ArrayList ar = new ArrayList();
			for(int i = 0; i < candidates.Length; i++) 			//remove unvailable
				if (candidates[i].ProdSelState == ProdSelectionState.Available)
					ar.Add(candidates[i]);

			
			candidates = new ProdPrice[ar.Count];
			ar.CopyTo(candidates);

			return candidates;

		}
		public static ProdPrice[] SetupAvailState(UOW uow, ProdPrice[] prods) // mark unavailable products
		{
			if (prods == null)
				throw new ArgumentException("Products cannot be null");

			if (prods.Length == 0)
				return prods;

			ProdPrice[] candidates = new ProdPrice[prods.Length];
			prods.CopyTo(candidates, 0);   // local copy

			ProdFilter[] rules = ProdQual.GetProdAvailRules(uow);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}
		public static ProdPrice[] SetupPreselected(UOW uow, ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Products cannot be null");

			if (prods.Length == 0)
				return prods;

			ProdPrice[] candidates = new ProdPrice[prods.Length];
			prods.CopyTo(candidates, 0);   // local copy

			ProdFilter[] rules = ProdQual.PreSelectedProdsRules(uow);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}
		public static ProdPrice[] FilterVisible(UOW uow, ProdPrice[] prods)// leaves only visible products
		{
			if (prods == null)
				throw new ArgumentException("Products cannot be null");

			if (prods.Length == 0)
				return prods;

			ProdPrice[] candidates = new ProdPrice[prods.Length];
			prods.CopyTo(candidates, 0);   // local copy

			ProdFilter[] rules = ProdQual.GetVisibleProdRules(uow); 
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}
		public static ProdPrice[] FilterLockedUnavail(UOW uow, ProdPrice[] prods)// removes locked unavailable products
		{
			if (prods == null)
				throw new ArgumentException("Products cannot be null");

			if (prods.Length == 0)
				return prods;

			ProdPrice[] candidates = new ProdPrice[prods.Length];
			prods.CopyTo(candidates, 0);   // local copy

			ProdFilter[] rules = ProdQual.RemLockedUnavailRules(uow); 
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}
		public static ProdPrice[] SuppressZeroPrice(UOW uow, ProdPrice[] products)
		{
			if (products == null)
				throw new ArgumentException("Products cannot be null");

			if (products.Length == 0)
				return products;
	
			ProdPrice[] candidates = new ProdPrice[products.Length];
			products.CopyTo(candidates, 0);

			ProdFilter[] rules = ProdQual.GetSuppressZeroPrice(uow);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}
		public static ProdPrice[] UnselectCurrent(UOW uow, IProdPrice selected, ProdPrice[] products)
		{
			if (products == null)
				throw new ArgumentException("Products cannot be null");

			if (products.Length == 0)
				return products;
	
			ProdPrice[] candidates = new ProdPrice[products.Length];
			products.CopyTo(candidates, 0);

			ProdFilter[] rules = ProdQual.GetUnselectCurrentRules(uow, selected);
			for (int i = 0; i < rules.Length; i++)
				candidates = rules[i].Qualify(candidates); 
			
			return candidates;
		}


	#endregion
	#region Implementation
		static RestrictedProdRule[] GetRestRiles(UOW uow, string storeCode, string criteria)
		{
			RestrictedProdRule[] rules = new RestrictedProdRule[0];
			
			StoreLocation sl = StoreLocation.find(uow, storeCode);
			Corporation corp = Corporation.find(uow, sl.CorpID);

			if ((sl.RestProdRule == null) || (sl.RestProdRule.Trim().Length == 0))
				if ((corp.DefaulRestProdRule == null) || (corp.DefaulRestProdRule.Trim().Length == 0))
					return rules;

			if ((sl.RestProdRule == null) || (sl.RestProdRule.Trim().Length == 0))
				return RestrictedProdRule.getAll(uow, corp.DefaulRestProdRule, criteria, DateTime.Now);
		
			return RestrictedProdRule.getAll(uow, sl.RestProdRule, criteria, DateTime.Now);
		}
		static ProdPrice[] SelectMinPrice(ProdPrice[] candidates)
		{
			if (candidates == null)
				return new ProdPrice[0];

			if (candidates.Length == 0)
				return new ProdPrice[0];


			decimal price = Decimal.MaxValue;
			int index = int.MinValue;

			for (int i = 0; i < candidates.Length; i++)
			{
				if (!(candidates[i].PriceAmt > 0))
					continue;

				if (candidates[i].PriceAmt  < price)
				{
					price = candidates[i].PriceAmt;
					index = i;
				}
			}
			candidates[index].ProdSelState = ProdSelectionState.Selected;
			return candidates;
		}
		static ProdPrice[] SelectHighestPrice(ProdPrice[] candidates)
		{
			decimal price = 0m;
			int index = int.MinValue;

			for (int i = 0; i < candidates.Length; i++)
				if (candidates[i].PriceAmt  > price)
				{
					price = candidates[i].PriceAmt;
					index = i;
				}
			
			candidates[index].ProdSelState = ProdSelectionState.Selected;
			return candidates;
		}
		static ProdPrice[] ConvertToPP(ArrayList ar)
		{
			ProdPrice[] prods = new ProdPrice[ar.Count];
			ar.CopyTo(prods);
			return prods;
		}

	#endregion
	}
}