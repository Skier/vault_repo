using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes; 
using DPI.Interfaces;
namespace DPI.Components
{
	public class VerifoneWrapper
	{				
		public static IVerifoneResult SubmitPendXact( // uses reserved accNumber
			UOW _uow,
			string _storeCode,
			string _clerkID,			
			string _transNum, 
			decimal _localAmount, 
			decimal _ldAmount,
			string _commPort,
			int _accNumber)				
		{
			SqlCommand cmd = _uow.Cn.CreateCommand();
			try
			{
				cmd.Transaction = _uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "spVFR_NewPayment2";
	
				cmd.Parameters.Add("@CommPort", SqlDbType.VarChar , 10).Value = _commPort;			
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = _storeCode;
				if (_clerkID == null)
					cmd.Parameters.Add("@ClerkID", SqlDbType.Char, 10).Value = DBNull.Value;
				else
				{
					if (_clerkID.Length == 0)
						cmd.Parameters.Add("@ClerkID", SqlDbType.Char, 10).Value = DBNull.Value;
					else
						cmd.Parameters.Add("@ClerkID", SqlDbType.Char, 10).Value = _clerkID;
				}
				cmd.Parameters.Add("@TransactionNumber", SqlDbType.VarChar, 20).Value = _transNum;			
				cmd.Parameters.Add("@LocalAmount", SqlDbType.Money, 0).Value = _localAmount;			
				cmd.Parameters.Add("@LDAmount", SqlDbType.Money, 0).Value = _ldAmount;			
			
				cmd.Parameters.Add("@ErrorCode", SqlDbType.Char, 1).Value = "";
				cmd.Parameters.Add("@ConfNum", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = _accNumber;
				cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar, 40).Value = "";
				cmd.Parameters.Add("@TransactionTime", SqlDbType.DateTime, 0).Value = DateTime.Now;
				cmd.Parameters.Add("@Message", SqlDbType.VarChar, 200).Value = "";
			//	cmd.Parameters.Add("@Rid", SqlDbType.Int, 0).Value = 0;

				// output parms					
		//		cmd.Parameters["@Rid"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@ErrorCode"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@ConfNum"].Direction = ParameterDirection.InputOutput;
		//		cmd.Parameters["@AccNumber"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@CustomerName"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@TransactionTime"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@Message"].Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();				
				if ((cmd.Parameters["@ErrorCode"].Value == System.DBNull.Value ? "E" : (string)cmd.Parameters["@ErrorCode"].Value) == "A")
				{
					VerifoneResult verifoneResult = new VerifoneResult(
					//	cmd.Parameters["@Rid"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@Rid"].Value,
						0,
						cmd.Parameters["@ErrorCode"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@ErrorCode"].Value,
						cmd.Parameters["@ConfNum"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@ConfNum"].Value,
						_accNumber,
						cmd.Parameters["@CustomerName"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@CustomerName"].Value,
						cmd.Parameters["@TransactionTime"].Value == System.DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@TransactionTime"].Value, 						
						cmd.Parameters["@Message"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@Message"].Value);
					return verifoneResult;
				}
				else
				{						
					throw new TransactionException(cmd.Parameters["@Message"].Value.ToString());
				}							
			}
			catch (TransactionException transactionException)
			{
				throw new TransactionException(transactionException.Message);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message, exception.InnerException);
			}
		}

		public static IVerifoneResult SubmitNewXact(
			UOW _uow,
			string _storeCode,
			string _clerkID,			
			string _transNum, 
			decimal _localAmount, 
			decimal _ldAmount,
			string _commPort)				
		{
  			SqlCommand cmd = _uow.Cn.CreateCommand();
			try
			{
				cmd.Transaction = _uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "spVFR_NewPayment_Id";

				cmd.Parameters.Add("@Rid", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@CommPort", SqlDbType.VarChar , 10).Value = _commPort;			
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = _storeCode;			
				cmd.Parameters.Add("@ClerkID", SqlDbType.Char, 10).Value = _clerkID;			
				cmd.Parameters.Add("@TransactionNumber", SqlDbType.VarChar, 20).Value = _transNum;			
				cmd.Parameters.Add("@LocalAmount", SqlDbType.Money, 0).Value = _localAmount;			
				cmd.Parameters.Add("@LDAmount", SqlDbType.Money, 0).Value = _ldAmount;			
			
				cmd.Parameters.Add("@ErrorCode", SqlDbType.Char, 1).Value = "";
				cmd.Parameters.Add("@ConfNum", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar, 40).Value = "";
				cmd.Parameters.Add("@TransactionTime", SqlDbType.DateTime, 0).Value = DateTime.Now;
				cmd.Parameters.Add("@Message", SqlDbType.VarChar, 200).Value = "";
				// output parms
				cmd.Parameters["@Rid"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@ErrorCode"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@ConfNum"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@AccNumber"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@CustomerName"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@TransactionTime"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@Message"].Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();				
				if ((cmd.Parameters["@ErrorCode"].Value == System.DBNull.Value ? "E" : (string)cmd.Parameters["@ErrorCode"].Value) == "A")
				{
					VerifoneResult verifoneResult = new VerifoneResult(
						cmd.Parameters["@Rid"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@Rid"].Value,
						cmd.Parameters["@ErrorCode"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@ErrorCode"].Value,
						cmd.Parameters["@ConfNum"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@ConfNum"].Value,
						cmd.Parameters["@AccNumber"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@AccNumber"].Value,
						cmd.Parameters["@CustomerName"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@CustomerName"].Value,
						cmd.Parameters["@TransactionTime"].Value == System.DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@TransactionTime"].Value, 						
						cmd.Parameters["@Message"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@Message"].Value);
					return verifoneResult;
				}
				else
				{						
					throw new TransactionException(cmd.Parameters["@Message"].Value.ToString());
				}							
			}
			catch (TransactionException transactionException)
			{
				throw new TransactionException(transactionException.Message);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message, exception.InnerException);
			}
		}
		
		public static IVerifoneResult SubmitMonthlyXact(
			UOW _uow,			
			string _storeCode,	
			string _clerkID,	
			string _transNum, 
			string _phNumber,
			decimal _localAmount, 
			decimal _ldAmount,
			string _commPort)
				
		{
			SqlCommand cmd = _uow.Cn.CreateCommand();
			VerifoneMonthlyResult verifoneMonthlyResult = null;
			try
			{
				cmd.Transaction = _uow.Tran; 
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = "spVFR_MonthlyPayment2";

				cmd.Parameters.Add("@Rid", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@CommPort", SqlDbType.VarChar , 10).Value = _commPort;			
				cmd.Parameters.Add("@StoreCode", SqlDbType.Char, 10).Value = _storeCode;			
				cmd.Parameters.Add("@ClerkID", SqlDbType.Char, 10).Value = _clerkID;			
				cmd.Parameters.Add("@TransactionNumber", SqlDbType.VarChar, 20).Value = _transNum;			
				cmd.Parameters.Add("@PhNumber", SqlDbType.Char, 10).Value = _phNumber;
				cmd.Parameters.Add("@LocalAmount", SqlDbType.Money, 0).Value = _localAmount;			
				cmd.Parameters.Add("@LDAmount", SqlDbType.Money, 0).Value = _ldAmount;						
				cmd.Parameters.Add("@ErrorCode", SqlDbType.Char, 1).Value = "";
				cmd.Parameters.Add("@ConfNum", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@AccNumber", SqlDbType.Int, 0).Value = 0;
				cmd.Parameters.Add("@AccStatus", SqlDbType.VarChar, 25).Value = "";
				cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar, 40).Value = "";
				cmd.Parameters.Add("@Balance", SqlDbType.Money, 0).Value = 0;
				cmd.Parameters.Add("@CurrentDueDate", SqlDbType.DateTime, 0).Value = DateTime.Now;
				cmd.Parameters.Add("@SDiscoDate", SqlDbType.DateTime, 0).Value = DateTime.Now;
				cmd.Parameters.Add("@TransactionTime", SqlDbType.DateTime, 0).Value = DateTime.Now;
				cmd.Parameters.Add("@Message", SqlDbType.VarChar, 200).Value = "";
				// output parms
				cmd.Parameters["@Rid"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@ErrorCode"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@ConfNum"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@AccNumber"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@AccStatus"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@CustomerName"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@Balance"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@CurrentDueDate"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@SDiscoDate"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@TransactionTime"].Direction = ParameterDirection.InputOutput;
				cmd.Parameters["@Message"].Direction = ParameterDirection.InputOutput;
				cmd.ExecuteNonQuery();				

				if ((cmd.Parameters["@ErrorCode"].Value == System.DBNull.Value ? "E" : (string)cmd.Parameters["@ErrorCode"].Value) == "A")
				{				
					verifoneMonthlyResult = new VerifoneMonthlyResult(
						cmd.Parameters["@Rid"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@Rid"].Value,
						cmd.Parameters["@ErrorCode"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@ErrorCode"].Value,
						cmd.Parameters["@ConfNum"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@ConfNum"].Value, 
						cmd.Parameters["@AccNumber"].Value == System.DBNull.Value ? 0 : (int)cmd.Parameters["@AccNumber"].Value, 
						cmd.Parameters["@CustomerName"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@CustomerName"].Value,
						cmd.Parameters["@TransactionTime"].Value == System.DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@TransactionTime"].Value,
						cmd.Parameters["@Message"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@Message"].Value,
						cmd.Parameters["@AccStatus"].Value == System.DBNull.Value ? "" : (string)cmd.Parameters["@AccStatus"].Value,
						cmd.Parameters["@Balance"].Value == System.DBNull.Value ? 0 : (decimal)cmd.Parameters["@Balance"].Value,						
						cmd.Parameters["@CurrentDueDate"].Value == System.DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@CurrentDueDate"].Value,
						cmd.Parameters["@SDiscoDate"].Value == System.DBNull.Value ? DateTime.MinValue : (DateTime)cmd.Parameters["@SDiscoDate"].Value);
					return verifoneMonthlyResult;
				}
				else
				{
					throw new TransactionException(cmd.Parameters["@Message"].Value.ToString());				
				}				
			}
			catch (TransactionException transactionException)
			{
				throw new TransactionException(transactionException.Message);
			}
			catch (Exception exception)
			{
				throw new Exception(exception.Message, exception.InnerException);
			}	
		}
	}
}