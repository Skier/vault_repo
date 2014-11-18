using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
public class Project
{
    private FileDataMapper fileDM = new FileDataMapper();
    private ProjectAttachmentDataMapper projectAttachmentDM = new ProjectAttachmentDataMapper();

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

            result = (new ProjectTabDataMapper()).GetByProjectId(tran, projectId);

            tran.Commit();
        }

        return result;
    }

    public ProjectAttachmentInfo AddAttachment(ProjectAttachmentInfo attachment, int userId)
    {
        string currentFilePath = attachment.FileRef.FilePath;
        string currentFileFullName = attachment.FileRef.FilePath + "\\" + attachment.FileRef.FileName;

        if (System.IO.File.Exists(currentFilePath))
        {
            attachment.FileRef.FilePath = string.Format(
                "{0}\\projects\\{1}\\", TrueTractConfig.AttachmentsBaseDir, attachment.ProjectId);
            attachment.FileRef.FileUrl = string.Format(
                "{0}/projects/{1}/", TrueTractConfig.AttachmentsBaseUrl, attachment.ProjectId);

            string newFileFullName = attachment.FileRef.FilePath + attachment.FileRef.FileName;

            CreateAttachment(attachment);

            //check document directory existing
            if (!Directory.Exists(attachment.FileRef.FilePath))
                Directory.CreateDirectory(attachment.FileRef.FilePath);

            //remove file with the same name from document folder
            if (System.IO.File.Exists(newFileFullName))
                System.IO.File.Delete(newFileFullName);

            //move file from uploading folder to document folder
            System.IO.File.Move(currentFileFullName, newFileFullName);

            //delete uploading folder
            Directory.Delete(currentFilePath, true);
        }
        else
        {
            throw new FileNotFoundException("File not found");
        }

        return attachment;
    }
    
    private ProjectAttachmentInfo CreateAttachment(ProjectAttachmentInfo attachment)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new ProjectAttachmentDataMapper()).Create(tran, attachment);

            (new FileDataMapper()).Update(tran, attachment.FileRef);

            tran.Commit();
        }

        return attachment;
    }
}
}
