using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.ScopeScetch.Data;
using TractInc.ScopeScetch.Entity;

namespace TractInc.ScopeScetch
{
    public class DocService
    {
        #region Fields

        private DocDataMapper m_docDM;

        #endregion

        #region Methods

        public List<Document> FindDocumentsByTemplate(Document template, bool getRelevant)
        {
            List<Document> docList;
            
            using (SqlConnection conn = SqlHelper.CreateConnection()) {
                conn.Open();
                
                SqlTransaction tran = conn.BeginTransaction();
                
                docList = DocDM.GetListByTemplate(tran, template, getRelevant);

                tran.Commit();
            }
            
            return docList;
        }
        
        public Document CreateDocument(Document document, int userId)
        {
            Document result;
            
            using (SqlConnection conn = SqlHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                result = DocDM.Create(tran, document, userId);
                
                tran.Commit();
            }

            return result;
            
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
