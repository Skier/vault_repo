package UI.Compose
{
    import Domain.File;
    import Domain.MailAddress;
    import Domain.Message;
    import Domain.MessageBody;
    
    import UI.AppController;
    
    import flash.net.URLRequest;
    import flash.net.URLRequestMethod;
    import flash.net.URLVariables;
    
    import mx.controls.Alert;
    import mx.controls.Text;
    import mx.events.CloseEvent;
    import mx.events.ValidationResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.remoting.mxml.RemoteObject;
    import mx.utils.UIDUtil;
    
    public class ComposeController {
        
        [Bindable]
        public var Model:Message;
        
        [Bindable]
        public var Parent:AppController;

        private var View:ComposeView;
        
        private var m_uploadResuest:URLRequest;
        
        private var m_remoteObject:RemoteObject;
        
        public function ComposeController(view:ComposeView, parent:AppController):void {
            this.Parent = parent;
            this.View = view;
            
            m_remoteObject = new RemoteObject(AppController.FLEX_MAIL_SERVER_OBJECT_NAME);
            m_remoteObject.addEventListener(ResultEvent.RESULT, OnUploadUriReturn);
            m_remoteObject.addEventListener(FaultEvent.FAULT, Parent.OnFault);
            m_remoteObject.showBusyCursor = true;
            
            m_uploadResuest = new URLRequest();
            m_uploadResuest.method = URLRequestMethod.POST;
            m_uploadResuest.data = new URLVariables();
            
            m_remoteObject.GetFileUploaderURL();
        }
        
        public function Initialize(message:Message):void
        {
            View.body.textArea.text = "";
            
            Model = message;
            
            m_uploadResuest.data.uid = Model.Uid = UIDUtil.createUID();

            if (Model.To.length == 0)
            {
                View.ToEmail.setFocus();
            } 
            else 
            {
                View.body.textArea.validateNow();
                View.body.textArea.setSelection(0,0);
                View.body.textArea.setFocus();
            }
            
            View.attachmentPanel.attachmentList.RemoveAllItems();
            View.attachmentPanel.attachmentList.UploadRequestURL = m_uploadResuest;
        }

        public function OnSend():void
        {
            if (View.ToEmail.text.length == 0) 
            {
                Alert.show("The mail Recipients (field 'To') must be specified.");
                return;
            }
            
            if (View.attachmentPanel.attachmentList.HasBusyItems){
                Alert.show("Some of your attachments is still uploading. Please wait.");
                return;
            }
            
            var message:Message = new Message();

            message.To = ParseEmailAddresses(View.ToEmail.text);
            message.Cc = ParseEmailAddresses(View.CcEmail.text);
            message.Bcc = ParseEmailAddresses(View.BccEmail.text);
            message.Subject = View.subjectTxt.text;
            message.Uid = Model.Uid;
            message.InReplyTo = Model.InReplyTo;
            message.Body = new MessageBody();

            message.Body.Attachments = View.attachmentPanel.attachmentList.GetItems();

			message.Body.PlainBody = View.body.text;

            if (!View.body.usePlainText)
            {
				message.Body.HtmlBody = new File();
				message.Body.HtmlBody.Text = View.body.htmlText;
            }

            Parent.SendMessage( message );
        }

        public function OnDiscard():void
        {
            if (View.ToEmail.text.length > 0 || View.subjectTxt.text.length > 0 ||
                View.body.text.length > 0 || View.attachmentPanel.attachmentList.Items.length > 0)
            {
                Alert.show('Are you sure to discard changes and return to Inbox view ?', 'Alert',
                    Alert.YES | Alert.NO, View, DiscardAlertListener, null, Alert.NO);
            } 
            else 
            {
                Parent.OnComposeDiscard();    
            }
        }
        
        public function DiscardAlertListener(event:CloseEvent):void 
        {
            if (event.detail == Alert.YES)
                Parent.OnComposeDiscard();
        }
        
        public function OnUploadUriReturn(event:ResultEvent):void
        {
            m_uploadResuest.url = String(event.result);
        }
        
        private function ParseEmailAddresses(addressList:String):Array {
            var result:Array = [];

            var atPosition:int = addressList.indexOf("@");
            while (atPosition > 0){
                
                var addressEndPosition:int = addressList.indexOf(">", atPosition);
                
                if (addressEndPosition != -1)                                
                    addressEndPosition++;
                else 
                    addressEndPosition = addressList.indexOf(",", atPosition);
                    
                if (addressEndPosition == -1)
                    addressEndPosition = addressList.indexOf(" ", atPosition);
                    
                var token:String;
                
                if (addressEndPosition != -1)
                    token = addressList.substring(0, addressEndPosition);
                else
                    token = addressList.substring(0, addressList.length);
                
                result.push(MailAddress.createFromString(token));
                
                addressList = addressList.substr(token.length);
                atPosition = addressList.indexOf("@");
            }
            
            return result;
        }

    }
}