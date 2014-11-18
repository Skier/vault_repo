package UI.Compose.Attachment.AttachmentItem
{
    import Domain.File;
    
    import flash.events.Event;
    import flash.events.IOErrorEvent;
    import flash.events.ProgressEvent;
    import flash.net.FileReference;
    import flash.net.URLRequest;
    
    import mx.controls.Alert;
    
    public class AttachmentItemController
    {
        [Bindable]
        public var Model:File;
		public var IsUploadInProgress:Boolean;

        private var View:AttachmentItemView;
        
        private var m_fileReference:FileReference;
        
        public function AttachmentItemController(view:AttachmentItemView):void 
        {
            this.View = view;   
        }
        
		public function uploadFile(fr:FileReference, requestURL:URLRequest):void{
			m_fileReference = fr;
			
			Model = new File();
			Model.Name = fr.name;
			Model.Size = fr.size;
			
			InitializeListeners();
			
 		    try 
 		    {
		        m_fileReference.upload(requestURL);
		        IsUploadInProgress = true;
		        View.pb.label="Uploading..";
		    } 
		    catch (error:Error) 
		    {
			    Alert.show("Unable to upload file (" + Model.Name + ")" );
				OnRemove();
		    }                	
		}

        private function InitializeListeners():void 
        {
			m_fileReference.addEventListener(Event.COMPLETE, OnUploadComplete);
			m_fileReference.addEventListener(IOErrorEvent.IO_ERROR, OnUploadError);
		    m_fileReference.addEventListener(ProgressEvent.PROGRESS, OnUploadProgress);
        }
        
        public function OnRemove():void {
            if (IsUploadInProgress)
                m_fileReference.cancel();
                
    		View.dispatchEvent(new Event("removeAttachment"));
        }

		private function OnUploadComplete(event:Event):void {
			IsUploadInProgress = false;
			View.pb.label = "Ok."
		}			

		private function OnUploadProgress(event:ProgressEvent):void {
			View.pb.setProgress(event.bytesLoaded, event.bytesTotal);
		}	 		

		private function OnUploadError(event:IOErrorEvent):void {
		    Alert.show("Unable to upload file (" + Model.Name + ")");
			OnRemove();
		}		        
		
    }
}