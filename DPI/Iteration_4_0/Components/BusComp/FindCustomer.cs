using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DPI.Interfaces;
 
namespace DPI.Components
{
	[Serializable]
	public class FindCustomer
	{
		#region Public Methods

		public static IAcctInfo[] FindCustomers(UOW uow, ICustomerFilter customerFilter)
		{
			if ( uow == null )
				throw new ArgumentException("UOW is required.");

			if ( customerFilter == null )
				throw new ArgumentException("Customer filter is required.");

			return (new FindCustomerSQL()).FindCustomer(uow, customerFilter);
		}

		#endregion
 
		#region Sql

		[Serializable]
		class FindCustomerSQL
		{
			private void SetParameters(SqlCommand cmd, ICustomerFilter customerFilter)
			{
				if ( customerFilter == null )
					return;

				if ( customerFilter.AccNumber != null )
					cmd.Parameters.Add("@AccNumber", SqlDbType.VarChar, 8).Value = customerFilter.AccNumber;

				if ( customerFilter.PhoneNumber != null )
					cmd.Parameters.Add("@PhNumber", SqlDbType.VarChar, 10).Value = customerFilter.PhoneNumber;

				if ( customerFilter.FirstName != null )
					cmd.Parameters.Add("@NameFirst", SqlDbType.VarChar, 50).Value = customerFilter.FirstName;

				if ( customerFilter.LastName != null )
					cmd.Parameters.Add("@NameLast", SqlDbType.VarChar, 50).Value = customerFilter.LastName;

				if ( customerFilter.Addr.StreetPrefix != null )
					cmd.Parameters.Add("@Prefix", SqlDbType.Char, 1).Value = customerFilter.Addr.StreetPrefix;

				if ( customerFilter.Addr.StreetNum != null )
					cmd.Parameters.Add("@StreetNumber", SqlDbType.VarChar, 10).Value = customerFilter.Addr.StreetNum;

				if ( customerFilter.Addr.Street != null )
					cmd.Parameters.Add("@Street", SqlDbType.VarChar, 50).Value = customerFilter.Addr.Street;

				if ( customerFilter.Addr.StreetSuffix != null )
					cmd.Parameters.Add("@Suffix", SqlDbType.Char, 1).Value = customerFilter.Addr.StreetSuffix;

				if ( customerFilter.Addr.Unit != null )
					cmd.Parameters.Add("@Unit", SqlDbType.VarChar, 10).Value = customerFilter.Addr.Unit;

				if ( customerFilter.Addr.City != null )
					cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = customerFilter.Addr.City;

				if ( customerFilter.Addr.State != null )
					cmd.Parameters.Add("@State", SqlDbType.Char, 2).Value = customerFilter.Addr.State;
				
				if ( customerFilter.Addr.Zipcode != null )
					cmd.Parameters.Add("@Zipcode", SqlDbType.VarChar, 50).Value = customerFilter.Addr.Zipcode;

			}

			protected SqlCommand GetCommand(UOW uow, string commandText, ICustomerFilter customerFilter)
			{
				SqlCommand cmd = Conn.GetConn().CreateCommand();
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = commandText;
				cmd.Transaction = uow.Tran;
				SetParameters(cmd, customerFilter);
				return cmd;
			}

			private IAcctInfo GetAcctInfo(SqlDataReader rdr)
			{
				int accNumber = 0;
				if ( rdr["AccNumber"] != DBNull.Value )
					accNumber = (int) rdr["AccNumber"];

				string phNumber = null;
				if ( rdr["PhNumber"] != DBNull.Value )
					phNumber = (string) rdr["PhNumber"];

				string nameLast = null;
				if ( rdr["NameLast"] != DBNull.Value )
					nameLast = (string) rdr["NameLast"];
 
				string nameFirst = null;
				if ( rdr["NameFirst"] != DBNull.Value )
					nameFirst = (string) rdr["NameFirst"];

				bool isActive = false;
				if (  rdr["IsActive"] != DBNull.Value )
				{
					object res = rdr["IsActive"];
					isActive = (bool)res;
				}

				decimal pastDueAmt = 0;
				if ( rdr["PastDueAmt"] != DBNull.Value )
					pastDueAmt = Decimal.Round((decimal)rdr["PastDueAmt"], 2);

				decimal currentChargeAmount = 0;
				if ( rdr["CurrentChargeAmt"] != DBNull.Value )
					currentChargeAmount = Decimal.Round((decimal)rdr["CurrentChargeAmt"], 2);

				decimal balance = 0;
				if ( rdr["Balance"] != DBNull.Value )
					balance = Decimal.Round((decimal)rdr["Balance"], 2);

				DateTime dueDate = DateTime.MinValue;
				if ( rdr["DueDate"] != DBNull.Value )
					dueDate = (DateTime) rdr["DueDate"];

				DateTime sDiscoDate = DateTime.MinValue;
				if ( rdr["SDiscoDate"] != DBNull.Value )
					sDiscoDate = (DateTime) rdr["SDiscoDate"];

				string status = null;
				if ( rdr["Status"] != DBNull.Value )
					status = Account.GetStatus((string)rdr["Status"]);

				return new AcctInfo(accNumber, 
					phNumber, 
					isActive, 
					pastDueAmt,
					currentChargeAmount, 
					balance, 
					dueDate, 
					sDiscoDate, 
					status, 
					nameFirst, 
					nameLast);
			}

			protected IAcctInfo[] ExecuteReader(SqlCommand sqlCommand)
			{
				ArrayList arrayList = new ArrayList();
				SqlDataReader sqlDataReader = null;

				try
				{
					sqlCommand.CommandTimeout = 0;
					sqlDataReader = sqlCommand.ExecuteReader();
					while(sqlDataReader.Read())
						arrayList.Add( GetAcctInfo(sqlDataReader) );

					IAcctInfo[] acctInfos = new IAcctInfo[arrayList.Count];
					arrayList.CopyTo(acctInfos);

					return acctInfos;
				}
				catch (Exception e)
				{

					if ( sqlCommand.Transaction != null)
						if ( sqlCommand.Transaction.Connection != null)
							sqlCommand.Transaction.Rollback();
				
					throw e;
				}
				finally
				{
					if ( !sqlDataReader.IsClosed)
						sqlDataReader.Close();
				}
			}

			public IAcctInfo[] FindCustomer(UOW uow, ICustomerFilter customerFilter)
			{
				SqlCommand cmd = GetCommand(uow, "spCustomer_Find", customerFilter);
				
				return ExecuteReader(cmd);
			}
		}
		#endregion
	}
}
