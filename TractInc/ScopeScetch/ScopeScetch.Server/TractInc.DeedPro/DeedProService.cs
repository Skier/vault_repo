using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.DeedPro.Data;
using TractInc.DeedPro.Entity;

namespace TractInc.DeedPro
{
    public class DeedProService
    {
        private User m_userBC;
        private Tract m_tractBC;
        private Document m_documentBC;

        
        public UserInfo Login (string login, string password) {
            return UserBC.Login(login, password);
        }
        
        public bool SendPassword(string login){
            return UserBC.SendPassword(login);
        }

        public TractInfo SaveTract(TractInfo tract, int userId) {
            
            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                if (null != tract.ParentDocument) {
                    DocumentBC.SaveDocument(tran, tract.ParentDocument, userId);
                    tract.DocId = tract.ParentDocument.DocID;
                }
                        
                TractBC.SaveTract(tran, tract, userId);

                tran.Commit();
            }

            return tract;
        }
        
        public TractInfo LoadTract(int tractId) {
            TractInfo tract;

            using (SqlConnection conn = SQLHelper.CreateConnection()) 
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                tract = TractBC.OpenTract(tractId);

                if (null != tract && tract.DocId > 0) {
                    tract.ParentDocument = DocumentBC.OpenDocument(tran, tract.DocId);
                }

                tran.Commit();
            }

            return tract;
        }
        
        public List<DocumentInfo> FindDocumentsByTemplate(DocumentInfo temlate) {
            return DocumentBC.FindDocumentsByTemplate(temlate);
        }
        
        public List<TractListInfo> GetRecentUserTractList(int userId) {
            return TractBC.GetRecentUserTractList(userId);
        }

        public List<TractListInfo> GetDrawingList() {
            return TractBC.GetDrawingList();
        }

        public List<TractListInfo> GetTractList(int documentId) {
            return TractBC.GetTractList(documentId);
        }

        private User UserBC {
            get {
                if (null == m_userBC) {
                    m_userBC = new User();
                }
                
                return m_userBC;
            }
        }

        private Tract TractBC {
            get {
                if (null == m_tractBC) {
                    m_tractBC = new Tract();
                }
                return m_tractBC;
            }
        }

        private Document DocumentBC {
            get {
                if (null == m_documentBC) {
                    m_documentBC = new Document();
                }

                return m_documentBC;
            }
        }
    }
}
