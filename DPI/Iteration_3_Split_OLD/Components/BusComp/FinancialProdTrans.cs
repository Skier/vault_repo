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
	public class FinancialProdTrans : DomainObj, IPayInfoTran
	{
	#region Data
		static string iName = "FinancialProdTrans";
		int id;
		string tranType;
		int product;
        
		IDemand demand;
		int dmdId;

		int agenInvoice;
		int vendor;
		string storecode;
		string clerkId;
        
		int custId;
		ICustInfo2 customer;

		DateTime tranDate;
		decimal tranAmt;
		decimal prodAmt;
		decimal feeAmt;
		decimal comAmt;
		decimal taxAmt;
		string confirmation;
		string status;
	#endregion

	#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public string TranType
		{
			get { return tranType; }
			set
			{
				setState();
				tranType = value;
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
		public int Dmd
		{
			get	
			{ 
				if (ParDemand == null)
					return 0;

				return ParDemand.Id;
			}
			set 
			{
				if (demand != null)
					if (demand.Id != value)
						throw new ApplicationException(
							"Demand id of Set propery conflicts with Demand already in this DmdItem");

				this.dmdId = value;
			}
		}
		public IDemand ParDemand
		{
			get 
			{
				if (demand == null)
					if (dmdId > 0)
						demand = Demand.find(uow, dmdId);
 
				return demand; 
			}
			set 
			{ 
				demand = (Demand)value; 
				dmdId = 0;
				
				if (demand != null)
					dmdId = demand.Id;
				
				setState();
			}
		}
		public int AgenInvoice
		{
			get { return agenInvoice; }
			set
			{
				setState();
				agenInvoice = value;
			}
		}
		public int Vendor
		{
			get { return vendor; }
			set
			{
				setState();
				vendor = value;
			}
		}
		public string Storecode
		{
			get { return storecode; }
			set
			{
				setState();
				storecode = value;
			}
		}
		public string ClerkId
		{
			get { return clerkId; }
			set
			{
				setState();
				clerkId = value;
			}
		}
		public ICustInfo2 Customer
		{
			get 
			{
				if (customer == null)
					if (custId > 0)
						customer = CustInfo.find(uow, custId);
 
				return customer; 
			}
			set 
			{ 
				customer = (ICustInfo2)value; 
				custId = 0;
				
				if (customer != null)
					custId = customer.CustInfoID;
				
				setState();
			}
		}
		public DateTime TranDate
		{
			get { return tranDate; }
			set
			{
				setState();
				tranDate = value;
			}
		}
		public decimal TranAmt
		{
			get { return tranAmt; }
			set
			{
				setState();
				tranAmt = value;
			}
		}
		public decimal ProdAmt
		{
			get { return prodAmt; }
			set
			{
				setState();
				prodAmt = Decimal.Round(value, 2);
			}
		}
		public decimal FeeAmt
		{
			get { return feeAmt; }
			set
			{
				setState();
				feeAmt = Decimal.Round(value, 2);
			}
		}
		public decimal ComAmt
		{
			get { return comAmt; }
			set
			{
				setState();
				comAmt = Decimal.Round(value, 2);
			}
		}
		public decimal TaxAmt
		{
			get { return taxAmt; }
			set
			{
				setState();
				taxAmt = Decimal.Round(value, 2);
			}
		}
		public string Confirmation
		{
			get { return confirmation; }
			set
			{
				setState();
				confirmation = value;
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
		public PayInfoSource Source { get { return PayInfoSource.FinProd;	}}
		public int TranNumber       { get { return Id;	}}
		public decimal ComAmount    { get { return this.ComAmt;         }}
	#endregion

	#region Constructors
		public FinancialProdTrans()
		{
			sql = new FinancialProdTransSQL();
			id = random.Next(Int32.MinValue, -1);
			priority = 18000;
			rowState = RowState.New;
		}
		public FinancialProdTrans(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
	#endregion

	#region Methods
		protected override SqlGateway loadSql()
		{
			return new FinancialProdTransSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
	#endregion

	#region Static methods
		public static FinancialProdTrans find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(FinancialProdTrans.getKey(id)))
				return (FinancialProdTrans)uow.Imap.find(FinancialProdTrans.getKey(id));
            
			FinancialProdTrans cls = new FinancialProdTrans();
			cls.uow = uow;
			cls.id = id;
			cls = (FinancialProdTrans)DomainObj.addToIMap(uow, getOne(((FinancialProdTransSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static FinancialProdTrans find(UOW uow, IDemand dmd)
		{
			FinancialProdTrans cls = (FinancialProdTrans)DomainObj.addToIMap(uow, getOne(new FinancialProdTransSQL().getKey(uow, dmd)));
			cls.uow = uow;
            
			return cls;
		}
		public static FinancialProdTrans[] getAll(UOW uow)
		{
			FinancialProdTrans[] objs = (FinancialProdTrans[])DomainObj.addToIMap(uow, (new FinancialProdTransSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}		
	#endregion

	#region Implementation
		static FinancialProdTrans getOne(FinancialProdTrans[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(FinancialProdTrans src, FinancialProdTrans tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.tranType = src.tranType;
			tar.product = src.product;
			tar.demand = src.demand;
			tar.agenInvoice = src.agenInvoice;
			tar.vendor = src.vendor;
			tar.storecode = src.storecode;
			tar.clerkId = src.clerkId;
			tar.customer = src.customer;
			tar.tranDate = src.tranDate;
			tar.tranAmt = src.tranAmt;
			tar.prodAmt = src.prodAmt;
			tar.feeAmt = src.feeAmt;
			tar.comAmt = src.comAmt;
			tar.taxAmt = src.taxAmt;
			tar.confirmation = src.confirmation;
			tar.status = src.status;
			tar.rowState = src.rowState;
		}
 
		public override	void RefreshForeignKeys()
		{
			if (dmdId < 0)
				if (ParDemand != null)
					dmdId = ParDemand.Id;

			if (custId < 0)
				if (Customer != null)
					custId = Customer.CustInfoID;

		}
	#endregion

	#region SQL
		[Serializable]
			class FinancialProdTransSQL : SqlGateway
		{
			public FinancialProdTrans[] getKey(FinancialProdTrans rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spFinancialProdTrans_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public FinancialProdTrans[] getKey(UOW uow, IDemand dmd)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spFinancialProdTrans_Get_By_Demand";
				cmd.Parameters.Add("@Demand", SqlDbType.Int, 0).Value = dmd.Id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				FinancialProdTrans rec = (FinancialProdTrans)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spFinancialProdTrans_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				FinancialProdTrans rec = (FinancialProdTrans)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spFinancialProdTrans_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				FinancialProdTrans rec = (FinancialProdTrans)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spFinancialProdTrans_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public FinancialProdTrans[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spFinancialProdTrans_Get_All";
				return convert(execReader(cmd));
			}
		#endregion

		#region Implementation
			void setParam(SqlCommand cmd, FinancialProdTrans rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				if (rec.tranType == null)
					cmd.Parameters.Add("@TranType", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.TranType.Length == 0)
						cmd.Parameters.Add("@TranType", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TranType", SqlDbType.VarChar, 25).Value = rec.tranType;
				}
                
				// Numeric, nullable foreign key treatment:
				if (rec.Product == 0)
					cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = rec.product;
                
				// Numeric, nullable foreign key treatment:
				if (rec.Dmd == 0)
					cmd.Parameters.Add("@Demand", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Demand", SqlDbType.Int, 0).Value = rec.Dmd;
                
				// Numeric, nullable foreign key treatment:
				if (rec.AgenInvoice == 0)
					cmd.Parameters.Add("@AgenInvoice", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@AgenInvoice", SqlDbType.Int, 0).Value = rec.agenInvoice;
				cmd.Parameters.Add("@Vendor", SqlDbType.Int, 0).Value = rec.vendor;
 
				if (rec.storecode == null)
					cmd.Parameters.Add("@Storecode", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Storecode.Length == 0)
						cmd.Parameters.Add("@Storecode", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Storecode", SqlDbType.VarChar, 10).Value = rec.storecode;
				}
 
				if (rec.clerkId == null)
					cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.ClerkId.Length == 0)
						cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 10).Value = rec.clerkId;
				}
				if (rec.Customer == null)
					cmd.Parameters.Add("@Customer", SqlDbType.Int, 0).Value =  DBNull.Value;
				else
					cmd.Parameters.Add("@Customer", SqlDbType.Int, 0).Value = rec.Customer.CustInfoID;
 
				if (rec.tranDate == DateTime.MinValue)
					cmd.Parameters.Add("@TranDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@TranDate", SqlDbType.DateTime, 0).Value = rec.tranDate;
                 
				cmd.Parameters.Add("@TranAmt", SqlDbType.Decimal, 0).Value = rec.tranAmt;
				cmd.Parameters.Add("@ProdAmt", SqlDbType.Decimal, 0).Value = rec.prodAmt;
				cmd.Parameters.Add("@FeeAmt", SqlDbType.Decimal, 0).Value = rec.feeAmt;
				cmd.Parameters.Add("@ComAmt", SqlDbType.Decimal, 0).Value = rec.comAmt;
				cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal, 0).Value = rec.taxAmt;
 
				if (rec.confirmation == null)
					cmd.Parameters.Add("@Confirmation", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Confirmation.Length == 0)
						cmd.Parameters.Add("@Confirmation", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Confirmation", SqlDbType.VarChar, 10).Value = rec.confirmation;
				}
 
				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.Status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
				}
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				FinancialProdTrans rec = new FinancialProdTrans();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["TranType"] != DBNull.Value)
					rec.tranType = (string) rdr["TranType"];
 
				if (rdr["Product"] != DBNull.Value)
					rec.product = (int) rdr["Product"];
 
				if (rdr["Demand"] != DBNull.Value)
					rec.dmdId = (int) rdr["Demand"];
 
				if (rdr["AgenInvoice"] != DBNull.Value)
					rec.agenInvoice = (int) rdr["AgenInvoice"];
 
				if (rdr["Vendor"] != DBNull.Value)
					rec.vendor = (int) rdr["Vendor"];
 
				if (rdr["Storecode"] != DBNull.Value)
					rec.storecode = (string) rdr["Storecode"];
 
				if (rdr["ClerkId"] != DBNull.Value)
					rec.clerkId = (string) rdr["ClerkId"];
 
				if (rdr["Customer"] != DBNull.Value)
					rec.custId = (int) rdr["Customer"];
 
				if (rdr["TranDate"] != DBNull.Value)
					rec.tranDate = (DateTime) rdr["TranDate"];
 
				if (rdr["TranAmt"] != DBNull.Value)
					rec.tranAmt = Decimal.Round((decimal)rdr["TranAmt"], 2);
 
				if (rdr["ProdAmt"] != DBNull.Value)
					rec.prodAmt = Decimal.Round((decimal)rdr["ProdAmount"], 2);
 
				if (rdr["FeeAmt"] != DBNull.Value)
					rec.feeAmt = Decimal.Round((decimal)rdr["FeeAmt"], 2);
 
				if (rdr["ComAmt"] != DBNull.Value)
					rec.comAmt = Decimal.Round((decimal)rdr["ComAmt"], 2); 

				if (rdr["TaxAmt"] != DBNull.Value)
					rec.taxAmt = Decimal.Round((decimal)rdr["TaxAmt"], 2);
 
				if (rdr["Confirmation"] != DBNull.Value)
					rec.confirmation = (string) rdr["Confirmation"];
 
				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			FinancialProdTrans[] convert(DomainObj[] objs)
			{
				FinancialProdTrans[] acls  = new FinancialProdTrans[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		#endregion		

		}
	}
}