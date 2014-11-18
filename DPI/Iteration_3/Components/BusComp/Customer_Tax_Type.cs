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
	public class Customer_Tax_Type : DomainObj
	{
		/*        Data        */
		static string iName = "Customer_Tax_Type";
		string surcharge_Indicator;
		string description;
		string tax_Group;
		string item_Sequence;
		bool unit_Based;
        
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, surcharge_Indicator); }
		}
		public string Surcharge_Indicator
		{
			get { return surcharge_Indicator; }
			set
			{
				setState();
				surcharge_Indicator = value;
			}
		}
		public string Description
		{
			get { return description; }
			set
			{
				setState();
				description = value;
			}
		}
		public string Tax_Group
		{
			get { return tax_Group; }
			set
			{
				setState();
				tax_Group = value;
			}
		}
		public string Item_Sequence
		{
			get { return item_Sequence; }
			set
			{
				setState();
				item_Sequence = value;
			}
		}
		public bool Unit_Based
		{
			get { return unit_Based; }
			set
			{
				setState();
				unit_Based = value;
			}
		}
        
		/*        Constructors			*/
		public Customer_Tax_Type()
		{
			sql = new Customer_Tax_TypeSQL();
			rowState = RowState.New;
		}
		/*
			public Customer_Tax_Type(SqlConnection cn) : this()
			{
				if(cn == null)
					throw new ApplicationException("Connection is required");
            
				this.cn = cn;
			}
			public Customer_Tax_Type(SqlTransaction xact) : this()
			{
				if(cn == null)
					throw new ApplicationException("Transaction is required");
            
				this.xact = xact;
			}
	*/
		public Customer_Tax_Type(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			// if(uow.Imap == null)
			//    throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			if (this.uow.Imap != null)
				this.uow.Imap.add(this);
		}
        
		/*        Methods        */
		public override void checkExists()
		{
			if ((Surcharge_Indicator ==  null))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static Customer_Tax_Type find(UOW uow, string surcharge_Indicator)
		{
			if (uow.Imap != null)
				if (uow.Imap.keyExists(Customer_Tax_Type.getKey(surcharge_Indicator)))
					return (Customer_Tax_Type)uow.Imap.find(Customer_Tax_Type.getKey(surcharge_Indicator));
            
			Customer_Tax_Type cls = new Customer_Tax_Type();
			cls.uow = uow;
			cls.surcharge_Indicator = surcharge_Indicator;
			cls = getOne(((Customer_Tax_TypeSQL)cls.sql).getKey(cls));
        
			if (uow.Imap != null)
				uow.Imap.add(cls);
            
			return cls;
		}

		/*        public static Customer_Tax_Type find(SqlTransaction xact, string surcharge_Indicator)
				{
					Customer_Tax_Type cls = new Customer_Tax_Type(xact);
					cls.surcharge_Indicator = surcharge_Indicator;
					return getOne(((Customer_Tax_TypeSQL)cls.sql).getKey(cls));
				}
				public static Customer_Tax_Type find(SqlConnection cn, string surcharge_Indicator)
				{
					Customer_Tax_Type cls = new Customer_Tax_Type(cn);
					cls.surcharge_Indicator = surcharge_Indicator;
					return getOne(((Customer_Tax_TypeSQL)cls.sql).getKey(cls));
				}
		*/
		public static Customer_Tax_Type find(string surcharge_Indicator)
		{
			Customer_Tax_Type cls = new Customer_Tax_Type();
			cls.surcharge_Indicator = surcharge_Indicator;
			return getOne(((Customer_Tax_TypeSQL)cls.sql).getKey(cls));
		}
		public static Customer_Tax_Type[] getAll(UOW uow)
		{
			Customer_Tax_Type[] objs=  (new Customer_Tax_TypeSQL()).getAll(uow);
			DomainObj.addToIMap(uow, objs);
			return objs;
		}

		/*        public static Customer_Tax_Type[] getAll(SqlTransaction xact)
				{
					return (new Customer_Tax_TypeSQL()).getAll(xact);
				}
				public static Customer_Tax_Type[] getAll(SqlConnection cn)
				{
					return (new Customer_Tax_TypeSQL()).getAll(cn);
				}
		*/
		public static Customer_Tax_Type[] getAll()
		{
			return (new Customer_Tax_TypeSQL()).getAll();
		}
		public static Key getKey(string surcharge_Indicator)
		{
			return new Key(iName, surcharge_Indicator.ToString());
		}
		/*		Implementation		*/
		static Customer_Tax_Type getOne(Customer_Tax_Type[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Customer_Tax_Type src, Customer_Tax_Type tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.surcharge_Indicator = src.surcharge_Indicator;
			tar.description = src.description;
			tar.tax_Group = src.tax_Group;
			tar.item_Sequence = src.item_Sequence;
			tar.unit_Based = src.unit_Based;
			tar.rowState = src.rowState;
		}

		override protected SqlGateway loadSql()
		{
			return new Customer_Tax_TypeSQL();
		}
 
		/*		SQL		*/
		class Customer_Tax_TypeSQL : SqlGateway
		{
			public Customer_Tax_Type[] getKey(Customer_Tax_Type rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustomer_Tax_Type_Get_Id";
				cmd.Parameters.Add("@Surcharge_Indicator", SqlDbType.VarChar, 3).Value = rec.surcharge_Indicator;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				Customer_Tax_Type rec = (Customer_Tax_Type)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustomer_Tax_Type_Ins";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				Customer_Tax_Type rec = (Customer_Tax_Type)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustomer_Tax_Type_Del_Id";
				cmd.Parameters.Add("@Surcharge_Indicator", SqlDbType.VarChar, 3).Value = rec.surcharge_Indicator;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Customer_Tax_Type rec = (Customer_Tax_Type)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCustomer_Tax_Type_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Customer_Tax_Type[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustomer_Tax_Type_Get_All";
				return convert(execReader(cmd));
			}
			/*
						public Customer_Tax_Type[] getAll(SqlTransaction xact)
						{
							SqlCommand cmd = makeCommand(xact);
							cmd.CommandText = "spCustomer_Tax_Type_Get_All";
							return convert(execReader(cmd));
						}
						public Customer_Tax_Type[] getAll(SqlConnection cn)
						{
							SqlCommand cmd = makeCommand(cn);
							cmd.CommandText = "spCustomer_Tax_Type_Get_All";
							return convert(execReader(cmd));
						}
			*/
			public Customer_Tax_Type[] getAll()
			{
				SqlCommand cmd = makeCommand();
				cmd.CommandText = "spCustomer_Tax_Type_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, Customer_Tax_Type rec)
			{
 
				cmd.Parameters.Add("@Surcharge_Indicator", SqlDbType.VarChar, 3).Value = rec.surcharge_Indicator;
 
				cmd.Parameters.Add("@Description", SqlDbType.VarChar, 55).Value = rec.description;
 
				cmd.Parameters.Add("@Tax_Group", SqlDbType.VarChar, 1).Value = rec.tax_Group;
 
				cmd.Parameters.Add("@Item_Sequence", SqlDbType.VarChar, 3).Value = rec.item_Sequence;
 
				cmd.Parameters.Add("@Unit_Based", SqlDbType.Bit, 0).Value = rec.unit_Based;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				Customer_Tax_Type rec = new Customer_Tax_Type();
                
				if (rdr["Surcharge_Indicator"] != DBNull.Value)
					rec.surcharge_Indicator = (string) rdr["Surcharge_Indicator"];
 
				if (rdr["Description"] != DBNull.Value)
					rec.description = (string) rdr["Description"];
 
				if (rdr["Tax_Group"] != DBNull.Value)
					rec.tax_Group = (string) rdr["Tax_Group"];
 
				if (rdr["Item_Sequence"] != DBNull.Value)
					rec.item_Sequence = (string) rdr["Item_Sequence"];
 
				if (rdr["Unit_Based"] != DBNull.Value)
					rec.unit_Based = (bool) rdr["Unit_Based"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			Customer_Tax_Type[] convert(DomainObj[] objs)
			{
				Customer_Tax_Type[] acls  = new Customer_Tax_Type[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
	}
}
