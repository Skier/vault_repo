using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class GroupDataMapper
    {

        #region Constants

        private const string SQL_SELECT = @"
            SELECT [Group].GroupId, [Group].GroupName
              FROM [Group]
        ";

        private const string SQL_SELECT_BY_GROUP_ID = SQL_SELECT + @"
            WHERE GroupId = @GroupId
        ";

        private const string SQL_SELECT_BY_USER_ID = SQL_SELECT + @"
            INNER JOIN GroupUsers on GroupUsers.GroupId = [Group].GroupId
            WHERE GroupUsers.UserId = @UserId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [Group] ( GroupName ) 
                 VALUES ( @GroupName )

            select scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Group] set 
                GroupName = @GroupName
             WHERE GroupID = @GroupId
        ";

        private const string SQL_DELETE = @"
            DELETE [Group]
             WHERE GroupID = @GroupId
        ";
        
        #endregion

        #region Methods

        public UserGroupInfo SelectByGroupId(SqlTransaction tran, int groupId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));

            List<UserGroupInfo> list = Select(tran, SQL_SELECT_BY_GROUP_ID, paramList);
            
            if (list.Count == 0)
            {
                return null;
            } else
            {
                return list[0];
            }
        }

        public List<UserGroupInfo> SelectByUserId(SqlTransaction tran, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));

            return Select(tran, SQL_SELECT_BY_USER_ID, paramList);
        }

        public int Create(SqlTransaction tran, string groupName)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupName", groupName));
            
            return int.Parse(SQLHelper.ExecuteScalar(
                                 tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        }

        public void Update(SqlTransaction tran, int groupId, string groupName)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupName", groupName));
            paramList.Add(new SqlParameter("@GroupId", groupId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }

        public void Delete(SqlTransaction tran, int groupId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        private List<UserGroupInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList) {
            
            List<UserGroupInfo> result = new List<UserGroupInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray())) {
                
                while (dataReader.Read()) {
                    UserGroupInfo groupInfo = new UserGroupInfo();
                    groupInfo.groupId = dataReader.GetInt32(0);
                    groupInfo.groupName = dataReader.GetString(1);
                    groupInfo.systemGroup = false;
                    
                    result.Add(groupInfo);
                }
            }

            return result;
        }        
        #endregion

        
    }
}
