package com.llsvc.component
{
import flash.events.Event;
import flash.events.FocusEvent;

import mx.controls.TextInput;

public class SelectableInput extends TextInput
{
	public var isUpperCase:Boolean = true;
	
    public function SelectableInput() {
        super();
        
        addEventListener(FocusEvent.FOCUS_IN, onFocusIn);
        addEventListener(FocusEvent.FOCUS_OUT, onFocusOut);
        addEventListener(Event.CHANGE, onChange);
    }

    public function onFocusIn(event:Event):void {
        setSelection(0, text.length);
    }
    
    public function onFocusOut(event:Event):void {
    }
    
    private function onChange(event:Event):void 
    {
    	if (isUpperCase) {
    		this.text = this.text.toUpperCase();
    	}
    }
    
}

}
