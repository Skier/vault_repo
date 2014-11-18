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
	public class StoreStats2 : DomainObj, IStoreStats	
	{
		/*        Data        */
		static string iName = "StoreStats2";
		int id; 
		DateTime statDate;
		int corpId;
		string storeCode;
		string storeNumber;
		int activeCustCnt;
		int mDT_NewCustCnt;
		int activeCustRank;
		int mDT_NewCustRank;
		decimal revenue;
		decimal lDRevenue;
		decimal wirelessRev;
		int revenueRank;
		int	actualCorpId;
     
		/*        Properties        */
		public override IDomKey IKey 
		{
			get { return new Key(iName, id.ToString()); }
		}
		public int Id
		{
			get { return id; }
		}
		public DateTime StatDate
		{
			get { return statDate; }
			set
			{
				setState();
				statDate = value;
			}
		}
		public int CorpId
		{
			get { return corpId; }
			set
			{
				setState();
				corpId = value;
			}
		}
		public string StoreCode
		{
			get { return storeCode; }
			set
			{
				setState();
				storeCode = value;
			}
		}
		public string StoreNumber
		{
			get { return storeNumber; }
			set
			{
				setState();
				storeNumber = value;
			}
		}
		public int ActiveCust
		{
			get { return activeCustCnt; }
			set
			{
				setState();
				activeCustCnt = value;
			}
		}
		public int MDT_NewCustCnt
		{
			get { return mDT_NewCustCnt; }
			set
			{
				setState();
				mDT_NewCustCnt = value;
			}
		}
		public decimal Revenue
		{
			get { return revenue + lDRevenue + wirelessRev; }
			
		}
		public decimal LocalRevenue	
		{ 
			get { return revenue; }
			set
			{
				setState();
				revenue = Decimal.Round(value, 2);
			}
		}
		public int RevenueRank
		{ 
			get { return revenueRank; } 
			set { revenueRank = value; } 
		}
		public decimal LDRevenue
		{
			get { return lDRevenue; }
			set
			{
				setState();
				lDRevenue = Decimal.Round(value, 2);
			}
		}
		public decimal WirelessRev
		{
			get { return wirelessRev; }
			set
			{
				setState();
				wirelessRev = Decimal.Round(value, 2);
			}
		}
        
		public int ActiveCustRank
		{
			get { return activeCustRank;}
			set { activeCustRank = value;}
		}

		public decimal LdRevenue
		{
			get { return lDRevenue; }
			set { lDRevenue = value; }
		}

		public int MDT_NewCust
		{
			get { return mDT_NewCustCnt; }
			set { mDT_NewCustCnt = value; }
		}

		public int MDT_NewCustRank
		{
			get	{return mDT_NewCustRank;}
			set { mDT_NewCustRank = value; }
		}

		public decimal WirelessRevenue 
		{
			get { return wirelessRev; }
			set { wirelessRev = value; }
		}

		public int	ActualCorpId
		{
			get { return actualCorpId;	}
			set { actualCorpId = value;	}
		}

		/*        Constructors			*/
		public StoreStats2()
		{
			sql = new StoreStatsSQL();
			id = random.Next(Int32.MinValue, -1);
			rowState = RowState.New;
		}
		public StoreStats2(UOW uow) : this()
		{
			if(uow == null)
				throw new ArgumentException("Unit Of Work is required", "Unit Of Work");
            
			if(uow.Imap == null)
				throw new ArgumentException("IdentityMap is required", "Identity Map");
            
			this.uow = uow;
			this.uow.Imap.add(this);
		}
		public StoreStats2(string storeCode, int corpId, string storeNumber, int actualCorpId) : this()
		{
			this.corpId = corpId;
			this.storeCode = storeCode;
			this.storeNumber = storeNumber; 
			this.actualCorpId = actualCorpId;
		}

		public StoreStats2(UOW uow, IStoreStats sstat) : this(uow)
		{
			this.activeCustCnt = sstat.ActiveCust;
			this.statDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			this.corpId = sstat.CorpId;
			this.storeCode = sstat.StoreCode;
			this.storeNumber = sstat.StoreNumber;
			this.activeCustCnt = sstat.ActiveCust;
			this.mDT_NewCustCnt= sstat.MDT_NewCust;
			this.revenue = sstat.Revenue;
			this.lDRevenue = sstat.LdRevenue;
			this.wirelessRev = sstat.WirelessRevenue;
			this.activeCustRank = sstat.ActiveCustRank;
			this.mDT_NewCustRank = sstat.MDT_NewCustRank;
			this.revenueRank = sstat.RevenueRank;
			this.actualCorpId = sstat.ActualCorpId;
			//int parent;
			//bool useParent;

		}
		/*        Methods        */
		protected override SqlGateway loadSql()
		{
			return new StoreStatsSQL();
		}
		public override void checkExists()
		{
			if ((Id < 1))
				throw new ArgumentException("Valid row is required for update and delete");
		}
		/*		Static methods		*/
		public static void DeleteDate(UOW uow, DateTime statDate)
		{
			new StoreStatsSQL().DeleteDate(uow, statDate);
		}
		public static StoreStats2 find(UOW uow, int id)
		{
			if (uow.Imap.keyExists(StoreStats2.getKey(id)))
				return (StoreStats2)uow.Imap.find(StoreStats2.getKey(id));
            
			StoreStats2 cls = new StoreStats2();
			cls.uow = uow;
			cls.id = id;
			cls = (StoreStats2)DomainObj.addToIMap(uow, getOne(((StoreStatsSQL)cls.Sql).getKey(cls)));
			cls.uow = uow;
            
			return cls;
		}
		public static StoreStats2[] getAll(UOW uow)
		{
			StoreStats2[] objs = (StoreStats2[])DomainObj.addToIMap(uow, (new StoreStatsSQL()).getAll(uow));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static StoreStats2[] getAll_Date(UOW uow, DateTime statDate)
		{
			StoreStats2[] objs = (StoreStats2[])DomainObj.addToIMap(uow, (new StoreStatsSQL()).getAll_Date(uow, statDate));
			for (int i = 0; i < objs.Length; i++)
				objs[i].uow = uow;
			return objs;
		}
		public static Key getKey(int id)
		{
			return new Key(iName, id.ToString());
		}
		public static int GetCount(UOW uow, DateTime dt)
		{
			return new StoreStatsSQL().GetCount(uow, dt);
		}
		public static int GetTotalCount(UOW uow)
		{
			return new StoreStatsSQL().GetTotalCount(uow);
		}
		/*		Internal methods		*/
		public static IStoreStats[] GetNewCust(UOW uow, DateTime fromDate, DateTime endDate)
		{
			return new StoreStatsSQL().getMTDsales(uow, fromDate, endDate);
		}
		public static IStoreStats[] GetRevenues(UOW uow, DateTime fromDate, DateTime endDate)
		{
			return new StoreStatsSQL().getRevenue(uow, fromDate, endDate);
		}
		public static IStoreStats[] GetActiveCust(UOW uow)
		{
			return new StoreStatsSQL().getActiveCust(uow);
		}
		public static IStoreStats[] GetWirelessRevenue(UOW uow, DateTime fromDate, DateTime endDate)
		{
			return new StoreStatsSQL().getWirelessRevenue(uow,fromDate, endDate);
		}
		
		/*		Implementation		*/
		static StoreStats2 getOne(StoreStats2[] acls)
		{
			if (acls.Length == 1)
				return acls[0];
            
			if (acls.Length == 0)
				throw new ArgumentException("Row not found");
            
			throw new ArgumentException("More than one row found");
		}
		static void copyAttrs(StoreStats2 src, StoreStats2 tar)
		{
			if (tar == null)
				throw new ArgumentNullException("Target object must not be null");
            
			if (src == null)
				throw new ArgumentNullException("Source object must not be null");
            
			tar.id = src.id;
			tar.statDate = src.statDate;
			tar.corpId = src.corpId;
			tar.storeCode = src.storeCode;
			tar.storeNumber = src.storeNumber;
			tar.activeCustCnt = src.activeCustCnt;
			tar.mDT_NewCustCnt = src.mDT_NewCustCnt;
			tar.revenue = src.revenue;
			tar.lDRevenue = src.lDRevenue;
			tar.wirelessRev = src.wirelessRev;
			tar.rowState = src.rowState;
		}
 
		/*		SQL		*/
		[Serializable]
        class StoreStatsSQL : SqlGateway
		{			
			public StoreStats2[] getKey(StoreStats2 rec)
			{
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spStoreStats_Get_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
				return convert(execReader(cmd));
			}
			public override void insert(DomainObj obj)
			{
				StoreStats2 rec = (StoreStats2)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spStoreStats_Ins";
				setParam(cmd, rec);
                
				cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
				execScalar(cmd);
				rec.id = (int)cmd.Parameters["@Id"].Value;
				rec.rowState = RowState.Clean;
			}
			public override void delete(DomainObj obj)
			{
				StoreStats2 rec = (StoreStats2)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spStoreStats_Del_Id";
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
                
				execScalar(cmd);
				rec.rowState = RowState.Deleted;
			}
			// [dbo].[spStoreStats_Del_Date]
			public void DeleteDate(UOW uow, DateTime statDate)
			{
				
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spStoreStats_Del_Date";
				cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = new DateTime(statDate.Year, statDate.Month, statDate.Day);                
				execScalar(cmd);
				
			}
			public int GetCount(UOW uow, DateTime dt)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spStoreStats_Get_Date_Cnt";
				cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = dt;                
				return	(int)cmd.ExecuteScalar();
			}
			public int GetTotalCount(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spStoreStats_Get_Total_Cnt";				              
				return	(int)cmd.ExecuteScalar();
			}
			public override void update(DomainObj obj)
			{
				StoreStats2 rec = (StoreStats2)obj;
				SqlCommand cmd = getCommand(rec);
				cmd.CommandText = "spStoreStats_Upd";
				setParam(cmd, rec);
                
				execScalar(cmd); 
				rec.rowState = RowState.Clean;
			}
			public StoreStats2[] getAll(UOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spStoreStats_Get_All";			
				return convert(execReader(cmd));
			}
			public StoreStats2[] getAll_Date(UOW uow, DateTime statDate)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandText = "spStoreStats_Get_All_Date";

				if (statDate == DateTime.MinValue)
					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = statDate;

				return convert(execReader(cmd));
			}
//			public StoreStats2[] getActive(UOW uow, DateTime from, DateTime to)
//			{
//				extra = true;
//				SqlCommand cmd = makeCommand(uow);
//				cmd.CommandText = "spStoreStats_Get_All";
//				
//				if (from == DateTime.MinValue)
//					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
//				else
//					cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = from;
//
//				if (to == DateTime.MinValue)
//					cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
//				else
//					cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = to;
//
//				return convert(execReader(cmd));
//			}
//           
			public IStoreStats[] getActiveCust(IUOW uow)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandTimeout = 800;

				cmd.CommandText = "spCustData_Get_ActiveByStore2";
				return execReader5(cmd);
			}
			public IStoreStats[] getMTDsales(IUOW uow, DateTime startDate, DateTime endDate)
			{
				const int VERIFONE_TRAN_TYPE = 2;
				const decimal MIN_AMT = 35m;
				const int backDays = 21; 
				DateTime origDate = startDate.AddDays(-backDays);
			
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandTimeout = 800;
			
				cmd.CommandText = "spCustData_Get_NewMTD3";
				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = startDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = endDate;
				cmd.Parameters.Add("@MinAmt", SqlDbType.Money, 0).Value = MIN_AMT;
				cmd.Parameters.Add("@TranType", SqlDbType.Int, 0).Value = VERIFONE_TRAN_TYPE;
				cmd.Parameters.Add("@OrigDate", SqlDbType.DateTime, 0).Value = origDate;
			
				//@OrigDate
			
				return execReader3(cmd);
			}
			public IStoreStats[] getRevenue(IUOW uow, DateTime startDate, DateTime endDate)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandTimeout = 800;
							
				cmd.CommandText = "spCustData_Get_RevenueByStore2";
				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = startDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = endDate;
			
				return execReader2(cmd);
			}
			public IStoreStats[] getWirelessRevenue(IUOW uow, DateTime startDate, DateTime endDate)
			{
				SqlCommand cmd = makeCommand(uow);
				cmd.CommandTimeout = 800;
							
				cmd.CommandText = "spCustData_Get_WirelessRevenueByStore";
				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = startDate;
				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = endDate;
			
				return execReader4(cmd);
			}
			IStoreStats[] execReader5(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = null;
			
				rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader5(rdr));
			
					IStoreStats[] stats = new IStoreStats[ar.Count];
					ar.CopyTo(stats);
					return stats;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}
						
			/*        Implementation        */
			void setParam(SqlCommand cmd, StoreStats2 rec)
			{
				cmd.Parameters.Add("@Id", SqlDbType.Int, 0).Value = rec.id;
 
				if (rec.statDate == DateTime.MinValue)
					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = rec.statDate;
                
				// Numeric, nullable foreign key treatment:
				if (rec.CorpId == 0)
					cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = DBNull.Value;
				else
					cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = rec.corpId;
 
				cmd.Parameters.Add("@StoreCode", SqlDbType.VarChar, 10).Value = rec.storeCode;
 
				if (rec.storeNumber == null)
					cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
				else
				{
					if (rec.StoreNumber.Length == 0)
						cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@StoreNumber", SqlDbType.VarChar, 10).Value = rec.storeNumber;
				}
				cmd.Parameters.Add("@ActiveCustCnt", SqlDbType.Int, 0).Value = rec.activeCustCnt;
				cmd.Parameters.Add("@MDT_NewCustCnt", SqlDbType.Int, 0).Value = rec.mDT_NewCustCnt;
				cmd.Parameters.Add("@Revenue", SqlDbType.Decimal, 0).Value = rec.revenue;
				cmd.Parameters.Add("@LDRevenue", SqlDbType.Decimal, 0).Value = rec.lDRevenue;
				cmd.Parameters.Add("@WirelessRev", SqlDbType.Decimal, 0).Value = rec.wirelessRev;

				cmd.Parameters.Add("@ActiveCustRank", SqlDbType.Int, 0).Value = rec.activeCustRank;
				cmd.Parameters.Add("@MDT_NewCustRank", SqlDbType.Int, 0).Value = rec.mDT_NewCustRank;
				cmd.Parameters.Add("@RevenueRank", SqlDbType.Int, 0).Value = rec.revenueRank;

				cmd.Parameters.Add("@ActualCorpId", SqlDbType.Int, 0).Value = rec.actualCorpId;
			}
			protected override DomainObj reader(SqlDataReader rdr)
			{
				StoreStats2 rec = new StoreStats2();
                
				if (rdr["Id"] != DBNull.Value)
					rec.id = (int) rdr["Id"];
 
				if (rdr["StatDate"] != DBNull.Value)
					rec.statDate = (DateTime) rdr["StatDate"];
 
				if (rdr["CorpId"] != DBNull.Value)
					rec.corpId = (int) rdr["CorpId"];
 
				if (rdr["StoreCode"] != DBNull.Value)
					rec.storeCode = (string) rdr["StoreCode"];
 
				if (rdr["StoreNumber"] != DBNull.Value)
					rec.storeNumber = (string) rdr["StoreNumber"];
 
				if (rdr["ActiveCustCnt"] != DBNull.Value)
					rec.activeCustCnt = (int) rdr["ActiveCustCnt"];
 
				if (rdr["MDT_NewCustCnt"] != DBNull.Value)
					rec.mDT_NewCustCnt = (int) rdr["MDT_NewCustCnt"];
 
				if (rdr["Revenue"] != DBNull.Value)
					rec.revenue = Decimal.Round((decimal)rdr["Revenue"], 2);
 
				if (rdr["LDRevenue"] != DBNull.Value)
					rec.lDRevenue = Decimal.Round((decimal)rdr["LDRevenue"], 2);
 
				if (rdr["WirelessRev"] != DBNull.Value)
					rec.wirelessRev = Decimal.Round((decimal)rdr["WirelessRev"], 2);

				if (rdr["ActiveCustRank"] != DBNull.Value)
					rec.activeCustRank = (int) rdr["ActiveCustRank"];
 
				if (rdr["MDT_NewCustRank"] != DBNull.Value)
					rec.mDT_NewCustRank = (int) rdr["MDT_NewCustRank"];

				if (rdr["RevenueRank"] != DBNull.Value)
					rec.revenueRank = (int) rdr["RevenueRank"];
                
				rec.rowState = RowState.Clean;				
	
//				if (!extra)
//					return rec;
//
//				if (rdr["ParentId"] != DBNull.Value)
//					rec.parent = (int)rdr["ParentId"];
//
//				if (rdr["UsePapentForStoreStats"] != DBNull.Value)
//					rec.useParent = (bool)rdr["UsePapentForStoreStats"];
//	            
				return rec;
			}
			StoreStats2[] convert(DomainObj[] objs)
			{
				StoreStats2[] acls  = new StoreStats2[objs.Length];
				objs.CopyTo(acls, 0);
				return  acls;
			}
			// ***********************************
			IStoreStats[] execReader2(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = null;
			
				rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader2(rdr));
			
					IStoreStats[] stats = new IStoreStats[ar.Count];
					ar.CopyTo(stats);
					return stats;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}	
			IStoreStats[] execReader4(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader4(rdr));
			
					IStoreStats[] stats = new IStoreStats[ar.Count];
					ar.CopyTo(stats);
					return stats;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}	
			IStoreStats[] execReader3(SqlCommand cmd)
			{
				ArrayList ar = new ArrayList();
				SqlDataReader rdr = cmd.ExecuteReader();
			
				try
				{
					while(rdr.Read())
						ar.Add(reader3(rdr));
			
					IStoreStats[] stats = new IStoreStats[ar.Count];
					ar.CopyTo(stats);
					return stats;
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
			
					if (cmd.Transaction != null)
						if (cmd.Transaction.Connection != null)
							cmd.Transaction.Rollback();
							
					throw e;
				}
				finally
				{
					if (!rdr.IsClosed)
						rdr.Close();
				}
			}	
			
