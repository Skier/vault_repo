using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PromiseToPay : DomainObj
    {
        #region Static Members

        private static string iName = "PromiseToPay";

        public static PromiseToPay find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(PromiseToPay.getKey(id))) {
                return (PromiseToPay) uow.Imap.find(PromiseToPay.getKey(id));
            }

            PromiseToPay cls = new PromiseToPay();

            cls.uow = uow;
            cls.id = id;
            cls = (PromiseToPay) DomainObj.addToIMap(
                uow, getOne(((PromiseToPaySQL) cls.Sql).getKey(cls)));
            cls.uow = uow;

            return cls;
        }

        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }

        private static PromiseToPay getOne(PromiseToPay[] acls)
        {
            if (acls.Length == 1) {
                return acls[0];
            }

            if (acls.Length == 0) {
                throw new ArgumentException("Row not found");
            }

            throw new ArgumentException("More than one row found");
        }

        public static bool IsEligibleForPromiseToPay(UOW uow, int accNumber)
        {
            PromiseToPaySQL ds = new PromiseToPaySQL();
            return ds.IsEligibleForPromiseToPay(uow, accNumber);
        }

        public static bool DoesPromiseToPayExist(UOW uow, int accNumber, DateTime dueDate)
        {
            PromiseToPaySQL ds = new PromiseToPaySQL();
            return ds.DoesPromiseToPayExist(uow, accNumber, dueDate);
        }

        public static PromiseToPay GetPromiseToPay(UOW uow, int accNumber, DateTime dueDate)
        {
            PromiseToPaySQL ds = new PromiseToPaySQL();
            return ds.GetPromiseToPay(uow, accNumber, dueDate);
        }

        public static DateTime GetPromiseToPayDate(UOW uow, int acctNumber)
        {
            PromiseToPaySQL ds = new PromiseToPaySQL();
            return ds.GetPromiseToPayDate(uow, acctNumber);
        }

        public static void MakeIvrRecord(
            UOW uow, int accNumber, DateTime payDate, 
            decimal payAmount, string userId)
        {
            PromiseToPaySQL ds = new PromiseToPaySQL();
            ds.MakeIvrRecord(uow, accNumber, payDate, payAmount, userId);
        }

        #endregion

        #region Fields

        internal int id;
        internal int accNumber;
        internal DateTime ptpDate;
        internal decimal ptpAmount;
        internal DateTime sDiscoDate;
        internal int dayCredit;
        internal DateTime createdDate;
        internal string userId;

        #endregion

        #region Constructors

        public PromiseToPay()
        {
            base.sql = new PromiseToPaySQL();
            base.rowState = RowState.New;
            this.id = random.Next(Int32.MinValue, -1);
        }

        public PromiseToPay(UOW uow) : this()
        {
            if (uow == null) {
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            }

            if (uow.Imap == null) {
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            }

            this.uow = uow;
            this.uow.Imap.add(this);
        }

        #endregion

        #region Override Methods

        public override IDomKey IKey
        {
            get { return new Key(iName, id.ToString()); }
        }

        protected override SqlGateway loadSql()
        {
            return new PromiseToPaySQL();
        }

        public override void checkExists()
        {
            if (Id < 1) {
                throw new ArgumentException("Valid row is required for update and delete");
            }
        }

        #endregion

        #region Properties

        public int Id
        {
            get { return id; }
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

        public DateTime PtpDate
        {
            get { return ptpDate; }
            set
            {
                setState();
                ptpDate = value;
            }
        }

        public decimal PtpAmount
        {
            get { return ptpAmount; }
            set
            {
                setState();
                ptpAmount = value;
            }
        }

        public DateTime SDiscoDate
        {
            get { return sDiscoDate; }
        }

        public int DayCredit
        {
            get { return dayCredit; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
        }

        public string UserId
        {
            get { return userId; }
            set
            {
                setState();
                userId = value;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        private class PromiseToPaySQL : SqlGateway
        {
            public void MakeIvrRecord(
                UOW uow, int accNumber, DateTime payDate, 
                decimal payAmount, string userId)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spIVR_AddPTP";
                cmd.Parameters.Add("@accnumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@ptpdate", SqlDbType.DateTime).Value = payDate;
                cmd.Parameters.Add("@ptpamount", SqlDbType.SmallMoney).Value = payAmount;
                cmd.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = userId;
                cmd.ExecuteNonQuery();
            }

            public bool IsEligibleForPromiseToPay(UOW uow, int accNumber)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [dbo].[fnPtp_IsEligible](@AccNumber)";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                object value = cmd.ExecuteScalar();
                return ((string) value == "T");
            }

            public bool DoesPromiseToPayExist(UOW uow, int accNumber, DateTime dueDate)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spAccount_CheckPTPByAccNumberAndDueDate";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = dueDate;
                object value = cmd.ExecuteScalar();
                return ((int) value > 0);
            }

            public PromiseToPay GetPromiseToPay(UOW uow, int accNumber, DateTime dueDate)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spAccount_GetPTPByAccNumberAndDueDate";
                cmd.Parameters.Add("@AccNumber", SqlDbType.Int).Value = accNumber;
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = dueDate;

                PromiseToPay ptp = new PromiseToPay();
                using (SqlDataReader rdr = cmd.ExecuteReader()) {
                    if (!rdr.Read()) {
                        throw new ApplicationException("The method 'GetPromiseToPay' must return value.");
                    }

                    ptp.ptpAmount = (decimal) rdr["PTP_Amount"];
                    ptp.ptpDate = (DateTime) rdr["PTP_Date"];
                    ptp.createdDate = (DateTime) rdr["Date_Created"];
                }

                return ptp;
            }

            public DateTime GetPromiseToPayDate(UOW uow, int accNumber)
            {
                SqlCommand cmd = uow.Cn.CreateCommand();
                cmd.Transaction = uow.Tran;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT [dbo].[fnPtp_IVRDate](@accnumber, getdate())";
                cmd.Parameters.Add("@accnumber", SqlDbType.Int).Value = accNumber;
                object value = cmd.ExecuteScalar();
                if (value is DBNull) {
                    return DateTime.MinValue;
                } else {
                    return (DateTime) value;
                }
            }

            public PromiseToPay[] getKey(PromiseToPay rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_GetPTPById";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }

            public override void insert(DomainObj rec)
            {
                throw new NotImplementedException();
            }

            public override void delete(DomainObj rec)
            {
                throw new NotImplementedException();
            }

            public override void update(DomainObj rec)
            {
                throw new NotImplementedException();
            }

            protected override DomainObj reader(SqlDataReader rdr)
            {
                PromiseToPay rec = new PromiseToPay();

                if (rdr["Account_PTP_ID"] != DBNull.Value) {
                    rec.id = (int) rdr["Account_PTP_ID"];
                }

                if (rdr["AccNumber"] != DBNull.Value) {
                    rec.accNumber = (int) rdr["AccNumber"];
                }

                if (rdr["PTP_Date"] != DBNull.Value) {
                    rec.ptpDate = (DateTime) rdr["PTP_Date"];
                }

                if (rdr["PTP_Amount"] != DBNull.Value) {
                    rec.ptpAmount = (decimal) rdr["PTP_Amount"];
                }

                if (rdr["SDiscoDate"] != DBNull.Value) {
                    rec.sDiscoDate = (DateTime) rdr["SDiscoDate"];
                }

                if (rdr["DayCredit"] != DBNull.Value) {
                    rec.dayCredit = (int) rdr["DayCredit"];
                }

                if (rdr["Date_Created"] != DBNull.Value) {
                    rec.createdDate = (DateTime) rdr["Date_Created"];
                }

                if (rdr["UserID"] != DBNull.Value) {
                    rec.userId = (string) rdr["UserID"];
                }

                rec.rowState = RowState.Clean;

                return rec;
            }

            private PromiseToPay[] convert(DomainObj[] objs)
            {
                PromiseToPay[] acls = new PromiseToPay[objs.Length];
                objs.CopyTo(acls, 0);
                return acls;
            }
        }
    }
}