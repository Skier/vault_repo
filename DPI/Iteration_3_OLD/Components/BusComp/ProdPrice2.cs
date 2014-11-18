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
	[Serializable]  
	public class ProdPrice2 : Immutable, IProdPrice
	{
		#region Data

		static string iName = "ProdPrice2";
		int id;
		int package;
		int priceExclSupplier;
		string priceRule;
		string priceType;
		string pricePriority;
		decimal unitPrice;
		ProdSelectionState pst;
		bool locked;	
		string priceExcOrderType;		

		#endregion

		#region Static Properties

		static bool ContainsBasicService(UOW uow, ProdPrice2 prod, string provCat)
		{
			if (IsLocalService(prod, provCat)) 
				return true;
			
			return CheckCompForBS(uow, prod.ProdId, provCat);
		}

		static bool ContainsBasicService(UOW uow, ProdPrice2 prod)
		{
			if (IsLocalService(prod)) 
				return true;
			
			return CheckCompForBS(uow, prod.ProdId, prod.ProvCategory);
		}
		
		static bool IsLocalService(ProdPrice2 prod, string provCat)
		{
			if (!IsSameProvCat(prod.ProvCategory, provCat))
				return false;

			return IsLocalService(prod.ProdSubclass);
		}

		static bool IsLocalService(ProdPrice2 prod)
		{
			return IsLocalService(prod.ProdSubclass);
		}
		
		static bool IsLocalService(string subClass)
		{
			return subClass.Trim().ToLower() == Const.LOCAL_SERVICE.ToLower();
		}
		
		static bool IsSameProvCat(string source, string provCat)
		{
			if (source == null)
				return false;
			
			if (provCat == null)
				return false;

			if (source.Trim().ToLower() == provCat.Trim().ToLower())
				return true;

			return false;
		}
		
		static bool CheckBillableBS(UOW uow, ref ProdPrice2[] billable, string provCat)
		{
			for (int i = 0; i < billable.Length; i++)
				if (ContainsBasicService(uow, billable[i], provCat))
					return true;

			return false;
		}
	
		#endregion

		#region Direct Instance Properties

		public ProdSelectionState ProdSelState 
		{ 
			get { return pst;} 
			set { pst = value; }
		}
		
		public bool     Locked
		{
			get { return locked; }
			set { locked = value; }
		}
		
		public int      PackageId         
		{ 
			get { return package; }
			set { package = value; }
		}			
		
		public override IDomKey IKey      { get { return new Key(iName, id.ToString());		}}
		public int      ProdId            { get { return id;								}}	
		public decimal  UnitPrice         { get { return unitPrice;							}}
		public string   PricePriority	  { get { return pricePriority;						}}
		public string   PriceRule		  { get { return priceRule;							}}
		public string   PriceType		  { get { return priceType;							}}
		public int      PriceExclSupplier { get { return priceExclSupplier;					}}
		public string   PriceExcOrderType { get { return priceExcOrderType;					}}
		
	
		#endregion
	
		#region Derived Instance Properties

		public bool IsPreselectedWebOrderL2 { get { return ProdInfoCol.GetProd(id).IsPreselectedWebOrderL2; }}
		public bool IsAgentVisible	{ get { return ProdInfoCol.GetProd(id).IsAgentVisible; }}
		public bool IsBillable      { get { return ProdInfoCol.GetProd(id).IsBillable; }}
		public bool IsFee           { get {	return ProdTypeCol.GetProdType(ProdInfoCol.GetProd(id).ProdType).IsFee; }}
		public bool IsComponentOnly { get { return ProdInfoCol.GetProd(id).IsComponentOnly;  }}
	//	public decimal PriceAmt		{ get { return this.StartServMon == 1 ? unitPrice : 0; }}
		public string ProdType		{ get { return ProdInfoCol.GetProd(id).ProdType; }}
		public string ProdName		{ get {	return ProdInfoCol.GetProd(id).ProdName; }}
		//	public string ProdName		{ get {	return id.ToString() + '-' + ProdInfoCol.GetProd(id).ProdName;}}
		public string BillText		{ get { return ProdInfoCol.GetProd(id).BillText;} } 
	//	public string BillText		{ get { return id.ToString() + '-' + ProdInfoCol.GetProd(id).BillText; }}
		public string Description	{ get { return  ProdInfoCol.GetProd(id).Description; }}
		public string TaxCode		{ get { return  ProdInfoCol.GetProd(id).TaxCode; }}
		public string ProvCategory	{ get { return  ProdInfoCol.GetProd(id).ProvCategory; }}
		public bool IsProvisionable { get { return  ProdInfoCol.GetProd(id).IsProvisionable; }}
		public bool IsProvViaMapping{ get { return  ProdInfoCol.GetProd(id).IsProvViaMapping; }}
		public int StartServMon		{ get { return  ProdInfoCol.GetProd(id).StartServMon; }}
		public int EndServMon		{ get { return  ProdInfoCol.GetProd(id).EndServMon; }}
		public int ExclusiveSupplier{ get { return  ProdInfoCol.GetProd(id).Supplier; }}
		public string ProdSubclass	{ get { return  ProdInfoCol.GetProd(id).ProdSubClass; }}		
		public string OrdSumryStartMon2	{ get { return ProdInfoCol.GetProd(id).OrdSumryStartMon2; }	}		
		public bool IsInstallForEachInstance  
		{
			get 
			{
				return ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass)
					  .IsInstallForEachInstance; }
		}
		
		public bool IsRestrictedToOneInstance 
		{
			get 
			{
				return ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass)
					  .IsRestrictedToOneInstance; }
		}

		public bool DisplayUnclickMessage { get { return ProdInfoCol.GetProd(id).DisplayUnclickMessage; }}
		public string FulfillMethod	          
		{
			get 
			{
				return  ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass)
					  .FulfillMethod; }
		}
		
		public bool SuppressZeroPrice         
		{
			get 
			{
				return ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass)
					  .SuppressZeroPrice; }
		}
		
		public bool SuppressOnWebReceipt      
		{
			get 
			{
				return ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass)
					  .SuppressOnWebReceipt; }
		}
	
		public bool SuppressZeroPriceProd{ get { return  ProdInfoCol.GetProd(id).SuppressZeroPriceProd; }}
		//suppressZeroPriceProd
		
		#endregion

		#region Constructors

		public ProdPrice2()	
		{

		}
		public ProdPrice2(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
		public ProdPrice2(IDmdItem di) : this()
		{	
			id				  = di.Prod;
			package			  = di.PackageId;
			priceExclSupplier = di.Supplier;
			priceRule		  = di.PriceRule;
			unitPrice         = di.EffPrice;
			priceExcOrderType = di.ParDemand.DmdType;
		
			//no longer required:
			//	priceType  
			//	pricePriority 
			//	pst
			//	locked	

		}	
		#endregion
	
		#region Methods

		public static ProdPrice2[] GetTopProducts(UOW uow, string zipcode, int ilec)
		{
			return GetTopProducts(uow, Location.find(uow, zipcode).LocId,  ilec);
		}
		public static ProdPrice2[] GetTopProducts(UOW uow, int loc, int ilec)
		{
			ArrayList ar = new ArrayList();
			ProdPrice2[] tops = GetBillableForZip(uow, loc, ilec);

			for (int i = 0; i < tops.Length; i++)
				if (ContainsBasicService(uow, tops[i]))
					ar.Add(tops[i]);

			return ConvertToPP2(ar);
		}
		public static ProdPrice2[] GetBillableForZip(UOW uow, int loc, int ilec)
		{
			return FilterCompOnlyAndFee(GetAllBillableForZip(uow, loc, ilec)); 
		}
		public static ProdPrice2[] GetAllBillableForZip(UOW uow, int loc, int ilec)
		{
			// 1. Filter out duplicates, inactive, non-billable
			ProdPrice2[] billable = FilterOutDups( 
				FilterOutNonBillable(new PriceSQL2().GetBillableProdForZip(uow, loc, ilec)));

			// 2. Filter out Resale if there is no Resale Basic Service 
			if (!CheckBillableBS(uow, ref billable, Const.RESALE.ToLower()))
				RemoveProvCat(ref billable, Const.RESALE.ToLower());

			// 3.  Filter out UNE-P products if there is no UNE-P Basic Service
			if (!CheckBillableBS(uow, ref billable, Const.UNEP.ToLower()))
				RemoveProvCat(ref billable, Const.UNEP.ToLower());

			// 4. Filter out Provisionable Via Mapping products and packages that do not map   
			return CheckForProvViaMapping(uow, billable, loc, ilec);
		}

		public static ProdPrice2[] GetProvisionableForZip(UOW uow, int loc, int ilec)
		{
			return FilterOutDups(new PriceSQL2().GetProvProdForZip(uow, loc, ilec));
		}
		public static ProdPrice2[] GetFees(UOW uow, int prod, int loc, int ilec, OrderType oType)
		{
			ArrayList ar = new ArrayList();
			ar.AddRange(new PriceSQL2().GetFees(uow, prod, loc, ilec, oType));

			Hashtable ht = new Hashtable();
			for (int i = 0; i<ar.Count;i++)
			{
				ProdPrice2 p2 = (ProdPrice2)ar[i];
				if ((p2.UnitPrice == decimal.Zero) && p2.SuppressZeroPriceProd)
					continue;

				if (!ht.ContainsKey(p2.ProdId))
					ht.Add(p2.ProdId, ar[i]);
			}
			ProdPrice2[] pp = new ProdPrice2[ht.Count];

			int j = 0;
			IDictionaryEnumerator enumerator = ht.GetEnumerator();
			while(enumerator.MoveNext())
			{
				if (enumerator.Value != null)
					pp[j++]= (ProdPrice2)enumerator.Value;
			}
			return pp;
		}
		public static ProdPrice2 GetProdPrice(UOW uow, int prod, int loc, int ilec)
		{
			return new PriceSQL2().GetProdPrice(uow, prod, loc, ilec);
		}
		public override string ToString() {	return BillText; }		
		public static ProdPrice2[] GetFees(UOW uow, int prod, string storeCode, OrderType otype)
		{
			StoreLocation store = StoreLocation.find(uow,  storeCode);

			if (store.St == null)
				throw new ArgumentException("StoreLocation.State is required");

			if (store.St.Trim().Length == 0)
				throw new ArgumentException("StoreLocation.State is required");

			return ProdPrice2.GetFees(uow, prod,  Location.find(uow, store.St).LocId, 0, otype);
		}
	
		#endregion
	
		#region Implementation

		static ProdPrice2[] CheckForProvViaMapping(UOW uow, ProdPrice2[] billable, int loc, int ilec)
		{
			ProdPrice2[] provs = FilterOutDups(new PriceSQL2().GetProvProdForZip(uow, loc, ilec));
			ArrayList ar = new ArrayList();
 
			for (int i = 0; i < billable.Length; i++)
			{
				if (!CheckMapping(billable[i].ProdId, provs, ilec))
					continue;

				ProdComposition[] comps = ProdComposition.getAllPackageComp(uow, billable[i].ProdId);
				bool ok = true;
				
				for (int j = 0; j < comps.Length; j++)
				{
					if (!CheckMapping(comps[j].SubProd, provs, ilec))
					{
						ok = false;;
						break;
					}
				}
				
				if (ok)
					ar.Add(billable[i]);
			}

			return ConvertToPP2(ar);
		}

		static ProdPrice2[] GetBillible(ProdPrice2[] prods) 
		{
			ArrayList ar = new ArrayList();
			for (int i = 0; i < prods.Length; i++)
			{
				if (!prods[i].IsBillable)
					continue;

				if (prods[i].IsComponentOnly)
					continue;
				
				if (prods[i].IsFee)
					continue;
			
				ar.Add(prods[i]);
			}

			return ConvertToPP2(ar);
		}
		static ProdPrice2[] ConvertToPP2(ArrayList ar)
		{
			ProdPrice2[] prods = new ProdPrice2[ar.Count];
			ar.CopyTo(prods);
			return prods;
		}
		static void         RemoveProvCat(ref ProdPrice2[] prods, string provCat)
		{
			ArrayList ar = new ArrayList(prods.Length);
			
			for (int i = 0; i < prods.Length; i++)
				if (prods[i].ProvCategory.Trim().ToLower() != provCat)
					ar.Add(prods[i]);
			
			prods = ConvertToPP2(ar);
		}

		static bool         CheckCompForBS(UOW uow, int prod, string provCat)
		{
			ProdComposition[] comps = ProdComposition.getAllPackageComp(uow, prod);
				
			for (int i = 0; i < comps.Length; i++)
			{
				ProdInfo pi = ProdInfoCol.GetProd(comps[i].SubProd);
				
				if (!pi.IsBillable)
					continue;
				
				if (pi.ProvCategory.Trim().ToLower() != provCat.Trim().ToLower())
					continue;
				
				if (IsLocalService(pi.ProdSubClass))
					return true;
			}

			return false;
		}

		static ProdPrice2[] FilterCompOnlyAndFee(ProdPrice2[] billable)
		{
			ArrayList ar = new ArrayList(billable.Length);
			for (int i = 0; i < billable.Length; i++)
			{
				if (billable[i].IsComponentOnly)
					continue;

				if (billable[i].IsFee)
					continue;
				
				ar.Add(billable[i]);
			}

			return ConvertToPP2(ar);
		}
		static ProdPrice2[] FilterOutNonBillable(ProdPrice2[] prods)
		{
			ArrayList ar = new ArrayList(prods.Length);

			for (int i = 0; i < prods.Length; i++)
			{
				if  (!ProdInfoCol.GetProd(prods[i].ProdId).IsBillable)
					continue;

				if  (!ProdInfoCol.GetProd(prods[i].ProdId).IsAgentVisible)
					continue;
				ar.Add(prods[i]);
			}
			return ConvertToPP2(ar);
		}
		static ProdPrice2[] FilterOutDups(ProdPrice2[] prods)
		{
			Hashtable hashtable = new Hashtable(400);
			ArrayList ar = new ArrayList(200);

			for (int i = 0; i < prods.Length; i++)
			{
				if  (ProdInfoCol.GetProd(prods[i].ProdId).Status.Trim().ToLower() != Const.PRODUCT_ACTIVE)
					continue;

				if (!hashtable.ContainsKey(prods[i].ProdId))
				{
					ar.Add(prods[i]);
					hashtable.Add(prods[i].ProdId, prods[i]);
				}
			}
			return ConvertToPP2(ar);
		}

		static bool CheckMapping(int prodId, ProdPrice2[] provs, int ilec)
		{
			if (!ProdInfoCol.GetProd(prodId).IsProvViaMapping)
				return true;

			ProdComposition mapped = ProdInfoCol.getAllComps(prodId, ProdComposition.MAPPING, ilec);
			if (mapped == null)
				return false;

			for (int j = 0; j < provs.Length; j++) // is mapped prod available in this zip?
				if (mapped.SubProd == provs[j].ProdId)
					return true;
			
			return false;
		}
	
		#endregion

		#region SQL

		class PriceSQL2 
		{
			#region Methods

			public ProdPrice2 GetProdPrice(UOW uow, int prod, int loc, int ilec)
			{
				// Order type ("new") is hardcoded in the stored proc...
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spOrder_GeProdPrice"; 

				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = loc;
				cmd.Parameters.Add("@Prod",SqlDbType.Int, 0).Value = prod; 
				cmd.Parameters.Add("@Ilec", SqlDbType.Int, 0).Value = ilec;
				cmd.Parameters.Add("@Dpi", SqlDbType.Int, 0).Value = Const.DPI;
				                
				ProdPrice2[] pp2 = execReader(cmd, true);
				
				if (pp2.Length == 0)
					return null;
				
				return pp2[0];
			}
//			public ProdPrice2 GetPriceForProd(UOW uow, int loc, int ilec, int prod)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.CommandText = "dbo.spOrder_GetPriceForProdAtZip";
//			
//				cmd.Parameters.Add("@DPI", SqlDbType.Int, 0).Value = Const.DPI;
//				cmd.Parameters.Add("@ILEC", SqlDbType.Int, 0).Value = ilec;
//				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = loc;
//				cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = prod;
//
//				ProdPrice2[] pp2 = execReader(cmd, true);
//				if (pp2.Length > 0)
//					return pp2[0];
//				
//				return null;
//			}

//			public ProdPrice2[] GetFeeProd(UOW uow, int loc, string orderType, int ilec, int prod)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.CommandText = "dbo.spOrder_GetTagAlong";
//
//				cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 10).Value = orderType;
//				cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = prod;
//				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = loc;
//				cmd.Parameters.Add("@ILEC", SqlDbType.Int, 0).Value = ilec;
//				cmd.Parameters.Add("@DPI", SqlDbType.Int, 0).Value = Const.DPI;
//				
//				return execReader(cmd, true);
//			}

			public ProdPrice2[] GetBillableProdForZip(UOW uow, int loc, int ilec)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spOrder_GetBillProdsForZip";

				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = loc;
				cmd.Parameters.Add("@ILEC", SqlDbType.Int, 0).Value = ilec;
				cmd.Parameters.Add("@DPI", SqlDbType.Int, 0).Value = Const.DPI;
				
				return execReader(cmd, true);
			}
			public ProdPrice2[] GetProvProdForZip(UOW uow, int loc, int ilec)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spOrder_GetProvProdsForZip";

				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = loc;
				cmd.Parameters.Add("@ILEC", SqlDbType.Int, 0).Value = ilec;

				return execReader(cmd, false);
			}

			public ProdPrice2[] GetFees(UOW uow, int prod, int loc, int ilec, OrderType oType)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spOrder_GetFees";

				cmd.Parameters.Add("@DPI",  SqlDbType.Int, 0).Value = Const.DPI;
				cmd.Parameters.Add("@LocId",SqlDbType.Int, 0).Value = loc;
				cmd.Parameters.Add("@ILEC", SqlDbType.Int, 0).Value = ilec;
				cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = prod;
				cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 15).Value = oType.ToString();

				return execReader(cmd, true);
			}
		
			#endregion
		
			#region Implementation

			ProdPrice2[] execReader(SqlCommand cmd, bool billable)
			{
				SqlDataReader rdr = null; 
				ArrayList ar = new ArrayList();

				try
				{
					rdr = cmd.ExecuteReader();

					while(rdr.Read())
						ar.Add(reader(rdr, billable));

					return ConvertToPP2(ar);
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}
			void setParam(SqlCommand cmd, ProdPrice2 rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
			}

			protected ProdPrice2 reader(SqlDataReader rdr, bool billable)
			{
				ProdPrice2 rec = new ProdPrice2();

				if (rdr["Prod"] != DBNull.Value)
					rec.id = (int)rdr["Prod"];

				if (!billable)
					return rec;

				// Price attributes
				if (rdr["PricePriority"] != DBNull.Value)
					rec.pricePriority = (string)rdr["PricePriority"];

				if (rdr["ExclusiveSupplier"] != DBNull.Value)
					rec.priceExclSupplier = (int)rdr["ExclusiveSupplier"];

				if (rdr["ExcOrderType"] != DBNull.Value)
					rec.priceExcOrderType = (string)rdr["ExcOrderType"];

				if (rdr["UnitPrice"] != DBNull.Value)
					rec.unitPrice = Decimal.Round((decimal)rdr["UnitPrice"], 2);
				
				if (rdr["PriceRule"] != DBNull.Value)
					rec.priceRule = (string) rdr["PriceRule"];
 
				if (rdr["PriceType"] != DBNull.Value)
					rec.priceType = (string) rdr["PriceType"]; 
			
				
				return rec;
			}
			SqlCommand makeCommand(UOW uow)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				return cmd;
			}	
		
			#endregion
		}

		#endregion
	}
}