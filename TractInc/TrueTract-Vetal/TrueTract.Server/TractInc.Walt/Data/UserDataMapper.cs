using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.Walt.Entity;

namespace TractInc.Walt.Data
{
    class UserDataMapper
    {
        private const string SQL_EXISTS =
            @"
            SELECT * from db_ddladmin.[user_def]
             WHERE [user_name] = '{0}'
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO db_ddladmin.[user_def] (
                [user_id],
                [user_name],
                [password], 
                [first_name], 
                [last_name],
                [email],
                [phone_num]
                ) 
            SELECT MAX([user_id]) + 1, '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'
              FROM db_ddladmin.[user_def]
        ";

        private const string SQL_GET_MAX_ID =
            @"
            SELECT MAX([user_id])
              FROM db_ddladmin.[user_def] 
        ";

        private const string SQL_UPDATE_MAX_ID =
            @"
            UPDATE db_ddladmin.[ids] 
               SET LastValue = {0} 
             WHERE ColumnName = '{1}' 
        ";

        public UserInfo Create(SqlConnection conn, UserInfo user)
        {
            string sql = String.Format(SQL_CREATE,
                                       user.UserName, user.Password,
                                       user.FirstName, user.LastName,
                                       user.Email, user.PhoneNum);

            SQLHelper.ExecuteNonQuery(conn, CommandType.Text, sql, null);

            user.UserId = GetMaxId(conn);
            
            UpdateMaxId(conn, user.UserId);

            return user;
        }

        public bool Exists(SqlConnection conn, string userName)
        {
            string sql = String.Format(SQL_EXISTS,
                                       userName);

            using (SqlDataReader rdr = SQLHelper.ExecuteReader(conn, CommandType.Text, sql, null))
            {
                return rdr.Read();
            }

        }

        private void UpdateMaxId(SqlConnection conn, int id)
        {
            string sql = String.Format(SQL_UPDATE_MAX_ID,
                                       id, UserInfo.IDS_COLUMN_NAME);

            SQLHelper.ExecuteNonQuery(conn, CommandType.Text, sql, null);
        }

        private int GetMaxId(SqlConnection conn)
        {
            string sql = String.Format(SQL_GET_MAX_ID);

            return int.Parse(SQLHelper.ExecuteScalar(conn, CommandType.Text, sql, null).ToString());
        }

    }
}
