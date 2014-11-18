package com.tmnc.mail.business
{
    import mx.controls.Text;
    import com.tmnc.mail.vo.MessageInfo;
    
    
    [Bindable]
    public class MessageTreeGroup extends MessageTreeItem
    {
        
        public var isOpened:Boolean;
        public var items:Array;
        
        public function MessageTreeGroup(correspondetMsg:MessageInfo):void {
            super(correspondetMsg, null);
            
            isOpened = true;
            items = [];
        }

        override public function get messageSubjectDisplayValue():String {
            return "";
        }

        override public function get messageFromDisplayValue():String {
            return "";
        }

        override public function get messageFromTipValue():String {
            return "";
        }

        override public function get messageSentDisplayValue():Date {
            return null;
        }

        override public function get messageSizeDisplayValue():String {
            return "";
        }
        
        public function get messageGroupName():String{
            return message.Subject;
        }
    
        override public function get hasAttachments():Boolean {
            return false;
        }

        override public function get messageSubjectSortValue():String {
            return super.messageSubjectDisplayValue;
        }
        
        override public function get messageFromSortValue():String {
            return super.messageFromDisplayValue;
        }

    }
}