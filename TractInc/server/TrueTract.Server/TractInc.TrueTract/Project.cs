using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;
using FileInfo=TractInc.TrueTract.Entity.FileInfo;

namespace TractInc.TrueTract
{
public class Project
{
    private FileDataMapper fileDM = new FileDataMapper();
    private ProjectAttachmentDataMapper projectAttachmentDM = new ProjectAttachmentDataMapper();
    private ProjectTabDocumentDataMapper tabDocDM = new ProjectTabDocumentDataMapper();
    private ProjectTabDataMapper tabDM = new ProjectTabDataMapper();
    private ProjectTabContactDataMapper tabContactDM = new ProjectTabContactDataMapper();
    private DocDataMapper docDM = new DocDataMapper();
    private ProjectDataMapper projDM = new ProjectDataMapper();

    public void InitProjectsSearch()
    {
        List<ProjectInfo> projects = new List<ProjectInfo>();
        
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            foreach (ProjectInfo proj in (new ProjectDataMapper()).GetAll(tran))
            {
                ProjectInfo project = LoadProject(tran, proj.ProjectId);
                
                
            }

            tran.Commit();
        }

    }

    public List<ProjectInfo> GetProjectsByClientAndUser(int clientId, int userId)
    {
        List<ProjectInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ProjectDataMapper()).GetByClientAndUser(tran, clientId, userId);

