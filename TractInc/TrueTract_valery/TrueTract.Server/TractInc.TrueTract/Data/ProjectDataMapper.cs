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

        private const string SQL_GET_BY_CLIENT_AND_USERID = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
               and exists (select 1 
                             from [AssetAssignment] 
                                join [User] on [User].AssetId = [AssetAssignment].AssetId
                                 and [AssetAssignment].ProjectId = [Project].ProjectId
                                 and [User].UserId = @UserId
							where [AssetAssignment].StartDate < getDate()
							  and ([AssetAssignment].EndDate is null or [AssetAssignment].EndDate > getDate())
                           )
        ";

        private const string SQL_GET_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
        ";

        private const string SQL_GET_ALL = @"
            SELECT [Project].*
              FROM [Project]
        ";

        private const string SQL_GET_OPEN_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
               AND [Project].Status = 'ACTIVE'
        ";

        private const string SQL_GET_CLOSED_LASTWEEK_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
               AND [Project].Status = 'COMPLETE'
               AND [Project].Changed > (GETDATE() - 7)
        ";

        private const string SQL_GET_ALL_CLOSED_BY_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
               AND [Project].Status = 'COMPLETE'
        ";

        private const string SQL_GET_BY_DOCUMENT_AND_CLIENT = @"
            SELECT [Project].*
              FROM [Project]
             WHERE [Project].ClientId = @ClientId
               AND [Project].ProjectId in 
						(
						SELECT Distinct ProjectId from ProjectTab pt
							inner join ProjectTabDocument ptd on ptd.ProjectTabId = pt.ProjectTabId
						 where ptd.DocumentId = @DocumentId
						)
        ";

        private const string SQL_GET_BY_ID = @"
            SELECT * FROM [Project]
             WHERE ProjectId = @ProjectId
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Project] SET 
                ClientId = @ClientId,
                [Name] = @Name,
                ShortName = @ShortName,
                ClientAccountId = @ClientAccountId,
                Description = @Description,
                Status = @Status,
                Changed = @Changed,
                ChangedBy = @ChangedBy
             WHERE ProjectId = @ProjectId
        ";

        
        #endregion

        #region Methods

        public List<ProjectInfo> GetAll(SqlTransaction tran)
        {
            return Select(tran, SQL_GET_ALL, new List<SqlParameter>());
        }

        public List<ProjectInfo> GetByClientAndUser(SqlTransaction tran, int clientId, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            paramList.Add(new SqlParameter("@ClientId", clientId));
            
            return Select(tran, SQL_GET_BY_CLIENT_AND_USERID, paramList);
        }

        public List<ProjectInfo> GetAllByClient(SqlTransaction tran, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ClientId", clientId));

            return Select(tran, SQL_GET_BY_CLIENT, paramList);
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

        public List<ProjectInfo> GetByDocumentAndClient(SqlTransaction tran, int documentId, int clientId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentId", documentId));
            paramList.Add(new SqlParameter("@ClientId", clientId));

            return Select(tran, SQL_GET_BY_DOCUMENT_AND_CLIENT, paramList);
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
            paramList.Add(new SqlParameter("@ClientId", project.ClientId));
            paramList.Add(new SqlParameter("@Name", project.Name));
            paramList.Add(new SqlParameter("@ShortName", project.ShortName));
            paramList.Add(new SqlParameter("@ClientAccountId", project.ClientAccountId));
            paramList.Add(new SqlParameter("@Description", project.Description));
            paramList.Add(new SqlParameter("@Status", project.Status));
            paramList.Add(new SqlParameter("@Changed", (project.Changed == DateTime.MinValue) ? SqlDateTime.Null : project.Changed));
            paramList.Add(new SqlParameter("@ChangedBy", (project.ChangedBy == 0) ? SqlInt32.Null : project.ChangedBy));

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
                    info.ClientId = dataReader.GetInt32(dataReader.GetOrdinal("ClientId"));
                    info.Name = dataReader.GetSqlString(dataReader.GetOrdinal("Name")).ToString();
                    info.ShortName = dataReader.GetSqlString(dataReader.GetOrdinal("ShortName")).ToString();
                    info.ClientAccountId = dataReader.GetInt32(dataReader.GetOrdinal("ClientAccountId"));
                    info.Description = dataReader.GetSqlString(dataReader.GetOrdinal("Description")).ToString();
                    info.Status = dataReader.GetSqlString(dataReader.GetOrdinal("Status")).ToString();
                    info.Changed = dataReader.IsDBNull(dataReader.GetOrdinal("Changed")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("Changed"));
                    info.ChangedBy = dataReader.IsDBNull(dataReader.GetOrdinal("ChangedBy"))
                                         ? 0
                                         : dataReader.GetInt32(dataReader.GetOrdinal("ChangedBy"));
                    result.Add(info);
                }
            }

            return result;
        }
        
        #endregion

    }
}
