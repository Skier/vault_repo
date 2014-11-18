using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.DeedPro.Data;
using TractInc.DeedPro.Entity;

namespace TractInc.DeedPro
{
    public class Document
    {
        #region Fields

        private DocDataMapper m_docDM;

        #endregion

        #region Methods

        public List<DocumentInfo> FindDocumentsByTemplate(DocumentInfo template)
        {
            List<DocumentInfo> docList;
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetListByTemplate(tran, template);

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

        public DocumentInfo SaveDocument(SqlTransaction tran, DocumentInfo document, int userId) {
            if (document.DocID > 0) {
                DocDM.Update(tran, document, userId);
            } else {
                DocDM.Create(tran, document, userId);
            }

            return document;
        }

        public DocumentInfo OpenDocument(SqlTransaction tran, int docId) {
            return DocDM.GetDocumentById(tran, docId);
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
