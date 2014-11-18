using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.DeedPro.Entity;

namespace TractInc.DeedPro.Data
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
        
        public UserInfo GetUserByLogin(SqlTransaction tran, string login) {
            string sql = String.Format(SQL_SELECT_BY_LOGIN, login);
            
            UserInfo userInfo = null;
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null )) {
                if (dataReader.Read()) {
                    userInfo = new UserInfo(
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
            
            return userInfo;
        }
        
        public void Update(SqlTransaction tran, UserInfo userInfo) {
            string sql = String.Format(SQL_UPDATE, userInfo.Login, userInfo.Password, userInfo.Email, 
                userInfo.IsActive ? 1 : 0, userInfo.HackingAttempts, userInfo.NewTracts, userInfo.UserId);
            
            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
    }
}
