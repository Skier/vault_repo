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
    public class DmdTypeVsProdSubClassRule : DomainObj
    {
        /*        Data        */
        static string iName = "DmdTypeVsProdSubClassRule";
        int id;
        string dmdType;
        string prodSubClass;
        bool isRequired;
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
        public string DmdType
        {
            get { return dmdType; }
            set
            {
                setState();
                dmdType = value;
            }
        }
        public string ProdSubClass
        {
            get { return prodSubClass; }
            set
            {
                setState();
                prodSubClass = value;
            }
        }
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                setState();
                isRequired = value;
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
        public DmdTypeVsProdSubClassRule()
        {
            sql = new DmdTypeVsProdSubClassRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public DmdTypeVsProdSubClassRule(UOW uow) : this()
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
            return new DmdTypeVsProdSubClassRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static DmdTypeVsProdSubClassRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(DmdTypeVsProdSubClassRule.getKey(id)))
                return (DmdTypeVsProdSubClassRule)uow.Imap.find(DmdTypeVsProdSubClassRule.getKey(id));
            
            DmdTypeVsProdSubClassRule cls = new DmdTypeVsProdSubClassRule();
            cls.uow = uow;
            cls.id = id;
            cls = (DmdTypeVsProdSubClassRule)DomainObj.addToIMap(uow, getOne(((DmdTypeVsProdSubClassRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static DmdTypeVsProdSubClassRule[] getAll(UOW uow)
        {
            DmdTypeVsProdSubClassRule[] objs = (DmdTypeVsProdSubClassRule[])DomainObj.addToIMap(uow, (new DmdTypeVsProdSubClassRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static DmdTypeVsProdSubClassRule getOne(DmdTypeVsProdSubClassRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(DmdTypeVsProdSubClassRule src, DmdTypeVsProdSubClassRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.dmdType = src.dmdType;
            tar.prodSubClass = src.prodSubClass;
            tar.isRequired = src.isRequired;
            tar.effStartDate = src.effStartDate;
            tar.effEndDate = src.effEndDate;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class DmdTypeVsProdSubClassRuleSQL : SqlGateway
        {
            public DmdTypeVsProdSubClassRule[] getKey(DmdTypeVsProdSubClassRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTypeVsProdSubClassRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                DmdTypeVsProdSubClassRule rec = (DmdTypeVsProdSubClassRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTypeVsProdSubClassRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                DmdTypeVsProdSubClassRule rec = (DmdTypeVsProdSubClassRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTypeVsProdSubClassRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                DmdTypeVsProdSubClassRule rec = (DmdTypeVsProdSubClassRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdTypeVsProdSubClassRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public DmdTypeVsProdSubClassRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spDmdTypeVsProdSubClassRule_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, DmdTypeVsProdSubClassRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = rec.dmdType;
 
                cmd.Parameters.Add("@ProdSubClass", SqlDbType.VarChar, 15).Value = rec.prodSubClass;
 
                cmd.Parameters.Add("@IsRequired", SqlDbType.Bit, 0).Value = rec.isRequired;
 
                if (rec.effStartDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffStartDate", SqlDbType.DateTime, 0).Value = rec.effStartDate;
 
                if (rec.effEndDate == DateTime.MinValue)
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@EffEndDate", SqlDbType.DateTime, 0).Value = rec.effEndDate;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                DmdTypeVsProdSubClassRule rec = new DmdTypeVsProdSubClassRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["DmdType"] != DBNull.Value)
                    rec.dmdType = (string) rdr["DmdType"];
 
                if (rdr["ProdSubClass"] != DBNull.Value)
                    rec.prodSubClass = (string) rdr["ProdSubClass"];
 
                if (rdr["IsRequired"] != DBNull.Value)
                    rec.isRequired = (bool) rdr["IsRequired"];
 
                if (rdr["EffStartDate"] != DBNull.Value)
                    rec.effStartDate = (DateTime) rdr["EffStartDate"];
 
                if (rdr["EffEndDate"] != DBNull.Value)
                    rec.effEndDate = (DateTime) rdr["EffEndDate"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            DmdTypeVsProdSubClassRule[] convert(DomainObj[] objs)
            {
                DmdTypeVsProdSubClassRule[] acls  = new DmdTypeVsProdSubClassRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
