/*
 *  
 *
 * 
 */
//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//===============================================================================

using System;
using System.Configuration;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using log4net;

namespace AerSysCo.Common
{
/// <summary>
/// The SqlHelper class is intended to encapsulate high performance, 
/// scalable best practices for common uses of SqlClient
/// </summary>
public abstract class SQLHelper 
{
    public const string CONNECTION_STRING = "connection";
    private const string CANNOT_OPEN_CONNECTION = "Cannot open connection";
    private const int SQL_TIMEOUT = 3600;

    public static string CONN_STRING {
        get {
            string connectionString = ConfigurationManager.ConnectionStrings["warehouse"].ConnectionString;
            return connectionString;
        }
    }

    public static SqlConnection CreateConnection() {
        return new SqlConnection(SQLHelper.CONN_STRING);
    }

    /// <summary>
    /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        GetLogger().Debug(cmdText);

        SqlCommand cmd = CreateSqlCommand();

        PrepareCommand(cmd, connString, cmdType, cmdText, cmdParms);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }

    // Executes specified stored procedure in transaction and returns its return value
    public static int ExecuteStoredProcedure(SqlTransaction transaction, string cmdText,
        params SqlParameter[] cmdParms)
    {
        GetLogger().Debug(cmdText);
        SqlCommand cmd = CreateSqlCommand();
        PrepareCommand(cmd, transaction, CommandType.StoredProcedure, cmdText, cmdParms);

        SqlParameter returnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
        returnValue.Direction = ParameterDirection.ReturnValue;

        cmd.Parameters.Add(returnValue);

        cmd.ExecuteNonQuery();

        return (int)returnValue.Value;
    }

    // Executes specified stored procedure and returns its return value
    public static int ExecuteStoredProcedure(string cmdText, params SqlParameter[] cmdParms) {
        GetLogger().Debug(cmdText);
        SqlCommand cmd = CreateSqlCommand();
        PrepareCommand(cmd, CONN_STRING, CommandType.StoredProcedure, cmdText, cmdParms);

        SqlParameter returnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
        returnValue.Direction = ParameterDirection.ReturnValue;

        cmd.Parameters.Add(returnValue);

        cmd.ExecuteNonQuery();

        return (int)returnValue.Value;
    }

    /// <summary>
    /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int result = ExecuteNonQuery(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        return ExecuteNonQuery(CONN_STRING, cmdType, cmdText, cmdParms);
    }

    /// <summary>
    /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="trans">an existing sql transaction</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        GetLogger().Debug(cmdText);
        SqlCommand cmd = CreateSqlCommand();
        PrepareCommand(cmd, trans, cmdType, cmdText, cmdParms);
        int val = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();
        return val;
    }

    /// <summary>
    /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>A SqlDataReader containing the results</returns>
    public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        GetLogger().Debug(cmdText);

        SqlCommand cmd = CreateSqlCommand();
        
        // we use a try/catch here because if the method throws an exception we want to 
        // close the connection throw code, because no datareader will exist, hence the 
        // commandBehaviour.CloseConnection will not work
        try {
            PrepareCommand(cmd, connString, cmdType, cmdText, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return rdr;
        } catch {
            throw;
        }
    }

    /// <summary>
    /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  SqlDataReader r = ExecuteReader(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>A SqlDataReader containing the results</returns>
    public static SqlDataReader ExecuteReader(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        return ExecuteReader(CONN_STRING, cmdType, cmdText, cmdParms);
    }

    public static SqlDataReader ExecuteReader(SqlTransaction tx, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        GetLogger().Debug(cmdText);
        SqlCommand cmd = CreateSqlCommand();
        PrepareCommand(cmd, tx, cmdType, cmdText, cmdParms);
        SqlDataReader rdr = cmd.ExecuteReader();
        cmd.Parameters.Clear();
        return rdr;
    }
    
    /// <summary>
    /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        GetLogger().Debug(cmdText);

        SqlCommand cmd = CreateSqlCommand();

        PrepareCommand(cmd, connString, cmdType, cmdText, cmdParms);
        object val = cmd.ExecuteScalar();
        cmd.Parameters.Clear();
        return val;
    }

    /// <summary>
    /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
    /// using the provided parameters.
    /// </summary>
    /// <remarks>
    /// e.g.:  
    ///  Object obj = ExecuteScalar(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
    /// </remarks>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
    /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
    public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] cmdParms) {
        return ExecuteScalar(CONN_STRING, cmdType, cmdText, cmdParms);
    }

    public static Int32 GetIdentity(SqlTransaction trans) {
        SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, "select cast(@@identity as int)", new SqlParameter[0]);
        using(rdr) {
            if ( rdr.Read() ) {
                return rdr.GetInt32(0);
            } else {
                throw new SystemException("Can't read @@identity");
            }
        }
    }

    public static Int64 GetBigintIdentity(SqlTransaction trans) {
        SqlDataReader rdr = SQLHelper.ExecuteReader(trans, CommandType.Text, "select cast(@@identity as bigint)", new SqlParameter[0]);
        using(rdr) {
            if ( rdr.Read() ) {
                return rdr.GetInt64(0);
            } else {
                throw new SystemException("Cannot read @@identity");
            }
        }
    }

    /// <summary>
    /// Prepare a command for execution
    /// </summary>
    /// <param name="cmd">SqlCommand object</param>
    /// <param name="conn">SqlConnection object</param>
    /// <param name="trans">SqlTransaction object</param>
    /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
    /// <param name="cmdText">Command text, e.g. Select * from Products</param>
    /// <param name="cmdParms">SqlParameters to use in the command</param>
    private static void PrepareCommand(SqlCommand cmd, string connString, 
              CommandType cmdType, string cmdText, SqlParameter[] cmdParms) 
    {

        SqlConnection conn = new SqlConnection(connString);
        try {
            conn.Open();
        } catch (SqlException ex){
            throw new SystemException(SQLHelper.CANNOT_OPEN_CONNECTION, ex);
        }

        cmd.Connection = conn;
        cmd.CommandText = cmdText;
        cmd.CommandType = cmdType;

        if ( null != cmdParms ) {
            foreach (SqlParameter parm in cmdParms) {
                cmd.Parameters.Add(parm);
                GetLogger().Debug(string.Format("{0}={1}", parm.ParameterName, parm.Value));
            }
        }
    }

    private static void PrepareCommand(SqlCommand cmd, SqlTransaction trans, 
            CommandType cmdType, string cmdText, SqlParameter[] cmdParms) 
    {

        cmd.Connection = trans.Connection;
        cmd.Transaction = trans;
        cmd.CommandText = cmdText;
        cmd.CommandType = cmdType;

        if ( null != cmdParms ) {
            foreach (SqlParameter parm in cmdParms) {
                cmd.Parameters.Add(parm);
                GetLogger().Debug(string.Format("{0}={1}", parm.ParameterName, parm.Value));
            }
        }
    }

    private static ILog GetLogger() {
        return LogManager.GetLogger(typeof(SQLHelper));
    }

    private static SqlCommand CreateSqlCommand() {
        SqlCommand result = new SqlCommand();
        result.CommandTimeout = SQL_TIMEOUT;
        return result;
    }
}
}