/**
 * MaskedInput
 * 
 * This class extends TextInput and provides input masking. For example,
 * to enter a US telephone number, you can set up a mask for (617) 555-1212
 * such that the parentheses, space, and hyphen are present. All the user
 * has to do it enter 6175551212 and the insertion point moves along.
 * 
 * The mask can consist of any characters except:
 * 
 * # which stands for single digit
 * C which capitalizes a letter (no digits allowed)
 * c which forces a letter to lowercase (no digits allowed)
 * A or a which allows any character.
 * 
 * The mask for a phone number would be: (###) ###-####
 * 
 * Properties: (in addition to those of TextInput)
 * 
 * inputMask - the string to use to mask typed characters.
 * blankChar - (default '_') the character used to represent the typable fields
 *      so users know input is expected.
 * defaultChar - (default '_') the character used in place of blankChar when
 *      actualText is returned.
 * text - the text to display or the text entered, without the mask. For example,
 *      given the phone number mask, the text would be "6175551212".
 * actualText (read only) - the text as displayed and entered, including the
 *      mask: "(617) 555-1212" or "(__) 555-1212". If defaultChar is different
 *      than blankChar, defaultChar is substituted for the blankChar in the
 *      actual text: "(000) 555-1212".
 * 
 * Events: (in addition to those of TextInput)
 * 
 * inputMaskEnd - dispatched when the last character is entered that completes
 *      the mask. In the example, it would be the final 2.
 * 
 * Styles: (no new styles)
 * 
 * Keyboard:
 * 
 * Backspace moves the insertion point back a single space, replacing
 *     the character with the blankChar character.
 * 
 * Delete replaces the character to the right of the insertion point with
 *     the blankChar character and then advances the insertion point to
 *     the next viable input position.
 * 
 * Space replaces the next visbale input position with the defaultChar.
 * 
 * Left and Right Arrow move the insertion point to the previous or next
 *     viable input position.
 * 
 * Home moves the insertion point to the first viable input character
 *     in the field.
 * 
 * End moves the insertion point to the last viable input character
 *     in the field.
 */
