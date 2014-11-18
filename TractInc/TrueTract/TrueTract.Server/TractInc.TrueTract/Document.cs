using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;
using FileInfo=TractInc.TrueTract.Entity.FileInfo;

namespace TractInc.TrueTract
{
public class Document
{
    #region Fields

    private DocDataMapper m_docDM;
    private DocumentLeaseDataMapper m_docLeaseDM;
    private UserDataMapper m_userDM;
    private DocumentAttachmentDataMapper m_DocumentAttDM;
    private Tract m_tractBC;
    private DocumentReferenceDataMapper m_docRefDM;

    #endregion

    #region Methods

    public List<TractInfo> GetUserDrawings(int userId, TractsFilterInfo filter)
    {
        return TractBC.GetDrawingsList(userId, filter);
    }
    
    public List<TractInfo> GetDocumentTracts(int documentId)
    {
        return TractBC.GetTractList(documentId);
    }

    public DocumentInfo GetDocument(int docId)
    {
        DocumentInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = DocDM.GetById(tran, docId);

            result.CreatedByName = UserDM.GetUserById(tran, result.CreatedBy).Login;
            
            tran.Commit();
        }

        return result;
    }

    public List<DocumentInfo> GetDocuments(DocumentsFilterInfo filter, bool canBeInactive)
    {
        List<DocumentInfo> docList;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            docList = DocDM.GetAll(tran, filter, canBeInactive);

            PostProcessDocumentList(tran, docList);

            tran.Commit();
        }

