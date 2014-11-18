package
{
	import mx.collections.ArrayCollection;
	

    [Bindable]
    public class PDFUploaderModel
    {

        public var uploadingInProgress:Boolean = false;
        
        public var documentUploaded:Boolean = false;
        
        public var links:ArrayCollection = new ArrayCollection();
        
        public var documents:ArrayCollection = new ArrayCollection();
        
        public function PDFUploaderModel() {
        }

    }
    
}
