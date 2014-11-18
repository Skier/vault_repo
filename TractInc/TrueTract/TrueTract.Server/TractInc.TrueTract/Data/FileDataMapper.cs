using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class FileDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_DOCUMENT = @"
            SELECT [TT_File].*, [User].Login as CreatedByName
              FROM [TT_File]
                JOIN [TT_DocumentAttachment] on [TT_DocumentAttachment].FileId = [TT_File].FileId
                JOIN [User] on [User].UserId = [TT_File].CreatedBy
             WHERE [DocumentId] = @DocumentId        
        ";

        private const string SQL_SELECT_BY_PROJECT = @"
            SELECT [TT_File].*, [User].Login as CreatedByName
              FROM [TT_File]
                JOIN [TT_ProjectAttachment] on [TT_ProjectAttachment].FileId = [TT_File].FileId
                JOIN [User] on [User].UserId = [TT_File].CreatedBy
             WHERE [ProjectId] = @ProjectId
        ";
        
        private const string SQL_SELECT_BY_ID = @"
            SELECT [TT_File].*, [User].Login as CreatedByName
              FROM [TT_File]
                JOIN [User] on [User].UserId = [TT_File].CreatedBy
             WHERE [TT_File].FileId = @FileId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_File] (FileName, FileUrl, FilePath, Description, Created, CreatedBy)
                 VALUES (@FileName, @FileUrl, @FilePath, @Description, @Created, @CreatedBy)

            SELECT scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [TT_File] SET 
                FileName = @FileName,
                FileUrl = @FileUrl,
                FilePath = @FilePath,
                Description = @Description,
                Created = @Created,
                CreatedBy = @CreatedBy
             WHERE FileId = @FileId
        ";

        private const string SQL_DELETE = @"
            DELETE [TT_File] WHERE FileId = @FileId
        ";

        #endregion

        #region Methods

        public FileInfo GetById(SqlTransaction tran, int fileId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@FileID", fileId));

            List<FileInfo> items = Select(tran, SQL_SELECT_BY_ID, paramList);
            if (items.Count > 0)
            {
                return items[0];
            } else 
                return null;
        }

        public List<FileInfo> GetByDocument(SqlTransaction tran, int documentId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", documentId));

            return Select(tran, SQL_SELECT_BY_DOCUMENT, paramList);
        }

        public List<FileInfo> GetByProject(SqlTransaction tran, int projectId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectID", projectId));

            return Select(tran, SQL_SELECT_BY_PROJECT, paramList);
        }
        
        private List<FileInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
        {
            List<FileInfo> result = new List<FileInfo>();
            
            using (SqlDataReader dataReader = (null != tran) ?
                SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()) :
                SQLHelper.ExecuteReader(CommandType.Text, sql, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    FileInfo file = new FileInfo();
                    file.FileId = dataReader.GetInt32(dataReader.GetOrdinal("FileID"));
                    file.FileName = dataReader.GetString(dataReader.GetOrdinal("FileName"));
                    file.FileUrl = dataReader.GetString(dataReader.GetOrdinal("FileUrl"));
                    file.FilePath = dataReader.GetString(dataReader.GetOrdinal("FilePath"));
                    file.Created = dataReader.GetDateTime(dataReader.GetOrdinal("Created"));
                    file.CreatedBy = dataReader.GetInt32(dataReader.GetOrdinal("CreatedBy"));
                    file.CreatedByName = dataReader.GetString(dataReader.GetOrdinal("CreatedByName"));
                    file.Description = dataReader.GetString(dataReader.GetOrdinal("Description"));
                    result.Add(file);
                }
            }
            
            return result;
        }

        public FileInfo Create(SqlTransaction tran, FileInfo file)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@FileName", file.FileName));
            paramList.Add(new SqlParameter("@FilePath", file.FilePath));
            paramList.Add(new SqlParameter("@FileUrl", file.FileUrl));
            paramList.Add(new SqlParameter("@Description", file.Description));
            paramList.Add(new SqlParameter("@Created", file.Created));
            paramList.Add(new SqlParameter("@CreatedBy", file.CreatedBy));

            file.FileId = int.Parse(
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
            
            return file;
        }

        public FileInfo Update(SqlTransaction tran, FileInfo file)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@FileID", file.FileId));
            paramList.Add(new SqlParameter("@FileName", file.FileName));
            paramList.Add(new SqlParameter("@FilePath", file.FilePath));
            paramList.Add(new SqlParameter("@FileUrl", file.FileUrl));
            paramList.Add(new SqlParameter("@Description", file.Description));
            paramList.Add(new SqlParameter("@Created", file.Created));
            paramList.Add(new SqlParameter("@CreatedBy", file.CreatedBy));

            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            return file;
        }

        public void Delete(SqlTransaction tran, FileInfo file)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@FileId", file.FileId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        #endregion

    }
}
