using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocAttachmentDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_DOCID =
            @"
            SELECT 
                DocumentAttachmentId,
                DocId,
                FileName,
                OriginalFileName
              FROM [DocumentAttachment]
             WHERE DocId = @DocId ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [DocumentAttachment] (
                DocId,
                FileName,
                OriginalFileName
                ) 
                 VALUES ( @DocId, @FileName, @OriginalFileName)

            select scope_identity();
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [DocumentAttachment] SET 
                DocId = @DocId,
                FileName = @FileName,
                OriginalFileName = @OriginalFileName
             WHERE DocumentAttachmentId = @DocumentAttachmentId
        ";

        private const string SQL_DELETE_BY_DOC_ID = @"
            DELETE [DocumentAttachment] WHERE DocId = @DocId
        ";

        #endregion

        #region Methods

        public DocAttachmentInfo GetAttachmentByDocId(SqlTransaction tran, int docId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", docId));

            DocAttachmentInfo result = null;

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_DOCID, paramList.ToArray()))
            {
                if (dataReader.Read())
                {
                    result = new DocAttachmentInfo();

                    result.DocumentAttachmentId = dataReader.GetInt32(0);
                    result.DocId = dataReader.GetInt32(1);
                    result.FileName = dataReader.GetString(2);
                    result.OriginalFileName = dataReader.GetString(3);
                }
            }

            return result;
        }

        public DocAttachmentInfo Create(SqlTransaction tran, DocAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", attach.DocId));
            paramList.Add(new SqlParameter("@FileName", attach.FileName));
            paramList.Add(new SqlParameter("@OriginalFileName", attach.OriginalFileName));

            attach.DocumentAttachmentId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());
            
            return attach;
        }

        public DocAttachmentInfo Update(SqlTransaction tran, DocAttachmentInfo attach)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", attach.DocId));
            paramList.Add(new SqlParameter("@FileName", attach.FileName));
            paramList.Add(new SqlParameter("@OriginalFileName", attach.OriginalFileName));
            paramList.Add(new SqlParameter("@DocumentAttachmentId", attach.DocumentAttachmentId));

            SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            return attach;
        }

        public void DeleteByDocumentId(SqlTransaction tran, int docId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", docId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_DELETE_BY_DOC_ID, paramList.ToArray());
        }

        #endregion

    }
}
