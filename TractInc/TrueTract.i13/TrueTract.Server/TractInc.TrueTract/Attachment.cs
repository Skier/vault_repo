using System;
using System.Data.SqlClient;
using System.IO;
using TractInc.TrueTract.Data;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract
{
    internal class Attachment
    {
        private const string ATTACHMENT_SUB_DIR = "DocAttachments";
        
        private DocAttachmentDataMapper m_attachmentDM;

        #region Methods

        internal DocAttachmentInfo Update(SqlTransaction tran, DocAttachmentInfo attachment)
        {
            return AttachmentDM.Update(tran, RenameFile(attachment));
        }

        private DocAttachmentInfo RenameFile(DocAttachmentInfo attachment)
        {
            string source = Path.Combine(Path.Combine(Uploader.StorageDir, ATTACHMENT_SUB_DIR), attachment.FileName);

            if (!File.Exists(source))
                throw new IOException(
                    String.Format("File {0} not found in file storage directory.", attachment.FileName));

            FileInfo fi = new FileInfo(source);

            if (attachment.FileName == (attachment.DocId + fi.Extension))
                return attachment;
            
            attachment.FileName = attachment.DocId + fi.Extension;

            string destination = Path.Combine(Path.Combine(Uploader.StorageDir, ATTACHMENT_SUB_DIR), attachment.FileName);

            if (File.Exists(destination))
                File.Delete(destination);

            File.Move(source, destination);
            
            return attachment;
        }

        #endregion

        #region Properties

        private DocAttachmentDataMapper AttachmentDM
        {
            get
            {
                if (null == m_attachmentDM)
                {
                    m_attachmentDM = new DocAttachmentDataMapper();
                }

                return m_attachmentDM;
            }
        }

        #endregion
    }
}