            tran.Commit();
        }

        return result;
    }

    public List<ProjectInfo> GetAllProjectsByClient(int clientId)
    {
        List<ProjectInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ProjectDataMapper()).GetAllByClient(tran, clientId);

            tran.Commit();
        }

        return result;
    }

    public List<ProjectInfo> GetOpenProjectsByClient(int clientId)
    {
        List<ProjectInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ProjectDataMapper()).GetOpenByClient(tran, clientId);

            tran.Commit();
        }

        return result;
    }

    public List<ProjectInfo> GetProjectsClosedLastWeekByClient(int clientId)
    {
        List<ProjectInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ProjectDataMapper()).GetClosedLastWeekByClient(tran, clientId);

            tran.Commit();
        }

        return result;
    }

    public List<ProjectInfo> GetAllClosedProjectsByClient(int clientId)
    {
        List<ProjectInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ProjectDataMapper()).GetAllClosedByClient(tran, clientId);

            tran.Commit();
        }

        return result;
    }

    public List<ProjectInfo> GetProjectsByDocumentAndClient(int documentId, int clientId)
    {
        List<ProjectInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = (new ProjectDataMapper()).GetByDocumentAndClient(tran, documentId, clientId);

            tran.Commit();
        }

        return result;
    }

    public List<ProjectAttachmentInfo> GetProjectAttachments(int projectId)
    {
        List<ProjectAttachmentInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = projectAttachmentDM.GetByProject(tran, projectId);

            foreach (ProjectAttachmentInfo info in result)
            {
                info.FileRef = fileDM.GetById(tran, info.FileId);
            }

            tran.Commit();
        }

        return result;
    }

    public List<ProjectTabInfo> GetProjectTabs(int projectId)
    {
        List<ProjectTabInfo> result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {

            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = tabDM.GetByProjectId(tran, projectId);

            foreach (ProjectTabInfo tabInfo in result)
            {
                tabInfo.Documents = tabDocDM.GetByProjectTab(tran, tabInfo.ProjectTabId);
                tabInfo.Contacts = tabContactDM.GetByProjectTabId(tran, tabInfo.ProjectTabId);
            }
            tran.Commit();
        }

        return result;
    }

    public ProjectTabInfo AddTab(ProjectTabInfo projectTab)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            tabDM.Create(tran, projectTab);

            tran.Commit();
        }

        return projectTab;
    }

    public ProjectTabInfo UpdateTab(ProjectTabInfo projectTab)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            tabDM.Update(tran, projectTab);

            tran.Commit();
        }

        return projectTab;
    }

    public void DeleteTab(ProjectTabInfo projectTab)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            tabDM.Delete(tran, projectTab);

            tran.Commit();
        }
    }

    public void SaveOrderedTabs(ProjectTabInfo[] projectTabs)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            foreach (ProjectTabInfo tab in projectTabs)
            {
                tabDM.Update(tran, tab);
            }

            tran.Commit();
        }
    }

    public List<ProjectTabDocumentInfo> AddTabDocuments(ProjectTabInfo projectTab, int[] documentsIds)
    {
        List<ProjectTabDocumentInfo> result = new List<ProjectTabDocumentInfo>();
        List<ProjectTabDocumentInfo> currentTabDocs;
        ProjectTabDocumentInfo tabDocument;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            currentTabDocs = tabDocDM.GetByProject(tran, projectTab.ProjectId);

            foreach (int documentId in documentsIds)
            {
                bool existsInTab = false;
                
                DocumentInfo document = docDM.GetById(tran, documentId);
                
                for (int i = 0; i < currentTabDocs.Count; i++ )
                {
                    tabDocument = currentTabDocs[i];

                    if (tabDocument.DocumentRef != null && tabDocument.DocumentRef.DocBranchUid == document.DocBranchUid)
                    {
                        tabDocument.DocumentId = documentId;
                        tabDocDM.Update(tran, tabDocument);
                        if (tabDocument.ProjectTabId == projectTab.ProjectTabId)
                        {
                            result.Add(tabDocument);
                            existsInTab = true;
                        }
                    }
                }

                if (!existsInTab)
                {
                    tabDocument = new ProjectTabDocumentInfo();
                    tabDocument.ProjectTabId = projectTab.ProjectTabId;
                    tabDocument.DocumentId = documentId;
                    tabDocument.Description = "";
                    tabDocument.Remarks = "";
                    tabDocument.IsActive = false;

                    tabDocument = tabDocDM.Create(tran, tabDocument);

                    result.Add(tabDocument);
                }
            }

            tran.Commit();
        }

        return result;
    }

    public DocumentInfo ActualizeDocument(int projectId, int docId)
    {
        DocumentInfo result;
        
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            DocumentInfo actualDoc = docDM.GetActualByDocId(tran, docId);

            if (actualDoc == null)
                throw new Exception("Can't find actual version of this document. Please contact System administrator.");

            result = actualDoc;
            
            List<ProjectTabDocumentInfo> projectTabDocs = tabDocDM.GetByProject(tran, projectId);

            foreach (ProjectTabDocumentInfo tabDoc in projectTabDocs)
            {
                if (tabDoc.DocumentRef != null && tabDoc.DocumentRef.DocBranchUid == actualDoc.DocBranchUid)
                {
                    tabDoc.DocumentId = actualDoc.DocID;
                    tabDoc.DocumentRef = actualDoc;
                    tabDoc.updateTracts();
                    
                    tabDocDM.Update(tran, tabDoc);
                }
            }

            tran.Commit();
        }

        return result;
    }

    public ProjectTabDocumentInfo UpdateTabDocument(ProjectTabDocumentInfo tabDocument)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            tabDocument = tabDocDM.Update(tran, tabDocument);

            tran.Commit();
        }

        return tabDocument;
    }

    public void DeleteTabDocuments(ProjectTabDocumentInfo[] tabDocuments)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            foreach (ProjectTabDocumentInfo tabDocument in tabDocuments)
            {
                tabDocDM.Delete(tran, tabDocument);
            }

            tran.Commit();
        }
    }

    public ProjectTabContactInfo AddTabContact(ProjectTabContactInfo tabContact)
    {
        ProjectTabContactInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = tabContactDM.Create(tran, tabContact);

            tran.Commit();
        }

        return result;
    }

    public ProjectTabContactInfo UpdateTabContact(ProjectTabContactInfo tabContact)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            tabContactDM.Update(tran, tabContact);

            tran.Commit();
        }

        return tabContact;
    }

    public void DeleteTabContacts(ProjectTabContactInfo[] tabContacts)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            foreach (ProjectTabContactInfo tabContact in tabContacts)
            {
                tabContactDM.Delete(tran, tabContact);
            }

            tran.Commit();
        }
    }
    
    public ProjectAttachmentInfo AddAttachment(ProjectAttachmentInfo attachment, string uploadId)
    {
        string uploadFilePath = Weborb.Util.Paths.GetUploadPath() + uploadId;
        string uploadedFileFullName = uploadFilePath + "\\" + attachment.FileRef.FileName;

        if (!File.Exists(uploadedFileFullName))
        {
            throw new FileNotFoundException("File not found");
        }

        attachment.FileRef.FilePath = string.Format(
            "{0}\\projects\\{1}\\", TrueTractConfig.AttachmentsBaseDir, attachment.ProjectId);

        attachment.FileRef.FileUrl = string.Format(
            "{0}/projects/{1}/", TrueTractConfig.AttachmentsBaseUrl, attachment.ProjectId);

        string newFileFullName = attachment.FileRef.FilePath + attachment.FileRef.FileName;

        //check document directory existing
        if (!Directory.Exists(attachment.FileRef.FilePath))
            Directory.CreateDirectory(attachment.FileRef.FilePath);

        //remove file with the same name from document folder
        if (File.Exists(newFileFullName))
            File.Delete(newFileFullName);

        //move file from uploading folder to document folder
        File.Move(uploadedFileFullName, newFileFullName);

        //delete uploading folder
        Directory.Delete(uploadFilePath, true);

        //database changes
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new FileDataMapper()).Create(tran, attachment.FileRef);

            attachment.FileId = attachment.FileRef.FileId;
            attachment.FileRef.CreatedByName = (new UserDataMapper()).GetUserById(
                tran, attachment.FileRef.CreatedBy).Login;

            (new ProjectAttachmentDataMapper()).Create(tran, attachment);

            tran.Commit();
        }

        return attachment;
    }

    public ProjectAttachmentInfo UpdateAttachment(ProjectAttachmentInfo attachment, string uploadId)
    {
        string uploadFilePath = Weborb.Util.Paths.GetUploadPath() + uploadId;
        string uploadedFileFullName = uploadFilePath + "\\" + attachment.FileRef.FileName;

        if (File.Exists(uploadedFileFullName))
        {
            FileInfo storedFile = (new FileDataMapper()).GetById(null, attachment.FileId);
            File.Delete(storedFile.FileFullName);
         
            attachment.FileRef.FilePath = string.Format(
                "{0}\\projects\\{1}\\", TrueTractConfig.AttachmentsBaseDir, attachment.ProjectId);

            attachment.FileRef.FileUrl = string.Format(
                "{0}/projects/{1}/", TrueTractConfig.AttachmentsBaseUrl, attachment.ProjectId);
            
            string newFileFullName = attachment.FileRef.FileFullName;
            //remove file with the same name from document folder
            if (File.Exists(newFileFullName))
                File.Delete(newFileFullName);

            //move file from uploading folder to document folder
            File.Move(uploadedFileFullName, newFileFullName);

            //delete uploading folder
            Directory.Delete(uploadFilePath, true);
        }

        //database changes
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new ProjectAttachmentDataMapper()).Update(tran, attachment);

            (new FileDataMapper()).Update(tran, attachment.FileRef);

            tran.Commit();
        }

        return attachment;
    }

    public void DeleteAttachment(ProjectAttachmentInfo attachment)
    {
        if (File.Exists(attachment.FileRef.FileFullName))
        {
            File.Delete(attachment.FileRef.FileFullName);
        }
        
        //database changes
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new ProjectAttachmentDataMapper()).Delete(tran, attachment);

            (new FileDataMapper()).Delete(tran, attachment.FileRef);

            tran.Commit();
        }
    }
    
    public string GetProjectTabExcelFileUrl(int projectTabId)
    {
        ProjectTabInfo tab = LoadTab(projectTabId);
        string content = ExcelConverter.ConvertTabToExcelXml(tab);
        
        string fileName = tab.ProjectRef.ShortName + "_" + tab.Name + ".xls";
        string fullName = Path.Combine(TrueTractConfig.ExcelExportDir, fileName);

        if (!Directory.Exists(TrueTractConfig.ExcelExportDir))
            Directory.CreateDirectory(TrueTractConfig.ExcelExportDir);
        
        File.Delete(fullName);
        
        using (StreamWriter fileStream = new StreamWriter(fullName))
        {
            fileStream.Write(content);
            fileStream.Close();
        }

        return (TrueTractConfig.ExcelExportUrl.EndsWith("/") ? TrueTractConfig.ExcelExportUrl : TrueTractConfig.ExcelExportUrl + "/") + fileName;
    }

    public ProjectInfo ChangeProjectStatus(int projectId, int statusId, string changedBy)
    {
        ProjectInfo project;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();


            project = projDM.GetById(tran, projectId);

            project.ProjectStatusId = statusId;
            project.CreatedBy = changedBy;

            projDM.Update(tran, project);
            
            tran.Commit();
        }

        return project;
    }
    
    public ProjectInfo LoadFullProject(int projectId)
    {
        ProjectInfo project;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            project = LoadProject(tran, projectId);

            tran.Commit();
        }

        return project;
    }

    private ProjectInfo LoadProject(SqlTransaction tran, int projectId)
    {
        ProjectInfo project;

        project = projDM.GetById(tran, projectId);
        project.Tabs = tabDM.GetByProjectId(tran, projectId).ToArray();
        project.Attachments = projectAttachmentDM.GetByProject(tran, projectId).ToArray();

        foreach (ProjectAttachmentInfo info in project.Attachments)
        {
            info.FileRef = fileDM.GetById(tran, info.FileId);
        }

        foreach (ProjectTabContactInfo contact in tabContactDM.GetByProjectId(tran, projectId))
        {
            foreach (ProjectTabInfo tab in project.Tabs)
            {
                if (tab.Contacts == null)
                    tab.Contacts = new List<ProjectTabContactInfo>();

                if (contact.ProjectTabId == tab.ProjectTabId)
                {
                    tab.Contacts.Add(contact);
                    break;
                }
            }
        }

        foreach (ProjectTabDocumentInfo tabDoc in tabDocDM.GetByProject(tran, projectId))
        {
            tabDoc.DocumentRef = docDM.GetDocumentReferences(tran, tabDoc.DocumentId);

            foreach (ProjectTabInfo tab in project.Tabs)
            {
                if (tab.Documents == null)
                    tab.Documents = new List<ProjectTabDocumentInfo>();

                if (tabDoc.ProjectTabId == tab.ProjectTabId)
                {
                    tab.Documents.Add(tabDoc);
                    break;
                }
            }
        }

        return project;
    }

    private ProjectTabInfo LoadTab(int projectTabId)
    {
        ProjectTabInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = tabDM.GetById(tran, projectTabId);
            result.Documents = tabDocDM.GetByProjectTab(tran, projectTabId);

            result.ProjectRef = projDM.GetById(tran, result.ProjectId);
            
            foreach(ProjectTabDocumentInfo tabDoc in result.Documents)
            {
                tabDoc.DocumentRef = docDM.GetById(tran, tabDoc.DocumentId);
            }
            
            tran.Commit();
        }

        return result;
    }
    
}
}
