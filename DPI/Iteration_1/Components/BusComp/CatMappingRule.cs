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
    public class CatMappingRule : DomainObj
    {
        /*        Data        */
        static string iName = "CatMappingRule";
        int id;
        string fromDomain;
        string fromCategory;
        string fromValue;
        string toDomain;
        string toCategory;
        string toValue;
        DateTime startEffDate;
        DateTime endEffDate;
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
        public string FromDomain
        {
            get { return fromDomain; }
            set
            {
                setState();
                fromDomain = value;
            }
        }
        public string FromCategory
        {
            get { return fromCategory; }
            set
            {
                setState();
                fromCategory = value;
            }
        }
        public string FromValue
        {
            get { return fromValue; }
            set
            {
                setState();
                fromValue = value;
            }
        }
        public string ToDomain
        {
            get { return toDomain; }
            set
            {
                setState();
                toDomain = value;
            }
        }
        public string ToCategory
        {
            get { return toCategory; }
            set
            {
                setState();
                toCategory = value;
            }
        }
        public string ToValue
        {
            get { return toValue; }
            set
            {
                setState();
                toValue = value;
            }
        }
        public DateTime StartEffDate
        {
            get { return startEffDate; }
            set
            {
                setState();
                startEffDate = value;
            }
        }
        public DateTime EndEffDate
        {
            get { return endEffDate; }
            set
            {
                setState();
                endEffDate = value;
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
        public CatMappingRule()
        {
            sql = new CatMappingRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public CatMappingRule(UOW uow) : this()
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
            return new CatMappingRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static CatMappingRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(CatMappingRule.getKey(id)))
                return (CatMappingRule)uow.Imap.find(CatMappingRule.getKey(id));
            
            CatMappingRule cls = new CatMappingRule();
            cls.uow = uow;
            cls.id = id;
            cls = (CatMappingRule)DomainObj.addToIMap(uow, getOne(((CatMappingRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
		public static string TransTo(UOW uow, MapDomain toDomain, MapCategory category, int val)
		{
			return TransTo(uow, toDomain, category, category, val.ToString());
		}
		public static string TransTo(UOW uow, MapDomain toDomain, MapCategory category, string val)
		{
			return TransTo(uow, toDomain, category, category, val);
		}
		public static string TransTo(UOW uow, MapDomain toDomain, MapCategory fromCategory, MapCategory toCategory, int val)
		{
			return TransTo(uow, toDomain, fromCategory, toCategory, val.ToString());
		}
		public static string TransTo(UOW uow, MapDomain toDomain, MapCategory fromCategory, MapCategory toCategory, string val)
		{
			return TransTo(uow, toDomain, fromCategory, toCategory, val, DateTime.Now);
		}
		public static string TransTo(UOW uow, MapDomain toDomain, MapCategory fromCategory, MapCategory toCategory, string val, DateTime effDate)
		{
			DateTime eff = effDate;
			if (effDate == DateTime.MinValue)
				eff = DateTime.Now;

			if (val == null)
				throw new ArgumentNullException("FromValue");
			
			if (val.Trim().Length == 0)
				throw new ArgumentException("FromValue is required"); 
			
			return find(uow, MapDomain.DPI, toDomain, fromCategory, toCategory, val.Trim(), eff).toValue;
		}
		static CatMappingRule find(UOW uow, MapDomain fromDomain, MapDomain toDomain, 
			                       MapCategory fromCategory, MapCategory toCategory, string fromValue, DateTime effDate)
		{
			CatMappingRule cls = new CatMappingRule();
			cls.uow = uow;

			cls.fromDomain = fromDomain.ToString();
			cls.toDomain = toDomain.ToString();
			cls.fromCategory = fromCategory.ToString();
			cls.toCategory = toCategory.ToString();
			cls.fromValue = fromValue;

			cls = (CatMappingRule)DomainObj.addToIMap(uow, getOne(((CatMappingRuleSQL)cls.Sql).find(cls, effDate)));
			cls.uow = uow;
            
			return cls;
		}
        public static CatMappingRule[] getAll(UOW uow)
        {
            CatMappingRule[] objs = (CatMappingRule[])DomainObj.addToIMap(uow, (new CatMappingRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static CatMappingRule getOne(CatMappingRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(CatMappingRule src, CatMappingRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.fromDomain = src.fromDomain;
            tar.fromCategory = src.fromCategory;
            tar.fromValue = src.fromValue;
            tar.toDomain = src.toDomain;
            tar.toCategory = src.toCategory;
            tar.toValue = src.toValue;
            tar.startEffDate = src.startEffDate;
            tar.endEffDate = src.endEffDate;
            tar.status = src.status;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class CatMappingRuleSQL : SqlGateway
        {
            public CatMappingRule[] getKey(CatMappingRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCatMappingRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
			public CatMappingRule[] find(CatMappingRule rec, DateTime effDate)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spCatMappingRule_Xlate";
				
				cmd.Parameters.Add("@FromDomain",   SqlDbType.VarChar, 50).Value = rec.fromDomain;
				cmd.Parameters.Add("@ToDomain",     SqlDbType.VarChar, 50).Value = rec.toDomain;
				cmd.Parameters.Add("@FromCategory", SqlDbType.VarChar, 50).Value = rec.fromCategory;
				cmd.Parameters.Add("@ToCategory", SqlDbType.VarChar, 50).Value = rec.toCategory;
				cmd.Parameters.Add("@FromValue",    SqlDbType.VarChar, 50).Value = rec.fromValue;

				cmd.Parameters.Add("@Effdate", SqlDbType.DateTime, 0).Value = effDate;

				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                CatMappingRule rec = (CatMappingRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCatMappingRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                CatMappingRule rec = (CatMappingRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCatMappingRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                CatMappingRule rec = (CatMappingRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spCatMappingRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public CatMappingRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spCatMappingRule_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, CatMappingRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.fromDomain == null)
                    cmd.Parameters.Add("@FromDomain", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.FromDomain.Length == 0)
                        cmd.Parameters.Add("@FromDomain", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FromDomain", SqlDbType.VarChar, 50).Value = rec.fromDomain;
                }
 
                if (rec.fromCategory == null)
                    cmd.Parameters.Add("@FromCategory", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.FromCategory.Length == 0)
                        cmd.Parameters.Add("@FromCategory", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FromCategory", SqlDbType.VarChar, 50).Value = rec.fromCategory;
                }
 
                if (rec.fromValue == null)
                    cmd.Parameters.Add("@FromValue", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.FromValue.Length == 0)
                        cmd.Parameters.Add("@FromValue", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@FromValue", SqlDbType.VarChar, 50).Value = rec.fromValue;
                }
 
                if (rec.toDomain == null)
                    cmd.Parameters.Add("@ToDomain", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ToDomain.Length == 0)
                        cmd.Parameters.Add("@ToDomain", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ToDomain", SqlDbType.VarChar, 50).Value = rec.toDomain;
                }
 
                if (rec.toCategory == null)
                    cmd.Parameters.Add("@ToCategory", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ToCategory.Length == 0)
                        cmd.Parameters.Add("@ToCategory", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ToCategory", SqlDbType.VarChar, 50).Value = rec.toCategory;
                }
 
                if (rec.toValue == null)
                    cmd.Parameters.Add("@ToValue", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ToValue.Length == 0)
                        cmd.Parameters.Add("@ToValue", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ToValue", SqlDbType.VarChar, 50).Value = rec.toValue;
                }
 
                if (rec.startEffDate == DateTime.MinValue)
                    cmd.Parameters.Add("@StartEffDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartEffDate", SqlDbType.DateTime, 0).Value = rec.startEffDate;
 
                if (rec.endEffDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndEffDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndEffDate", SqlDbType.DateTime, 0).Value = rec.endEffDate;
 
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
                CatMappingRule rec = new CatMappingRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["FromDomain"] != DBNull.Value)
                    rec.fromDomain = (string) rdr["FromDomain"];
 
                if (rdr["FromCategory"] != DBNull.Value)
                    rec.fromCategory = (string) rdr["FromCategory"];
 
                if (rdr["FromValue"] != DBNull.Value)
                    rec.fromValue = (string) rdr["FromValue"];
 
                if (rdr["ToDomain"] != DBNull.Value)
                    rec.toDomain = (string) rdr["ToDomain"];
 
                if (rdr["ToCategory"] != DBNull.Value)
                    rec.toCategory = (string) rdr["ToCategory"];
 
                if (rdr["ToValue"] != DBNull.Value)
                    rec.toValue = (string) rdr["ToValue"];
 
                if (rdr["StartEffDate"] != DBNull.Value)
                    rec.startEffDate = (DateTime) rdr["StartEffDate"];
 
                if (rdr["EndEffDate"] != DBNull.Value)
                    rec.endEffDate = (DateTime) rdr["EndEffDate"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            CatMappingRule[] convert(DomainObj[] objs)
            {
                CatMappingRule[] acls  = new CatMappingRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
