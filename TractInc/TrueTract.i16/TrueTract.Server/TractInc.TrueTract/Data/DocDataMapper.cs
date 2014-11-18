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
                [Document].DocID,
                [Document].IsPublic,
                [Document].DocTypeId,
                [Document].Volume,
                [Document].Page,
                [Document].DocumentNo,
                [Document].County,
                [Document].State,
                [Document].Filed,
                [Document].Signed,
                [Document].ResearchNote,
                [Document].ImageLink,
                [Document].CreatedBy,
                [Document].Created,
                [Document].DocBranchUid,
                [Document].IsActive,
                IsNull(Tracts.TractsCount, 0) as TractsCount, 
                IsNull(Tracts.TractsAcres, 0) as TractsAcres
              FROM [Document]
                left join
                    (select Tract.DocId, count(*) TractsCount, sum(CalledAC / Unit.AcresRate) TractsAcres
                       from Tract 
                            join Unit on Unit.UnitId = Tract.UnitId
                      group by DocId
                    ) as Tracts on Tracts.DocId = [Document].DocId
        ";

        private const string SQL_SELECT_BY_DOC_ID = SQL_SELECT + @" 
             WHERE DocID = @DocId ";

        private const string SQL_SELECT_BY_DOC_BRANCH_UID = SQL_SELECT + @" 
             WHERE [Document].DocBranchUid = @DocBranchUid ";
        
        private const string SQL_SELECT_BY_CREATED_BY = SQL_SELECT + @" 
             WHERE CreatedBy = @CreatedBy ";

        private const string SQL_SELECT_RECENT_ONLY = SQL_SELECT + @"
             WHERE DocBranchUid in (select DocBranchUid 
                                      from [Document] 
                                     where CreatedBy = @CreatedBy 
                                       and Created > GetDate() - 5)
        ";

        private const string SQL_AND_ACTIVE_ONLY = @" 
               AND [Document].IsActive = 1 ";

        private const string SQL_AND_INACTIVE_ONLY = @" 
               AND [Document].IsActive = 0 ";
        
        private const string SQL_AND_PARTICIPANT_LIKE = @"
               AND EXISTS (select 1 from Participant 
                            where Participant.DocID = [Document].DocId
                              and Participant.IsSeller = {0}
                              and Participant.AsNamed like '%{1}%' )
        ";

        private const string SQL_DATE_FIELD_BETWEEN = @"
               AND {0} between '{1}' and '{2}'
        ";

        private const string SQL_SELECT_BY_GROUP_AND_USER = SQL_SELECT + @"
                INNER JOIN [GroupItems] on [GroupItems].DocBranchUid = [Document].DocBranchUid
                INNER JOIN [GroupUsers] on [GroupUsers].GroupId = [GroupItems].GroupId
             WHERE [GroupUsers].GroupId = @GroupId
               AND [GroupUsers].UserId = @UserId
        ";

        private const string SQL_CREATE = @"
            INSERT INTO [Document] (
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
                DocBranchUid
                ) 
                 VALUES ( @IsPublic, @DocTypeId, @Volume, @Page, @DocumentNo, @County, @State, 
                    @ResearchNote, @ImageLink, @CreatedBy, @Filed, @Signed, GetDate(), @IsActive, @DocBranchUid)

            select scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Document] set 
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
                Signed = @Signed
             WHERE DocID = @DocID
        ";

        #endregion

        #region Fields

        private TractDataMapper m_tractDM;
        private ParticipantDataMapper m_participantDM;

        #endregion

        #region Methods

        private string GetFilterQueryString(DocumentFilterInfo filter) {

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
            SqlTransaction tran, int userId, DocumentFilterInfo filter)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@CreatedBy", userId));

            string sqlQuery = SQL_SELECT_RECENT_ONLY + SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);

            return Select(tran, sqlQuery, paramList);
        }

        public List<DocumentInfo> GetByGroupAndUser(
            SqlTransaction tran, int groupId, int userId, DocumentFilterInfo filter)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@GroupId", groupId));
            paramList.Add(new SqlParameter("@UserId", userId));

            string sqlQuery = SQL_SELECT_BY_GROUP_AND_USER + SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);
            
            return Select(tran, sqlQuery, paramList);
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

        public List<DocumentInfo> GetAll(SqlTransaction tran, DocumentFilterInfo filter) {

            string sqlQuery = SQL_SELECT + " WHERE 1=1 " + SQL_AND_ACTIVE_ONLY;
            
            if (null != filter)
                sqlQuery += GetFilterQueryString(filter);

            return Select(tran, sqlQuery, new List<SqlParameter>());
        }

        public List<DocumentInfo> GetByCreatedBy(SqlTransaction tran, int userId, DocumentFilterInfo filter) {

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

                    documentInfo.Filed = dataReader.GetDateTime(dataReader.GetOrdinal("Filed"));

                    documentInfo.Signed = dataReader.GetDateTime(dataReader.GetOrdinal("Signed"));

                    documentInfo.Created = dataReader.GetDateTime(dataReader.GetOrdinal("Created"));

                    documentInfo.CreatedBy = dataReader.GetInt32(dataReader.GetOrdinal("CreatedBy"));

                    documentInfo.IsActive = dataReader.GetSqlBoolean(dataReader.GetOrdinal("IsActive")).IsTrue;

                    documentInfo.DocBranchUid = dataReader.GetSqlGuid(dataReader.GetOrdinal("DocBranchUid")).ToString();

                    documentInfo.TractsCount = dataReader.GetInt32(dataReader.GetOrdinal("TractsCount"));

                    documentInfo.TractsAcres = dataReader.GetSqlDouble(dataReader.GetOrdinal("TractsAcres")).Value;

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
        
        public DocumentInfo Create(SqlTransaction tran, DocumentInfo documentInfo) {
            List<SqlParameter> paramList = new List<SqlParameter>();

//            List<DocumentInfo> similarDocuments = GetByTemplate(tran, documentInfo);
//
//            if (similarDocuments.Count > 0)
//            {
//                throw new Exception(DOCUMENT_IS_NOT_UNIQUE);
//            }

            documentInfo.DocBranchUid = Guid.NewGuid().ToString();
            
            paramList.Add(new SqlParameter("@IsPublic", documentInfo.IsPublic));
            paramList.Add(new SqlParameter("@DocTypeId", documentInfo.DocTypeId));
            paramList.Add(new SqlParameter("@County", documentInfo.County));
            paramList.Add(new SqlParameter("@State", documentInfo.State));
            paramList.Add(new SqlParameter("@Filed", documentInfo.Filed));
            paramList.Add(new SqlParameter("@Signed", documentInfo.Signed));
            paramList.Add(new SqlParameter("@Volume", (null != documentInfo.Volume) ? documentInfo.Volume : SqlString.Null));
            paramList.Add(new SqlParameter("@Page", (null != documentInfo.Page) ? documentInfo.Page : SqlString.Null));
            paramList.Add(new SqlParameter("@DocumentNo", (null != documentInfo.DocumentNo) ? documentInfo.DocumentNo : SqlString.Null));
            paramList.Add(new SqlParameter("@ResearchNote", (null != documentInfo.ResearchNote) ? documentInfo.ResearchNote : SqlString.Null));
            paramList.Add(new SqlParameter("@ImageLink", (null != documentInfo.ImageLink) ? documentInfo.ImageLink : SqlString.Null));
            paramList.Add(new SqlParameter("@IsActive", documentInfo.IsActive));
            paramList.Add(new SqlParameter("@CreatedBy", documentInfo.CreatedBy));
            paramList.Add(new SqlParameter("@DocBranchUid", documentInfo.DocBranchUid));

            object id = SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray());

            documentInfo.DocID = int.Parse(id.ToString());

            if (null != documentInfo.TractList) {
                foreach (TractInfo tract in documentInfo.TractList) {
                    tract.DocId = documentInfo.DocID;
                    TractDM.Create(tran, tract, documentInfo.CreatedBy);
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

        public int CreateCopy(SqlTransaction tran, DocumentInfo documentInfo, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@docID", documentInfo.DocID));
            paramList.Add(new SqlParameter("@userId", userId));
            SqlParameter returnParam = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            returnParam.Direction = ParameterDirection.ReturnValue;
            paramList.Add(returnParam);

            SQLHelper.ExecuteNonQuery(tran, CommandType.StoredProcedure, "sp_CopyDocument", paramList.ToArray());

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
            paramList.Add(new SqlParameter("@Filed", documentInfo.Filed));
            paramList.Add(new SqlParameter("@Signed", documentInfo.Signed));
            paramList.Add(new SqlParameter("@CreatedBy", userId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            if (documentInfo.Buyer.ParticipantID > 0) {
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

        #endregion
    }
}