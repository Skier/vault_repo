using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class UserDataMapper
    {
        private const string SQL_SELECT_BY_LOGIN =
            @"
            SELECT UserId, Login, FirstName, LastName, PhoneNumber, Password, Email, IsActive, 
                   HackingAttempts, NewTracts, DefaultSite 
              FROM [User] 
             WHERE Login = '{0}'";

        private const string SQL_UPDATE =
            @"
            UPDATE [User] set 
                Login = '{0}', 
                FirstName = '{1}',
                LastName = '{2}',
                PhoneNumber = '{3}',
                Password = '{4}', 
                Email = '{5}', 
                IsActive = '{6}',
                HackingAttempts = {7},
                NewTracts = {8}, 
                DefaultSite = '{9}'
            WHERE UserId = {10}
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [User] (
                Login,
                FirstName, 
                LastName, 
                PhoneNumber,
                Password,
                Email,
                IsActive,
                HackingAttempts,
                NewTracts,
                DefaultSite
                ) 
            VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8}, '{9}')

            select scope_identity();
        ";

        public UserInfo GetUserByLogin(SqlTransaction tran, string login)
        {
            string sql = String.Format(SQL_SELECT_BY_LOGIN, login);

            UserInfo userInfo = null;
            using (
                SqlDataReader dataReader =
                    SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                if (dataReader.Read())
                {
                    userInfo = new UserInfo(
                        dataReader.GetInt32(0),
                        dataReader.GetString(1),
                        dataReader.GetString(2),
                        dataReader.GetString(3),
                        dataReader.GetString(4),
                        dataReader.GetString(5),
                        dataReader.GetString(6),
                        dataReader.GetSqlBoolean(7).IsTrue,
                        dataReader.GetInt32(8),
                        dataReader.GetInt32(9),
                        dataReader.GetString(10)
                        );
                }
            }

            return userInfo;
        }

        public UserInfo Create(SqlTransaction tran, UserInfo userInfo)
        {
            string sql = String.Format(SQL_CREATE,
                                       userInfo.Login, userInfo.FirstName, userInfo.LastName,
                                       userInfo.PhoneNumber, userInfo.Password, userInfo.Email,
                                       userInfo.IsActive ? 1 : 0,
                                       userInfo.HackingAttempts, userInfo.NewTracts, userInfo.DefaultSite);

            userInfo.UserId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return userInfo;
        }

        public void Update(SqlTransaction tran, UserInfo userInfo)
        {
            string sql =
                String.Format(SQL_UPDATE, userInfo.Login, userInfo.FirstName, userInfo.LastName,
                              userInfo.PhoneNumber, userInfo.Password, userInfo.Email,
                              userInfo.IsActive ? 1 : 0, userInfo.HackingAttempts,
                              userInfo.NewTracts, userInfo.DefaultSite, userInfo.UserId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
    }
}