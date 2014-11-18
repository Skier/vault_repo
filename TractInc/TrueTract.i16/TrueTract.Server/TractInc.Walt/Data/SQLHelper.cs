using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace TractInc.Walt.Data
{
    internal class SQLHelper
    {

        #region Configuration Values

        private const string CONNECTION_STRING_KEY = "waltConnectionString";

        #endregion

        #region Factory Methods

        internal static SqlConnection CreateConnection() {
            return new SqlConnection(ConnectionString);
        }

        private static string ConnectionString
        {
            get {
                string result = ConfigurationManager.AppSettings[CONNECTION_STRING_KEY];
                if (null == result || result.Length == 0) {
                    throw new ConfigurationErrorsException("Connection string not found");
                }

                return result;
            }
        }

        #endregion

        #region Internal Methods

        internal static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType,
                                            string cmdText, params DbParameter[] cmdParms)
        {
            SqlCommand cmd = PrepareCommand(conn, cmdType, cmdText, cmdParms);
            int result = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            return result;
        }

        internal static SqlDataReader ExecuteReader(SqlConnection conn, CommandType cmdType,
                                                  string cmdText, params SqlParameter[] cmdParms)
        {

            SqlCommand cmd = PrepareCommand(conn, cmdType, cmdText, cmdParms);
            SqlDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();

            return rdr;
        }

        private static SqlCommand PrepareCommand(SqlConnection conn,
                                            CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (null != cmdParms){
                foreach (DbParameter parm in cmdParms){
                    cmd.Parameters.Add(parm);
                }
            }

            return cmd;
        }

        #endregion
    }
}
