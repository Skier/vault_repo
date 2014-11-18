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
	[Serializable]
    public class Account_ActivityLog : DomainObj
    {
        /*        Data        */
        static string iName = "Account_ActivityLog";
        int account_ActivityLog_ID;
        int accNumber;
        DateTime date;
        string userid;
		string activity;
        string department;

        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, account_ActivityLog_ID.ToString()); }
        }
        public int Account_ActivityLog_ID
        {
            get { return account_ActivityLog_ID; }
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
        public string UserId
        {
            get { return userid; }
            set
            {
                setState();
                userid = value;
            }
        }
        public string Activity
        {
            get { return activity; }
            set
            {
                setState();
                activity = value;
            }
        }
        public string Department
        {
            get { return department; }
            set
            {
                setState();
                department = value;
            }
        }
        
        /*        Constructors			*/
        public Account_ActivityLog()
        {
            sql = new Account_ActivityLogSQL();
            account_ActivityLog_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Account_ActivityLog(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
		public Account_ActivityLog(UOW uow, string userId, int accNumber, string activity, string dept) : this(uow)
		{
			this.date = DateTime.Now;
			this.userid = userId;
			this.accNumber = accNumber;
			this.activity = activity;
			this.department = dept;
		}
        
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new Account_ActivityLogSQL();
        }
        public override void checkExists()
        {
            if ((Account_ActivityLog_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Account_ActivityLog find(UOW uow, int account_ActivityLog_ID)
        {
            if (uow.Imap.keyExists(Account_ActivityLog.getKey(account_ActivityLog_ID)))
                return (Account_ActivityLog)uow.Imap.find(Account_ActivityLog.getKey(account_ActivityLog_ID));
            
            Account_ActivityLog cls = new Account_ActivityLog();
            cls.uow = uow;
            cls.account_ActivityLog_ID = account_ActivityLog_ID;
            cls = (Account_ActivityLog)DomainObj.addToIMap(uow, getOne(((Account_ActivityLogSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Account_ActivityLog[] getAll(UOW uow)
        {
            Account_ActivityLog[] objs = (Account_ActivityLog[])DomainObj.addToIMap(uow, (new Account_ActivityLogSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int account_ActivityLog_ID)
        {
            return new Key(iName, account_ActivityLog_ID.ToString());
        }
        /*		Implementation		*/
        static Account_ActivityLog getOne(Account_ActivityLog[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Account_ActivityLog src, Account_ActivityLog tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.account_ActivityLog_ID = src.account_ActivityLog_ID;
            tar.accNumber = src.accNumber;
            tar.date = src.date;
            tar.userid = src.userid;
            tar.activity = src.activity;
            tar.department = src.department;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class Account_ActivityLogSQL : SqlGateway
        {
            public Account_ActivityLog[] getKey(Account_ActivityLog rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_ActivityLog_Get_Id";
                cmd.Parameters.Add("@Account_ActivityLog_ID", SqlDbType.Int, 0).Value = rec.account_ActivityLog_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Account_ActivityLog rec = (Account_ActivityLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_ActivityLog_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Account_ActivityLog_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.account_ActivityLog_ID = (int)cmd.Parameters["@Account_ActivityLog_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Account_ActivityLog rec = (Account_ActivityLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_ActivityLog_Del_Id";
                cmd.Parameters.Add("@Account_ActivityLog_ID", SqlDbType.Int, 0).Value = rec.account_ActivityLog_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Account_ActivityLog rec = (Account_ActivityLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAccount_ActivityLog_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Account_ActivityLog[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAccount_ActivityLog_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Account_ActivityLog rec)
            {
                cmd.Parameters.Add("@Account_ActivityLog_ID", SqlDbType.Int, 0).Value = rec.account_ActivityLog_ID;
                
                // Numeric, nullable foreign key treatment:
                if (rec.AccNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
                cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = rec.date;
 
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = rec.userid;
                cmd.Parameters.Add("@Activity", SqlDbType.VarChar, 256).Value = rec.activity;
                cmd.Parameters.Add("@Department", SqlDbType.VarChar, 20).Value = rec.department;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Account_ActivityLog rec = new Account_ActivityLog();
                
                if (rdr["Account_ActivityLog_ID"] != DBNull.Value)
                    rec.account_ActivityLog_ID = (int) rdr["Account_ActivityLog_ID"];
 
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["Date"] != DBNull.Value)
                    rec.date = (DateTime) rdr["Date"];
 
                if (rdr["UserId"] != DBNull.Value)
                    rec.userid = (string) rdr["UserId"];
 
                if (rdr["Actvity"] != DBNull.Value)
                    rec.activity = (string) rdr["Activity"];
 
                if (rdr["Department"] != DBNull.Value)
                    rec.department = (string) rdr["Department"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Account_ActivityLog[] convert(DomainObj[] objs)
            {
                Account_ActivityLog[] acls  = new Account_ActivityLog[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
