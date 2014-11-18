using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ProjectDataMapper
    {

        #region Constants
        private const string SQL_GET_ALL = @"
            SELECT [Project].*
              FROM [Project]
        ";

        private const string SQL_GET_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
             WHERE [Contract].ClientId = @ClientId
        ";

        private const string SQL_GET_BY_DOCUMENT_AND_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
             WHERE [Contract].ClientId = @ClientId
               AND [Project].ProjectId in 
                        (
                        SELECT Distinct ProjectId from [TT_ProjectTab] pt
                            inner join [TT_ProjectTabDocument] ptd on ptd.ProjectTabId = pt.ProjectTabId
                         where ptd.DocumentId = @DocumentId
                        )
        ";

        private const string SQL_GET_BY_CLIENT_AND_USERID = @"
            SELECT [Project].*
              FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
             WHERE [Contract].ClientId = @ClientId
               and exists (select 1 
                             from TeamMember tm 
                                inner join TeamAssignment ta on tm.TeamId = ta.TeamId
                                inner join Asset on Asset.AssetId = tm.AssetId
                                inner join [User] on [User].PersonId = [Asset].PersonId
                                 and ta.ProjectId = [Project].ProjectId
                                 and [User].UserId = @UserId
                            where tm.StartDate < getDate()
                              and (tm.EndDate is null or tm.EndDate > getDate())
                           )
        ";

        private const string SQL_GET_OPEN_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
                inner join ProjectStatus on Project.ProjectStatusId = ProjectStatus.ProjectStatusId
             WHERE [Contract].ClientId = @ClientId
               AND [ProjectStatus].StatusName = 'ACTIVE'
        ";

        private const string SQL_GET_CLOSED_LASTWEEK_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
                inner join ProjectStatus on Project.ProjectStatusId = ProjectStatus.ProjectStatusId
             WHERE [Contract].ClientId = @ClientId
               AND [ProjectStatus].StatusName = 'COMPLETE'
        ";
/*
               AND [TT_Project].Changed > (GETDATE() - 7)
*/

        private const string SQL_GET_ALL_CLOSED_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
                inner join ProjectStatus on Project.ProjectStatusId = ProjectStatus.ProjectStatusId
             WHERE [Contract].ClientId = @ClientId
               AND [ProjectStatus].StatusName = 'COMPLETE'
        ";

        private const string SQL_GET_BY_ID = @"
            SELECT [Project].*
            FROM [Project]
                inner join Contract on Project.ContractId = Contract.ContractId
             WHERE ProjectId = @ProjectId
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Project] SET 
                ProjectStatusId = @ProjectStatusId,
                CreatedBy = @CreatedBy
             WHERE ProjectId = @ProjectId
        ";
        
        #endregion

        #region Methods

        public List<ProjectInfo> GetAll(SqlTransaction tran)
        {
            return Select(tran, SQL_GET_ALL, new List<SqlParameter>());
        }

        public List<ProjectInfo> GetAllByClient(SqlTransaction tran, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@ClientId", clientId));
            return Select(tran, SQL_GET_BY_CLIENT, paramList);
        }

        public List<ProjectInfo> GetByDocumentAndClient(SqlTransaction tran, int documentId, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentId", documentId));
            paramList.Add(new SqlParameter("@ClientId", clientId));

            return Select(tran, SQL_GET_BY_DOCUMENT_AND_CLIENT, paramList);
        }

        public List<ProjectInfo> GetByClientAndUser(SqlTransaction tran, int clientId, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@ClientId", clientId));
            
            return Select(tran, SQL_GET_BY_CLIENT_AND_USERID, paramList);
        }

        public List<ProjectInfo> GetOpenByClient(SqlTransaction tran, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ClientId", clientId));

            return Select(tran, SQL_GET_OPEN_BY_CLIENT, paramList);
        }

        public List<ProjectInfo> GetClosedLastWeekByClient(SqlTransaction tran, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ClientId", clientId));

            return Select(tran, SQL_GET_CLOSED_LASTWEEK_BY_CLIENT, paramList);
        }

        public List<ProjectInfo> GetAllClosedByClient(SqlTransaction tran, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ClientId", clientId));

            return Select(tran, SQL_GET_ALL_CLOSED_BY_CLIENT, paramList);
        }

        public ProjectInfo GetById(SqlTransaction tran, int projectId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectId", projectId));
            
            List<ProjectInfo> projectList = Select(tran, SQL_GET_BY_ID, paramList);
            
            if (projectList.Count > 0)
            {
                return projectList[0];
            } else
            {
                return null;
            }
        }

        public void Update(SqlTransaction tran, ProjectInfo project)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectId", project.ProjectId));
            paramList.Add(new SqlParameter("@ProjectStatusId", project.ProjectStatusId));
            paramList.Add(new SqlParameter("@CreatedBy", project.CreatedBy));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
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
                    info.ContractId = dataReader.GetInt32(dataReader.GetOrdinal("ContractId"));
                    info.AccountId = dataReader.IsDBNull(dataReader.GetOrdinal("AccountId"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("AccountId"));
                    info.ProjectStatusId = dataReader.GetInt32(dataReader.GetOrdinal("ProjectStatusId"));
                    info.Name = dataReader.GetSqlString(dataReader.GetOrdinal("ProjectName")).ToString();
                    info.ProjectName = dataReader.GetSqlString(dataReader.GetOrdinal("ProjectName")).ToString();
                    info.ShortName = dataReader.GetSqlString(dataReader.GetOrdinal("ShortName")).ToString();
                    info.Description = dataReader.GetSqlString(dataReader.GetOrdinal("Description")).ToString();
                    info.CreatedBy = dataReader.GetSqlString(dataReader.GetOrdinal("CreatedBy")).ToString();
                    result.Add(info);
                }
            }

            return result;
        }
        
        #endregion

    }
}
