using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.DeedPro.Entity;

namespace TractInc.DeedPro.Data
{
    internal class DocDataMapper
    {

        #region Constants

        private const string SQL_FIND_BY_KEY_FIELDS = @"
            SELECT 
                DocID
              FROM [Document]
             WHERE 1=1 ";

        private const string SQL_SELECT_BY_DOCID = @"
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
        
        private const string SQL_CREATE = @"
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
                 VALUES ( {0}, {1}, '{2}', '{3}', '{4}', {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, '{13}', '{14}')

            select scope_identity();
        ";

        private const string SQL_UPDATE = @"
            UPDATE [Document] set 
                IsPublic = {0},
                DocTypeId = {1},
                Volume = '{2}',
                Page = '{3}',
                DocumentNo = '{4}',
                County = {5},
                State = {6},
                DateFiledYear = {7},
                DateFiledMonth = {8},
                DateFiledDay = {9},
                DateSignedYear = {10},
                DateSignedMonth = {11},
                DateSignedDay = {12},
                ResearchNote = '{13}',
                ImageLink = '{14}'
             WHERE DocID = {15}
        ";        
        #endregion

        #region Fields

        private TractDataMapper m_tractDM;
        private ParticipantDataMapper m_participantDM;

        #endregion

        #region Methods
        
        public List<DocumentInfo> GetListByTemplate(SqlTransaction tran, DocumentInfo template)
        {
            List<DocumentInfo> docList = new List<DocumentInfo>();

            string sql = SQL_FIND_BY_KEY_FIELDS;

            if (template.DocTypeId != 0)
            {
                sql += " and DocTypeId = ";
                sql += template.DocTypeId.ToString();
            }
            if (template.State != 0)
            {
                sql += " and State = ";
                sql += template.State.ToString();
            }
            if (template.County != 0)
            {
                sql += " and County = ";
                sql += template.County.ToString();
            }
            if (template.DocumentNo != null && template.DocumentNo.Trim() != String.Empty)
            {
                sql += " and ( DocumentNo = '";
                sql += template.DocumentNo;
                sql += "' or ( ";

                if (template.Volume != null && template.Volume.Trim() != String.Empty)
                {
                    sql += " Volume = '";
                    sql += template.Volume;
                    sql += "' and ";
                }
                else
                {
                    sql += " 1!=1 and ";
                }

                if (template.Page != null && template.Page.Trim() != String.Empty)
                {
                    sql += " Page = '";
                    sql += template.Page;
                    sql += "' and ";
                }
                else
                {
                    sql += " 1!=1 and ";
                }

                sql += " 1=1 ";

                sql += " ) ) ";

            }
            else
            {
                if (template.Volume != null && template.Volume.Trim() != String.Empty)
                {
                    sql += " and Volume = '";
                    sql += template.Volume;
                    sql += "' ";
                }
                if (template.Page != null && template.Page.Trim() != String.Empty)
                {
                    sql += " and Page = '";
                    sql += template.Page;
                    sql += "' ";
                }
            }

            List<int> docIdList = new List<int>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    docIdList.Add(dataReader.GetInt32(0));
                }
            }

            foreach (int docId in docIdList) {
                docList.Add(GetDocumentById(tran, docId));
            }
            
            return docList;
        }

        public DocumentInfo GetDocumentById(SqlTransaction tran, int docId)
        {
            string sql = String.Format(SQL_SELECT_BY_DOCID, docId);
            
            DocumentInfo documentInfo = null;

            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                if (dataReader.Read())
                {
                    documentInfo = new DocumentInfo();

                    documentInfo.DocID = dataReader.GetInt32(0);
                    documentInfo.IsPublic = dataReader.GetSqlBoolean(1).IsTrue;
                    documentInfo.DocTypeId = dataReader.GetInt32(2);
                    documentInfo.Volume = dataReader.GetString(3);
                    documentInfo.Page = dataReader.GetString(4);
                    documentInfo.DocumentNo = dataReader.GetString(5);
                    documentInfo.County = dataReader.GetInt32(6);
                    documentInfo.State = dataReader.GetInt32(7);
                    documentInfo.DateFiledYear = dataReader.GetInt32(8);
                    documentInfo.DateFiledMonth = dataReader.GetInt32(9);
                    documentInfo.DateFiledDay = dataReader.GetInt32(10);
                    documentInfo.DateSignedYear = dataReader.GetInt32(11);
                    documentInfo.DateSignedMonth = dataReader.GetInt32(12);
                    documentInfo.DateSignedDay = dataReader.GetInt32(13);
                    documentInfo.ResearchNote = dataReader.GetString(14);
                    documentInfo.ImageLink = dataReader.GetString(15);
                }
            }

            if (null != documentInfo) {
                List <ParticipantInfo> participants = ParticipantDM.GetParticipantsByDocId(tran, documentInfo.DocID);

                foreach (ParticipantInfo participant in participants) {
                    if (participant.IsSeler)
                        documentInfo.Seller = participant;
                    else 
                        documentInfo.Buyer = participant;
                }
            }

            return documentInfo;
        }
        
        public DocumentInfo Create(SqlTransaction tran, DocumentInfo documentInfo, int userId) {
            
            string sql = String.Format(SQL_CREATE,
                    documentInfo.IsPublic ? 1 : 0,
                    documentInfo.DocTypeId,
                    documentInfo.Volume,
                    documentInfo.Page,
                    documentInfo.DocumentNo,
                    documentInfo.County,
                    documentInfo.State,
                    documentInfo.DateFiledYear,
                    documentInfo.DateFiledMonth,
                    documentInfo.DateFiledDay,
                    documentInfo.DateSignedYear,
                    documentInfo.DateSignedMonth,
                    documentInfo.DateSignedDay,
                    documentInfo.ResearchNote,
                    documentInfo.ImageLink
                );

            documentInfo.DocID = int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            if (null != documentInfo.TractList)
            {
                foreach (TractInfo tract in documentInfo.TractList)
                {
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

        public void Update(SqlTransaction tran, DocumentInfo documentInfo, int userId) {
            
            string sql = String.Format(SQL_UPDATE,
                                       documentInfo.IsPublic ? 1 : 0,
                                       documentInfo.DocTypeId,
                                       documentInfo.Volume,
                                       documentInfo.Page,
                                       documentInfo.DocumentNo,
                                       documentInfo.County,
                                       documentInfo.State,
                                       documentInfo.DateFiledYear,
                                       documentInfo.DateFiledMonth,
                                       documentInfo.DateFiledDay,
                                       documentInfo.DateSignedYear,
                                       documentInfo.DateSignedMonth,
                                       documentInfo.DateSignedDay,
                                       documentInfo.ResearchNote,
                                       documentInfo.ImageLink, documentInfo.DocID);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
            
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
