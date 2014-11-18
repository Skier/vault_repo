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
    public class DmdProdTypeRule : DomainObj, IDmdProdTypeRule
    {
        /*        Data        */
        static string iName = "DmdProdTypeRule";
        int id;
        string dmdType;
        string prodType;
        
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
        public string ProdType
        {
            get { return prodType; }
            set
            {
                setState();
                prodType = value;
            }
        }
        
        /*        Constructors			*/
        public DmdProdTypeRule()
        {
            sql = new DmdProdTypeRuleSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public DmdProdTypeRule(UOW uow) : this()
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
            return new DmdProdTypeRuleSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static DmdProdTypeRule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(DmdProdTypeRule.getKey(id)))
                return (DmdProdTypeRule)uow.Imap.find(DmdProdTypeRule.getKey(id));
            
            DmdProdTypeRule cls = new DmdProdTypeRule();
            cls.uow = uow;
            cls.id = id;
            cls = (DmdProdTypeRule)DomainObj.addToIMap(uow, getOne(((DmdProdTypeRuleSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static DmdProdTypeRule[] getAll(UOW uow)
        {
            DmdProdTypeRule[] objs = (DmdProdTypeRule[])DomainObj.addToIMap(uow, (new DmdProdTypeRuleSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static DmdProdTypeRule getOne(DmdProdTypeRule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(DmdProdTypeRule src, DmdProdTypeRule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.dmdType = src.dmdType;
            tar.prodType = src.prodType;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class DmdProdTypeRuleSQL : SqlGateway
        {
            public DmdProdTypeRule[] getKey(DmdProdTypeRule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdProdTypeRule_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                DmdProdTypeRule rec = (DmdProdTypeRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdProdTypeRule_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                DmdProdTypeRule rec = (DmdProdTypeRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdProdTypeRule_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                DmdProdTypeRule rec = (DmdProdTypeRule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spDmdProdTypeRule_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public DmdProdTypeRule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spDmdProdTypeRule_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, DmdProdTypeRule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@DmdType", SqlDbType.VarChar, 25).Value = rec.dmdType;
 
                cmd.Parameters.Add("@ProdType", SqlDbType.VarChar, 15).Value = rec.prodType;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                DmdProdTypeRule rec = new DmdProdTypeRule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["DmdType"] != DBNull.Value)
                    rec.dmdType = (string) rdr["DmdType"];
 
                if (rdr["ProdType"] != DBNull.Value)
                    rec.prodType = (string) rdr["ProdType"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            DmdProdTypeRule[] convert(DomainObj[] objs)
            {
                DmdProdTypeRule[] acls  = new DmdProdTypeRule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
