using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocumentReferenceDataMapper
    {
        #region Constants

        private const string SQL_SELECT_BY_ID = @"
            SELECT 
                DocumentReferenceId,
                DocumentId,
                ReferenceId,
                Description,
                State, County, DocTypeId, DocumentNo, Volume, Page
              FROM [TT_DocumentReference]
             WHERE DocumentReferenceId = @DocumentReferenceId";

        private const string SQL_SELECT_BY_DOCUMENT_ID = @"
            SELECT 
                DocumentReferenceId,
                DocumentId,
                ReferenceId,
                Description,
                State, County, DocTypeId, DocumentNo, Volume, Page
              FROM [TT_DocumentReference]
             WHERE DocumentId = @DocumentId";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_DocumentReference] (
                DocumentId,
                ReferenceId,
                Description,
                State, County, DocTypeId, DocumentNo, Volume, Page
                ) 
            VALUES ( 
                @DocumentId,
                @ReferenceId,
                @Description,
                @State, @County, @DocTypeId, @DocumentNo, @Volume, @Page
                )

            SELECT scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [TT_DocumentReference] set 
                DocumentId = @DocumentId, 
                ReferenceId = @ReferenceId,
                Description = @Description,
                State = @State,
                County = @County,
                DocTypeId = @DocTypeId,
                DocumentNo = @DocumentNo,
                Volume = @Volume,
                Page = @Page
            WHERE DocumentReferenceId = @DocumentReferenceId";
        
        private const string SQL_DELETE = @"
            DELETE [TT_DocumentReference] WHERE DocumentReferenceId = @DocumentReferenceId
        ";

        private const string SQL_DELETE_BY_DOC_ID = @"
            DELETE [TT_DocumentReference] WHERE DocumentId = @DocumentId
        ";

        #endregion

        #region Methods

        public DocumentReferenceInfo GetById(SqlTransaction tran, int docRefId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentReferenceId", docRefId));

            DocumentReferenceInfo result = null;

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(
                tran, CommandType.Text, SQL_SELECT_BY_ID, paramList.ToArray()))
            {
                if (dataReader.Read())
                {
                    result = new DocumentReferenceInfo();

                    result.DocumentReferenceId = dataReader.GetInt32(dataReader.GetOrdinal("DocumentReferenceId"));
                    result.DocumentId = dataReader.GetInt32(dataReader.GetOrdinal("DocumentId"));
                    result.ReferenceId = dataReader.IsDBNull(dataReader.GetOrdinal("ReferenceId"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ReferenceId"));
                    result.Description = dataReader.IsDBNull(dataReader.GetOrdinal("Description"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Description"));
                    result.State = dataReader.IsDBNull(dataReader.GetOrdinal("State"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("State"));
                    result.County = dataReader.IsDBNull(dataReader.GetOrdinal("County"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("County"));
                    result.DocTypeId = dataReader.IsDBNull(dataReader.GetOrdinal("DocTypeId"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DocTypeId"));
                    result.DocumentNo = dataReader.IsDBNull(dataReader.GetOrdinal("DocumentNo"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("DocumentNo"));
                    result.Volume = dataReader.IsDBNull(dataReader.GetOrdinal("Volume"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Volume"));
                    result.Page = dataReader.IsDBNull(dataReader.GetOrdinal("Page"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Page"));
                }
            }

            return result;
        }

        public List<DocumentReferenceInfo> GetByDocumentId(SqlTransaction tran, int documentId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentID", documentId));

            List<DocumentReferenceInfo> result = new List<DocumentReferenceInfo>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(
                tran, CommandType.Text, SQL_SELECT_BY_DOCUMENT_ID, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    DocumentReferenceInfo item = new DocumentReferenceInfo();

                    item.DocumentReferenceId = dataReader.GetInt32(dataReader.GetOrdinal("DocumentReferenceId"));
                    item.DocumentId = dataReader.GetInt32(dataReader.GetOrdinal("DocumentId"));
                    item.ReferenceId = dataReader.IsDBNull(dataReader.GetOrdinal("ReferenceId"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ReferenceId"));
                    item.Description = dataReader.IsDBNull(dataReader.GetOrdinal("Description"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Description"));
                    item.State = dataReader.IsDBNull(dataReader.GetOrdinal("State"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("State"));
                    item.County = dataReader.IsDBNull(dataReader.GetOrdinal("County"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("County"));
                    item.DocTypeId = dataReader.IsDBNull(dataReader.GetOrdinal("DocTypeId"))
                                           ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DocTypeId"));
                    item.DocumentNo = dataReader.IsDBNull(dataReader.GetOrdinal("DocumentNo"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("DocumentNo"));
                    item.Volume = dataReader.IsDBNull(dataReader.GetOrdinal("Volume"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Volume"));
                    item.Page = dataReader.IsDBNull(dataReader.GetOrdinal("Page"))
                                           ? null : dataReader.GetString(dataReader.GetOrdinal("Page"));

                    result.Add(item);
                }
            }

            return result;
        }
        
        public void Update(SqlTransaction tran, DocumentReferenceInfo reference)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentId", reference.DocumentId));
            paramList.Add(new SqlParameter("@ReferenceId", (0 != reference.ReferenceId) ? reference.ReferenceId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@Description", (null != reference.Description) ? reference.Description : SqlString.Null));
            paramList.Add(new SqlParameter("@State", (0 != reference.State) ? reference.State : SqlInt32.Null));
            paramList.Add(new SqlParameter("@County", (0 != reference.County) ? reference.County : SqlInt32.Null));
            paramList.Add(new SqlParameter("@DocTypeId", (0 != reference.DocTypeId) ? reference.DocTypeId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != reference.DocumentNo) ? reference.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@Volume", (null != reference.Volume) ? reference.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != reference.Page) ? reference.Page : SqlString.Null));
            paramList.Add(new SqlParameter("@DocumentReferenceId", reference.DocumentReferenceId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }

        public DocumentReferenceInfo Create(SqlTransaction tran, DocumentReferenceInfo reference)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentId", reference.DocumentId));
            paramList.Add(new SqlParameter("@ReferenceId", (0 != reference.ReferenceId) ? reference.ReferenceId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@Description", (null != reference.Description) ? reference.Description : SqlString.Null));
            paramList.Add(new SqlParameter("@State", (0 != reference.State) ? reference.State : SqlInt32.Null));
            paramList.Add(new SqlParameter("@County", (0 != reference.County) ? reference.County : SqlInt32.Null));
            paramList.Add(new SqlParameter("@DocTypeId", (0 != reference.DocTypeId) ? reference.DocTypeId : SqlInt32.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != reference.DocumentNo) ? reference.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@Volume", (null != reference.Volume) ? reference.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != reference.Page) ? reference.Page : SqlString.Null));

            reference.DocumentReferenceId = int.Parse(
                SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

            return reference;
        }

        public void Delete(SqlTransaction tran, DocumentReferenceInfo reference)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocumentReferenceId", reference.DocumentReferenceId));

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
