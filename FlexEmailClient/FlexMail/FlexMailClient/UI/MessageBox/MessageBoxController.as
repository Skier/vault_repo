package UI.MessageBox
{
    import Domain.File;
    import Domain.Message;
    import Domain.MessageBody;
    
    import UI.AppController;
    import UI.IAppControllerListener;
    import UI.MessageBox.MessageTree.MessageTreeGroup;
    import UI.MessageBox.MessageTree.MessageTreeItem;
    
    import flash.text.TextField;
    import flash.text.TextFormat;
    
    import mx.collections.ArrayCollection;
    import mx.controls.Alert;
    import mx.controls.Text;
    import mx.events.DataGridEvent;
    
    public class MessageBoxController implements IAppControllerListener
    {
        [Bindable]
        public var Model:MessageBoxModel;
        
        [Bindable]
        public var Parent:AppController;
        public var View:MessageBoxView;
        
        public function MessageBoxController(view:MessageBoxView, parentController:AppController):void 
        {
            View = view;
            
            Parent = parentController;
            Parent.AddListener(this);
            
            Model = new MessageBoxModel();
            Model.Reset();
        }
        
        public function OnGetMore():void 
        {
            Parent.RetrieveMoreMessages();
        }

        public function OnReply():void
        {
            var composeMessage:Message = new Message();
            
            composeMessage.To.push( Model.CurrentMessage.From );
            composeMessage.Cc = Model.CurrentMessage.Cc;
            composeMessage.Subject = "Re: " + Model.CurrentMessage.Subject;
            
            if (Model.CurrentMessage.MessageId)
                composeMessage.InReplyTo = Model.CurrentMessage.MessageId;
         
            var header:String = StringToProperHtml(Model.CurrentMessage.From.DisplayValue + " wrote: \n\n");
            
            var replyText:String = "";
            
            if (Model.CurrentMessage.Body && Model.CurrentMessage.Body.PlainBody.length > 0)
            {
                //insert symbols ">>" before each line
                replyText = Model.CurrentMessage.Body.PlainBody.replace(/^|\r\n|\r|\n/g, "\n>>");
                replyText = StringToProperHtml(replyText, 0x663300);
            }
            
            composeMessage.Body = new MessageBody();
            composeMessage.Body.HtmlBody = new File();
            composeMessage.Body.HtmlBody.Text = "\n\n" + header + replyText;
                
            Parent.ComposeMessage( composeMessage );
        }

        public function OnForward():void 
        {
            var composeMessage:Message = new Message();
            
            composeMessage.Subject = "Fwd: " + Model.CurrentMessage.Subject;
            
            var header:String = "\n========This is a forwarded message======.\n";
            header += "From    : " + Model.CurrentMessage.From.DisplayValue + "\n";
            header += "To      : " + Model.CurrentMessage.From.DisplayValue + "\n";            
            header += "Sent    : " + Model.CurrentMessage.Sent.toDateString() + "\n";
            header += "Subject : " + Model.CurrentMessage.Subject + "\n";
            header += "=======================================\n\n";
            header = StringToProperHtml(header, 0x663300);

            var body:String = "";
            
            if (Model.CurrentMessage.Body && Model.CurrentMessage.Body.PlainBody.length > 0)
                body = StringToProperHtml(Model.CurrentMessage.Body.PlainBody);

			composeMessage.Body = new MessageBody();
			composeMessage.Body.HtmlBody = new File();
			composeMessage.Body.HtmlBody.Text = "\n\n" + header + body;
            
            Parent.ComposeMessage( composeMessage );
        }

        public function OnDelete():void 
        {
            if (View.m_inboxGrid.selectedItems.length == 0)
                return;

            //Associative array where keys are Message objects. Used for do not allow duplicate values.
            var deleteCandidates:ArrayCollection = new ArrayCollection();
            
            for each (var treeItem:* in View.m_inboxGrid.selectedItems)
            {
                if (treeItem is MessageTreeGroup)
                {
                    for each(var groupItem:MessageTreeItem in treeItem.items)
                    {
                        if (!deleteCandidates.contains(groupItem.message))
                            deleteCandidates.addItem(groupItem.message);
                    }
                } 
                else 
                {
                    if (!deleteCandidates.contains(treeItem.message))
                        deleteCandidates.addItem(treeItem.message);
                }
            }
            
            Parent.DeleteMessages( deleteCandidates.toArray() );
        }

        public function OnGroupIconClick(group:MessageTreeGroup):void
        {
            group.isOpened = !group.isOpened;
            Model.Messages.refresh();
        }
        
        public function OnCurrentMessageChanged():void
        {
            if (View.m_inboxGrid.selectedItem && !(View.m_inboxGrid.selectedItem is MessageTreeGroup))
            {
                Model.CurrentMessage = Message(View.m_inboxGrid.selectedItem.message);
                
                if (!Model.CurrentMessage.Body)
                    Parent.RetrieveMessageBody(Model.CurrentMessage);
            }
            else
            {
                Model.CurrentMessage = null;
            }
        }

        public function OnNewMessagesReceipt(messages:Array):void
        {
            Model.Messages.AddItems(messages);
        }

        public function OnMessagesDeleted(messages:Array):void
        {
            Model.Messages.RemoveItems(messages);

            if (messages.indexOf(Model.CurrentMessage) != -1)
                Model.CurrentMessage = null;
        }

        public function OnReset():void
        {
            Model.Reset();
        }
        
        private static function StringToProperHtml(s:String, fontColor:uint=0x000000, fontFace:String="Verdana"):String 
        {
            var tf:TextFormat = new TextFormat();
            tf.color = fontColor;
            tf.font = fontFace;

            var field:TextField = new TextField();
            field.text = s;
            field.setTextFormat(tf);

            return field.htmlText;
        }
        
    }
}