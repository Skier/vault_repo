using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TractInc.Expense.Entity;

namespace TractInc.Expense.Data
{

    public class UserRole
    {

        private static UserRole c_UserRole = new UserRole();

        public static UserRole GetInstance()
        {
            return c_UserRole;
        }

        private UserRole()
        {
        }

        private const string SQL_SELECT_BY_USER_ID = @"
            select  [UserRoleId],
                    [UserId],
                    [RoleId]
            from    [UserRole]
            where   [UserId] = @UserId";

        public UserRoleDataObject GetUserRole(SqlTransaction tran, int userId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@UserId", userId) };

            UserRoleDataObject userRoleInfo = null;

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_USER_ID, parms))
            {
                if (dataReader.Read())
                {
                    userRoleInfo = new UserRoleDataObject();

                    userRoleInfo.UserRoleId = (int)dataReader.GetValue(0);
                    userRoleInfo.UserId = (int)dataReader.GetValue(1);
                    userRoleInfo.RoleId = (int)dataReader.GetValue(2);
                }
            }

            return userRoleInfo;
        }

        private const string SQL_INSERT = @"
        insert  into [UserRole]
              ( [UserId],
                [RoleId] )
        values( @UserId,
                @RoleId );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, UserRoleDataObject userRoleInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@UserId", userRoleInfo.UserId),
                new SqlParameter("@RoleId", userRoleInfo.RoleId)
            };

            userRoleInfo.UserRoleId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

    }

}
