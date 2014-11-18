package UI.MessageBox.MessageDetail
{
    import Domain.File;
    
    import flash.events.Event;
    import flash.events.ProgressEvent;
    import flash.net.FileReference;
    
    import mx.managers.PopUpManager;
    
    public class FileDownloadController
    {
        
        [Bindable]
        public var Model:File;
        
        private var m_fileReference:FileReference;
        private var View:FileDownloadView;
        
        public function FileDownloadController( view:FileDownloadView ):void
        {
            View = view;
        }
        
        public function ShowProgress(fileReference:FileReference, file:File):void 
        {
            m_fileReference = fileReference;
            Model = file;
                            
            m_fileReference.addEventListener(ProgressEvent.PROGRESS, ProgressHandler);
            m_fileReference.addEventListener(Event.COMPLETE, CompleteHandler);
            
            View.pb.label = "DOWNLOADING %3%%";
        }

        public function OnCancel():void {
            m_fileReference.cancel();
            
            PopUpManager.removePopUp(View);
        }
            
        private function ProgressHandler(event:ProgressEvent):void {
            View.pb.setProgress(event.bytesLoaded, event.bytesTotal);
        }

        private function CompleteHandler(event:Event):void {
            View.pb.setProgress(1, 1);                
            View.pb.label = "DOWNLOAD COMPLETE";
            View.cancelButton.label = "Close";
        }

    }
}