using System;
using System.Collections;
using System.Data;
using Weborb.Samples.Email.Entities;

namespace Weborb.Samples.Email
{
    public class Account
    {
        internal static int Create(IDbConnection connection, AccountInfo accountInfo) {
            // Insert accountInfo into db.
            string sql = string.Format(DataAccessBrigde.SQL_INSERT_ACCOUNT, accountInfo.Email,
                                       accountInfo.Pop3SettingsId, accountInfo.SmtpSettingsId);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);

            // Get settings primary key value.
            int accountId;
            using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                connection, CommandType.Text, DataAccessBrigde.SQL_SELECT_LAST_ACCOUNTID)) {
                if (rdr.Read()) {
                    accountId = rdr.GetInt32(0);
                } else {
                    throw new Exception("Reading server settings Id failed.");
                }
            }

            return accountId;
        }

        private static AccountInfo Retreive(string sql) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql)) {
                    if (rdr.Read()) {
                        return new AccountInfo(rdr.GetInt32(0), rdr.GetString(1),
                                               rdr.GetInt32(2), rdr.GetInt32(3));
                    }
                }

                throw new Exception("Account not found.");
            }
        }

        internal static AccountInfo RetreiveById(int accountId) {
            string sql = string.Format(DataAccessBrigde.SQL_SELECT_ACCOUNT_BY_ACCOUNTID, accountId);
            return Retreive(sql);
        }

        internal static AccountInfo RetreiveByEmail(string email) {
            string sql = string.Format(DataAccessBrigde.SQL_SELECT_ACCOUNT_BY_EMAIL, email);
            return Retreive(sql);
        }

        internal static AccountInfo[] RetreiveAll() {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_SELECT_ALL_ACCOUNTS);
                ArrayList container = new ArrayList();

                using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                    connection, CommandType.Text, sql)) {
                    while (rdr.Read()) {
                        container.Add(new AccountInfo(rdr.GetInt32(0), rdr.GetString(1),
                                                      rdr.GetInt32(2), rdr.GetInt32(3)));
                    }
                }

                AccountInfo[] accounts = new AccountInfo[container.Count];
                container.CopyTo(accounts);

                return accounts;
            }
        }

        internal static void Update(AccountInfo accountInfo) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_UPDATE_ACCOUNT,
                                           accountInfo.Email, accountInfo.Id);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }

        internal static void Update(IDbConnection connection, AccountInfo accountInfo) {
            string sql = string.Format(DataAccessBrigde.SQL_UPDATE_ACCOUNT,
                                       accountInfo.Email, accountInfo.Id);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
        }

        internal static void Delete(int accountId) {
            using (IDbConnection connection = DataAccessBrigde.CreateConnection()) {
                connection.Open();
                string sql = string.Format(DataAccessBrigde.SQL_DELETE_ACCOUNT, accountId);
                DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);
            }
        }
    }
}