package com.affilia.controls
{
import mx.controls.TextInput;
import flash.events.TextEvent;
import flash.events.MouseEvent;
import flash.events.KeyboardEvent;
import flash.events.FocusEvent;
import flash.events.Event;
import flash.ui.Keyboard;

[Event(name="inputMaskEnd")]

public class MaskedInput extends TextInput
{
    public function MaskedInput()
    {
        super();
        
        addEventListener(TextEvent.TEXT_INPUT,interceptChar,true,0);
        addEventListener(MouseEvent.CLICK,reposition,true);
        addEventListener(KeyboardEvent.KEY_DOWN,interceptKey,true);
        addEventListener(FocusEvent.FOCUS_IN,interceptFocus,false);
    }
    
    /*
     * private vars
     */
    private var _working:Array = [];
    private var _position:Number = 0;
    private var bWorkingUpdated:Boolean = false;
    private var bMaskUpdated:Boolean = false;
    private var bTextUpdated:Boolean = false;
    
    /**
     * blankChar
     **/
    private var _blankChar:String = "_";
    public function get blankChar() : String
    {
        return _blankChar;
    }
    public function set blankChar( s:String ) : void
    {
        if( s.length == 0 ) return;
        _blankChar = s.charAt(0);
        invalidateDisplayList();
    }
    
    /**
     * defaultChar
     */
    private var _defaultChar:String = "_";
    public function set defaultChar( s:String ) : void
    {
        _defaultChar = s;
    }
    public function get defaultChar() : String
    {
        return _defaultChar;
    }
    
    /**
     * inputMask
     */
    private var _inputMask:String;
    public function get inputMask() : String
    {
        return _inputMask;
    }
    public function set inputMask(s:String) : void
    {
        _inputMask = s;
        bMaskUpdated = true;
        invalidateDisplayList();
    }
    
    /**
     * text
     */
    private var _text:String = "";
    override public function get text():String
    {
        var result:String = "";
        for(var i:Number=0; i < _working.length; i++) {
            var c:String = _working[i];
            if( _inputMask.charAt(i) == c ) continue;
            if( c == _blankChar ) c = " ";
            result += c;
        }
        return result;
    }
    
    override public function set text(value:String):void
    {
        _text = value;
        bTextUpdated = true;
        invalidateDisplayList();
    }
    
    /**
     * actualText (read only)
     */
    public function get actualText() : String
    {
        var result:String = "";
        for(var i:Number=0; i < _working.length; i++) {
            var c:String = _working[i];
            if( c == _blankChar ) c = _defaultChar;
            result += c;
        }
        return result;
    }
    
    /*
     * event handlers
     */
    
    /**
     * reposition
     * 
     * Handles MOUSE_CLICK event; repositions the insertion point
     */
    private function reposition( event:flash.events.MouseEvent ) : void
    {
        var p:Number = this.selectionBeginIndex;
        _position = p;
    }
    
    /**
     * interceptKey
     * 
     * Looks for special keys and modifies the insertion point
     */
    private function interceptKey( event:flash.events.KeyboardEvent ) : void
    {
        if( event.keyCode == Keyboard.BACKSPACE ) {
            _position = selectionBeginIndex;
            retreatPosition();
            allowChar(_blankChar);
        }
        else if( event.keyCode == Keyboard.SPACE ) {
            allowChar(_defaultChar);
            advancePosition();
        }
        else if( event.keyCode == Keyboard.DELETE ) {
            if( _position < _inputMask.length ) {
                allowChar(_blankChar);
                advancePosition(true);
            }
        }
        else if( event.keyCode == Keyboard.LEFT ) {
            _position = this.selectionBeginIndex;
            retreatPosition();
            event.preventDefault();
        }
        else if( event.keyCode == Keyboard.RIGHT ) {
            advancePosition(true);
            event.preventDefault();
        }
        else if( event.keyCode == Keyboard.END ) {
            _position = _working.length;
            event.preventDefault();
        }
        else if( event.keyCode == Keyboard.HOME ) {
            _position = -1;
            advancePosition(true);
        }
        bWorkingUpdated = true;
        invalidateDisplayList();
    }
    
    /**
     * interceptFocus
     * 
     * Consumes the FOCUS_IN event and repositions the insertion
     * point.
     */
    private function interceptFocus( event:FocusEvent ) : void
    {
        var start:Number = selectionBeginIndex;
        if( event.relatedObject != null ) start = 0;
        setSelection( start, start );
        _position = start - 1;
        
        // advance the insertion point to the first viable input field.
        advancePosition();
    }
    
    /**
     * interceptChar
     * 
     * Handle TEXT_INPUT events by matching the character with
     * the mask and either blocking or allowing the character.
     */
    private function interceptChar( event:TextEvent ) : void
    {
        var input:String = event.text;
        
        if( _inputMask.length <= _position ) {
            event.preventDefault();
            dispatchEvent(new Event("inputMaskEnd"));
            return;
        }
        
        var c:String = input.charAt(0);
        var m:String = _inputMask.charAt(_position);
        var bAdvance:Boolean = true;
        
        switch(m) 
        {
            case "#":
                if( isDigit(c) ) {
                    allowChar(c);
                } else {
                    event.preventDefault();
                    bAdvance = false;
                }
                break;
            case "C":
                if( isDigit(c) ) {
                    event.preventDefault();
                    bAdvance = false;
                } else {
                    allowChar(c.toUpperCase());
                }
                break;
            case "c":
                if( isDigit(c) ) {
                    event.preventDefault();
                    bAdvance = false;
                } else {
                    allowChar(c.toLowerCase());
                }
                break;
            case "A","a":
                allowChar( c.toLowerCase() );
                break;
            default:
                break;
        }
        
        if( bAdvance ) {
            advancePosition();
        }
        bWorkingUpdated = true;
        invalidateDisplayList();
    }
    
    /**
     * advancePosition
     * 
     * Moves the insertion point forward (if possible) to the next viable
     * input position.
     */
    protected function advancePosition(byArrow:Boolean=false) : void
    {
        var p:Number = _position;
        while( (++p) < _inputMask.length && !isMask(_inputMask.charAt(p)) ) ;
        _position = p;
        if( p >= _inputMask.length && !byArrow ) {
            dispatchEvent(new Event("inputMaskEnd"));
        }
        setSelection(_position,_position);
    }
    
    /**
     * retreatPosition
     * 
     * Moves the insertion point backward (if possible) to the previous
     * viable input position.
     */
    protected function retreatPosition() : void
    {
        var p:Number = _position;
        while( (--p) > 0 && !isMask(_inputMask.charAt(p)) ) ;
        _position = p;
        setSelection(_position,_position);
    }
    
    /**
     * isMask
     * 
     * Returns true if the given character is a masking character.
     */
    protected function isMask( c:String ) : Boolean
    {
        return c == "#" || c == "A" || c == "C" || c == "c";
    }
    
    /**
     * isDigit
     * 
     * Returns true if the given character is a digit.
     */
    protected function isDigit( c:String ) : Boolean
    {
        return c == "0" || c == "1" || c == "2" || c == "3" ||
               c == "4" || c == "5" || c == "6" || c == "7" ||
               c == "8" || c == "9";
    }
    
    /**
     * allowChar
     * 
     * Inserts the character into the working array.
     */
    private function allowChar( c:String ) : void
    {
        _working[_position] = c;
    }
    
    /**
     * updateDisplayList
     * 
     * Modifies the display according to how flags are set: if
     * text has been updated, fold the text according to the mask. If
     * the mask has been updated, modify the display.
     */
    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        if( bMaskUpdated ) {
            bMaskUpdated = false;
            
            _working = [];
            var s:String = _inputMask;
            for(var i:int=0; i < s.length; i++) {
                var c:String = s.charAt(i);
                if( isMask(c) ) {
                    c = _blankChar;
                }
                _working.push(c);
            }
            bWorkingUpdated = true;
        }
        
        if( bTextUpdated ) {
            bTextUpdated = false;
            
            var t:Number = 0;
            var value:String = _text;
            if (value == null)
            	value = "";
            
            for(var j:Number=0; j < _inputMask.length; j++) {
                var m:String = _inputMask.charAt(j);
                if( isMask(m) ) {
                    if( t >= value.length ) _working[j] = _blankChar;
                    else _working[j] = value.charAt(t);
                    t += 1;
                } else {
                    _working[j] = m;
                }
            }
            bWorkingUpdated = true;
        }
        
        if( bWorkingUpdated ) {
            super.text = _working.join("");
            bWorkingUpdated = false;
        }
        
        super.updateDisplayList( unscaledWidth, unscaledHeight );
    }
    
}
}
