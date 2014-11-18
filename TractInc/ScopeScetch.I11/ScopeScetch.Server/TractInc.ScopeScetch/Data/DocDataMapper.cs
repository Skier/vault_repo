using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch.Data
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
        
        public List<Document> GetListByTemplate(SqlTransaction tran, Document template, bool getRelevant)
        {
            List<Document> docList = new List<Document>();

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
            
            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    docIdList.Add(dataReader.GetInt32(0));
                }
            }

            foreach (int docId in docIdList) {
                docList.Add(GetDocumentById(tran, docId, true));
            }
            
            return docList;
        }

        public Document GetDocumentById(SqlTransaction tran, int docId, bool getRelevant)
        {
            string sql = String.Format(SQL_SELECT_BY_DOCID, docId);
            
            Document document = null;

            using (SqlDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, sql, null))
            {
                if (dataReader.Read())
                {
                    document = new Document();

                    document.DocID = dataReader.GetInt32(0);
                    document.IsPublic = dataReader.GetSqlBoolean(1).IsTrue;
                    document.DocTypeId = dataReader.GetInt32(2);
                    document.Volume = dataReader.GetString(3);
                    document.Page = dataReader.GetString(4);
                    document.DocumentNo = dataReader.GetString(5);
                    document.County = dataReader.GetInt32(6);
                    document.State = dataReader.GetInt32(7);
                    document.DateFiledYear = dataReader.GetInt32(8);
                    document.DateFiledMonth = dataReader.GetInt32(9);
                    document.DateFiledDay = dataReader.GetInt32(10);
                    document.DateSignedYear = dataReader.GetInt32(11);
                    document.DateSignedMonth = dataReader.GetInt32(12);
                    document.DateSignedDay = dataReader.GetInt32(13);
                    document.ResearchNote = dataReader.GetString(14);
                    document.ImageLink = dataReader.GetString(15);
                }
            }

            if (null != document) {
                List <Participant> participants = ParticipantDM.GetParticipantsByDocId(tran, document.DocID);

                foreach (Participant participant in participants) {
                    if (participant.IsSeler)
                        document.Seller = participant;
                    else 
                        document.Buyer = participant;
                }

                if (getRelevant) {
                    document.Tracts = TractDM.SelectTractsByDocId(tran, document.DocID, getRelevant).ToArray();
                }
            }

            return document;
        }
        
        public Document Create(SqlTransaction tran, Document document, int userId) {
            
            string sql = String.Format(SQL_CREATE,
                    document.IsPublic ? 1 : 0,
                    document.DocTypeId,
                    document.Volume,
                    document.Page,
                    document.DocumentNo,
                    document.County,
                    document.State,
                    document.DateFiledYear,
                    document.DateFiledMonth,
                    document.DateFiledDay,
                    document.DateSignedYear,
                    document.DateSignedMonth,
                    document.DateSignedDay,
                    document.ResearchNote,
                    document.ImageLink
                );

            document.DocID = int.Parse(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            if (null != document.Tracts)
            {
                foreach (Tract tract in document.Tracts)
                {
                    tract.DocId = document.DocID;
                    TractDM.Create(tran, tract, userId);
                }
            }

            if (null != document.Buyer) {
                document.Buyer.DocID = document.DocID;
                ParticipantDM.Create(tran, document.Buyer);
            }
            
            if (null != document.Seller) {
                document.Seller.DocID = document.DocID;
                ParticipantDM.Create(tran, document.Seller);
            }

            return document;
            
        }

        public void Update(SqlTransaction tran, Document document, int userId) {
            
            string sql = String.Format(SQL_UPDATE,
                                       document.IsPublic ? 1 : 0,
                                       document.DocTypeId,
                                       document.Volume,
                                       document.Page,
                                       document.DocumentNo,
                                       document.County,
                                       document.State,
                                       document.DateFiledYear,
                                       document.DateFiledMonth,
                                       document.DateFiledDay,
                                       document.DateSignedYear,
                                       document.DateSignedMonth,
                                       document.DateSignedDay,
                                       document.ResearchNote,
                                       document.ImageLink, document.DocID);

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
            
            if (null != document.Buyer) {
                if (document.Buyer.ParticipantID > 0) {
                    ParticipantDM.Update(tran, document.Buyer);
                } else {
                    ParticipantDM.Create(tran, document.Buyer);
                }
            }
            
            if (null != document.Seller) {
                if (document.Seller.ParticipantID > 0) {
                    ParticipantDM.Update(tran, document.Seller);
                } else {
                    ParticipantDM.Create(tran, document.Seller);
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
