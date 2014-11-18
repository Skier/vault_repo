using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    public class Document
    {
        #region Fields

        private DocDataMapper m_docDM;
        private UserDataMapper m_userDM;
        private DocAttachmentDataMapper m_docAttDM;

        #endregion

        #region Methods

        public List<DocumentInfo> GetDocumentList(int userId, DocumentsFilterInfo filter)
        {
            List<DocumentInfo> docList;

            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetByCreatedBy(tran, userId, filter);

                FillUserNames(tran, docList);
                
                tran.Commit();
            }

            return docList;
        }

        public List<DocumentInfo> GetByGroupAndUser(int groupId, int userId, DocumentsFilterInfo filter)
        {
            List<DocumentInfo> docList;

            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetByGroupAndUser(tran, groupId, userId, filter);

                FillUserNames(tran, docList);
                
                tran.Commit();
            }
            
            return docList;
        }

        public List<DocumentInfo> GetRecent(int userId, DocumentsFilterInfo filter)
        {
            List<DocumentInfo> docList;

            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetRecent(tran, userId, filter);

                FillUserNames(tran, docList);

                tran.Commit();
            }
            
            return docList;
            
        }
        
        public List<DocumentInfo> GetDocumentList(DocumentsFilterInfo filter)
        {
            List<DocumentInfo> docList;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetAll(tran, filter);

                FillUserNames(tran, docList);

                tran.Commit();
            }
            
            return docList;
        }

        public void AddAttachment(DocumentAttachmentInfo attachment)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                DocAttDM.Create(tran, attachment);

                tran.Commit();
            }
        }

        public void UpdateAttachment(DocumentAttachmentInfo attachment)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                DocAttDM.Update(tran, attachment);

                tran.Commit();
            }
        }

        public void DeleteAttachment(DocumentAttachmentInfo attachment, int userId)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                DocumentInfo document = DocDM.GetById(tran, attachment.DocumentId);

                SaveDocument(tran, document, userId);

                DocAttDM.Delete(tran, attachment);

                tran.Commit();
            }
        }

        public List<DocumentInfo> GetDocumentBranchRevisions(string docBranchUid)
        {
            List<DocumentInfo> docList;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetDocumentRevisioins(tran, docBranchUid);

                FillUserNames(tran, docList);
                
                tran.Commit();
            }
            
            return docList;
        }

        public DocumentInfo SaveDocument(DocumentInfo document, int userId) {

            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                
                SaveDocument(tran, document, userId);

                FillUserNames(tran, new List<DocumentInfo>(new DocumentInfo[]{ document }));

                tran.Commit();
            }

            return document;
        }

        public DocumentInfo SaveDocument(SqlTransaction tran, DocumentInfo document, int userId)
        {
            if (document.DocID > 0){
                DocDM.CreateCopy(tran, document, userId);
                document = DocDM.Update(tran, document, userId);
            } else {
                document.CreatedBy = userId;
                document.IsActive = true;
                document = DocDM.Create(tran, document);
            }

            FillUserNames(tran, new List<DocumentInfo>(new DocumentInfo[]{ document }));

            return document;
        }

        public DocumentInfo OpenDocument(SqlTransaction tran, int docId) {
            return DocDM.GetById(tran, docId);
        }
        
        private void FillUserNames(SqlTransaction tran, List<DocumentInfo> list)
        {
            Hashtable userNames = new Hashtable();

            foreach (DocumentInfo info in list)
            {
                if (userNames[info.CreatedBy] == null)
                {
                    userNames[info.CreatedBy] = UserDM.GetUserById(tran, info.CreatedBy).Login;
                }
                
                info.CreatedByName = (string) userNames[info.CreatedBy];
            }
        }

        #endregion

        #region Properties

        private DocDataMapper DocDM {
            get {
                if (null == m_docDM) {
                    m_docDM = new DocDataMapper();
                }
                
                return m_docDM;
            }
        }

        private DocAttachmentDataMapper DocAttDM {
            get {
                if (null == m_docAttDM) {
                    m_docAttDM = new DocAttachmentDataMapper();
                }
                
                return m_docAttDM;
            }
        }

        private UserDataMapper UserDM {
            get {
                if (null == m_userDM) {
                    m_userDM = new UserDataMapper();
                }
                
                return m_userDM;
            }
        }
        
        #endregion
    }
}
