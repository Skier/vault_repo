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
    public class Wireless_Transactions_Archive : DomainObj, IWireless_Transactions, IPayInfoTran
    {
	#region Data
        static string iName = "Wireless_Transactions_Archive";
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
        string invoice_Number;
		decimal commission;
		string status;
		string supplier_tran;
		int acctID;
		IWireless_Custdata customer;
		decimal activationFee;
		decimal taxAmt;
	#endregion

	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, wireless_Transaction_ID.ToString()); }
        }
        public int Wireless_Transaction_ID
        {
            get { return wireless_Transaction_ID; }
            set
            {
                setState();
                wireless_Transaction_ID = value;
            }
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
        public string Invoice_Number
        {
            get { return invoice_Number; }
            set
            {
                setState();
                invoice_Number = value;
            }
        }
		public decimal Commission
		{
			get { return commission; }
			set { commission = decimal.Round(commission, 2); }
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
		public IWireless_Custdata Customer
		{
			get { return customer; }
			set { customer = (IWireless_Custdata)value; }
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
		public decimal TaxAmt
		{
			get { return taxAmt; }
			set 
			{
				setState();
				taxAmt = value;
			}
		}
		public PayInfoSource Source { get { return PayInfoSource.Wireless;	}}
		public int TranNumber       { get { return Wireless_Transaction_ID;	}}
		public decimal ComAmount    { get { return this.Commission;         }}
	#endregion

	#region Constructors
		public Wireless_Transactions_Archive()
        {
            sql = new Wireless_Transactions_ArchiveSQL();
            rowState = RowState.New;
        }
        public Wireless_Transactions_Archive(UOW uow) : this()
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
            return new Wireless_Transactions_ArchiveSQL();
        }
        public override void checkExists()
        {
            if ((Wireless_Transaction_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		public static IWireless_Transactions find(UOW uow, string conf, string storeCode, decimal amount)
		{
			int confirm = int.Parse(conf);

			Wireless_Transactions_Archive[] xact 
				= (Wireless_Transactions_Archive[])DomainObj.addToIMap(
				uow, new Wireless_Transactions_ArchiveSQL().Locate(uow, confirm, storeCode, amount));

			if (xact.Length == 0)
				return null;
			
			for (int i = 0; i < xact.Length; i++)
				xact[i].uow = uow;

			return xact[0];
		}
		public static IWireless_Transactions[] Locate(UOW uow, int id)
		{
			if (uow.Imap.keyExists(Wireless_Transactions_Archive.getKey(id)))
				return new IWireless_Transactions[] 
				{  
					(IWireless_Transactions)uow.Imap.find(Wireless_Transactions_Archive.getKey(id))
				};
            
			return (IWireless_Transactions[])DomainObj.addToIMap(
				uow, new Wireless_Transactions_ArchiveSQL().getKey(uow, id));
		}
        public static Wireless_Transactions_Archive find(UOW uow, int wireless_Transaction_ID)
        {
            if (uow.Imap.keyExists(Wireless_Transactions_Archive.getKey(wireless_Transaction_ID)))
                return (Wireless_Transactions_Archive)uow.Imap.find(Wireless_Transactions_Archive.getKey(wireless_Transaction_ID));
            
            Wireless_Transactions_Archive cls = new Wireless_Transactions_Archive();
            cls.uow = uow;
            cls.wireless_Transaction_ID = wireless_Transaction_ID;
            cls = (Wireless_Transactions_Archive)DomainObj.addToIMap(uow, getOne(((Wireless_Transactions_ArchiveSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Wireless_Transactions_Archive[] getAll(UOW uow)
        {
            Wireless_Transactions_Archive[] objs = (Wireless_Transactions_Archive[])DomainObj.addToIMap(uow, (new Wireless_Transactions_ArchiveSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int wireless_Transaction_ID)
        {
            return new Key(iName, wireless_Transaction_ID.ToString());
        }
	#endregion

	#region	Implementation
        static Wireless_Transactions_Archive getOne(Wireless_Transactions_Archive[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Wireless_Transactions_Archive src, Wireless_Transactions_Archive tar)
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
            tar.invoice_Number = src.invoice_Number;
            tar.rowState = src.rowState;
			tar.commission = src.commission;
			tar.status = src.status;
			tar.supplier_tran = src.supplier_tran;
			tar.acctID = src.acctID;
			tar.activationFee = src.activationFee;
			tar.taxAmt = src.taxAmt;

			tar.rowState = src.rowState;
        }
	#endregion

	#region SQL
		[Serializable]
        class Wireless_Transactions_ArchiveSQL : SqlGateway
        {
            public Wireless_Transactions_Archive[] getKey(Wireless_Transactions_Archive rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Transactions_Archive_Get_Id";
                cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = rec.wireless_Transaction_ID;
                return convert(execReader(cmd));
            }
			public Wireless_Transactions_Archive[] getKey(UOW uow, int id)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spWireless_Transactions_Archive_Get_Id";
				cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = id;
				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                Wireless_Transactions_Archive rec = (Wireless_Transactions_Archive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Transactions_Archive_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Wireless_Transactions_Archive rec = (Wireless_Transactions_Archive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Transactions_Archive_Del_Id";
                cmd.Parameters.Add("@Wireless_Transaction_ID", SqlDbType.Int, 0).Value = rec.wireless_Transaction_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Wireless_Transactions_Archive rec = (Wireless_Transactions_Archive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spWireless_Transactions_Archive_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Wireless_Transactions_Archive[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spWireless_Transactions_Archive_Get_All";
                return convert(execReader(cmd));
            }

			public Wireless_Transactions_Archive[] Locate(UOW uow, int conf, string storeCode, decimal amount)
			{
				SqlCommand cmd = makeCommand(uow);
				
				cmd.CommandText = "spWireless_Transactions_Archive_Get_By_Confirm";
				cmd.Parameters.Add("@Confirm", SqlDbType.Int, 0).Value = conf;
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 14).Value = storeCode;
				cmd.Parameters.Add("@Amount", SqlDbType.Money, 0).Value = amount;
				
				return convert(execReader(cmd));
			}
		#endregion

		#region SQL Implementation
            void setParam(SqlCommand cmd, Wireless_Transactions_Archive rec)
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
                cmd.Parameters.Add("@Commission", SqlDbType.Decimal, 0).Value = rec.commission;
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
 
                cmd.Parameters.Add("@Invoice_Number", SqlDbType.VarChar, 11).Value = rec.invoice_Number;

				if (rec.status == null)
					cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.status.Length == 0)
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
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

				if (rec.acctID == 0)
					cmd.Parameters.Add("@AcctID", SqlDbType.Int, 0).Value = DBNull.Value;
				else
				cmd.Parameters.Add("@AcctID", SqlDbType.Int, 0).Value = rec.acctID;

				cmd.Parameters.Add("@ActivationFee", SqlDbType.Decimal, 0).Value = rec.activationFee;
				cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal, 0).Value = rec.taxAmt;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Wireless_Transactions_Archive rec = new Wireless_Transactions_Archive();
                
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
 
                if (rdr["Invoice_Number"] != DBNull.Value)
                    rec.invoice_Number = (string) rdr["Invoice_Number"];

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
				 
				if (rdr["TaxAmt"] != DBNull.Value)
					rec.taxAmt = Decimal.Round((decimal)rdr["TaxAmt"], 2);
				 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Wireless_Transactions_Archive[] convert(DomainObj[] objs)
            {
                Wireless_Transactions_Archive[] acls  = new Wireless_Transactions_Archive[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
	#endregion
    }
}