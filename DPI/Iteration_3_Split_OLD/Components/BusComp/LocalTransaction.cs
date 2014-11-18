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
	public class LocalTransaction : DomainObj
	{
		#region Member Variables
		static string iName = "LocalTransaction";
		int transaction_Id;
		string trConfirm;
		string trLDConfirm;
		string trNumber;
		int accNumber;
		string phNumber;
		DateTime payDate;
		string payTime;
		decimal localAmount;
		decimal ldAmount;
		decimal comAmount;
		string clerkID;
		int transaction_Type_ID;
		int transaction_Method_ID;
		string storeCode;
		string aNI;
		#endregion
        
		#region Properties
		public override IDomKey IKey 
		{
			get { return new Key(iName, transaction_Id.ToString()); }
		}
		public LocalTransactionInfo LocalTransactionInfo
		{
			get 
			{
				LocalTransactionInfo localTransactionInfo = new LocalTransactionInfo();
				localTransactionInfo.Transaction_Id =  this.transaction_Id;
				localTransactionInfo.TrConfirm = this.trConfirm;
				localTransactionInfo.AccNumber = this.accNumber;
				localTransactionInfo.Transaction_Type_Id = (Transaction_Type_Id)this.transaction_Type_ID;
				localTransactionInfo.PhNumber = this.phNumber;
				localTransactionInfo.PayDate = this.payDate;
				localTransactionInfo.PayTime = this.payTime;
				localTransactionInfo.LocalAmount = this.localAmount;
				localTransactionInfo.LDAmount = this.ldAmount;				
				localTransactionInfo.Tax = 0;
				return localTransactionInfo;
			}
		}
		public int Transaction_Id
		{
			get { return transaction_Id; }
		}
		public string TrConfirm
		{
			get { return trConfirm; }
			set
			{
				setState();
				trConfirm = value;
			}
		}
		public string TrLDConfirm
		{
			get { return trLDConfirm; }
			set
			{
				setState();
				trLDConfirm = value;
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
		public int AccNumber
		{
			get { return accNumber; }
			set
			{
				setState();
				accNumber = value;
			}
		}
		public string PhNumber
		{
			get { return phNumber; }
			set
			{
				setState();
				phNumber = value;
			}
		}
		public DateTime PayDate
		{
			get { return payDate; }
			set
			{
				setState();
				payDate = value;
			}
		}
		public string PayTime
		{
			get { return payTime; }
			set
			{
				setState();
				payTime = value;
			}
		}
		public decimal LocalAmount
		{
			get { return localAmount; }
			set
			{
				setState();
				localAmount = Decimal.Round(value, 2);
			}
		}
		public decimal LDAmount
		{
			get { return ldAmount; }
			set
			{
				setState();
				ldAmount = Decimal.Round(value, 2);
			}
		}
		public decimal ComAmount
		{
			get { return comAmount; }
			set
			{
				setState();
				comAmount = Decimal.Round(value, 2);
			}
		}
		public string ClerkID
		{
			get { return clerkID; }
			set
			{
				setState();
				clerkID = value;
			}
		}
		public int Transaction_Type_ID
		{
			get { return transaction_Type_ID; }
			set
			{
				setState();
				transaction_Type_ID = value;
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
		public string ANI
		{
			get { return aNI; }
			set
			{
				setState();
				aNI = value;
			}
		}
		#endregion
		#region Constructors
		public LocalTransaction()
		{
			sql = new LocalTransactionSQL();
			transaction_Id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public LocalTransaction(UOW uow) : this()
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
			return new LocalTransactionSQL();
		}
		public override void checkExists()
		{
			if ((Transaction_Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		#endregion
		#region Static methods
		public static LocalTransaction find(UOW uow, int transaction_Id)
		{
			if (uow.Imap.keyExists(LocalTransaction.getKey(transaction_Id)))
				return (LocalTransaction)uow.Imap.find(LocalTransaction.getKey(transaction_Id));
            
			LocalTransaction cls = new LocalTransaction();
			cls.uow = uow;
			cls.transaction_Id = transaction_Id;
			cls = (LocalTransaction)DomainObj.addToIMap(uow, getOne(((LocalTransactionSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static LocalTransaction[] getAll(UOW uow)
		{
			LocalTransaction[] objs = (LocalTransaction[])DomainObj.addToIMap(uow, (new LocalTransactionSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static LocalTransaction[] GetVoidableTransactions(UOW uow, string storecode, DateTime paydate)
		{
			return new LocalTransactionSQL().GetVoidableTransactions(uow, storecode, paydate);
		}
		public static Key getKey(int transaction_Id)
		{
			return new Key(iName, transaction_Id.ToString());
		}
	
		public static ILocalTransactionInfo[] ToInfo(LocalTransaction[] xacts)
		{
			LocalTransactionInfo[] xactInfos = new LocalTransactionInfo[xacts.Length];

			for (int i = 0; i < xactInfos.Length; i++)
				xactInfos[i] = xacts[i].LocalTransactionInfo;
				
			return xactInfos;
		}
		#endregion		
		#region Implementation
		static LocalTransaction getOne(LocalTransaction[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(LocalTransaction src, LocalTransaction tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.transaction_Id = src.transaction_Id;
			tar.trConfirm = src.trConfirm;
			tar.trLDConfirm = src.trLDConfirm;
			tar.trNumber = src.trNumber;
			tar.accNumber = src.accNumber;
			tar.phNumber = src.phNumber;
			tar.payDate = src.payDate;
			tar.payTime = src.payTime;
			tar.localAmount = src.localAmount;
			tar.ldAmount = src.ldAmount;
			tar.comAmount = src.comAmount;
			tar.clerkID = src.clerkID;
			tar.transaction_Type_ID = src.transaction_Type_ID;
			tar.transaction_Method_ID = src.transaction_Method_ID;
			tar.storeCode = src.storeCode;
			tar.aNI = src.aNI;
			tar.rowState = src.rowState;
		}
 		
		public static bool VoidTransaction(UOW uow, int transaction_id, int create_void_record)
		{
			return new LocalTransactionSQL().VoidTransaction(uow, transaction_id, create_void_record);
		}
		#region SQL
		[Serializable]
		class LocalTransactionSQL : SqlGateway
		{
			public LocalTransaction[] getKey(LocalTransaction rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spLocalTransaction_Get_Id";
				cmd.Parameters.Add("@Transaction_ID", SqlDbType.Int, 0).Value = rec.transaction_Id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				LocalTransaction rec = (LocalTransaction)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spLocalTransaction_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Transaction_ID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.transaction_Id = (int)cmd.Parameters["@Transaction_ID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				LocalTransaction rec = (LocalTransaction)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spLocalTransaction_Del_Id";
				cmd.Parameters.Add("@Transaction_ID", SqlDbType.Int, 0).Value = rec.transaction_Id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				LocalTransaction rec = (LocalTransaction)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spLocalTransaction_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public LocalTransaction[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spLocalTransaction_Get_All";
				return convert(execReader(cmd));
			}
			#region Implementation
			public LocalTransaction[] GetVoidableTransactions(UOW uow, string storecode, DateTime paydate)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = storecode;
				cmd.Parameters.Add("@PayDate", SqlDbType.DateTime, 0).Value = paydate;				
				cmd.CommandText = "dbo.spLocalTransaction_GetVoidableTransactions";
				return convert(execReader(cmd));
			}		
			public bool VoidTransaction(UOW uow, int transaction_id, int create_void_record)
			{
				SqlCommand cmd = uow.Cn.CreateCommand();
				cmd.Transaction = uow.Tran;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@Verifone_Transaction_ID", SqlDbType.Int, 0).Value = transaction_id;
				cmd.Parameters.Add("@CreateVoidTransaction", SqlDbType.Int, 0).Value = create_void_record;
				cmd.CommandText = "dbo.spVFR_VoidTransaction";
				return ((int) cmd.ExecuteNonQuery() != -1);
			}
			void setParam(SqlCommand cmd, LocalTransaction rec)
			{
				cmd.Parameters.Add("@Transaction_ID", SqlDbType.Int, 0).Value = rec.transaction_Id;
 
				if (rec.trConfirm == null)
					cmd.Parameters.Add("@TrConfirm", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.TrConfirm.Length == 0)
						cmd.Parameters.Add("@TrConfirm", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TrConfirm", SqlDbType.VarChar, 20).Value = rec.trConfirm;
				}
 
				if (rec.trLDConfirm == null)
					cmd.Parameters.Add("@TrLDConfirm", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.TrLDConfirm.Length == 0)
						cmd.Parameters.Add("@TrLDConfirm", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TrLDConfirm", SqlDbType.VarChar, 20).Value = rec.trLDConfirm;
				}
 
				if (rec.trNumber == null)
					cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.TrNumber.Length == 0)
						cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TrNumber", SqlDbType.VarChar, 20).Value = rec.trNumber;
				}
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
				if (rec.phNumber == null)
					cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.PhNumber.Length == 0)
						cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = rec.phNumber;
				}
 
				if (rec.payDate == DateTime.MinValue)
					cmd.Parameters.Add("@PayDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@PayDate", SqlDbType.DateTime, 0).Value = rec.payDate;
 
				if (rec.payTime == null)
					cmd.Parameters.Add("@PayTime", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.PayTime.Length == 0)
						cmd.Parameters.Add("@PayTime", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@PayTime", SqlDbType.VarChar, 10).Value = rec.payTime;
				}
				cmd.Parameters.Add("@LocalAmount", SqlDbType.Decimal, 0).Value = rec.localAmount;
				cmd.Parameters.Add("@LDAmount", SqlDbType.Decimal, 0).Value = rec.ldAmount;
				cmd.Parameters.Add("@ComAmount", SqlDbType.Decimal, 0).Value = rec.comAmount;
 
				if (rec.clerkID == null)
					cmd.Parameters.Add("@ClerkID", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.ClerkID.Length == 0)
						cmd.Parameters.Add("@ClerkID", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ClerkID", SqlDbType.VarChar, 10).Value = rec.clerkID;
				}
                
				// Numeric, nullable foreign key treatment:
				if (rec.Transaction_Type_ID == 0)
					cmd.Parameters.Add("@Transaction_Type_ID", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Transaction_Type_ID", SqlDbType.Int, 0).Value = rec.transaction_Type_ID;
                
				// Numeric, nullable foreign key treatment:
				if (rec.Transaction_Method_ID == 0)
					cmd.Parameters.Add("@Transaction_Method_ID", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Transaction_Method_ID", SqlDbType.Int, 0).Value = rec.transaction_Method_ID;
 
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
 
				if (rec.aNI == null)
					cmd.Parameters.Add("@ANI", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.ANI.Length == 0)
						cmd.Parameters.Add("@ANI", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ANI", SqlDbType.VarChar, 10).Value = rec.aNI;
				}
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				LocalTransaction rec = new LocalTransaction();
                
				if (rdr["Verifone_Transaction_ID"] != DBNull.Value)
					rec.transaction_Id = (int) rdr["Verifone_Transaction_ID"];
 
				if (rdr["TrConfirm"] != DBNull.Value)
					rec.trConfirm = (string) rdr["TrConfirm"];
 
				if (rdr["TrLDConfirm"] != DBNull.Value)
					rec.trLDConfirm = (string) rdr["TrLDConfirm"];
 
				if (rdr["TrNumber"] != DBNull.Value)
					rec.trNumber = (string) rdr["TrNumber"];
 
				if (rdr["AccNumber"] != DBNull.Value)
					rec.accNumber = (int) rdr["AccNumber"];
 
				if (rdr["PhNumber"] != DBNull.Value)
					rec.phNumber = (string) rdr["PhNumber"];
 
				if (rdr["PayDate"] != DBNull.Value)
					rec.payDate = (DateTime) rdr["PayDate"];
 
				if (rdr["PayTime"] != DBNull.Value)
					rec.payTime = (string) rdr["PayTime"];
 
				if (rdr["LocalAmount"] != DBNull.Value)
					rec.localAmount = Decimal.Round((decimal)rdr["LocalAmount"], 2);
 
				if (rdr["LDAmount"] != DBNull.Value)
					rec.ldAmount = Decimal.Round((decimal)rdr["LDAmount"], 2);
 
				if (rdr["ComAmount"] != DBNull.Value)
					rec.comAmount = Decimal.Round((decimal)rdr["ComAmount"], 2);
 
				if (rdr["ClerkID"] != DBNull.Value)
					rec.clerkID = (string) rdr["ClerkID"];
 
				if (rdr["Transaction_Type_ID"] != DBNull.Value)
					rec.transaction_Type_ID = (int) rdr["Transaction_Type_ID"];
 
				if (rdr["Transaction_Method_ID"] != DBNull.Value)
					rec.transaction_Method_ID = (int) rdr["Transaction_Method_ID"];
 
				if (rdr["StoreCode"] != DBNull.Value)
					rec.storeCode = (string) rdr["StoreCode"];
 
				if (rdr["ANI"] != DBNull.Value)
					rec.aNI = (string) rdr["ANI"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			LocalTransaction[] convert(DomainObj[] objs)
			{
				LocalTransaction[] acls  = new LocalTransaction[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			#endregion
		}
		#endregion
		#endregion
	}
}
