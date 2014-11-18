using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using Weborb.Samples.Email.Pop3;
using Weborb.Samples.Email.Entities;

namespace Weborb.Samples.Email
{
    public class ServerSettings
    {
        private ServerSettings() {
        }

        public static Pop3MimeClient ConnectToPop3(ServerSettingsInfo info) {
            
            Pop3MimeClient pop3 = new Pop3MimeClient(
                info.Host,
                info.Port,
                info.ConnectionType == ServerSettingsInfo.CONNECTION_TYPE_SECURE_TLS,
                info.UserName,
                info.UserPassword);
            
            pop3.ReadTimeout = 30000; //30 seconds
            pop3.Connect();

            return pop3;
        }

        public static SmtpClient ConnectToSmtp(ServerSettingsInfo info) {
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = info.Host;
            smtp.Port = info.Port;
            smtp.Timeout = 30000; //30 seconds
            smtp.EnableSsl = info.ConnectionType == ServerSettingsInfo.CONNECTION_TYPE_SECURE_TLS;
            smtp.Credentials = new NetworkCredential(info.UserName, info.UserPassword);
            
            return smtp;
        }
        
        public static int Create(IDbConnection connection, ServerSettingsInfo settingsInfo) {
            
            // Insert settingsInfo into db.
            string sql = string.Format(DataAccessBrigde.SQL_INSERT_SETTINGS,
                                       settingsInfo.Host, settingsInfo.Port,
                                       settingsInfo.ConnectionType,
                                       settingsInfo.UserName, settingsInfo.UserPassword);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);

            // Get settingsInfo primary key value.
            sql = DataAccessBrigde.SQL_SELECT_LAST_SETTINGID;
            using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                connection, CommandType.Text, sql)) {
                if (rdr.Read()) {
                    return rdr.GetInt32(0);
                } else {
                    throw new Exception("Reading server settings Id failed.");
                }
            }
        }

        public static ServerSettingsInfo Retrieve(int settingsId) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_SELECT_SETTING_BY_SETTINGS_ID,
                                           settingsId);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql)) {
                    if (rdr.Read()) {
                        ServerSettingsInfo result = new ServerSettingsInfo();
                        result.Host = GetString(rdr, "host");
                        result.ConnectionType = GetString(rdr, "connectiontype");                        
                        result.Port = rdr.GetInt32(rdr.GetOrdinal("port"));
                        result.Id = rdr.GetInt32(rdr.GetOrdinal("serversettingsid"));
                        result.UserName = GetString(rdr, "username");
                        result.UserPassword = GetString(rdr, "userpassword");
                        return result;
                    } else {
                        throw new Exception("Settings not found.");
                    }
                }
            }
        }

        public static void Update(ServerSettingsInfo settingsInfo) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_UPDATE_SETTINGS,
                                           settingsInfo.Host, settingsInfo.Port,
                                           settingsInfo.ConnectionType,
                                           settingsInfo.UserName,
                                           settingsInfo.UserPassword, settingsInfo.Id);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }

        public static void Update(IDbConnection connection, ServerSettingsInfo settingsInfo) {
            string sql = string.Format(DataAccessBrigde.SQL_UPDATE_SETTINGS,
                                       settingsInfo.Host, settingsInfo.Port,
                                       settingsInfo.ConnectionType,
                                       settingsInfo.UserName,
                                       settingsInfo.UserPassword, settingsInfo.Id);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
        }

        public static void Delete(int settingsId) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_DELETE_SETTINGS, settingsId);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }

        private static string GetString(IDataReader reader, string columnName) {
            int columnId = reader.GetOrdinal(columnName);

            return (reader.IsDBNull(columnId))
                ? ""
                : reader.GetString(columnId);
        }
        
    }
}