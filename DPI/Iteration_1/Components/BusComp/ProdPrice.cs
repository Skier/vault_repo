using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;


namespace DPI.Components
{
	[Serializable]  
	public class ProdPrice :  Immutable,  IProdPrice
	{
		/*        Data        */
		static   string iName = "ProdPrice";
		internal int id;
		internal int package;
		internal bool isPurchased; 
		internal decimal unitPrice;
		internal bool isRecurring; // derived
		internal string priceRule;
		internal string priceType; // derived
		internal ProdSelectionState pst;
		internal bool locked;
		

		/*        Properties        */
		public bool  IsPreselectedWebOrderL2 { get { return ProdInfoCol.GetProd(id).IsPreselectedWebOrderL2; }}
		public bool  DisplayUnclickMessage { get { return ProdInfoCol.GetProd(id).DisplayUnclickMessage; }}
		public int PackageId
		{
			get { return package; }
			set { package = value; }
		}	
		public ProdSelectionState ProdSelState 
		{ 
			get { return pst;} 
			set { pst = value; }
		}
		public bool Locked
		{
			get { return locked; }
			set { locked = value; }
		}
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int ProdId
		{
			get { return id; }
			set { id = value; }
		}	
		public decimal UnitPrice
		{
			get { return unitPrice; }
		}
		public decimal PriceAmt // value for the first month 
		{
			get { return this.StartServMon == 1 ? unitPrice : 0; }
		}
		public bool IsAgentVisible
		{
			get { return ProdInfoCol.GetProd(id).IsAgentVisible; }
		}
		public string ProdType
		{
			get { return ProdInfoCol.GetProd(id).ProdType; }
		}
		public string ProdName
		{
			get 
			{ 
				return ProdInfoCol.GetProd(id).ProdName;
				// return id.ToString() + '-' + ProdInfoCol.GetProd(id).ProdName;
			}
		}
		public string BillText 
		{
			get 
			{ 
				//if (Conn.Env == Const.PROD)
				return ProdInfoCol.GetProd(id).BillText; 
		
				//	return id.ToString() + '-' + ProdInfoCol.GetProd(id).BillText; 
			}
		}
		public string Description
		{
			get { return  ProdInfoCol.GetProd(id).Description; }
		}
		public decimal NominalPrice
		{
			get { return unitPrice; }
		}
		public bool IsPurchased { get { return isPurchased; }}

		public string TaxCode
		{
			get { return  ProdInfoCol.GetProd(id).TaxCode; }
		}
		public string ProvCategory
		{
			get { return  ProdInfoCol.GetProd(id).ProvCategory; }
		}
		public bool IsRecurring
		{
			get { return isRecurring; }
		}
		public string PriceRule
		{
			get { return priceRule; }
		}
		//        public int PriceRuleId { get { return priceRuleId; } }
		public bool IsProvisionable
		{
			get { return  ProdInfoCol.GetProd(id).IsProvisionable; }
		}
		public bool IsProvViaMapping
		{
			get { return  ProdInfoCol.GetProd(id).IsProvViaMapping; }
		}
		public bool IsInstallForEachInstance
		{
			get { return  ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass).IsInstallForEachInstance; }
		}
		public bool IsRestrictedToOneInstance
		{
			get { return  ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass).IsRestrictedToOneInstance; }
		}
		public string FulfillMethod
		{
			get { return  ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass).FulfillMethod; }
		}
		public bool SuppressZeroPrice
		{
			get { return  ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass).SuppressZeroPrice; }
		}
		public bool SuppressOnWebReceipt
		{
			get { return  ProdSubClassCol.GetSubClass(ProdInfoCol.GetProd(id).ProdSubClass).SuppressOnWebReceipt; }
		}
		public string ProdSubclass
		{
			get { return  ProdInfoCol.GetProd(id).ProdSubClass; }
		}
		public string OrdSumryStartMon2
		{
			get { return  ProdInfoCol.GetProd(id).OrdSumryStartMon2; }	
		}
		public int StartServMon
		{
			get { return  ProdInfoCol.GetProd(id).StartServMon; }
		}
		public int EndServMon
		{
			get { return  ProdInfoCol.GetProd(id).EndServMon; }
		}
		public int ExclusiveSupplier
		{
			get { return  ProdInfoCol.GetProd(id).Supplier; }
		}
		//        public string EligibilityCriteria
		//        {
		//            get { return  ProdInfoCol.GetProd(id).EligibilityCriteria; }
		//       }
		public string PriceType
		{
			get { return priceType; }
		}
		//suppressZeroPriceProd
		public bool SuppressZeroPriceProd
		{
			get { return  ProdInfoCol.GetProd(id).SuppressZeroPriceProd; }
		}

        /*        Constructors			*/
		public ProdPrice()
        {
//            sql = new ProdPriceSQL();
  //          state = RowState.New;
			//pkgComponents = new ArrayList();
        }
        public ProdPrice(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
			//pkgComponents = new ArrayList();
		}
        
		public ProdPrice(
			int id,
			int package,
			bool isPurchased,
			decimal unitPrice,
			bool isRecurring, 
			string priceRule,
			string priceType, 
			ProdSelectionState pst,
			bool locked)
		{
			// need this contructor so we can build customized ProdPrices for components.  See OrderedProduct...
			this.id = id;
			this.package = package;
			this.isPurchased = isPurchased;
			this.unitPrice = unitPrice;
			this.isRecurring = isRecurring; 
			this.priceRule = priceRule;
			this.priceType = priceType; 
			this.pst = pst;
			this.locked = locked;
		}
		
		/*        Methods        */

		public override string ToString()
		{
			return BillText;
		}

