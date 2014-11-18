using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using DPI.Interfaces;
 
namespace DPI.Components
{
    [Serializable]
    public class DmdReqProdRule : DomainObj
    {
        /*        Data        */
        static string iName = "DmdReqProdRule";
        int id;
        DemandType dmdType;
        string ruleName;
        int reqProd;
        DateTime effStartDate;
        DateTime effEndDate;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public DemandType DmdType
        {
            get { return dmdType; }
            set
            {
                setState();
                dmdType = value;
            }
        }
        public string RuleName
        {
            get { return ruleName; }
            set
            {
                setState();
                ruleName = value;
            }
        }
        public int ReqProd
        {
            get { return reqProd; }
            set
            {
                setState();
                reqProd = value;
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
        
        /*        Constructors			*/
        public DmdReqProdRule()
        {
            sql = new DmdReqProdRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public DmdReqProdRule(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
        
        /*        Methods        */
		public static DmdReqProdRule[][] GetRules(UOW uow, string dmdType)
		{
			return  GetRules(uow, (DemandType)Enum.Parse(typeof (DemandType), dmdType));
		}

		public static DmdReqProdRule[][] GetRules(UOW uow, DemandType dmdType)
		{
			return ConvertRules(new DmdReqProdRuleSQL().getForDmdType(uow, dmdType));
		}
		static ArrayList Split(DmdReqProdRule[] rules)
		{
			string ruleName = null;
			ArrayList all = new ArrayList(); 
			ArrayList ar = new ArrayList();

			for (int i = 0; i < rules.Length; i++)
			{
				if (rules[i].RuleName != ruleName)
				{
					if (ar.Count > 0)
					{
						all.Add(ar);
						ar = new ArrayList();
					}
					ruleName = rules[i].RuleName;
				}		
				ar.Add(rules[i]);	
			}

			if (ar.Count > 0)
				all.Add(ar);

			return all;
		}

		static DmdReqProdRule[][] ConvertRules(DmdReqProdRule[] rules)
		{
			if (rules == null)
				return null;

			if (rules.Length == 0)
				return null;

			ArrayList all =  Split(rules);
			DmdReqProdRule[][] groups = new DmdReqProdRule[all.Count][];

			ArrayList grp;

			for (int i = 0; i < all.Count; i++)
			{
				grp = (ArrayList)all[i];
				DmdReqProdRule[] g = new DmdReqProdRule[grp.Count];
				grp.CopyTo(g);
				groups[i] = g;
			}

			return groups;
 		}

        protected override SqlGateway loadSql()
        {
            return new DmdReqProdRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static DmdReqProdRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(DmdReqProdRule.getKey(id)))
                return (DmdReqProdRule)uow.Imap.find(DmdReqProdRule.getKey(id));
            
            DmdReqProdRule cls = new DmdReqProdRule();
            cls.uow = uow;
            cls.id = id;
            cls = (DmdReqProdRule)DomainObj.addToIMap(uow, getOne(((DmdReqProdRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static DmdReqProdRule[] getAll(UOW uow)
        {
            DmdReqProdRule[] objs = (DmdReqProdRule[])DomainObj.addToIMap(uow, (new DmdReqProdRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static DmdReqProdRule getOne(DmdReqProdRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(DmdReqProdRule src, DmdReqProdRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.dmdType = src.dmdType;
            tar.ruleName = src.ruleName;
            tar.reqProd = src.reqProd;
            tar.effStartDate = src.effStartDate;
            tar.effEndDate = src.effEndDate;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class DmdReqProdRuleSQL : SqlGateway
        {
            public DmdReqProdRule[] getKey(DmdReqProdRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdReqProdRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                DmdReqProdRule rec = (DmdReqProdRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdReqProdRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                DmdReqProdRule rec = (DmdReqProdRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdReqProdRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                DmdReqProdRule rec = (DmdReqProdRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdReqProdRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public DmdReqProdRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spDmdReqProdRule_Get_All";
                return convert(execReader(cmd));
            }
			public DmdReqProdRule[] getForDmdType(UOW uow, DemandType dmdType)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmdReqProdRuleGetForDmdType";
			    cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = dmdType.ToString();
				
				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, DmdReqProdRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = rec.dmdType.ToString();
 
                cmd.Parameters.Add("@RuleName", SqlDbType.VarChar, 25).Value = rec.ruleName;
                cmd.Parameters.Add("@ReqProd", SqlDbType.Int, 0).Value = rec.reqProd;
 
                cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = rec.effStartDate;
 
                if (rec.effEndDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = rec.effEndDate;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                DmdReqProdRule rec = new DmdReqProdRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["DmdType"] != DBNull.Value)
                    rec.dmdType = (DemandType)Enum.Parse(typeof(DemandType), (string) rdr["DmdType"]);
 
				if (rdr["RuleName"] != DBNull.Value)
                    rec.ruleName = (string) rdr["RuleName"];
 
                if (rdr["ReqProd"] != DBNull.Value)
                    rec.reqProd = (int) rdr["ReqProd"];
 
                if (rdr["EffStartDate"] != DBNull.Value)
                    rec.effStartDate = (DateTime) rdr["EffStartDate"];
 
                if (rdr["EffEndDate"] != DBNull.Value)
                    rec.effEndDate = (DateTime) rdr["EffEndDate"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            DmdReqProdRule[] convert(DomainObj[] objs)
            {
                DmdReqProdRule[] acls  = new DmdReqProdRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}