//			StoreStats[] execReader(SqlCommand cmd)
//			{
//				ArrayList ar = new ArrayList();
//				SqlDataReader rdr = cmd.ExecuteReader();
//			
//				try
//				{
//					while(rdr.Read())
//						ar.Add(reader(rdr));
//			
//					StoreStats[] stats = new StoreStats[ar.Count];
//					ar.CopyTo(stats);
//					return stats;
//				}
//				catch (Exception e)
//				{
//					Console.WriteLine(e.Message);
//			
//					if (cmd.Transaction != null)
//						if (cmd.Transaction.Connection != null)
//							cmd.Transaction.Rollback();
//							
//					throw e;
//				}
//				finally
//				{
//					if (!rdr.IsClosed)
//						rdr.Close();
//				}
//			}	
			IStoreStats reader4(SqlDataReader rdr)
			{
				StoreStats2 stats = new StoreStats2();
			
				if ( rdr["StoreCode"] != DBNull.Value)
					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
							
				if (rdr["StoreNumber"] != DBNull.Value)
					stats.storeNumber = (string) rdr["StoreNumber"];
			
				if (rdr["CorpId"] != DBNull.Value)
					stats.corpId = (int) rdr["CorpId"];
			
				if (rdr["WirelessRev"] != DBNull.Value)
					stats.wirelessRev = (decimal) rdr["WirelessRev"];
			
				return stats; 
			}
			IStoreStats reader3(SqlDataReader rdr)
			{
				StoreStats2 stats = new StoreStats2();
			
				if ( rdr["StoreCode"] != DBNull.Value)
					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
							
				if (rdr["StoreNumber"] != DBNull.Value)
					stats.storeNumber = (string) rdr["StoreNumber"];
			
				if (rdr["CorpId"] != DBNull.Value)
					stats.corpId = (int) rdr["CorpId"];
			
				if (rdr["NewSales"] != DBNull.Value)
					stats.mDT_NewCustCnt = (int) rdr["NewSales"];
			
				return stats; 
			}
			IStoreStats reader2(SqlDataReader rdr)
			{
				StoreStats2 stats = new StoreStats2();
			
				if ( rdr["StoreCode"] != DBNull.Value)
					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
							
				if (rdr["StoreNumber"] != DBNull.Value)
					stats.storeNumber = (string) rdr["StoreNumber"];
			
				if (rdr["CorpId"] != DBNull.Value)
					stats.corpId = (int) rdr["CorpId"];
			
				if (rdr["LocAmt"] != DBNull.Value)
					stats.revenue = (decimal) rdr["LocAmt"];
			
				if (rdr["LDAmt"] != DBNull.Value)
					stats.lDRevenue = (decimal) rdr["LDAmt"];
			
				return stats; 
			}
			IStoreStats reader5(SqlDataReader rdr)
			{
				StoreStats2 stats = new StoreStats2();
			
				if ( rdr["StoreCode"] != DBNull.Value)
					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
							
				if (rdr["StoreNumber"] != DBNull.Value)
					stats.storeNumber = (string) rdr["StoreNumber"];
			
				if (rdr["CorpId"] != DBNull.Value)
					stats.corpId = (int) rdr["CorpId"];
							
				if (rdr["Cnt"] != DBNull.Value)
					stats.activeCustCnt = (int) rdr["Cnt"];
			
				return stats; 
			}
		}

	}
}
