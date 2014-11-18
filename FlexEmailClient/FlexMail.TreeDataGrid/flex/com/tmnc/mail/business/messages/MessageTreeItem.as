package com.tmnc.mail.business.messages
{
    import com.tmnc.components.treeDataClasses.ITreeItem;
    
    [Bindable]
    public class MessageTreeItem implements ITreeItem
    {
        private var _isOpened:Boolean = false;
        private var _hasChildren:Boolean = false;
        private var _depthLevel:int = 0; //root level

        private var data:MessageInfo = null;

        public var hasAttachment:Boolean = false;
                
        public function MessageTreeItem(message:MessageInfo):void {
            this.data = message;
            this.hasAttachment = data.BodyPartList.length > 0;
        }
        
        public function get messageSubject():String {
            return this.data.Subject;
        }
        
        public function get messageFrom():String {
            return data.From;
        }
        
        public function get sentDate():Date {
            return data.Sent;
        }

        public function get size():String {
            if (data.Size > 1024){
		        return Math.round(data.Size/1024) + " Kb";
            } else {
		        return data.Size + " b";                
            }
        }
        
        public function get hasChildren():Boolean {
            return _hasChildren;
        }
        
        public function set hasChildren(value:Boolean):void {
            _hasChildren = value;
        }
        
        public function get depthLevel():int {
            return _depthLevel;
        }
        
        public function set depthLevel(value:int):void {
            _depthLevel = value;
        }
        
        public function get isOpened():Boolean {
            return _isOpened;
        }
        
        public function set isOpened(value:Boolean):void {
            _isOpened = value;
        }
        
        public function get itemId():String {
            return data.Uid;
        }
        
        public function get parentId():String {
            if (data.InReplyTo && data.InReplyTo.length > 0){
                return data.InReplyTo;
            }
            
            return "";
        }
        
    }
}