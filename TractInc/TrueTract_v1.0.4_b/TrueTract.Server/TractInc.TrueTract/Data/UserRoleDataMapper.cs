using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    class UserRoleDataMapper
    {
        private const string SQL_CREATE =
            @"
            INSERT INTO [UserRole] (
                UserId,
                RoleId
                ) 
            VALUES ( {0}, {1})

            select scope_identity();
        ";

        private const string SQL_REMOVE =
            @"
            DELETE FROM [UserRole] 
             WHERE UserRoleId = {0}
        ";

        public UserRoleInfo Create(SqlTransaction tran, UserRoleInfo userRoleInfo)
        {
            string sql = String.Format(SQL_CREATE,
                                       userRoleInfo.UserId, userRoleInfo.RoleId);

            userRoleInfo.UserId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return userRoleInfo;
        }

        public UserRoleInfo Remove(SqlTransaction tran, UserRoleInfo userRoleInfo)
        {
            string sql = String.Format(SQL_REMOVE,
                                       userRoleInfo.UserId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);

            return userRoleInfo;
        }

    }
}
