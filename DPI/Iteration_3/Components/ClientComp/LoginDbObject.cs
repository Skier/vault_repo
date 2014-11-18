using System;
using System.Data;
using System.Data.SqlClient;

namespace DPI.ClientComp
{
	public abstract class LoginDbObject
	{
		protected SqlConnection Connection;
		private string connectionString;

		/// <param name="newConnectionString">Connection String to the associated database</param>
		public LoginDbObject( string newConnectionString )
		{
			connectionString = newConnectionString;
			Connection = new SqlConnection( connectionString );
		}

		protected string ConnectionString
		{
			get 
			{
				return connectionString;
			}
		}


		/// <summary>
		/// Private routine allowed only by this base class, it automates the task
		/// of building a SqlCommand object designed to obtain a return value from
		/// the stored procedure.
		/// </summary>
		private SqlCommand BuildIntCommand(string storedProcName, IDataParameter[] parameters)
		{
			SqlCommand command = BuildQueryCommand( storedProcName, parameters );			
			command.Parameters.Add( new SqlParameter ( "ReturnValue",
				SqlDbType.Int,
				4,						/* Size */
				ParameterDirection.ReturnValue,
				false,					/* is nullable */
				0,						/* byte precision */
				0,						/* byte scale */
				string.Empty,
				DataRowVersion.Default,
				null ));

			return command;
		}


		/// <summary>
		/// Builds a SqlCommand designed to return a SqlDataReader, and not
		/// an actual integer value.
		/// </summary>
		private SqlCommand BuildQueryCommand(string storedProcName, IDataParameter[] parameters)
		{
			SqlCommand command = new SqlCommand( storedProcName, Connection );
			command.CommandType = CommandType.StoredProcedure;

			foreach (SqlParameter parameter in parameters)
			{
				command.Parameters.Add( parameter );
			}
			return command;
		}


		/// <summary>
		/// Runs a stored procedure, can only be called by those classes deriving
		/// from this base. It returns an integer indicating the return value of the
		/// stored procedure, and also returns the value of the RowsAffected aspect
		/// of the stored procedure that is returned by the ExecuteNonQuery method.
		/// </summary>
		protected int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected )
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "sp_xnetAccounts_ValidateLogin";
			cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = parameters[0].Value;
			cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = parameters[1].Value;
			cmd.Parameters.Add("@returnValue", SqlDbType.Int,4);
			cmd.Parameters["@returnValue"].Direction = ParameterDirection.Output;

			conn.Open();
			rowsAffected = cmd.ExecuteNonQuery();
			conn.Close();
			return rowsAffected;
		}


		/// <summary>
		/// Will run a stored procedure, can only be called by those classes deriving
		/// from this base. It returns a SqlDataReader containing the result of the stored
		/// procedure.
		/// </summary>
		/// <param name="storedProcName">Name of the stored procedure</param>
		/// <param name="parameters">Array of parameters to be passed to the procedure</param>
		/// <returns>A newly instantiated SqlDataReader object</returns>
		protected SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters )
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "sp_xnetAccounts_RolesForUser";
			cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = parameters[0].Value;

			conn.Open();
			return cmd.ExecuteReader();
		}


		/// <summary>
		/// Creates a DataSet by running the stored procedure and placing the results
		/// of the query/proc into the given tablename.
		/// </summary>
		/// <param name="storedProcName"></param>
		/// <param name="parameters"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		protected DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName )
		{
			DataSet dataSet = new DataSet();
			Connection.Open();
			SqlDataAdapter sqlDA = new SqlDataAdapter();
			sqlDA.SelectCommand = BuildQueryCommand( storedProcName, parameters );
			sqlDA.Fill( dataSet, tableName );
			Connection.Close();

			return dataSet;
		}


		/// <summary>
		/// Takes an -existing- dataset and fills the given table name with the results
		/// of the stored procedure.
		/// </summary>
		/// <param name="storedProcName"></param>
		/// <param name="parameters"></param>
		/// <param name="dataSet"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		protected void RunProcedure(string storedProcName, IDataParameter[] parameters, DataSet dataSet, string tableName )
		{
			Connection.Open();
			SqlDataAdapter sqlDA = new SqlDataAdapter();
			sqlDA.SelectCommand = BuildIntCommand( storedProcName, parameters );
			sqlDA.Fill( dataSet, tableName );
			Connection.Close();			
		}


	}

}
