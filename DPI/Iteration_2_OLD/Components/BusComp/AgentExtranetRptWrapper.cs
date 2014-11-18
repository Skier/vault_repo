using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DPI.Interfaces;


namespace DPI.Components
{
	public class AgentExtranetRptWrapper
	{
		public static DataSet GetSalesRptByStateData(SqlConnection cn, int corpId, string state, string dma, DateTime startDate, DateTime endDate, string tranType)
		{	
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spWeb_LocalSalesByDMA";		
			
			cmd.Parameters.Add("@CorpID_IN", SqlDbType.Char, 5).Value = corpId.ToString();
			cmd.Parameters.Add("@DMA_IN", SqlDbType.Char, 5).Value = dma;
			cmd.Parameters.Add("@EndDate_IN", SqlDbType.DateTime, 0).Value = endDate;
			cmd.Parameters.Add("@StartDate_IN", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@State_IN", SqlDbType.Char, 5).Value = state;
			cmd.Parameters.Add("@TransactionTypeID_IN", SqlDbType.VarChar, 5).Value = tranType;

			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spWeb_LocalSalesByDMA");
			return ds;
		}
		public static DataSet GetSalesRptByStoreData(SqlConnection cn, int corpId, string state, string dma, DateTime startDate, DateTime endDate, string tranType)
		{	
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spWeb_Rpt_LocalSalesByStore";		
			
			cmd.Parameters.Add("@AdvertisementAmount_IN", SqlDbType.Money, 0).Value = 0;
			cmd.Parameters.Add("@CorpID_IN", SqlDbType.Char, 5).Value = corpId.ToString();
			cmd.Parameters.Add("@DMA_IN", SqlDbType.Char, 5).Value = dma;
			cmd.Parameters.Add("@EndDate_IN", SqlDbType.DateTime, 0).Value = endDate;
			cmd.Parameters.Add("@StartDate_IN", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@State_IN", SqlDbType.Char, 5).Value = state;
			cmd.Parameters.Add("@TransactionTypeID_IN", SqlDbType.VarChar, 5).Value = tranType;

			SqlDataAdapter da = new SqlDataAdapter(cmd);
			
			DataSet ds = new DataSet();
			da.Fill(ds, "spWeb_Rpt_LocalSalesByStore");
			return ds;
		}
		public static IDropDownListItem[] GetDMA()
		{
			SqlConnection cn = Conn.GetConn();			
			
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spWeb_DMADesc_GetAll";
			
			return execDMAReader(cmd);
		}
		public static IDropDownListItem[] GetDMA(string state)
		{
			SqlConnection cn = Conn.GetConn();			
			
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spWeb_DMADesc_GetByState";

			cmd.Parameters.Add("@DMAState", SqlDbType.Char, 2).Value = state;
			
			return execDMAReader(cmd);
		}		
		public static string GetSalesRptByStoreData(string header, int corpId, string state, string dma, DateTime startDate, DateTime endDate, string tranType)
		{	
			SqlConnection cn = Conn.GetConn();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spWeb_Rpt_LocalSalesByStore";		
			
			cmd.Parameters.Add("@AdvertisementAmount_IN", SqlDbType.Money, 0).Value = 0;
			cmd.Parameters.Add("@CorpID_IN", SqlDbType.Char, 5).Value = corpId.ToString();
			cmd.Parameters.Add("@DMA_IN", SqlDbType.Char, 5).Value = dma;
			cmd.Parameters.Add("@EndDate_IN", SqlDbType.DateTime, 0).Value = endDate;
			cmd.Parameters.Add("@StartDate_IN", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@State_IN", SqlDbType.Char, 5).Value = state;
			cmd.Parameters.Add("@TransactionTypeID_IN", SqlDbType.VarChar, 5).Value = tranType;

			return CreateComaSepString1(cmd, header);
		}
		public static string GetSalesRptByStateData(string header, int corpId, string state, string dma, DateTime startDate, DateTime endDate, string tranType)
		{	
			SqlConnection cn = Conn.GetConn();
			SqlCommand cmd = cn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;			
			cmd.CommandText = "spWeb_LocalSalesByDMA";		
			
			cmd.Parameters.Add("@CorpID_IN", SqlDbType.Char, 5).Value = corpId.ToString();
			cmd.Parameters.Add("@DMA_IN", SqlDbType.Char, 5).Value = dma;
			cmd.Parameters.Add("@EndDate_IN", SqlDbType.DateTime, 0).Value = endDate;
			cmd.Parameters.Add("@StartDate_IN", SqlDbType.DateTime, 0).Value = startDate;
			cmd.Parameters.Add("@State_IN", SqlDbType.Char, 5).Value = state;
			cmd.Parameters.Add("@TransactionTypeID_IN", SqlDbType.VarChar, 5).Value = tranType;

			return CreateComaSepString(cmd, header);
		}
		static IDropDownListItem[] execDMAReader(SqlCommand cmd)
		{
			ArrayList ar = new ArrayList();
			SqlDataReader rdr = cmd.ExecuteReader();
			
			try
			{
				ar.Add(new DropDownListItem("All", "%"));

				while(rdr.Read())
					ar.Add(DMAreader(rdr));
			
				IDropDownListItem[] items = new DropDownListItem[ar.Count];
				ar.CopyTo(items);
				return items;
			}
			finally
			{
				if (!rdr.IsClosed)
					rdr.Close();
			}
		}
		static IDropDownListItem DMAreader(SqlDataReader rdr)
		{
			IDropDownListItem rec = new DropDownListItem();
                
			if (rdr["DMA"] != DBNull.Value)
				rec.DDLValue = ((int)rdr["DMA"]).ToString();
 
			if (rdr["DMADesc"] != DBNull.Value)
				rec.DDLText = (string) rdr["DMADesc"];

			return rec;
		}

		static string SalesByDMAReader(SqlDataReader rdr)
		{
			StringBuilder sb = new StringBuilder();
			
			if (rdr["StateName"] != DBNull.Value)
				sb.Append((string)rdr["StateName"]);

			sb.Append(",");
			if (rdr["DMADesc"] != DBNull.Value)
				sb.Append(RemoveComa((string)rdr["DMADesc"]));

			sb.Append(",");
			if (rdr["Sales_Count"] != DBNull.Value)
				sb.Append(((int)rdr["Sales_Count"]).ToString());

			sb.Append(",");
			if (rdr["Average_Collected"] != DBNull.Value)
				sb.Append(((decimal)rdr["Average_Collected"]).ToString());

			sb.Append(",");
			if (rdr["Amount_Collected"] != DBNull.Value)
				sb.Append(((decimal)rdr["Amount_Collected"]).ToString());

			sb.Append(",");
			if (rdr["Agent_Locations"] != DBNull.Value)
				sb.Append(((int)rdr["Agent_Locations"]).ToString());

			sb.Append(",");
			if (rdr["Sales_Per_Store"] != DBNull.Value)
				sb.Append(((decimal)rdr["Sales_Per_Store"]).ToString());

			return sb.ToString(); 
		}
		static string SalesByStoreReader(SqlDataReader rdr)
		{
			StringBuilder sb = new StringBuilder();
			
			if (rdr["DMADesc"] != DBNull.Value)
				sb.Append(RemoveComa((string)rdr["DMADesc"]));

			sb.Append(",");
			if (rdr["StoreName_Number"] != DBNull.Value)
				sb.Append(RemoveComa((string)rdr["StoreName_Number"]));

			sb.Append(",");
			if (rdr["Address"] != DBNull.Value)
				sb.Append(RemoveComa((string)rdr["Address"]));

			sb.Append(",");
			if (rdr["CityStateZip"] != DBNull.Value)
				sb.Append(RemoveComa((string)rdr["CityStateZip"]));

			sb.Append(",");
			if (rdr["Sales_Count"] != DBNull.Value)
				sb.Append(((int)rdr["Sales_Count"]).ToString());

			sb.Append(",");
			if (rdr["pLM"] != DBNull.Value)
				sb.Append(((int)rdr["pLM"]).ToString());

			return sb.ToString(); 
		}
		static string CreateComaSepString(SqlCommand cmd, string header)
		{
			StringBuilder sb = new StringBuilder();
			SqlDataReader rdr = cmd.ExecuteReader();

			try
			{
				sb.Append(header);				

				while(rdr.Read())
				{
					sb.Append("\n");
					sb.Append(SalesByDMAReader(rdr));
				}
			
				return sb.ToString();
			}
			finally
			{
				if (!rdr.IsClosed)
					rdr.Close();
			}
		}

		static string CreateComaSepString1(SqlCommand cmd, string header)
		{
			StringBuilder sb = new StringBuilder();
			SqlDataReader rdr = cmd.ExecuteReader();

			try
			{
				sb.Append(header);				

				while(rdr.Read())
				{
					sb.Append("\n");
					sb.Append(SalesByStoreReader(rdr));
				}
			
				return sb.ToString();
			}
			finally
			{
				if (!rdr.IsClosed)
					rdr.Close();
			}
		}

		static string RemoveComa(string str)
		{
			return str.Replace(",", "");
		}
	}
}