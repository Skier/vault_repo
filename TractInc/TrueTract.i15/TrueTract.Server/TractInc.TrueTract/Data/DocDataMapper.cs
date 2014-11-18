using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocDataMapper
    {
        #region Constants

        private const string DOCUMENT_IS_NOT_UNIQUE = "Document with such unique fields (State, County, DocType, DocNu, Volume, Page) is already exists";

        private const string SQL_SELECT = @"
            SELECT 
                DocID,
                IsPublic,
                DocTypeId,
                Volume,
                Page,
                DocumentNo,
                County,
                State,
                DateFiledYear,
                DateFiledMonth,
                DateFiledDay,
                DateSignedYear,
                DateSignedMonth,
                DateSignedDay,
                ResearchNote,
                ImageLink,
                CreatedBy,
                DateModified,
                DocBranchUid,
                IsActive
              FROM [Document]
        ";

        private const string SQL_SELECT_BY_DOC_ID = SQL_SELECT + " WHERE DocID = @DocId ";

        private const string SQL_SELECT_BY_DOC_BRANCH_UID = SQL_SELECT + " WHERE DocBranchUid = @DocBranchUid ";
        
        private const string SQL_SELECT_BY_CREATED_BY = SQL_SELECT + " WHERE CreatedBy = @CreatedBy ";

        private const string SQL_AND_ACTIVE_ONLY = " AND [Document].IsActive = 1 ";

        private const string SQL_SELECT_BY_GROUP_AND_USER = SQL_SELECT + @"
            INNER JOIN [GroupItems] on [GroupItems].DocumentId = [Document].DocId
            INNER JOIN [GroupUsers] on [GroupUsers].GroupId = [GroupItems].GroupId
         where [GroupUsers].GroupId = @GroupId
           and [GroupUsers].UserId = @UserId
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [Document] (
                IsPublic,
                DocTypeId,
                Volume,
                Page,
                DocumentNo,
                County,
                State,
                DateFiledYear,
                DateFiledMonth,
                DateFiledDay,
                DateSignedYear,
                DateSignedMonth,
                DateSignedDay,
                ResearchNote,
                ImageLink,
                CreatedBy,
                DateModified,
                IsActive,
                DocBranchUid
                ) 
                 VALUES ( @IsPublic, @DocTypeId, @Volume, @Page, @DocumentNo, @County, @State, 
                    @DateFiledYear, @DateFiledMonth, @DateFiledDay, 
                    @DateSignedYear, @DateSignedMonth, @DateSignedDay, @ResearchNote, @ImageLink, @CreatedBy, GetDate(), 1, @DocBranchUid)

            select scope_identity();
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [Document] set 
                IsPublic = @IsPublic,
                DocTypeId = @DocTypeId,
                Volume = @Volume,
                Page = @Page,
                DocumentNo = @DocumentNo,
                County = @County,
                State = @State,
                DateFiledYear = @DateFiledYear,
                DateFiledMonth = @DateFiledMonth,
                DateFiledDay = @DateFiledDay,
                DateSignedYear = @DateSignedYear,
                DateSignedMonth = @DateSignedMonth,
                DateSignedDay = @DateSignedDay,
                ResearchNote = @ResearchNote,
                ImageLink = @ImageLink,
                CreatedBy = @CreatedBy,
                DateModified = GetDate()
             WHERE DocID = @DocID
        ";

        #endregion

        #region Fields

        private TractDataMapper m_tractDM;
        private ParticipantDataMapper m_participantDM;

        #endregion

        #region Methods

        public List<DocumentInfo> GetByTemplate(SqlTransaction tran, DocumentInfo template) {

            string sql = SQL_SELECT + " WHERE 1 = 1 " + SQL_AND_ACTIVE_ONLY;

            if (template.DocTypeId != 0) {
                sql += " and DocTypeId = ";
                sql += template.DocTypeId.ToString();
            }
            if (template.State != 0) {
                sql += " and State = ";
                sql += template.State.ToString();
            }
            if (template.County != 0) {
                sql += " and County = ";
                sql += template.County.ToString();
            }
            if (template.DocumentNo != null && template.DocumentNo.Trim() != String.Empty) {
                sql += " and ( DocumentNo = '";
                sql += template.DocumentNo;
                sql += "' or ( ";

                if (template.Volume != null && template.Volume.Trim() != String.Empty) {
                    sql += " Volume = '";
                    sql += template.Volume;
                    sql += "' and ";
                } else {
                    sql += " 1!=1 and ";
                }

                if (template.Page != null && template.Page.Trim() != String.Empty) {
                    sql += " Page = '";
                    sql += template.Page;
                    sql += "' and ";
                } else {
                    sql += " 1!=1 and ";
                }

                sql += " 1=1 ";

                sql += " ) ) ";
            } else {
                if (template.Volume != null && template.Volume.Trim() != String.Empty) {
                    sql += " and Volume = '";
                    sql += template.Volume;
                    sql += "' ";
                }
                if (template.Page != null && template.Page.Trim() != String.Empty) {
                    sql += " and Page = '";
                    sql += template.Page;
                    sql += "' ";
                }
            }

            return Select(tran, sql, new List<SqlParameter>());
        }

        public List<DocumentInfo> GetByGroupAndUser(SqlTransaction tran, int groupId, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@UserId", userId));

            return Select(tran, SQL_SELECT_BY_GROUP_AND_USER + SQL_AND_ACTIVE_ONLY, paramList);
        }

        public DocumentInfo GetById(SqlTransaction tran, int docId) {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocId", docId));
            
            List<DocumentInfo> list = Select(tran, SQL_SELECT_BY_DOC_ID + SQL_AND_ACTIVE_ONLY, paramList);
            
            if (list.Count == 0)
            {
                return null;
            } else
            {
                return list[0];
            }
        }

        public List<DocumentInfo> GetByCreatedBy(SqlTransaction tran, int userId) {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@CreatedBy", userId));
            
            return Select(tran, SQL_SELECT_BY_CREATED_BY + SQL_AND_ACTIVE_ONLY, paramList);
        }

        public List<DocumentInfo> GetDocumentRevisioins(SqlTransaction tran, string docBranchId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocBranchUid", docBranchId));
            
            return Select(tran, SQL_SELECT_BY_DOC_BRANCH_UID, paramList);
        }

        private List<DocumentInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList) {
            
            List<DocumentInfo> result = new List<DocumentInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray())) {
                
                while (dataReader.Read()) {
                    DocumentInfo documentInfo = new DocumentInfo();

                    documentInfo.DocID = dataReader.GetInt32(0);
                    documentInfo.IsPublic = dataReader.GetSqlBoolean(1).IsTrue;
                    documentInfo.DocTypeId = dataReader.GetInt32(2);
                    documentInfo.Volume = dataReader.IsDBNull(3) ? null : dataReader.GetString(3);
                    documentInfo.Page = dataReader.IsDBNull(4) ? null : dataReader.GetString(4);
                    documentInfo.DocumentNo = dataReader.IsDBNull(5) ? null : dataReader.GetString(5);
                    documentInfo.County = dataReader.GetInt32(6);
                    documentInfo.State = dataReader.GetInt32(7);
                    documentInfo.DateFiledYear = dataReader.GetInt32(8);
                    documentInfo.DateFiledMonth = dataReader.GetInt32(9);
                    documentInfo.DateFiledDay = dataReader.GetInt32(10);
                    documentInfo.DateSignedYear = dataReader.GetInt32(11);
                    documentInfo.DateSignedMonth = dataReader.GetInt32(12);
                    documentInfo.DateSignedDay = dataReader.GetInt32(13);
                    documentInfo.ResearchNote = dataReader.IsDBNull(14) ? null : dataReader.GetString(14);
                    documentInfo.ImageLink = dataReader.IsDBNull(15) ? null : dataReader.GetString(15);
                    documentInfo.CreatedBy = dataReader.GetInt32(16);
                    documentInfo.DateModified = dataReader.GetDateTime(17);
                    documentInfo.IsActive = dataReader.GetSqlBoolean(19).IsTrue;
                    documentInfo.DocBranchUid = dataReader.GetSqlGuid(18).ToString();

                    List<ParticipantInfo> participants = ParticipantDM.GetParticipantsByDocId(tran, documentInfo.DocID);

                    foreach (ParticipantInfo participant in participants) {
                        if (participant.IsSeler) {
                            documentInfo.Seller = participant;
                        } else {
                            documentInfo.Buyer = participant;
                        }
                    }
                    
                    result.Add(documentInfo);
                }
            }

            return result;
        }
        
        public DocumentInfo Create(SqlTransaction tran, DocumentInfo documentInfo, int userId) {
            List<SqlParameter> paramList = new List<SqlParameter>();

            List<DocumentInfo> similarDocuments = GetByTemplate(tran, documentInfo);

            if (similarDocuments.Count > 0)
            {
                throw new Exception(DOCUMENT_IS_NOT_UNIQUE);
            }

            documentInfo.DocBranchUid = Guid.NewGuid().ToString();

            paramList.Add(new SqlParameter("@IsPublic", documentInfo.IsPublic));
            paramList.Add(new SqlParameter("@DocTypeId", documentInfo.DocTypeId));
            paramList.Add(new SqlParameter("@County", documentInfo.County));
            paramList.Add(new SqlParameter("@State", documentInfo.State));
            paramList.Add(new SqlParameter("@DateFiledYear", documentInfo.DateFiledYear));
            paramList.Add(new SqlParameter("@DateFiledMonth", documentInfo.DateFiledMonth));
            paramList.Add(new SqlParameter("@DateFiledDay", documentInfo.DateFiledDay));
            paramList.Add(new SqlParameter("@DateSignedYear", documentInfo.DateSignedYear));
            paramList.Add(new SqlParameter("@DateSignedMonth", documentInfo.DateSignedMonth));
            paramList.Add(new SqlParameter("@DateSignedDay", documentInfo.DateSignedDay));
            paramList.Add(new SqlParameter("@Volume", (null != documentInfo.Volume) ? documentInfo.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != documentInfo.Page) ? documentInfo.Page : SqlString.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != documentInfo.DocumentNo) ? documentInfo.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@ResearchNote", (null != documentInfo.ResearchNote) ? documentInfo.ResearchNote : SqlString.Null));
            paramList.Add(new SqlParameter("@ImageLink", (null != documentInfo.ImageLink) ? documentInfo.ImageLink : SqlString.Null));
            paramList.Add(new SqlParameter("@CreatedBy", userId));
            paramList.Add(new SqlParameter("@DocBranchUid", documentInfo.DocBranchUid));

            object id = SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray());

            documentInfo.DocID = int.Parse(id.ToString());

            if (null != documentInfo.TractList) {
                foreach (TractInfo tract in documentInfo.TractList) {
                    tract.DocId = documentInfo.DocID;
                    TractDM.Create(tran, tract, userId);
                }
            }

            if (null != documentInfo.Buyer) {
                documentInfo.Buyer.DocID = documentInfo.DocID;
                ParticipantDM.Create(tran, documentInfo.Buyer);
            }

            if (null != documentInfo.Seller) {
                documentInfo.Seller.DocID = documentInfo.DocID;
                ParticipantDM.Create(tran, documentInfo.Seller);
            }

            return documentInfo;
        }

        public DocumentInfo Update(SqlTransaction tran, DocumentInfo documentInfo, int userId) {

//            List<DocumentInfo> similarDocuments = GetByTemplate(tran, documentInfo);
//
//            if (similarDocuments.Count > 0 && similarDocuments[0].DocID != documentInfo.DocID)
//            {
//                throw new Exception(DOCUMENT_IS_NOT_UNIQUE);
//            }

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@docID", documentInfo.DocID));
            paramList.Add(new SqlParameter("@userId", userId));
            SqlParameter returnParam = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            paramList.Add(returnParam);

            SQLHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_CopyDocument", paramList.ToArray());

            int copiedDocId = (int) returnParam.Value;
            DocumentInfo newDoc = GetById(tran, copiedDocId);

            paramList.Clear();
            paramList.Add(new SqlParameter("@DocID", copiedDocId));
            paramList.Add(new SqlParameter("@IsPublic", documentInfo.IsPublic));
            paramList.Add(new SqlParameter("@DocTypeId", documentInfo.DocTypeId));
            paramList.Add(new SqlParameter("@County", documentInfo.County));
            paramList.Add(new SqlParameter("@State", documentInfo.State));
            paramList.Add(new SqlParameter("@DateFiledYear", documentInfo.DateFiledYear));
            paramList.Add(new SqlParameter("@DateFiledMonth", documentInfo.DateFiledMonth));
            paramList.Add(new SqlParameter("@DateFiledDay", documentInfo.DateFiledDay));
            paramList.Add(new SqlParameter("@DateSignedYear", documentInfo.DateSignedYear));
            paramList.Add(new SqlParameter("@DateSignedMonth", documentInfo.DateSignedMonth));
            paramList.Add(new SqlParameter("@DateSignedDay", documentInfo.DateSignedDay));
            paramList.Add(new SqlParameter("@Volume", (null != documentInfo.Volume) ? documentInfo.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != documentInfo.Page) ? documentInfo.Page : SqlString.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != documentInfo.DocumentNo) ? documentInfo.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@ResearchNote", (null != documentInfo.ResearchNote) ? documentInfo.ResearchNote : SqlString.Null));
            paramList.Add(new SqlParameter("@ImageLink", (null != documentInfo.ImageLink) ? documentInfo.ImageLink : SqlString.Null));
            paramList.Add(new SqlParameter("@CreatedBy", userId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            if (null != documentInfo.Buyer) {
                documentInfo.Buyer.DocID = copiedDocId;
                documentInfo.Buyer.ParticipantID = newDoc.Buyer.ParticipantID;

                if (documentInfo.Buyer.ParticipantID > 0) {
                    ParticipantDM.Update(tran, documentInfo.Buyer);
                } else {
                    ParticipantDM.Create(tran, documentInfo.Buyer);
                }
            }

            if (null != documentInfo.Seller) {
                documentInfo.Seller.DocID = copiedDocId;
                documentInfo.Seller.ParticipantID = newDoc.Seller.ParticipantID;

                if (documentInfo.Seller.ParticipantID > 0) {
                    ParticipantDM.Update(tran, documentInfo.Seller);
                } else {
                    ParticipantDM.Create(tran, documentInfo.Seller);
                }
            }

            return newDoc;
        }

        #endregion

        #region Properties

        private TractDataMapper TractDM {
            get {
                if (null == m_tractDM) {
                    m_tractDM = new TractDataMapper();
                }

                return m_tractDM;
            }
        }

        private ParticipantDataMapper ParticipantDM {
            get {
                if (null == m_participantDM) {
                    m_participantDM = new ParticipantDataMapper();
                }

                return m_participantDM;
            }
        }

        #endregion
    }
}