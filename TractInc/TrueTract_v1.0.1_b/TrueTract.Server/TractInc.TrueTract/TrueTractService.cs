using System.Collections.Generic;
using System.Data.SqlClient;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    public class TrueTractService
    {
        private User m_userBC;
        private Tract m_tractBC;
        private Document m_documentBC;
        private Module m_moduleBC;

        public UserInfo Login(string login, string password)
        {
            return UserBC.Login(login, password);
        }

        public UserInfo SignUp(UserInfo userInfo)
        {
            return UserBC.SignUp(userInfo);
        }

        public bool SendPassword(string login)
        {
            return UserBC.SendPassword(login);
        }

        public TractInfo SaveTract(TractInfo tract, int userId)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                //Command "SET ARITHABORT ON" is needed to work with IndexedViews 
                SqlCommand arithabortCmd = new SqlCommand("SET ARITHABORT ON", conn);
                arithabortCmd.ExecuteNonQuery();

                SqlTransaction tran = conn.BeginTransaction();

                if (null != tract.ParentDocument)
                {
                    DocumentBC.SaveDocument(tran, tract.ParentDocument, userId);
                    tract.DocId = tract.ParentDocument.DocID;
                }

                TractBC.SaveTract(tran, tract, userId);

                tran.Commit();
            }

            return tract;
        }

        public TractInfo LoadTract(int tractId)
        {
            TractInfo tract;

            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                tract = TractBC.OpenTract(tractId);

                if (null != tract && tract.DocId > 0)
                {
                    tract.ParentDocument = DocumentBC.OpenDocument(tran, tract.DocId);
                }

                tran.Commit();
            }

            return tract;
        }

        public List<DocumentInfo> FindDocumentsByTemplate(DocumentInfo temlate)
        {
            return DocumentBC.FindDocumentsByTemplate(temlate);
        }

        public List<TractListInfo> GetRecentUserTractList(int userId)
        {
            return TractBC.GetRecentUserTractList(userId);
        }

        public List<TractListInfo> GetDrawingList()
        {
            return TractBC.GetDrawingList();
        }

        public List<TractListInfo> GetTractList(int documentId)
        {
            return TractBC.GetTractList(documentId);
        }

        public List<ModuleInfo> GetModuleListByUserId(int userId)
        {
            return ModuleBC.GetModuleListByUserId(userId);
        }
        
        private User UserBC
        {
            get
            {
                if (null == m_userBC)
                {
                    m_userBC = new User();
                }

                return m_userBC;
            }
        }

        private Tract TractBC
        {
            get
            {
                if (null == m_tractBC)
                {
                    m_tractBC = new Tract();
                }
                return m_tractBC;
            }
        }

        private Document DocumentBC
        {
            get
            {
                if (null == m_documentBC)
                {
                    m_documentBC = new Document();
                }

                return m_documentBC;
            }
        }

        private Module ModuleBC
        {
            get
            {
                if (null == m_moduleBC)
                {
                    m_moduleBC = new Module();
                }

                return m_moduleBC;
            }
        }
    }
}