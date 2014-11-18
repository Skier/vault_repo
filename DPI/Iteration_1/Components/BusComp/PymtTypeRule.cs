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
    public class PymtTypeRule : DomainObj, IPymtTypeRule
    {
        /*        Data        */
        static string iName = "PymtTypeRule";
        int id;
        string pymtRule;
        string pymtType;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string PymtRule
        {
            get { return pymtRule; }
            set
            {
                setState();
                pymtRule = value;
            }
        }
        public string PymtType
        {
            get { return pymtType; }
            set
            {
                setState();
                pymtType = value;
            }
        }
        
        /*        Constructors			*/
        public PymtTypeRule()
        {
            sql = new PymtTypeRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public PymtTypeRule(UOW uow) : this()
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
            return new PymtTypeRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static PymtTypeRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(PymtTypeRule.getKey(id)))
                return (PymtTypeRule)uow.Imap.find(PymtTypeRule.getKey(id));
            
            PymtTypeRule cls = new PymtTypeRule();
            cls.uow = uow;
            cls.id = id;
            cls = (PymtTypeRule)DomainObj.addToIMap(uow, getOne(((PymtTypeRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static PymtTypeRule[] getAll(UOW uow)
        {
            PymtTypeRule[] objs = (PymtTypeRule[])DomainObj.addToIMap(uow, (new PymtTypeRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static PaymentType[] GetPymtTypes(UOW uow, string pymtRule)
		{
			PymtTypeRule[] rules = GetPymtRules(uow, pymtRule);
			PaymentType[] ptypes = new PaymentType[rules.Length];

			for (int i = 0; i < ptypes.Length; i++)
				ptypes[i] = (PaymentType)Enum.Parse(typeof(PaymentType), rules[i].pymtType, false);

			return ptypes;
		}
		public static PymtTypeRule[]  GetPymtRules(UOW uow, string pymtRule)
		{
			PymtTypeRule[] objs = (PymtTypeRule[])DomainObj.addToIMap(uow, (new PymtTypeRuleSQL()).getRule(uow, pymtRule));

			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;

			return objs;
		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static PymtTypeRule getOne(PymtTypeRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(PymtTypeRule src, PymtTypeRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.pymtRule = src.pymtRule;
            tar.pymtType = src.pymtType;
            tar.rowState = src.rowState;
        }
		static void VerifyPymtTypes(string[] pymtTypes)
		{
			for (int i = 0; i < pymtTypes.Length; i++)
				if (!Enum.IsDefined(typeof(PaymentType), pymtTypes[i]))
					throw new ApplicationException("Payment Type not found, Payment Type: " + pymtTypes[i]);
		}

		/*		SQL		*/
		[Serializable]
			class PymtTypeRuleSQL : SqlGateway
        {
            public PymtTypeRule[] getKey(PymtTypeRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPymtTypeRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                PymtTypeRule rec = (PymtTypeRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPymtTypeRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                PymtTypeRule rec = (PymtTypeRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPymtTypeRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                PymtTypeRule rec = (PymtTypeRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spPymtTypeRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public PymtTypeRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spPymtTypeRule_Get_All";
                return convert(execReader(cmd));
            }
			public PymtTypeRule[] getRule(UOW uow, string rule)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spPymtTypeRule_Get_Rule";
			    cmd.Parameters.Add("@PymtRule", SqlDbType.VarChar, 25).Value = rule;
			
				return convert(execReader(cmd));
			}
            /*        Implementation        */
            void setParam(SqlCommand cmd, PymtTypeRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.pymtRule == null)
                    cmd.Parameters.Add("@PymtRule", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.PymtRule.Length == 0)
                        cmd.Parameters.Add("@PymtRule", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PymtRule", SqlDbType.VarChar, 25).Value = rec.pymtRule;
                }
 
                if (rec.pymtType == null)
                    cmd.Parameters.Add("@PymtType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.PymtType.Length == 0)
                        cmd.Parameters.Add("@PymtType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@PymtType", SqlDbType.VarChar, 50).Value = rec.pymtType;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                PymtTypeRule rec = new PymtTypeRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["PymtRule"] != DBNull.Value)
                    rec.pymtRule = (string) rdr["PymtRule"];
 
                if (rdr["PymtType"] != DBNull.Value)
                    rec.pymtType = (string) rdr["PymtType"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            PymtTypeRule[] convert(DomainObj[] objs)
            {
                PymtTypeRule[] acls  = new PymtTypeRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
