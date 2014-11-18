using System;
using System.Data;
using System.Collections; 
using System.Data.SqlClient;

namespace DPI.Components
{
	public class User  // accesses WEB_DPI database using imbedded connection string
	{ 
		const string ConnectionString = 
			  "Persist Security Info=False;User ID=ASPNETUSR;"
			+ "password=qW0h357W4 ;Initial Catalog=WEB_DPI;"
			+ "Data Source=SQLDBSDEV;";
		
		// Validates IP address 
		public bool IpAuthenticate(string ipAddress)
		{
			SqlCommand cmd = makeCommand();
			cmd.CommandText = "sp_xnetAccounts_ValidateIP";
			
			cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar, 50).Value = ipAddress;
			cmd.Parameters.Add("@returnValue", SqlDbType.Int,4);
			cmd.Parameters["@returnValue"].Direction = ParameterDirection.Output;
			
			try 
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				return (int)cmd.Parameters["@returnValue"].Value > -1;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return  false;
			}
			finally
			{
				cmd.Connection.Close();
			}
		}
		// Validates user & password
		public bool ValidateLogin(string UserID, string password)
		{
			SqlCommand cmd = makeCommand();
			cmd.CommandText = "sp_xnetAccounts_ValidateLogin";

			cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = UserID;
			cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = password;
			cmd.Parameters.Add("@returnValue", SqlDbType.Int,4);
			cmd.Parameters["@returnValue"].Direction = ParameterDirection.Output; 
			
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();	
				return (int)cmd.Parameters["@returnValue"].Value > -1;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
			finally
			{
				cmd.Connection.Close();
			}
		}
		// Returns a comma-delimited string with user roles
		public string GetUserRoles( string userID )
		{
			SqlCommand cmd = makeCommand();
			cmd.CommandText = "sp_xnetAccounts_RolesForUser";
			cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50).Value = userID;
			
			try 
			{
				cmd.Connection.Open();
				return getRoles(cmd.ExecuteReader());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
			finally
			{
				cmd.Connection.Close();
			}
		}
		// Returns a comma-delimited string with user roles
		public string GetUserRolesIP( string ipAddress )
		{
			SqlCommand cmd = makeCommand();
			cmd.CommandText = "sp_xnetAccounts_RolesForUserIP";
			cmd.Parameters.Add("@ipAddress", SqlDbType.VarChar, 50).Value = ipAddress;

			try
			{
				cmd.Connection.Open();
				return getRoles(cmd.ExecuteReader());
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
			finally
			{
				cmd.Connection.Close();
			}
		}
		string getRoles(SqlDataReader rdr)
		{
			ArrayList ar = new ArrayList(); 
			while(rdr.Read())
				ar.Add((string)rdr["Description"]);

			rdr.Close();

			string[] lines = new string[ar.Count];
			string userRoles = null;

			for (int i = 0; i < lines.Length; i++)
				if (i < lines.Length -1)
					userRoles += ar[i].ToString() + ",";
				else
					userRoles += ar[i].ToString();
		
			return userRoles;
		}
		SqlCommand makeCommand()
		{
			SqlConnection conn = new SqlConnection(ConnectionString);
			SqlCommand cmd = conn.CreateCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			
			return cmd;
		}
	}
}