/**
		public void ClearComponents()
		{
			pkgComponents.Clear();
		}
		public void AddPkgComponent(ProdPrice pp)
		{
			ProdPrice comp= (ProdPrice)pp.MemberwiseClone();
			comp.priceRule=null;
			comp.unitPrice = 0;
			pkgComponents.Add(comp);
		}
**/
        /*		Static methods		*/
/*		public static IProdPrice[] Conv(IOrderedProduct[] prods)
		{
			IProdPrice[] app = new ProdPrice[prods.Length];
			
			for (int i = 0; i < app.Length; i++)
				app[i] = Conv(prods[i]);
			
			return app;
		}
*/
		public static IProdPrice[] Conv(IOrderedProduct[] prods)
		{
			ArrayList prodlist = new ArrayList();
			IProdPrice[] components, fees;

			for (int i = 0; i < prods.Length; i++)
			{
				IProdPrice op = Conv(prods[i]);
				prodlist.Add(op);
				components = prods[i].Components;  // for speed, so OrderedProduct doesn't have to convert multiple times.
				for (int j = 0; j < components.Length; j++)
				{
					IProdPrice comp = components[j];  
					comp.PackageId = op.ProdId;
					prodlist.Add(comp);
				}
				fees = prods[i].Fees;
				for (int j = 0; j < fees.Length; j++)
				{
					IProdPrice fee = fees[j]; 
//					fee.PackageId = op.ProdId; // removed per A.M.
					prodlist.Add(fee); 
				}
			}
			return (IProdPrice[])prodlist.ToArray(typeof(IProdPrice));
		}
		public static IProdPrice Conv(IOrderedProduct prods)
		{
            return prods.Prod;
		}
       
		public static ProdPrice[] ToProdPrice(IProdPrice[] prods)
		{	
			ProdPrice[] pp =  new ProdPrice[prods.Length]; 
			for (int i = 0; i < pp.Length; i++)
				pp[i] = (ProdPrice)prods[i];
			
			return pp;
		}
		public static ProdPrice[] getAvaProdForZip(UOW uow, int loc, int ilec, string role, string storeCode)
        {
             return new ProdPriceSQL().getAll(uow, loc, ilec, role, storeCode);
        }
		public static IKeyVal[] GetAllProdDiscounts(UOW uow, string orderType)
		{
			return new ProdPriceSQL().GetAllProdDiscounts(uow, orderType);
		}
		public static ProdPrice[] getAvaProdTest(UOW uow)
		{
			return new ProdPriceSQL().getAll(uow);
		}
		public static ProdPrice getPriceForProd(UOW uow, int prodid, int loc, int ilec)
		{
			//Console.WriteLine("getPriceForProd(uow, {0}, {1}, {2})",prodid, loc, ilec);
			ProdPrice pp = new PriceSQL().getPriceForProd(uow, prodid, loc, ilec);
			if (pp != null)
			{
				Product prod = Product.find(uow, prodid);
				//				ProdSubClass sc = ProdSubClass.getKey(prod.ProdSubClass);  //  find(uow, prod.ProdSubClass);

				//				pp.Description=prod.Description;
				//				pp.EndServMon=prod.EndServMon;
				//				pp.FulfillMethod=sc.FulfillMethod;
				//				pp.IsInstallForEachInstance=sc.IsInstallForEachInstance;
				//				pp.IsProvisionable=prod.IsProvisionable;
				//				pp.IsProvViaMapping=prod.IsProvViaMapping;
				//				pp.IsRestrictedToOneInstance=sc.IsRestrictedToOneInstance;
				//				pp.ProdName=prod.ProdName;
				//				pp.ProdSubclass=prod.ProdSubClass;
				//				pp.ProdType=prod.ProdType;
				//				pp.ProvCategory=prod.ProvCategory;
				//				pp.StartServMon=prod.StartServMon;
				//				pp.TaxCode=prod.TaxCode;
			}
			else
			{
				Console.WriteLine(" ****** Couldn't find {0}?!?", prodid);
			}
			return pp;
		}

		/*		Implementation		*/
   
        /*		SQL		*/
        class ProdPriceSQL 
        {
			public ProdPrice[] getAll(UOW uow)
			{
				//throw new ApplicationException("Should not be here!!!");
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandType = CommandType.Text;        
				cmd.CommandText = "Select * from dbo.viewProdPriceTest";
				return execReader(cmd);
			}
  
            public ProdPrice[] getAll(UOW uow, int loc, int ilec, string role, string storeCode)
            {
                SqlCommand cmd = makeCommand(uow);

				cmd.CommandText = "dbo.spOrder_Web_GetProductsAvailable";  
				cmd.Parameters.Add("@LocId",  SqlDbType.Int, 0).Value      = loc;              
				cmd.Parameters.Add("@IlecId", SqlDbType.Int, 0).Value      = ilec;
                cmd.Parameters.Add("@Role",   SqlDbType.VarChar, 50).Value = role; 
				cmd.Parameters.Add("@StoreCode",   SqlDbType.VarChar, 10).Value = storeCode; 
				                
				return execReader(cmd);
            }
			public IKeyVal[] GetAllProdDiscounts(UOW uow, string orderType)
			{
				SqlCommand cmd = makeCommand(uow);

				cmd.CommandText = "dbo.spOrderGetDiscountAmount";  
				cmd.Parameters.Add("@OrderType",  SqlDbType.VarChar, 10).Value      = orderType;              
				                
				return execReader1(cmd);

			}
            /*        Implementation        */
			ProdPrice[] execReader(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();

				try
				{
					while(rdr.Read())
					{
						ProdPrice pp =  reader(rdr);
						if (pp != null)
							ar.Add(pp);
					}
					ProdPrice[] recs = new ProdPrice[ar.Count];
					ar.CopyTo(recs);
					return recs;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}
			IKeyVal[] execReader1(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();

				try
				{
					while(rdr.Read())
					{
						IKeyVal kv =  reader1(rdr);
						if (kv != null)
							ar.Add(kv);
					}
					IKeyVal[] recs = new IKeyVal[ar.Count];
					ar.CopyTo(recs);
					return recs;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}
            void setParam(SqlCommand cmd, ProdPrice rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
     /*           if (rec.prodType == null)
                    cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ProdType.Length == 0)
                        cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 50).Value = rec.prodType;
                }
 
                if (rec.prodName == null)
                    cmd.Parameters.Add("@ProdName", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ProdName.Length == 0)
                        cmd.Parameters.Add("@ProdName", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdName", SqlDbType.VarChar, 50).Value = rec.prodName;
                }
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 4000).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 4000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 4000).Value = rec.description;
                }
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal, 0).Value = rec.unitPrice;
                cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = rec.loc;
 
                if (rec.taxCode == null)
                    cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = DBNull.Value;
                else
                {
                    if (rec.TaxCode.Length == 0)
                        cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@TaxCode", SqlDbType.VarChar, 2).Value = rec.taxCode;
                }
 
                cmd.Parameters.Add("@IsRecurring", SqlDbType.Char, 1).Value = (rec.isRecurring == true) ? "T" : "F";
 
                if (rec.priceRule == null)
                    cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.PriceRule.Length == 0)
                        cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 50).Value = rec.priceRule;
                }
                cmd.Parameters.Add("@PriceRuleId", SqlDbType.Int, 0).Value = rec.priceRuleId;
 
                cmd.Parameters.Add("@IsProvisionable", SqlDbType.Char, 1).Value = (rec.isProvisionable == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsProvViaMapping", SqlDbType.Char, 1).Value = (rec.isProvViaMapping == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsInstallForEachInstance", SqlDbType.Char, 1).Value = (rec.isInstallForEachInstance == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsRestrictedToOneInstance", SqlDbType.Char, 1).Value = (rec.isRestrictedToOneInstance == true) ? "T" : "F";
 
                if (rec.fulfillMethod == null)
                    cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.FulfillMethod.Length == 0)
                        cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FulfillMethod", SqlDbType.VarChar, 50).Value = rec.fulfillMethod;
                }
 
                if (rec.prodSubclass == null)
                    cmd.Parameters.Add("@ProdSubclass", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ProdSubclass.Length == 0)
                        cmd.Parameters.Add("@ProdSubclass", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProdSubclass", SqlDbType.VarChar, 50).Value = rec.prodSubclass;
                }
                cmd.Parameters.Add("@StartServMon", SqlDbType.Int, 0).Value = rec.startServMon;
                cmd.Parameters.Add("@EndServMon", SqlDbType.Int, 0).Value = rec.endServMon;
                cmd.Parameters.Add("@ExclusiveSupplier", SqlDbType.Int, 0).Value = rec.exclusiveSupplier;
 
                if (rec.eligibilityCriteria == null)
                    cmd.Parameters.Add("@EligibilityCriteria", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.EligibilityCriteria.Length == 0)
                        cmd.Parameters.Add("@EligibilityCriteria", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@EligibilityCriteria", SqlDbType.VarChar, 50).Value = rec.eligibilityCriteria;
                }
 
                if (rec.priceType == null)
                    cmd.Parameters.Add("@PriceType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.PriceType.Length == 0)
                        cmd.Parameters.Add("@PriceType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PriceType", SqlDbType.VarChar, 50).Value = rec.priceType;
              }
*/          }

             protected ProdPrice reader(SqlDataReader rdr)
            {
                ProdPrice rec = new ProdPrice();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
   //             if (rdr["ProdType"] != DBNull.Value)
   //                 rec.prodType = (string) rdr["ProdType"];
 
   //             if (rdr["ProdName"] != DBNull.Value)
   //                 rec.prodName = (string) rdr["ProdName"];
 
   //             if (rdr["Description"] != DBNull.Value)
   //                 rec.description = (string) rdr["Description"];
 
				 if (rdr["UnitPrice"] == DBNull.Value)
					return null;

				 if (rdr["UnitPrice"] != DBNull.Value)
					 rec.unitPrice = Decimal.Round((decimal)rdr["UnitPrice"], 2);
 
     //           if (rdr["Loc"] != DBNull.Value)
     //               rec.loc = (int) rdr["Loc"];
 
    //            if (rdr["TaxCode"] != DBNull.Value)
     //               rec.taxCode = (string) rdr["TaxCode"];
 
                if (rdr["IsRecurring"] != DBNull.Value)
                    rec.isRecurring = (string) rdr["IsRecurring"] == "T" ?  true : false;
 
                if (rdr["PriceRule"] != DBNull.Value)
                    rec.priceRule = (string) rdr["PriceRule"];
 
         //       if (rdr["PriceRuleId"] != DBNull.Value)
         //           rec.priceRuleId = (int) rdr["PriceRuleId"];
 
     //           if (rdr["IsProvisionable"] != DBNull.Value)
     //               rec.isProvisionable = (string) rdr["IsProvisionable"] == "T" ?  true : false;
 
       //         if (rdr["IsProvViaMapping"] != DBNull.Value)
         //           rec.isProvViaMapping = (string) rdr["IsProvViaMapping"] == "T" ?  true : false;
 
       //         if (rdr["IsInstallForEachInstance"] != DBNull.Value)
         //           rec.isInstallForEachInstance = (string) rdr["IsInstallForEachInstance"] == "T" ?  true : false;
 
         //       if (rdr["IsRestrictedToOneInstance"] != DBNull.Value)
         //           rec.isRestrictedToOneInstance = (string) rdr["IsRestrictedToOneInstance"] == "T" ?  true : false;
 
      //          if (rdr["FulfillMethod"] != DBNull.Value)
       //             rec.fulfillMethod = (string) rdr["FulfillMethod"];
 
         //       if (rdr["ProdSubclass"] != DBNull.Value)
 //                   rec.prodSubclass = (string) rdr["ProdSubclass"];
 
   //             if (rdr["StartServMon"] != DBNull.Value)
     //               rec.startServMon = (int) rdr["StartServMon"];
 
      //          if (rdr["EndServMon"] != DBNull.Value)
       //             rec.endServMon = (int) rdr["EndServMon"];
 
       //         if (rdr["ExclusiveSupplier"] != DBNull.Value)
       //             rec.exclusiveSupplier = (int) rdr["ExclusiveSupplier"];
 
       //         if (rdr["EligibilityCriteria"] != DBNull.Value)
       //             rec.eligibilityCriteria = (string) rdr["EligibilityCriteria"];
 
                if (rdr["PriceType"] != DBNull.Value)
                    rec.priceType = (string) rdr["PriceType"];
                return rec;
            }
			protected IKeyVal reader1(SqlDataReader rdr)
			{
				IKeyVal rec = new KeyVal();
                
				if (rdr["OrderedProduct"] != DBNull.Value)
					rec.Key = ((int)rdr["OrderedProduct"]).ToString();

				if (rdr["DiscountAmount"] != DBNull.Value)
					rec.Val = Decimal.Round((decimal)rdr["DiscountAmount"], 2).ToString();
 			
				return rec;
			}
			SqlCommand makeCommand(UOW uow)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				return cmd;
			}
        }
		class PriceSQL 
		{
			public ProdPrice getPriceForProd(UOW uow, int prodid, int loc, int ilec)
			{
				ProdPrice pp;

				SqlCommand cmd = makeCommand(uow);
                
				cmd.CommandText = "dbo.spOrder_GetPriceAtLoc";

				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value = loc;
				cmd.Parameters.Add("@pid",SqlDbType.Int, 0).Value = prodid; 
				cmd.Parameters.Add("@Supplier", SqlDbType.Int, 0).Value = ilec;
				                
				pp = execReader(cmd);

				pp.id=prodid;
				return pp;
			}
				/*        Implementation        */
			ProdPrice execReader(SqlCommand cmd)
			{
				ProdPrice pp=null;
				SqlDataReader rdr = cmd.ExecuteReader();

				try
				{
					if(rdr.Read())
						pp=reader(rdr);

					return pp;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();

				}
			}
			void setParam(SqlCommand cmd, ProdPrice rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
			}

			protected ProdPrice reader(SqlDataReader rdr)
			{
				ProdPrice rec = new ProdPrice();
/*                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
*/ 
				if (rdr["UnitPrice"] != DBNull.Value)
					rec.unitPrice = Decimal.Round((decimal)rdr["UnitPrice"], 2);
 /*
				if (rdr["Loc"] != DBNull.Value)
					rec.loc = (int) rdr["Loc"];
 */
				if (rdr["IsRecurring"] != DBNull.Value)
					rec.isRecurring = (string) rdr["IsRecurring"] == "T" ?  true : false;
 
				if (rdr["PriceRule"] != DBNull.Value)
					rec.priceRule = (string) rdr["PriceRule"];
 
			//	if (rdr["PriceRuleId"] != DBNull.Value)
			//		rec.priceRuleId = (int) rdr["PriceRuleId"];
 
	 
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
		}
	}
}
