using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	public class ProdProcessor
	{
	#region Methods
		public static IProdPrice[] GetDependentProds(UOW uow, IProdPrice basicLine, IILECInfo ilec, 
			string zipcode, string role, string storeCode, string dmdType)
		{
			ProdPrice[] prods 
				= ProdQual.RemoveDuplicates(AvailProds.GetAvailProds(uow, ilec.OrgId, zipcode, role, storeCode)); 

			prods = ProdQual.DmdFilter(prods, dmdType); // removes products of specified prod type

			prods = ProdQual.SuppressZeroPrice
				(uow, ProdQual.SetupAvailState
				(uow, ProdQual.FilterLockedUnavail
				(uow, ProdQual.CheckPreReq				//      (uow,	ProdQual.FilterVisible
				(uow, ProdQual.FilterBySelectedTopProduct
				(uow, prods, (ProdPrice)basicLine)
				))));

			for (int i = 0; i < prods.Length; i++)
				if ((prods[i].IsPreselectedWebOrderL2)
					&& (prods[i].ProdSelState == ProdSelectionState.Available))
					AddProd(uow, prods, prods[i]);
			
			return prods;
		}
		public static IProdPrice[] AddProd (UOW uow, IProdPrice[] prods, IProdPrice added) 
		{
			for(int i = 0; i < prods.Length; i++)
				if (((ProdPrice)prods[i]).ProdId == added.ProdId)
				{	
					if (! ProdQual.ValidateBeforeAdd(uow, prods, added))
					{
						((ProdPrice)prods[i]).ProdSelState = ProdSelectionState.Unavailable; // reject
						return prods;
					}
					((ProdPrice)prods[i]).ProdSelState = ProdSelectionState.Selected;// mark selected product
					break;
				} 
			
			return ProdQual.UnselectCurrent(uow, added, 
				ProdQual.SuppressZeroPrice(uow,  
				ProdQual.FilterLockedUnavail(uow,
				ProdQual.CheckPreReq(uow, 
				//ProdQual.FilterVisible(uow, 
				ProdQual.SetupAvailState(uow, (ProdPrice[])prods))))); 
		}
		public static IProdPrice[] RemoveProd(UOW uow, IProdPrice[] prods, IProdPrice removed)
		{
			for(int i = 0; i < prods.Length; i++)
				if (((ProdPrice)prods[i]).ProdId == removed.ProdId)
				{
					((ProdPrice)prods[i]).ProdSelState = ProdSelectionState.Available;// unselect removed product
					break;
				}

			ProdPrice[] prds = ProdQual.SuppressZeroPrice(uow, 
				ProdQual.FilterLockedUnavail(uow, 
				//ProdQual.FilterVisible(uow, 
				ProdQual.FilterAfterUnselect(uow, (ProdPrice[])prods)));
			
			prds = ProdQual.CheckPreReq(uow, prds);
			return prds;
		}	
		public static string ValidateOrder(UOW uow, IProdPrice[] prods, string dmdType)
		{
			string error = CheckRequiredProducts(uow, prods, dmdType);
			if (error != string.Empty)
				return error;

			// Next validation ... 

			return string.Empty;
		}	
	#endregion 

	#region Implementation 
		static string CheckRequiredProducts(UOW uow, IProdPrice[] prods, string dmdType)
		{
			DmdReqProdRule[][] rules = DmdReqProdRule.GetRules(uow, dmdType);
			
			if (rules == null)
				return string.Empty;

			for (int i = 0; i < rules.Length; i++)
				if (!CheckRequiredProd(rules[i], prods))   // each rule(name) must be satisfied 
					return "Please select one of the " + rules[i][0].RuleName + " products";

			return string.Empty;
		}
		static bool CheckRequiredProd(DmdReqProdRule[] rules, IProdPrice[] prods)
		{
			for (int i = 0; i < rules.Length; i++)
				if (ContainsProduct(rules[i].ReqProd, prods)) // need a single match for this rule name
					return true; 

			return false;
		}
		static bool ContainsProduct(int prod, IProdPrice[] prods)
		{
			for (int i = 0; i < prods.Length; i++)
			{
				if (prods[i].ProdId == prod && prods[i].ProdSelState == ProdSelectionState.Selected)
					return true;

				if (prods[i].ProdSelState != ProdSelectionState.Selected)
					continue;

				if (ContainsComponent(prod, prods[i]))
					return true;
			}

			return false;
		}
		static bool ContainsComponent(int reqProd,  IProdPrice package)
		{		
			ProdComposition[] pc = ProdInfoCol.getAllPackageComps(package.ProdId);

			for (int i = 0; i < pc.Length; i++)
				if (pc[i].SubProd == reqProd)
					return true;
			
			return false;
		}
	#endregion
	}
}