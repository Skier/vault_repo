using System;
using System.Data;
using Weborb.Samples.Email.Entities;

namespace Weborb.Samples.Email
{
    public class Account
    {
        internal static int Create(IDbConnection connection, AccountInfo account) {
            
            //Check if account with the given email already exists
            if (null != RetreiveByEmail(connection, account.Email)) {
                throw new Exception(string.Format("Account with email [{0}] already exists.", account.Email));
            }
            
            // Insert account into database.
            string sql = string.Format(DataAccessBrigde.SQL_INSERT_ACCOUNT, account.Email,
                                       account.Pop3SettingsId, account.SmtpSettingsId);
            DataAccessBrigde.ExecuteNonQuery(connection, CommandType.Text, sql);

            // Get account primary key value.
            int accountId;
            
            using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                connection, CommandType.Text, DataAccessBrigde.SQL_SELECT_LAST_ACCOUNTID)) {
                if (rdr.Read()) {
                    accountId = rdr.GetInt32(0);
                } else {
                    throw new Exception("Reading account Id failed.");
                }
            }

            return accountId;
        }

        internal static AccountInfo RetreiveByEmail(IDbConnection connection, string email) {
            string sql = string.Format(DataAccessBrigde.SQL_SELECT_ACCOUNT_BY_EMAIL, email);
            
            AccountInfo result = null;
            
            using (IDataReader rdr = DataAccessBrigde.ExecuteReader(
                connection, CommandType.Text, sql)) {
                if (rdr.Read()) {
                    result = new AccountInfo(rdr.GetInt32(0), rdr.GetString(1),
                                             rdr.GetInt32(2), rdr.GetInt32(3));
                }
            }
            
            return result;            
        }

        internal static void Update(IDbConnection connection, AccountInfo accountInfo) {

            //Check if account with the given email already exists
            AccountInfo a = RetreiveByEmail(connection, accountInfo.Email);
            if (null != a && a.Id != accountInfo.Id) {
                throw new Exception(string.Format("Unable to change email address. Account with email [{0}] already exists.", accountInfo.Email));
            }
            
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