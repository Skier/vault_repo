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

        #endregion

        #region Methods

        public List<DocumentInfo> GetDocumentList(int userId)
        {
            List<DocumentInfo> docList;

            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetByCreatedBy(tran, userId);

                tran.Commit();
            }
            
            return docList;
            
        }
        
        public List<DocumentInfo> GetDocumentList(DocumentInfo template)
        {
            List<DocumentInfo> docList;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetByTemplate(tran, template);

                tran.Commit();
            }
            
            return docList;
        }

        public List<DocumentInfo> GetDocumentBranchRevisions(string docBranchUid)
        {
            List<DocumentInfo> docList;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetDocumentRevisioins(tran, docBranchUid);

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
                
                tran.Commit();
            }

            return document;
        }

        public DocumentInfo SaveDocument(SqlTransaction tran, DocumentInfo document, int userId)
        {
            if (document.DocID > 0) {
                document = DocDM.Update(tran, document, userId);
            } else {
                document = DocDM.Create(tran, document, userId);
            }

            return document;
        }

        public DocumentInfo OpenDocument(SqlTransaction tran, int docId) {
            return DocDM.GetById(tran, docId);
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

        #endregion
    }
}
