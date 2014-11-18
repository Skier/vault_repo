using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
internal class ProjectTabDocumentDataMapper
{

    #region Constants

    private const string SQL_GET_BY_PROJECT_TAB_ID = @"
        SELECT * 
          FROM TT_ProjectTabDocument 
         WHERE ProjectTabId = @ProjectTabId
    ";

    private const string SQL_GET_BY_PROJECT_ID = @"
        SELECT ptd.* 
          FROM TT_ProjectTabDocument ptd
                INNER JOIN TT_ProjectTab pt ON pt.ProjectTabId = ptd.ProjectTabId
         WHERE pt.ProjectId = @ProjectId
    ";

    private const string SQL_CREATE = @"
        INSERT INTO [TT_ProjectTabDocument]
                   ([ProjectTabId], [DocumentId],[Description],[Remarks],[IsActive])
             VALUES (
                   @ProjectTabId,
                   @DocumentId,
                   @Description,
                   @Remarks,
                   @IsActive)

        SELECT scope_identity();
    ";

    private const string SQL_UPDATE = @"
        UPDATE [TT_ProjectTabDocument] set 
            ProjectTabId = @ProjectTabId,
            DocumentId = @DocumentId,
            Description = @Description,
            Remarks = @Remarks,
            IsActive = @IsActive
        WHERE ProjectTabDocumentId = @ProjectTabDocumentId";
    
    private const string SQL_DELETE = @"
        DELETE [TT_ProjectTabDocument] WHERE ProjectTabDocumentId = @ProjectTabDocumentId
    ";

    #endregion

    private ProjectTabDocumentTractDataMapper tabTractDM = new ProjectTabDocumentTractDataMapper();
    private TractDataMapper tractDM = new TractDataMapper();
    
    #region Methods

    public List<ProjectTabDocumentInfo> GetByProjectTab(SqlTransaction tran, int projectTabId)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabId", projectTabId));
        
        return Select(tran, SQL_GET_BY_PROJECT_TAB_ID, paramList);
    }

    public List<ProjectTabDocumentInfo> GetByProject(SqlTransaction tran, int projectId)
    {
        List<ProjectTabDocumentInfo> result;
        
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectId", projectId));

        result = Select(tran, SQL_GET_BY_PROJECT_ID, paramList);

        foreach (ProjectTabDocumentInfo tabDoc in result)
        {
            tabDoc.DocumentRef = (new DocDataMapper()).GetById(tran, tabDoc.DocumentId);
        }
        
        return result;
    }

    public ProjectTabDocumentInfo Create(SqlTransaction tran, ProjectTabDocumentInfo tabDocument)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabID", tabDocument.ProjectTabId));
        paramList.Add(new SqlParameter("@DocumentId", tabDocument.DocumentId));
        paramList.Add(new SqlParameter("@Description", tabDocument.Description));
        paramList.Add(new SqlParameter("@Remarks", tabDocument.Remarks));
        paramList.Add(new SqlParameter("@IsActive", tabDocument.IsActive));

        tabDocument.ProjectTabDocumentId = int.Parse(
            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
        
        if (tabDocument.Tracts != null)
        {
            tabTractDM.DeleteByProjectTabDocumentId(tran, tabDocument.ProjectTabDocumentId);
            
            foreach (ProjectTabDocumentTractInfo tabTract in tabDocument.Tracts)
            {
                tabTract.ProjectTabDocumentTractId = (tabTractDM.Create(tran, tabTract)).ProjectTabDocumentTractId;
                tabTract.TractRef = tractDM.GetByTractId(tran, tabTract.TractId, false);
            }
        }
        
        return tabDocument;
    }

    public ProjectTabDocumentInfo Update(SqlTransaction tran, ProjectTabDocumentInfo tabDocument)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentId", tabDocument.ProjectTabDocumentId));
        paramList.Add(new SqlParameter("@ProjectTabID", tabDocument.ProjectTabId));
        paramList.Add(new SqlParameter("@DocumentId", tabDocument.DocumentId));
        paramList.Add(new SqlParameter("@Description", tabDocument.Description));
        paramList.Add(new SqlParameter("@Remarks", tabDocument.Remarks));
        paramList.Add(new SqlParameter("@IsActive", tabDocument.IsActive));

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

        if (tabDocument.Tracts != null)
        {
            tabTractDM.DeleteByProjectTabDocumentId(tran, tabDocument.ProjectTabDocumentId);

            foreach (ProjectTabDocumentTractInfo tabTract in tabDocument.Tracts)
            {
                tabTract.ProjectTabDocumentTractId = (tabTractDM.Create(tran, tabTract)).ProjectTabDocumentTractId;
                tabTract.TractRef = tractDM.GetByTractId(tran, tabTract.TractId, false);
            }
        }

        return tabDocument;
    }
    
    public void Delete(SqlTransaction tran, ProjectTabDocumentInfo tabDocument)
    {
        List<SqlParameter> paramList = new List<SqlParameter>();

        paramList.Add(new SqlParameter("@ProjectTabDocumentId", tabDocument.ProjectTabDocumentId));

        tabTractDM.DeleteByProjectTabDocumentId(tran, tabDocument.ProjectTabDocumentId);

        SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
    }    

    private List<ProjectTabDocumentInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
    {
        List<ProjectTabDocumentInfo> result = new List<ProjectTabDocumentInfo>();

        using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
        {
            while (dataReader.Read())
            {
                ProjectTabDocumentInfo info = new ProjectTabDocumentInfo();
                info.ProjectTabId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabId"));
                info.DocumentId = dataReader.GetInt32(dataReader.GetOrdinal("DocumentId"));
                info.ProjectTabDocumentId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectTabDocumentId"));
                info.Description = dataReader.GetSqlString(dataReader.GetOrdinal("Description")).ToString();
                info.Remarks = dataReader.GetSqlString(dataReader.GetOrdinal("Remarks")).ToString();
                info.IsActive = dataReader.GetSqlBoolean(dataReader.GetOrdinal("IsActive")).IsTrue;

                result.Add(info);
            }
        }
        

        foreach (ProjectTabDocumentInfo tabDoc in result)
        {
            tabDoc.Tracts = tabTractDM.GetByProjectTabDocument(tran, tabDoc.ProjectTabDocumentId).ToArray();
        }


        return result;
    }

    #endregion
}
}
