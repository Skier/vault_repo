using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes; 
using DPI.Interfaces;
namespace DPI.Components
{
	
	public class CGMWrapper
	{				
		
		public static string LineTypeIDMapping(DateTime from)	
		{
			IUOW uow = new UOW();

			SqlCommand cmd = uow.Cn.CreateCommand();
			
			try
			{
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "spCGM_LineTypeIDMaping";
				cmd.CommandTimeout = 0;
				cmd.Parameters.Add("@From", SqlDbType.VarChar , 0).Value = from;			
				//cmd.Parameters.Add("@To", SqlDbType.Char, 0).Value = to;			
				return ExecuteReaderLineType(cmd);		
											
			}
			catch (TransactionException transactionException)
			{
				throw new TransactionException(transactionException.Message);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message, exception.InnerException);
			}
			finally
			{
				uow.close();
			}
		}

		public static string LineDisconnectReconnectFile(int month, int year)	
		{
			IUOW uow = new UOW();

			SqlCommand cmd = uow.Cn.CreateCommand();
			try
			{
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandTimeout = 0;
				cmd.CommandText = "spCGM_LineDisconnectReconnectFile";
				cmd.Parameters.Add("@Month", SqlDbType.VarChar , 0).Value = month;			
				cmd.Parameters.Add("@Year", SqlDbType.Char, 0).Value = year;			

				return ExecuteReaderLineDiscRecFile(cmd);		
											
			}
			catch (TransactionException transactionException)
			{
				throw new TransactionException(transactionException.Message);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message, exception.InnerException);
			}
			finally
			{
				uow.close();
			}
		}
		
		public static string SubscriberDataUSOC(int month, int year)	
		{
			IUOW uow = new UOW();

			SqlCommand cmd = uow.Cn.CreateCommand();
			
			try
			{
				cmd.Transaction = uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "spCGM_SUBSCRIBER_DATA_USOC_Level_Customer_Charges";
				cmd.CommandTimeout = 0;
				cmd.Parameters.Add("@Month", SqlDbType.VarChar , 0).Value = month;			
				cmd.Parameters.Add("@Year", SqlDbType.Char, 0).Value = year;			
				return ExecuteReaderSubscriberDataUSOC(cmd);		
											
			}
			catch (TransactionException transactionException)
			{
				throw new TransactionException(transactionException.Message);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message, exception.InnerException);
			}
			finally
			{
				uow.close();
			}
		}

		private static string ExecuteReaderLineType(SqlCommand cmd)
		{
			SqlDataReader rdr = cmd.ExecuteReader();

			StringBuilder sb = new StringBuilder();

			sb.Append("WTN|Line Type|Line Sub Type|Multiplier|B/R/P Indicator|Resale/UNE Indicator|State Indicator|Customer Account #");
			sb.Append("\r\n");

			while (rdr.Read())
			{
				if (rdr["WTN"] != DBNull.Value)
					sb.Append((string)rdr["WTN"]);
						
				sb.Append("|");
				if (rdr["Line Type"] != DBNull.Value)
					sb.Append((string)rdr["Line Type"]);
						
				sb.Append("|");
				if (rdr["Line Sub Type"] != DBNull.Value)
					sb.Append((string)rdr["Line Sub Type"]);
						
				sb.Append("|");	
				if (rdr["Multiplier"] != DBNull.Value)
					sb.Append((string)rdr["Multiplier"]);
						
				sb.Append("|");	
				if (rdr["B/R/P Indicator"] != DBNull.Value)
					sb.Append((string)rdr["B/R/P Indicator"]);
				
				sb.Append("|");
				if (rdr["Resale/UNE Indicator"] != DBNull.Value)
					sb.Append((string)rdr["Resale/UNE Indicator"]);
				
				sb.Append("|");
				if (rdr["State Indicator"] != DBNull.Value)
					sb.Append((string)rdr["State Indicator"]);

				sb.Append("|");				
				if ( rdr["Customer Account #"] != DBNull.Value)
					sb.Append((string)rdr["Customer Account #"].ToString());

				sb.Append("\r\n");
				
			}
			return sb.ToString();
		}
		private static string ExecuteReaderLineDiscRecFile(SqlCommand cmd)
		{
			SqlDataReader rdr = cmd.ExecuteReader();

			StringBuilder sb = new StringBuilder();
			
			sb.Append("WTN|Install Date|Install PON|Disconnect Date|Disconnect PON");
			sb.Append("\r\n");
	
			while (rdr.Read())
			{
				
				if (rdr["WTN"] != DBNull.Value)
					sb.Append((string)rdr["WTN"]);
				
				sb.Append("|");
				if ( rdr["Install Date"] != DBNull.Value)
					sb.Append((string)rdr["Install Date"].ToString());
							
				sb.Append("|");
				if (rdr["Install Purchase Order Number"] != DBNull.Value)
					sb.Append((string)rdr["Install Purchase Order Number"]);
						
//				sb.Append("|");
//				if (rdr["Install Service Order Number"] != DBNull.Value)
//					sb.Append((string)rdr["Install Service Order Number"]);
						
				sb.Append("|");	
				if (rdr["Disconnect Date"] != DBNull.Value)
					sb.Append((string)rdr["Disconnect Date"]);
						
				sb.Append("|");	
				if (rdr["Disconnect Purchase Order Number"] != DBNull.Value)
					sb.Append((string)rdr["Disconnect Purchase Order Number"]);
				
//				sb.Append("|");
//				if (rdr["Disconnect Service Order Number"] != DBNull.Value)
//					sb.Append((string)rdr["Disconnect Service Order Number"]);
				
				sb.Append("\r\n");
				
			}
			return sb.ToString();
		}
		private static string ExecuteReaderSubscriberDataUSOC(SqlCommand cmd)
		{
			SqlDataReader rdr = cmd.ExecuteReader();

			StringBuilder sb = new StringBuilder();

			sb.Append("Subscriber Account #|Subscriber Name|BTN|WTN|USOC|USOC Descripton|Amount Billed|Qty Billed|Data Type|Billed Date|Activation Date|Cancellation Date|Line Type|PType|LSR USOC");
			sb.Append("\r\n");

			while (rdr.Read())
			{
				if (rdr["Subscriber Account Number"] != DBNull.Value)
					sb.Append(((int)rdr["Subscriber Account Number"]).ToString());
						
				sb.Append("|");
				if (rdr["Subscriber Name"] != DBNull.Value)
					sb.Append((string)rdr["Subscriber Name"]);
						
				sb.Append("|");
				if (rdr["BTN"] != DBNull.Value)
					sb.Append((string)rdr["BTN"]);
						
				sb.Append("|");	
				if (rdr["WTN"] != DBNull.Value)
					sb.Append((string)rdr["WTN"]);
						
				sb.Append("|");	
				if (rdr["USOC"] != DBNull.Value)
					sb.Append(((int)rdr["USOC"]).ToString());
				
				sb.Append("|");
				if (rdr["USOC Description"] != DBNull.Value)
					sb.Append((string)rdr["USOC Description"]);
				
				sb.Append("|");
				if (rdr["Amount Billed"] != DBNull.Value)
					sb.Append(((decimal)rdr["Amount Billed"]).ToString());

				sb.Append("|");				
				if ( rdr["QTY Billed"] != DBNull.Value)
					sb.Append(((int)rdr["QTY Billed"]).ToString());

				sb.Append("|");				
				if ( rdr["Data Type"] != DBNull.Value)
					sb.Append((string)rdr["Data Type"]);

				sb.Append("|");				
				if ( rdr["Billed Date"] != DBNull.Value)
					sb.Append((string)rdr["Billed Date"]);

				sb.Append("|");				
				if ( rdr["Activation Date"] != DBNull.Value)
					sb.Append((string)rdr["Activation Date"]);

				sb.Append("|");				
				if ( rdr["Cancellation Date"] != DBNull.Value)
					sb.Append((string)rdr["Cancellation Date"]);

				sb.Append("|");				
				if ( rdr["Line Type"] != DBNull.Value)
					sb.Append((string)rdr["Line Type"]);

				sb.Append("|");				
				if ( rdr["PType"] != DBNull.Value)
					sb.Append((string)rdr["PType"]);

				sb.Append("|");				
				if ( rdr["LSR USOC"] != DBNull.Value)
					sb.Append((string)rdr["LSR USOC"]);
			

				sb.Append("\r\n");
				
			}
			return sb.ToString();
		}
	}
}