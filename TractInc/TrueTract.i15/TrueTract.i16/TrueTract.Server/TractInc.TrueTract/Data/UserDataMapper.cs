using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class UserDataMapper
    {
        private const string SQL_SELECT =
            @"SELECT UserId, Login, FirstName, LastName, PhoneNumber, Password, Email, IsActive, HackingAttempts, NewTracts 
                FROM [User] ";

        private const string SQL_SELECT_BY_LOGIN = SQL_SELECT + " WHERE Login = @Login";
        private const string SQL_SELECT_BY_USER_ID = SQL_SELECT + " WHERE UserId = @UserId";

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
                NewTracts = {8} 
            WHERE UserId = {9}
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
                NewTracts
                ) 
            VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', {6}, {7}, {8})

            select scope_identity();
        ";

        public UserInfo GetUserByLogin(SqlTransaction tran, string login)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Login", login));
            
            List<UserInfo> list = Select(tran, SQL_SELECT_BY_LOGIN, paramList);
            
            if (list.Count == 0)
            {
                return null;
            } else
            {
                return list[0];
            }            
        }

        public UserInfo GetUserById(SqlTransaction tran, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            
            List<UserInfo> list = Select(tran, SQL_SELECT_BY_USER_ID, paramList);
            
            if (list.Count == 0)
            {
                return null;
            } else
            {
                return list[0];
            }            
        }

        private List<UserInfo> Select(SqlTransaction tran, string sqlQuery, List<SqlParameter> paramList)
        {
            List<UserInfo> result = new List<UserInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sqlQuery, paramList.ToArray())) {
                
                while (dataReader.Read()) {

                    UserInfo userInfo = new UserInfo(
                        dataReader.GetInt32(0),
                        dataReader.GetString(1),
                        dataReader.GetString(2),
                        dataReader.GetString(3),
                        dataReader.GetString(4),
                        dataReader.GetString(5),
                        dataReader.GetString(6),
                        dataReader.GetSqlBoolean(7).IsTrue,
                        dataReader.GetInt32(8),
                        dataReader.GetInt32(9)
                        );
                    
                    result.Add(userInfo);
                }
            }
            
            return result;
        }

        public UserInfo Create(SqlTransaction tran, UserInfo userInfo)
        {
            string sql = String.Format(SQL_CREATE,
                                       userInfo.Login, userInfo.FirstName, userInfo.LastName,
                                       userInfo.PhoneNumber, userInfo.Password, userInfo.Email,
                                       userInfo.IsActive ? 1 : 0,
                                       userInfo.HackingAttempts, userInfo.NewTracts);

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
                              userInfo.NewTracts, userInfo.UserId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
    }
}