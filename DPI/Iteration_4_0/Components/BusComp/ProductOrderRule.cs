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
    public class ProductOrderRule : DomainObj, IProductOrderRule
    {
        /*        Data        */
        static string iName = "ProductOrderRule";
        int id;
        int product;
        string dmdType;
        decimal minAmt;
        decimal maxAmt;
        int expirationPeriod;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
            set
            {
                setState();
                id = value;
            }
        }
        public int Product
        {
            get { return product; }
            set
            {
                setState();
                product = value;
            }
        }
        public string DmdType
        {
            get { return dmdType; }
            set
            {
                setState();
                dmdType = value;
            }
        }
        public decimal MinAmt
        {
            get { return minAmt; }
            set
            {
                setState();
                minAmt = Decimal.Round(value, 2);
            }
        }
        public decimal MaxAmt
        {
            get { return maxAmt; }
            set
            {
                setState();
                maxAmt = Decimal.Round(value, 2);
            }
        }
        public int ExpirationPeriod
        {
            get { return expirationPeriod; }
            set
            {
                setState();
                expirationPeriod = value;
            }
        }
        
        /*        Constructors			*/
        public ProductOrderRule()
        {
            sql = new ProductOrderRuleSQL();
            rowState = RowState.New;
        }
        public ProductOrderRule(UOW uow) : this()
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
            return new ProductOrderRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ProductOrderRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ProductOrderRule.getKey(id)))
                return (ProductOrderRule)uow.Imap.find(ProductOrderRule.getKey(id));
            
            ProductOrderRule cls = new ProductOrderRule();
            cls.uow = uow;
            cls.id = id;
            cls = (ProductOrderRule)DomainObj.addToIMap(uow, getOne(((ProductOrderRuleSQL)cls.Sql).getKey(cls)));
			
            cls.uow = uow;
            
            return cls;
        }
		public static ProductOrderRule find(UOW uow, string dmdType, int prod)
		{
			ProductOrderRule[] rules = 
				(ProductOrderRule[])DomainObj.addToIMap(uow, new ProductOrderRuleSQL().getByProdDmdType(uow, dmdType, prod));
			
			for (int i = 0; i < rules.Length; i++)
				rules[i].uow = uow;

			if (rules.Length > 0)
				return rules[0];
			
			return null;
		}
        public static ProductOrderRule[] getAll(UOW uow)
        {
            ProductOrderRule[] objs = (ProductOrderRule[])DomainObj.addToIMap(uow, (new ProductOrderRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static ProductOrderRule getOne(ProductOrderRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ProductOrderRule src, ProductOrderRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.product = src.product;
            tar.dmdType = src.dmdType;
            tar.minAmt = src.minAmt;
            tar.maxAmt = src.maxAmt;
            tar.expirationPeriod = src.expirationPeriod;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ProductOrderRuleSQL : SqlGateway
        {
            public ProductOrderRule[] getKey(ProductOrderRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductOrderRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
			public ProductOrderRule[] getByProdDmdType(UOW uow, string dmdType, int prod)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spProductOrderRule_Get_Prod_DmdType";
				cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = prod;
				cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = dmdType;
				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                ProductOrderRule rec = (ProductOrderRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductOrderRule_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProductOrderRule rec = (ProductOrderRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductOrderRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProductOrderRule rec = (ProductOrderRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductOrderRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ProductOrderRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProductOrderRule_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProductOrderRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Product == 0)
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = rec.product;
 
                if (rec.dmdType == null)
                    cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.DmdType.Length == 0)
                        cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = rec.dmdType;
                }
                cmd.Parameters.Add("@MinAmt", SqlDbType.Decimal, 0).Value = rec.minAmt;
                cmd.Parameters.Add("@MaxAmt", SqlDbType.Decimal, 0).Value = rec.maxAmt;
                cmd.Parameters.Add("@ExpirationPeriod", SqlDbType.Int, 0).Value = rec.expirationPeriod;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProductOrderRule rec = new ProductOrderRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Product"] != DBNull.Value)
                    rec.product = (int) rdr["Product"];
 
                if (rdr["DmdType"] != DBNull.Value)
                    rec.dmdType = (string) rdr["DmdType"];
 
                if (rdr["MinAmt"] != DBNull.Value)
                    rec.minAmt = Decimal.Round((decimal)rdr["MinAmt"], 2);
 
                if (rdr["MaxAmt"] != DBNull.Value)
                    rec.maxAmt = Decimal.Round((decimal)rdr["MaxAmt"], 2);
 
                if (rdr["ExpirationPeriod"] != DBNull.Value)
                    rec.expirationPeriod = (int) rdr["ExpirationPeriod"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ProductOrderRule[] convert(DomainObj[] objs)
            {
                ProductOrderRule[] acls  = new ProductOrderRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