        return docList;
    }

    public List<DocumentInfo> GetUserDocuments(int userId, DocumentsFilterInfo filter)
    {
        List<DocumentInfo> docList;

        using (SqlConnection conn = SQLHelper.CreateConnection()) {
            conn.Open();
            
            SqlTransaction tran = conn.BeginTransaction();
            
            docList = DocDM.GetByCreatedBy(tran, userId, filter);

            PostProcessDocumentList(tran, docList);
            
            tran.Commit();
        }

        return docList;
    }

    public List<DocumentInfo> GetGroupDocuments(int groupId, int userId, DocumentsFilterInfo filter)
    {
        List<DocumentInfo> docList;

        using (SqlConnection conn = SQLHelper.CreateConnection()) {
            conn.Open();
            
            SqlTransaction tran = conn.BeginTransaction();
            
            docList = DocDM.GetByGroupAndUser(tran, groupId, userId, filter);

            PostProcessDocumentList(tran, docList);
            
            tran.Commit();
        }
        
        return docList;
    }

    public List<DocumentInfo> GetUserRecentDocuments(int userId, DocumentsFilterInfo filter)
    {
        List<DocumentInfo> docList;

        using (SqlConnection conn = SQLHelper.CreateConnection()) {
            conn.Open();
            
            SqlTransaction tran = conn.BeginTransaction();
            
            docList = DocDM.GetRecent(tran, userId, filter);

            PostProcessDocumentList(tran, docList);

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

            PostProcessDocumentList(tran, docList);
            
            tran.Commit();
        }
        
        return docList;
    }
    
    public DocumentInfo GetDocumentReferences(int documentId)
    {
        DocumentInfo resultDocument;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            resultDocument = DocDM.GetDocumentReferences(tran, documentId);

            List<DocumentInfo> docList = new List<DocumentInfo>();
            
            foreach (DocumentReferenceInfo docRef in resultDocument.References)
            {
                if (docRef.ReferencedDoc != null)
                {
                    docList.Add(docRef.ReferencedDoc);
                }
            }
            
            foreach (DocumentAttachmentInfo attach in resultDocument.Attachments)
            {
                string inputFileName = Path.Combine(attach.FileRef.FilePath, attach.FileRef.FileName);
                internalConvertToFlash(inputFileName, inputFileName + ".swf", false);
            }
            
            PostProcessDocumentList(tran, docList);

            tran.Commit();
        }

        return resultDocument;
    }
    
    public DocumentReferenceInfo AddReference(DocumentReferenceInfo reference)
    {
        DocumentReferenceInfo result;
        
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = DocRefDM.Create(tran, reference);

            result.ReferencedDoc = DocDM.GetById(tran, result.ReferenceId);
            
            tran.Commit();
        }

        return result;
    }

    public DocumentReferenceInfo SaveReference(DocumentReferenceInfo reference, int userId)
    {
        DocumentReferenceInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            DocumentsFilterInfo filter = new DocumentsFilterInfo();
            filter.stateId = reference.State;
            filter.countyId = reference.County;
            filter.docTypeId = reference.DocTypeId;
            filter.docNumber = reference.DocumentNo;
            filter.volume = reference.Volume;
            filter.page = reference.Page;

            List<DocumentInfo> docList = DocDM.GetAll(tran, filter, false);
            
            if (filter.isComplete())
            {
                if (docList.Count == 1)
                {
                    reference.ReferenceId = docList[0].DocID;
                } else if (docList.Count == 0)
                {
                    DocumentInfo doc = new DocumentInfo();
                    
                    doc.State = filter.stateId;
                    doc.County = filter.countyId;
                    doc.DocTypeId = filter.docTypeId;
                    doc.DocumentNo = filter.docNumber;
                    doc.Volume = filter.volume;
                    doc.Page = filter.page;

                    doc.Signed = DateTime.MinValue;
                    doc.Filed = DateTime.MinValue;

                    reference.ReferenceId = SaveDocument(tran, doc, userId).DocID;
                } else
                {
                    throw new Exception("Found more than one document in the system with the same key fields ! Please contact System administrator.");
                }
            }
            else
            {
                reference.ReferenceId = 0;
            }
            
            if (reference.DocumentReferenceId > 0)
            {
                DocRefDM.Update(tran, reference);
                result = reference;
            } else
            {
                result = DocRefDM.Create(tran, reference);
            }

            result.ReferencedDoc = DocDM.GetById(tran, result.ReferenceId);
            
            tran.Commit();
        }

        return result;
    }

    public void DeleteReference(DocumentReferenceInfo reference)
    {
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            DocRefDM.Delete(tran, reference);

            tran.Commit();
        }
    }

    public DocumentReferenceInfo ActualizeDocumentReference(int docRefId)
    {
        DocumentReferenceInfo result;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            result = DocRefDM.GetById(tran, docRefId);

            DocumentInfo actualDoc = DocDM.GetActualByDocId(tran, result.ReferenceId);

            if (actualDoc == null)
                throw new Exception("Can't find actual version of this document. Please contact System administrator.");

            result.ReferenceId = actualDoc.DocID;

            DocRefDM.Update(tran, result);

            result.ReferencedDoc = actualDoc;
            
            tran.Commit();
        }

        return result;
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

            newDocRevision = SaveDocument(tran, document, userId);

            tran.Commit();
        }

        return newDocRevision;
    }
        
        public DocumentInfo SaveDocument(SqlTransaction tran, DocumentInfo document, int userId)
        {
            if (document.DocID > 0)
            {
                DocumentInfo currDoc = DocDM.GetById(tran, document.DocID);
                
                if (!currDoc.IsActive)
                    throw new Exception("You can update only last version of Document. Please get last version.");

// checking for document with the same key fields.
                
                DocumentsFilterInfo filter = new DocumentsFilterInfo();
                
                filter.stateId = document.State;
                filter.countyId = document.County;
                filter.docTypeId = document.DocTypeId;
                filter.docNumber = document.DocumentNo;
                filter.volume = document.Volume;
                filter.page = document.Page;
                
                List<DocumentInfo> docList = DocDM.GetAll(tran, filter, false);
                
                foreach (DocumentInfo doc in docList)
                {
                    if (doc.DocBranchUid != document.DocBranchUid)
                    {
                        throw new Exception("You can not save this document. Document with the same key fields alredy exists in the system.");
                    }
                }
// ----------------------------------------------
                
                DocumentInfo newDoc = DocDM.GetById(tran, DocDM.CreateCopy(tran, document, userId));

                newDoc.State = document.State;
                newDoc.County = document.County;
                newDoc.DocTypeId = document.DocTypeId;
                newDoc.DocumentNo = document.DocumentNo;
                newDoc.Volume = document.Volume;
                newDoc.Page = document.Page;

                newDoc.Filed = document.Filed;
                newDoc.Signed = document.Signed;
                
                newDoc.Tracts = document.Tracts;
                newDoc.Attachments = document.Attachments;

                if (newDoc.Buyer != null)
                {
                    newDoc.Buyer.AsNamed = document.Buyer.AsNamed;
                } else
                {
                    newDoc.Buyer = document.Buyer;
                }

                if (newDoc.Seller != null)
                {
                    newDoc.Seller.AsNamed = document.Seller.AsNamed;
                } else
                {
                    newDoc.Seller = document.Seller;
                }

                newDoc.Lease = document.Lease;

                document = DocDM.Update(tran, newDoc, userId);
            }
            else
            {
                document.CreatedBy = userId;
                document.IsActive = true;
                document = DocDM.Create(tran, document);
            }

            if ( null != document.Lease ) {
                document.Lease.DocId = document.DocID;
                DocLeaseDM.Create(tran, document.Lease);
            }

            PostProcessDocumentList(tran, new List<DocumentInfo>(new DocumentInfo[] { document }));

            if (document.References != null)
            {
                List<DocumentInfo> docList = new List<DocumentInfo>();

                foreach (DocumentReferenceInfo docRef in document.References)
                {
                    if (docRef.ReferenceId > 0)
                    {
                        docRef.ReferencedDoc = DocDM.GetById(tran, docRef.ReferenceId);
                        docList.Add(docRef.ReferencedDoc);
                    }
                }

                PostProcessDocumentList(tran, docList);
            }

            return document;
        }

        public DocumentInfo OpenDocument(SqlTransaction tran, int docId) {
            return DocDM.GetById(tran, docId);
        }
        

    public DocumentAttachmentInfo AddAttachment(DocumentAttachmentInfo attachment, string uploadId)
    {
        string uploadFilePath = Weborb.Util.Paths.GetUploadPath() + uploadId;
        string uploadedFileFullName = uploadFilePath + "\\" + attachment.FileRef.FileName;

        if (!File.Exists(uploadedFileFullName))
        {
            throw new FileNotFoundException("File not found");
        }

        attachment.FileRef.FilePath =
            Path.Combine(Path.Combine(TrueTractConfig.AttachmentsBaseDir, "documents"),
                         attachment.DocumentId.ToString());

        attachment.FileRef.FileUrl = string.Format(
            "{0}/documents/{1}/{2}", TrueTractConfig.AttachmentsBaseUrl, attachment.DocumentId, attachment.FileRef.FileName);

        string newFileFullName = Path.Combine(attachment.FileRef.FilePath, attachment.FileRef.FileName);

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
        
        //create Flash copy of pdf file
        if (newFileFullName.ToUpper().EndsWith(".PDF"))
            internalConvertToFlash(newFileFullName, newFileFullName + ".swf");

        //db changes
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            (new FileDataMapper()).Create(tran, attachment.FileRef);

            attachment.FileId = attachment.FileRef.FileId;
            attachment.FileRef.CreatedByName = (new UserDataMapper()).GetUserById(
                tran, attachment.FileRef.CreatedBy).Login;

            DocumentAttDM.Create(tran, attachment);

            tran.Commit();
        }

        return attachment;
    }

    public DocumentAttachmentInfo UpdateAttachment(DocumentAttachmentInfo attachment, string uploadId)
    {
        string uploadFilePath = Weborb.Util.Paths.GetUploadPath() + uploadId;
        string uploadedFileFullName = uploadFilePath + "\\" + attachment.FileRef.FileName;

        if (File.Exists(uploadedFileFullName))
        {
            FileInfo storedFile = (new FileDataMapper()).GetById(null, attachment.FileId);
            File.Delete(storedFile.FileFullName);

            attachment.FileRef.FilePath =
                Path.Combine(Path.Combine(TrueTractConfig.AttachmentsBaseDir, "documents"),
                             attachment.DocumentId.ToString());

            attachment.FileRef.FileUrl = string.Format(
                "{0}/documents/{1}/{2}", TrueTractConfig.AttachmentsBaseUrl, attachment.DocumentId, attachment.FileRef.FileName);

            string newFileFullName = Path.Combine(attachment.FileRef.FilePath, attachment.FileRef.FileName);

            //remove file with the same name from document folder
            if (File.Exists(newFileFullName))
                File.Delete(newFileFullName);

            //move file from uploading folder to document folder
            File.Move(uploadedFileFullName, newFileFullName);

            //delete uploading folder
            Directory.Delete(uploadFilePath, true);

            //create Flash copy of pdf file
            if (newFileFullName.ToUpper().EndsWith(".PDF"))
                internalConvertToFlash(newFileFullName, newFileFullName + ".swf");
        }

        //db changes
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            DocumentAttDM.Update(tran, attachment);

            (new FileDataMapper()).Update(tran, attachment.FileRef);

            tran.Commit();
        }

        return attachment;
    }

    public void DeleteAttachment(DocumentAttachmentInfo attachment)
    {
        //db changes
        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            DocumentAttDM.Delete(tran, attachment);

            (new FileDataMapper()).Delete(tran, attachment.FileRef);

            tran.Commit();
        }
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
                    SaveDocument(tran, tract.ParentDocument, userId);
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

    public string getFlashCopyOfPdf(int attachmentId)
    {
        DocumentAttachmentInfo attachment;

        using (SqlConnection conn = SQLHelper.CreateConnection())
        {
            conn.Open();

            SqlTransaction tran = conn.BeginTransaction();

            attachment = DocumentAttDM.GetById(tran, attachmentId);
            if (attachment == null)
                throw new Exception("Can't find attachment in the database. Please contact System administrator.");

            attachment.FileRef = (new FileDataMapper()).GetById(tran, attachment.FileId);
                
            tran.Commit();
        }

        string inputFileName = Path.Combine(attachment.FileRef.FilePath, attachment.FileRef.FileName);
        if (!File.Exists(inputFileName))
            throw new Exception("Can't find attachment file '" + attachment.FileRef.FileName + "'. Please contact System administrator.");

        string outputFileName = inputFileName + ".swf";
        if (!File.Exists(outputFileName))
            internalConvertToFlash(inputFileName, outputFileName);

        return attachment.FileRef.FileUrl + ".swf";
    }

    private void internalConvertToFlash(string inputFileName, string outputFileName, bool overwrite)
    {
        if (File.Exists(outputFileName) && !overwrite)
            return;
/*        
        P2F.Server2 p2fServer = new P2F.Server2();
        p2fServer.ConvertFile(inputFileName, outputFileName, null, null, null);
 */ 
    }

    private void internalConvertToFlash(string inputFileName, string outputFileName)
    {
        internalConvertToFlash(inputFileName, outputFileName, true);
    }

    private void PostProcessDocumentList(SqlTransaction tran, List<DocumentInfo> list)
    {
        Hashtable userNames = new Hashtable();

        foreach (DocumentInfo info in list)
        {
            if (userNames[info.CreatedBy] == null)
            {
                userNames[info.CreatedBy] = UserDM.GetUserById(tran, info.CreatedBy).Login;
            }
            try {
            info.Lease = DocLeaseDM.GetByDocument(tran, info.DocID);
            } catch (Exception ex) {
                info.Lease = new DocumentLeaseInfo();
                info.Lease.LCN = ex.StackTrace;
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

    private DocumentLeaseDataMapper DocLeaseDM {
        get {
            if (null == m_docLeaseDM) {
                m_docLeaseDM = new DocumentLeaseDataMapper();
            }
            
            return m_docLeaseDM;
        }
    }

    private DocumentAttachmentDataMapper DocumentAttDM {
        get {
            if (null == m_DocumentAttDM) {
                m_DocumentAttDM = new DocumentAttachmentDataMapper();
            }
            
            return m_DocumentAttDM;
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

    private DocumentReferenceDataMapper DocRefDM
    {
        get
        {
            if (null == m_docRefDM)
            {
                m_docRefDM = new DocumentReferenceDataMapper();
            }
            return m_docRefDM;
        }
    }

    #endregion
}
}
