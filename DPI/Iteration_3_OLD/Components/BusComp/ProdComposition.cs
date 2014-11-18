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
    public class ProdComposition : DomainObj
    {
        /*        Data        */
        static string iName = "ProdComposition";
		
		public const string MAPPING = "Map";
		public const string COMP = "Prod";
		public const string PREREQ = "PreReq";
		public const string DM = "DarkMatter";
		public const string RECURSIVE_PRE_REQ = "RecursPreReq";
		public const string TOP_ONLY_PRE_REQ = "TopOnlyPreReq";

        int id;
        int prod;
        int subProd;
        string compType;
	    string alternativeComp;
        int rev;
        string status;
        
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
        public int SubProd
        {
            get { return subProd; }
            set
            {
                setState();
                subProd = value;
            }
        }
        public string CompType
        {
            get { return compType; }
            set
            {
                setState();
                compType = value;
            }
        }
		public string AlternativeComp
		{
			get { return alternativeComp; }
			set
			{
				setState();
				alternativeComp = value;
			}
		}
        public int Rev
        {
            get { return rev; }
            set
            {
                setState();
                rev = value;
            }
        }
        public string Status
        {
            get { return status; }
            set
            {
                setState();
                status = value;
            }
        }
        
        /*        Constructors			*/
        public ProdComposition()
        {
            sql = new ProdCompositionSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public ProdComposition(UOW uow) : this()
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
            return new ProdCompositionSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ProdComposition   find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ProdComposition.getKey(id)))
                return (ProdComposition)uow.Imap.find(ProdComposition.getKey(id));
            
            ProdComposition cls = new ProdComposition();
            cls.uow = uow;
            cls.id = id;
            cls = (ProdComposition)DomainObj.addToIMap(uow, getOne(((ProdCompositionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ProdComposition[] getAll(UOW uow)
        {
            ProdComposition[] objs = (ProdComposition[])DomainObj.addToIMap(uow, (new ProdCompositionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static ProdComposition[] getMapProd(UOW uow, int parent)
		{
			ProdComposition[] objs 
				= (ProdComposition[])DomainObj.addToIMap(
					uow, (new ProdCompositionSQL()).findChildren(uow, parent, ProdComposition.MAPPING));
			
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			
			return objs;
		}
		public static ProdComposition[] getPackageComp(UOW uow, int parent, bool includeDM)
		{
			ArrayList ar = new ArrayList();
			ar.AddRange((ProdComposition[])DomainObj.addToIMap(
				uow, (new ProdCompositionSQL()).findChildren(uow, parent, ProdComposition.COMP)));
			
			if (includeDM)
				ar.AddRange((ProdComposition[])DomainObj.addToIMap(
					uow, (new ProdCompositionSQL()).findChildren(uow, parent, ProdComposition.DM)));
			
			ProdComposition[] objs = new ProdComposition[ar.Count];
 			ar.CopyTo(objs);
			
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
		
			return objs;
		}
		public static ProdComposition[] getAllVisiblePackageComp(UOW uow, int parent)
		{
			ArrayList ar = new ArrayList(50);

			getChildren(uow, parent, ar, false);
			ProdComposition[] children = new ProdComposition[ar.Count]; 
			ar.CopyTo(children);
			
			return children;
		}

		public static ProdComposition[] getAllPackageComp(UOW uow, int parent)
		{
			ArrayList ar = new ArrayList(50);

			getChildren(uow, parent, ar, true);
			ProdComposition[] children = new ProdComposition[ar.Count]; 
			ar.CopyTo(children);
			
			return children;
		}
		static void getChildren(UOW uow, int parent, ArrayList ar, bool includeBM)
		{
			ProdComposition[] children = getPackageComp(uow, parent, includeBM);
			if (children.Length > 0)
			{
				ar.AddRange(children);
				for (int i = 0; i < children.Length; i++)
					getChildren(uow, children[i].subProd, ar, includeBM);
			}
		}
		public static ProdComposition[] getPre(UOW uow, int parent)
		{
			ProdComposition[] objs 
				= (ProdComposition[])DomainObj.addToIMap(
					uow, (new ProdCompositionSQL()).findChildren(uow, parent, ProdComposition.COMP));
			
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			
			return objs;
		}

        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static ProdComposition getOne(ProdComposition[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ProdComposition src, ProdComposition tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.prod = src.prod;
            tar.subProd = src.subProd;
            tar.compType = src.compType;
            tar.alternativeComp = src.alternativeComp;
            tar.rev = src.rev;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ProdCompositionSQL : SqlGateway
        {
            public ProdComposition[] getKey(ProdComposition rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdComposition_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
			public ProdComposition[] findChildren(UOW uow, int parent, string compType)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spProdComposition_GetSubProd_CompType";
				cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = parent;
				cmd.Parameters.Add("@CompType", SqlDbType.VarChar, 20).Value = compType;

				 return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                ProdComposition rec = (ProdComposition)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdComposition_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProdComposition rec = (ProdComposition)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdComposition_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProdComposition rec = (ProdComposition)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProdComposition_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ProdComposition[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProdComposition_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProdComposition rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Prod == 0)
                    cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Prod", SqlDbType.Int, 0).Value = rec.prod;
                
                // Numeric, nullable foreign key treatment:
                if (rec.SubProd == 0)
                    cmd.Parameters.Add("@SubProd", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@SubProd", SqlDbType.Int, 0).Value = rec.subProd;
 
                cmd.Parameters.Add("@CompType", SqlDbType.VarChar, 20).Value = rec.compType;
                cmd.Parameters.Add("@Rev", SqlDbType.Int, 0).Value = rec.rev;
  
				if (rec.alternativeComp == null)
					cmd.Parameters.Add("@AlternativeComp", SqlDbType.VarChar, 8).Value = DBNull.Value;
				else
				{
					if (rec.AlternativeComp.Length == 0)
						cmd.Parameters.Add("@AlternativeComp", SqlDbType.VarChar, 8).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@AlternativeComp", SqlDbType.VarChar, 8).Value = rec.alternativeComp;
				}

                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProdComposition rec = new ProdComposition();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Prod"] != DBNull.Value)
                    rec.prod = (int) rdr["Prod"];
 
                if (rdr["SubProd"] != DBNull.Value)
                    rec.subProd = (int) rdr["SubProd"];
 
                if (rdr["CompType"] != DBNull.Value)
                    rec.compType = (string) rdr["CompType"];

				if (rdr["AlternativeComp"] != DBNull.Value)
					rec.alternativeComp = (string) rdr["AlternativeComp"];
 
                if (rdr["Rev"] != DBNull.Value)
                    rec.rev = (int) rdr["Rev"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ProdComposition[] convert(DomainObj[] objs)
            {
                ProdComposition[] acls  = new ProdComposition[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
