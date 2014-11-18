namespace TractInc.TrueTract.Entity
{
    public class DocAttachmentInfo
    {
        #region Fields

        public int DocumentAttachmentId;
        public int DocId;
        public string FileName;
        public string OriginalFileName;
        #endregion

        #region Constructors

        public DocAttachmentInfo() 
        {
        }

        public DocAttachmentInfo(
                        int documentAttachmentId,
                        int docId,
                        string fileName,
                        string originalFileName
            ) 
        {
            DocumentAttachmentId = documentAttachmentId;
            DocId = docId;
            FileName = fileName;
            OriginalFileName = originalFileName;
        }

        #endregion
        
    }
}
