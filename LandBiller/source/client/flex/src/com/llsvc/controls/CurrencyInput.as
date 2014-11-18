package com.llsvc.controls
{
import flash.events.Event;

import mx.formatters.CurrencyFormatter;

[Bindable]
public class CurrencyInput extends SelectableInput
{
    public var currencyFormatter:CurrencyFormatter = new CurrencyFormatter();
    
    public function CurrencyInput() {
        super();
        
        currencyFormatter.precision = 2;
    }

    public function set currency(val:String):void {
        text = currencyFormatter.format(val);
    }

    public function get currency():String { 
        var r:String = "";
        for (var i:int=0; i<text.length; i++ ) {
            var ch:String = text.charAt(i);
            if ( '.' == ch || ('0' <= ch && '9' >= ch ) ) {
                r += ch;
            }
        }
        return r;       
    }
        
    public override function onFocusIn(event:Event):void {
        text = currency;
        super.onFocusIn(event);
    }
    
    public override function onFocusOut(event:Event):void {
        text = currencyFormatter.format(text);
    }
    
}

}
