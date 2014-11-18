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
    public class Energy_Transactions : DomainObj, IEnergy_Transactions, IPayInfoTran
    {
    #region Data
        static string iName = "Energy_Transactions";
        int iD;
        int confirmNum;
        DateTime payDateTime;
        decimal tran_Amount;
        string storeCode;
        string clerkid;
        string pin;
        decimal commission;
        string status;
        int acctID;
        decimal activationFee;
        decimal taxAmt;
    #endregion
        
    #region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, iD.ToString()); }
        }
        public int ID
        {
            get { return iD; }
        }
        public int ConfirmNum
        {
            get { return confirmNum; }
            set
            {
                setState();
                confirmNum = value;
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
                activationFee = Decimal.Round(value, 2);
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

		public PayInfoSource Source { get { return PayInfoSource.Energy;	}}
		public int TranNumber       { get { return this.iD;	}}
		public decimal ComAmount    { get { return this.commission;         }}
    #endregion
        
    #region Constructors
        public Energy_Transactions()
        {
            sql = new Energy_TransactionsSQL();
            iD = random.Next(Int32.MinValue, -1);
			this.priority =  16600;
            rowState = RowState.New;
        }
        public Energy_Transactions(UOW uow) : this()
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
            return new Energy_TransactionsSQL();
        }
        public override void checkExists()
        {
            if ((ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
    #endregion

    #region	Static methods
        public static Energy_Transactions find(UOW uow, int iD)
        {
            if (uow.Imap.keyExists(Energy_Transactions.getKey(iD)))
                return (Energy_Transactions)uow.Imap.find(Energy_Transactions.getKey(iD));
            
            Energy_Transactions cls = new Energy_Transactions();
            cls.uow = uow;
            cls.iD = iD;
            cls = (Energy_Transactions)DomainObj.addToIMap(uow, getOne(((Energy_TransactionsSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Energy_Transactions[] getAll(UOW uow)
        {
            Energy_Transactions[] objs = (Energy_Transactions[])DomainObj.addToIMap(uow, (new Energy_TransactionsSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int iD)
        {
            return new Key(iName, iD.ToString());
        }
    #endregion

    #region Implementation
        static Energy_Transactions getOne(Energy_Transactions[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Energy_Transactions src, Energy_Transactions tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.iD = src.iD;
            tar.confirmNum = src.confirmNum;
            tar.payDateTime = src.payDateTime;
            tar.tran_Amount = src.tran_Amount;
            tar.storeCode = src.storeCode;
            tar.clerkid = src.clerkid;
            tar.pin = src.pin;
            tar.commission = src.commission;
            tar.status = src.status;
            tar.acctID = src.acctID;
            tar.activationFee = src.activationFee;
            tar.taxAmt = src.taxAmt;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class Energy_TransactionsSQL : SqlGateway
        {
            public Energy_Transactions[] getKey(Energy_Transactions rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergy_Transactions_Get_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Energy_Transactions rec = (Energy_Transactions)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergy_Transactions_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.iD = (int)cmd.Parameters["@ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Energy_Transactions rec = (Energy_Transactions)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergy_Transactions_Del_Id";
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Energy_Transactions rec = (Energy_Transactions)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spEnergy_Transactions_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Energy_Transactions[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spEnergy_Transactions_Get_All";
                return convert(execReader(cmd));
            }
        #region Implementation
            void setParam(SqlCommand cmd, Energy_Transactions rec)
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int, 0).Value = rec.iD;
                cmd.Parameters.Add("@ConfirmNum", SqlDbType.Int, 0).Value = rec.confirmNum;
 
                cmd.Parameters.Add("@PayDateTime", SqlDbType.DateTime, 0).Value = rec.payDateTime;
                cmd.Parameters.Add("@Tran_Amount", SqlDbType.Decimal, 0).Value = rec.tran_Amount;
 
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
 
                if (rec.pin == null)
                    cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 250).Value = DBNull.Value;
                else
                {
                    if (rec.Pin.Length == 0)
                        cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 250).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 250).Value = rec.pin;
                }
                cmd.Parameters.Add("@Commission", SqlDbType.Decimal, 0).Value = rec.commission;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.AcctID == 0)
                    cmd.Parameters.Add("@AcctID", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AcctID", SqlDbType.Int, 0).Value = rec.acctID;
                cmd.Parameters.Add("@ActivationFee", SqlDbType.Decimal, 0).Value = rec.activationFee;
                cmd.Parameters.Add("@TaxAmt", SqlDbType.Decimal, 0).Value = rec.taxAmt;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Energy_Transactions rec = new Energy_Transactions();
                
                if (rdr["ID"] != DBNull.Value)
                    rec.iD = (int) rdr["ID"];
 
                if (rdr["ConfirmNum"] != DBNull.Value)
                    rec.confirmNum = (int) rdr["ConfirmNum"];
 
                if (rdr["PayDateTime"] != DBNull.Value)
                    rec.payDateTime = (DateTime) rdr["PayDateTime"];
 
                if (rdr["Tran_Amount"] != DBNull.Value)
                    rec.tran_Amount = Decimal.Round((decimal)rdr["Tran_Amount"], 2);
 
                if (rdr["StoreCode"] != DBNull.Value)
                    rec.storeCode = (string) rdr["StoreCode"];
 
                if (rdr["Clerkid"] != DBNull.Value)
                    rec.clerkid = (string) rdr["Clerkid"];
 
                if (rdr["Pin"] != DBNull.Value)
                    rec.pin = (string) rdr["Pin"];
 
                if (rdr["Commission"] != DBNull.Value)
                    rec.commission = Decimal.Round((decimal)rdr["Commission"], 2);
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["AcctID"] != DBNull.Value)
                    rec.acctID = (int) rdr["AcctID"];
 
                if (rdr["ActivationFee"] != DBNull.Value)
                    rec.activationFee = Decimal.Round((decimal)rdr["ActivationFee"], 2);
 
                if (rdr["TaxAmt"] != DBNull.Value)
                    rec.taxAmt = Decimal.Round((decimal)rdr["TaxAmt"], 2);
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Energy_Transactions[] convert(DomainObj[] objs)
            {
                Energy_Transactions[] acls  = new Energy_Transactions[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
