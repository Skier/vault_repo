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
    public class Statement : DomainObj
    {
        /*        Data        */
        static string iName = "Statement";
        int stmtId;
		int servPeriod;
        int billpayer;
        string stmtType;
        string status;
        DateTime perStartDate;
        DateTime perEndDate;
        DateTime lastPymtDate;
        decimal lastPymtAmt;
        DateTime pymtDueDate;
        decimal pymtDueAmt;
        decimal totBalance;
        decimal unapplCR;
        decimal unbldBalance;
        decimal unbldDR;
        decimal unbldUsageDR;
        decimal unbldCR;
        decimal ubldApplCR;
        decimal billedBalance;
        decimal billedDR;
        decimal billedApplCR;
        decimal pD30Balance;
        decimal pD30DR;
        decimal pD30ApplCR;
        decimal pDOver30Balance;
        decimal pD0ver30DR;
        decimal pD0ver30ApplCR;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, stmtId.ToString()); }
        }
        public int StmtId
        {
            get { return stmtId; }
        }
		public int ServPeriod { get { return servPeriod; }}

        public int Billpayer
        {
            get { return billpayer; }
            set
            {
                setState();
                billpayer = value;
            }
        }
        public string StmtType
        {
            get { return stmtType; }
            set
            {
                setState();
                stmtType = value;
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
        public DateTime PerStartDate
        {
            get { return perStartDate; }
            set
            {
                setState();
                perStartDate = value;
            }
        }
        public DateTime PerEndDate
        {
            get { return perEndDate; }
            set
            {
                setState();
                perEndDate = value;
            }
        }
        public DateTime LastPymtDate
        {
            get { return lastPymtDate; }
            set
            {
                setState();
                lastPymtDate = value;
            }
        }
        public decimal LastPymtAmt
        {
            get { return lastPymtAmt; }
            set
            {
                setState();
                lastPymtAmt = Decimal.Round(value, 2);
            }
        }
        public DateTime PymtDueDate
        {
            get { return pymtDueDate; }
            set
            {
                setState();
                pymtDueDate = value;
            }
        }
        public decimal PymtDueAmt
        {
            get { return pymtDueAmt; }
            set
            {
                setState();
                pymtDueAmt = Decimal.Round(value, 2);
            }
        }
        public decimal TotBalance
        {
            get { return totBalance; }
            set
            {
                setState();
                totBalance = Decimal.Round(value, 2);
            }
        }
        public decimal UnapplCR
        {
            get { return unapplCR; }
            set
            {
                setState();
                unapplCR = Decimal.Round(value, 2);
            }
        }
        public decimal UnbldBalance
        {
            get { return unbldBalance; }
            set
            {
                setState();
                unbldBalance = Decimal.Round(value, 2);
            }
        }
        public decimal UnbldDR
        {
            get { return unbldDR; }
            set
            {
                setState();
                unbldDR = Decimal.Round(value, 2);
            }
        }
        public decimal UnbldUsageDR
        {
            get { return unbldUsageDR; }
            set
            {
                setState();
                unbldUsageDR = Decimal.Round(value, 2);
            }
        }
        public decimal UnbldCR
        {
            get { return unbldCR; }
            set
            {
                setState();
                unbldCR = Decimal.Round(value, 2);
            }
        }
        public decimal UbldApplCR
        {
            get { return ubldApplCR; }
            set
            {
                setState();
                ubldApplCR = Decimal.Round(value, 2);
            }
        }
        public decimal BilledBalance
        {
            get { return billedBalance; }
            set
            {
                setState();
                billedBalance = Decimal.Round(value, 2);
            }
        }
        public decimal BilledDR
        {
            get { return billedDR; }
            set
            {
                setState();
                billedDR = Decimal.Round(value, 2);
            }
        }
        public decimal BilledApplCR
        {
            get { return billedApplCR; }
            set
            {
                setState();
                billedApplCR = Decimal.Round(value, 2);
            }
        }
        public decimal PD30Balance
        {
            get { return pD30Balance; }
            set
            {
                setState();
                pD30Balance = Decimal.Round(value, 2);
            }
        }
        public decimal PD30DR
        {
            get { return pD30DR; }
            set
            {
                setState();
                pD30DR = Decimal.Round(value, 2);
            }
        }
        public decimal PD30ApplCR
        {
            get { return pD30ApplCR; }
            set
            {
                setState();
                pD30ApplCR = Decimal.Round(value, 2);
            }
        }
        public decimal PDOver30Balance
        {
            get { return pDOver30Balance; }
            set
            {
                setState();
                pDOver30Balance = Decimal.Round(value, 2);
            }
        }
        public decimal PD0ver30DR
        {
            get { return pD0ver30DR; }
            set
            {
                setState();
                pD0ver30DR = Decimal.Round(value, 2);
            }
        }
        public decimal PD0ver30ApplCR
        {
            get { return pD0ver30ApplCR; }
            set
            {
                setState();
                pD0ver30ApplCR = Decimal.Round(value, 2);
            }
        }
        
        /*        Constructors			*/
        public Statement()
        {
            sql = new StatementSQL();
            stmtId = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Statement(UOW uow) : this()
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
            return new StatementSQL();
        }
        public override void checkExists()
        {
            if ((StmtId < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Statement find(UOW uow, int stmtId)
        {
            if (uow.Imap.keyExists(Statement.getKey(stmtId)))
                return (Statement)uow.Imap.find(Statement.getKey(stmtId));
            
            Statement cls = new Statement();
            cls.uow = uow;
            cls.stmtId = stmtId;
            cls = (Statement)DomainObj.addToIMap(uow, getOne(((StatementSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Statement[] getAll(UOW uow)
        {
            Statement[] objs = (Statement[])DomainObj.addToIMap(uow, (new StatementSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int stmtId)
        {
            return new Key(iName, stmtId.ToString());
        }
        /*		Implementation		*/
        static Statement getOne(Statement[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Statement src, Statement tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.stmtId = src.stmtId;
			tar.servPeriod = src.servPeriod;
            tar.ver = src.ver;
            tar.billpayer = src.billpayer;
            tar.stmtType = src.stmtType;
            tar.status = src.status;
            tar.perStartDate = src.perStartDate;
            tar.perEndDate = src.perEndDate;
            tar.lastPymtDate = src.lastPymtDate;
            tar.lastPymtAmt = src.lastPymtAmt;
            tar.pymtDueDate = src.pymtDueDate;
            tar.pymtDueAmt = src.pymtDueAmt;
            tar.totBalance = src.totBalance;
            tar.unapplCR = src.unapplCR;
            tar.unbldBalance = src.unbldBalance;
            tar.unbldDR = src.unbldDR;
            tar.unbldUsageDR = src.unbldUsageDR;
            tar.unbldCR = src.unbldCR;
            tar.ubldApplCR = src.ubldApplCR;
            tar.billedBalance = src.billedBalance;
            tar.billedDR = src.billedDR;
            tar.billedApplCR = src.billedApplCR;
            tar.pD30Balance = src.pD30Balance;
            tar.pD30DR = src.pD30DR;
            tar.pD30ApplCR = src.pD30ApplCR;
            tar.pDOver30Balance = src.pDOver30Balance;
            tar.pD0ver30DR = src.pD0ver30DR;
            tar.pD0ver30ApplCR = src.pD0ver30ApplCR;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class StatementSQL : SqlGateway
        {
            public Statement[] getKey(Statement rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStatement_Get_Id";
                cmd.Parameters.Add("@StmtId", SqlDbType.Int, 0).Value = rec.stmtId;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Statement rec = (Statement)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStatement_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@StmtId"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.stmtId = (int)cmd.Parameters["@StmtId"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Statement rec = (Statement)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStatement_Del_Id";
                cmd.Parameters.Add("@StmtId", SqlDbType.Int, 0).Value = rec.stmtId;
            //    rec.ver++;
             //   cmd.Parameters.Add("@Ver", SqlDbType.Int, 4).Value = rec.ver;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Statement rec = (Statement)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spStatement_Upd";
                rec.ver++;
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Statement[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spStatement_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Statement rec)
            {
				cmd.Parameters.Add("@StmtId", SqlDbType.Int, 0).Value = rec.stmtId;
				cmd.Parameters.Add("@ServPeriod", SqlDbType.Int, 0).Value = rec.servPeriod;

           //    cmd.Parameters.Add("@Ver", SqlDbType.Int, 0).Value = rec.ver;
                cmd.Parameters.Add("@Billpayer", SqlDbType.Int, 0).Value = rec.billpayer;
 
                if (rec.stmtType == null)
                    cmd.Parameters.Add("@StmtType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.StmtType.Length == 0)
                        cmd.Parameters.Add("@StmtType", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@StmtType", SqlDbType.VarChar, 15).Value = rec.stmtType;
                }
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 50).Value = rec.status;
                }
 
                if (rec.perStartDate == DateTime.MinValue)
                    cmd.Parameters.Add("@PerStartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@PerStartDate", SqlDbType.DateTime, 0).Value = rec.perStartDate;
 
                if (rec.perEndDate == DateTime.MinValue)
                    cmd.Parameters.Add("@PerEndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@PerEndDate", SqlDbType.DateTime, 0).Value = rec.perEndDate;
 
                if (rec.lastPymtDate == DateTime.MinValue)
                    cmd.Parameters.Add("@LastPymtDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@LastPymtDate", SqlDbType.DateTime, 0).Value = rec.lastPymtDate;
                cmd.Parameters.Add("@LastPymtAmt", SqlDbType.Decimal, 0).Value = rec.lastPymtAmt;
 
                if (rec.pymtDueDate == DateTime.MinValue)
                    cmd.Parameters.Add("@PymtDueDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@PymtDueDate", SqlDbType.DateTime, 0).Value = rec.pymtDueDate;
                cmd.Parameters.Add("@PymtDueAmt", SqlDbType.Decimal, 0).Value = rec.pymtDueAmt;
                cmd.Parameters.Add("@TotBalance", SqlDbType.Decimal, 0).Value = rec.totBalance;
                cmd.Parameters.Add("@UnapplCR", SqlDbType.Decimal, 0).Value = rec.unapplCR;
                cmd.Parameters.Add("@UnbldBalance", SqlDbType.Decimal, 0).Value = rec.unbldBalance;
                cmd.Parameters.Add("@UnbldDR", SqlDbType.Decimal, 0).Value = rec.unbldDR;
                cmd.Parameters.Add("@UnbldUsageDR", SqlDbType.Decimal, 0).Value = rec.unbldUsageDR;
                cmd.Parameters.Add("@UnbldCR", SqlDbType.Decimal, 0).Value = rec.unbldCR;
                cmd.Parameters.Add("@UbldApplCR", SqlDbType.Decimal, 0).Value = rec.ubldApplCR;
                cmd.Parameters.Add("@BilledBalance", SqlDbType.Decimal, 0).Value = rec.billedBalance;
                cmd.Parameters.Add("@BilledDR", SqlDbType.Decimal, 0).Value = rec.billedDR;
                cmd.Parameters.Add("@BilledApplCR", SqlDbType.Decimal, 0).Value = rec.billedApplCR;
                cmd.Parameters.Add("@PD30Balance", SqlDbType.Decimal, 0).Value = rec.pD30Balance;
                cmd.Parameters.Add("@PD30DR", SqlDbType.Decimal, 0).Value = rec.pD30DR;
                cmd.Parameters.Add("@PD30ApplCR", SqlDbType.Decimal, 0).Value = rec.pD30ApplCR;
                cmd.Parameters.Add("@PDOver30Balance", SqlDbType.Decimal, 0).Value = rec.pDOver30Balance;
                cmd.Parameters.Add("@PD0ver30DR", SqlDbType.Decimal, 0).Value = rec.pD0ver30DR;
                cmd.Parameters.Add("@PD0ver30ApplCR", SqlDbType.Decimal, 0).Value = rec.pD0ver30ApplCR;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Statement rec = new Statement();
                
				if (rdr["StmtId"] != DBNull.Value)
					rec.stmtId = (int) rdr["StmtId"];   
				
				if (rdr["ServPeriod"] != DBNull.Value)
					 rec.servPeriod = (int) rdr["ServPeriod"];

//                if (rdr["Ver"] != DBNull.Value)
//                    rec.ver = (int) rdr["Ver"];
 
                if (rdr["Billpayer"] != DBNull.Value)
                    rec.billpayer = (int) rdr["Billpayer"];
 
                if (rdr["StmtType"] != DBNull.Value)
                    rec.stmtType = (string) rdr["StmtType"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["PerStartDate"] != DBNull.Value)
                    rec.perStartDate = (DateTime) rdr["PerStartDate"];
 
                if (rdr["PerEndDate"] != DBNull.Value)
                    rec.perEndDate = (DateTime) rdr["PerEndDate"];
 
                if (rdr["LastPymtDate"] != DBNull.Value)
                    rec.lastPymtDate = (DateTime) rdr["LastPymtDate"];
 
                if (rdr["LastPymtAmt"] != DBNull.Value)
                    rec.lastPymtAmt = Decimal.Round((decimal)rdr["LastPymtAmt"], 2);
 
                if (rdr["PymtDueDate"] != DBNull.Value)
                    rec.pymtDueDate = (DateTime) rdr["PymtDueDate"];
 
                if (rdr["PymtDueAmt"] != DBNull.Value)
                    rec.pymtDueAmt = Decimal.Round((decimal)rdr["PymtDueAmt"], 2);
 
                if (rdr["TotBalance"] != DBNull.Value)
                    rec.totBalance = Decimal.Round((decimal)rdr["TotBalance"], 2);
 
                if (rdr["UnapplCR"] != DBNull.Value)
                    rec.unapplCR = Decimal.Round((decimal)rdr["UnapplCR"], 2);
 
                if (rdr["UnbldBalance"] != DBNull.Value)
                    rec.unbldBalance = Decimal.Round((decimal)rdr["UnbldBalance"], 2);
 
                if (rdr["UnbldDR"] != DBNull.Value)
                    rec.unbldDR = Decimal.Round((decimal)rdr["UnbldDR"], 2);
 
                if (rdr["UnbldUsageDR"] != DBNull.Value)
                    rec.unbldUsageDR = Decimal.Round((decimal)rdr["UnbldUsageDR"], 2);
 
                if (rdr["UnbldCR"] != DBNull.Value)
                    rec.unbldCR = Decimal.Round((decimal)rdr["UnbldCR"], 2);
 
                if (rdr["UbldApplCR"] != DBNull.Value)
                    rec.ubldApplCR = Decimal.Round((decimal)rdr["UbldApplCR"], 2);
 
                if (rdr["BilledBalance"] != DBNull.Value)
                    rec.billedBalance = Decimal.Round((decimal)rdr["BilledBalance"], 2);
 
                if (rdr["BilledDR"] != DBNull.Value)
                    rec.billedDR = Decimal.Round((decimal)rdr["BilledDR"], 2);
 
                if (rdr["BilledApplCR"] != DBNull.Value)
                    rec.billedApplCR = Decimal.Round((decimal)rdr["BilledApplCR"], 2);
 
                if (rdr["PD30Balance"] != DBNull.Value)
                    rec.pD30Balance = Decimal.Round((decimal)rdr["PD30Balance"], 2);
 
                if (rdr["PD30DR"] != DBNull.Value)
                    rec.pD30DR = Decimal.Round((decimal)rdr["PD30DR"], 2);
 
                if (rdr["PD30ApplCR"] != DBNull.Value)
                    rec.pD30ApplCR = Decimal.Round((decimal)rdr["PD30ApplCR"], 2);
 
                if (rdr["PDOver30Balance"] != DBNull.Value)
                    rec.pDOver30Balance = Decimal.Round((decimal)rdr["PDOver30Balance"], 2);
 
                if (rdr["PD0ver30DR"] != DBNull.Value)
                    rec.pD0ver30DR = Decimal.Round((decimal)rdr["PD0ver30DR"], 2);
 
                if (rdr["PD0ver30ApplCR"] != DBNull.Value)
                    rec.pD0ver30ApplCR = Decimal.Round((decimal)rdr["PD0ver30ApplCR"], 2);
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Statement[] convert(DomainObj[] objs)
            {
                Statement[] acls  = new Statement[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
