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
    public class CommissionRule : DomainObj
    {
        /*        Data        */
        static string iName = "CommissionRule";
        int id;
        int product;
        string vendor;
        int agent;
        string minAmt;
        string maxAmt;
        decimal commissionAmt;
        int rate;
        DateTime fromEffDate;
        DateTime toEffDate;
        DateTime status;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public int Product
        {
            get { return product; }
            set
            {
                setState();
                product = value;
            }
        }
        public string Vendor
        {
            get { return vendor; }
            set
            {
                setState();
                vendor = value;
            }
        }
        public int Agent
        {
            get { return agent; }
            set
            {
                setState();
                agent = value;
            }
        }
        public string MinAmt
        {
            get { return minAmt; }
            set
            {
                setState();
                minAmt = value;
            }
        }
        public string MaxAmt
        {
            get { return maxAmt; }
            set
            {
                setState();
                maxAmt = value;
            }
        }
        public decimal CommissionAmt
        {
            get { return commissionAmt; }
            set
            {
                setState();
                commissionAmt = Decimal.Round(value, 2);
            }
        }
        public int Rate
        {
            get { return rate; }
            set
            {
                setState();
                rate = value;
            }
        }
        public DateTime FromEffDate
        {
            get { return fromEffDate; }
            set
            {
                setState();
                fromEffDate = value;
            }
        }
        public DateTime ToEffDate
        {
            get { return toEffDate; }
            set
            {
                setState();
                toEffDate = value;
            }
        }
        public DateTime Status
        {
            get { return status; }
            set
            {
                setState();
                status = value;
            }
        }
        
        /*        Constructors			*/
        public CommissionRule()
        {
            sql = new CommissionRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public CommissionRule(UOW uow) : this()
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
            return new CommissionRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static CommissionRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(CommissionRule.getKey(id)))
                return (CommissionRule)uow.Imap.find(CommissionRule.getKey(id));
            
            CommissionRule cls = new CommissionRule();
            cls.uow = uow;
            cls.id = id;
            cls = (CommissionRule)DomainObj.addToIMap(uow, getOne(((CommissionRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static CommissionRule[] getAll(UOW uow)
        {
            CommissionRule[] objs = (CommissionRule[])DomainObj.addToIMap(uow, (new CommissionRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static CommissionRule getOne(CommissionRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CommissionRule src, CommissionRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.product = src.product;
            tar.vendor = src.vendor;
            tar.agent = src.agent;
            tar.minAmt = src.minAmt;
            tar.maxAmt = src.maxAmt;
            tar.commissionAmt = src.commissionAmt;
            tar.rate = src.rate;
            tar.fromEffDate = src.fromEffDate;
            tar.toEffDate = src.toEffDate;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class CommissionRuleSQL : SqlGateway
        {
            public CommissionRule[] getKey(CommissionRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCommissionRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                CommissionRule rec = (CommissionRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCommissionRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CommissionRule rec = (CommissionRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCommissionRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CommissionRule rec = (CommissionRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCommissionRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CommissionRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCommissionRule_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, CommissionRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Product == 0)
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Product", SqlDbType.Int, 0).Value = rec.product;
 
                if (rec.vendor == null)
                    cmd.Parameters.Add("@Vendor", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Vendor.Length == 0)
                        cmd.Parameters.Add("@Vendor", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Vendor", SqlDbType.VarChar, 10).Value = rec.vendor;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.Agent == 0)
                    cmd.Parameters.Add("@Agent", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Agent", SqlDbType.Int, 0).Value = rec.agent;
 
                if (rec.minAmt == null)
                    cmd.Parameters.Add("@MinAmt", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.MinAmt.Length == 0)
                        cmd.Parameters.Add("@MinAmt", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@MinAmt", SqlDbType.VarChar, 10).Value = rec.minAmt;
                }
 
                if (rec.maxAmt == null)
                    cmd.Parameters.Add("@MaxAmt", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.MaxAmt.Length == 0)
                        cmd.Parameters.Add("@MaxAmt", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@MaxAmt", SqlDbType.VarChar, 10).Value = rec.maxAmt;
                }
                cmd.Parameters.Add("@CommissionAmt", SqlDbType.Decimal, 0).Value = rec.commissionAmt;
                cmd.Parameters.Add("@Rate", SqlDbType.Int, 0).Value = rec.rate;
 
                if (rec.fromEffDate == DateTime.MinValue)
                    cmd.Parameters.Add("@FromEffDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@FromEffDate", SqlDbType.DateTime, 0).Value = rec.fromEffDate;
 
                if (rec.toEffDate == DateTime.MinValue)
                    cmd.Parameters.Add("@ToEffDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ToEffDate", SqlDbType.DateTime, 0).Value = rec.toEffDate;
 
                if (rec.status == DateTime.MinValue)
                    cmd.Parameters.Add("@Status", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Status", SqlDbType.DateTime, 0).Value = rec.status;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                CommissionRule rec = new CommissionRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Product"] != DBNull.Value)
                    rec.product = (int) rdr["Product"];
 
                if (rdr["Vendor"] != DBNull.Value)
                    rec.vendor = (string) rdr["Vendor"];
 
                if (rdr["Agent"] != DBNull.Value)
                    rec.agent = (int) rdr["Agent"];
 
                if (rdr["MinAmt"] != DBNull.Value)
                    rec.minAmt = (string) rdr["MinAmt"];
 
                if (rdr["MaxAmt"] != DBNull.Value)
                    rec.maxAmt = (string) rdr["MaxAmt"];
 
                if (rdr["CommissionAmt"] != DBNull.Value)
                    rec.commissionAmt = Decimal.Round((decimal)rdr["CommissionAmt"], 2);
 
                if (rdr["Rate"] != DBNull.Value)
                    rec.rate = (int) rdr["Rate"];
 
                if (rdr["FromEffDate"] != DBNull.Value)
                    rec.fromEffDate = (DateTime) rdr["FromEffDate"];
 
                if (rdr["ToEffDate"] != DBNull.Value)
                    rec.toEffDate = (DateTime) rdr["ToEffDate"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (DateTime) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CommissionRule[] convert(DomainObj[] objs)
            {
                CommissionRule[] acls  = new CommissionRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
