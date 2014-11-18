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
    public class AgentIncentive : DomainObj, IAgentIncentive
    {
	#region Data
        static string iName = "AgentIncentive";
        int id;
        string incentType;
        string incentName;
        string incentDescr;
        DateTime effStartDate;
        DateTime effEndDate;
        string incentCond;
        string eligibility;
        string status;
	#endregion
        
	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string IncentType
        {
            get { return incentType; }
            set
            {
                setState();
                incentType = value;
            }
        }
        public string IncentName
        {
            get { return incentName; }
            set
            {
                setState();
                incentName = value;
            }
        }
        public string IncentDescr
        {
            get { return incentDescr; }
            set
            {
                setState();
                incentDescr = value;
            }
        }
        public DateTime EffStartDate
        {
            get { return effStartDate; }
            set
            {
                setState();
                effStartDate = value;
            }
        }
        public DateTime EffEndDate
        {
            get { return effEndDate; }
            set
            {
                setState();
                effEndDate = value;
            }
        }
        public string IncentCond
        {
            get { return incentCond; }
            set
            {
                setState();
                incentCond = value;
            }
        }
        public string Eligibility
        {
            get { return eligibility; }
            set
            {
                setState();
                eligibility = value;
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
		#endregion
        
	#region Constructors
        public AgentIncentive()
        {
            sql = new AgentIncentiveSQL();
            id = random.Next(Int32.MinValue, -1);
			priority = 21000;
            rowState = RowState.New;
        }
        public AgentIncentive(UOW uow) : this()
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
            return new AgentIncentiveSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
	#endregion
        
	#region Static methods
        public static AgentIncentive find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(AgentIncentive.getKey(id)))
                return (AgentIncentive)uow.Imap.find(AgentIncentive.getKey(id));
            
            AgentIncentive cls = new AgentIncentive();
            cls.uow = uow;
            cls.id = id;
            cls = (AgentIncentive)DomainObj.addToIMap(uow, getOne(((AgentIncentiveSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static AgentIncentive[] getAll(UOW uow)
        {
            AgentIncentive[] objs = (AgentIncentive[])DomainObj.addToIMap(uow, (new AgentIncentiveSQL()).getAll(uow));
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
        static AgentIncentive getOne(AgentIncentive[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(AgentIncentive src, AgentIncentive tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.incentType = src.incentType;
            tar.incentName = src.incentName;
            tar.incentDescr = src.incentDescr;
            tar.effStartDate = src.effStartDate;
            tar.effEndDate = src.effEndDate;
            tar.incentCond = src.incentCond;
            tar.eligibility = src.eligibility;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
	#endregion
        
	#region SQL
        [Serializable]
        class AgentIncentiveSQL : SqlGateway
        {
            public AgentIncentive[] getKey(AgentIncentive rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncentive_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                AgentIncentive rec = (AgentIncentive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncentive_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                AgentIncentive rec = (AgentIncentive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncentive_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                AgentIncentive rec = (AgentIncentive)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spAgentIncentive_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public AgentIncentive[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spAgentIncentive_Get_All";
                return convert(execReader(cmd));
            }
		#endregion
        
		#region Implementation
            void setParam(SqlCommand cmd, AgentIncentive rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.incentType == null)
                    cmd.Parameters.Add("@IncentType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.IncentType.Length == 0)
                        cmd.Parameters.Add("@IncentType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IncentType", SqlDbType.VarChar, 50).Value = rec.incentType;
                }
 
                if (rec.incentName == null)
                    cmd.Parameters.Add("@IncentName", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.IncentName.Length == 0)
                        cmd.Parameters.Add("@IncentName", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IncentName", SqlDbType.VarChar, 50).Value = rec.incentName;
                }
 
                if (rec.incentDescr == null)
                    cmd.Parameters.Add("@IncentDescr", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                else
                {
                    if (rec.IncentDescr.Length == 0)
                        cmd.Parameters.Add("@IncentDescr", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IncentDescr", SqlDbType.VarChar, 1000).Value = rec.incentDescr;
                }
 
                if (rec.effStartDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = rec.effStartDate;
 
                if (rec.effEndDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = rec.effEndDate;
 
                if (rec.incentCond == null)
                    cmd.Parameters.Add("@IncentCond", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                else
                {
                    if (rec.IncentCond.Length == 0)
                        cmd.Parameters.Add("@IncentCond", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@IncentCond", SqlDbType.VarChar, 2000).Value = rec.incentCond;
                }
 
                if (rec.eligibility == null)
                    cmd.Parameters.Add("@Eligibility", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Eligibility.Length == 0)
                        cmd.Parameters.Add("@Eligibility", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Eligibility", SqlDbType.VarChar, 50).Value = rec.eligibility;
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
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                AgentIncentive rec = new AgentIncentive();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["IncentType"] != DBNull.Value)
                    rec.incentType = (string) rdr["IncentType"];
 
                if (rdr["IncentName"] != DBNull.Value)
                    rec.incentName = (string) rdr["IncentName"];
 
                if (rdr["IncentDescr"] != DBNull.Value)
                    rec.incentDescr = (string) rdr["IncentDescr"];
 
                if (rdr["EffStartDate"] != DBNull.Value)
                    rec.effStartDate = (DateTime) rdr["EffStartDate"];
 
                if (rdr["EffEndDate"] != DBNull.Value)
                    rec.effEndDate = (DateTime) rdr["EffEndDate"];
 
                if (rdr["IncentCond"] != DBNull.Value)
                    rec.incentCond = (string) rdr["IncentCond"];
 
                if (rdr["Eligibility"] != DBNull.Value)
                    rec.eligibility = (string) rdr["Eligibility"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            AgentIncentive[] convert(DomainObj[] objs)
            {
                AgentIncentive[] acls  = new AgentIncentive[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
		#endregion

        }
    }
}
