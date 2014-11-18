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
    public class Notes : DomainObj, IAcctNotes
    {
        /*        Data        */
        static string iName = "Notes";
        int id;
        string user;
        int dmdId;
        DateTime date;
        string text;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string User
        {
            get { return user; }
            set
            {
                setState();
                user = value;
            }
        }
        public int DmdId
        {
            get { return dmdId; }
            set
            {
                setState();
                dmdId = value;
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                setState();
                date = value;
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                setState();
                text = value;
            }
        }
        
		/*        Constructors			*/
		public Notes()
		{
			sql = new NotesSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
			date = DateTime.Now;
		}
        public Notes(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
		public Notes(UOW uow, string user, string text) : this(uow)
		{
			this.text = text;
			this.user = user;
		}
        /*        Methods        */
        protected override SqlGateway loadSql()
        {
            return new NotesSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
        /*		Static methods		*/
        public static Notes find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(Notes.getKey(id)))
                return (Notes)uow.Imap.find(Notes.getKey(id));
            
            Notes cls = new Notes();
            cls.uow = uow;
            cls.id = id;
            cls = (Notes)DomainObj.addToIMap(uow, getOne(((NotesSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static Notes[] getAll(UOW uow)
        {
            Notes[] objs = (Notes[])DomainObj.addToIMap(uow, (new NotesSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		public static Notes[] GetDmdNotes(UOW uow, int dmdId)
		{
			Notes[] objs = (Notes[])DomainObj.addToIMap(uow, (new NotesSQL()).getDmd(uow, dmdId));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			
			return objs;
		}

        /*		Implementation		*/
        static Notes getOne(Notes[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Notes src, Notes tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.user = src.user;
            tar.dmdId = src.dmdId;
            tar.date = src.date;
            tar.text = src.text;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class NotesSQL : SqlGateway
        {
            public Notes[] getKey(Notes rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spNotes_Get_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
			public Notes[] getDmd(UOW uow, int dmdId)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spNotes_Get_Dmd";
				cmd.Parameters.Add("@DmdId", SqlDbType.Int, 0).Value = dmdId;
				return convert(execReader(cmd));
			}
            public override void insert(DomainObj obj)
            {
                Notes rec = (Notes)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spNotes_Ins";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Notes rec = (Notes)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spNotes_Del_Id";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Notes rec = (Notes)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spNotes_Upd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Notes[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spNotes_Get_All";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Notes rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.user == null)
                    cmd.Parameters.Add("@User", SqlDbType.VarChar, 25).Value = DBNull.Value;
                else
                {
                    if (rec.User.Length == 0)
                        cmd.Parameters.Add("@User", SqlDbType.VarChar, 25).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@User", SqlDbType.VarChar, 25).Value = rec.user;
                }
                cmd.Parameters.Add("@DmdId", SqlDbType.Int, 0).Value = rec.dmdId;
 
                if (rec.date == DateTime.MinValue)
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = rec.date;
 
                if (rec.text == null)
                    cmd.Parameters.Add("@Text", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                else
                {
                    if (rec.Text.Length == 0)
                        cmd.Parameters.Add("@Text", SqlDbType.VarChar, 1000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Text", SqlDbType.VarChar, 1000).Value = rec.text;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Notes rec = new Notes();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["User"] != DBNull.Value)
                    rec.user = (string) rdr["User"];
 
                if (rdr["DmdId"] != DBNull.Value)
                    rec.dmdId = (int) rdr["DmdId"];
 
                if (rdr["Date"] != DBNull.Value)
                    rec.date = (DateTime) rdr["Date"];
 
                if (rdr["Text"] != DBNull.Value)
                    rec.text = (string) rdr["Text"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Notes[] convert(DomainObj[] objs)
            {
                Notes[] acls  = new Notes[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
