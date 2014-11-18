using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Components;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class DPICompLog : DomainObj
	{
		/*        Data        */
		static string iName = "DPICompLog";
		int id;
		string subsys;
		string dPI_User;
		DateTime dateTime;
		string message;
        
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public string Subsys
		{
			get { return subsys; }
			set
			{
				setState();
				subsys = value;
			}
		}
		public string DPI_User
		{
			get { return dPI_User; }
			set
			{
				setState();
				dPI_User = value;
			}
		}
		public DateTime DateTime
		{
			get { return dateTime; }
			set
			{
				setState();
				dateTime = value;
			}
		}
		public string Message
		{
			get { return message; }
			set
			{
				setState();
				message = value;
			}
		}
        
		/*        Constructors			*/
		public DPICompLog()
		{
			sql = new DPICompLogSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public DPICompLog(UOW uow) : this()
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
			return new DPICompLogSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static DPICompLog find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(DPICompLog.getKey(id)))
				return (DPICompLog)uow.Imap.find(DPICompLog.getKey(id));
            
			DPICompLog cls = new DPICompLog();
			cls.uow = uow;
			cls.id = id;
			cls = (DPICompLog)DomainObj.addToIMap(uow, getOne(((DPICompLogSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static DPICompLog[] getAll(UOW uow)
		{
			DPICompLog[] objs = (DPICompLog[])DomainObj.addToIMap(uow, (new DPICompLogSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
		public static void AddLogEntry(UOW uow, string subSys, string user, string message)
		{
			DPICompLog err = new DPICompLog(uow);
			err.Subsys = subSys;
			err.DPI_User = user;
			err.DateTime = DateTime.Now;
			err.Message = message; 
			err.add();
		}
		public static void AddLogEntry(string subSys, string user, string message)
		{
			UOW uow = null;
			try
			{
				uow = new UOW();
	
				DPICompLog err = new DPICompLog(uow);
				err.Subsys = subSys;
				err.DPI_User = user;
				err.DateTime = DateTime.Now;
				err.Message = message; 
				err.add();
			}
			catch {}
			finally
			{
				uow.close();
			}
		}
		/*		Implementation		*/
		static DPICompLog getOne(DPICompLog[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(DPICompLog src, DPICompLog tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.subsys = src.subsys;
			tar.dPI_User = src.dPI_User;
			tar.dateTime = src.dateTime;
			tar.message = src.message;
			tar.rowState = src.rowState;
		}
 
		/*		SQL		*/
		[Serializable]
			class DPICompLogSQL : SqlGateway
		{
			public DPICompLog[] getKey(DPICompLog rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDPICompLog_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				DPICompLog rec = (DPICompLog)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDPICompLog_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				DPICompLog rec = (DPICompLog)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDPICompLog_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				DPICompLog rec = (DPICompLog)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDPICompLog_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public DPICompLog[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDPICompLog_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, DPICompLog rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				cmd.Parameters.Add("@Subsys", SqlDbType.VarChar, 25).Value = rec.subsys;
 
				if (rec.dPI_User == null)
					cmd.Parameters.Add("@DPI_User", SqlDbType.VarChar, 20).Value = DBNull.Value;
				else
				{
					if (rec.DPI_User.Length == 0)
						cmd.Parameters.Add("@DPI_User", SqlDbType.VarChar, 20).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DPI_User", SqlDbType.VarChar, 20).Value = rec.dPI_User;
				}
 
				cmd.Parameters.Add("@DateTime", SqlDbType.DateTime, 0).Value = rec.dateTime;
 
				if (rec.message == null)
					cmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000).Value = DBNull.Value;
				else
				{
					if (rec.Message.Length == 0)
						cmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Message", SqlDbType.VarChar, 2000).Value = rec.message;
				}
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				DPICompLog rec = new DPICompLog();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["Subsys"] != DBNull.Value)
					rec.subsys = (string) rdr["Subsys"];
 
				if (rdr["DPI_User"] != DBNull.Value)
					rec.dPI_User = (string) rdr["DPI_User"];
 
				if (rdr["DateTime"] != DBNull.Value)
					rec.dateTime = (DateTime) rdr["DateTime"];
 
				if (rdr["Message"] != DBNull.Value)
					rec.message = (string) rdr["Message"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			DPICompLog[] convert(DomainObj[] objs)
			{
				DPICompLog[] acls  = new DPICompLog[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
	}
}
