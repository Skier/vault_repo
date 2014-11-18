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
    public class Verifone_Transaction : DomainObj, IVerifone_Transaction, IPayInfoTran
    {
        /*        Data        */
        static string iName = "Verifone_Transaction";
        int verifone_Transaction_ID;
        string trConfirm;
        string trLDConfirm;
        string trNumber;
        int accNumber;
        string phNumber;
        DateTime payDate;
        string payTime;
        decimal localAmount;
        decimal lDAmount;
        decimal comAmount;
        string clerkID;
        int transaction_Type_ID;
        int transaction_Method_ID;
        string storeCode;
        string aNI;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, verifone_Transaction_ID.ToString()); }
        }
        public int Verifone_Transaction_ID
        {
            get { return verifone_Transaction_ID; }
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
            get { return lDAmount; }
            set
            {
                setState();
                lDAmount = Decimal.Round(value, 2);
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
		public PayInfoSource Source { get { return PayInfoSource.Verifone;	}}
		public int TranNumber       { get { return Verifone_Transaction_ID;	}}       
        /*        Constructors			*/
        public Verifone_Transaction()
        {
            sql = new Verifone_TransactionSQL();
            verifone_Transaction_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Verifone_Transaction(UOW uow) : this()
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
            return new Verifone_TransactionSQL();
        }
        public override void checkExists()
        {
            if ((Verifone_Transaction_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Verifone_Transaction find(UOW uow, int verifone_Transaction_ID)
        {
            if (uow.Imap.keyExists(Verifone_Transaction.getKey(verifone_Transaction_ID)))
                return (Verifone_Transaction)uow.Imap.find(Verifone_Transaction.getKey(verifone_Transaction_ID));
            
            Verifone_Transaction cls = new Verifone_Transaction();
            cls.uow = uow;
            cls.verifone_Transaction_ID = verifone_Transaction_ID;
            cls = (Verifone_Transaction)DomainObj.addToIMap(uow, getOne(((Verifone_TransactionSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }

		public static Verifone_Transaction find(UOW uow, IPayInfo payInfo)
		{
			Verifone_Transaction cls = (Verifone_Transaction)DomainObj.addToIMap(uow, getOne( new Verifone_TransactionSQL().getKey(uow, payInfo)));
			cls.uow = uow;
            
			return cls;
		}
		public static IVerifone_Transaction find(UOW uow, int accNumber, string confNum)
		{
			int confNumber = int.Parse(confNum);

			Verifone_Transaction[] xact 
				= (Verifone_Transaction[])DomainObj.addToIMap(
				uow, new Verifone_TransactionSQL().Locate(uow, accNumber, confNumber));

			if (xact.Length == 0)
				return Verifone_Transaction_Archive.find(uow, accNumber, confNumber);

			for (int i = 0; i < xact.Length; i++)
				xact[i].uow = uow;

			return xact[0];
		}		
        public static Verifone_Transaction[] getAll(UOW uow)
        {
            Verifone_Transaction[] objs = (Verifone_Transaction[])DomainObj.addToIMap(uow, (new Verifone_TransactionSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int verifone_Transaction_ID)
        {
            return new Key(iName, verifone_Transaction_ID.ToString());
        }
		public static decimal GetCommissionAmt(UOW uow, int accNumber, string confNum)
		{
			IVerifone_Transaction vt = find(uow, accNumber, confNum);

			if (vt == null)
				return 0M;
 
			return vt.ComAmount;
		}
        /*		Implementation		*/
        static Verifone_Transaction getOne(Verifone_Transaction[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Verifone_Transaction src, Verifone_Transaction tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.verifone_Transaction_ID = src.verifone_Transaction_ID;
            tar.trConfirm = src.trConfirm;
            tar.trLDConfirm = src.trLDConfirm;
            tar.trNumber = src.trNumber;
            tar.accNumber = src.accNumber;
            tar.phNumber = src.phNumber;
            tar.payDate = src.payDate;
            tar.payTime = src.payTime;
            tar.localAmount = src.localAmount;
            tar.lDAmount = src.lDAmount;
            tar.comAmount = src.comAmount;
            tar.clerkID = src.clerkID;
            tar.transaction_Type_ID = src.transaction_Type_ID;
            tar.transaction_Method_ID = src.transaction_Method_ID;
            tar.storeCode = src.storeCode;
            tar.aNI = src.aNI;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class Verifone_TransactionSQL : SqlGateway
        {
            public Verifone_Transaction[] getKey(Verifone_Transaction rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Get_Id";
                cmd.Parameters.Add("@Verifone_Transaction_ID", SqlDbType.Int, 0).Value = rec.verifone_Transaction_ID;
                return convert(execReader(cmd));
            }
			public Verifone_Transaction[] getKey(UOW uow, IPayInfo payInfo)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spVerifone_Transaction_Get_By_TrConfirm";
				cmd.Parameters.Add("@trConfirm", SqlDbType.VarChar, 20).Value = payInfo.VFConf;
				return convert(execReader(cmd));
			}
			public Verifone_Transaction[] Locate(UOW uow, int accNumber, int confNum)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spVerifone_Transaction_Get_By_Acc_ConfNum";
				cmd.Parameters.Add("@AccNumber", SqlDbType.VarChar, 20).Value = accNumber;
				cmd.Parameters.Add("@ConfNum", SqlDbType.VarChar, 20).Value = confNum;
				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                Verifone_Transaction rec = (Verifone_Transaction)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Verifone_Transaction_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.verifone_Transaction_ID = (int)cmd.Parameters["@Verifone_Transaction_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Verifone_Transaction rec = (Verifone_Transaction)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Del_Id";
                cmd.Parameters.Add("@Verifone_Transaction_ID", SqlDbType.Int, 0).Value = rec.verifone_Transaction_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Verifone_Transaction rec = (Verifone_Transaction)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Verifone_Transaction[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spVerifone_Transaction_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Verifone_Transaction rec)
            {
                cmd.Parameters.Add("@Verifone_Transaction_ID", SqlDbType.Int, 0).Value = rec.verifone_Transaction_ID;
 
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
                
                // Numeric, nullable foreign key treatment:
                if (rec.AccNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
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
                cmd.Parameters.Add("@LDAmount", SqlDbType.Decimal, 0).Value = rec.lDAmount;
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
                Verifone_Transaction rec = new Verifone_Transaction();
                
                if (rdr["Verifone_Transaction_ID"] != DBNull.Value)
                    rec.verifone_Transaction_ID = (int) rdr["Verifone_Transaction_ID"];
 
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
                    rec.lDAmount = Decimal.Round((decimal)rdr["LDAmount"], 2);
 
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
            Verifone_Transaction[] convert(DomainObj[] objs)
            {
                Verifone_Transaction[] acls  = new Verifone_Transaction[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
