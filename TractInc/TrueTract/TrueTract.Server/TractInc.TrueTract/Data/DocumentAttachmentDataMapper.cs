using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocumentAttachmentDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_DOCUMENT = @"
            SELECT 
                DocumentAttachmentId,
                DocumentAttachmentTypeId,
                DocumentId,
                FileId
              FROM [TT_DocumentAttachment]
             WHERE DocumentId = @DocumentId";

        private const string SQL_SELECT_BY_ID = @"
            SELECT 
                DocumentAttachmentId,
                DocumentAttachmentTypeId,
                DocumentId,
                FileId
              FROM [TT_DocumentAttachment]
             WHERE DocumentAttachmentId = @DocumentAttachmentId";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_DocumentAttachment] (
                DocumentAttachmentTypeId,
                DocumentId,
                FileId
                ) 
            VALUES ( 
                @DocumentAttachmentTypeId,
                @DocumentId,
                @FileId)

            SELECT scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [TT_DocumentAttachment] set 
                DocumentAttachmentTypeId = @DocumentAttachmentTypeId, 
                DocumentId = @DocumentId,
                FileId = @FileId
            WHERE DocumentAttachmentId = @DocumentAttachmentId";
        
        private const string SQL_DELETE = @"
            DELETE [TT_DocumentAttachment] WHERE DocumentAttachmentId = @DocumentAttachmentId
        ";

        private const string SQL_DELETE_BY_DOC_ID = @"
            DELETE [TT_DocumentAttachment] WHERE DocumentId = @DocumentId
        ";

        #endregion

        #region Methods

        public List<DocumentAttachmentInfo> GetByDocument(SqlTransaction tran, int documentId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", documentId));

            List<DocumentAttachmentInfo> result = new List<DocumentAttachmentInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(
                tran, CommandType.Text, SQL_SELECT_BY_DOCUMENT, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    DocumentAttachmentInfo item = new DocumentAttachmentInfo();

                    item.DocumentAttachmentId = dataReader.GetInt32(0);
                    item.DocumentAttachmentTypeId = dataReader.GetInt32(1);
                    item.DocumentId = dataReader.GetInt32(2);
                    item.FileId = dataReader.GetInt32(3);

                    result.Add(item);
                }
            }

            return result;
        }

        public DocumentAttachmentInfo GetById(SqlTransaction tran, int documentAttachmentId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentAttachmentId", documentAttachmentId));

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(
                tran, CommandType.Text, SQL_SELECT_BY_ID, paramList.ToArray()))
            {
                if (dataReader.Read())
                {
                    DocumentAttachmentInfo item = new DocumentAttachmentInfo();

                    item.DocumentAttachmentId = dataReader.GetInt32(0);
                    item.DocumentAttachmentTypeId = dataReader.GetInt32(1);
                    item.DocumentId = dataReader.GetInt32(2);
                    item.FileId = dataReader.GetInt32(3);

                    return item;
                } else
                {
                    return null;
                }
            }
        }

        public void Update(SqlTransaction tran, DocumentAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", attach.DocumentId));
            paramList.Add(new SqlParameter("@DocumentAttachmentTypeId", attach.DocumentAttachmentTypeId));
            paramList.Add(new SqlParameter("@FileId", attach.FileId));
            paramList.Add(new SqlParameter("@DocumentAttachmentId", attach.DocumentAttachmentId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }
        
        public DocumentAttachmentInfo Create(SqlTransaction tran, DocumentAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", attach.DocumentId));
            paramList.Add(new SqlParameter("@DocumentAttachmentTypeId", attach.DocumentAttachmentTypeId));
            paramList.Add(new SqlParameter("@FileId", attach.FileId));

            attach.DocumentAttachmentId = int.Parse(
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
            
            return attach;
        }

        public void Delete(SqlTransaction tran, DocumentAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentAttachmentId", attach.DocumentAttachmentId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE, paramList.ToArray());
        }

        public void DeleteByDocId(SqlTransaction tran, int docId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentId", docId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE_BY_DOC_ID, paramList.ToArray());
        }

        #endregion

    }
}
