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
    public class AgentInvoice : DomainObj
    {
        /*        Data        */
        static string iName = "AgentInvoice";
        int id;
        string invoiceType;
        string vendor;
        int agent;
        DateTime startDate;
        DateTime endDate;
        string terms;
        decimal amount;
        string status;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string InvoiceType
        {
            get { return invoiceType; }
            set
            {
                setState();
                invoiceType = value;
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
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                setState();
                startDate = value;
            }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                setState();
                endDate = value;
            }
        }
        public string Terms
        {
            get { return terms; }
            set
            {
                setState();
                terms = value;
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
        public string Status
        {
            get { return status; }
            set
            {
                setState();
                status = value;
            }
        }
        
        /*        Constructors			*/
        public AgentInvoice()
        {
            sql = new AgentInvoiceSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public AgentInvoice(UOW uow) : this()
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
            return new AgentInvoiceSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static AgentInvoice find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(AgentInvoice.getKey(id)))
                return (AgentInvoice)uow.Imap.find(AgentInvoice.getKey(id));
            
            AgentInvoice cls = new AgentInvoice();
            cls.uow = uow;
            cls.id = id;
            cls = (AgentInvoice)DomainObj.addToIMap(uow, getOne(((AgentInvoiceSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static AgentInvoice[] getAll(UOW uow)
        {
            AgentInvoice[] objs = (AgentInvoice[])DomainObj.addToIMap(uow, (new AgentInvoiceSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static AgentInvoice getOne(AgentInvoice[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(AgentInvoice src, AgentInvoice tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.invoiceType = src.invoiceType;
            tar.vendor = src.vendor;
            tar.agent = src.agent;
            tar.startDate = src.startDate;
            tar.endDate = src.endDate;
            tar.terms = src.terms;
            tar.amount = src.amount;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class AgentInvoiceSQL : SqlGateway
        {
            public AgentInvoice[] getKey(AgentInvoice rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentInvoice_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                AgentInvoice rec = (AgentInvoice)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentInvoice_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                AgentInvoice rec = (AgentInvoice)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentInvoice_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                AgentInvoice rec = (AgentInvoice)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentInvoice_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public AgentInvoice[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAgentInvoice_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, AgentInvoice rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.invoiceType == null)
                    cmd.Parameters.Add("@InvoiceType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.InvoiceType.Length == 0)
                        cmd.Parameters.Add("@InvoiceType", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@InvoiceType", SqlDbType.VarChar, 25).Value = rec.invoiceType;
                }
 
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
 
                if (rec.startDate == DateTime.MinValue)
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
 
                if (rec.terms == null)
                    cmd.Parameters.Add("@Terms", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Terms.Length == 0)
                        cmd.Parameters.Add("@Terms", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Terms", SqlDbType.VarChar, 10).Value = rec.terms;
                }
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal, 0).Value = rec.amount;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                AgentInvoice rec = new AgentInvoice();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["InvoiceType"] != DBNull.Value)
                    rec.invoiceType = (string) rdr["InvoiceType"];
 
                if (rdr["Vendor"] != DBNull.Value)
                    rec.vendor = (string) rdr["Vendor"];
 
                if (rdr["Agent"] != DBNull.Value)
                    rec.agent = (int) rdr["Agent"];
 
                if (rdr["StartDate"] != DBNull.Value)
                    rec.startDate = (DateTime) rdr["StartDate"];
 
                if (rdr["EndDate"] != DBNull.Value)
                    rec.endDate = (DateTime) rdr["EndDate"];
 
                if (rdr["Terms"] != DBNull.Value)
                    rec.terms = (string) rdr["Terms"];
 
                if (rdr["Amount"] != DBNull.Value)
                    rec.amount = Decimal.Round((decimal)rdr["Amount"], 2);
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            AgentInvoice[] convert(DomainObj[] objs)
            {
                AgentInvoice[] acls  = new AgentInvoice[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
