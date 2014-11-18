using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ProjectAttachmentDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_PROJECT = @"
            SELECT 
                ProjectAttachmentId,
                ProjectAttachmentTypeId,
                ProjectId,
                FileId
              FROM [ProjectAttachment]
             WHERE ProjectId = @ProjectId";

        private const string SQL_CREATE = @"
            INSERT INTO [ProjectAttachment] (
                ProjectAttachmentTypeId,
                ProjectId,
                FileId
                ) 
            VALUES ( 
                @ProjectAttachmentTypeId,
                @ProjectId,
                @FileId)

            SELECT scope_identity();
        ";

        private const string SQL_DELETE = @"
            DELETE [ProjectAttachment] WHERE ProjectAttachmentId = @ProjectAttachmentId
        ";

        #endregion

        #region Methods

        public List<ProjectAttachmentInfo> GetByProject(SqlTransaction tran, int projectId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectID", projectId));

            List<ProjectAttachmentInfo> result = new List<ProjectAttachmentInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(
                tran, CommandType.Text, SQL_SELECT_BY_PROJECT, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    ProjectAttachmentInfo item = new ProjectAttachmentInfo();

                    item.ProjectAttachmentId = dataReader.GetInt32(0);
                    item.ProjectAttachmentTypeId = dataReader.GetInt32(1);
                    item.ProjectId = dataReader.GetInt32(2);
                    item.FileId = dataReader.GetInt32(3);

                    result.Add(item);
                }
            }

            return result;
        }

        public ProjectAttachmentInfo Create(SqlTransaction tran, ProjectAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectID", attach.ProjectId));
            paramList.Add(new SqlParameter("@ProjectAttachmentTypeId", attach.ProjectAttachmentTypeId));
            paramList.Add(new SqlParameter("@FileId", attach.FileId));

            attach.ProjectAttachmentId = int.Parse(
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
            
            return attach;
        }

        public void Delete(SqlTransaction tran, ProjectAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectAttachmentId", attach.ProjectAttachmentId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        #endregion

    }
}
