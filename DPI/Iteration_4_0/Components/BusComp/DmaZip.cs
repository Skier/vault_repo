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
	public class DmaZip : DomainObj
	{
		/*        Data        */
		static string iName = "DmaZip";
		int id;
		int dMA;
		string dMA_name;
		string stateUS;
		string zip;
		string city;
		string dMA_rank;
		string metro;
        
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public int DMA
		{
			get { return dMA; }
			set
			{
				setState();
				dMA = value;
			}
		}
		public string DMA_name
		{
			get { return dMA_name; }
			set
			{
				setState();
				dMA_name = value;
			}
		}
		public string StateUS
		{
			get { return stateUS; }
			set
			{
				setState();
				stateUS = value;
			}
		}
		public string Zip
		{
			get { return zip; }
			set
			{
				setState();
				zip = value;
			}
		}
		public string City
		{
			get { return city; }
			set
			{
				setState();
				city = value;
			}
		}
		public string DMA_rank
		{
			get { return dMA_rank; }
			set
			{
				setState();
				dMA_rank = value;
			}
		}
		public string Metro
		{
			get { return metro; }
			set
			{
				setState();
				metro = value;
			}
		}
        
		/*        Constructors			*/
		public DmaZip()
		{
			sql = new DmaZipSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public DmaZip(UOW uow) : this()
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
			return new DmaZipSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static DmaZip find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(DmaZip.getKey(id)))
				return (DmaZip)uow.Imap.find(DmaZip.getKey(id));
            
			DmaZip cls = new DmaZip();
			cls.uow = uow;
			cls.id = id;
			cls = (DmaZip)DomainObj.addToIMap(uow, getOne(((DmaZipSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static string getState(UOW uow, string zip)
		{
			return getZip(uow, zip)[0].StateUS;
		}
		public static DmaZip[] getZip(UOW uow, string zip)
		{
    		DmaZip[] objs = (DmaZip[])DomainObj.addToIMap(uow, (new DmaZipSQL()).getZip(uow, zip));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
	
			return objs;
		}

		public static DmaZip[] getAll(UOW uow)
		{
			DmaZip[] objs = (DmaZip[])DomainObj.addToIMap(uow, (new DmaZipSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static bool checkZip(UOW uow, string zip)
		{
			return getZip(uow, zip).Length > 0;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
		/*		Implementation		*/
		static DmaZip getOne(DmaZip[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(DmaZip src, DmaZip tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.dMA = src.dMA;
			tar.dMA_name = src.dMA_name;
			tar.rowState = src.rowState;
			tar.zip = src.zip;
			tar.city = src.city;
			tar.dMA_rank = src.dMA_rank;
			tar.metro = src.metro;
			tar.rowState = src.rowState;
		}
 
		/*		SQL		*/
		[Serializable]
			class DmaZipSQL : SqlGateway
		{
			public DmaZip[] getKey(DmaZip rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmaZip_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public DmaZip[] getZip(UOW uow, string zip)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmaZip_Get_Zip";
				cmd.Parameters.Add("@Zip", SqlDbType.VarChar,5).Value = zip;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				DmaZip rec = (DmaZip)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmaZip_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				DmaZip rec = (DmaZip)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmaZip_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			public override void update(DomainObj obj)
			{
				DmaZip rec = (DmaZip)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spDmaZip_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd);
				rec.rowState = RowState.Clean;
			}
			public DmaZip[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spDmaZip_Get_All";
				return convert(execReader(cmd));
			}
			/*        Implementation        */
			void setParam(SqlCommand cmd, DmaZip rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				cmd.Parameters.Add("@DMA", SqlDbType.Int, 0).Value = rec.dMA;
 
				if (rec.dMA_name == null)
					cmd.Parameters.Add("@DMA_name", SqlDbType.VarChar, 255).Value = DBNull.Value;
				else
				{
					if (rec.DMA_name.Length == 0)
						cmd.Parameters.Add("@DMA_name", SqlDbType.VarChar, 255).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DMA_name", SqlDbType.VarChar, 255).Value = rec.dMA_name;
				}
 
				if (rec.stateUS == null)
					cmd.Parameters.Add("@State", SqlDbType.VarChar, 255).Value = DBNull.Value;
				else
				{
					if (rec.stateUS.Length == 0)
						cmd.Parameters.Add("@State", SqlDbType.VarChar, 255).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@State", SqlDbType.VarChar, 255).Value = rec.rowState;
				}
 
				if (rec.zip == null)
					cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 255).Value = DBNull.Value;
				else
				{
					if (rec.Zip.Length == 0)
						cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 255).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 255).Value = rec.zip;
				}
 
				if (rec.city == null)
					cmd.Parameters.Add("@City", SqlDbType.VarChar, 255).Value = DBNull.Value;
				else
				{
					if (rec.City.Length == 0)
						cmd.Parameters.Add("@City", SqlDbType.VarChar, 255).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@City", SqlDbType.VarChar, 255).Value = rec.city;
				}
 
				if (rec.dMA_rank == null)
					cmd.Parameters.Add("@DMA_rank", SqlDbType.VarChar, 255).Value = DBNull.Value;
				else
				{
					if (rec.DMA_rank.Length == 0)
						cmd.Parameters.Add("@DMA_rank", SqlDbType.VarChar, 255).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@DMA_rank", SqlDbType.VarChar, 255).Value = rec.dMA_rank;
				}
 
				if (rec.metro == null)
					cmd.Parameters.Add("@Metro", SqlDbType.VarChar, 255).Value = DBNull.Value;
				else
				{
					if (rec.Metro.Length == 0)
						cmd.Parameters.Add("@Metro", SqlDbType.VarChar, 255).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@Metro", SqlDbType.VarChar, 255).Value = rec.metro;
				}
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				DmaZip rec = new DmaZip();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["DMA"] != DBNull.Value)
					rec.dMA = (int) rdr["DMA"];
 
				if (rdr["DMA_name"] != DBNull.Value)
					rec.dMA_name = (string) rdr["DMA_name"];
 
				if (rdr["State"] != DBNull.Value)
					rec.stateUS = (string) rdr["State"];
 
				if (rdr["Zip"] != DBNull.Value)
					rec.zip = (string) rdr["Zip"];
 
				if (rdr["City"] != DBNull.Value)
					rec.city = (string) rdr["City"];
 
				if (rdr["DMA_rank"] != DBNull.Value)
					rec.dMA_rank = (string) rdr["DMA_rank"];
 
				if (rdr["Metro"] != DBNull.Value)
					rec.metro = (string) rdr["Metro"];
 
				rec.rowState = RowState.Clean;
				return rec;
			}
			DmaZip[] convert(DomainObj[] objs)
			{
				DmaZip[] acls  = new DmaZip[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
		}
	}
}

//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Windows.Forms;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using DPI.Interfaces;
//
//namespace DPI.Components
//{
//	//[Serializable]
//	public class DmaZip // Read-only
//	{
//		/*        Data        */
//	
//		/*        Properties        */
//
//		/*		Methods		*/
//		public static bool checkZip(UOW uow, string zip)
//		{
//			string[] zips = new DmaZipSql().checkZip(uow, zip);
//			return zips.Length > 0;
//		}
//		public static string getState(UOW uow, string zip)
//		{
//	//		return new DmaZipSql().
//		}
//		/*		SQL		*/
//		class DmaZipSql
//		{
//			public string[] checkZip(UOW uow, string zip)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = zip;
//				cmd.CommandText = "dbo.spDMAZip_Get_Zip";
//				return execReader(cmd);
//			}
//			public string[] getState(UOW uow, string zip)
//			{
//				SqlCommand cmd = makeCommand(uow);
//				cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5).Value = zip;
//				cmd.CommandText = "dbo.spDMAZip_Get_Zip";
//				return execReader(cmd);
//			}
//
//			/*        Implementation        */
//			SqlCommand makeCommand(UOW uow)
//			{
//				SqlCommand cmd = uow.Cn.CreateCommand();
//				cmd.Transaction = uow.Tran; 
//				cmd.CommandType = CommandType.StoredProcedure;
//				return cmd;
//			}
//			string[] execReader(SqlCommand cmd)
//			{
//				ArrayList ar = new ArrayList();
//				SqlDataReader rdr = cmd.ExecuteReader();
//
//				try
//				{
//					while(rdr.Read())
//						ar.Add(ZipReader(rdr));
//
//					string[] recs = new string[ar.Count];
//					ar.CopyTo(recs);
//					return recs;
//				}
//				catch (Exception e)
//				{
//					Console.WriteLine(e.Message);
//					throw e;
//				}
//				finally
//				{
//					if (!rdr.IsClosed)
//						rdr.Close();
//
//				}
//			}
//			protected string ZipReader(SqlDataReader rdr)
//			{
//				if (rdr["Zip"] != DBNull.Value)
//					return (string) rdr["Zip"];
//				return null;
//			}
//		}
//	}
//}