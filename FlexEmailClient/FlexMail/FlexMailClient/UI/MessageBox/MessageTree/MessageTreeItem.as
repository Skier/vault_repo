package UI.MessageBox.MessageTree
{
    import Domain.Message;
    import mx.collections.SortField;
    
    
    [Bindable]
    public class MessageTreeItem
    {
        
        public var parent:MessageTreeGroup;
        public var hasAttachments:Boolean;
        public var message:Message;
        
        public function MessageTreeItem(message:Message, parent:MessageTreeGroup):void 
        {
            this.message = message;
            this.parent = parent;
            
            hasAttachments = message.HasAttachments;
            
            if (parent)
                this.parent.items.push(this);
        }
        
        public function get messageSubjectDisplayValue():String 
        {
            return message.Subject;
        }

        public function get messageFromDisplayValue():String 
        {
            if (message.From.Name && message.From.Name.length > 0)
                return message.From.Name;
            else 
                return message.From.Address;
        }

        public function get messageFromTipValue():String 
        {
            return message.From.DisplayValue;
        }

        public function get messageSentDisplayValue():Date 
        {
            return message.Sent;
        }

        public function get messageSizeDisplayValue():String 
        {
            if (message.Size > 1024)
                return Math.round(message.Size/1024) + " Kb";
            else
                return message.Size + " b";                
        }
        
        public function get messageSubjectSortValue():String 
        {
            return messageSubjectDisplayValue;
        }
        
        public function get messageSizeSortValue():int 
        {
            return message.Size;
        }

        public function get messageFromSortValue():String 
        {
            return messageFromDisplayValue;
        }

        public function get messageSentSortValue():Number 
        {
            return message.Sent.getTime();
        }
        
        public static function AdjustSortField(sf:SortField):SortField
        {
            var result:SortField = null;
            
            switch (sf.name)
            {
                case "messageFromDisplayValue":
                    result = new SortField("messageFromSortValue", false, sf.descending);
                    break;
                case "messageSubjectDisplayValue":
                    result = new SortField("messageSubjectSortValue", false, sf.descending);
                    break;
                case "messageSentDisplayValue":
                    result = new SortField("messageSentSortValue", false, sf.descending, true);                
                    break;
                case "messageSizeDisplayValue":
                    result = new SortField("messageSizeSortValue", false, sf.descending, true);
                    break;
                default:
                    result = sf;
            }
            
            return result;
        }
    }
}