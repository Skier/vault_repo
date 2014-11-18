using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
    public class Account_PaymentLog : DomainObj, IAccountPaymentLog
    {
	#region Data
        static string iName = "Account_PaymentLog";
        int account_PaymentLog_ID;
        int accNumber;
        DateTime date;
        string description;
        decimal amount;
        decimal balance;
	#endregion

	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, account_PaymentLog_ID.ToString()); }
        }
        public int Account_PaymentLog_ID
        {
            get { return account_PaymentLog_ID; }
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
        public DateTime Date
        {
            get { return date; }
            set
            {
                setState();
                date = value;
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
        public decimal Amount
        {
            get { return amount; }
            set
            {
                setState();
                amount = Decimal.Round(value, 2);
            }
        }
        public decimal Balance
        {
            get { return balance; }
            set
            {
                setState();
                balance = Decimal.Round(value, 2);
            }
        }
	#endregion

	#region Constructors
        public Account_PaymentLog()
        {
            sql = new Account_PaymentLogSQL();
            account_PaymentLog_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Account_PaymentLog(UOW uow) : this()
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
            return new Account_PaymentLogSQL();
        }
        public override void checkExists()
        {
            if ((Account_PaymentLog_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
	#endregion

	#region Static methods
		public static void AddEntries(UOW uow, CustData cd, IDemand dmd, decimal startingBalance)
		{
			decimal balance = startingBalance;
			IOrderSum sumry = dmd.OrderSummary(uow);

			for (int i = 0; i < sumry.Items.Length; i++)
			{	
				AddAcctPayLogDmdItem(uow, cd, 
							sumry.Items[i], 
							ref balance, 
							 GetCompTaxAmt(sumry, sumry.Items[i]));

				for (int j = 0; j < sumry.Items[i].TagAlongs.Length; j++ )   // TagAlongs
					AddAcctPayLogDmdItem(uow, cd, sumry.Items[i].TagAlongs[j], 
										ref balance, 
										sumry.GetTaxAmt(sumry.Items[i].TagAlongs[j].Prod, 1));
			}
		}
        public static Account_PaymentLog find(UOW uow, int account_PaymentLog_ID)
        {
            if (uow.Imap.keyExists(Account_PaymentLog.getKey(account_PaymentLog_ID)))
                return (Account_PaymentLog)uow.Imap.find(Account_PaymentLog.getKey(account_PaymentLog_ID));
            
            Account_PaymentLog cls = new Account_PaymentLog();
            cls.uow = uow;
            cls.account_PaymentLog_ID = account_PaymentLog_ID;
            cls = (Account_PaymentLog)DomainObj.addToIMap(uow, getOne(((Account_PaymentLogSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static Account_PaymentLog[] getAccount(UOW uow, int accNumber)
		{
			Account_PaymentLog[] objs = (Account_PaymentLog[])DomainObj.addToIMap(uow, (new Account_PaymentLogSQL()).getAccount(uow, accNumber));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Account_PaymentLog[] getAll(UOW uow)
        {
            Account_PaymentLog[] objs = (Account_PaymentLog[])DomainObj.addToIMap(uow, (new Account_PaymentLogSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int account_PaymentLog_ID)
        {
            return new Key(iName, account_PaymentLog_ID.ToString());
        }
	#endregion

	#region Implementation
		static decimal GetCompTaxAmt(IOrderSum sumry, IDmdItem item)
		{	
			decimal taxAmount = sumry.GetTaxAmt(item.Prod, 1);
			if (item.Components == null)
				return decimal.Round(taxAmount, 2);

			IDmdItem[] comps = item.Components;

			for (int i = 0; i < comps.Length; i++)
				taxAmount += sumry.GetTaxAmt(comps[i].Prod, 1);

			return decimal.Round(taxAmount, 2);
		}
		static void AddAcctPayLogDmdItem(UOW uow, CustData cd, IDmdItem dmdItem, ref decimal balance, decimal taxAmt)
		{
			string prodName = (ProdInfoCol.GetProd(dmdItem.Prod)).ProdName;
			
			if (!IsMonth1(dmdItem))
				return;

			AddPaymtLog(uow, cd, prodName, dmdItem.EffPrice, ref balance); // Item
			AddPaymtLog(uow, cd, "Tax:" + prodName, taxAmt, ref balance);  // Item + components + tag alongs taxes
		}
		static bool IsMonth1(IDmdItem pp)
		{
			const int month = 1;

			int startMon = (ProdInfoCol.GetProd(pp.Prod)).StartServMon;
			int endMon   = (ProdInfoCol.GetProd(pp.Prod)).EndServMon;

			if (endMon == 0)
				endMon = int.MaxValue;

			if (startMon > month)
				return false;

			if (endMon < month)
				return false;

			return true;
		}
		static void AddPaymtLog(UOW uow, CustData cd, string prodName, decimal amt, ref decimal balance)
		{
			if (amt == 0m)
				return;

			Account_PaymentLog pl = new Account_PaymentLog(uow);

			pl.AccNumber   = cd.AccNumber;
			pl.Amount      = -amt;
			balance       += pl.Amount;
			pl.Balance     = balance;
			pl.Date        = DateTime.Now;
			pl.Description = prodName;
			
			pl.add();
		}	
        static Account_PaymentLog getOne(Account_PaymentLog[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Account_PaymentLog src, Account_PaymentLog tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.account_PaymentLog_ID = src.account_PaymentLog_ID;
            tar.accNumber = src.accNumber;
            tar.date = src.date;
            tar.description = src.description;
            tar.amount = src.amount;
            tar.balance = src.balance;
            tar.rowState = src.rowState;
        }
	#endregion

	#region SQL
        [Serializable]
        class Account_PaymentLogSQL : SqlGateway
        {
            public Account_PaymentLog[] getKey(Account_PaymentLog rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_PaymentLog_Get_Id";
                cmd.Parameters.Add("@Account_PaymentLog_ID", SqlDbType.Int, 0).Value = rec.account_PaymentLog_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Account_PaymentLog rec = (Account_PaymentLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_PaymentLog_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Account_PaymentLog_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.account_PaymentLog_ID = (int)cmd.Parameters["@Account_PaymentLog_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Account_PaymentLog rec = (Account_PaymentLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_PaymentLog_Del_Id";
                cmd.Parameters.Add("@Account_PaymentLog_ID", SqlDbType.Int, 0).Value = rec.account_PaymentLog_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Account_PaymentLog rec = (Account_PaymentLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_PaymentLog_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
			public Account_PaymentLog[] getAccount(UOW uow, int accNumber)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spAccount_PaymentLog_Get_Account";
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = accNumber;
				
				return convert(execReader(cmd));
			}
            public Account_PaymentLog[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAccount_PaymentLog_Get_All";

                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Account_PaymentLog rec)
            {
                cmd.Parameters.Add("@Account_PaymentLog_ID", SqlDbType.Int, 0).Value = rec.account_PaymentLog_ID;
                
                // Numeric, nullable foreign key treatment:
                if (rec.AccNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
                cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = rec.date;
 
                cmd.Parameters.Add("@Description", SqlDbType.VarChar, 50).Value = rec.description;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal, 0).Value = rec.amount;
                cmd.Parameters.Add("@Balance", SqlDbType.Decimal, 0).Value = rec.balance;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Account_PaymentLog rec = new Account_PaymentLog();
                
                if (rdr["Account_PaymentLog_ID"] != DBNull.Value)
                    rec.account_PaymentLog_ID = (int) rdr["Account_PaymentLog_ID"];
 
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["Date"] != DBNull.Value)
                    rec.date = (DateTime) rdr["Date"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                if (rdr["Amount"] != DBNull.Value)
                    rec.amount = Decimal.Round((decimal)rdr["Amount"], 2);
 
                if (rdr["Balance"] != DBNull.Value)
                    rec.balance = Decimal.Round((decimal)rdr["Balance"], 2);
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Account_PaymentLog[] convert(DomainObj[] objs)
            {
                Account_PaymentLog[] acls  = new Account_PaymentLog[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
	#endregion
    }
}