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
    public class ProdAvailability : DomainObj
    {
        /*        Data        */
        static string iName = "ProdAvailability";
        protected int id;
        protected int prod;
        protected int loc;
        protected string priceRule;
        protected string descr;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public int Prod
        {
            get { return prod; }
            set
            {
                setState();
                prod = value;
            }
        }
        public int Loc
        {
            get { return loc; }
            set
            {
                setState();
                loc = value;
            }
        }
        public string PriceRule
        {
            get { return priceRule; }
            set
            {
                setState();
                priceRule = value;
            }
        }
        public string Descr
        {
            get { return descr; }
            set
            {
                setState();
                descr = value;
            }
        }
        
        /*        Constructors			*/
        public ProdAvailability()
        {
            sql = new ProdAvailabilitySQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public ProdAvailability(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new ProdAvailabilitySQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ProdAvailability find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ProdAvailability.getKey(id)))
                return (ProdAvailability)uow.Imap.find(ProdAvailability.getKey(id));
            
            ProdAvailability cls = new ProdAvailability();
            cls.uow = uow;
            cls.id = id;
            cls = (ProdAvailability)DomainObj.addToIMap(uow, getOne(((ProdAvailabilitySQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ProdAvailability[] getAll(UOW uow)
        {
            ProdAvailability[] objs = (ProdAvailability[])DomainObj.addToIMap(uow, (new ProdAvailabilitySQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
//		public static ProdAvailability[] GetForZip(UOW uow, int loc, int ilec, bool isAgentVisible, string orderType)
//		{
//			ProdAvailabilityExt[] objs = 
//				(ProdAvailability[])DomainObj.addToIMap(uow, 
//				 (new ProdAvailabilityExtSQL()).GetForZip(uow, loc, ilec, Const.DPI, isAgentVisible, orderType));
//			
//			for (int i = 0; i < objs.Length; i++)
//				objs[i].uow = uow;
//
//			return MappingFilter(FilterOutDups(objs), ilec);
//		}

        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
		static ProdAvailability[] FilterOutDups(ProdAvailability[] pas)
		{
			Hashtable hashtable = new Hashtable(pas.Length * 2);
			ArrayList ar = new ArrayList(pas.Length);

			for (int i = 0; i < pas.Length; i++)
				if (!hashtable.ContainsKey(pas[i].prod))
				{
					ar.Add(pas[i]);
					hashtable.Add(pas[i].prod, pas[i]);
				}
				else
				{
					Console.WriteLine("Duplicate product {0}", pas[i].prod);
				}

			ProdAvailability[] pas2 = new ProdAvailability[ar.Count];
			ar.CopyTo(pas2);
			return pas2;
		}
		static ProdAvailability[] MappingFilter(ProdAvailability[] candidates, int ilec)
		{
			ProdInfo[] pis = GetProdInfo(candidates);
			ArrayList ar = new ArrayList();
			for (int i = 0; i < pis.Length; i++)
			{
				ProdInfo[] comps = GetChildrenPI(pis[i]);
				bool ok = true;
				for (int j = 0; j < comps.Length; j++)
				{ 
					if (!MappingExists(comps[j], ilec, candidates))
					{
						ok = false;
						break;
					}
				}
				if (ok)
					ar.Add(candidates[i]);
				else
					Console.WriteLine("Fail mapping {0} {1}, supplier {2}", pis[i].Id.ToString(),
						pis[i].ProdName, pis[i].Supplier.ToString()); 
			}
			
			ProdAvailability[] ava = new ProdAvailability[ar.Count];
			ar.CopyTo(ava);
			return ava;
		}

		static bool MappingExists(ProdInfo pi, int ilec, ProdAvailability[] availProds)
		{
			if (!pi.IsProvViaMapping)
				return true;
			
			ProdComposition[] mapped = ProdInfoCol.getAllComps(pi.Id, "Map");
			
			for (int i = 0; i < mapped.Length; i++)
				if ( ProdInfoCol.GetProd(mapped[i].SubProd).Supplier == ilec)
					for (int j = 0; j < availProds.Length; j++)
						if (mapped[i].SubProd == availProds[j].prod)
							return true;

			return false;
		}

		static ProdInfo[] GetProdInfo(ProdAvailability[] cands)
		{
			ArrayList ar = new ArrayList();

			for (int i = 0; i < cands.Length; i++)
				ar.Add(ProdInfoCol.GetProd(cands[i].prod));

			ProdInfo[] pis = new ProdInfo[ar.Count];
			ar.CopyTo(pis);
			return pis;
		}

		static ProdInfo[] GetChildrenPI(ProdInfo cand)
		{
			ArrayList ar = new ArrayList();
			ar.Add(cand);

			ProdComposition[] comps = ProdInfoCol.getAllPackageComps(cand.Id);
			for (int i = 0; i < comps.Length; i++)
					ar.Add(ProdInfoCol.GetProd(comps[i].SubProd));

			ProdInfo[] pis = new ProdInfo[ar.Count];
			ar.CopyTo(pis);
			return pis;
		}
        static ProdAvailability getOne(ProdAvailability[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ProdAvailability src, ProdAvailability tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.prod = src.prod;
            tar.loc = src.loc;
            tar.priceRule = src.priceRule;
            tar.descr = src.descr;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        protected class ProdAvailabilitySQL : SqlGateway
        {
            public ProdAvailability[] getKey(ProdAvailability rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdAvailability_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ProdAvailability rec = (ProdAvailability)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdAvailability_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProdAvailability rec = (ProdAvailability)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdAvailability_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProdAvailability rec = (ProdAvailability)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdAvailability_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
//			public ProdAvailability[] GetForZip(UOW uow, int loc, int ilec, int dpi, bool isAgentVisible,string orderType)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				
//				cmd.CommandText = "dbo.spOrder_GetProductsAvailable2";
//	
//				cmd.Parameters.Add("@Ilec", SqlDbType.Int, 0).Value = ilec;
//				cmd.Parameters.Add("@Dpi", SqlDbType.Int, 0).Value  = dpi;
//				cmd.Parameters.Add("@LocId", SqlDbType.Int, 0).Value  = loc;
//				cmd.Parameters.Add("@IsAgentVisible", SqlDbType.VarChar, 1).Value  
//					= isAgentVisible ? "T" : "F";
//				cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 20).Value  = orderType;
//				
//				return convert(execReader(cmd));
//			}
			
            public ProdAvailability[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProdAvailability_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProdAvailability rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Prod == 0)
                    cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = rec.prod;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Loc == 0)
                    cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Loc", SqlDbType.Int, 0).Value = rec.loc;
 
                if (rec.priceRule == null)
                    cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.PriceRule.Length == 0)
                        cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = rec.priceRule;
                }
 
                if (rec.descr == null)
                    cmd.Parameters.Add("@Descr", SqlDbType.VarChar, 200).Value = DBNull.Value;
                else
                {
                    if (rec.Descr.Length == 0)
                        cmd.Parameters.Add("@Descr", SqlDbType.VarChar, 200).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Descr", SqlDbType.VarChar, 200).Value = rec.descr;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProdAvailability rec = new ProdAvailability();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Prod"] != DBNull.Value)
                    rec.prod = (int) rdr["Prod"];
 
                if (rdr["Loc"] != DBNull.Value)
                    rec.loc = (int) rdr["Loc"];
 
                if (rdr["PriceRule"] != DBNull.Value)
                    rec.priceRule = (string) rdr["PriceRule"];
 
                if (rdr["Descr"] != DBNull.Value)
                    rec.descr = (string) rdr["Descr"];

				
                rec.rowState = RowState.Clean;
                return rec;
            }
            ProdAvailability[] convert(DomainObj[] objs)
            {
                ProdAvailability[] acls  = new ProdAvailability[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
