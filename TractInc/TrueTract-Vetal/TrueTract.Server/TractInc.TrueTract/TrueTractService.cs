using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

    public List<ClientInfo> GetClientListByUser(int userId)
    {
        List<ClientInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ClientDataMapper()).GetByUser(tran, userId);

            tran.Commit();
        }

        return result;
    }

    public DocumentAttachmentInfo AddDocumentAttachment(
        DocumentAttachmentInfo attachment, string uploadId)
    {
        string docDir = string.Format("{0}\\{1}\\", TrueTractConfig.AttachmentsBaseDir, attachment.DocumentId);
        string docUrl = string.Format("{0}/{1}/", TrueTractConfig.AttachmentsBaseUrl, attachment.DocumentId);

        string uploadPath = Weborb.Util.Paths.GetUploadPath() + uploadId;
        string uploadedFilePath = string.Format("{0}\\{1}", uploadPath, attachment.FileName);
        string newFilePath = docDir + attachment.FileName;         

        attachment.FileUrl = docUrl + attachment.FileName;

        if (System.IO.File.Exists(uploadedFilePath))
        {
            //First thing we do is the new record creation in the DB. 
            DocumentBC.AddAttachment(attachment);

            //check document directory existing
            if (!Directory.Exists(docDir))
                Directory.CreateDirectory(docDir);

            //remove file with the same name from document folder
            if (System.IO.File.Exists(newFilePath))
                System.IO.File.Delete(newFilePath);

            //move file from uploading folder to document folder
            System.IO.File.Move(uploadedFilePath, newFilePath);

            //delete uploading folder
            Directory.Delete(uploadPath, true);
        }
        else
        {
            throw new FileNotFoundException("File not found");
        }

        return attachment;
    }

    public DocumentAttachmentInfo UpdateDocumentAttachment(DocumentAttachmentInfo attachment)
    {
        DocumentBC.UpdateAttachment(attachment);

        return attachment;
    }

    public DocumentAttachmentInfo DeleteDocumentAttachment(
        DocumentAttachmentInfo attachment, int userId)
    {
        DocumentBC.DeleteAttachment(attachment, userId);

        //Delete physical file. commented for now. need review. 
        //We may have two or more links on this file.

//            string docDir = string.Format("{0}\\{1}\\", TrueTractConfig.AttachmentsBaseDir, attachment.DocumentId);
//            string filePath = docDir + attachment.FileName;
//
//            if (File.Exists(filePath))
//            {
//                File.Delete(filePath);
//            }

        return attachment;
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

    public void DeleteTract(int tractId, int userId)
    {
        TractBC.DeleteTract(tractId, userId);
    }

    public TractInfo LoadTract(int tractId)
    {
        return TractBC.LoadTract(tractId);
    }

    public List<DocumentInfo> GetDocuments(DocumentsFilterInfo filter)
    {
        return DocumentBC.GetDocumentList(filter);
    }

    public List<DocumentInfo> GetUserDocuments(int userId, DocumentsFilterInfo filter)
    {
        return DocumentBC.GetDocumentList(userId, filter);
    }

    public List<TractInfo> GetUserDrawings(int userId, TractsFilterInfo filter)
    {
        return TractBC.GetDrawingsList(userId, filter);
    }
    
    public List<DocumentInfo> GetUserRecentDocuments(int userId, DocumentsFilterInfo filter)
    {
        return DocumentBC.GetRecent(userId, filter);
    }
    
    public List<DocumentInfo> GetGroupDocuments(int groupId, int userId, DocumentsFilterInfo filter)
    {
        return DocumentBC.GetByGroupAndUser(groupId, userId, filter);
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
            (new GroupUserDataMapper()).Create(tran, userId, groupId);

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

    public void AddDocumentToGroup(int groupId, string docBranchUid)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            GroupItemDataMapper groupItemDM = new GroupItemDataMapper();

            if (!groupItemDM.IsGroupContains(tran, groupId, docBranchUid))
            {
                groupItemDM.Create(tran, groupId, docBranchUid);
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

            GroupItemDataMapper groupItemDM = new GroupItemDataMapper();
            groupItemDM.Delete(tran, groupId, docBranchUid);
            
            tran.Commit();
        }
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