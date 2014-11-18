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
	public class Wireless_Transactions : DomainObj, IWireless_Transactions, IPayInfoTran
	{

		#region Data
		static string iName = "Wireless_Transactions";
		int wireless_Transaction_ID;
		int trConfirm;
		string trNumber;
		DateTime payDateTime;
		decimal tran_Amount;
		int transaction_Method_ID;
		string storeCode;
		string clerkid;
		int wireless_product_ID;
		string pin;
		decimal commission;
		string status;
		string supplier_tran;
		int acctID;
		decimal activationFee;

		#endregion        

		#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, wireless_Transaction_ID.ToString()); }
		}
		public int Wireless_Transaction_ID
		{
			get { return wireless_Transaction_ID; }
		}
		public int TrConfirm
		{
			get { return trConfirm; }
			set
			{
				setState();
				trConfirm = value;
			}
		}
		public string TrNumber
		{
			get { return trNumber; }
			set
			{
				setState();
				trNumber = value;
			}
		}
		public DateTime PayDateTime
		{
			get { return payDateTime; }
			set
			{
				setState(); 
				payDateTime = value;
			}
		}
		public decimal Tran_Amount
		{
			get { return tran_Amount; }
			set
			{
				setState();
				tran_Amount = Decimal.Round(value, 2);
			}
		}
		public int Transaction_Method_ID
		{
			get { return transaction_Method_ID; }
			set
			{
				setState();
				transaction_Method_ID = value;
			}
		}
		public string StoreCode
		{
			get { return storeCode; }
			set
			{
				setState();
				storeCode = value;
			}
		}
		public string Clerkid
		{
			get { return clerkid; }
			set
			{
				setState();
				clerkid = value;
			}
		}
		public int Wireless_product_ID
		{
			get { return wireless_product_ID; }
			set
			{
				setState();
				wireless_product_ID = value;
			}
		}
		public string Pin
		{
			get { return pin; }
			set
			{
				setState();
				pin = value;
			}
		}
		public decimal Commission
		{
			get { return commission; }
			set
			{
				setState();
				commission = Decimal.Round(value, 2);
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
		public string Supplier_tran
		{
			get { return supplier_tran; }
			set
			{
				setState();
				supplier_tran = value;
			}
		}
		public int AcctID
		{
			get { return acctID; }
			set
			{
				setState();
				acctID = value;
			}
		}
		public decimal ActivationFee
		{
			get { return activationFee; }
			set 
			{
				setState();
				activationFee = value;
			}
		}
		public PayInfoSource Source { get { return PayInfoSource.Wireless;	}}
		public int TranNumber       { get { return Wireless_Transaction_ID;	}}
		public decimal ComAmount    { get { return this.Commission;         }}
		
		#endregion        

		#region Constructors
		public Wireless_Transactions()
		{
			sql = new Wireless_TransactionsSQL();
			wireless_Transaction_ID = random.Next(Int32.MinValue, -1);
			priority = 16500;
			rowState = RowState.New;

		}
		public Wireless_Transactions(UOW uow) : this()
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
			return new Wireless_TransactionsSQL();
		}
		public override void checkExists()
		{
			if ((Wireless_Transaction_ID < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static IWireless_Transactions[] GetNonRedeemedTrans(UOW uow)
		{
			return new Wireless_TransactionsSQL().GetNonRedeemedTrans(uow);
		}
		public static IWireless_Transactions Locate(UOW uow, int id)
		{
			if (uow.Imap.keyExists(Wireless_Transactions.getKey(id)))
				return (Wireless_Transactions)uow.Imap.find(Wireless_Transactions.getKey(id));
            
			IWireless_Transactions[]  xact = (IWireless_Transactions[])DomainObj.addToIMap(
				uow,(new Wireless_TransactionsSQL().getKey(uow, id)));
			
			if (xact.Length > 0)
				return xact[0];
            
			return Wireless_Transactions_Archive.Locate(uow, id)[0];
		}
		public static Wireless_Transactions find(UOW uow, int wireless_Transaction_ID)
		{
			if (uow.Imap.keyExists(Wireless_Transactions.getKey(wireless_Transaction_ID)))
				return (Wireless_Transactions)uow.Imap.find(Wireless_Transactions.getKey(wireless_Transaction_ID));
            
			Wireless_Transactions cls = new Wireless_Transactions();
			cls.uow = uow;
			cls.wireless_Transaction_ID = wireless_Transaction_ID;
			cls = (Wireless_Transactions)DomainObj.addToIMap(uow, getOne(((Wireless_TransactionsSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static Wireless_Transactions locate(UOW uow, string pin, int prodId, string confirm, string storeCode)
		{
			Wireless_Transactions[] xact 
				= (Wireless_Transactions[])DomainObj.addToIMap(
					uow, new Wireless_TransactionsSQL().Locate(uow, pin, prodId, storeCode, int.Parse(confirm)));

			if (xact == null)
				return null;
			
			if (xact.Length == 0)
				return null;

			for (int i = 0; i < xact.Length; i++)
				xact[i].uow = uow;

			return xact[0];
		}
		public static Wireless_Transactions find(UOW uow, IPayInfo payInfo)
		{
			Wireless_Transactions cls 
				= (Wireless_Transactions)DomainObj.addToIMap(
				uow, getOne( new Wireless_TransactionsSQL().getKey(uow, payInfo)));
			cls.uow = uow;
            
			return cls;
		}
		public static decimal GetCommissionAmt(UOW uow, string confirm, string storeCode, decimal amount)
		{
			IWireless_Transactions xact = find(uow, confirm, storeCode, amount);
			if (xact == null)
				return 0M;
 
			return xact.Commission;
		}
		public static IWireless_Transactions find(UOW uow, string conf, string storeCode, decimal amount)
		{
			int confirm = int.Parse(conf);

			Wireless_Transactions[] xact 
				= (Wireless_Transactions[])DomainObj.addToIMap(
				uow, new Wireless_TransactionsSQL().Locate(uow, confirm, storeCode, amount));

			if (xact.Length == 0)
				return Wireless_Transactions_Archive.find(uow, conf, storeCode, amount);

			for (int i = 0; i < xact.Length; i++)
				xact[i].uow = uow;

			return xact[0];
		}
		public static Wireless_Transactions[] getAll(UOW uow)
		{
			Wireless_Transactions[] objs = (Wireless_Transactions[])DomainObj.addToIMap(uow, (new Wireless_TransactionsSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Transactions[] GetVdblTransByStore(UOW uow, string storeCode)
		{
			Wireless_Transactions[] objs = (Wireless_Transactions[])DomainObj.addToIMap(uow, (new Wireless_TransactionsSQL()).GetVdblTransByStore(uow, storeCode));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Wireless_Transactions[] GetPendingTransByStore(UOW uow, string storeCode)
		{
			Wireless_Transactions[] objs = (Wireless_Transactions[])DomainObj.addToIMap(uow, (new Wireless_TransactionsSQL()).GetPendingTransByStore(uow, storeCode));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}		
		public static IWireless_Transactions PostWirelessTran(UOW uow, int prod, string storeCode, 
			                                                 string clerkId, string pin, string trNumber)
		{
			int id = new Wireless_TransactionsSQL().PostTran(uow, prod, storeCode, clerkId, pin, trNumber);
			return find(uow, id);
		}
		public static int PostPendWirelessTran(UOW uow, 
											int prod, 
											string storeCode, 
											string clerkId,
											string pin, 
											string trNumber)
		{
			return new Wireless_TransactionsSQL().PostPendTran(uow, prod, storeCode, clerkId, pin, trNumber);
			
		}
		public static Key getKey(int wireless_Transaction_ID)
		{
			return new Key(iName, wireless_Transaction_ID.ToString());
		}
		#endregion        

		#region Implementation
		static Wireless_Transactions getOne(Wireless_Transactions[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Wireless_Transactions src, Wireless_Transactions tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.wireless_Transaction_ID = src.wireless_Transaction_ID;
			tar.trConfirm = src.trConfirm;
			tar.trNumber = src.trNumber;
			tar.payDateTime = src.payDateTime;
			tar.tran_Amount = src.tran_Amount;
			tar.transaction_Method_ID = src.transaction_Method_ID;
			tar.storeCode = src.storeCode;
			tar.clerkid = src.clerkid;
			tar.wireless_product_ID = src.wireless_product_ID;
			tar.pin = src.pin;
			tar.rowState = src.rowState;
			tar.status = src.status;
			tar.supplier_tran = src.supplier_tran;
			tar.acctID = src.acctID;
			tar.activationFee = src.activationFee;
		}
		#endregion        

		#region SQL
		[Serializable]
			class Wireless_TransactionsSQL : SqlGateway
		{
			public int PostTran(UOW uow, int prod, string storeCode, string clerkId, string pin, string trNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWS_createWirelessTransactionWebCentral";
				
				setPostTranParam(cmd, prod, storeCode, clerkId, pin, trNumber);
				return execScalarInt(cmd);
			}
			public int PostPendTran(UOW uow, int prod, string storeCode, string clerkId, string pin, string trNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWS_createPendWirelessTransactionWebCentral";
				
				setPostTranParam(cmd, prod, storeCode, clerkId, pin, trNumber);
				return execScalarInt(cmd);
			}
			public Wireless_Transactions[] getKey(UOW uow, int id)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Get_Id";
				cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = id;
				return convert(execReader(cmd));
			}
			public Wireless_Transactions[] getKey(Wireless_Transactions rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Transactions_Get_Id";
				cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = rec.wireless_Transaction_ID;
				return convert(execReader(cmd));
			}
			public Wireless_Transactions[] getKey(UOW uow, IPayInfo payInfo)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Get_By_TrConfirm";
				cmd.Parameters.Add("@trConfirm", SqlDbType.Int, 0).Value = payInfo.VFConf;
				return convert(execReader(cmd));
			}
			public Wireless_Transactions[] Locate(UOW uow, int conf, string storeCode, decimal amount)
			{
				SqlCommand cmd = makeCommand(uow);
				
				cmd.CommandText = "spWireless_Transactions_Get_By_TrConfirm";
				cmd.Parameters.Add("@Confirm", SqlDbType.Int, 0).Value = conf;
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 14).Value = storeCode;
				cmd.Parameters.Add("@Amount", SqlDbType.Money, 0).Value = amount;
				
				return convert(execReader(cmd));
			}

			public Wireless_Transactions[] Locate(UOW uow, string pin, int prodId, string storeCode, int confirm)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWirelessTransactionsGetByTrConfirmProd";
			
				cmd.Parameters.Add("@Confirm", SqlDbType.Int).Value = confirm;
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 14).Value = storeCode;
				cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 50).Value = pin;
				cmd.Parameters.Add("@ProdId", SqlDbType.Int).Value = prodId;
				
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				Wireless_Transactions rec = (Wireless_Transactions)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Transactions_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Wireless_Transaction_ID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.wireless_Transaction_ID = (int)cmd.Parameters["@Wireless_Transaction_ID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				Wireless_Transactions rec = (Wireless_Transactions)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Transactions_Del_Id";
				cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = rec.wireless_Transaction_ID;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Wireless_Transactions rec = (Wireless_Transactions)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spWireless_Transactions_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Wireless_Transactions[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Get_All";
				return convert(execReader(cmd));
			}
			public Wireless_Transactions[] GetVdblTransByStore(UOW uow, string storeCode)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Get_VdblTransByStore";
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = storeCode;

				return convert(execReader(cmd));
			}
			public Wireless_Transactions[] GetPendingTransByStore(UOW uow, string storeCode)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Get_PendConfirmByStore";
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = storeCode;

				return convert(execReader(cmd));
			}
			public IWireless_Transactions[] GetNonRedeemedTrans(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Get_NonRed";				
				return convert(execReader(cmd));
			}
		#endregion        

		#region SQL Implementation
			void setPostTranParam(SqlCommand cmd, int prod, string storeCode, string clerkId, string pin, string trNumber)
			{
				cmd.Parameters.Add("@ProductID", SqlDbType.Int, 0).Value = prod;

				if (storeCode == null)
					cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (storeCode.Length == 0)
						cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = storeCode;
				}

				if (clerkId == null)
					cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (clerkId.Length == 0)
						cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ClerkId", SqlDbType.VarChar, 10).Value = clerkId;
				}
				
				if (pin == null)
					cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (pin.Length == 0)
						cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 50).Value = pin;
				}
				
				if (trNumber == null)
					cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (trNumber.Length == 0)
						cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 50).Value = trNumber;
				}
			}
		
			void setParam(SqlCommand cmd, Wireless_Transactions rec)
			{
				cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = rec.wireless_Transaction_ID;
				cmd.Parameters.Add("@TrConfirm", SqlDbType.Int, 0).Value = rec.trConfirm;
 
				if (rec.trNumber == null)
					cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
				else
				{
					if (rec.TrNumber.Length == 0)
						cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 50).Value = rec.trNumber;
				}
 
				cmd.Parameters.Add("@PayDateTime", SqlDbType.DateTime, 0).Value = rec.payDateTime;
				cmd.Parameters.Add("@Tran_Amount", SqlDbType.Decimal, 0).Value = rec.tran_Amount;
				cmd.Parameters.Add("@Transaction_Method_ID", SqlDbType.Int, 0).Value = rec.transaction_Method_ID;
 
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
 
				if (rec.clerkid == null)
					cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.Clerkid.Length == 0)
						cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Clerkid", SqlDbType.VarChar, 10).Value = rec.clerkid;
				}
				cmd.Parameters.Add("@Wireless_product_ID", SqlDbType.Int, 0).Value = rec.wireless_product_ID;
				cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 25).Value = rec.pin;
				cmd.Parameters.Add("@Commission", SqlDbType.Decimal, 0).Value = rec.commission;

				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 10).Value = rec.status;
				}
				if (rec.supplier_tran == null)
					cmd.Parameters.Add("@Supplier_tran", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.supplier_tran.Length == 0)
						cmd.Parameters.Add("@Supplier_tran", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Supplier_tran", SqlDbType.VarChar, 25).Value = rec.supplier_tran;
				}
				cmd.Parameters.Add("@AcctID", SqlDbType.Int, 0).Value = rec.acctID;
				cmd.Parameters.Add("@ActivationFee", SqlDbType.Decimal, 0).Value = rec.activationFee;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				Wireless_Transactions rec = new Wireless_Transactions();
                
				if (rdr["Wireless_Transaction_ID"] != DBNull.Value)
					rec.wireless_Transaction_ID = (int) rdr["Wireless_Transaction_ID"];
 
				if (rdr["TrConfirm"] != DBNull.Value)
					rec.trConfirm = (int) rdr["TrConfirm"];
 
				if (rdr["TrNumber"] != DBNull.Value)
					rec.trNumber = (string) rdr["TrNumber"];
 
				if (rdr["PayDateTime"] != DBNull.Value)
					rec.payDateTime = (DateTime) rdr["PayDateTime"];
 
				if (rdr["Tran_Amount"] != DBNull.Value)
					rec.tran_Amount = Decimal.Round((decimal)rdr["Tran_Amount"], 2);
 
				if (rdr["Transaction_Method_ID"] != DBNull.Value)
					rec.transaction_Method_ID = (int) rdr["Transaction_Method_ID"];
 
				if (rdr["StoreCode"] != DBNull.Value)
					rec.storeCode = (string) rdr["StoreCode"];
 
				if (rdr["Clerkid"] != DBNull.Value)
					rec.clerkid = (string) rdr["Clerkid"];
 
				if (rdr["Wireless_product_ID"] != DBNull.Value)
					rec.wireless_product_ID = (int) rdr["Wireless_product_ID"];
 
				if (rdr["Pin"] != DBNull.Value)
					rec.pin = (string) rdr["Pin"];

				if (rdr["Commission"] != DBNull.Value)
					rec.commission = Decimal.Round((decimal)rdr["Commission"], 2);

				if (rdr["Status"] != DBNull.Value)
					rec.status = (string) rdr["Status"];

				if (rdr["Supplier_tran"] != DBNull.Value)
					rec.supplier_tran = (string) rdr["Supplier_tran"];

				if (rdr["AcctID"] != DBNull.Value)
					rec.acctID = (int) rdr["AcctID"];

				if (rdr["ActivationFee"] != DBNull.Value)
					rec.activationFee = Decimal.Round((decimal)rdr["ActivationFee"], 2);

				rec.rowState = RowState.Clean;
				return rec;
			}
			Wireless_Transactions[] convert(DomainObj[] objs)
			{
				Wireless_Transactions[] acls  = new Wireless_Transactions[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
		#endregion        
	}
}