package truetract.domain
{
    [Bindable]
	[RemoteClass(alias="TractInc.TrueTract.Entity.DocumentAttachmentInfo")]
	public class DocumentAttachment
	{
	    private static const PDF_COPY_TYPE:String = "Document PDF Copy";

        public var DocumentAttachmentId:int;
        public var DocumentAttachmentTypeId:int;
        public var DocumentId:int;
        public var FileName:String;
        public var FileUrl:String;
        public var Description:String;
        
        private var dict:DictionaryRegistry = DictionaryRegistry.getInstance();
        
        public function get TypeName():String
        {
            return dict.getDocumentAttachmentType(DocumentAttachmentTypeId).@Name;
        }

        public function IsPdfCopy():Boolean
        {
            return TypeName == PDF_COPY_TYPE;
        }
        
        public function clone():DocumentAttachment
        {
            var clone:DocumentAttachment = new DocumentAttachment();
            
            clone.DocumentAttachmentId = DocumentAttachmentId;
            clone.DocumentAttachmentTypeId = DocumentAttachmentTypeId;
            clone.DocumentId = DocumentId;
            clone.FileName = FileName;
            clone.FileUrl = FileUrl;
            clone.Description = Description;

            return clone;
        }
	}
}