using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Weborb.Samples.Email
{
    internal sealed class DataAccessBrigde
    {
        private DataAccessBrigde() {
        }

        #region Configuration Values

        const string OLEDB_DATA_ACCESS_LAYER = "OleDBDataSource";
        const string MY_SQL_DATA_ACCESS_LAYER = "MySqlDataSource";
        
        private static string DEFAULT_CONNECTION_STRING = 
            "Jet OLEDB:Global Partial Bulk Ops=2; " +
            "Jet OLEDB:Registry Path=; " +
            "Jet OLEDB:Database Locking Mode=1; " +
            "Jet OLEDB:Engine Type=5; " +
            "Jet OLEDB:System database=; " +
            "Jet OLEDB:SFP=False; " +
            "Jet OLEDB:Encrypt Database=False; " +
            "Jet OLEDB:Create System Database=False; " +
            "Jet OLEDB:Don't Copy Locale on Compact=False; " +
            "Jet OLEDB:Compact Without Replica Repair=False; " +
            "Jet OLEDB:Global Bulk Transactions=1; " +
            "persist security info=False; " +
            "Extended Properties=; " +
            "Provider=\"Microsoft.Jet.OLEDB.4.0\"; " +
            "Data Source=\"" + AppDomain.CurrentDomain.BaseDirectory + "Database\\email_client.mdb\";" +
            "User ID=Admin";

        #endregion

        #region Sql query templates

        // Address templates
        internal const string SQL_SELECT_ADDRESS_BY_ID =
            "select addressid, accountid, email from address where addressid={0}";

        internal const string SQL_SELECT_ADDRESS_BY_ACCOUNTID_AND_EMAIL =
            "select addressid, accountid, email from address where accountid={0} and email='{1}'";

        internal const string SQL_SELECT_ALL_ADDRESSES_BY_ACCOUNTID =
            "select addressid, accountid, email from address where accountid={0}";

        internal const string SQL_INSERT_ADDRESS =
            "insert into address (accountid, email) values ({0}, '{1}')";

        internal const string SQL_DELETE_ADDRESS =
            "delete from address where addressid={0}";

        // Account templates
        internal const string SQL_SELECT_LAST_ACCOUNTID =
            "select accountid from account order by accountid desc";

        internal const string SQL_SELECT_ALL_ACCOUNTS =
            "select accountid, email, pop3settingsid, smtpsettingsid from account";

        internal const string SQL_SELECT_ACCOUNT_BY_ACCOUNTID =
            "select accountid, email, pop3settingsid, smtpsettingsid from account where accountid={0}";

        internal const string SQL_SELECT_ACCOUNT_BY_EMAIL =
            "select accountid, email, pop3settingsid, smtpsettingsid from account where email='{0}'";

        internal const string SQL_INSERT_ACCOUNT =
            "insert into account (email, pop3settingsid, smtpsettingsid) values ('{0}', '{1}', {2})";

        internal const string SQL_UPDATE_ACCOUNT =
            "update account set email='{0}' where accountid={1}";

        internal const string SQL_DELETE_ACCOUNT =
            "delete from account where accountid={0}";

        // Settings templates
        internal const string SQL_SELECT_LAST_SETTINGID =
            "select serversettingsid from serversettings order by serversettingsid desc";

        internal const string SQL_SELECT_SETTING_BY_SETTINGS_ID =
            "select serversettingsid, host, port, connectiontype, username, userpassword from serversettings where serversettingsid={0}";

        internal const string SQL_INSERT_SETTINGS =
            "insert into serversettings (host, port, connectiontype, username, userpassword) values ('{0}', {1}, '{2}', '{3}', '{4}')";

        internal const string SQL_UPDATE_SETTINGS =
            "update serversettings set host='{0}', port={1}, connectiontype='{2}', username='{3}', userpassword='{4}' where serversettingsid={5}";

        internal const string SQL_DELETE_SETTINGS =
            "delete from serversettings where serversettingsid={0}";

        #endregion

        #region Factory Methods

        static internal IDbConnection CreateConnection() {

            if (DataAccessLayer == MY_SQL_DATA_ACCESS_LAYER)
                return new MySqlConnection(ConnectionString);
            else if (DataAccessLayer == OLEDB_DATA_ACCESS_LAYER)
                return new OleDbConnection(ConnectionString);
            else
                throw new NotSupportedException();
        }

        static internal IDbCommand CreateCommand() {
            if (DataAccessLayer == MY_SQL_DATA_ACCESS_LAYER)
                return new MySqlCommand();
            else if (DataAccessLayer == OLEDB_DATA_ACCESS_LAYER)
                return new OleDbCommand();
            else
                throw new NotSupportedException();
        }

        static string ConnectionString {
            get {
                string result = ConfigurationManager.AppSettings["connectionString"];
                if (null == result || result.Length == 0) {
                    result = DEFAULT_CONNECTION_STRING;
                }
                
                return result;
            }
        }

        static string DataAccessLayer {
            get { return ConfigurationManager.AppSettings["dataAccessLayer"]; }
        }

        #endregion

        #region Internal Methods

        static internal int ExecuteNonQuery(IDbConnection connection, CommandType cmdType,
                                            string cmdText, params DbParameter[] cmdParms)
        {
            IDbCommand cmd = CreateCommand();
            PrepareCommand(connection, cmd, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        static internal IDataReader ExecuteReader(IDbConnection connection, CommandType cmdType,
                                                  string cmdText, params DbParameter[] cmdParms)
        {
            IDbCommand cmd = CreateCommand();
            PrepareCommand(connection, cmd, cmdType, cmdText, cmdParms);
            IDataReader rdr = cmd.ExecuteReader();
            cmd.Parameters.Clear();
            return rdr;
        }

        static internal void PrepareCommand(IDbConnection connection, IDbCommand cmd,
                                            CommandType cmdType, string cmdText, DbParameter[] cmdParms)
        {
            cmd.Connection = connection;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (null != cmdParms)
                foreach (DbParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
        }

        #endregion
    }
}