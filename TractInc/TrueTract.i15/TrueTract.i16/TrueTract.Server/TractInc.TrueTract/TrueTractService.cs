using System;
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

        public DocumentInfo ActivateDocumentRevision(DocumentInfo document, int userId)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                //Command "SET ARITHABORT ON" is needed to work with IndexedViews 
                SqlCommand arithabortCmd = new SqlCommand("SET ARITHABORT ON", conn);
                arithabortCmd.ExecuteNonQuery();

                SqlTransaction tran = conn.BeginTransaction();

                //TODO: ??

                tran.Commit();
            }

            return null;
        }

        public DocumentInfo SaveDocument(DocumentInfo document, int userId)
        {
            DocumentInfo newDocRevision;

            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                //Command "SET ARITHABORT ON" is needed to work with IndexedViews 
                SqlCommand arithabortCmd = new SqlCommand("SET ARITHABORT ON", conn);
                arithabortCmd.ExecuteNonQuery();

                SqlTransaction tran = conn.BeginTransaction();

                newDocRevision = DocumentBC.SaveDocument(tran, document, userId);

                tran.Commit();
            }

            return newDocRevision;
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

                if (tract.TractId > 0)
                {
                    if (null != tract.ParentDocument)
                    {
                        DocumentBC.SaveDocument(tran, tract.ParentDocument, userId);
                        tract.DocId = tract.ParentDocument.DocID;
                    }

                    TractBC.SaveTract(tran, tract, userId);
                    
                } else
                {
                    TractBC.CreateTract(tran, tract, userId);
                }
                
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

                tract = TractBC.LoadTract(tractId);

                
                tran.Commit();
            }

            return tract;
        }

        public List<TractInfo> FindDrawingsByTemplate(int userId, string refName)
        {
            if (refName.Length > 0)
            {
                return TractBC.GetUserDrawingsByRefName(userId, refName);
            } else
            {
                return TractBC.GetUserDrawings(userId);
            }
        }

        public List<DocumentInfo> GetDocuments(DocumentFilterInfo filter)
        {
            return DocumentBC.GetDocumentList(filter);
        }

        public List<DocumentInfo> GetDocumentBranchRevisions(string docBrachUid)
        {
            return DocumentBC.GetDocumentBranchRevisions(docBrachUid);
        }
        
        public List<TractInfo> GetDocumentTractList(int documentId)
        {
            return TractBC.GetTractList(documentId);
        }

        public List<ModuleInfo> GetModuleListByUserId(int userId)
        {
            return ModuleBC.GetModuleListByUserId(userId);
        }

        public List<UserGroupInfo> GetGroupListByUserId(int userId)
        {
            List<UserGroupInfo> result;

            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                result = (new GroupDataMapper()).SelectByUserId(tran, userId);

                tran.Commit();
            }
            
            return result;
        }

        public void SaveUserGroup(int groupId, string groupName)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                (new GroupDataMapper()).Update(tran, groupId, groupName);

                tran.Commit();
            }
        }

        public int CreateUserGroup(string groupName, int userId)
        {
            int groupId;
            
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                groupId = (new GroupDataMapper()).Create(tran, groupName);
                (new GroupUsersDataMapper()).Create(tran, userId, groupId);

                tran.Commit();
            }
            
            return groupId;
        }

        public void DeleteUserGroup(int groupId)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                (new GroupDataMapper()).Delete(tran, groupId);

                tran.Commit();
            }
        }

        public UserGroupInfo LoadUserItemsGroup(int userId, DocumentFilterInfo docFilter)
        {
            UserGroupInfo result = new UserGroupInfo();
            result.systemGroup = true;
            result.groupDocuments = DocumentBC.GetDocumentList(userId, docFilter);
//            result.groupDrawings = TractBC.GetUserDrawings(userId);

            return result;
        }
        
        public UserGroupInfo LoadUserRecentItemsGroup(int userId, DocumentFilterInfo docFilter)
        {
            UserGroupInfo result = new UserGroupInfo();

            result.systemGroup = true;
            result.groupDocuments = DocumentBC.GetRecent(userId, docFilter);

            return result;
        }
        
        public void AddDocumentToGroup(int groupId, string docBranchUid)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                GroupItemsDataMapper groupItemsDM = new GroupItemsDataMapper();

                if (!groupItemsDM.IsGroupContains(tran, groupId, docBranchUid))
                {
                    groupItemsDM.Create(tran, groupId, docBranchUid);
                }

                tran.Commit();
            }
        }

        public void RemoveDocumentFromGroup(int groupId, string docBranchUid)
        {
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                GroupItemsDataMapper groupItemsDM = new GroupItemsDataMapper();
                groupItemsDM.Delete(tran, groupId, docBranchUid);
                
                tran.Commit();
            }
        }
        
        public UserGroupInfo LoadUserGroup(int groupId, int userId, DocumentFilterInfo filter)
        {
            UserGroupInfo result;
            
            using (SqlConnection conn = SQLHelper.CreateConnection())
            {
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();

                result = (new GroupDataMapper()).SelectByGroupId(tran, groupId);

                if (null == result)
                    throw new Exception("User Group not found");

                result.groupDocuments = (new DocDataMapper()).GetByGroupAndUser(tran, groupId, userId, filter);

                tran.Commit();
            }

            result.systemGroup = false;

            return result;
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