using System;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;

namespace DPI.Components
{
	public class StoreWrapper
	{
		public static EndOfDayDTO Store_EndOfDay(UOW uow, string storeCode, string storeNumber, DateTime date)
		{			
			return GetDayReceipts(uow, GetDaySummary(uow, 
				new EndOfDayDTO(new DateTime(date.Year, date.Month, date.Day), storeCode, storeNumber)));
		}
		static EndOfDayDTO GetDaySummary(UOW uow, EndOfDayDTO dto)
		{		
			SqlCommand cmd = CreateCmd(uow, "spEndofDaySummaryByStore");
			cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = dto.Date;
			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = dto.StoreCode;

			cmd.Parameters.Add("@LRev", SqlDbType.Money).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@ORev", SqlDbType.Money).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@LCom", SqlDbType.Money).Direction = ParameterDirection.Output;
			cmd.Parameters.Add("@OCom", SqlDbType.Money).Direction = ParameterDirection.Output;

			cmd.ExecuteNonQuery();
			
			dto.LocalRev = (decimal)(cmd.Parameters["@LRev"].Value);		
			dto.OtherRev = (decimal)(cmd.Parameters["@ORev"].Value);	
			dto.OtherRev = (decimal)(cmd.Parameters["@LCom"].Value);	
			dto.OtherRev = (decimal)(cmd.Parameters["@OCom"].Value);	

			return dto;
		}
		static EndOfDayDTO GetDayReceipts(UOW uow, EndOfDayDTO dto)
		{
			SqlCommand cmd = CreateCmd(uow, "spEndofDayReceiptsByStore");			
			cmd.Parameters.Add("@Date", SqlDbType.DateTime, 0).Value = dto.Date;
			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = dto.StoreCode;

			cmd.Parameters.Add("@CreditReceipts", SqlDbType.Money).Direction  = ParameterDirection.Output;
			cmd.Parameters.Add("@OtherReceipts", SqlDbType.Money).Direction = ParameterDirection.Output;

			cmd.ExecuteNonQuery();

			dto.CreditReceipts = 	(decimal)(cmd.Parameters["@CreditReceipts"].Value);		
			dto.OtherReceipts= 	(decimal)(cmd.Parameters["@OtherReceipts"].Value);		
			
			return dto;
		}
		static SqlCommand CreateCmd(UOW uow, string proc)
		{
			SqlCommand cmd = uow.Cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = proc;	
			return cmd;
		}
		public static DataSet spGetDayTotals(SqlConnection cn, IUser user, DateTime payDate)
		{			
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant.");
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spStores_GetDayTotals";		
			
			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
			cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = user.DisplayName;
			cmd.Parameters.Add("@PayDate", SqlDbType.DateTime, 0).Value = payDate;

			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spStores_GetDayTotals");
			return ds;
		}
		public static DataSet spGetOrderStatus(SqlConnection cn, IUser user, DateTime startDate, DateTime endDate)
		{			
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant."); 

			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spStores_GetOrderStatus";		 			               

			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
			cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = user.DisplayName;
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = endDate;		

			SqlDataAdapter da = new SqlDataAdapter(cmd);
		
			DataSet ds = new DataSet();
			da.Fill(ds, "spStores_GetOrderStatus");
			return ds;
		}		
		public static DataSet spGetCustomerList(SqlConnection cn, IUser user, DateTime startDate, DateTime endDate)
		{			
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant.");
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spStores_GetCustomerList";		 			               

			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
			cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = user.DisplayName;
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = endDate;		

			SqlDataAdapter da = new SqlDataAdapter(cmd);
		
			DataSet ds = new DataSet();
			da.Fill(ds, "spStores_GetCustomerList");
			return ds;
		}

		public static DataSet spGetCommission(SqlConnection cn, IUser user, DateTime startDate, DateTime endDate)
		{
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant.");
			try
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;			
				cmd.CommandText = "dbo.spStores_GetCommission_All";		 			               

				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
				cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = user.DisplayName;
				cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = startDate;
				cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = endDate;		

				SqlDataAdapter da = new SqlDataAdapter(cmd);
		
				DataSet ds = new DataSet();
				da.Fill(ds, "spStores_GetCommission");
				return ds;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		public static DataSet spGetWirelessCommission(SqlConnection cn, IUser user, DateTime startDate, DateTime endDate)
		{
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant.");
			try
			{
				SqlCommand cmd = cn.CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;			
				cmd.CommandText = "spStores_GetWirelessCommission";		 			               

				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
				cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = user.DisplayName;
				cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = startDate;
				cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = endDate;		

				SqlDataAdapter da = new SqlDataAdapter(cmd);
		
				DataSet ds = new DataSet();
				da.Fill(ds, "spStores_GetWirelessCommission");
				return ds;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}
		public static DataSet GetActiveDueDate(SqlConnection cn, IUser user, DateTime startDate, DateTime endDate)
		{			
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant.");
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spStores_GetActiveCustByDueDate";		 			               

			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
			cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 50).Value = user.DisplayName;
			cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 0).Value = endDate;		

			SqlDataAdapter da = new SqlDataAdapter(cmd);
		
			DataSet ds = new DataSet();
			da.Fill(ds, "spStores_GetActiveCustByDueDate");
			return ds;
		}
		public static DataSet spCertResults_Get_StoreCode(SqlConnection cn, IUser user)
		{			
			if (user.LoginStoreCode == null)
				throw new ArgumentException("User account not associated with a merchant.");
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spCertResults_Get_StoreCode";		
			
			cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = user.LoginStoreCode;
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spCertResults_Get_StoreCode");
			return ds;
		}
		public static DataSet spCertResults_Get_Corp(SqlConnection cn, int corpid)
		{			
			if (corpid == 0)
				throw new ArgumentException("Corp ID was not supplied");
			
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spCertResults_Get_Corp";		
			
			cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = corpid;
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spCertResults_Get_StoreCode"); //we need this table name for the dsCertResult.xsd data file and the report
			return ds;
		}
		public static DataSet spPendingOrderPaymentInfoByCorp(SqlConnection cn, int corpid)
		{			
			if (corpid == 0)
				throw new ArgumentException("Corp ID was not supplied");
			
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spPendingOrderPaymentInfoByCorp";		
			
			cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = corpid;
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spPendingOrderPaymentInfoByCorp"); //we need this table name for the dsPendingOrderPaymentInfoByCorp.xsd data file and the report
			return ds;
		}	
		public static DataSet spPendingTransactionsByStore(SqlConnection cn, int corpid, string storeNum, DateTime fromDate)
		{			
			if (corpid == 0)
				throw new ArgumentException("Corp ID was not supplied");

			if (storeNum == null)
				throw new ArgumentException("storNum was not supplied");
			
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spPendingTransactionsByStore";		
			
			cmd.Parameters.Add("@CorpId", SqlDbType.Int, 0).Value = corpid;
			cmd.Parameters.Add("@StoreNum", SqlDbType.VarChar, 10).Value = storeNum;
			cmd.Parameters.Add("@FromDate", SqlDbType.DateTime, 0).Value = fromDate;
			
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spPendingTransactionsByStore"); //we need this table name for the dsPendingOrderPaymentInfoByCorp.xsd data file and the report
			return ds;
		}
	}
}