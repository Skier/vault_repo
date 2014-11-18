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
	[Serializable]  
	public class ProdInfoCol 
	{
		/*        Data        */
		static ProdInfo[] data;
		static ProdComposition[] comps;
		static TaxCode[] taxcodes;
		static IProductCategory[] prodCats;
		static DateTime lastLoad;

 
		/*		Properties		*/
		public static ProdComposition[] Comps
		{
			get 
			{
				if (data == null)
					new ProdInfoCol();

				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return comps;
			}
		}

		static ProdInfo[] Data
		{
			get 
			{
				if (data == null)
					new ProdInfoCol();

				if (DateTime.Now.AddMinutes(- Const.REF_INTERVAL) > lastLoad)
					LoadData();
				
				return data;
			}
		}
		
		
		/*		Constructors	*/
		ProdInfoCol() 
		{
			LoadData();
			OperationMessenger.RefreshData += new EventHandler(OnRefresh);
		}

		/*		Methods		*/
		public static ProdInfo GetProd(int id)
		{
			if (data == null)
				new ProdInfoCol();

			for (int i = 0; i < Data.Length; i++)
				if (Data[i].Id == id)
					return Data[i].Clone();
	
			throw new ApplicationException("Can't find Product '" + id + "'") ;
		}
		public static ProdInfo[] GetProd()
		{
			ProdInfoCol pc = new ProdInfoCol();
			if (data == null)
				new ProdInfoCol();

			ProdInfo[] res = new ProdInfo[Data.Length];
			for (int i = 0; i < res.Length; i++)
				res[i] = Data[i].Clone();

			return res;
		}
		public static TaxCode GetTaxCode(int prod)
		{
			return GetTaxCode((GetProd(prod)).TaxCode);
		}
		public static TaxCode GetTaxCode(string taxcode)
		{
			if (taxcode == null)
				return null;
			
			if (taxcode.Trim().Length == 0)
				return null;

			for(int i = 0; i < taxcodes.Length; i++)
				if (taxcodes[i].TxCode == taxcode)
					return taxcodes[i];
			
			return null;
			//throw new ArgumentException("Taxcode :" + taxcode + " not found");
		}
		public static ProdComposition[] getAllPackageComps(int parent)
		{
			ProdComposition[] acomp = getAllComps(parent, ProdComposition.COMP);
			ProdComposition[] acompDM = getAllComps(parent, ProdComposition.DM);
			
			if (acompDM.Length == 0)
				return acomp;

			if (acomp.Length == 0)
				return acompDM;

			ProdComposition[] res = new ProdComposition[acomp.Length + acompDM.Length];
			acomp.CopyTo(res, 0);
			acompDM.CopyTo(res, acomp.Length);
			
			return res;
		}
		public static ProdComposition getAllComps(int parent, string compType, int ilec)
		{
			ArrayList ar = new ArrayList();
			getChildren(ar, parent, compType);

			ProdComposition[] components = new ProdComposition[ar.Count];
			ar.CopyTo(components);
			for (int i = 0; i < components.Length; i++)
				if (  GetProd(components[i].SubProd).Supplier == ilec)
					return components[i];

			return null;
		}
		public static ProdComposition[] getAllComps(int parent, string compType)
		{
			ArrayList ar = new ArrayList();
			getChildren(ar, parent, compType);

			ProdComposition[] components = new ProdComposition[ar.Count];
			ar.CopyTo(components);
			return components;
		}
		public static ProdComposition[][] getPreReqRecursive(int sub) 
		{
			ArrayList prq = new ArrayList();

			prq.AddRange(getParents(sub, ProdComposition.RECURSIVE_PRE_REQ));
			prq.AddRange(getParents(sub, ProdComposition.PREREQ));

			if (prq.Count == 0)
				return new ProdComposition[][] {};

			ProdComposition[] comps = new ProdComposition[prq.Count];
			prq.CopyTo(comps);
			
			if (comps.Length == 0)
				return new ProdComposition[][] {};

			string[] keys = new string[comps.Length];
			for (int i = 0; i < keys.Length; i++)
				keys[i] = ((ProdComposition)comps[i]).AlternativeComp.Trim().ToLower();

			Array.Sort(keys, comps);

			ArrayList ar = new ArrayList();
			ArrayList arGroup = new ArrayList();
			string altComp = null;

			for (int i = 0; i < comps.Length; i++)
			{
				if (altComp != comps[i].AlternativeComp.Trim().ToLower())
				{
					if (arGroup.Count > 0)
					{
						ar.Add(ToProdCompArray(arGroup));
					}
					arGroup.Clear();
					altComp = comps[i].AlternativeComp.Trim().ToLower();
				}
				arGroup.Add(comps[i]);
			}
			
			if (arGroup.Count > 0)
				ar.Add(ToProdCompArray(arGroup));
					
			ProdComposition[][] res = new ProdComposition[ar.Count][];
			for (int i = 0; i < res.Length; i++)
				res[i] = (ProdComposition[])ar[i];

			return res;
		}
		public static ProdComposition[][] getTopOnlyPreReqs(int sub) //, string compType) 
		{
			ArrayList prq = new ArrayList();

			prq.AddRange(getParents(sub, ProdComposition.TOP_ONLY_PRE_REQ));

			if (prq.Count == 0)
				return new ProdComposition[][] {};

			ProdComposition[] comps = new ProdComposition[prq.Count];
			prq.CopyTo(comps);
			
			if (comps.Length == 0)
				return new ProdComposition[][] {};

			string[] keys = new string[comps.Length];
			for (int i = 0; i < keys.Length; i++)
				keys[i] = ((ProdComposition)comps[i]).AlternativeComp.Trim().ToLower();

			Array.Sort(keys, comps);

			ArrayList ar = new ArrayList();
			ArrayList arGroup = new ArrayList();
			string altComp = null;

			for (int i = 0; i < comps.Length; i++)
			{
				if (altComp != comps[i].AlternativeComp.Trim().ToLower())
				{
					if (arGroup.Count > 0)
					{
						ar.Add(ToProdCompArray(arGroup));
					}
					arGroup.Clear();
					altComp = comps[i].AlternativeComp.Trim().ToLower();
				}
				arGroup.Add(comps[i]);
			}
			
			if (arGroup.Count > 0)
				ar.Add(ToProdCompArray(arGroup));
					
			ProdComposition[][] res = new ProdComposition[ar.Count][];
			for (int i = 0; i < res.Length; i++)
				res[i] = (ProdComposition[])ar[i];

			return res;
		}
		public static ProdComposition[] getComps(int prod, string compType)
		{
			ArrayList ar = new ArrayList();
			
			for (int i = 0; i < Comps.Length; i++)
				if ((Comps[i].Prod == prod) && (SameCompType(Comps[i].CompType, compType)))
					ar.Add(Comps[i]);
			
			ProdComposition[] components = new ProdComposition[ar.Count];
			ar.CopyTo(components);
			return components;
		}
		public static ProdComposition[] getParents(int sub, string compType)
		{
			ArrayList ar = new ArrayList();
			
			for (int i = 0; i < Comps.Length; i++)
				if ((Comps[i].SubProd == sub) && (SameCompType(Comps[i].CompType, compType)))
					ar.Add(Comps[i]);
			
			ProdComposition[] components = new ProdComposition[ar.Count];
			ar.CopyTo(components);
			return components;
		}
		public static IProductCategory getProdCategory(string prodCat)
		{
			for (int i = 0; i < prodCats.Length; i++)
				if (prodCats[i].ProdCategory.Trim().ToLower() == prodCat.Trim().ToLower())
					return prodCats[i];
			
			throw new ArgumentException("Unknown Product Category: " + prodCat);
		}
		/*		Implementation		*/
		static ProdComposition[] ToProdCompArray(ArrayList ar)
		{
			if (ar.Count == 0)
				throw new ArgumentNullException("ArrayList cannot be empty");

			ProdComposition[] pc = new ProdComposition[ar.Count];
			ar.CopyTo(pc);
			return pc;
		}
		static void getChildren(ArrayList ar, int parent, string compType)
		{
			ProdComposition[] children = getComps(parent, compType);
			
			if (children.Length > 0)
			{
				ar.AddRange(children);
				for (int i = 0; i < children.Length; i++)
					getChildren(ar, children[i].SubProd, compType);
			}
		}
		static bool SameCompType(string source, string candidate)
		{
			if ((source == null) && (candidate == null))
				return true;

			if (source == null)
				return false;
			
			if (candidate == null)
				return false;

			return source.Trim().ToLower() == candidate.Trim().ToLower();
		}
		static void LoadData()
		{
			UOW uow = null;
			try
			{
				uow = new UOW();
				uow.Service = "ProdInfoCol.LoadData()";
			
				data = ProdInfo.Conv(Product.getAll(uow));
				comps = ProdComposition.getAll(uow);
				taxcodes = TaxCode.getAll(uow);
				prodCats = ProductCategory.getAll(uow);
				
				lastLoad = DateTime.Now;
			}
			finally
			{
				uow.close();
			}
		}
		static void OnRefresh(object sender, EventArgs ea)
		{
			LoadData();
		}
	}
}