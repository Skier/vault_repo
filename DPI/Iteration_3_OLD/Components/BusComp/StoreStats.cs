//using System;
//using System.Collections;
//using System.Data;
//using System.Text;
//using System.Data.SqlClient; 
//using System.Data.SqlTypes;
//using DPI.Interfaces;
//using System.Timers;
// 
//namespace DPI.Components
//{
//
//	public class StoreStats : IStoreStats
//	{
//		string           storeCode;
//		internal string  storeNumber;
//		internal int	 corpId;
//		
//		internal int     activeCust;
//		internal int     mDT_NewCust; 
//		internal int     activeCustRank;
//		internal int     mtdNewCustRank;
//
//		internal decimal localRevenue;
//		internal int     revenueRank;
//		internal decimal ldRevenue;
//		internal decimal wirelessRevenue;
//		internal int     totalStores;
//		
//		/*		Properties		*/
//		public string  StoreCode       { get { return storeCode;      }}
//		public string  StoreNumber     { get { return storeNumber;    } set {storeNumber = value;}}
//		public int     CorpId          { get { return corpId;         } set {corpId = value;}}
//		public int     ActiveCust      { get { return activeCust;     } set {activeCust = value;}}
//		public int     ActiveCustRank  { get { return activeCustRank; } set {activeCustRank = value;}}
//		public int     MDT_NewCust     { get { return mDT_NewCust;    } set {mDT_NewCust = value;}}
//		public int     MDT_NewCustRank { get { return mtdNewCustRank; } set {mtdNewCustRank = value;}}
//		public decimal Revenue 	       { get { return localRevenue + ldRevenue + wirelessRevenue; }}
//		public int     TotalStores     { get { return totalStores; } set {totalStores = value;}}
//		public decimal LocalRevenue    { get { return localRevenue; } set {localRevenue = value;}}
//		public decimal LdRevenue       { get { return ldRevenue; } set	{ldRevenue = value;}}
//		public decimal WirelessRevenue { get { return wirelessRevenue; } set {wirelessRevenue = value;}}
//		public int     RevenueRank     
//		{ 
//			get { return revenueRank; } 
//			set { revenueRank = value; } 
//		}
//		
//		/*	Constructors	*/
//		StoreStats() {}
//		public StoreStats(string storeCode) 
//		{
//			this.storeCode = storeCode;
//		}
//		/*		Internal methods		*/
//		public static StoreStats[] GetNewCust(UOW uow, DateTime fromDate, DateTime endDate)
//		{
//			return new SQL().getMTDsales(uow, fromDate, endDate);
//		}
//		public static StoreStats[] GetRevenues(UOW uow, DateTime fromDate, DateTime endDate)
//		{
//			return new SQL().getRevenue(uow, fromDate, endDate);
//		}
//		public static StoreStats[] GetActiveCust(UOW uow, DateTime fromDate)
//		{
//			 return new SQL().getActiveCust(uow);
//		}
//		public static StoreStats[] GetWirelessRevenue(UOW uow, DateTime fromDate, DateTime endDate)
//		{
//			return new SQL().getWirelessRevenue(uow,fromDate, endDate);
//		}
//
//		public static void populateRankTable(UOW uow)
//		{
//			DateTime d = DateTime.Now.AddDays(1);
//
//			SqlCommand cmd = uow.Cn.CreateCommand();
//			
//			//cmd.CommandType = CommandType.StoredProcedure;
//			cmd.CommandText = "SELECT DISTINCT CorpId FROM StoreStats1";
//			SqlDataReader rdr = cmd.ExecuteReader(); 
//			while (rdr.Read())
//			{
//				SqlCommand cmd2 = uow.Cn.CreateCommand();
//				string sql = "SELECT top 25* FROM StoreStats1 where corpId = " + rdr["CorpId"];
//				sql = sql + " and ActiveCustCnt >0 order by ActiveCustCnt desc";					
//				cmd2.CommandText = sql;
//				SqlDataReader rdr2 = cmd2.ExecuteReader(); 
//				int iRank=0;
//				int preCnt=0;
//				while(rdr2.Read())
//				{ 
//					if (iRank==0 && preCnt==0)
//					{
//						iRank=1;
//						preCnt=rdr2.GetInt32(3);
//					}
//					else 
//					{
//						if(preCnt != rdr2.GetInt32(3))
//						{
//								iRank++;
//							preCnt=rdr2.GetInt32(3);
//						}
//					}
//					SqlCommand cmd3 = uow.Cn.CreateCommand();			
//					cmd3.CommandText = "spInsertIntoCorpLeaders";
//					cmd3.Parameters.Add("@StoreCode", SqlDbType.Char).Value = rdr2["StoreCode"];
//					cmd3.Parameters.Add("@StoreNumber", SqlDbType.VarChar).Value = rdr2["StoreNumber"];
//					cmd3.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = rdr2["CorpId"];
//					cmd3.Parameters.Add("@StatDate", SqlDbType.DateTime, 0).Value = d;
//					cmd3.Parameters.Add("@@StatType", SqlDbType.VarChar).Value = "ActiveCust";
//					cmd3.Parameters.Add("@Rank", SqlDbType.Int, 0).Value = iRank;
//					cmd3.Parameters.Add("@RankValue", SqlDbType.Char).Value = rdr2["ActiveCustCnt"];
//					cmd3.ExecuteNonQuery();
//				}
//			}			
//		}
//		class SQL
//		{
//			public StoreStats[] getActiveCust(UOW uow)
//			{
//				SqlCommand cmd = getCommand(uow);
//				cmd.CommandText = "spCustData_Get_ActiveByStore2";
//				return execReader(cmd);
//			}
//			public StoreStats[] getMTDsales(UOW uow, DateTime startDate, DateTime endDate)
//			{
//				const int VERIFONE_TRAN_TYPE = 2;
//				const decimal MIN_AMT = 35m;
//				const int backDays = 21; 
//	            DateTime origDate = startDate.AddDays(-backDays);
//
//				SqlCommand cmd = getCommand(uow);
//
//				cmd.CommandText = "spCustData_Get_NewMTD3";
//				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = startDate;
//				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = endDate;
//				cmd.Parameters.Add("@MinAmt", SqlDbType.Money, 0).Value = MIN_AMT;
//				cmd.Parameters.Add("@TranType", SqlDbType.Int, 0).Value = VERIFONE_TRAN_TYPE;
//				cmd.Parameters.Add("@OrigDate", SqlDbType.DateTime, 0).Value = origDate;
//
//				//@OrigDate
//
//				return execReader3(cmd);
//			}
//			public StoreStats[] getRevenue(UOW uow, DateTime startDate, DateTime endDate)
//			{
//				SqlCommand cmd = getCommand(uow);
//				cmd.CommandTimeout = 600;
//				
//				cmd.CommandText = "spCustData_Get_RevenueByStore2";
//				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = startDate;
//				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = endDate;
//
//				return execReader2(cmd);
//			}
//			public StoreStats[] getWirelessRevenue(UOW uow, DateTime startDate, DateTime endDate)
//			{
//				SqlCommand cmd = getCommand(uow);
//				
//				cmd.CommandText = "spCustData_Get_WirelessRevenueByStore";
//				cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = startDate;
//				cmd.Parameters.Add("@ToDate", SqlDbType.DateTime, 0).Value = endDate;
//
//				return execReader4(cmd);
//			}
//
//			/*        Implementation        */
//			SqlCommand getCommand(UOW uow)
//			{
//				SqlCommand cmd = uow.Cn.CreateCommand();
//				cmd.Transaction = uow.Tran; 
//				cmd.CommandType = CommandType.StoredProcedure;
//				return cmd;
//			}		
//
//			StoreStats[] execReader2(SqlCommand cmd)
//			{
//				ArrayList ar = new ArrayList();
//				SqlDataReader rdr = null;
//
//				rdr = cmd.ExecuteReader();
//
//				try
//				{
//					while(rdr.Read())
//						ar.Add(reader2(rdr));
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
//			StoreStats[] execReader4(SqlCommand cmd)
//			{
//				ArrayList ar = new ArrayList();
//				SqlDataReader rdr = cmd.ExecuteReader();
//
//				try
//				{
//					while(rdr.Read())
//						ar.Add(reader4(rdr));
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
//			StoreStats[] execReader3(SqlCommand cmd)
//			{
//				ArrayList ar = new ArrayList();
//				SqlDataReader rdr = cmd.ExecuteReader();
//
//				try
//				{
//					while(rdr.Read())
//						ar.Add(reader3(rdr));
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
//
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
//			IStoreStats reader4(SqlDataReader rdr)
//			{
//				StoreStats stats = new StoreStats();
//
//				if ( rdr["StoreCode"] != DBNull.Value)
//					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
//				
//				if (rdr["StoreNumber"] != DBNull.Value)
//					stats.storeNumber = (string) rdr["StoreNumber"];
//
//				if (rdr["CorpId"] != DBNull.Value)
//					stats.corpId = (int) rdr["CorpId"];
//
//				if (rdr["WirelessRev"] != DBNull.Value)
//					stats.wirelessRevenue = (decimal) rdr["WirelessRev"];
//
//				return stats; 
//			}
//			IStoreStats reader3(SqlDataReader rdr)
//			{
//				StoreStats stats = new StoreStats();
//
//				if ( rdr["StoreCode"] != DBNull.Value)
//					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
//				
//				if (rdr["StoreNumber"] != DBNull.Value)
//					stats.storeNumber = (string) rdr["StoreNumber"];
//
//				if (rdr["CorpId"] != DBNull.Value)
//					stats.corpId = (int) rdr["CorpId"];
//
//				if (rdr["NewSales"] != DBNull.Value)
//					stats.mDT_NewCust = (int) rdr["NewSales"];
//
//				return stats; 
//			}
//			IStoreStats reader2(SqlDataReader rdr)
//			{
//				StoreStats stats = new StoreStats();
//
//				if ( rdr["StoreCode"] != DBNull.Value)
//					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
//				
//				if (rdr["StoreNumber"] != DBNull.Value)
//					stats.storeNumber = (string) rdr["StoreNumber"];
//
//				if (rdr["CorpId"] != DBNull.Value)
//					stats.corpId = (int) rdr["CorpId"];
//
//				if (rdr["LocAmt"] != DBNull.Value)
//					stats.localRevenue = (decimal) rdr["LocAmt"];
//
//				if (rdr["LDAmt"] != DBNull.Value)
//					stats.ldRevenue = (decimal) rdr["LDAmt"];
//
//			//	stats.localRevenue = 0m;
//			//	stats.ldRevenue = 0m;
//
//				return stats; 
//			}
//
//			IStoreStats reader(SqlDataReader rdr)
//			{
//				StoreStats stats = new StoreStats();
//
//				if ( rdr["StoreCode"] != DBNull.Value)
//					stats.storeCode = ((string) rdr["StoreCode"]).Trim();
//				
//				if (rdr["StoreNumber"] != DBNull.Value)
//					stats.storeNumber = (string) rdr["StoreNumber"];
//
//				if (rdr["CorpId"] != DBNull.Value)
//					stats.corpId = (int) rdr["CorpId"];
//				
//				if (rdr["Cnt"] != DBNull.Value)
//					stats.activeCust = (int) rdr["Cnt"];
//
//				return stats; 
//			}
//		}
//	}
//}