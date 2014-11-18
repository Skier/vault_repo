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
    public class USOC_Rule : DomainObj
    {
        /*        Data        */
        static string iName = "USOC_Pattern";
        int id;
        string uSOC_Pattern;
        int step;
        int call_Feature;
        string cF_State;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
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
        public int Step
        {
            get { return step; }
            set
            {
                setState();
                step = value;
            }
        }
        public int Call_Feature
        {
            get { return call_Feature; }
            set
            {
                setState();
                call_Feature = value;
            }
        }
        public string CF_State
        {
            get { return cF_State; }
            set
            {
                setState();
                cF_State = value;
            }
        }
        
        /*        Constructors			*/
        public USOC_Rule()
        {
            sql = new USOC_PatternSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public USOC_Rule(UOW uow) : this()
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
            return new USOC_PatternSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static USOC_Rule find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(USOC_Rule.getKey(id)))
                return (USOC_Rule)uow.Imap.find(USOC_Rule.getKey(id));
            
            USOC_Rule cls = new USOC_Rule();
            cls.uow = uow;
            cls.id = id;
            cls = (USOC_Rule)DomainObj.addToIMap(uow, getOne(((USOC_PatternSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static USOC_Rule[] getAll(UOW uow)
        {
            USOC_Rule[] objs = (USOC_Rule[])DomainObj.addToIMap(uow, (new USOC_PatternSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
        /*		Implementation		*/
        static USOC_Rule getOne(USOC_Rule[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(USOC_Rule src, USOC_Rule tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.uSOC_Pattern = src.uSOC_Pattern;
            tar.step = src.step;
            tar.call_Feature = src.call_Feature;
            tar.cF_State = src.cF_State;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class USOC_PatternSQL : SqlGateway
        {
            public USOC_Rule[] getKey(USOC_Rule rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUSOC_Pattern_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                USOC_Rule rec = (USOC_Rule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUSOC_Pattern_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                USOC_Rule rec = (USOC_Rule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUSOC_Pattern_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                USOC_Rule rec = (USOC_Rule)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spUSOC_Pattern_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public USOC_Rule[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spUSOC_Pattern_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, USOC_Rule rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                cmd.Parameters.Add("@USOC_Pattern", SqlDbType.VarChar, 15).Value = rec.uSOC_Pattern;
                cmd.Parameters.Add("@Step", SqlDbType.Int, 0).Value = rec.step;
                
                // Numeric, nullable foreign key treatment:
                if (rec.Call_Feature == 0)
                    cmd.Parameters.Add("@Call_Feature", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Call_Feature", SqlDbType.Int, 0).Value = rec.call_Feature;
 
                cmd.Parameters.Add("@CF_State", SqlDbType.VarChar, 15).Value = rec.cF_State;
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                USOC_Rule rec = new USOC_Rule();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["USOC_Pattern"] != DBNull.Value)
                    rec.uSOC_Pattern = (string) rdr["USOC_Pattern"];
 
                if (rdr["Step"] != DBNull.Value)
                    rec.step = (int) rdr["Step"];
 
                if (rdr["Call_Feature"] != DBNull.Value)
                    rec.call_Feature = (int) rdr["Call_Feature"];
 
                if (rdr["CF_State"] != DBNull.Value)
                    rec.cF_State = (string) rdr["CF_State"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            USOC_Rule[] convert(DomainObj[] objs)
            {
                USOC_Rule[] acls  = new USOC_Rule[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
