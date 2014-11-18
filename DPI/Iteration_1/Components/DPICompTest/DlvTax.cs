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
	public class DlvTax : DomainObj
	{
		/*        Data        */
		static string iName = "DlvTax";
		int id;
		int dlv;
		string taxId;
		decimal taxAmount;
        
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public int Dlv
		{
			get { return dlv; }
			set
			{
				setState();
				dlv = value;
			}
		}
		public string TaxId
		{
			get { return taxId; }
			set
			{
				setState();
				taxId = value;
			}
		}
		public decimal TaxAmount
		{
			get { return taxAmount; }
			set
			{
				setState();
				taxAmount = Decimal.Round(value, 2);
			}
		}
        
		/*        Constructors			*/
		public DlvTax()
		{
			sql = new DlvTaxSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public DlvTax(UOW uow) : this()
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
			return new DlvTaxSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static DlvTax find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(DlvTax.getKey(id)))
				return (DlvTax)uow.Imap.find(DlvTax.getKey(id));
            
			DlvTax cls = new DlvTax();
			cls.uow = uow;
			cls.id = id;
			cls = (DlvTax)DomainObj.addToIMap(uow, getOne(((DlvTaxSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static DlvTax[] getAll(UOW uow)
		{
			DlvTax[] objs = (DlvTax[])DomainObj.addToIMap(uow, (new DlvTaxSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
		/*		Implementation		*/
		static DlvTax getOne(DlvTax[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(DlvTax src, DlvTax tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
		//	tar.ver = src.ver;
			tar.dlv = src.dlv;
			tar.taxId = src.taxId;
			tar.taxAmount = src.taxAmount;
			tar.rowState = src.rowState;
		}
 
		/*		SQL		*/
		[Serializable]
			class DlvTaxSQL : SqlGateway
		{
			public DlvTax[] getKey(DlvTax rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDlvTax_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				DlvTax rec = (DlvTax)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDlvTax_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				DlvTax rec = (DlvTax)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDlvTax_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
	//			rec.ver++;
	//			cmd.Parameters.Add("@Ver", SqlDbType.Int, 4).Value = rec.ver;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				DlvTax rec = (DlvTax)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDlvTax_Upd";
		//		rec.ver++;
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public DlvTax[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDlvTax_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, DlvTax rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				// cmd.Parameters.Add("@Ver", SqlDbType.Int, 0).Value = rec.ver;
                
				// Numeric, nullable foreign key treatment:
				if (rec.Dlv == 0)
					cmd.Parameters.Add("@Dlv", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@Dlv", SqlDbType.Int, 0).Value = rec.dlv;
 
				if (rec.taxId == null)
					cmd.Parameters.Add("@TaxId", SqlDbType.VarChar, 3).Value = DBNull.Value;
				else
				{
					if (rec.TaxId.Length == 0)
						cmd.Parameters.Add("@TaxId", SqlDbType.VarChar, 3).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@TaxId", SqlDbType.VarChar, 3).Value = rec.taxId;
				}
				cmd.Parameters.Add("@TaxAmount", SqlDbType.Decimal, 0).Value = rec.taxAmount;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				DlvTax rec = new DlvTax();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
//				if (rdr["Ver"] != DBNull.Value)
//					rec.ver = (int) rdr["Ver"];
 
				if (rdr["Dlv"] != DBNull.Value)
					rec.dlv = (int) rdr["Dlv"];
 
				if (rdr["TaxId"] != DBNull.Value)
					rec.taxId = (string) rdr["TaxId"];
 
				if (rdr["TaxAmount"] != DBNull.Value)
					rec.taxAmount = Decimal.Round((decimal)rdr["TaxAmount"], 2);
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			DlvTax[] convert(DomainObj[] objs)
			{
				DlvTax[] acls  = new DlvTax[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
	}
}