package com.tmnc.mail.business
{
    import com.tmnc.mail.vo.MessageInfo;
    
    
    [Bindable]
    public class MessageTreeItem
    {
        
        public var parent:MessageTreeGroup;
        
        public var message:MessageInfo;
        
        public function MessageTreeItem(message:MessageInfo, parent:MessageTreeGroup):void {
            this.message = message;
            this.parent = parent;
            if (parent)
                this.parent.items.push(this);
        }
        
        //DataGridColumns dataFields
        
        public function get messageSubjectDisplayValue():String {
            return message.Subject;
        }

        public function get messageFromDisplayValue():String {
            
            if (message.From.Name && message.From.Name.length > 0){
                return message.From.Name;
            } else {
                return message.From.Address;
            }
            
        }

        public function get messageFromTipValue():String {
            return message.From.DisplayValue;
        }

        public function get messageSentDisplayValue():Date {
            return message.Sent;
        }

        public function get messageSizeDisplayValue():String {
            
            if (message.Size > 1024){
                return Math.round(message.Size/1024) + " Kb";
            } else {
                return message.Size + " b";                
            }

        }
        
        public function get hasAttachments():Boolean {
            return message.HasAttachments;
        }

        //properties used for sorting
        
        public function get messageSubjectSortValue():String {
            return messageSubjectDisplayValue;
        }
        
        public function get messageSizeSortValue():int {
            return message.Size;
        }

        public function get messageFromSortValue():String {
            return messageFromDisplayValue;
        }

        public function get messageSentSortValue():Number {
            return message.Sent.getTime();
        }
        
        public static function getSortFieldName(dataFieldName:String):String{
            var result:String;
            
            switch (dataFieldName){
                case "messageSubjectDisplayValue":
                    result = "messageSubjectSortValue";
                    break;
                case "messageSizeDisplayValue":
                    result ="messageSizeSortValue";
                    break;
                case "messageSentDisplayValue":
                    result = "messageSentSortValue";
                    break;
                case "messageFromDisplayValue":
                    result = "messageFromSortValue";
                    break;
                default:
                    result = dataFieldName;
            }
            
            return result;
        }
    }
}