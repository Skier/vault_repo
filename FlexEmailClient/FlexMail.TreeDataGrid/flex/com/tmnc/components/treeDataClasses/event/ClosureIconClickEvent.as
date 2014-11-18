package com.tmnc.components.treeDataClasses.event
{
    import flash.events.Event;
    import com.tmnc.components.treeDataClasses.ITreeItem;
    
    public class ClosureIconClickEvent extends Event {
        
        public static const CLOSURE_ICON_CLICK_EVENT:String = "closure_icon_click_event";
	
        public function ClosureIconClickEvent(item:ITreeItem, bubbles:Boolean=true, cancelable:Boolean=false){
            super(CLOSURE_ICON_CLICK_EVENT, bubbles, cancelable);
            this.item = item;
        }
 
        override public function clone():Event {
            return new ClosureIconClickEvent(item, bubbles, cancelable);
        }
        
        public var item:ITreeItem;
           
    }
}