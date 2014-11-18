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
    public class ILEC_LSR_Rule : DomainObj
    {
        /*        Data        */
        static string iName = "ILEC_LSR_Rule";
        int id;
        string lSR_Rule;
        int step;
        string orderType;
        int rev;
        string provCategory;
        string uSOC_Pattern;
        string uSOC_List;
        string uSOC_List_Action;
        string uSOC_Description;
        string status;
        DateTime startDate;
        DateTime endDate;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string LSR_Rule
        {
            get { return lSR_Rule; }
            set
            {
                setState();
                lSR_Rule = value;
            }
        }
        public int Step
        {
            get { return step; }
            set
            {
                setState();
                step = value;
            }
        }
        public string OrderType
        {
            get { return orderType; }
            set
            {
                setState();
                orderType = value;
            }
        }
        public int Rev
        {
            get { return rev; }
            set
            {
                setState();
                rev = value;
            }
        }
        public string ProvCategory
        {
            get { return provCategory; }
            set
            {
                setState();
                provCategory = value;
            }
        }
        public string USOC_Pattern
        {
            get { return uSOC_Pattern; }
            set
            {
                setState();
                uSOC_Pattern = value;
            }
        }
        public string USOC_List
        {
            get { return uSOC_List; }
            set
            {
                setState();
                uSOC_List = value;
            }
        }
        public string USOC_List_Action
        {
            get { return uSOC_List_Action; }
            set
            {
                setState();
                uSOC_List_Action = value;
            }
        }
        public string USOC_Description
        {
            get { return uSOC_Description; }
            set
            {
                setState();
                uSOC_Description = value;
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
        
        /*        Constructors			*/
        public ILEC_LSR_Rule()
        {
            sql = new ILEC_LSR_RuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public ILEC_LSR_Rule(UOW uow) : this()
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
            return new ILEC_LSR_RuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static ILEC_LSR_Rule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ILEC_LSR_Rule.getKey(id)))
                return (ILEC_LSR_Rule)uow.Imap.find(ILEC_LSR_Rule.getKey(id));
            
            ILEC_LSR_Rule cls = new ILEC_LSR_Rule();
            cls.uow = uow;
            cls.id = id;
            cls = (ILEC_LSR_Rule)DomainObj.addToIMap(uow, getOne(((ILEC_LSR_RuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ILEC_LSR_Rule[] getAll(UOW uow)
        {
            ILEC_LSR_Rule[] objs = (ILEC_LSR_Rule[])DomainObj.addToIMap(uow, (new ILEC_LSR_RuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static ILEC_LSR_Rule[] GetRules(UOW uow, string rule)
		{
			ILEC_LSR_Rule[] objs = (ILEC_LSR_Rule[])DomainObj.addToIMap(uow, (new ILEC_LSR_RuleSQL().GetRules(uow, rule)));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}

		public static ILEC_LSR_Rule[] GetRules(UOW uow, string st, string ilecCode, OrderType otype, string provCat)
		{
			ILEC_LSR_Rule[] objs = (ILEC_LSR_Rule[])DomainObj.addToIMap(uow, 
				(new ILEC_LSR_RuleSQL().GetRules(uow, st, ilecCode, otype, provCat)));

			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}

        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static ILEC_LSR_Rule getOne(ILEC_LSR_Rule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ILEC_LSR_Rule src, ILEC_LSR_Rule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.lSR_Rule = src.lSR_Rule;
            tar.step = src.step;
            tar.orderType = src.orderType;
            tar.rev = src.rev;
            tar.provCategory = src.provCategory;
            tar.uSOC_Pattern = src.uSOC_Pattern;
            tar.uSOC_List = src.uSOC_List;
            tar.uSOC_List_Action = src.uSOC_List_Action;
            tar.uSOC_Description = src.uSOC_Description;
            tar.status = src.status;
            tar.startDate = src.startDate;
            tar.endDate = src.endDate;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ILEC_LSR_RuleSQL : SqlGateway
        {
            public ILEC_LSR_Rule[] getKey(ILEC_LSR_Rule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spILEC_LSR_Rule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ILEC_LSR_Rule rec = (ILEC_LSR_Rule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spILEC_LSR_Rule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ILEC_LSR_Rule rec = (ILEC_LSR_Rule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spILEC_LSR_Rule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ILEC_LSR_Rule rec = (ILEC_LSR_Rule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spILEC_LSR_Rule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ILEC_LSR_Rule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spILEC_LSR_Rule_Get_All";
                return convert(execReader(cmd));
            }
			public ILEC_LSR_Rule[] GetRules(UOW uow, string rule)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "dbo.spILEC_LSR_Rule_Get_Rules";
                cmd.Parameters.Add("@Rule", SqlDbType.VarChar, 25).Value = rule;

				return convert(execReader(cmd));
			}

			public ILEC_LSR_Rule[] GetRules(UOW uow, string st, string ilecCode, OrderType otype, string provCat)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spILEC_LSR_Rule_Get_ProvCat_OrderType";

				cmd.Parameters.Add("@Rule", SqlDbType.Char, 5).Value = st + ilecCode;
				cmd.Parameters.Add("@ProvCat", SqlDbType.Char, 25).Value = provCat;
				cmd.Parameters.Add("@OrderType", SqlDbType.Char, 25).Value = otype.ToString();

				return convert(execReader(cmd));
			}

			/*        Implementation        */
            void setParam(SqlCommand cmd, ILEC_LSR_Rule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@LSR_Rule", SqlDbType.VarChar, 15).Value = rec.lSR_Rule;
                cmd.Parameters.Add("@Step", SqlDbType.Int, 0).Value = rec.step;
 
                cmd.Parameters.Add("@OrderType", SqlDbType.VarChar, 10).Value = rec.orderType;
                cmd.Parameters.Add("@Rev", SqlDbType.Int, 0).Value = rec.rev;
 
                if (rec.provCategory == null)
                    cmd.Parameters.Add("@ProvCategory", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.ProvCategory.Length == 0)
                        cmd.Parameters.Add("@ProvCategory", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ProvCategory", SqlDbType.VarChar, 15).Value = rec.provCategory;
                }
 
                if (rec.uSOC_Pattern == null)
                    cmd.Parameters.Add("@USOC_Pattern", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.USOC_Pattern.Length == 0)
                        cmd.Parameters.Add("@USOC_Pattern", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@USOC_Pattern", SqlDbType.VarChar, 15).Value = rec.uSOC_Pattern;
                }
 
                if (rec.uSOC_List == null)
                    cmd.Parameters.Add("@USOC_List", SqlDbType.VarChar, 1024).Value = DBNull.Value;
                else
                {
                    if (rec.USOC_List.Length == 0)
                        cmd.Parameters.Add("@USOC_List", SqlDbType.VarChar, 1024).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@USOC_List", SqlDbType.VarChar, 1024).Value = rec.uSOC_List;
                }
 
                if (rec.uSOC_List_Action == null)
                    cmd.Parameters.Add("@USOC_List_Action", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.USOC_List_Action.Length == 0)
                        cmd.Parameters.Add("@USOC_List_Action", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@USOC_List_Action", SqlDbType.VarChar, 10).Value = rec.uSOC_List_Action;
                }
 
                if (rec.uSOC_Description == null)
                    cmd.Parameters.Add("@USOC_Description", SqlDbType.VarChar, 1024).Value = DBNull.Value;
                else
                {
                    if (rec.USOC_Description.Length == 0)
                        cmd.Parameters.Add("@USOC_Description", SqlDbType.VarChar, 1024).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@USOC_Description", SqlDbType.VarChar, 1024).Value = rec.uSOC_Description;
                }
 
                if (rec.status == null)
                    cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                else
                {
                    if (rec.Status.Length == 0)
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 15).Value = rec.status;
                }
 
                if (rec.startDate == DateTime.MinValue)
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = rec.startDate;
 
                if (rec.endDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = rec.endDate;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ILEC_LSR_Rule rec = new ILEC_LSR_Rule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["LSR_Rule"] != DBNull.Value)
                    rec.lSR_Rule = (string) rdr["LSR_Rule"];
 
                if (rdr["Step"] != DBNull.Value)
                    rec.step = (int) rdr["Step"];
 
                if (rdr["OrderType"] != DBNull.Value)
                    rec.orderType = (string) rdr["OrderType"];
 
                if (rdr["Rev"] != DBNull.Value)
                    rec.rev = (int) rdr["Rev"];
 
                if (rdr["ProvCategory"] != DBNull.Value)
                    rec.provCategory = (string) rdr["ProvCategory"];
 
                if (rdr["USOC_Pattern"] != DBNull.Value)
                    rec.uSOC_Pattern = (string) rdr["USOC_Pattern"];
 
                if (rdr["USOC_List"] != DBNull.Value)
                    rec.uSOC_List = (string) rdr["USOC_List"];
 
                if (rdr["USOC_List_Action"] != DBNull.Value)
                    rec.uSOC_List_Action = (string) rdr["USOC_List_Action"];
 
                if (rdr["USOC_Description"] != DBNull.Value)
                    rec.uSOC_Description = (string) rdr["USOC_Description"];
 
                if (rdr["Status"] != DBNull.Value)
                    rec.status = (string) rdr["Status"];
 
                if (rdr["StartDate"] != DBNull.Value)
                    rec.startDate = (DateTime) rdr["StartDate"];
 
                if (rdr["EndDate"] != DBNull.Value)
                    rec.endDate = (DateTime) rdr["EndDate"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ILEC_LSR_Rule[] convert(DomainObj[] objs)
            {
                ILEC_LSR_Rule[] acls  = new ILEC_LSR_Rule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
