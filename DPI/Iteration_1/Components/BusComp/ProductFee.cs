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
    public class ProductFee : DomainObj
    {
        /*        Data        */
        static string iName = "ProductFee";
        internal int id;
        internal string orderType;
        int orderedProduct;
        internal int fee;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }

		//public string OrderType 
		//{
		//	get { return orderType; }
		//	set
		//	{
		//		setState();
		//		orderType = value;
		//	}
		//}
        public OrderType OrderType // replacement for the above - alex
        {
            get 
			{
				switch (orderType.ToLower().Trim())
				{
					case "new" :
						return DPI.Interfaces.OrderType.New;
					
					case "add" :
						return DPI.Interfaces.OrderType.Add;

					default :
						throw new ApplicationException("Unknown order type: " + orderType);
				}
			}
            set
            {
                setState();
                orderType = value.ToString();
            }
        }
        public int OrderedProduct
        {
            get { return orderedProduct; }
            set
            {
                setState();
                orderedProduct = value;
            }
        }
        public int Fee
        {
            get { return fee; }
            set
            {
                setState();
                fee = value;
            }
        }
        
        /*        Constructors			*/
        public ProductFee()
        {
            sql = new ProductFeeSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public ProductFee(UOW uow) : this()
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
            return new ProductFeeSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ProductFee find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ProductFee.getKey(id)))
                return (ProductFee)uow.Imap.find(ProductFee.getKey(id));
            
            ProductFee cls = new ProductFee();
            cls.uow = uow;
            cls.id = id;
            cls = (ProductFee)DomainObj.addToIMap(uow, getOne(((ProductFeeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ProductFee[] getAll(UOW uow)
        {
            ProductFee[] objs = (ProductFee[])DomainObj.addToIMap(uow, (new ProductFeeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static ProductFee getOne(ProductFee[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        public static void copyAttrs(ProductFee src, ProductFee tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.orderType = src.orderType;
            tar.orderedProduct = src.orderedProduct;
            tar.fee = src.fee;
            tar.rowState = src.rowState;
        }
 

		public static ProdPrice[] getFeesForProd(UOW uow, int prodid, string zip, string ilecCode, OrderType oType)
		{
			int ilec=0; 
			ProductFee[] fees;
			ProdPrice ppTemp;

			ilec=ILECInfo.Find(uow, ilecCode).OrgId;  //need to convert the code to the id.

			fees=(new FeeSQL()).getForProd(uow, prodid, zip, ilecCode);
//			Console.WriteLine("Got {0} fees", fees.Length);
			ArrayList al=new ArrayList(fees.Length);

			for (int i=0; i<fees.Length; i++)
			{
				if (oType == fees[i].OrderType) // replaced string with enum - alex
			//	if ((oType==DPI.Interfaces.OrderType.New && fees[i].OrderType.Trim().Equals("New")) ||
			//		(oType==DPI.Interfaces.OrderType.Add && fees[i].OrderType.Trim().Equals("Add")))
				{
//					Console.WriteLine("Getting price for {0}", fees[i].Fee);
					ppTemp=ProdPrice.getPriceForProd(uow, fees[i].Fee, Location.find(uow, zip).LocId, ilec);
					al.Add(ppTemp);
				}
			}
			ProdPrice[] ppa=new ProdPrice[al.Count];
			al.CopyTo(ppa);
			return ppa;
		}

        /*		SQL		*/
        [Serializable]
        class ProductFeeSQL : SqlGateway
        {
            public ProductFee[] getKey(ProductFee rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductFee_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ProductFee rec = (ProductFee)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductFee_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ProductFee rec = (ProductFee)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductFee_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ProductFee rec = (ProductFee)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spProductFee_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ProductFee[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spProductFee_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, ProductFee rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 15).Value = rec.orderType;
                
                // Numeric, nullable foreign key treatment:
                if (rec.OrderedProduct == 0)
                    cmd.Parameters.Add("@OrderedProduct", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@OrderedProduct", SqlDbType.Int, 0).Value = rec.orderedProduct;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Fee == 0)
                    cmd.Parameters.Add("@Fee", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Fee", SqlDbType.Int, 0).Value = rec.fee;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ProductFee rec = new ProductFee();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["OrderType"] != DBNull.Value)
                    rec.orderType = (string) rdr["OrderType"];
 
                if (rdr["OrderedProduct"] != DBNull.Value)
                    rec.orderedProduct = (int) rdr["OrderedProduct"];
 
                if (rdr["Fee"] != DBNull.Value)
                    rec.fee = (int) rdr["Fee"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ProductFee[] convert(DomainObj[] objs)
            {
                ProductFee[] acls  = new ProductFee[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }

		class FeeSQL 
		{
			public ProductFee[] getForProd(UOW uow, int prodid, string  zip, string ilec)// does not return a "real" ProductFee
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spOrder_ProductFeesGet";

				cmd.Parameters.Add("@pid",SqlDbType.Int, 0).Value = prodid; 
				cmd.Parameters.Add("@zip", SqlDbType.Char, 5).Value = zip;
				cmd.Parameters.Add("@ilec", SqlDbType.Char, 3).Value = ilec;

				return execReader(cmd);
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, ProductFee rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 15).Value = rec.orderType;
                
				// Numeric, nullable foreign key treatment:
				if (rec.OrderedProduct == 0)
					cmd.Parameters.Add("@OrderedProduct", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@OrderedProduct", SqlDbType.Int, 0).Value = rec.orderedProduct;
                
				// Numeric, nullable foreign key treatment:
				if (rec.Fee == 0)
					cmd.Parameters.Add("@Fee", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Fee", SqlDbType.Int, 0).Value = rec.fee;
			}
	
			protected ProductFee reader(SqlDataReader rdr)  // does not return a "real" ProductFee
			{
				ProductFee rec = new ProductFee();
                
				if (rdr["OrderType"] != DBNull.Value)
					rec.orderType = (string) rdr["OrderType"];
 
				if (rdr["id"] != DBNull.Value)
					rec.fee = (int) rdr["id"];

				rec.rowState = RowState.Clean;
				return rec;
			}
/*			ProdPrice[] convert(DomainObj[] objs)
			{
				ProdPrice[] acls  = new ProductFee[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
*/
			/*        Implementation        */
			ProductFee[] execReader(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();

				try
				{
					while(rdr.Read())
						ar.Add(reader(rdr));

					ProductFee[] recs = new ProductFee[ar.Count];
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
