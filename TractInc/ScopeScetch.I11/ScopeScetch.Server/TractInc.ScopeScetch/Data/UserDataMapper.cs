using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch.Data
{
    internal class UserDataMapper
    {
        
        private const string SQL_SELECT_BY_LOGIN = @"
            SELECT UserId, Login, Password, Email, IsActive, HackingAttempts, NewTracts 
              FROM [User] 
             WHERE Login = '{0}'";
        
        private const string SQL_UPDATE = @"
            UPDATE [User] set 
                Login = '{0}', 
                Password = '{1}', 
                Email = '{2}', 
                IsActive = '{3}',
                HackingAttempts = {4},
                NewTracts = {5} 
            WHERE UserId = {6}
        ";
        
        public User GetUserByLogin(SqlTransaction tran, string login) {
            string sql = String.Format(SQL_SELECT_BY_LOGIN, login);
            
            User user = null;
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null )) {
                if (dataReader.Read()) {
                    user = new User(
                        dataReader.GetInt32(0),
                        dataReader.GetString(1),
                        dataReader.GetString(2),
                        dataReader.GetString(3),
                        dataReader.GetSqlBoolean(4).IsTrue,
                        dataReader.GetInt32(5),
                        dataReader.GetInt32(6)
                        );
                }
            }
            
            return user;
        }
        
        public void Update(SqlTransaction tran, User user) {
            string sql = String.Format(SQL_UPDATE, user.Login, user.Password, user.Email, 
                user.IsActive, user.HackingAttempts, user.NewTracts, user.UserId);
            
            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
    }
}
