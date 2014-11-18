using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class Account_Notes : DomainObj
	{
		/*        Data        */
		static string iName = "Account_Notes";
		int account_Notes_ID;
		int accNumber;
		DateTime date;
		string userid;
		string note;
		string department;

		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, account_Notes_ID.ToString()); }
		}
		public int Account_Notes_ID
		{
			get { return account_Notes_ID; }
		}
		public int AccNumber
		{
			get { return accNumber; }
			set
			{
				setState();
				accNumber = value;
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
		public string UserId
		{
			get { return userid; }
			set
			{
				setState();
				userid = value;
			}
		}
		public string Note
		{
			get { return note; }
			set
			{
				setState();
				note = value;
			}
		}
		public string Department
		{
			get { return department; }
			set
			{
				setState();
				department = value;
			}
		}
        
		/*        Constructors			*/
		public Account_Notes()
		{
			sql = new Account_NotesSQL();
			account_Notes_ID = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
			this.date = DateTime.Now;
		}
		public Account_Notes(UOW uow, string user, string note, string dept) : this(uow)
		{
			this.userid = user;
			this.note = note;
			this.department = dept;
		}
		public Account_Notes(UOW uow, int accNumber, string user, string note, string dept) : this(uow, user, note, dept)
		{
			this.accNumber = accNumber;
		}
		public Account_Notes(UOW uow) : this()
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
			return new Account_NotesSQL();
		}
		public override void checkExists()
		{
			if ((Account_Notes_ID < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static Account_Notes find(UOW uow, int account_Notes_ID)
		{
			if (uow.Imap.keyExists(Account_Notes.getKey(account_Notes_ID)))
				return (Account_Notes)uow.Imap.find(Account_Notes.getKey(account_Notes_ID));
            
			Account_Notes cls = new Account_Notes();
			cls.uow = uow;
			cls.account_Notes_ID = account_Notes_ID;
			cls = (Account_Notes)DomainObj.addToIMap(uow, getOne(((Account_NotesSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static Account_Notes[] getAll(UOW uow)
		{
			Account_Notes[] objs = (Account_Notes[])DomainObj.addToIMap(uow, (new Account_NotesSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int account_Notes_ID)
		{
			return new Key(iName, account_Notes_ID.ToString());
		}
		/*		Implementation		*/
		static Account_Notes getOne(Account_Notes[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(Account_Notes src, Account_Notes tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.account_Notes_ID = src.account_Notes_ID;
			tar.accNumber = src.accNumber;
			tar.date = src.date;
			tar.userid = src.userid;
			tar.note = src.note;
			tar.department = src.department;
			tar.rowState = src.rowState;
		}
 
		/*		SQL		*/
		[Serializable]
			class Account_NotesSQL : SqlGateway
		{
			public Account_Notes[] getKey(Account_Notes rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spAccount_Notes_Get_Id";
				cmd.Parameters.Add("@Account_Notes_ID", SqlDbType.Int, 0).Value = rec.account_Notes_ID;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				Account_Notes rec = (Account_Notes)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spAccount_Notes_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Account_Notes_ID"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.account_Notes_ID = (int)cmd.Parameters["@Account_Notes_ID"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				Account_Notes rec = (Account_Notes)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spAccount_Notes_Del_Id";
				cmd.Parameters.Add("@Account_Notes_ID", SqlDbType.Int, 0).Value = rec.account_Notes_ID;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				Account_Notes rec = (Account_Notes)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spAccount_Notes_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public Account_Notes[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spAccount_Notes_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, Account_Notes rec)
			{
				cmd.Parameters.Add("@Account_Notes_ID", SqlDbType.Int, 0).Value = rec.account_Notes_ID;
                
				// Numeric, nullable foreign key treatment:
				if (rec.AccNumber == 0)
					cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = rec.accNumber;
 
				cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = rec.date;
 
				cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 20).Value = rec.userid;
				cmd.Parameters.Add("@Note", SqlDbType.VarChar, 255).Value = rec.note;
				cmd.Parameters.Add("@Department", SqlDbType.VarChar, 20).Value = rec.department;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				Account_Notes rec = new Account_Notes();
                
				if (rdr["Account_Notes_ID"] != DBNull.Value)
					rec.account_Notes_ID = (int) rdr["Account_Notes_ID"];
 
				if (rdr["AccNumber"] != DBNull.Value)
					rec.accNumber = (int) rdr["AccNumber"];
 
				if (rdr["Date"] != DBNull.Value)
					rec.date = (DateTime) rdr["Date"];
 
				if (rdr["UserId"] != DBNull.Value)
					rec.userid = (string) rdr["UserId"];
 
				if (rdr["Actvity"] != DBNull.Value)
					rec.note = (string) rdr["Note"];
 
				if (rdr["Department"] != DBNull.Value)
					rec.department = (string) rdr["Department"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			Account_Notes[] convert(DomainObj[] objs)
			{
				Account_Notes[] acls  = new Account_Notes[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
	}
}
