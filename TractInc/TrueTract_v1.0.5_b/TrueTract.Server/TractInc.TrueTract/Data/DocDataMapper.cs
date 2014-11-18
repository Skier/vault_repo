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

        private const string SQL_FIND_BY_KEY_FIELDS =
            @"
            SELECT 
                DocID
              FROM [Document]
             WHERE 1=1 ";

        private const string SQL_SELECT_BY_DOCID =
            @"
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
                ImageLink
              FROM [Document]
             WHERE DocID = {0} ";

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
                ImageLink
                ) 
                 VALUES ( @IsPublic, @DocTypeId, @Volume, @Page, @DocumentNo, @County, @State, 
                    @DateFiledYear, @DateFiledMonth, @DateFiledDay, 
                    @DateSignedYear, @DateSignedMonth, @DateSignedDay, @ResearchNote, @ImageLink)

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
                ImageLink = @ImageLink
             WHERE DocID = @DocID
        ";

        #endregion

        #region Fields

        private TractDataMapper m_tractDM;
        private ParticipantDataMapper m_participantDM;
        private DocAttachmentDataMapper m_attachmentDM;

        #endregion

        #region Methods

        public List<DocumentInfo> GetListByTemplate(SqlTransaction tran, DocumentInfo template) {
            List<DocumentInfo> docList = new List<DocumentInfo>();

            string sql = SQL_FIND_BY_KEY_FIELDS;

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

            List<int> docIdList = new List<int>();

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null)) {
                while (dataReader.Read()) {
                    docIdList.Add(dataReader.GetInt32(0));
                }
            }

            foreach (int docId in docIdList) {
                docList.Add(GetDocumentById(tran, docId));
            }

            return docList;
        }

        public DocumentInfo GetDocumentById(SqlTransaction tran, int docId) {
            string sql = String.Format(SQL_SELECT_BY_DOCID, docId);

            DocumentInfo documentInfo = null;

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null)) {
                if (dataReader.Read()) {
                    documentInfo = new DocumentInfo();

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
                }
            }

            if (null != documentInfo) {
                List<ParticipantInfo> participants = ParticipantDM.GetParticipantsByDocId(tran, documentInfo.DocID);

                foreach (ParticipantInfo participant in participants) {
                    if (participant.IsSeler) {
                        documentInfo.Seller = participant;
                    } else {
                        documentInfo.Buyer = participant;
                    }
                }
            }

            if (null != documentInfo)
            {
                DocAttachmentInfo attachment = AttachmentDM.GetAttachmentByDocId(tran, documentInfo.DocID);
                documentInfo.Attachment = attachment;
            }

            return documentInfo;
        }

        public DocumentInfo Create(SqlTransaction tran, DocumentInfo documentInfo, int userId) {
            List<SqlParameter> paramList = new List<SqlParameter>();

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

            if (null != documentInfo.Attachment)
            {
                documentInfo.Attachment.DocId = documentInfo.DocID;
                AttachmentDM.Create(tran, documentInfo.Attachment);
            }

            return documentInfo;
        }

        public void Update(SqlTransaction tran, DocumentInfo documentInfo, int userId) {
            
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@DocID", documentInfo.DocID));
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

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());

            if (null != documentInfo.Buyer) {
                if (documentInfo.Buyer.ParticipantID > 0) {
                    ParticipantDM.Update(tran, documentInfo.Buyer);
                } else {
                    ParticipantDM.Create(tran, documentInfo.Buyer);
                }
            }

            if (null != documentInfo.Seller) {
                if (documentInfo.Seller.ParticipantID > 0) {
                    ParticipantDM.Update(tran, documentInfo.Seller);
                } else {
                    ParticipantDM.Create(tran, documentInfo.Seller);
                }
            }

            AttachmentDM.DeleteByDocumentId(tran, documentInfo.DocID);
            
            if (null != documentInfo.Attachment)
            {
                documentInfo.Attachment.DocId = documentInfo.DocID;
                AttachmentDM.Create(tran, documentInfo.Attachment);
            }

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

        private DocAttachmentDataMapper AttachmentDM
        {
            get
            {
                if (null == m_attachmentDM)
                {
                    m_attachmentDM = new DocAttachmentDataMapper();
                }

                return m_attachmentDM;
            }
        }

        #endregion
    }
}