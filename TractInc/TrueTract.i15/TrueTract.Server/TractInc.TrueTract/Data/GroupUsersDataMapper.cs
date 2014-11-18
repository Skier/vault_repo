using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class GroupUsersDataMapper
    {

        #region Constants

        private const string SQL_CREATE = @"
            INSERT INTO [GroupUsers] ( GroupId, UserId )
                 VALUES ( @GroupId, @UserId )

            select scope_identity();
        ";

        private const string SQL_DELETE = @"
            DELETE [GroupUsers]
             WHERE GroupID = @GroupId 
               abd UserId = @UserId
        ";

        #endregion

        #region Methods

        public int Create(SqlTransaction tran, int userId, int groupId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@GroupId", groupId));

            return int.Parse(SQLHelper.ExecuteScalar(
                                 tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        }

        public void Delete(SqlTransaction tran, int userId, int groupId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@GroupId", groupId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        #endregion

    }
}
