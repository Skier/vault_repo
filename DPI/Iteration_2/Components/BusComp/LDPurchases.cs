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
    public class LDPurchases : DomainObj
    {
        /*        Data        */
        static string iName = "LDPurchases";
        int			lDPurchases_ID;
        int			accNumber;
        decimal		amount;
        DateTime	purchase_Date;
        bool		ordered;
        string		lD_Type;
        string		lD_Product;
		int			productId;
		string		providerSKU;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, lDPurchases_ID.ToString()); }
        }
        public int LDPurchases_ID
        {
            get { return lDPurchases_ID; }
        }
        public int AccNumber
        {
            get { return accNumber; }
            set
            {
                setState();
                accNumber = value;
            }
        }
        public decimal Amount
        {
            get { return amount; }
            set
            {
                setState();
                amount = Decimal.Round(value, 2);
            }
        }
        public DateTime Purchase_Date
        {
            get { return purchase_Date; }
            set
            {
                setState();
                purchase_Date = value;
            }
        }
        public bool Ordered
        {
            get { return ordered; }
            set
            {
                setState();
                ordered = value;
            }
        }
        public string LD_Type
        {
            get { return lD_Type; }
            set
            {
                setState();
                lD_Type = value;
            }
        }
        public string LD_Product
        {
            get { return lD_Product; }
            set
            {
                setState();
                lD_Product = value;
            }
        }
		public int ProductId
		{
			get { return productId; }
			set
			{
				setState();
				productId = value;
			}
		}
		public string ProviderSKU
		{
			get { return providerSKU; }
			set
			{
				setState();
				providerSKU = value;
			}
		}
		
        /*        Constructors			*/
        public LDPurchases()
        {
            sql = new LDPurchasesSQL();
            lDPurchases_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public LDPurchases(UOW uow) : this()
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
            return new LDPurchasesSQL();
        }
        public override void checkExists()
        {
            if ((LDPurchases_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static LDPurchases find(UOW uow, int lDPurchases_ID)
        {
            if (uow.Imap.keyExists(LDPurchases.getKey(lDPurchases_ID)))
                return (LDPurchases)uow.Imap.find(LDPurchases.getKey(lDPurchases_ID));
            
            LDPurchases cls = new LDPurchases();
            cls.uow = uow;
            cls.lDPurchases_ID = lDPurchases_ID;
            cls = (LDPurchases)DomainObj.addToIMap(uow, getOne(((LDPurchasesSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static LDPurchases[] getAll(UOW uow)
        {
            LDPurchases[] objs = (LDPurchases[])DomainObj.addToIMap(uow, (new LDPurchasesSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int lDPurchases_ID)
        {
            return new Key(iName, lDPurchases_ID.ToString());
        }
        /*		Implementation		*/
        static LDPurchases getOne(LDPurchases[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(LDPurchases src, LDPurchases tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.lDPurchases_ID	= src.lDPurchases_ID;
            tar.accNumber		= src.accNumber;
            tar.amount			= src.amount;
            tar.purchase_Date	= src.purchase_Date;
            tar.ordered			= src.ordered;
            tar.lD_Type			= src.lD_Type;
            tar.lD_Product		= src.lD_Product;
			tar.productId		= src.productId;
			tar.providerSKU 	= src.providerSKU;

            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class LDPurchasesSQL : SqlGateway
        {
            public LDPurchases[] getKey(LDPurchases rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLDPurchases_Get_Id";
                cmd.Parameters.Add("@LDPurchases_ID", SqlDbType.Int, 0).Value = rec.lDPurchases_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                LDPurchases rec = (LDPurchases)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLDPurchases_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@LDPurchases_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.lDPurchases_ID = (int)cmd.Parameters["@LDPurchases_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                LDPurchases rec = (LDPurchases)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLDPurchases_Del_Id";
                cmd.Parameters.Add("@LDPurchases_ID", SqlDbType.Int, 0).Value = rec.lDPurchases_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                LDPurchases rec = (LDPurchases)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spLDPurchases_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public LDPurchases[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spLDPurchases_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, LDPurchases rec)
            {
                cmd.Parameters.Add("@LDPurchases_ID", SqlDbType.Int, 0).Value = rec.lDPurchases_ID;
                
                // Numeric, nullable foreign key treatment:
                if (rec.accNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
                
				cmd.Parameters.Add("@Amount", SqlDbType.Decimal, 0).Value = rec.amount;
 
                if (rec.purchase_Date == DateTime.MinValue)
					cmd.Parameters.Add("@Purchase_Date", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Purchase_Date", SqlDbType.DateTime, 0).Value = rec.purchase_Date;
 
                cmd.Parameters.Add("@Ordered", SqlDbType.Bit, 0).Value = rec.ordered;
 
				if (rec.lD_Type == null)
					cmd.Parameters.Add("@LD_Type", SqlDbType.VarChar, 1).Value = DBNull.Value;
				else
				{
					if (rec.lD_Type.Length == 0)
						cmd.Parameters.Add("@LD_Type", SqlDbType.VarChar, 1).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@LD_Type", SqlDbType.VarChar, 1).Value = rec.lD_Type;
				}

				if (rec.lD_Product == null)
					cmd.Parameters.Add("@LD_Product", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.lD_Product.Length == 0)				
						cmd.Parameters.Add("@LD_Product", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@LD_Product", SqlDbType.VarChar, 20).Value = rec.lD_Product;
				}

				if (rec.productId == 0)
					cmd.Parameters.Add("@ProductId", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@ProductId", SqlDbType.Int, 0).Value = rec.productId;
				
				if (rec.providerSKU == null)
					cmd.Parameters.Add("@ProviderSKU", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.providerSKU.Length == 0)
						cmd.Parameters.Add("@ProviderSKU", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ProviderSKU", SqlDbType.VarChar, 50).Value = rec.providerSKU;
				}

				
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                LDPurchases rec = new LDPurchases();
                
                if (rdr["LDPurchases_ID"] != DBNull.Value)
                    rec.lDPurchases_ID = (int) rdr["LDPurchases_ID"];
 
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["Amount"] != DBNull.Value)
                    rec.amount = Decimal.Round((decimal)rdr["Amount"], 2);
 
                if (rdr["Purchase_Date"] != DBNull.Value)
                    rec.purchase_Date = (DateTime) rdr["Purchase_Date"];
 
                if (rdr["Ordered"] != DBNull.Value)
                    rec.ordered = (bool) rdr["Ordered"];
 
                if (rdr["LD_Type"] != DBNull.Value)
                    rec.lD_Type = (string) rdr["LD_Type"];
 
                if (rdr["LD_Product"] != DBNull.Value)
                    rec.lD_Product = (string) rdr["LD_Product"];

				if (rdr["ProductId"] != DBNull.Value)
					rec.productId = (int) rdr["ProductId"];

				if (rdr["ProviderSKU"] != DBNull.Value)
					rec.providerSKU = (string) rdr["ProviderSKU"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            LDPurchases[] convert(DomainObj[] objs)
            {
                LDPurchases[] acls  = new LDPurchases[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
