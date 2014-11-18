using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.MapOptix.Entity;

namespace TractInc.MapOptix.Data
{
    class UserDataMapper
    {
        private const string SQL_CREATE =
            @"
            INSERT INTO db_ddladmin.[user_def_mapoptix] (
                [user_id],
	            [profile_id],
                [user_admin],
                [user_active]
                ) 
            VALUES ( {0}, {1}, {2}, {3} )
        ";

        private const string SQL_ADD_PROFILE =
            @"
            INSERT INTO db_ddladmin.[profile_user_def] (
                [profile_id],
	            [user_id]
                ) 
            VALUES ( {0}, {1} )
        ";

        public UserInfo Create(SqlConnection conn, UserInfo user)
        {
            string sql = String.Format(SQL_CREATE,
                                       user.UserId, user.ProfileId,
                                       user.UserAdmin, user.UserActive);

            SQLHelper.ExecuteNonQuery(conn, CommandType.Text, sql, null);

            return user;
        }

        public UserInfo AddProfile(SqlConnection conn, UserInfo user)
        {
            string sql = String.Format(SQL_ADD_PROFILE,
                                       user.ProfileId, user.UserId);

            SQLHelper.ExecuteNonQuery(conn, CommandType.Text, sql, null);

            return user;
        }

    }
}
