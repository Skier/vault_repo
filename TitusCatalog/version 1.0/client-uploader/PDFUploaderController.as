package
{

    import flash.events.Event;
    import flash.events.IOErrorEvent;
    import flash.events.ProgressEvent;
    import flash.net.FileFilter;
    import flash.net.FileReference;
    import flash.net.URLRequest;
    import flash.net.URLVariables;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.RemoteObject;
    import mx.utils.UIDUtil;
    
    [Bindable]
    public class PDFUploaderController
    {
        
        public static const PDF_STORAGE_DIRECTORY:String = "PDF";
        
        private var fileRef:FileReference;
        
        private var view:PDFUploaderView;

        public var model:PDFUploaderModel;
        
        public var uploaderUrl:String = null;
        public var uploaderUrlLoaded:Boolean = false;

        public var storageBaseUrl:String = null;
        public var storageBaseUrlLoaded:Boolean = false;
        
        private var service:RemoteObject;
        
        public function PDFUploaderController(view:PDFUploaderView)
        {
            this.view = view;

           	service = new RemoteObject("GenericDestination");
           	service.source = "Titus.ECatalog.Service.UploaderService";
            	
            service.GetStorageUrl.addEventListener(ResultEvent.RESULT, onGetStorageUrl);
            service.GetUploaderUrl.addEventListener(ResultEvent.RESULT, onGetUploaderUrl);
            service.ProcessDocuments.addEventListener(ResultEvent.RESULT, onDocumentsProcessed);
            service.addEventListener(FaultEvent.FAULT, onFault);
            
            service.GetStorageUrl();
            service.GetUploaderUrl();
        }
        
        private function onFault(event:FaultEvent):void 
        {
            Alert.show(event.fault.message);
        }
            
        private function onGetStorageUrl(event:ResultEvent):void 
        {
            storageBaseUrl = event.result as String;
            storageBaseUrlLoaded = true;
            
            if (uploaderUrlLoaded) {
				init();
            }
        }

        private function onGetUploaderUrl(event:ResultEvent):void 
        {
            uploaderUrl = event.result as String;
            uploaderUrlLoaded = true;
            
            if (storageBaseUrlLoaded) {
				init();
            }
        }
            
        public function init():void 
        {
        	view.enabled = true;
        	
            model = new PDFUploaderModel();
            
            fileRef = new FileReference();
            
            fileRef.addEventListener(Event.SELECT, onSelectLoad);
            fileRef.addEventListener(Event.COMPLETE, onCompleteLoad);
            fileRef.addEventListener(IOErrorEvent.IO_ERROR, onErrorUpload);
            fileRef.addEventListener(ProgressEvent.PROGRESS, onProgressLoad);
        }
        
        public function onClickUpload():void
        {
            fileRef.browse([new FileFilter("PDF Documents", "*.pdf")]);
        }

        public function onClickCancelUpload():void 
        {
            if (model.uploadingInProgress) {
                model.uploadingInProgress = false;
                fileRef.cancel();
                model.uploadingInProgress = false;
            }
        }
            
        private function onDocumentsProcessed(result:ResultEvent):void {
        	view.enabled = true;
        	model.documentUploaded = false;
            model.uploadingInProgress = false;
        	model.links = new ArrayCollection();
        }
        
        private function onSelectLoad(event:Event):void 
        {
            var requestURL:URLRequest = new URLRequest;
            requestURL.url = uploaderUrl;
            requestURL.method = "POST";

            var variables:URLVariables = new URLVariables();
            var uid:String = UIDUtil.createUID().toString() + "__";

            variables.UploadDir = PDF_STORAGE_DIRECTORY;
            variables.UniqueKey = uid;
            
            requestURL.data = variables;

            fileRef = FileReference(event.target);

            model.uploadingInProgress = true;
        	view.enabled = false;
            fileRef.upload(requestURL);
        }

        private function onProgressLoad(event:ProgressEvent):void 
        {
            view.pbUpload.setProgress(event.bytesLoaded, event.bytesTotal);
        }

        private function onCompleteLoad(event:Event):void 
        {
            service.ProcessDocument(model.links.toArray());
        }
            
        private function onErrorUpload(event:IOErrorEvent):void 
        {
            model.uploadingInProgress = false;
            Alert.show(event.text, "Uploading Error");
        }
        
    }

}
