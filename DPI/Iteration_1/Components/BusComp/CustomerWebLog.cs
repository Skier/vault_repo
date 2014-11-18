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
    public class CustomerWebLog : DomainObj
    {
    #region Data
        static string iName = "CustomerWebLog";
        int id;
        int acctNumber;
        DateTime visitDate;
    #endregion
        
    #region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
            set
            {
                setState();
                id = value;
            }
        }
        public int AcctNumber
        {
            get { return acctNumber; }
            set
            {
                setState();
                acctNumber = value;
            }
        }
        public DateTime VisitDate
        {
            get { return visitDate; }
            set
            {
                setState();
                visitDate = value;
            }
        }
    #endregion
        
    #region Constructors
        public CustomerWebLog()
        {
            sql = new CustomerWebLogSQL();
            rowState = RowState.New;
        }
        public CustomerWebLog(UOW uow) : this()
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
            return new CustomerWebLogSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
    #endregion

    #region	Static methods
        public static CustomerWebLog find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(CustomerWebLog.getKey(id)))
                return (CustomerWebLog)uow.Imap.find(CustomerWebLog.getKey(id));
            
            CustomerWebLog cls = new CustomerWebLog();
            cls.uow = uow;
            cls.id = id;
            cls = (CustomerWebLog)DomainObj.addToIMap(uow, getOne(((CustomerWebLogSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static CustomerWebLog[] getAll(UOW uow)
        {
            CustomerWebLog[] objs = (CustomerWebLog[])DomainObj.addToIMap(uow, (new CustomerWebLogSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
    #endregion

    #region Implementation
        static CustomerWebLog getOne(CustomerWebLog[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CustomerWebLog src, CustomerWebLog tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.acctNumber = src.acctNumber;
            tar.visitDate = src.visitDate;
            tar.rowState = src.rowState;
        }
    #endregion
 
    #region	SQL
        [Serializable]
        class CustomerWebLogSQL : SqlGateway
        {
            public CustomerWebLog[] getKey(CustomerWebLog rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomerWebLog_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                CustomerWebLog rec = (CustomerWebLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomerWebLog_Ins";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CustomerWebLog rec = (CustomerWebLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomerWebLog_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CustomerWebLog rec = (CustomerWebLog)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCustomerWebLog_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CustomerWebLog[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCustomerWebLog_Get_All";
                return convert(execReader(cmd));
            }
        #region Implementation
            void setParam(SqlCommand cmd, CustomerWebLog rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                cmd.Parameters.Add("@AcctNumber", SqlDbType.Int, 0).Value = rec.acctNumber;
 
                if (rec.visitDate == DateTime.MinValue)
                    cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime, 0).Value = rec.visitDate;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                CustomerWebLog rec = new CustomerWebLog();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["AcctNumber"] != DBNull.Value)
                    rec.acctNumber = (int) rdr["AcctNumber"];
 
                if (rdr["VisitDate"] != DBNull.Value)
                    rec.visitDate = (DateTime) rdr["VisitDate"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CustomerWebLog[] convert(DomainObj[] objs)
            {
                CustomerWebLog[] acls  = new CustomerWebLog[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        #endregion
        }
    #endregion
    }
}
