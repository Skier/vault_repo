using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class ProjectTabDocumentTractDataMapper
{

    #region Constants

    private const string SQL_GET_BY_PROJECT_TAB_DOCUMENT_ID = @"
        SELECT * 
          FROM TT_ProjectTabDocumentTract 
         WHERE ProjectTabDocumentId = @ProjectTabDocumentId
    ";

    private const string SQL_CREATE = @"
        INSERT INTO [TT_ProjectTabDocumentTract]
                   ([ProjectTabDocumentId], [TractId])
             VALUES (
                   @ProjectTabDocumentId,
                   @TractId)

        SELECT scope_identity();
    ";

    private const string SQL_UPDATE = @"
        UPDATE [TT_ProjectTabDocumentTract] set 
            ProjectTabDocumentId = @ProjectTabDocumentId,
            TractId = @TractId
        WHERE ProjectTabDocumentTractId = @ProjectTabDocumentTractId";
    
    private const string SQL_DELETE = @"
        DELETE [TT_ProjectTabDocumentTract] WHERE ProjectTabDocumentTractId = @ProjectTabDocumentTractId
    ";

    private const string SQL_DELETE_BY_TAB_DOCUMENT = @"
        DELETE [TT_ProjectTabDocumentTract] WHERE ProjectTabDocumentId = @ProjectTabDocumentId
    ";

    #endregion

    #region Methods

    public List<ProjectTabDocumentTractInfo> GetByProjectTabDocument(SqlTransaction tran, int projectTabDocumentId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentId", projectTabDocumentId));
        
        return Select(tran, SQL_GET_BY_PROJECT_TAB_DOCUMENT_ID, paramList);
    }

    public ProjectTabDocumentTractInfo Create(SqlTransaction tran, ProjectTabDocumentTractInfo tabDocumentTract)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentId", tabDocumentTract.ProjectTabDocumentId));
        paramList.Add(new SqlParameter("@TractId", tabDocumentTract.TractId));

        tabDocumentTract.ProjectTabDocumentTractId = int.Parse(
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        
        return tabDocumentTract;
    }

    public void Update(SqlTransaction tran, ProjectTabDocumentTractInfo tabDocumentTract)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentTractId", tabDocumentTract.ProjectTabDocumentTractId));
        paramList.Add(new SqlParameter("@ProjectTabDocumentId", tabDocumentTract.ProjectTabDocumentId));
        paramList.Add(new SqlParameter("@TractId", tabDocumentTract.TractId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
    }

    public void Delete(SqlTransaction tran, ProjectTabDocumentTractInfo tabDocumentTract)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentTractId", tabDocumentTract.ProjectTabDocumentTractId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
    }

    public void DeleteByProjectTabDocumentId(SqlTransaction tran, int tabDocumentId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentId", tabDocumentId));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE_BY_TAB_DOCUMENT, paramList.ToArray());
    }

    private List<ProjectTabDocumentTractInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<ProjectTabDocumentTractInfo> result = new List<ProjectTabDocumentTractInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                ProjectTabDocumentTractInfo info = new ProjectTabDocumentTractInfo();
                info.ProjectTabDocumentTractId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabDocumentTractId"));
                info.ProjectTabDocumentId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabDocumentId"));
                info.TractId = dataReader.GetInt32(dataReader.GetOrdinal("TractId"));

                result.Add(info);
            }
        }

        TractDataMapper tractDM = new TractDataMapper();
        
        foreach(ProjectTabDocumentTractInfo info in result)
        {
            info.TractRef = tractDM.GetByTractId(tran, info.TractId, false);
        }
        
        return result;
    }

    #endregion
}
}
