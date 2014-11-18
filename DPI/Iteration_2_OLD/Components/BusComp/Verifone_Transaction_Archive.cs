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
    public class Verifone_Transaction_Archive : DomainObj, IVerifone_Transaction, IPayInfoTran
    {
	#region Data
        static string iName = "Verifone_Transaction_Archive";
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
        string invoice_Number;
	#endregion

	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, verifone_Transaction_ID.ToString()); }
        }
        public int Verifone_Transaction_ID
        {
            get { return verifone_Transaction_ID; }
            set
            {
                setState();
                verifone_Transaction_ID = value;
            }
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
        public string Invoice_Number
        {
            get { return invoice_Number; }
            set
            {
                setState();
                invoice_Number = value;
            }
        }
		public PayInfoSource Source { get { return PayInfoSource.Verifone;	}}
		public int TranNumber       { get { return Verifone_Transaction_ID;	}}  

	#endregion
        
	#region Constructors
        public Verifone_Transaction_Archive()
        {
            sql = new Verifone_Transaction_ArchiveSQL();
            rowState = RowState.New;
        }
        public Verifone_Transaction_Archive(UOW uow) : this()
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
            return new Verifone_Transaction_ArchiveSQL();
        }
        public override void checkExists()
        {
            if ((Verifone_Transaction_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
	#endregion

	#region Static methods
        public static Verifone_Transaction_Archive find(UOW uow, int verifone_Transaction_ID)
        {
            if (uow.Imap.keyExists(Verifone_Transaction_Archive.getKey(verifone_Transaction_ID)))
                return (Verifone_Transaction_Archive)uow.Imap.find(Verifone_Transaction_Archive.getKey(verifone_Transaction_ID));
            
            Verifone_Transaction_Archive cls = new Verifone_Transaction_Archive();
            cls.uow = uow;
            cls.verifone_Transaction_ID = verifone_Transaction_ID;
            cls = (Verifone_Transaction_Archive)DomainObj.addToIMap(uow, getOne(((Verifone_Transaction_ArchiveSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }


		public static IVerifone_Transaction find(UOW uow, int accNumber, int confNum )
		{
			Verifone_Transaction_Archive[] xact 
				= (Verifone_Transaction_Archive[])DomainObj.addToIMap(
				uow, new Verifone_Transaction_ArchiveSQL().Locate(uow, accNumber, confNum));

			if (xact.Length == 0)
				return null;
			
			for (int i = 0; i < xact.Length; i++)
				xact[i].uow = uow;

			return xact[0];
		}


        public static Verifone_Transaction_Archive[] getAll(UOW uow)
        {
            Verifone_Transaction_Archive[] objs = (Verifone_Transaction_Archive[])DomainObj.addToIMap(uow, (new Verifone_Transaction_ArchiveSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int verifone_Transaction_ID)
        {
            return new Key(iName, verifone_Transaction_ID.ToString());
        }
	#endregion

	#region Implementation
        static Verifone_Transaction_Archive getOne(Verifone_Transaction_Archive[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Verifone_Transaction_Archive src, Verifone_Transaction_Archive tar)
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
            tar.invoice_Number = src.invoice_Number;
            tar.rowState = src.rowState;
        }
	#endregion

	#region SQL
        [Serializable]
        class Verifone_Transaction_ArchiveSQL : SqlGateway
        {
            public Verifone_Transaction_Archive[] getKey(Verifone_Transaction_Archive rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Archive_Get_Id";
                cmd.Parameters.Add("@Verifone_Transaction_ID", SqlDbType.Int, 0).Value = rec.verifone_Transaction_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Verifone_Transaction_Archive rec = (Verifone_Transaction_Archive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Archive_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Verifone_Transaction_Archive rec = (Verifone_Transaction_Archive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Archive_Del_Id";
                cmd.Parameters.Add("@Verifone_Transaction_ID", SqlDbType.Int, 0).Value = rec.verifone_Transaction_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Verifone_Transaction_Archive rec = (Verifone_Transaction_Archive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spVerifone_Transaction_Archive_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }

			public Verifone_Transaction_Archive[] Locate(UOW uow, int accNumber, int confNum)
			{
				SqlCommand cmd = makeCommand(uow);
				
				cmd.CommandText = "spVerifone_Transactions_Archive_Get_By_AccNum_Confirm";
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
				cmd.Parameters.Add("@ConfNum",  SqlDbType.VarChar, 20).Value = confNum;
						
				return convert(execReader(cmd));
			}



            public Verifone_Transaction_Archive[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spVerifone_Transaction_Archive_Get_All";
                return convert(execReader(cmd));
            }
		#endregion

	#region Implementation
            void setParam(SqlCommand cmd, Verifone_Transaction_Archive rec)
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
 
                if (rec.invoice_Number == null)
                    cmd.Parameters.Add("@Invoice_Number", SqlDbType.VarChar, 11).Value = DBNull.Value;
                else
                {
                    if (rec.Invoice_Number.Length == 0)
                        cmd.Parameters.Add("@Invoice_Number", SqlDbType.VarChar, 11).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Invoice_Number", SqlDbType.VarChar, 11).Value = rec.invoice_Number;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Verifone_Transaction_Archive rec = new Verifone_Transaction_Archive();
                
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
 
                if (rdr["Invoice_Number"] != DBNull.Value)
                    rec.invoice_Number = (string) rdr["Invoice_Number"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Verifone_Transaction_Archive[] convert(DomainObj[] objs)
            {
                Verifone_Transaction_Archive[] acls  = new Verifone_Transaction_Archive[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
	#endregion
    }
}
