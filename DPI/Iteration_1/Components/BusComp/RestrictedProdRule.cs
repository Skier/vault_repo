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
    public class RestrictedProdRule : DomainObj
    {
        /*        Data        */
        static string iName = "RestrictedProdRule";
        int id;
        string name;
        int enabledProd;
        string criteria;
        DateTime startDate;
        DateTime endDate;
        string status;
        string description;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
            set
            {
                setState();
                name = value;
            }
        }
        public int EnabledProd
        {
            get { return enabledProd; }
            set
            {
                setState();
                enabledProd = value;
            }
        }
        public string Criteria
        {
            get { return criteria; }
            set
            {
                setState();
                criteria = value;
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
        public string Status
        {
            get { return status; }
            set
            {
                setState();
                status = value;
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                setState();
                description = value;
            }
        }
        
        /*        Constructors			*/
        public RestrictedProdRule()
        {
            sql = new RestrictedProdRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public RestrictedProdRule(UOW uow) : this()
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
            return new RestrictedProdRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static RestrictedProdRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(RestrictedProdRule.getKey(id)))
                return (RestrictedProdRule)uow.Imap.find(RestrictedProdRule.getKey(id));
            
            RestrictedProdRule cls = new RestrictedProdRule();
            cls.uow = uow;
            cls.id = id;
            cls = (RestrictedProdRule)DomainObj.addToIMap(uow, getOne(((RestrictedProdRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static RestrictedProdRule[] getAll(UOW uow)
        {
            RestrictedProdRule[] objs = (RestrictedProdRule[])DomainObj.addToIMap(uow, (new RestrictedProdRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static RestrictedProdRule[] getAll(UOW uow, string rule, string criteria, DateTime dt)
		{
			DateTime dtime = dt == DateTime.MinValue ? DateTime.Today : dt;
			
			RestrictedProdRule[] objs 
				= (RestrictedProdRule[])DomainObj.addToIMap(uow, (new RestrictedProdRuleSQL()).getAll(uow, rule, criteria, dt));

			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;

		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static RestrictedProdRule getOne(RestrictedProdRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(RestrictedProdRule src, RestrictedProdRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.name = src.name;
            tar.enabledProd = src.enabledProd;
            tar.criteria = src.criteria;
            tar.startDate = src.startDate;
            tar.endDate = src.endDate;
            tar.status = src.status;
            tar.description = src.description;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class RestrictedProdRuleSQL : SqlGateway
        {
            public RestrictedProdRule[] getKey(RestrictedProdRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spRestrictedProdRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                RestrictedProdRule rec = (RestrictedProdRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spRestrictedProdRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                RestrictedProdRule rec = (RestrictedProdRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spRestrictedProdRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                RestrictedProdRule rec = (RestrictedProdRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spRestrictedProdRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public RestrictedProdRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spRestrictedProdRule_Get_All";
                return convert(execReader(cmd));
            }
			public RestrictedProdRule[] getAll(UOW uow, string rule, string criteria, DateTime dt)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spRestrictedProdRule_Criteria";

				cmd.Parameters.Add("@Name", SqlDbType.VarChar, 25).Value = rule;
				
				if ((criteria == null) || (criteria.Trim().Length == 0))
					cmd.Parameters.Add("@Criteria", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Criteria", SqlDbType.VarChar, 25).Value = criteria;
	
				cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dt;

				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, RestrictedProdRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.name == null)
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.Name.Length == 0)
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = rec.name;
                }
                
                // Numeric, nullable foreign key treatment:
                if (rec.EnabledProd == 0)
                    cmd.Parameters.Add("@EnabledProd", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EnabledProd", SqlDbType.Int, 0).Value = rec.enabledProd;
 
                if (rec.criteria == null)
                    cmd.Parameters.Add("@Criteria", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.Criteria.Length == 0)
                        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Criteria", SqlDbType.VarChar, 50).Value = rec.criteria;
                }
 
                if (rec.startDate == DateTime.MinValue)
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 25).Value = rec.status;
                }
 
                if (rec.description == null)
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = DBNull.Value;
                else
                {
                    if (rec.Description.Length == 0)
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 250).Value = rec.description;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                RestrictedProdRule rec = new RestrictedProdRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Name"] != DBNull.Value)
                    rec.name = (string) rdr["Name"];
 
                if (rdr["EnabledProd"] != DBNull.Value)
                    rec.enabledProd = (int) rdr["EnabledProd"];
 
                if (rdr["Criteria"] != DBNull.Value)
                    rec.criteria = (string) rdr["Criteria"];
 
                if (rdr["StartDate"] != DBNull.Value)
                    rec.startDate = (DateTime) rdr["StartDate"];
 
                if (rdr["EndDate"] != DBNull.Value)
                    rec.endDate = (DateTime) rdr["EndDate"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["Description"] != DBNull.Value)
                    rec.description = (string) rdr["Description"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            RestrictedProdRule[] convert(DomainObj[] objs)
            {
                RestrictedProdRule[] acls  = new RestrictedProdRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
