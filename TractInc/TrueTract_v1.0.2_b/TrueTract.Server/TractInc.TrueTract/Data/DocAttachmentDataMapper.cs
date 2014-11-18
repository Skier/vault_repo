using System;
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
             WHERE DocId = {0} ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [DocumentAttachment] (
                DocId,
                FileName,
                OriginalFileName
                ) 
                 VALUES ( {0}, '{1}', '{2}')

            select scope_identity();
        ";

        private const string SQL_DELETE_BY_DOC_ID = @"
            DELETE [DocumentAttachment] WHERE DocId = {0}
        ";

        #endregion

        #region Methods

        public List<DocAttachmentInfo> GetAttachmentsByDocId(SqlTransaction tran, int docId)
        {
            string sql = String.Format(SQL_SELECT_BY_DOCID, docId);

            List<DocAttachmentInfo> result = new List<DocAttachmentInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    DocAttachmentInfo attach = new DocAttachmentInfo();

                    attach.DocumentAttachmentId = dataReader.GetInt32(0);
                    attach.DocId = dataReader.GetInt32(1);
                    attach.FileName = dataReader.GetString(2);
                    attach.OriginalFileName = dataReader.GetString(3);

                    result.Add(attach);
                }
            }

            return result;
        }

        public void Create(SqlTransaction tran, DocAttachmentInfo attach)
        {
            string sql = String.Format(SQL_CREATE,
                attach.DocId, attach.FileName, attach.OriginalFileName);

            attach.DocumentAttachmentId = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());
        }

        public void DeleteByDocumentId(SqlTransaction tran, int docId)
        {
            string sql = String.Format(SQL_DELETE_BY_DOC_ID, docId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }

        #endregion

    }
}
