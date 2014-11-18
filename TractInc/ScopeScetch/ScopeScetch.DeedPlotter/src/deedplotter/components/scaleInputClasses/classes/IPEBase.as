package src.deedplotter.components.scaleInputClasses.classes
{
import flash.display.Bitmap;
import flash.display.DisplayObject;
import flash.display.Sprite;
import flash.events.Event;
import flash.events.FocusEvent;
import flash.events.KeyboardEvent;
import flash.events.MouseEvent;
import flash.events.TextEvent;
import flash.geom.Point;

import mx.controls.Label;
import mx.controls.TextInput;
import mx.core.IDataRenderer;
import mx.core.UIComponent;
import mx.effects.Resize;
import mx.events.EffectEvent;
import mx.managers.IFocusManagerComponent;
import mx.core.UIComponent;

[Event(name="dataChange", type="mx.events.FlexEvent")]
public class IPEBase extends UIComponent implements IDataRenderer, IFocusManagerComponent
{
	private var _nonEditableControl:UIComponent;
	private var _editableControl:UIComponent;
	private var _editable:Boolean = false;
	private var _changing:Boolean = false;
	
	public var tilt:Boolean = true;
	public var commitOnEnter:Boolean = false;
	public var commitOnBlur:Boolean = false;
	
	public var editOnEnter:Boolean = false;
	public var editOnClick:Boolean = false;

	private var _editButton:EditButton;
	private var _enableIcon:Boolean = false;
	private var _showIcon:Boolean = false;	
	
	// ---- constructor  ----------------------------------------------------------------------------------
	public function IPEBase():void
	{
		super();
		
		addEventListener("enter", enterHandler);
		addEventListener(MouseEvent.MOUSE_DOWN,mouseHandler);
		addEventListener(FocusEvent.FOCUS_OUT,commitOnBlurHandler);
	}

	// -- whether to show the edit icon  ----------------------------------------------------------------------------------
	public function set showIcon(value:Boolean):void
	{
		if(value == _showIcon)
			return;
		_showIcon = value;
		updateEditButton();
		invalidateSize();
	}
	
	public function get showIcon():Boolean { return _showIcon; }

	public function set enableIcon(value:Boolean):void
	{
		if(value == _enableIcon)
			return;
		if(_enableIcon)
			_editButton.removeEventListener(MouseEvent.CLICK,iconClickHandler);		
		_enableIcon = value;
		updateEditButton();
			
		if(_enableIcon)
			_editButton.addEventListener(MouseEvent.CLICK,iconClickHandler);
		
		invalidateSize();
	}
	
	public function get enableIcon():Boolean { return _enableIcon; }

	// -- whether to show the edit icon  ----------------------------------------------------------------------------------
	
	public function set focusReadOnlyEnabled(value:Boolean):void
	{
		_nonEditableControl.focusEnabled = focusEnabled = value;
	}
	public function get focusReadOnlyEnabled():Boolean { return _nonEditableControl.focusEnabled; }


	
	//--- the editable version ----------------------------------------------------------------------------------
	
	protected function set editableControl(value:UIComponent):void
	{
		if(_editableControl != null)
			removeChild(_editableControl);
		_editableControl = value;
		_editableControl.styleName = this;
		addChild(_editableControl);				
		_editableControl.visible = _editable;
		facadeEvents(_editableControl,"dataChange");

		_editableControl.addEventListener(FocusEvent.FOCUS_OUT,commitOnBlurHandler);
		
		invalidateDisplayList();
	}
	protected function get editableControl():UIComponent { return _editableControl; }	
	
	//--- the non-editable version ----------------------------------------------------------------------------------

	protected function set nonEditableControl(value:UIComponent):void
	{
		if(_nonEditableControl != null)
			removeChild(_nonEditableControl);
		_nonEditableControl= value;
		_nonEditableControl.styleName = this;
		addChild(_nonEditableControl);				
		_nonEditableControl.visible = !_editable;
		_nonEditableControl.focusEnabled = focusEnabled;
	}
	
	protected function get nonEditableControl():UIComponent { return _nonEditableControl; }
	
	
	//-- support for using it as an inline renderer
	
	[Bindable("dataChange")]
	public function get data():Object {
		return (_editableControl is IDataRenderer)? IDataRenderer(_editableControl).data:null
	}
	
	public function set data(value:Object):void {			
		if(_nonEditableControl is IDataRenderer)
			IDataRenderer(_nonEditableControl).data = value;
		if(_editableControl is IDataRenderer)
			IDataRenderer(_editableControl).data = value;
	}
	
	
	//-- event handlers ----------------------------------------------------------------------------------
	
	
	private function commitOnBlurHandler(e:FocusEvent):void
	{
		if(editable && commitOnBlur && (e.relatedObject == null || !contains(e.relatedObject)))
		{
			editable = false;
		}		
	}
	private function mouseHandler(e:MouseEvent):void
	{
		if(editOnClick == false)
			return;
		if(_enableIcon)
		{
			if(_editButton.contains(DisplayObject(e.target)))
				return;
		}
		if(_editable)
			return;
		setEditable(true,true);
	}
	
	protected function enterHandler(e:Event):void
	{
	    if ( _editable && commitOnEnter && !commitEditedValue() ) return;
	    
		setEditable(!editable, true);
	}
	
	private function iconClickHandler(e:Event):void
	{
	    if ( _editable && !commitEditedValue() ) return;
		setEditable(!editable, true);
	}
	
	// ---- editable ----------------------------------------------------------------------------------
	
	public function set editable(value:Boolean):void
	{
		var focus:IFocusManagerComponent = focusManager.getFocus();
		var takeFocus:Boolean = false;
		if(_editable)
		{
			takeFocus = (focus == this || focus == _editableControl)
		}
		else
		{
			takeFocus = (focus == this || focusManager.getFocus() == _nonEditableControl);
		}
		setEditable(value,takeFocus);
	}

	public function get editable():Boolean { return _editable; }

	protected function setEditable(value:Boolean, takeFocus:Boolean):void {
		if(value == _editable)
			return;
			
		_editable = value;
	
		tabEnabled = !_editable;

		_changing = true;
		updateEditButton();

        if (_editable) 
            convertToEditable(takeFocus);
        else 
            convertToNotEditable(takeFocus);            
	}
	
    private function convertToEditable(takeFocus:Boolean):void {
		var gap:Number = (_editButton == null)? 0:4;
		var iconWidth:Number = (_editButton == null)? 0:_editButton.measuredWidth;
        
        var resizeEffect:Resize = new Resize(this);
        resizeEffect.heightTo = editableControl.measuredHeight;
        resizeEffect.widthTo = gap + iconWidth + _editableControl.measuredWidth;

		var flipEffect:FlipBitmap = new FlipBitmap(_nonEditableControl, _editableControl);
		var that:IPEBase = this;

        resizeEffect.addEventListener(EffectEvent.EFFECT_END, 
            function (e:Event):void 
            { 
                addChildAt(flipEffect, 0);
		        flipEffect.duration = 450;
		        flipEffect.tilt = tilt;

        		_nonEditableControl.visible = _editableControl.visible = false;

		        flipEffect.play();        
            } );

		flipEffect.addEventListener("complete",
		    function(e:Event):void
		    {
    			removeChild(flipEffect);
    
    			_editableControl.visible = true;
    
    			if(takeFocus) {
    				if (_editable && (_editableControl is IFocusManagerComponent))
    					focusManager.setFocus(IFocusManagerComponent(_editableControl));
    				else
    					focusManager.setFocus(that);
    			}
    			
    			_changing = false;
    		} );

        resizeEffect.play();
    }
    
    private function convertToNotEditable(takeFocus:Boolean):void {
		var gap:Number = (_editButton == null)? 0:4;
		var iconWidth:Number = (_editButton == null)? 0:_editButton.measuredWidth;

        _nonEditableControl.validateSize();
        var resizeEffect:Resize = new Resize(this);
        resizeEffect.heightTo = _nonEditableControl.measuredHeight;
        resizeEffect.widthTo = gap + iconWidth + _nonEditableControl.measuredWidth;

		var flipEffect:FlipBitmap = new FlipBitmap(_editableControl, nonEditableControl);
		var that:IPEBase = this;

        resizeEffect.addEventListener(EffectEvent.EFFECT_END, 
            function (e:Event):void 
            { 
    
    			if(takeFocus) {
    				if (_editable && (_editableControl is IFocusManagerComponent))
    					focusManager.setFocus(IFocusManagerComponent(_editableControl));
    				else
    					focusManager.setFocus(that);
    			}
    			
    			_changing = false;
                
            } );

		flipEffect.addEventListener("complete",
		    function(e:Event):void
		    {
    			removeChild(flipEffect);

    			_nonEditableControl.visible = true;
    			
                resizeEffect.play();
    		} );

		_nonEditableControl.visible = _editableControl.visible = false;

        addChildAt(flipEffect, 0);
        flipEffect.duration = 450;
        flipEffect.tilt = tilt;
        flipEffect.play();        
    }
    
	// --- editing status  ----------------------------------------------------------------------------------
	private function updateEditButton():void
	{		
		if(_showIcon || _enableIcon)
		{
			if(_editButton == null)
			{
				_editButton = new EditButton();
				_editButton.focusEnabled = false;
				_editButton.toggle = true;
				addChild(_editButton);
			}
			_editButton.selected = _editable;
			_editButton.enabled = _enableIcon;
		}
		else
		{
			if(_editButton != null)
			{
				removeChild(_editButton);
				_editButton = null;
			}
		}
//		invalidateSize();			
	}

	// ---- defined by subclasses ----------------------------------------------------------------------------------
	
	protected function commitEditedValue():Boolean
	{
	    return false;
	}

	// ---- layout and measurement ----------------------------------------------------------------------------------
	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	{
		var gap:Number = (_editButton == null)? 0:4;
		var iconWidth:Number = (_editButton == null)? 0:_editButton.measuredWidth;
		var controlWidth:Number = unscaledWidth - iconWidth - gap;
		
		_nonEditableControl.setActualSize(controlWidth*_nonEditableControl.scaleX,unscaledHeight*_nonEditableControl.scaleY);
		_editableControl.setActualSize(controlWidth*_editableControl.scaleX,unscaledHeight*_editableControl.scaleY);
		
		_nonEditableControl.move(
		    iconWidth + gap,
		    unscaledHeight/2 - unscaledHeight*_nonEditableControl.scaleY/2);
		    
		_editableControl.move(
		    iconWidth + gap, 
		    unscaledHeight/2 - unscaledHeight*_editableControl.scaleY/2);
		
		if(_editButton != null)
		{
//            _editButton.move(_editButton.x, unscaledHeight / 2 - _editButton.measuredHeight / 2);
			_editButton.y = 0;
			_editButton.setActualSize(_editButton.measuredWidth,_editButton.measuredHeight);
		}

	}
	

	override protected function measure():void
	{

		var gap:Number = (_editButton == null)? 0:4;
		var iconWidth:Number = (_editButton == null)? 0:_editButton.measuredWidth;
		var iconHeight:Number = (_editButton == null)? 0:_editButton.measuredHeight;
		var controlWidth:Number = unscaledWidth - iconWidth - gap;

        if (_editable){
            
    		measuredWidth = gap + iconWidth + _editableControl.measuredWidth;
    		measuredHeight = Math.max(iconHeight, _editableControl.measuredHeight);
    		measuredMinWidth = gap + iconWidth + _editableControl.measuredMinWidth;
    		measuredMinHeight = Math.max(iconHeight, _editableControl.measuredMinHeight);
    		
        } else {
            
    		measuredWidth = gap + iconWidth + _nonEditableControl.measuredWidth;
    		measuredHeight = Math.max(iconHeight, _nonEditableControl.measuredHeight);
    		measuredMinWidth = gap + iconWidth + _nonEditableControl.measuredMinWidth;
    		measuredMinHeight = Math.max(iconHeight, _nonEditableControl.measuredMinHeight);
        }
	}	
	
	// --- utility function to facade events from the children 
	protected function facadeEvents(target:UIComponent,...events):void
	{
		for(var i:int = 0;i<events.length;i++)
		{
			target.addEventListener(events[i],redispatchHandler);
		}
	}
	protected function redispatchHandler(e:Event):void
	{
		dispatchEvent(e.clone());
	}
}
}