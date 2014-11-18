package UI.MessageBox.MessageDetail
{
    import Domain.File;
    import Domain.Message;
    
    import flash.display.DisplayObject;
    import flash.events.Event;
    import flash.net.FileReference;
    import flash.net.URLRequest;
    import flash.net.navigateToURL;
    
    import mx.core.Application;
    import mx.events.ItemClickEvent;
    import mx.managers.PopUpManager;
    import UI.MessageBox.MessageBoxController;
    import mx.binding.utils.ChangeWatcher;
    import mx.events.PropertyChangeEvent;
    
    public class MessageDetailController
    {
        [Bindable]
    	public var Model:Message;

        [Bindable]
        private var Parent:MessageBoxController;
        
    	private var View:MessageDetailView;
    	
    	public function MessageDetailController(view:MessageDetailView, parent:MessageBoxController):void
    	{
    		this.View = view;
    		this.Parent = parent;
    		this.Model = parent.Model.CurrentMessage;
    		
    		ChangeWatcher.watch(parent.Model, "CurrentMessage", OnModelChange);
    	}
    	
    	private function OnModelChange(event:PropertyChangeEvent):void
    	{
    	    Model = Message(event.newValue);
    		ChangeWatcher.watch(Model, "Status", OnModelStatusChange);
    		
            UpdateViewState();
    	}

        private function OnModelStatusChange(event:PropertyChangeEvent):void
        {
            UpdateViewState();
        }

        private function UpdateViewState():void
        {
    	    if (null == Model)
    	        View.currentState = "noMessageView";
    	    else if (Model.Status == Message.BODY_RETRIEVING)
    	        View.currentState = "messageBodyLoading";
    	    else
    	        View.currentState = "messageView";
        }
        
        public function OnAttachmentClick(event:ItemClickEvent):void
        {
            var file:File = File(event.item);
            var popupParent:DisplayObject = DisplayObject(Application.application);
            
            var fr:FileReference = new FileReference();
            
            fr.addEventListener(Event.OPEN, 
                function (event:Event):void 
                {
                    var downloadView:FileDownloadView = FileDownloadView( 
                        PopUpManager.createPopUp( DisplayObject(Application.application), FileDownloadView, false ));
                    
                    downloadView.Controller.ShowProgress(fr, file);
                }
            );

            var request:URLRequest = new URLRequest();

            request.url = file.Url;

            fr.download(request, file.Name);
        }
        
        public function OnHtmlBodyLinkClick():void
        {
        	var bodyRequest:URLRequest = new URLRequest(Model.Body.HtmlBody.Url);
            navigateToURL(bodyRequest, "_blank");
        }
    }
}