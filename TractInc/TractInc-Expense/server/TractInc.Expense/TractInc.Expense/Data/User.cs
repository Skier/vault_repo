using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class User
    {

        private static User c_User = new User();

        public static User GetInstance()
        {
            return c_User;
        }

        private User()
        {
        }

        private const string SQL_SELECT_ALL = @"
            select  [UserId],
                    [Login],
                    [Password],
                    [Email],
                    [IsActive],
                    [HackingAttempts],
                    [Deleted]
            from    [User]";

        public List<UserDataObject> GetUsers(SqlTransaction tran)
        {
            List<UserDataObject> result = new List<UserDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_ALL, parms))
            {
                while (dataReader.Read())
                {
                    UserDataObject userInfo = new UserDataObject();

                    userInfo.UserId = (int)dataReader.GetValue(0);
                    userInfo.Login = (string)dataReader.GetValue(1);
                    userInfo.Password = (string)dataReader.GetValue(2);
                    userInfo.Email = (string)dataReader.GetValue(3);
                    userInfo.IsActive = (bool)dataReader.GetValue(4);
                    userInfo.HackingAttempts = (int)dataReader.GetValue(5);
                    userInfo.Deleted = (bool)dataReader.GetValue(6);

                    result.Add(userInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_ID = @"
            select  [UserId],
                    [Login],
                    [Password],
                    [Email],
                    [IsActive],
                    [HackingAttempts],
                    [Deleted]
            from    [User]
            where   [UserId] = @UserId";

        public UserDataObject GetUser(SqlTransaction tran, int userId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@UserId", userId) };

            UserDataObject userInfo = null;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    userInfo = new UserDataObject();

                    userInfo.UserId = (int)dataReader.GetValue(0);
                    userInfo.Login = (string)dataReader.GetValue(1);
                    userInfo.Password = (string)dataReader.GetValue(2);
                    userInfo.Email = (string)dataReader.GetValue(3);
                    userInfo.IsActive = (bool)dataReader.GetValue(4);
                    userInfo.HackingAttempts = (int)dataReader.GetValue(5);
                    userInfo.Deleted = (bool)dataReader.GetValue(6);
                }
            }

            return userInfo;
        }

        private const string SQL_SELECT_BY_ASSET_ID = @"
            select  u.[UserId],
                    u.[Login],
                    u.[Password],
                    u.[Email],
                    u.[IsActive],
                    u.[HackingAttempts],
                    u.[Deleted]
            from    [User] u
                    inner join [UserAsset] ua
                            on ua.[UserId] = u.[UserId]
            where   ua.[AssetId] = @AssetId";

        public UserDataObject GetUserByAsset(SqlTransaction tran, int assetId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@AssetId", assetId) };

            UserDataObject userInfo = null;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ASSET_ID, parms))
            {
                if (dataReader.Read())
                {
                    userInfo = new UserDataObject();

                    userInfo.UserId = (int)dataReader.GetValue(0);
                    userInfo.Login = (string)dataReader.GetValue(1);
                    userInfo.Password = (string)dataReader.GetValue(2);
                    userInfo.Email = (string)dataReader.GetValue(3);
                    userInfo.IsActive = (bool)dataReader.GetValue(4);
                    userInfo.HackingAttempts = (int)dataReader.GetValue(5);
                    userInfo.Deleted = (bool)dataReader.GetValue(6);
                }
            }

            return userInfo;
        }

        private const string SQL_INSERT = @"
        insert  into [User]
              ( [Login],
                [Password],
                [Email],
                [IsActive],
                [HackingAttempts],
                [Deleted] )
        values( @Login,
                @Password,
                @Email,
                @IsActive,
                @HackingAttempts,
                @Deleted);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, UserDataObject userInfo)
        {
            DbParameter[] parms = new DbParameter[6] {
                new SqlParameter("@Login",           userInfo.Login),
                new SqlParameter("@Password",        userInfo.Password),
                new SqlParameter("@Email",           userInfo.Email),
                new SqlParameter("@IsActive",        userInfo.IsActive),
                new SqlParameter("@HackingAttempts", userInfo.HackingAttempts),
                new SqlParameter("@Deleted",         userInfo.Deleted)
            };

            userInfo.UserId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_SELECT_BY_LOGIN = @"
            SELECT UserId, Login, Password, Email, IsActive, HackingAttempts, Deleted
              FROM [User] 
             WHERE Login = @Login
               AND Deleted = 0";

        public UserDataObject GetUserByLogin(SqlTransaction tran, string login)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@Login", login)
            };

            UserDataObject userInfo = null;
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_LOGIN, parms))
            {
                if (dataReader.Read())
                {
                    userInfo = new UserDataObject();

                    userInfo.UserId = (int)dataReader.GetValue(0);
                    userInfo.Login = (string)dataReader.GetValue(1);
                    userInfo.Password = (string)dataReader.GetValue(2);
                    userInfo.Email = (string)dataReader.GetValue(3);
                    userInfo.IsActive = (bool)dataReader.GetValue(4);
                    userInfo.HackingAttempts = (int)dataReader.GetValue(5);
                    userInfo.Deleted = (bool)dataReader.GetValue(6);
                }
            }

            return userInfo;
        }

        private const string SQL_UPDATE = @"
        update  [User]
        set     [Login] = @Login,
                [Password] = @Password,
                [Email] = @Email,
                [IsActive] = @IsActive,
                [HackingAttempts] = @HackingAttempts,
                [Deleted] = @Deleted
        where   [UserId] = @UserId";

        public void Update(SqlTransaction tran, UserDataObject userInfo)
        {
            DbParameter[] parms = new DbParameter[7] {
                new SqlParameter("@Login",           userInfo.Login),
                new SqlParameter("@Password",        userInfo.Password),
                new SqlParameter("@Email",           userInfo.Email),
                new SqlParameter("@IsActive",        userInfo.IsActive),
                new SqlParameter("@HackingAttempts", userInfo.HackingAttempts),
                new SqlParameter("@Deleted",         userInfo.Deleted),
                new SqlParameter("@UserId",          userInfo.UserId)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

    }

}
