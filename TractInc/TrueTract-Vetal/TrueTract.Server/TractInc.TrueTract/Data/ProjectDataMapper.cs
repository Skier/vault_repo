using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ProjectDataMapper
    {

        #region Constants

        private const string SQL_GET_BY_CLIENT_AND_USERID = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
               and exists (select 1 
                             from [AssetAssignment]
                                join [User] on [User].AssetId = [AssetAssignment].AssetId
                                 and [AssetAssignment].ProjectId = [Project].ProjectId
                                 and [User].UserId = @UserId
                           )
        ";

        #endregion

        #region Methods

        public List<ProjectInfo> GetByClientAndUser(SqlTransaction tran, int clientId, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@ClientId", clientId));
            
            return Select(tran, SQL_GET_BY_CLIENT_AND_USERID, paramList);
        }

        private List<ProjectInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
        {
            List<ProjectInfo> result = new List<ProjectInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    ProjectInfo info = new ProjectInfo();
                    info.ProjectId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectId"));
                    info.ClientId = dataReader.GetInt32(dataReader.GetOrdinal("ClientId"));
                    info.Name = dataReader.GetSqlString(dataReader.GetOrdinal("Name")).ToString();
                    info.ShortName = dataReader.GetSqlString(dataReader.GetOrdinal("ShortName")).ToString();
                    result.Add(info);
                }
            }

            return result;
        }
        
        #endregion

    }
}
