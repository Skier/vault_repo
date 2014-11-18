using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class ProjectTabDataMapper
{

    #region Constants

    private const string SQL_GET_BY_PPROJECT_ID = @"
        SELECT * 
          FROM ProjectTab 
         WHERE ProjectId = @ProjectId
    ";

    #endregion

    #region Methods

    public List<ProjectTabInfo> GetByProjectId(SqlTransaction tran, int projectId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectId", projectId));
        
        return Select(tran, SQL_GET_BY_PPROJECT_ID, paramList);
    }

    private List<ProjectTabInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<ProjectTabInfo> result = new List<ProjectTabInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                ProjectTabInfo info = new ProjectTabInfo();
                info.ProjectTabId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabId"));
                info.ProjectId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectId"));
                info.Name = dataReader.GetSqlString(dataReader.GetOrdinal("Name")).ToString();
                info.Description = dataReader.GetSqlString(dataReader.GetOrdinal("Description")).ToString();
                result.Add(info);
            }
        }

        return result;
    }
    
    #endregion

}
}
