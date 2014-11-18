//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Components;
//using DPI.Interfaces;
// 
//namespace DPI.Components
//{
//    [Serializable]
//    public class DPI_Err_Log : DomainObj
//    {
//        /*        Data        */
//        static string iName = "DPI_Err_Log";
//        int id;
//        string subsys;
//        string dPI_User;
//        DateTime dateTime;
//        string message;
//        
//        /*        Properties        */
//        public override IDomKey IKey 
//        {
//             get { return new Key(iName, id.ToString()); }
//        }
//        public int Id
//        {
//            get { return id; }
//        }
//        public string Subsys
//        {
//            get { return subsys; }
//            set
//            {
//                setState();
//                subsys = value;
//            }
//        }
//        public string DPI_User
//        {
//            get { return dPI_User; }
//            set
//            {
//                setState();
//                dPI_User = value;
//            }
//        }
//        public DateTime DateTime
//        {
//            get { return dateTime; }
//            set
//            {
//                setState();
//                dateTime = value;
//            }
//        }
//        public string Message
//        {
//            get { return message; }
//            set
//            {
//                setState();
//                message = value;
//            }
//        }
//        
//        /*        Constructors			*/
//        public DPI_Err_Log()
//        {
//            sql = new DPI_Err_LogSQL();
//            id = random.Next(Int32.MinValue, -1);
//            rowState = RowState.New;
//        }
//        public DPI_Err_Log(UOW uow) : this()
//        {
//            if(uow == null)
//                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
//            
//            if(uow.Imap == null)
//                throw new ArgumentException("IdentityMap is required", "Identity Map");
//            
//            this.uow = uow;
//            this.uow.Imap.add(this);
//        }
//        
//        /*        Methods        */
//        protected override SqlGateway loadSql()
//        {
//            return new DPI_Err_LogSQL();
//        }
//        public override void checkExists()
//        {
//            if ((Id < 1))
//                throw new ArgumentException("Valid row is required for update and delete");
//        }
//        /*		Static methods		*/
//        public static DPI_Err_Log find(UOW uow, int id)
//        {
//            if (uow.Imap.keyExists(DPI_Err_Log.getKey(id)))
//                return (DPI_Err_Log)uow.Imap.find(DPI_Err_Log.getKey(id));
//            
//            DPI_Err_Log cls = new DPI_Err_Log();
//            cls.uow = uow;
//            cls.id = id;
//            cls = (DPI_Err_Log)DomainObj.addToIMap(uow, getOne(((DPI_Err_LogSQL)cls.Sql).getKey(cls)));
//            cls.uow = uow;
//            
//            return cls;
//        }
//        public static DPI_Err_Log[] getAll(UOW uow)
//        {
//            DPI_Err_Log[] objs = (DPI_Err_Log[])DomainObj.addToIMap(uow, (new DPI_Err_LogSQL()).getAll(uow));
//            for (int i = 0; i < objs.Length; i++)
//                objs[i].uow = uow;
//            return objs;
//        }
//        public static Key getKey(int id)
//        {
//            return new Key(iName, id.ToString());
//        }
//		public static void AddLogEntry(UOW uow, string subSys, string user, string message)
//		{
//			DPI_Err_Log err = new DPI_Err_Log(uow);
//			err.Subsys = subSys;
//			err.DPI_User = user;
//			err.DateTime = DateTime.Now;
//			err.Message = message; 
//			err.add();
//		}
//		public static void AddLogEntry(string subSys, string user, string message)
//		{
//			UOW uow = null;
//			try
//			{
//				uow = new UOW();
//	
//				DPI_Err_Log err = new DPI_Err_Log(uow);
//				err.Subsys = subSys;
//				err.DPI_User = user;
//				err.DateTime = DateTime.Now;
//				err.Message = message; 
//				err.add();
//			}
//			catch(Exception e) {}
//			finally
//			{
//				uow.close();
//			}
//		}
//        /*		Implementation		*/
//        static DPI_Err_Log getOne(DPI_Err_Log[] acls)
//        {
//            if (acls.Length == 1)
//                return acls[0];
//            
//            if (acls.Length == 0)
//                throw new ArgumentException("Row not found");
//            
//            throw new ArgumentException("More than one row found");
//        }
//        static void copyAttrs(DPI_Err_Log src, DPI_Err_Log tar)
//        {
//            if (tar == null)
//                throw new ArgumentNullException("Target object must not be null");
//            
//            if (src == null)
//                throw new ArgumentNullException("Source object must not be null");
//            
//            tar.id = src.id;
//            tar.subsys = src.subsys;
//            tar.dPI_User = src.dPI_User;
//            tar.dateTime = src.dateTime;
//            tar.message = src.message;
//            tar.rowState = src.rowState;
//        }
// 
//        /*		SQL		*/
//        [Serializable]
//        class DPI_Err_LogSQL : SqlGateway
//        {
//            public DPI_Err_Log[] getKey(DPI_Err_Log rec)
//            {
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spDPI_Err_Log_Get_Id";
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
//                return convert(execReader(cmd));
//            }
//            public override void insert(DomainObj obj)
//            {
//                DPI_Err_Log rec = (DPI_Err_Log)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spDPI_Err_Log_Ins";
//                setParam(cmd, rec);
//                
//                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
//                execScalar(cmd);
//                rec.id = (int)cmd.Parameters["@Id"].Value;
//                rec.rowState = RowState.Clean;
//            }
//            public override void delete(DomainObj obj)
//            {
//                DPI_Err_Log rec = (DPI_Err_Log)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spDPI_Err_Log_Del_Id";
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
//                
//                execScalar(cmd);
//                rec.rowState = RowState.Deleted;
//            }
//            public override void update(DomainObj obj)
//            {
//                DPI_Err_Log rec = (DPI_Err_Log)obj;
//                SqlCommand cmd = getCommand(rec);
//                cmd.CommandText = "spDPI_Err_Log_Upd";
//                setParam(cmd, rec);
//                
//                execScalar(cmd);
//                rec.rowState = RowState.Clean;
//            }
//            public DPI_Err_Log[] getAll(UOW uow)
//            {
//                SqlCommand cmd = makeCommand(uow);
//                cmd.CommandText = "spDPI_Err_Log_Get_All";
//                return convert(execReader(cmd));
//            }
//            /*        Implementation        */
//            void setParam(SqlCommand cmd, DPI_Err_Log rec)
//            {
//                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
// 
//                cmd.Parameters.Add("@Subsys", SqlDbType.VarChar, 500).Value = rec.subsys;
// 
//                if (rec.dPI_User == null)
//                    cmd.Parameters.Add("@DPI_User", SqlDbType.VarChar, 50).Value = DBNull.Value;
//                else
//                {
//                    if (rec.DPI_User.Length == 0)
//                        cmd.Parameters.Add("@DPI_User", SqlDbType.VarChar, 50).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@DPI_User", SqlDbType.VarChar, 50).Value = rec.dPI_User;
//                }
// 
//                cmd.Parameters.Add("@DateTime", SqlDbType.DateTime, 0).Value = rec.dateTime;
// 
//                if (rec.message == null)
//                    cmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000).Value = DBNull.Value;
//                else
//                {
//                    if (rec.Message.Length == 0)
//                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000).Value = DBNull.Value;
//                    else
//                        cmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000).Value = rec.message;
//                }
//            }
//            protected override DomainObj reader(SqlDataReader rdr)
//            {
//                DPI_Err_Log rec = new DPI_Err_Log();
//                
//                if (rdr["Id"] != DBNull.Value)
//                    rec.id = (int) rdr["Id"];
// 
//                if (rdr["Subsys"] != DBNull.Value)
//                    rec.subsys = (string) rdr["Subsys"];
// 
//                if (rdr["DPI_User"] != DBNull.Value)
//                    rec.dPI_User = (string) rdr["DPI_User"];
// 
//                if (rdr["DateTime"] != DBNull.Value)
//                    rec.dateTime = (DateTime) rdr["DateTime"];
// 
//                if (rdr["Message"] != DBNull.Value)
//                    rec.message = (string) rdr["Message"];
// 
//                rec.rowState = RowState.Clean;
//                return rec;
//            }
//            DPI_Err_Log[] convert(DomainObj[] objs)
//            {
//                DPI_Err_Log[] acls  = new DPI_Err_Log[objs.Length];
//                objs.CopyTo(acls, 0);
//                return  acls;
//            }
//        }
//    }
//}
