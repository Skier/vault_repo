using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocAttachmentDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_DOCID = @"
            SELECT 
                DocumentAttachmentId,
                DocumentAttachmentTypeId,
                DocumentId,
                FileName,
                FileUrl,
                Description
              FROM [DocumentAttachment]
             WHERE DocumentId = @DocumentId";

        private const string SQL_CREATE = @"
            INSERT INTO [DocumentAttachment] (
                DocumentAttachmentTypeId,
                DocumentId,
                FileName,
                FileUrl,
                Description
                ) 
            VALUES ( 
                @DocumentAttachmentTypeId,
                @DocumentId,
                @FileName,
                @FileUrl,
                @Description)

            SELECT scope_identity();
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [DocumentAttachment] SET 
                DocumentAttachmentTypeId = @DocumentAttachmentTypeId,
                DocumentId = @DocumentId,
                FileName = @FileName,
                FileUrl = @FileUrl,
                Description = @Description
             WHERE DocumentAttachmentId = @DocumentAttachmentId
        ";

        private const string SQL_DELETE = @"
            DELETE [DocumentAttachment] WHERE DocumentAttachmentId = @DocumentAttachmentId
        ";

        #endregion

        #region Methods

        public List<DocumentAttachmentInfo> GetAttachmentsByDocId(SqlTransaction tran, int documentId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", documentId));

            List<DocumentAttachmentInfo> result = new List<DocumentAttachmentInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(
                tran, CommandType.Text, SQL_SELECT_BY_DOCID, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    DocumentAttachmentInfo item = new DocumentAttachmentInfo();

                    item.DocumentAttachmentId = dataReader.GetInt32(0);
                    item.DocumentAttachmentTypeId = dataReader.GetInt32(1);
                    item.DocumentId = dataReader.GetInt32(2);
                    item.FileName = dataReader.GetString(3);
                    item.FileUrl = dataReader.GetString(4);
                    item.Description = dataReader.GetString(5);
                    
                    result.Add(item);
                }
            }

            return result;
        }

        public DocumentAttachmentInfo Create(SqlTransaction tran, DocumentAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", attach.DocumentId));
            paramList.Add(new SqlParameter("@DocumentAttachmentTypeId", attach.DocumentAttachmentTypeId));
            paramList.Add(new SqlParameter("@FileName", attach.FileName));
            paramList.Add(new SqlParameter("@FileUrl", attach.FileUrl));
            paramList.Add(new SqlParameter("@Description", attach.Description));

            attach.DocumentAttachmentId = int.Parse(
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
            
            return attach;
        }

        public DocumentAttachmentInfo Update(SqlTransaction tran, DocumentAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", attach.DocumentId));
            paramList.Add(new SqlParameter("@FileName", attach.FileName));
            paramList.Add(new SqlParameter("@FileUrl", attach.FileUrl));
            paramList.Add(new SqlParameter("@Description", attach.Description));
            paramList.Add(new SqlParameter("@DocumentAttachmentTypeId", attach.DocumentAttachmentTypeId));
            paramList.Add(new SqlParameter("@DocumentAttachmentId", attach.DocumentAttachmentId));

            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            return attach;
        }

        public void Delete(SqlTransaction tran, DocumentAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentAttachmentId", attach.DocumentAttachmentId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        #endregion

    }
}
