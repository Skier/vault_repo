using System;
using System.Data;
using Indy.Sockets;
using Weborb.Samples.Email.Entities;

namespace Weborb.Samples.Email
{
    public class ServerSettings
    {
        private ServerSettings()
        {
        }
        
        static public POP3 ConnectToPop3(ServerSettingsInfo info)
        {
            POP3 pop3Protocol = new POP3();
            
            pop3Protocol.Username = info.UserName;
            pop3Protocol.Password = info.Password;

            if (info.ConnectionType == ServerSettingsInfo.SECURE_TLS_CONNECTION_TYPE) {
                //TODO: implement secure TLS connection support
            }

            pop3Protocol.Connect(info.Host, info.Port);

            return pop3Protocol;
        }

        static public SMTP ConnectToSmtp(ServerSettingsInfo info)
        {
            SMTP smtpProtocol = new SMTP();

            smtpProtocol.Username = info.UserName;
            smtpProtocol.Password = info.Password;

            if (info.ConnectionType == ServerSettingsInfo.SECURE_TLS_CONNECTION_TYPE) {
                //TODO: implement secure TLS connection support
            }
            
            smtpProtocol.Connect(info.Host, info.Port);

            return smtpProtocol;
        }

        static public int Create(IDbConnection connection, ServerSettingsInfo settingsInfo)
        {
            // Insert settingsInfo into db.
            string sql = string.Format(DataAccessBrigde.SQL_INSERT_SETTINGS, 
                                       settingsInfo.Host, settingsInfo.Port, 
                                       settingsInfo.ConnectionType,
                                       settingsInfo.UserName, settingsInfo.Password);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);

            // Get settingsInfo primary key value.
            sql = DataAccessBrigde.SQL_SELECT_LAST_SETTINGID;
            using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                connection, CommandType.Text, sql))
                if (rdr.Read())
                    return rdr.GetInt32(0);
                else
                    throw new Exception("Reading server settings id failed.");
        }

        static public ServerSettingsInfo Retrieve(int settingsId)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_SELECT_SETTING_BY_SETTINGS_ID, 
                                           settingsId);
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql))
                    if (rdr.Read())
                        return new ServerSettingsInfo(rdr.GetInt32(0), rdr.GetString(1),
                                                      rdr.GetInt32(2), rdr.GetString(3),
                                                      rdr.GetString(4), 
                                                      rdr.GetString(5));
                    else
                        throw new Exception("Settings not found.");
            }
        }

        static public void Update(ServerSettingsInfo settingsInfo)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_UPDATE_SETTINGS,
                                           settingsInfo.Host, settingsInfo.Port,
                                           settingsInfo.ConnectionType,
                                           settingsInfo.UserName,
                                           settingsInfo.Password, settingsInfo.Id);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }

        static public void Update(IDbConnection connection, ServerSettingsInfo settingsInfo) {

            string sql = string.Format(DataAccessBrigde.SQL_UPDATE_SETTINGS,
                                       settingsInfo.Host, settingsInfo.Port,
                                       settingsInfo.ConnectionType,
                                       settingsInfo.UserName,
                                       settingsInfo.Password, settingsInfo.Id);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);

        }
        
        static public void Delete(int settingsId)
        {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection())
            {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_DELETE_SETTINGS, settingsId);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }
    }
}
