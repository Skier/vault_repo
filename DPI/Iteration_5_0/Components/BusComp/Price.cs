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
    public class Price : DomainObj
    {
        /*        Data        */
        static string iName = "Price";
        int price_ID;
        string priceRule;
        int revision;
        string priceType;
        string priceCat;
        int product;
        string supplierPriceClass;
        string excOrderType;
        float packDisc;
        DateTime startDate;
        DateTime endDate;
        int unitQuantity;
        decimal unitPrice;
        string uOM;
        string status;
        bool isRecurring;
        bool isUsage;
        int usageBillingThreshold;
        int exclusiveSupplier;
        bool isRevClosed;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, price_ID.ToString()); }
        }
        public int Price_ID
        {
            get { return price_ID; }
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
        public int Revision
        {
            get { return revision; }
            set
            {
                setState();
                revision = value;
            }
        }
        public string PriceType
        {
            get { return priceType; }
            set
            {
                setState();
                priceType = value;
            }
        }
        public string PriceCat
        {
            get { return priceCat; }
            set
            {
                setState();
                priceCat = value;
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
        public string SupplierPriceClass
        {
            get { return supplierPriceClass; }
            set
            {
                setState();
                supplierPriceClass = value;
            }
        }
        public string ExcOrderType
        {
            get { return excOrderType; }
            set
            {
                setState();
                excOrderType = value;
            }
        }
        public float PackDisc
        {
            get { return packDisc; }
            set
            {
                setState();
                packDisc = value;
            }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                setState();
                startDate = value;
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                setState();
                endDate = value;
            }
        }
        public int UnitQuantity
        {
            get { return unitQuantity; }
            set
            {
                setState();
                unitQuantity = value;
            }
        }
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set
            {
                setState();
                unitPrice = Decimal.Round(value, 2);
            }
        }
        public string UOM
        {
            get { return uOM; }
            set
            {
                setState();
                uOM = value;
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
        public bool IsRecurring
        {
            get { return isRecurring; }
            set
            {
                setState();
                isRecurring = value;
            }
        }
        public bool IsUsage
        {
            get { return isUsage; }
            set
            {
                setState();
                isUsage = value;
            }
        }
        public int UsageBillingThreshold
        {
            get { return usageBillingThreshold; }
            set
            {
                setState();
                usageBillingThreshold = value;
            }
        }
        public int ExclusiveSupplier
        {
            get { return exclusiveSupplier; }
            set
            {
                setState();
                exclusiveSupplier = value;
            }
        }
        public bool IsRevClosed
        {
            get { return isRevClosed; }
            set
            {
                setState();
                isRevClosed = value;
            }
        }
        
        /*        Constructors			*/
        public Price()
        {
            sql = new PriceSQL();
            price_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Price(UOW uow) : this()
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
            return new PriceSQL();
        }
        public override void checkExists()
        {
            if ((Price_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Price find(UOW uow, int price_ID)
        {
            if (uow.Imap.keyExists(Price.getKey(price_ID)))
                return (Price)uow.Imap.find(Price.getKey(price_ID));
            
            Price cls = new Price();
            cls.uow = uow;
            cls.price_ID = price_ID;
            cls = (Price)DomainObj.addToIMap(uow, getOne(((PriceSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
//		getPriceRule(UOW uow, string priceRule)
		
		public static Price getPriceRule(UOW uow, string priceRule)
		{
			Price[] objs = (Price[])DomainObj.addToIMap(uow, (new PriceSQL()).getPriceRule(uow, priceRule));
			if (objs.Length == 0)
				return null;

			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			
			return objs[0];
		}

		public static Price[] getAll(UOW uow)
        {
            Price[] objs = (Price[])DomainObj.addToIMap(uow, (new PriceSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int price_ID)
        {
            return new Key(iName, price_ID.ToString());
        }
		public static bool IsSupplierRequired(UOW uow, string priceRule)
		{
			if (priceRule == null)
				throw new ArgumentException("PriceRule is required");

			if (priceRule.Trim().Length == 0)
				throw new ArgumentException("PriceRule is required");

			return new PriceSQL().IsSupplierRequired(uow, priceRule);
		}
		/*		Implementation		*/
        static Price getOne(Price[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Price src, Price tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.price_ID = src.price_ID;
            tar.priceRule = src.priceRule;
            tar.revision = src.revision;
            tar.priceType = src.priceType;
            tar.priceCat = src.priceCat;
            tar.product = src.product;
            tar.supplierPriceClass = src.supplierPriceClass;
            tar.excOrderType = src.excOrderType;
            tar.packDisc = src.packDisc;
            tar.startDate = src.startDate;
            tar.endDate = src.endDate;
            tar.unitQuantity = src.unitQuantity;
            tar.unitPrice = src.unitPrice;
            tar.uOM = src.uOM;
            tar.status = src.status;
            tar.isRecurring = src.isRecurring;
            tar.isUsage = src.isUsage;
            tar.usageBillingThreshold = src.usageBillingThreshold;
            tar.exclusiveSupplier = src.exclusiveSupplier;
            tar.isRevClosed = src.isRevClosed;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class PriceSQL : SqlGateway
        {
            public Price[] getKey(Price rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPrice_Get_Id";
                cmd.Parameters.Add("@Price_ID", SqlDbType.Int, 0).Value = rec.price_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Price rec = (Price)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPrice_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Price_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.price_ID = (int)cmd.Parameters["@Price_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Price rec = (Price)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPrice_Del_Id";
                cmd.Parameters.Add("@Price_ID", SqlDbType.Int, 0).Value = rec.price_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Price rec = (Price)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPrice_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
			public Price[] getPriceRule(UOW uow, string priceRule)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPrice_Get_PriceRule";
				cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = priceRule;
				cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = Const.PriceActiveStatus;
                cmd.Parameters.Add("@EffDate", SqlDbType.DateTime, 0).Value = DateTime.Today;
				return convert(execReader(cmd));
			}
            public Price[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spPrice_Get_All";
                return convert(execReader(cmd));
            }
			public bool IsSupplierRequired(UOW uow, string priceRule)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPrice_IsSupplierReq";
				cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = priceRule;

				SqlDataReader rdr = cmd.ExecuteReader();
				try
				{
					rdr.Read();

					if (rdr["IsSupplierReq"] == DBNull.Value)
						return false;

					return (string)rdr["IsSupplierReq"] == "T" ?  true : false;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);

					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
				
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}
			/*        Implementation        */
            void setParam(SqlCommand cmd, Price rec)
            {
                cmd.Parameters.Add("@Price_ID", SqlDbType.Int, 0).Value = rec.price_ID;
 
                cmd.Parameters.Add("@PriceRule", SqlDbType.VarChar, 15).Value = rec.priceRule;
                cmd.Parameters.Add("@Revision", SqlDbType.Int, 0).Value = rec.revision;
 
                cmd.Parameters.Add("@PriceType", SqlDbType.VarChar, 15).Value = rec.priceType;
 
                if (rec.priceCat == null)
                    cmd.Parameters.Add("@PriceCat", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.PriceCat.Length == 0)
                        cmd.Parameters.Add("@PriceCat", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PriceCat", SqlDbType.VarChar, 50).Value = rec.priceCat;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.Product == 0)
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = rec.product;
 
                if (rec.supplierPriceClass == null)
                    cmd.Parameters.Add("@SupplierPriceClass", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.SupplierPriceClass.Length == 0)
                        cmd.Parameters.Add("@SupplierPriceClass", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@SupplierPriceClass", SqlDbType.VarChar, 15).Value = rec.supplierPriceClass;
                }
 
                if (rec.excOrderType == null)
                    cmd.Parameters.Add("@ExcOrderType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.ExcOrderType.Length == 0)
                        cmd.Parameters.Add("@ExcOrderType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ExcOrderType", SqlDbType.VarChar, 15).Value = rec.excOrderType;
                }
                cmd.Parameters.Add("@PackDisc", SqlDbType.Float, 0).Value = rec.packDisc;
 
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;

                cmd.Parameters.Add("@UnitQuantity", SqlDbType.Int, 0).Value = rec.unitQuantity;
                cmd.Parameters.Add("@UnitPrice", SqlDbType.Decimal, 0).Value = rec.unitPrice;
 
                cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 15).Value = rec.uOM;
 
                cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
 
                cmd.Parameters.Add("@IsRecurring", SqlDbType.Char, 1).Value = (rec.isRecurring == true) ? "T" : "F";
 
                cmd.Parameters.Add("@IsUsage", SqlDbType.Char, 1).Value = (rec.isUsage == true) ? "T" : "F";
                cmd.Parameters.Add("@UsageBillingThreshold", SqlDbType.Int, 0).Value = rec.usageBillingThreshold;
                
                // Numeric, nullable foreign key treatment:
                if (rec.ExclusiveSupplier == 0)
                    cmd.Parameters.Add("@ExclusiveSupplier", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ExclusiveSupplier", SqlDbType.Int, 0).Value = rec.exclusiveSupplier;
 
                cmd.Parameters.Add("@IsRevClosed", SqlDbType.Char, 1).Value = (rec.isRevClosed == true) ? "T" : "F";
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Price rec = new Price();
                
                if (rdr["Price_ID"] != DBNull.Value)
                    rec.price_ID = (int) rdr["Price_ID"];
 
                if (rdr["PriceRule"] != DBNull.Value)
                    rec.priceRule = (string) rdr["PriceRule"];
 
                if (rdr["Revision"] != DBNull.Value)
                    rec.revision = (int) rdr["Revision"];
 
                if (rdr["PriceType"] != DBNull.Value)
                    rec.priceType = (string) rdr["PriceType"];
 
                if (rdr["PriceCat"] != DBNull.Value)
                    rec.priceCat = (string) rdr["PriceCat"];
 
                if (rdr["Product"] != DBNull.Value)
                    rec.product = (int) rdr["Product"];
 
                if (rdr["SupplierPriceClass"] != DBNull.Value)
                    rec.supplierPriceClass = (string) rdr["SupplierPriceClass"];
 
                if (rdr["ExcOrderType"] != DBNull.Value)
                    rec.excOrderType = (string) rdr["ExcOrderType"];
 				
				if (rdr["PackDisc"] != DBNull.Value)
					rec.packDisc = float.Parse(((double)rdr["PackDisc"]).ToString());
//				rec.packDisc = (float)rdr["PackDisc"];
		
                if (rdr["StartDate"] != DBNull.Value)
                    rec.startDate = (DateTime) rdr["StartDate"];
 
                if (rdr["EndDate"] != DBNull.Value)
                    rec.endDate = (DateTime) rdr["EndDate"];
 
                if (rdr["UnitQuantity"] != DBNull.Value)
                    rec.unitQuantity = (int) rdr["UnitQuantity"];
 
                if (rdr["UnitPrice"] != DBNull.Value)
                    rec.unitPrice = Decimal.Round((decimal)rdr["UnitPrice"], 2);
 
                if (rdr["UOM"] != DBNull.Value)
                    rec.uOM = (string) rdr["UOM"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["IsRecurring"] != DBNull.Value)
                    rec.isRecurring = (string) rdr["IsRecurring"] == "T" ?  true : false;
 
                if (rdr["IsUsage"] != DBNull.Value)
                    rec.isUsage = (string) rdr["IsUsage"] == "T" ?  true : false;
 
                if (rdr["UsageBillingThreshold"] != DBNull.Value)
                    rec.usageBillingThreshold = (int) rdr["UsageBillingThreshold"];
 
                if (rdr["ExclusiveSupplier"] != DBNull.Value)
                    rec.exclusiveSupplier = (int) rdr["ExclusiveSupplier"];
 
                if (rdr["IsRevClosed"] != DBNull.Value)
                    rec.isRevClosed = (string) rdr["IsRevClosed"] == "T" ?  true : false;
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Price[] convert(DomainObj[] objs)
            {
                Price[] acls  = new Price[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
