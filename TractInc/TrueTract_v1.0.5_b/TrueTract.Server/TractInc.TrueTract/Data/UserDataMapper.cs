using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class UserDataMapper
    {
        private const string DEFAULT_SITE = "Barnett Shale";
        
        private const string SQL_SELECT_BY_LOGIN =
            @"
            SELECT UserId, Login, FirstName, LastName, PhoneNumber, Password, Email, IsActive, 
                   HackingAttempts, NewTracts, DefaultSite 
              FROM [User] 
             WHERE Login = @Login";

        private const string SQL_UPDATE =
            @"
            UPDATE [User] set 
                Login = Login, 
                FirstName = @FirstName,
                LastName = @LastName,
                PhoneNumber = @PhoneNumber,
                Password = @Password, 
                Email = @Email, 
                IsActive = @IsActive,
                HackingAttempts = @HackingAttempts,
                NewTracts = @NewTracts, 
                DefaultSite = @DefaultSite
            WHERE UserId = @UserId
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
            VALUES ( 
                @Login,
                @FirstName, 
                @LastName, 
                @PhoneNumber,
                @Password,
                @Email,
                @IsActive,
                @HackingAttempts,
                @NewTracts,
                @DefaultSite
            )

            select scope_identity();
        ";

        public UserInfo GetUserByLogin(SqlTransaction tran, string login)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Login", login));

            UserInfo userInfo = null;
            using (
                SqlDataReader dataReader =
                    SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_LOGIN, paramList.ToArray()))
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
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Login", userInfo.Login));
            paramList.Add(new SqlParameter("@FirstName", userInfo.FirstName));
            paramList.Add(new SqlParameter("@LastName", userInfo.LastName));
            paramList.Add(new SqlParameter("@PhoneNumber", userInfo.PhoneNumber));
            paramList.Add(new SqlParameter("@Password", userInfo.Password));
            paramList.Add(new SqlParameter("@Email", userInfo.Email));
            paramList.Add(new SqlParameter("@IsActive", userInfo.IsActive));
            paramList.Add(new SqlParameter("@HackingAttempts", userInfo.HackingAttempts));
            paramList.Add(new SqlParameter("@NewTracts", userInfo.NewTracts));
            paramList.Add(new SqlParameter("@DefaultSite", (null != userInfo.DefaultSite) ? userInfo.DefaultSite : DEFAULT_SITE));

            userInfo.UserId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

            return userInfo;
        }

        public void Update(SqlTransaction tran, UserInfo userInfo)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Login", userInfo.Login));
            paramList.Add(new SqlParameter("@FirstName", userInfo.FirstName));
            paramList.Add(new SqlParameter("@LastName", userInfo.LastName));
            paramList.Add(new SqlParameter("@PhoneNumber", userInfo.PhoneNumber));
            paramList.Add(new SqlParameter("@Password", userInfo.Password));
            paramList.Add(new SqlParameter("@Email", userInfo.Email));
            paramList.Add(new SqlParameter("@IsActive", userInfo.IsActive));
            paramList.Add(new SqlParameter("@HackingAttempts", userInfo.HackingAttempts));
            paramList.Add(new SqlParameter("@NewTracts", userInfo.NewTracts));
            paramList.Add(new SqlParameter("@DefaultSite", (null != userInfo.DefaultSite) ? userInfo.DefaultSite : DEFAULT_SITE));
            paramList.Add(new SqlParameter("@UserId", userInfo.UserId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }
    }
}