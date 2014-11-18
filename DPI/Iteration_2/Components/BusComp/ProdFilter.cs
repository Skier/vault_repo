using System;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;

namespace DPI.Components
{
	public class ProdFilter
	{
		/*		Data		*/
		protected UOW uow;

		/*		Constructors		*/
		public ProdFilter (UOW uow)
		{
			if (uow == null)
				throw new ArgumentException("UOW is required");
			this.uow = uow;
		}

		/*		Methods		*/
		public virtual  ProdPrice[] Qualify(ProdPrice[] prods)
		{
			return null;
		}
		
		/*		Static methods		*/
		protected static ProdPrice[] SplitByStatus(ProdPrice[] all, ProdSelectionState ps )
		{
			ArrayList ar = new ArrayList();
			for (int i = 0; i < all.Length; i++)
				if (all[i].ProdSelState == ps)
					ar.Add(all[i]);

			ProdPrice[] res = new ProdPrice[ar.Count];
			ar.CopyTo(res);
			return res;
		}
	}
	public class RemoveLockedUnavailable : ProdFilter // removes locked, unavailable prods
	{
		internal RemoveLockedUnavailable(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
			
			ArrayList ar = new ArrayList(prods.Length);
			for (int i = 0; i < prods.Length; i++)
				if (!((prods[i].Locked) && (prods[i].ProdSelState == ProdSelectionState.Unavailable)))
					ar.Add(prods[i]);
				
			ProdPrice[] filtered = new ProdPrice[ar.Count];
			ar.CopyTo(filtered);
			return filtered;
		}
	}

	public class LockTopProducts : ProdFilter // locks top products, selected or not
	{
		internal LockTopProducts(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
			
			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProdType == "Local Service")
				{
					prods[i].Locked = true;
				}

