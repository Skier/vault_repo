package com.llsvc.component
{
import mx.containers.HBox;	
import mx.controls.TextInput;
import flash.events.TextEvent;
import flash.events.MouseEvent;
import flash.events.KeyboardEvent;
import flash.events.FocusEvent;
import flash.events.Event;
import flash.ui.Keyboard;
import mx.formatters.NumberFormatter;

[Event(name="inputMaskEnd")]

public class FracInput extends HBox
{
    public var fracInput:TextInput;
    public var decInput:TextInput;
	public var decFormatter:NumberFormatter = new NumberFormatter();
	
    public function FracInput() {
        super();
        
        fracInput = new TextInput();
        decInput = new TextInput();
        fracInput.width = 60;
        decInput.width = 60;
        addChild(fracInput);
        addChild(decInput);
    
        fracInput.addEventListener(Event.CHANGE, onFracChanged);
        decInput.addEventListener(Event.CHANGE, onDecChanged);
        
        decFormatter.precision = 16;
    }
    
    private function onFracChanged(event:Event):void {
        var expr:String = fracInput.text;
        var slashIndex:int = expr.indexOf("/");
        if ( -1 != slashIndex ) {
            var a1:int = new int(expr.substr(0, slashIndex));
            var a2:int = new int(expr.substr(slashIndex+1));
            if ( 0 != a2 ) {
                decInput.text = decFormatter.format(a1/a2);
            }
        }
    }
    
    private function onDecChanged(event:Event):void {
    	var dec:Number = new Number(decInput.text);
    	if ( 0  != dec ) {
	    	var w:Number = Math.floor(dec);
	    	var q:Number = 1;
	    	while ( w != dec ) 	{
	    		dec = dec * 10;
	    		w = Math.floor(dec);
	    		q = q * 10;
	    	}
	 
	 		var i:Number = w;
	 		var j:Number = q;   	
	 		if ( i != 0 && j != 0 ) {
				while (i != j) {
					if (i > j) {
						i -= j;
					} else {
						j -= i;
					}
				}
				w = w/i;
				q = q/i;
	 		}
	 
	    	fracInput.text = w + "/" + q;
    	} else {
	    	fracInput.text = "";
    	}
    }
}
}
