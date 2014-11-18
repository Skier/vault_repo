using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class ProjectTabDataMapper
{

    #region Constants

    private const string SQL_GET_BY_PPROJECT_TAB_ID = @"
        SELECT * 
          FROM TT_ProjectTab 
         WHERE ProjectTabId = @ProjectTabId
    ";

    private const string SQL_GET_BY_PPROJECT_ID = @"
        SELECT * 
          FROM TT_ProjectTab 
         WHERE ProjectId = @ProjectId
      ORDER BY TabOrder
    ";

    private const string SQL_CREATE = @"
        INSERT INTO [TT_ProjectTab]
                   ([ProjectId],[Name], [Label], [TabOrder])
             VALUES (
                   @ProjectId,
                   @Name,
                   @Label,
                   @TabOrder)

        SELECT scope_identity();
    ";

    private const string SQL_UPDATE = @"
        UPDATE [TT_ProjectTab] set 
            ProjectId = @ProjectId,
            Name = @Name,
            Label = @Label,
            TabOrder = @TabOrder
        WHERE ProjectTabId = @ProjectTabId";
    
    private const string SQL_DELETE = @"
        DELETE [TT_ProjectTab] WHERE ProjectTabId = @ProjectTabId
    ";
    
    #endregion

    #region Methods

    public ProjectTabInfo GetById(SqlTransaction tran, int projectTabId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabId", projectTabId));

        List<ProjectTabInfo> result = Select(tran, SQL_GET_BY_PPROJECT_TAB_ID, paramList);

        if (result.Count > 0)
            return result[0];
        else
            return null;
    }

    public List<ProjectTabInfo> GetByProjectId(SqlTransaction tran, int projectId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectId", projectId));
        
        return Select(tran, SQL_GET_BY_PPROJECT_ID, paramList);
    }

    public ProjectTabInfo Create(SqlTransaction tran, ProjectTabInfo projectTab)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectID", projectTab.ProjectId));
        paramList.Add(new SqlParameter("@Name", projectTab.Name));
        paramList.Add(new SqlParameter("@Label", (null != projectTab.Label) ? projectTab.Label : SqlString.Null));
        paramList.Add(new SqlParameter("@TabOrder", projectTab.TabOrder));

        projectTab.ProjectTabId = int.Parse(
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        
        return projectTab;
    }

    public void Update(SqlTransaction tran, ProjectTabInfo projectTab)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabId", projectTab.ProjectTabId));
        paramList.Add(new SqlParameter("@ProjectID", projectTab.ProjectId));
        paramList.Add(new SqlParameter("@Name", projectTab.Name));
        paramList.Add(new SqlParameter("@Label", (null != projectTab.Label) ? projectTab.Label : SqlString.Null));
        paramList.Add(new SqlParameter("@TabOrder", projectTab.TabOrder));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
    }
    
    public void Delete(SqlTransaction tran, ProjectTabInfo projectTab)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabId", projectTab.ProjectTabId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
    }    

    private List<ProjectTabInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<ProjectTabInfo> result = new List<ProjectTabInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                ProjectTabInfo projectTab = new ProjectTabInfo();
                projectTab.ProjectTabId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabId"));
                projectTab.ProjectId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectId"));
                projectTab.Name = dataReader.GetSqlString(dataReader.GetOrdinal("Name")).ToString();
                projectTab.Label = dataReader.IsDBNull(dataReader.GetOrdinal("Label"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Label"));
                projectTab.TabOrder = dataReader.GetInt32(dataReader.GetOrdinal("TabOrder"));
                result.Add(projectTab);
            }
        }

        return result;
    }
    
    #endregion

}
}
