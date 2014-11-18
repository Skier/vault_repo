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
	public class AvailProds
	{

		public static ProdPrice[] GetAvailProds(UOW uow, int ilec, string zipcode, string role, string storeCode)
		{
            ProdPrice[] prods = ProdPrice.getAvaProdForZip(uow,  Location.find(uow, zipcode).LocId, ilec, role, storeCode); 
			prods = ProdQual.RemoveDuplicates(prods);

			int[] keys = new int[prods.Length];

			for (int i = 0; i < keys.Length; i++)
			{
				keys[i] = ProdTypeCol.GetProdType(prods[i].ProdType).OrderDisplaySeq;
				if (keys[i] == 0)
					keys[i] = int.MaxValue;
			}
				
			Array.Sort(keys, prods);

			return SortByProdId(keys, prods);
		}
		public static ProdPrice[] SortByProdId(int[] keys, ProdPrice[] prods)
		{
			int[] prodIds = new int[keys.Length];
			int j = 0;
			int i = 0;
			
			for (i = 0; i < keys.Length; i++)
			{
				prodIds[i] = prods[i].id;
			
				if (i == 0)
					continue;

				if (keys[i] == keys[i-1])
				{
					j += 1;
					continue;
				}
				Array.Sort(prodIds, prods, i-j-1, j+1);	
				j = 0;
			}
			Array.Sort(prodIds, prods, i-j-1, j+1);	

			return prods;
		}
	}
}