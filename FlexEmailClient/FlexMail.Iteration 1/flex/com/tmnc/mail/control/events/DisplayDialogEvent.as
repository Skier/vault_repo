package com.tmnc.mail.control.events
{
    import flash.events.Event;
    import flash.display.DisplayObject;

    public class DisplayDialogEvent extends Event
    {

        /** Event type constant; indicates that the Account Registration dialog should be displayed. */
        public static const EVENT_DISPLAY_REGISTRATION_DIALOG:String = "displayRegistrationDialog";
        
        /** Event type constant; indicates that the Account Settings dialog should be displayed. */        
        public static const EVENT_DISPLAY_SETTINGS_DIALOG:String = "displaySettingsDialog";

        /**
         *  Constructor.
         * 
         *  @param dialogParent the DisplayObject which should be the parent of the dialog
         *  @param bubbles Determines whether the Event object participates in
         *  the bubbling stage of the event flow.  The default value is true.
         *  @param cancelable Determines whether the Event object can be canceled.
         *  The default value is false.
         **/
        public function DisplayDialogEvent(type:String, bubbles:Boolean=true, cancelable:Boolean=false):void {
            super(type, bubbles, cancelable);
        }
        
        /**
         *  Duplicates this Event object.
         *  @return the cloned Event object
         **/
        public override function clone():Event {
            return new DisplayDialogEvent(type, bubbles, cancelable);
        }

    }
}