			return prods;
		}
	}
	public class EnableNonTopProducts : ProdFilter // enables products with the exception of selected and/or locked
	{
		internal EnableNonTopProducts(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
			
			for (int i = 0; i < prods.Length; i++)
				
				if (prods[i].Locked)
				{
					if (prods[i].ProdSelState != ProdSelectionState.Selected)
						prods[i].ProdSelState = ProdSelectionState.Unavailable;
				}
				else
				{
					prods[i].ProdSelState = ProdSelectionState.Available;
				}

			return prods;
		}
	}
	public class RemoveUnselectedTopProducts : ProdFilter 
	{
		int selTopProd;
		internal RemoveUnselectedTopProducts(UOW uow, int selTopProd) : base(uow) 
		{
			this.selTopProd = selTopProd;
		}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
			ArrayList ar = new ArrayList(prods.Length);

			for (int i = 0; i < prods.Length; i++)
			{
				if (prods[i].ProdType == "Local Service")
					if (prods[i].ProdId != selTopProd)
						continue;
				
				ar.Add(prods[i]);
			}
			ProdPrice[] products = new ProdPrice[ar.Count];
			ar.CopyTo(products);
			return products;
		}
	}
	public class RemoveProdCategoryTopProducts : ProdFilter 
	{
		IProductCategory topCat;

		internal RemoveProdCategoryTopProducts(UOW uow, int topProd) : base(uow) 
		{
			topCat = ProdInfoCol.getProdCategory((ProdInfoCol.GetProd(topProd)).ProdCategory);
		}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
			
			ArrayList ar = new ArrayList(prods.Length);

			for (int i = 0; i < prods.Length; i++)
				if (Match(prods[i]))
					ar.Add(prods[i]);

			ProdPrice[] products = new ProdPrice[ar.Count];
			ar.CopyTo(products);
			return products;
		}
		bool Match(ProdPrice pp)
		{
			return topCat.Compare(ProdInfoCol.getProdCategory(ProdInfoCol.GetProd(pp.ProdId).ProdCategory));
		}
	}
	public class ProvCategoryProds : ProdFilter // removes "wrong" provcats like "Resale" for UNE-P basic service
	{
		protected string provCat;
		protected const string bypassCat = "none";
		internal ProvCategoryProds(UOW uow, string basicServiceProvCat) : base(uow) 
		{
			this.provCat = basicServiceProvCat.ToLower();
		}
		public override ProdPrice[] Qualify(ProdPrice[] prods) 
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			ArrayList ar = new ArrayList(prods.Length);

			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProvCategory.ToLower() ==  bypassCat)
					ar.Add(prods[i]);
				else
					if (prods[i].ProvCategory.ToLower() ==  provCat)
					ar.Add(prods[i]);
			
			ProdPrice[] goodProds = new ProdPrice[ar.Count];
			ar.CopyTo(goodProds);
			return goodProds;
		}
	}
	public class MakeAvailable : ProdFilter // resets all product except for locked and/or selected
	{
		internal MakeAvailable(UOW uow) : base(uow) {	} 
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			for (int i = 0; i < prods.Length; i++)
			{
				if (prods[i].Locked)
					continue;

				if (prods[i].ProdSelState == ProdSelectionState.Selected)
					continue;

				if (!(prods[i].ProdSelState == ProdSelectionState.Unavailable))
					continue;

				prods[i].ProdSelState = ProdSelectionState.Available;
			}
			return prods;
		}
	}
	public class TopProducts : ProdFilter // returns products containing Basic Service
	{
		internal TopProducts(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			for (int i = 0; i < prods.Length; i++)
			{
				prods[i].ProdSelState = ProdSelectionState.Unavailable;
				if (prods[i].ProdType == "Local Service")
					prods[i].ProdSelState = ProdSelectionState.Available;
			}
			return prods;
		}
	}
	public class VisibleProducts : ProdFilter // returns AgentVisible products 
	{
		internal VisibleProducts(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Products are required");

			ArrayList ar = new ArrayList(prods.Length);

			for (int i = 0; i < prods.Length; i++)
				if (prods[i].IsAgentVisible)
					ar.Add(prods[i]);

			ProdPrice[] vis = new ProdPrice[ar.Count];
			ar.CopyTo(vis);
			return vis;
		}
	}
	public class DuplicateProduct2 : ProdFilter // makes duplicate products unvailable
	{
		internal DuplicateProduct2(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			ProdPrice[] sel =  SplitByStatus(prods, ProdSelectionState.Selected);
			Hashtable selected = new Hashtable();
			addProd(uow, selected, sel);
			
			Hashtable hashtable;
			
			for (int i = 0; i <  prods.Length; i++)
			{
				if (prods[i].ProdSelState != ProdSelectionState.Available) 
					continue;
				
				hashtable = new Hashtable(selected);
				if (IsDuplicate(uow, hashtable, prods[i]))			        // duplicate entry
					prods[i].ProdSelState = ProdSelectionState.Unavailable; // disqualify
			}
			return prods;
		}		
		protected static bool IsDuplicate(UOW uow, Hashtable hashtable, ProdPrice av) 
		{
			if (hashtable.ContainsKey(av.ProdId))
				return true;
			
			if (checkProd(hashtable, ProdComposition.getAllPackageComp(uow, av.ProdId)))
				return true;
		
			return false;
		}
		internal virtual void addProd(UOW uow, Hashtable hashtable, ProdPrice[] prods)
		{
			for (int i = 0; i < prods.Length; i++)
			{
				if (!hashtable.ContainsKey(prods[i].ProdId))
					hashtable.Add(prods[i].ProdId, prods[i]);

				addProd(hashtable, ProdComposition.getAllPackageComp(uow, prods[i].ProdId));
			}
		}
		protected virtual void addProd(Hashtable hashtable, ProdComposition[] prods) 
		{
			for (int i = 0; i < prods.Length; i++)
				if (!hashtable.ContainsKey(prods[i].SubProd))
					hashtable.Add(prods[i].SubProd, prods[i]);
		}
		static bool checkProd(Hashtable hashtable, ProdComposition[] prods) 
		{
			for (int i = 0; i < prods.Length; i++)
				if (hashtable.ContainsKey(prods[i].SubProd))
					return true;
			
			return false;
		}
	}
	public class DuplicateProduct3 : DuplicateProduct2 
	{
		internal DuplicateProduct3(UOW uow) : base(uow) {	}
		internal override void addProd(UOW uow, Hashtable hashtable, ProdPrice[] prods) // no components
		{
			for (int i = 0; i < prods.Length; i++)
			{
				if (!hashtable.ContainsKey(prods[i].ProdId))
					hashtable.Add(prods[i].ProdId, prods[i]);

//				addProd(hashtable, ProdComposition.getAllPackageComp(uow, prods[i].ProdId));
			}
		}
	}

	public class PreSelectedProds : ProdFilter 
	{
		internal PreSelectedProds(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			for (int i = 0; i < prods.Length; i++)
				if ((prods[i].IsPreselectedWebOrderL2)
					&& (prods[i].ProdSelState == ProdSelectionState.Available))
						prods[i].ProdSelState = ProdSelectionState.Selected;
	
			return prods;
		}
	}
	public class DuplicateSubClass : ProdFilter // makes duplicate products unvailable
	{
		internal DuplicateSubClass(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			ProdPrice[] sel =  SplitByStatus(prods, ProdSelectionState.Selected);
			Hashtable selected = new Hashtable();
			
			ArrayList presel = new ArrayList(); // for preselected conflicts

			for (int i = 0; i < sel.Length; i++) // add prod/package if subclass is restrited to 1 
			{
				if (ProdSubClassCol.GetSubClass(sel[i].ProdSubclass).IsRestrictedToOneInstance)
					if (selected.ContainsKey(sel[i].ProdSubclass))
						presel.Add(sel[i]);
					else
						selected.Add(sel[i].ProdSubclass, ProdSubClassCol.GetSubClass(sel[i].ProdSubclass));

				addProd(selected, ProdComposition.getAllPackageComp(uow, sel[i].ProdId)); // same for components if any
			}
			
			Hashtable hashtable;
			
			for (int i = 0; i <  prods.Length; i++)
			{
				if (prods[i].ProdSelState != ProdSelectionState.Available)  // skip
					continue;
				
				hashtable = new Hashtable(selected);   // already selected, recursively
	
				if (IsDuplicate(uow, hashtable, prods[i]))			        // duplicate entry
				{
					prods[i].ProdSelState = ProdSelectionState.Unavailable; // disqualify
				//	DPI_Err_Log.AddLogEntry(uow, "ProdFilter", "N/A", "Prod: " + prods[i].ProdId + " unavailable because of duplicate subclass");
				}
			}

			for (int i = 0; i < presel.Count; i++)		
				for (int j = 0; j < prods.Length; j++)
					if (((IProdPrice)presel[i]).ProdId == prods[j].ProdId)
						prods[j].ProdSelState = ProdSelectionState.Unavailable;

			return prods;
		}
		protected static bool IsDuplicate(UOW uow, Hashtable hashtable, ProdPrice av) 
		{
			
			if (hashtable.ContainsKey(av.ProdSubclass))
				return true;
			
			if (checkProd(hashtable, ProdComposition.getAllPackageComp(uow, av.ProdId)))
				return true;
		
			return false;
		}
		static bool checkProd(Hashtable hashtable, ProdComposition[] prods) 
		{
			ProdSubClassInfo subclass;

			for (int i = 0; i < prods.Length; i++)
			{
				subclass = ProdSubClassCol.GetSubClass((ProdInfoCol.GetProd(prods[i].SubProd).ProdSubClass));
				if (!subclass.IsRestrictedToOneInstance)
					continue;

				if (hashtable.ContainsKey(subclass.SubClass))
					return true;
			}
			return false;
		}
		static void addProd(Hashtable hashtable, ProdComposition[] prods) // add selected if subclass is restricted to 1
		{
			ProdSubClassInfo subclass;
			for (int i = 0; i < prods.Length; i++)
			{
				subclass = ProdSubClassCol.GetSubClass((ProdInfoCol.GetProd(prods[i].SubProd).ProdSubClass));
				if (!subclass.IsRestrictedToOneInstance)
					continue;

				if (hashtable.ContainsKey(subclass.SubClass))
					continue;

				hashtable.Add(subclass.SubClass, subclass);
			}
		}
	}
	public class PreReqRecursive : ProdFilter // makes duplicate products unvailable
	{
		internal PreReqRecursive(UOW uow) : base(uow) {	}

		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			ProdPrice[] sel =  SplitByStatus(prods, ProdSelectionState.Selected);
			
			Hashtable selected = new Hashtable();
			new DuplicateProduct2(uow).addProd(uow, selected, sel);  // loads selected products with their components

			for (int i = 0; i <  prods.Length; i++)
			{
				if (prods[i].Locked)
					continue;

				if (prods[i].ProdSelState != ProdSelectionState.Available) 
					continue;
					
				if (!PreReqSatisfied(selected, prods[i]))
					prods[i].ProdSelState = ProdSelectionState.Unavailable; // disqualify
			}

			return prods;
		}
		protected virtual ProdComposition[][] GetProdComp(int prod)
		{
			return ProdInfoCol.getPreReqRecursive(prod);
		}
		protected bool PreReqSatisfied(Hashtable hashtable, ProdPrice ava)
		{
			ArrayList ar = new ArrayList();
			ProdComposition[][] comps = GetProdComp(ava.ProdId);
			if (comps.Length == 0)
				return true;
				
			for (int i = 0; i < comps.Length; i++)
			{
				bool passed = true;
				for(int j = 0; j < comps[i].Length; j++)
				{
					if (!hashtable.ContainsKey(comps[i][j].Prod))
					{
						passed = false;
						break;
					}
				}
				if (passed)
					return true;
			}
			return false;
		}
	}
	public class PreReqTopOnly : PreReqRecursive 
	{
		internal PreReqTopOnly(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");

			ProdPrice[] sel =  SplitByStatus(prods, ProdSelectionState.Selected);
			
			Hashtable selected = new Hashtable();
			new DuplicateProduct3(uow).addProd(uow, selected, sel);  // loads selected products with their components

			for (int i = 0; i <  prods.Length; i++)
			{
				if (prods[i].Locked)
					continue;

				if (prods[i].ProdSelState != ProdSelectionState.Available) 
					continue;
					
				if (!PreReqSatisfied(selected, prods[i]))
					prods[i].ProdSelState = ProdSelectionState.Unavailable; // disqualify
			}

			return prods;
		}
		protected override ProdComposition[][] GetProdComp(int prod)
		{
			return ProdInfoCol.getTopOnlyPreReqs(prod);
		}
	}
	
	public class SuppressZeroPrice  : ProdFilter 
	{
		internal SuppressZeroPrice(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
		
			ArrayList ar = new ArrayList(prods.Length);
			
			for (int i = 0; i < prods.Length; i++)
				if (!Suppress(prods[i]))
					ar.Add(prods[i]);

			ProdPrice[] filteredProds  = new ProdPrice[ar.Count];
			ar.CopyTo( filteredProds);
			return  filteredProds;
		}
		bool Suppress(ProdPrice prod)
		{
			if  (prod.PackageId != 0) // do not suppress package components
				return false;

			if (!prod.SuppressZeroPrice) // do not suppress given subclass
				return false;
			
			if (prod.unitPrice != 0) // do suppress non-zero price 
				return false;
			
			if (prod.StartServMon == 1) // do not suppress non-zero proce products starting first month
				return false;

			return true;

		}
	}
	public class SuppressZeroPriceProd  : ProdFilter 
	{
		internal SuppressZeroPriceProd(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
		
			ArrayList ar = new ArrayList(prods.Length);
			
			for (int i = 0; i < prods.Length; i++)
			{	
				if (prods[i].UnitPrice == 0)
					if (prods[i].SuppressZeroPriceProd)
						continue;
				
				ar.Add(prods[i]);
			}
			ProdPrice[] filteredProds  = new ProdPrice[ar.Count];
			ar.CopyTo( filteredProds);
			return  filteredProds;
		}
	}
	public class SuppressOnWebReceipt  : ProdFilter // removes products with SuppressOnWebReceipt set on
	{
		internal SuppressOnWebReceipt(UOW uow) : base(uow) {	}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
		
			ArrayList ar = new ArrayList(prods.Length);
			
			for (int i = 0; i < prods.Length; i++)
				if (!prods[i].SuppressOnWebReceipt)
					ar.Add(prods[i]);

			ProdPrice[] filteredProds  = new ProdPrice[ar.Count];
			ar.CopyTo( filteredProds);
			return  filteredProds;
		}
	}
	public class UnselectCurrent : ProdFilter // removes products with SuppressOnWebReceipt set on
	{
		IProdPrice selected;

		internal UnselectCurrent(UOW uow, IProdPrice selected) : base(uow) 
		{	
			this.selected = selected;
		}
		public override ProdPrice[] Qualify(ProdPrice[] prods)
		{
			if (prods == null)
				throw new ArgumentException("Available products are required");
		
			ProdSubClassInfo ps = ProdSubClassCol.GetSubClass(selected.ProdSubclass);

			if (!ps.SelectionUnselectsCurrent) 
				return prods;

			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProdSubclass == selected.ProdSubclass)
					prods[i].ProdSelState = ProdSelectionState.Available; 

			selected.ProdSelState = ProdSelectionState.Selected;
			return  prods;
		}
	}
}	