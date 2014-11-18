package UI.MessageBox.MessageTree
{
    import Domain.Message;
    
    [Bindable]
    public class MessageTreeGroup extends MessageTreeItem
    {
        
        public var isOpened:Boolean;
        public var items:Array;
        public var groupName:String;
        
        public function MessageTreeGroup(groupMessage:Message):void 
        {
            super(groupMessage, null);
            
            isOpened = true;
            items = [];
            hasAttachments = false;
            groupName = groupMessage.Subject;
        }

        override public function get messageSubjectDisplayValue():String 
        {
            return "";
        }

        override public function get messageFromDisplayValue():String 
        {
            return "";
        }

        override public function get messageFromTipValue():String 
        {
            return "";
        }

        override public function get messageSentDisplayValue():Date 
        {
            return null;
        }

        override public function get messageSizeDisplayValue():String 
        {
            return "";
        }
        
        override public function get messageSubjectSortValue():String 
        {
            return super.messageSubjectDisplayValue;
        }
        
        override public function get messageFromSortValue():String 
        {
            return super.messageFromDisplayValue;
        }

    }
}