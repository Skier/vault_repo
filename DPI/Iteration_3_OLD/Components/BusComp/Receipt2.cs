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
    public class Receipt2 : DomainObj, IReceipt2
    {
	#region Data
        static string iName = "Receipt2";
        int id;
        string name;
        string csharpName;
        string comments;
		IReceiptItem[] items;

	#endregion

	#region Properties
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public string Name
        {
            get { return name; }
            set
            {
                setState();
                name = value;
            }
        }
        public string CSharpName
        {
            get { return csharpName; }
            set
            {
                setState();
                csharpName = value;
            }
        }
        public string Comments
        {
            get { return comments; }
            set
            {
                setState();
                comments = value;
            }
        }
		public IReceiptItem[] Items { get { return items; } }
	#endregion

	#region Constructors
 
		public Receipt2()
        {
            sql = new ReceiptSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public Receipt2(UOW uow) : this()
        {
            if(uow == null)
                throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
            if(uow.Imap == null)
                throw new ArgumentException("IdentityMap is required", "Identity Map");
            
            this.uow = uow;
            this.uow.Imap.add(this);
        }
 
	#endregion

	#region Methods
 
		public IReceiptItem[] GetItems(IUOW uow)
		{
			if (items != null)
				return items;

			return ReceiptItem.getItems((UOW)uow, this.id);
		}

        protected override SqlGateway loadSql()
        {
            return new ReceiptSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
		public IReceiptItem[] FilterItems(ReceiptItemType iType)
		{
			return ReceiptItem.FilterItems(Items, iType);
		}
		public IReceiptItem[] GetFirst(ReceiptItemType iType)
		{
			return ReceiptItem.GetFirst(Items, iType);
		}
		public IReceiptItem[]  GetNext(IReceiptItem prev)
		{
			return ReceiptItem.GetNext
				(items, (ReceiptItemType)Enum.Parse(typeof(ReceiptItemType), items[0].ItemType), prev);
		}
		public string Conv(IReceiptItem[] lines)
		{
			if (lines == null)
				return string.Empty;

			if (lines.Length == 0)
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < lines.Length; i++)
				sb.Append(lines[i].Text + @"\n");
			
			return sb.ToString();
		}
	#endregion

	#region Static methods
 
		public static IReceipt2 find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(Receipt2.getKey(id)))
				return (Receipt2)uow.Imap.find(Receipt2.getKey(id));
            
			Receipt2 cls = new Receipt2();
			cls.uow = uow;
			cls.id = id;
			cls = (Receipt2)DomainObj.addToIMap(uow, getOne(((ReceiptSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
			cls.items = cls.GetItems(uow);
			return cls;
		} 

		public static IReceipt2 find
			(UOW uow, string prodGroup, int prod, int wlProd, int corp, string store, string workflow, bool isCompleted)
        {
			int rctId = ReceiptSelRule.GetReceiptId(uow, prodGroup, prod, wlProd, corp, store, workflow, isCompleted);
			
			if (uow.Imap.keyExists(Receipt2.getKey(rctId)))
                return (Receipt2)uow.Imap.find(Receipt2.getKey(rctId));
            
            Receipt2 cls = new Receipt2();
            cls.uow = uow;
            cls.id = rctId;
            cls = (Receipt2)DomainObj.addToIMap(uow, getOne(((ReceiptSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            cls.items = cls.GetItems(uow);
            return cls;
        }
        public static IReceipt2[] getAll(UOW uow)
        {
            Receipt2[] objs = (Receipt2[])DomainObj.addToIMap(uow, (new ReceiptSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }


	#endregion

	#region Implementation	

		static Receipt2 getOne(Receipt2[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(Receipt2 src, Receipt2 tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.name = src.name;
            tar.csharpName = src.csharpName;
            tar.comments = src.comments;
            tar.rowState = src.rowState;
        }
 
	#endregion

	#region SQL	

		[Serializable]
        class ReceiptSQL : SqlGateway
        {
            public Receipt2[] getKey(Receipt2 rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptGetId";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                Receipt2 rec = (Receipt2)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptIns";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                Receipt2 rec = (Receipt2)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptDelId";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                Receipt2 rec = (Receipt2)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptUpd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public Receipt2[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spReceiptGetAll";
                return convert(execReader(cmd));
            }
            /*        Implementation        */
            void setParam(SqlCommand cmd, Receipt2 rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
                if (rec.name == null)
                    cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.Name.Length == 0)
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 100).Value = rec.name;
                }
 
                if (rec.csharpName == null)
                    cmd.Parameters.Add("@C#Name", SqlDbType.VarChar, 100).Value = DBNull.Value;
                else
                {
                    if (rec.CSharpName.Length == 0)
                        cmd.Parameters.Add("@C#Name", SqlDbType.VarChar, 100).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@C#Name", SqlDbType.VarChar, 100).Value = rec.csharpName;
                }
 
                if (rec.comments == null)
                    cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 500).Value = DBNull.Value;
                else
                {
                    if (rec.Comments.Length == 0)
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 500).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Comments", SqlDbType.VarChar, 500).Value = rec.comments;
                }
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                Receipt2 rec = new Receipt2();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["Name"] != DBNull.Value)
                    rec.name = (string) rdr["Name"];
 
                if (rdr["C#Name"] != DBNull.Value)
                    rec.csharpName = (string) rdr["C#Name"];
 
                if (rdr["Comments"] != DBNull.Value)
                    rec.comments = (string) rdr["Comments"];
 
                rec.rowState = RowState.Clean;
                return rec;
            }
            Receipt2[] convert(DomainObj[] objs)
            {
                Receipt2[] acls  = new Receipt2[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
	#endregion
    }
}
