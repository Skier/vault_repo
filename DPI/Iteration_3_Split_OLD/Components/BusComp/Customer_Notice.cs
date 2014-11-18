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
    public class Customer_Notice : DomainObj
    {
        /*        Data        */
        static string iName = "Customer_Notice";
        int notice_ID;
        int accNumber;
        string notice;
        DateTime bill_Date;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, notice_ID.ToString()); }
        }
        public int Notice_ID
        {
            get { return notice_ID; }
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
        public string Notice
        {
            get { return notice; }
            set
            {
                setState();
                notice = value;
            }
        }
        public DateTime Bill_Date
        {
            get { return bill_Date; }
            set
            {
                setState();
                bill_Date = value;
            }
        }
        
        /*        Constructors			*/
        public Customer_Notice()
        {
            sql = new Customer_NoticeSQL();
            notice_ID = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Customer_Notice(UOW uow) : this()
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
            return new Customer_NoticeSQL();
        }
        public override void checkExists()
        {
            if ((Notice_ID < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Customer_Notice find(UOW uow, int notice_ID)
        {
            if (uow.Imap.keyExists(Customer_Notice.getKey(notice_ID)))
                return (Customer_Notice)uow.Imap.find(Customer_Notice.getKey(notice_ID));
            
            Customer_Notice cls = new Customer_Notice();
            cls.uow = uow;
            cls.notice_ID = notice_ID;
            cls = (Customer_Notice)DomainObj.addToIMap(uow, getOne(((Customer_NoticeSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Customer_Notice[] getAll(UOW uow)
        {
            Customer_Notice[] objs = (Customer_Notice[])DomainObj.addToIMap(uow, (new Customer_NoticeSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static Customer_Notice[] getDate(UOW uow, DateTime date)
		{
			Customer_Notice[] objs = (Customer_Notice[])DomainObj.addToIMap(uow, (new Customer_NoticeSQL()).getDate(uow, date));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(int notice_ID)
        {
            return new Key(iName, notice_ID.ToString());
        }
        /*		Implementation		*/
        static Customer_Notice getOne(Customer_Notice[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Customer_Notice src, Customer_Notice tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.notice_ID = src.notice_ID;
            tar.accNumber = src.accNumber;
            tar.notice = src.notice;
            tar.bill_Date = src.bill_Date;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class Customer_NoticeSQL : SqlGateway
        {
            public Customer_Notice[] getKey(Customer_Notice rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_Notice_Get_Id";
                cmd.Parameters.Add("@Notice_ID", SqlDbType.Int, 0).Value = rec.notice_ID;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Customer_Notice rec = (Customer_Notice)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_Notice_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Notice_ID"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.notice_ID = (int)cmd.Parameters["@Notice_ID"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Customer_Notice rec = (Customer_Notice)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_Notice_Del_Id";
                cmd.Parameters.Add("@Notice_ID", SqlDbType.Int, 0).Value = rec.notice_ID;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Customer_Notice rec = (Customer_Notice)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomer_Notice_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Customer_Notice[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCustomer_Notice_Get_All";
                return convert(execReader(cmd));
            }
			public Customer_Notice[] getDate(UOW uow, DateTime date)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spCustomer_Notice_Get_Date";
				DateTime from = new DateTime(date.Year,date.Month, date.Day);
				DateTime to = new DateTime(date.Year, date.Month, date.Day, 23, 59,59, 999);
				
				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = from;
				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = to;

				return convert(execReader(cmd));
			}
            /*        Implementation        */
            void setParam(SqlCommand cmd, Customer_Notice rec)
            {
                cmd.Parameters.Add("@Notice_ID", SqlDbType.Int, 0).Value = rec.notice_ID;
                
                // Numeric, nullable foreign key treatment:
                if (rec.AccNumber == 0)
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
                cmd.Parameters.Add("@Notice", SqlDbType.VarChar, 50).Value = rec.notice;
 
                if (rec.bill_Date == DateTime.MinValue)
                    cmd.Parameters.Add("@Bill_Date", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Bill_Date", SqlDbType.DateTime, 0).Value = rec.bill_Date;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Customer_Notice rec = new Customer_Notice();
                
                if (rdr["Notice_ID"] != DBNull.Value)
                    rec.notice_ID = (int) rdr["Notice_ID"];
 
                if (rdr["AccNumber"] != DBNull.Value)
                    rec.accNumber = (int) rdr["AccNumber"];
 
                if (rdr["Notice"] != DBNull.Value)
                    rec.notice = (string) rdr["Notice"];
 
                if (rdr["Bill_Date"] != DBNull.Value)
                    rec.bill_Date = (DateTime) rdr["Bill_Date"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Customer_Notice[] convert(DomainObj[] objs)
            {
                Customer_Notice[] acls  = new Customer_Notice[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
