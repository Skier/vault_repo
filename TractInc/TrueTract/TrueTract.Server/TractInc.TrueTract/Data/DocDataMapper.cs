using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class DocDataMapper
    {
        #region Constants

//        private const string DOCUMENT_IS_NOT_UNIQUE = "Document with such unique fields (State, County, DocType, DocNu, Volume, Page) is already exists";

        private const string SQL_SELECT = @"
            SELECT DISTINCT
                [TT_Document].DocID,
                [TT_Document].IsPublic,
                [TT_Document].DocTypeId,
                [TT_Document].Volume,
                [TT_Document].Page,
                [TT_Document].DocumentNo,
                [TT_Document].County,
                [TT_Document].State,
                [TT_Document].Filed,
                [TT_Document].Signed,
                [TT_Document].ResearchNote,
                [TT_Document].ImageLink,
                [TT_Document].CreatedBy,
                [TT_Document].Created,
                [TT_Document].DocBranchUid,
                [TT_Document].IsActive,
                [TT_Document].PreviousVersion,
                IsNull(Tracts.TractsCount, 0) as TractsCount, 
                IsNull(Tracts.TractsAcres, 0) as TractsAcres
              FROM [TT_Document]
                left join
                    (select Tract.DocId, count(*) TractsCount, sum(CalledAC / TT_Unit.AcresRate) TractsAcres
                       from TT_Tract as Tract 
                            join TT_Unit on TT_Unit.UnitId = Tract.UnitId
                      group by DocId
                    ) as Tracts on Tracts.DocId = [TT_Document].DocId
        ";

        private const string SQL_SELECT_BY_DOC_ID = SQL_SELECT + @" 
             WHERE [TT_Document].DocID = @DocId ";

        private const string SQL_SELECT_ACTUAL_BY_DOC_ID = SQL_SELECT + @" 
             WHERE [TT_Document].DocBranchUid IN ( SELECT DocBranchUid 
                                                  FROM [TT_Document] 
                                                 WHERE [TT_Document].DocId = @DocId ) 
               AND [TT_Document].IsActive = 1 ";

        private const string SQL_SELECT_BY_DOC_BRANCH_UID = SQL_SELECT + @" 
             WHERE [TT_Document].DocBranchUid = @DocBranchUid ";
        
        private const string SQL_SELECT_BY_CREATED_BY = SQL_SELECT + @" 
             WHERE CreatedBy = @CreatedBy ";

        private const string SQL_SELECT_RECENT_ONLY = SQL_SELECT + @"
             WHERE DocBranchUid in (select DocBranchUid 
                                      from [TT_Document] 
                                     where CreatedBy = @CreatedBy 
                                       and Created > GetDate() - 5)
        ";

        private const string SQL_AND_ACTIVE_ONLY = @" 
               AND [TT_Document].IsActive = 1 ";

        private const string SQL_AND_INACTIVE_ONLY = @" 
               AND [TT_Document].IsActive = 0 ";
        
        private const string SQL_AND_PARTICIPANT_LIKE = @"
               AND EXISTS (select 1 from TT_Participant 
                            where TT_Participant.DocID = [TT_Document].DocId
                              and TT_Participant.IsSeller = {0}
                              and TT_Participant.AsNamed like '%{1}%' )
        ";

        private const string SQL_DATE_FIELD_BETWEEN = @"
               AND {0} between '{1}' and '{2}'
        ";

        private const string SQL_SELECT_BY_GROUP_AND_USER = SQL_SELECT + @"
                INNER JOIN [TT_GroupItem] on [TT_GroupItem].DocBranchUid = [TT_Document].DocBranchUid
                INNER JOIN [TT_GroupUser] on [TT_GroupUser].GroupId = [TT_GroupItem].GroupId
             WHERE [TT_GroupUser].GroupId = @GroupId
               AND [TT_GroupUser].UserId = @UserId
        ";

        private const string SQL_SELECT_BY_PROJECT = SQL_SELECT + @"
                inner join TT_ProjectTabDocument td on [TT_Document].DocId = td.DocumentId 
                inner join TT_ProjectTab pt on td.ProjectTabId = pt.ProjectTabId
                inner join Project p on pt.ProjectId = p.ProjectId 
            where p.ProjectId = @ProjectId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [TT_Document] (
                IsPublic,
                DocTypeId,
                Volume,
                Page,
                DocumentNo,
                County,
                State,
                ResearchNote,
                ImageLink,
                CreatedBy,
                Filed,
                Signed,
                Created,
                IsActive,
                DocBranchUid,
                PreviousVersion
                ) 
                 VALUES ( @IsPublic, @DocTypeId, @Volume, @Page, @DocumentNo, @County, @State, 
                    @ResearchNote, @ImageLink, @CreatedBy, @Filed, @Signed, @Created, @IsActive, @DocBranchUid, @PreviousVersion)

            select scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [TT_Document] set 
                IsPublic = @IsPublic,
                DocTypeId = @DocTypeId,
                Volume = @Volume,
                Page = @Page,
                DocumentNo = @DocumentNo,
                County = @County,
                State = @State,
                ResearchNote = @ResearchNote,
                ImageLink = @ImageLink,
                CreatedBy = @CreatedBy,
                Created = GetDate(),
                Filed = @Filed,
                Signed = @Signed,
                PreviousVersion = @PreviousVersion
             WHERE DocID = @DocID
        ";

        private const string SQL_GET_TYPE_NAME = @"
            SELECT [Name] as DocTypeName FROM [TT_DocumentType]
             WHERE [DocTypeID] = @DocTypeID        
        ";

        #endregion

        #region Fields

        private TractDataMapper m_tractDM;
        private ParticipantDataMapper m_participantDM;
        private DocumentAttachmentDataMapper m_documentAttachmentsDM;
        private DocumentReferenceDataMapper m_documentReferenceDM;

        #endregion

        #region Methods

        private string GetFilterQueryString(DocumentsFilterInfo filter) {

            StringBuilder result = new StringBuilder();

            if (filter.buyer != null) {
                result.Append(string.Format(SQL_AND_PARTICIPANT_LIKE, "0", filter.buyer));
            }

            if (filter.seller != null) {
                result.Append(string.Format(SQL_AND_PARTICIPANT_LIKE, "1", filter.seller));
            }

            if (filter.filedRange != null)
            {
                result.Append(string.Format(SQL_DATE_FIELD_BETWEEN, "filed", 
                    filter.filedRange.dateFrom.ToString("MM-dd-yyyy"), 
                    filter.filedRange.dateTo.ToString("MM-dd-yyyy")));
            }

            if (filter.signedRange != null)
            {
                result.Append(string.Format(SQL_DATE_FIELD_BETWEEN, "signed", 
                    filter.signedRange.dateFrom.ToString("MM-dd-yyyy"), 
                    filter.signedRange.dateTo.ToString("MM-dd-yyyy")));
            }

            if (filter.createdRange != null)
            {
                result.Append(string.Format(SQL_DATE_FIELD_BETWEEN, "created", 
                    filter.createdRange.dateFrom.ToString("MM-dd-yyyy"), 
                    filter.createdRange.dateTo.ToString("MM-dd-yyyy")));
            }

            if (filter.documentId != 0) {
                result.Append(" and DocumentId = ");
                result.Append(filter.documentId.ToString());
            }

            if (filter.docTypeId != 0) {
                result.Append(" and DocTypeId = ");
                result.Append(filter.docTypeId.ToString());
            }
            
            if (filter.stateId != 0) {
                result.Append(" and State = ");
                result.Append(filter.stateId.ToString());
            }

            if (filter.countyId != 0) {
                result.Append(" and County = ");
                result.Append(filter.countyId.ToString());
            }
            if (filter.docNumber != null && filter.docNumber.Trim() != String.Empty) {
                result.Append(" and ( DocumentNo = '");
                result.Append(filter.docNumber);
                result.Append("' or ( ");

                if (filter.volume != null && filter.volume.Trim() != String.Empty) {
                    result.Append(" Volume = '");
                    result.Append(filter.volume);
                    result.Append("' and ");
                } else {
                    result.Append(" 1!=1 and ");
                }

                if (filter.page != null && filter.page.Trim() != String.Empty) {
                    result.Append(" Page = '");
                    result.Append(filter.page);
                    result.Append("' and ");
                } else {
                    result.Append(" 1!=1 and ");
                }

                result.Append(" 1=1 ");

                result.Append(" ) ) ");
            } else {
                if (filter.volume != null && filter.volume.Trim() != String.Empty) {
                    result.Append(" and volume = '");
                    result.Append(filter.volume);
                    result.Append("' ");
                }
                if (filter.page != null && filter.page.Trim() != String.Empty) {
                    result.Append(" and Page = '");
                    result.Append(filter.page);
                    result.Append("' ");
                }
            }

            return result.ToString();
        }

        public List<DocumentInfo> GetRecent(
            SqlTransaction tran, int userId, DocumentsFilterInfo filter)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@CreatedBy", userId));

            string sqlQuery = SQL_SELECT_RECENT_ONLY + SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);

            return Select(tran, sqlQuery, paramList);
        }

        public List<DocumentInfo> GetByGroupAndUser(
            SqlTransaction tran, int groupId, int userId, DocumentsFilterInfo filter)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@UserId", userId));

            string sqlQuery = SQL_SELECT_BY_GROUP_AND_USER + SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);
            
            return Select(tran, sqlQuery, paramList);
        }

        public List<DocumentInfo> GetByProject(
            SqlTransaction tran, int projectId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@ProjectId", projectId));

            string sqlQuery = SQL_SELECT_BY_PROJECT;

            return Select(tran, sqlQuery, paramList);
        }

        public DocumentInfo GetById(SqlTransaction tran, int docId)
        {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocId", docId));
            
            List<DocumentInfo> list = Select(tran, SQL_SELECT_BY_DOC_ID, paramList);
            
            if (list.Count == 0)
            {
                return null;
            } else
            {
                return list[0];
            }
        }

        public DocumentInfo GetActualByDocId(SqlTransaction tran, int docId)
        {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocId", docId));

            List<DocumentInfo> list = Select(tran, SQL_SELECT_ACTUAL_BY_DOC_ID, paramList);

            if (list.Count == 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }

        public DocumentInfo GetDocumentReferences(SqlTransaction tran, int documentId)
        {
            DocumentInfo document = GetById(tran, documentId);
            
            foreach (DocumentReferenceInfo docRef in document.References)
            {
                docRef.ReferencedDoc = GetById(tran, docRef.ReferenceId);
            }

            return document;
        }
        
        public List<DocumentInfo> GetAll(SqlTransaction tran, DocumentsFilterInfo filter, bool canBeInactive)
        {

            string sqlQuery = SQL_SELECT + " WHERE 1=1 ";

            if (!canBeInactive)
                sqlQuery += SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);

            return Select(tran, sqlQuery, new List<SqlParameter>());
        }

        public List<DocumentInfo> GetByCreatedBy(SqlTransaction tran, int userId, DocumentsFilterInfo filter)
        {

            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@CreatedBy", userId));
            
            string sqlQuery = SQL_SELECT_BY_CREATED_BY + SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);

            return Select(tran, sqlQuery, paramList);
        }

        public List<DocumentInfo> GetDocumentRevisioins(SqlTransaction tran, string docBranchId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocBranchUid", docBranchId));
            
            return Select(tran, SQL_SELECT_BY_DOC_BRANCH_UID + SQL_AND_INACTIVE_ONLY, paramList);
        }

        private List<DocumentInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList) {
            
            List<DocumentInfo> result = new List<DocumentInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray())) {
                
                while (dataReader.Read()) {
                    DocumentInfo documentInfo = new DocumentInfo();

                    documentInfo.DocID = dataReader.GetInt32(dataReader.GetOrdinal("DocID"));
                    
                    documentInfo.IsPublic = dataReader.GetSqlBoolean(dataReader.GetOrdinal("IsPublic")).IsTrue;

                    documentInfo.DocTypeId = dataReader.GetInt32(dataReader.GetOrdinal("DocTypeId"));

                    documentInfo.Volume = dataReader.IsDBNull(dataReader.GetOrdinal("Volume")) 
                        ? null : dataReader.GetString(dataReader.GetOrdinal("Volume"));

                    documentInfo.Page = dataReader.IsDBNull(dataReader.GetOrdinal("Page")) 
                        ? null : dataReader.GetString(dataReader.GetOrdinal("Page"));

                    documentInfo.DocumentNo = dataReader.IsDBNull(dataReader.GetOrdinal("DocumentNo")) 
                        ? null : dataReader.GetString(dataReader.GetOrdinal("DocumentNo"));

                    documentInfo.County = dataReader.GetInt32(dataReader.GetOrdinal("County"));

                    documentInfo.State = dataReader.GetInt32(dataReader.GetOrdinal("State"));

                    documentInfo.Filed = dataReader.IsDBNull(dataReader.GetOrdinal("Filed"))
                        ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("Filed"));

                    documentInfo.Signed = dataReader.IsDBNull(dataReader.GetOrdinal("Signed"))
                        ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("Signed"));

                    documentInfo.Created = dataReader.GetDateTime(dataReader.GetOrdinal("Created"));

                    documentInfo.CreatedBy = dataReader.GetInt32(dataReader.GetOrdinal("CreatedBy"));

                    documentInfo.IsActive = dataReader.GetSqlBoolean(dataReader.GetOrdinal("IsActive")).IsTrue;

                    documentInfo.DocBranchUid = dataReader.GetSqlGuid(dataReader.GetOrdinal("DocBranchUid")).ToString();

                    documentInfo.PreviousVersion = dataReader.IsDBNull(dataReader.GetOrdinal("PreviousVersion"))
                        ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("PreviousVersion"));

                    documentInfo.TractsCount = dataReader.GetInt32(dataReader.GetOrdinal("TractsCount"));

                    documentInfo.TractsAcres = dataReader.GetSqlDouble(dataReader.GetOrdinal("TractsAcres")).Value;

                    result.Add(documentInfo);
                }
            }

            foreach (DocumentInfo info in result)
            {
                List<ParticipantInfo> participants = ParticipantDM.GetParticipantsByDocId(tran, info.DocID);

                foreach (ParticipantInfo participant in participants) {
                    if (participant.IsSeler) {
                        info.Seller = participant;
                    } else {
                        info.Buyer = participant;
                    }
                }

                FileDataMapper fileDM = new FileDataMapper();

                info.Attachments = DocumentAttachmentsDM.GetByDocument(tran, info.DocID).ToArray();
                foreach (DocumentAttachmentInfo attachment in info.Attachments)
                {
                    attachment.FileRef = fileDM.GetById(tran, attachment.FileId);
                }

                info.Tracts = TractDM.GetTractsByDocId(tran, info.DocID, true).ToArray();

                info.References = DocumentReferenceDM.GetByDocumentId(tran, info.DocID).ToArray();

                info.DocTypeName = getTypeName(tran, info.DocTypeId);
            }

            return result;
        }
        
        private string getTypeName(SqlTransaction tran, int docTypeId)
        {
            string result = null;
            
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocTypeID", docTypeId));

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_GET_TYPE_NAME, paramList.ToArray()))
            {
                if (dataReader.Read())
                {
                    result = dataReader.GetSqlString(dataReader.GetOrdinal("DocTypeName")).ToString();
                }
            }

            return result;
        }

        public DocumentInfo Create(SqlTransaction tran, DocumentInfo documentInfo) {
            List<SqlParameter> paramList = new List<SqlParameter>();

//            List<DocumentInfo> similarDocuments = GetByTemplate(tran, documentInfo);
//
//            if (similarDocuments.Count > 0)
//            {
//                throw new Exception(DOCUMENT_IS_NOT_UNIQUE);
//            }

            documentInfo.DocBranchUid = Guid.NewGuid().ToString();
            documentInfo.Created = DateTime.Now;

            paramList.Add(new SqlParameter("@IsPublic", documentInfo.IsPublic));
            paramList.Add(new SqlParameter("@DocTypeId", documentInfo.DocTypeId));
            paramList.Add(new SqlParameter("@County", documentInfo.County));
            paramList.Add(new SqlParameter("@State", documentInfo.State));
            paramList.Add(new SqlParameter("@Filed", (DateTime.MinValue != documentInfo.Filed) ? documentInfo.Filed : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@Signed", (DateTime.MinValue != documentInfo.Signed) ? documentInfo.Signed : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@Volume", (null != documentInfo.Volume) ? documentInfo.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != documentInfo.Page) ? documentInfo.Page : SqlString.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != documentInfo.DocumentNo) ? documentInfo.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@ResearchNote", (null != documentInfo.ResearchNote) ? documentInfo.ResearchNote : SqlString.Null));
            paramList.Add(new SqlParameter("@ImageLink", (null != documentInfo.ImageLink) ? documentInfo.ImageLink : SqlString.Null));
            paramList.Add(new SqlParameter("@IsActive", documentInfo.IsActive));
            paramList.Add(new SqlParameter("@Created", documentInfo.Created));
            paramList.Add(new SqlParameter("@CreatedBy", documentInfo.CreatedBy));
            paramList.Add(new SqlParameter("@DocBranchUid", documentInfo.DocBranchUid));
            paramList.Add(new SqlParameter("@PreviousVersion", documentInfo.PreviousVersion));

            object id = SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray());

            documentInfo.DocID = int.Parse(id.ToString());

            if (null != documentInfo.Tracts) {
                foreach (TractInfo tract in documentInfo.Tracts) {
                    tract.DocId = documentInfo.DocID;
                    TractDM.Create(tran, tract, documentInfo.CreatedBy);
                }
            }

            if (null != documentInfo.Attachments)
            {
                foreach (DocumentAttachmentInfo attachment in documentInfo.Attachments)
                {
                    attachment.DocumentId = documentInfo.DocID;
                    DocumentAttachmentsDM.Create(tran, attachment);
                }
            }
            
            if (null != documentInfo.Buyer) {
                documentInfo.Buyer.DocID = documentInfo.DocID;
                ParticipantDM.Create(tran, documentInfo.Buyer);
            } else
            {
                documentInfo.Buyer = new ParticipantInfo();
                documentInfo.Buyer.AsNamed = "";
                documentInfo.Buyer.FirstName = "";
                documentInfo.Buyer.MiddleName = "";
                documentInfo.Buyer.LastName = "";
                documentInfo.Buyer.DocID = documentInfo.DocID;
                documentInfo.Buyer.IsSeler = false;
                ParticipantDM.Create(tran, documentInfo.Buyer);
            }

            if (null != documentInfo.Seller) {
                documentInfo.Seller.DocID = documentInfo.DocID;
                ParticipantDM.Create(tran, documentInfo.Seller);
            }
            else
            {
                documentInfo.Seller = new ParticipantInfo();
                documentInfo.Seller.AsNamed = "";
                documentInfo.Seller.FirstName = "";
                documentInfo.Seller.MiddleName = "";
                documentInfo.Seller.LastName = "";
                documentInfo.Seller.DocID = documentInfo.DocID;
                documentInfo.Seller.IsSeler = true;
                ParticipantDM.Create(tran, documentInfo.Seller);
            }

            if (null != documentInfo.References)
            {
                foreach (DocumentReferenceInfo reference in documentInfo.References)
                {
                    reference.DocumentId = documentInfo.DocID;
                    DocumentReferenceDM.Create(tran, reference);
                }
            }

            return documentInfo;
        }

        public int CreateCopy(SqlTransaction tran, DocumentInfo documentInfo, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@docID", documentInfo.DocID));
            paramList.Add(new SqlParameter("@userId", userId));
            SqlParameter returnParam = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            paramList.Add(returnParam);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, "SET ARITHABORT ON", null);
            SQLHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_TT_CopyDocument", paramList.ToArray());

            return (int) returnParam.Value;
        }

        public DocumentInfo Update(SqlTransaction tran, DocumentInfo documentInfo, int userId) {

//            List<DocumentInfo> similarDocuments = GetByTemplate(tran, documentInfo);
//
//            if (similarDocuments.Count > 0 && similarDocuments[0].DocID != documentInfo.DocID)
//            {
//                throw new Exception(DOCUMENT_IS_NOT_UNIQUE);
//            }

            List<SqlParameter> paramList = new List<SqlParameter>();
            
            paramList.Clear();
            paramList.Add(new SqlParameter("@DocID", documentInfo.DocID));
            paramList.Add(new SqlParameter("@IsPublic", documentInfo.IsPublic));
            paramList.Add(new SqlParameter("@DocTypeId", documentInfo.DocTypeId));
            paramList.Add(new SqlParameter("@County", documentInfo.County));
            paramList.Add(new SqlParameter("@State", documentInfo.State));
            paramList.Add(new SqlParameter("@Volume", (null != documentInfo.Volume) ? documentInfo.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != documentInfo.Page) ? documentInfo.Page : SqlString.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != documentInfo.DocumentNo) ? documentInfo.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@ResearchNote", (null != documentInfo.ResearchNote) ? documentInfo.ResearchNote : SqlString.Null));
            paramList.Add(new SqlParameter("@ImageLink", (null != documentInfo.ImageLink) ? documentInfo.ImageLink : SqlString.Null));
            paramList.Add(new SqlParameter("@Filed", (DateTime.MinValue != documentInfo.Filed) ? documentInfo.Filed : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@Signed", (DateTime.MinValue != documentInfo.Signed) ? documentInfo.Signed : SqlDateTime.Null));
            paramList.Add(new SqlParameter("@CreatedBy", userId));
            paramList.Add(new SqlParameter("@PreviousVersion", documentInfo.PreviousVersion));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            if (null != documentInfo.Tracts)
            {
                TractDM.DeleteByDocId(tran, documentInfo.DocID);
                
                foreach (TractInfo tract in documentInfo.Tracts)
                {
                    tract.DocId = documentInfo.DocID;
                    TractDM.Create(tran, tract, documentInfo.CreatedBy);
                }
            }

            if (null != documentInfo.Attachments)
            {
                DocumentAttachmentsDM.DeleteByDocId(tran, documentInfo.DocID);
                
                foreach (DocumentAttachmentInfo attachment in documentInfo.Attachments)
                {
                    attachment.DocumentId = documentInfo.DocID;
                    DocumentAttachmentsDM.Create(tran, attachment);
                }
            }

            if (documentInfo.Buyer.ParticipantID > 0)
            {
                ParticipantDM.Update(tran, documentInfo.Buyer);
            } else {
                documentInfo.Buyer.DocID = documentInfo.DocID;
                ParticipantDM.Create(tran, documentInfo.Buyer);
            }
            
            if (documentInfo.Seller.ParticipantID > 0) {
                ParticipantDM.Update(tran, documentInfo.Seller);
            } else {
                documentInfo.Seller.DocID = documentInfo.DocID;
                ParticipantDM.Create(tran, documentInfo.Seller);
            }

            if (null != documentInfo.References)
            {
                DocumentReferenceDM.DeleteByDocId(tran, documentInfo.DocID);

                foreach (DocumentReferenceInfo reference in documentInfo.References)
                {
                    reference.DocumentId = documentInfo.DocID;
                    DocumentReferenceDM.Create(tran, reference);
                }
            }

            return documentInfo;
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

        private DocumentAttachmentDataMapper DocumentAttachmentsDM {
            get {
                if (null == m_documentAttachmentsDM) {
                    m_documentAttachmentsDM = new DocumentAttachmentDataMapper();
                }

                return m_documentAttachmentsDM;
            }
        }

        private DocumentReferenceDataMapper DocumentReferenceDM
        {
            get
            {
                if (null == m_documentReferenceDM)
                {
                    m_documentReferenceDM = new DocumentReferenceDataMapper();
                }

                return m_documentReferenceDM;
            }
        }

        #endregion
    }
}