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
    public class ReceiptItem : DomainObj, IReceiptItem, System.Collections.IComparer, System.IComparable
    {
        /*        Data        */
        static string iName = "ReceiptItem";
        int id;
        int receiptId;
        string itemType;
        string itemOrder;
        string text;
        string overrideFontFamilty;
        string overrideFontStyle;
        int overrideFontSize;
		string itemGroup;
		string hAlignment;
		string vAlignment;
        
        /*        Properties        */
        public override IDomKey IKey 
        {
             get { return new Key(iName, id.ToString()); }
        }
        public int Id
        {
            get { return id; }
        }
        public int ReceiptId
        {
            get { return receiptId; }
            set
            {
                setState();
                receiptId = value;
            }
        }
        public string ItemType
        {
            get { return itemType; }
            set
            {
                setState();
                itemType = value;
            }
        }
        public string ItemOrder
        {
            get { return itemOrder; }
            set
            {
                setState();
                itemOrder = value;
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
        public string OverrideFontFamilty
        {
            get { return overrideFontFamilty; }
            set
            {
                setState();
                overrideFontFamilty = value;
            }
        }
        public string OverrideFontStyle
        {
            get { return overrideFontStyle; }
            set
            {
                setState();
                overrideFontStyle = value;
            }
        }
        public int OverrideFontSize
        {
            get { return overrideFontSize; }
            set
            {
                setState();
                overrideFontSize = value;
            }
        }
		public string ItemGroup
		{
			get { return itemGroup; }
			set
			{
				setState();
				itemGroup = value;
			}
		}
		public string HAlignment
		{
			get { return hAlignment; }
			set
			{
				setState();
				hAlignment = value;
			}
		}
		public string VAlignment
		{
			get { return vAlignment; }
			set
			{
				setState();
				vAlignment = value;
			}
		}

        /*        Constructors			*/
        public ReceiptItem()
        {
            sql = new ReceiptItemSQL();
            id = random.Next(Int32.MinValue, -1);
            rowState = RowState.New;
        }
        public ReceiptItem(UOW uow) : this()
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
            return new ReceiptItemSQL();
        }
        public override void checkExists()
        {
            if ((Id < 1))
                throw new ArgumentException("Valid row is required for update and delete");
        }
	
		new public int CompareTo(object item)
		{
			int res = Compare(this, item);

			string me = this.itemGroup;
			string he = ((ReceiptItem)item).itemGroup;

			return res;
		}
		public int Compare(object first, object second)
		{
			if (first == null)
				return -1;
			
			if (second == null)
				return 1;

			if (first == second)
				return 0;

			return string.Compare(((ReceiptItem)first).GetSortKey(), ((ReceiptItem)second).GetSortKey()); 
		//	Less than zero  x is less than y. 
		//  Zero  x equals y.    
		//	Greater than zero  x is greater than y.
		}
		string GetSortKey()
		{
			return itemType.Trim().ToLower() + itemGroup.Trim().ToLower() + itemOrder.Trim().ToLower();
		}
        /*		Static methods		*/
        public static ReceiptItem find(UOW uow, int id)
        {
            if (uow.Imap.keyExists(ReceiptItem.getKey(id)))
                return (ReceiptItem)uow.Imap.find(ReceiptItem.getKey(id));
            
            ReceiptItem cls = new ReceiptItem();
            cls.uow = uow;
            cls.id = id;
            cls = (ReceiptItem)DomainObj.addToIMap(uow, getOne(((ReceiptItemSQL)cls.Sql).getKey(cls)));
            cls.uow = uow;
            
            return cls;
        }
        public static ReceiptItem[] getAll(UOW uow)
        {
            ReceiptItem[] objs = (ReceiptItem[])DomainObj.addToIMap(uow, (new ReceiptItemSQL()).getAll(uow));
            for (int i = 0; i < objs.Length; i++)
                objs[i].uow = uow;
            return objs;
        }
		public static ReceiptItem[] getItems(UOW uow, int receiptId)
		{
			ReceiptItem[] objs = (ReceiptItem[])DomainObj.addToIMap(uow, (new ReceiptItemSQL()).getItems(uow, receiptId));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
        public static Key getKey(int id)
        {
            return new Key(iName, id.ToString());
        }
		public static IReceiptItem[] FilterItems(IReceiptItem[] items, ReceiptItemType iType)
		{
			ArrayList ar = new ArrayList();
			
			for (int i = 0; i < items.Length; i++)
				if (items[i].ItemType.Trim().ToLower() == iType.ToString().ToLower())
					ar.Add(items[i]);

			IReceiptItem[] filtered = new  IReceiptItem[ar.Count];
			ar.CopyTo(filtered);
		
			//Array.Sort(filtered);
			return filtered;
		}
		public static IReceiptItem[] GetFirst(IReceiptItem[] items, ReceiptItemType iType)
		{
			ArrayList ar = new ArrayList();

			ar.AddRange(FilterItems(items, iType));			
			ar.Sort();

			if (ar.Count == 0)
				return new IReceiptItem[0]; 

			string b1 = ((IReceiptItem)ar[0]).ItemGroup;
			
			ArrayList arGroup = new ArrayList();
			for (int i = 0; i < ar.Count; i++)
				if (((IReceiptItem)ar[i]).ItemGroup == b1)
					arGroup.Add(ar[i]);
			
			IReceiptItem[] filtered = new  IReceiptItem[arGroup.Count];
			arGroup.CopyTo(filtered);

			return filtered;
		}
		public static IReceiptItem[] GetNext(IReceiptItem[] items, ReceiptItemType iType, IReceiptItem prev)
		{
			if (items == null)
				return new ReceiptItem[0];
 
			if (items.Length == 0)
				return new ReceiptItem[0];

			if (prev == null)
				return new ReceiptItem[0];
 
			ArrayList ar = new ArrayList();
			ar.AddRange(FilterItems(items, iType));			

			string nextGroup = FindNextGroup(ar, prev);

			ArrayList arGroup = new ArrayList();
			for (int i = 0; i < ar.Count; i++)
				if (((ReceiptItem)ar[i]).ItemGroup == nextGroup)
					arGroup.Add(ar[i]);

			if (arGroup.Count == 0)
				return new IReceiptItem[0];

			ReceiptItem[] filtered = new ReceiptItem[arGroup.Count];
			arGroup.CopyTo(filtered);
			Array.Sort(filtered);
			return filtered;
		}
        /*		Implementation		*/
		static string FindNextGroup(ArrayList ar, IReceiptItem prev)
		{
			if (ar == null)
				return string.Empty;

			if (ar.Count < 2)
				return string.Empty;

			if (prev == null)
				throw new ArgumentNullException("Previous item");

			IReceiptItem[] items = new IReceiptItem[ar.Count];
			ar.CopyTo(items);
			Array.Sort(items);
			
			int i = 0;
			for (; i < items.Length; i++)
				if (items[i] == prev)
				  break;

			if (i == items.Length)
				throw new ArgumentException("Previous item must be in the array");				 				  

			for (; i < items.Length; i++)
				if (((IReceiptItem)items[i]).ItemGroup != prev.ItemGroup)
					return ((IReceiptItem)items[i]).ItemGroup;
				
			return string.Empty;
		}
        static ReceiptItem getOne(ReceiptItem[] acls)
        {
            if (acls.Length == 1)
                return acls[0];
            
            if (acls.Length == 0)
                throw new ArgumentException("Row not found");
            
            throw new ArgumentException("More than one row found");
        }
        static void copyAttrs(ReceiptItem src, ReceiptItem tar)
        {
            if (tar == null)
                throw new ArgumentNullException("Target object must not be null");
            
            if (src == null)
                throw new ArgumentNullException("Source object must not be null");
            
            tar.id = src.id;
            tar.receiptId = src.receiptId;
            tar.itemType = src.itemType;
            tar.itemOrder = src.itemOrder;
            
			tar.text = src.text;
            tar.overrideFontFamilty = src.overrideFontFamilty;
            tar.overrideFontStyle = src.overrideFontStyle;
            tar.overrideFontSize = src.overrideFontSize;

			tar.itemGroup = src.itemGroup;
			tar.hAlignment = src.hAlignment;
			tar.vAlignment = src.vAlignment;
            tar.rowState = src.rowState;
        }
 
        /*		SQL		*/
        [Serializable]
        class ReceiptItemSQL : SqlGateway
        {
            public ReceiptItem[] getKey(ReceiptItem rec)
            {
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptItemGetId";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                return convert(execReader(cmd));
            }
            public override void insert(DomainObj obj)
            {
                ReceiptItem rec = (ReceiptItem)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptItemIns";
                setParam(cmd, rec);
                
                cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
                execScalar(cmd);
                rec.id = (int)cmd.Parameters["@Id"].Value;
                rec.rowState = RowState.Clean;
            }
            public override void delete(DomainObj obj)
            {
                ReceiptItem rec = (ReceiptItem)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptItemDelId";
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                execScalar(cmd);
                rec.rowState = RowState.Deleted;
            }
            public override void update(DomainObj obj)
            {
                ReceiptItem rec = (ReceiptItem)obj;
                SqlCommand cmd = getCommand(rec);
                cmd.CommandText = "spReceiptItemUpd";
                setParam(cmd, rec);
                
                execScalar(cmd);
                rec.rowState = RowState.Clean;
            }
            public ReceiptItem[] getAll(UOW uow)
            {
                SqlCommand cmd = makeCommand(uow);
                cmd.CommandText = "spReceiptItemGetAll";
                return convert(execReader(cmd));
            }
			public ReceiptItem[] getItems(UOW uow, int receiptId)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spReceiptItemGetReceiptId";
                cmd.Parameters.Add("@ReceiptId", SqlDbType.Int, 0).Value = receiptId;
				return convert(execReader(cmd));
			}

            /*        Implementation        */
            void setParam(SqlCommand cmd, ReceiptItem rec)
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
                // Numeric, nullable foreign key treatment:
                if (rec.ReceiptId == 0)
                    cmd.Parameters.Add("@ReceiptId", SqlDbType.Int, 0).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@ReceiptId", SqlDbType.Int, 0).Value = rec.receiptId;
 
                if (rec.itemType == null)
                    cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.ItemType.Length == 0)
                        cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ItemType", SqlDbType.VarChar, 50).Value = rec.itemType;
                }
 
                if (rec.itemOrder == null)
                    cmd.Parameters.Add("@ItemOrder", SqlDbType.VarChar, 10).Value = DBNull.Value;
                else
                {
                    if (rec.ItemOrder.Length == 0)
                        cmd.Parameters.Add("@ItemOrder", SqlDbType.VarChar, 10).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@ItemOrder", SqlDbType.VarChar, 10).Value = rec.itemOrder;
                }
 
                if (rec.text == null)
                    cmd.Parameters.Add("@Text", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                else
                {
                    if (rec.Text.Length == 0)
                        cmd.Parameters.Add("@Text", SqlDbType.VarChar, 2000).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@Text", SqlDbType.VarChar, 2000).Value = rec.text;
                }
 
                if (rec.overrideFontFamilty == null)
                    cmd.Parameters.Add("@OverrideFontFamilty", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.OverrideFontFamilty.Length == 0)
                        cmd.Parameters.Add("@OverrideFontFamilty", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@OverrideFontFamilty", SqlDbType.VarChar, 50).Value = rec.overrideFontFamilty;
                }
 
                if (rec.overrideFontStyle == null)
                    cmd.Parameters.Add("@OverrideFontStyle", SqlDbType.VarChar, 50).Value = DBNull.Value;
                else
                {
                    if (rec.OverrideFontStyle.Length == 0)
                        cmd.Parameters.Add("@OverrideFontStyle", SqlDbType.VarChar, 50).Value = DBNull.Value;
                    else
                        cmd.Parameters.Add("@OverrideFontStyle", SqlDbType.VarChar, 50).Value = rec.overrideFontStyle;
                }

                cmd.Parameters.Add("@OverrideFontSize", SqlDbType.Int, 0).Value = rec.overrideFontSize;
				
				if (rec.itemGroup == null)
					cmd.Parameters.Add("@ItemGroup", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.itemGroup.Length == 0)
						cmd.Parameters.Add("@ItemGroup", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ItemGroup", SqlDbType.VarChar, 50).Value = rec.itemGroup;
				}
								
				if (rec.hAlignment == null)
					cmd.Parameters.Add("@HAlignment", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.hAlignment.Length == 0)
						cmd.Parameters.Add("@HAlignment", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@HAlignment", SqlDbType.VarChar, 50).Value = rec.hAlignment;
				}

				if (rec.vAlignment == null)
					cmd.Parameters.Add("@VAlignment", SqlDbType.VarChar, 25).Value = DBNull.Value;
				else
				{
					if (rec.vAlignment.Length == 0)
						cmd.Parameters.Add("@VAlignment", SqlDbType.VarChar, 50).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@VAlignment", SqlDbType.VarChar, 50).Value = rec.vAlignment;
				}
            }
            protected override DomainObj reader(SqlDataReader rdr)
            {
                ReceiptItem rec = new ReceiptItem();
                
                if (rdr["Id"] != DBNull.Value)
                    rec.id = (int) rdr["Id"];
 
                if (rdr["ReceiptId"] != DBNull.Value)
                    rec.receiptId = (int) rdr["ReceiptId"];
 
                if (rdr["ItemType"] != DBNull.Value)
                    rec.itemType = (string) rdr["ItemType"];
 
                if (rdr["ItemOrder"] != DBNull.Value)
                    rec.itemOrder = (string) rdr["ItemOrder"];
 
                if (rdr["Text"] != DBNull.Value)
                    rec.text = (string) rdr["Text"];
 
                if (rdr["OverrideFontFamilty"] != DBNull.Value)
                    rec.overrideFontFamilty = (string) rdr["OverrideFontFamilty"];
 
                if (rdr["OverrideFontStyle"] != DBNull.Value)
                    rec.overrideFontStyle = (string) rdr["OverrideFontStyle"];
 
                if (rdr["OverrideFontSize"] != DBNull.Value)
                    rec.overrideFontSize = (int) rdr["OverrideFontSize"];


				if (rdr["ItemGroup"] != DBNull.Value)
					rec.itemGroup = (string) rdr["ItemGroup"];
 
				if (rdr["HAlignment"] != DBNull.Value)
					rec.hAlignment = (string) rdr["HAlignment"];
 
				if (rdr["VAlignment"] != DBNull.Value)
					rec.vAlignment = (string) rdr["VAlignment"];

 
                rec.rowState = RowState.Clean;
                return rec;
            }
            ReceiptItem[] convert(DomainObj[] objs)
            {
                ReceiptItem[] acls  = new ReceiptItem[objs.Length];
                objs.CopyTo(acls, 0);
                return  acls;
            }
        }
    }
}
