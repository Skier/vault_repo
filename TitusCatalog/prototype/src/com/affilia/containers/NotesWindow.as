package com.affilia.containers 
{
	
	import com.affilia.util.Constant;
	import com.affilia.util.CursorUtils;
	
	import flash.events.MouseEvent;
	import flash.geom.Point;
	
	import mx.containers.TitleWindow;
	import mx.controls.Button;
	import mx.controls.TextArea;
	import mx.core.Application;
	import mx.events.FlexEvent;
	
	[Event(name="closeWindow", type="mx.events.FlexEvent")]
			
	public class NotesWindow extends TitleWindow{
		
		private static var resizeObj:Object;
		private static var mouseState:Number = 0;
		private static var mouseMargin:Number = 10;
		
		private var oWidth:Number = 0;
		private var oHeight:Number = 0;
		private var oX:Number = 0;
		private var oY:Number = 0;
		private var oPoint:Point = new Point();
		
		private var _showWindowButtons:Boolean = false;
		private var _windowMinSize:Number = 50;
		
		private var _text:String;
		public function get text():String 
		{
			return this.content.text;
		}
		public function set text(value:String):void 
		{
			this.content.text = value;
		}
		
		private var content:TextArea;
		
		public function NotesWindow(){
			super();
			initPosition(this);
			
			this.addEventListener(MouseEvent.MOUSE_MOVE, oMouseMove);
			this.addEventListener(MouseEvent.MOUSE_OUT, oMouseOut);
			this.addEventListener(MouseEvent.MOUSE_DOWN, oMouseDown);
			this.addEventListener(FlexEvent.CREATION_COMPLETE, addButton);
			
			Application.application.parent.addEventListener(MouseEvent.MOUSE_UP, oMouseUp);
			Application.application.parent.addEventListener(MouseEvent.MOUSE_MOVE, oResize);
			
			addContent();
		}
		
		public function set showWindowButtons(show:Boolean):void{
			_showWindowButtons = show;
			if(titleBar != null){
				addButton(new FlexEvent(""));
			}
		}
		
		public function get showWindowButtons():Boolean{
			return _showWindowButtons;
		}
		
		public function set windowMinSize(size:Number):void{
			if(size > 0){
				_windowMinSize = size;
			}
		}
		
		public function get windowMinSize():Number{
			return _windowMinSize;
		}
		
		private function addContent():void 
		{
			content = new TextArea();
			content.percentHeight = 100;
			content.percentWidth = 100;
			
			this.addChild(content);
		}
		
		private static function initPosition(obj:Object):void{
			obj.oHeight = obj.height;
			obj.oWidth = obj.width;
			obj.oX = obj.x;
			obj.oY = obj.y;
		}
		
		private function oMouseDown(event:MouseEvent):void{
			if(mouseState != Constant.SIDE_OTHER){
				resizeObj = event.currentTarget;
				initPosition(resizeObj);
				oPoint.x = resizeObj.mouseX;
				oPoint.y = resizeObj.mouseY;
				oPoint = this.localToGlobal(oPoint);
			}
		}
		
		private function oMouseUp(event:MouseEvent):void{
			if(resizeObj != null){
				initPosition(resizeObj);
			}
			resizeObj = null;
		}
		
		private function oMouseMove(event:MouseEvent):void{
			if(resizeObj == null){
				var xPosition:Number = Application.application.parent.mouseX;
				var yPosition:Number = Application.application.parent.mouseY;
				if(xPosition >= (this.x + this.width - mouseMargin) && yPosition >= (this.y + this.height - mouseMargin)){
					CursorUtils.changeCursor(Constant.LEFT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_RIGHT | Constant.SIDE_BOTTOM;
				}else if(xPosition <= (this.x + mouseMargin) && yPosition <= (this.y + mouseMargin)){
					CursorUtils.changeCursor(Constant.LEFT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_LEFT | Constant.SIDE_TOP;
				}else if(xPosition <= (this.x + mouseMargin) && yPosition >= (this.y + this.height - mouseMargin)){
					CursorUtils.changeCursor(Constant.RIGHT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_LEFT | Constant.SIDE_BOTTOM;
				}else if(xPosition >= (this.x + this.width - mouseMargin) && yPosition <= (this.y + mouseMargin)){
					CursorUtils.changeCursor(Constant.RIGHT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_RIGHT | Constant.SIDE_TOP;
				}else if(xPosition >= (this.x + this.width - mouseMargin)){
					CursorUtils.changeCursor(Constant.HORIZONTAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_RIGHT;	
				}else if(xPosition <= (this.x + mouseMargin)){
					CursorUtils.changeCursor(Constant.HORIZONTAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_LEFT;
				}else if(yPosition >= (this.y + this.height - mouseMargin)){
					CursorUtils.changeCursor(Constant.VERTICAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_BOTTOM;
				}else if(yPosition <= (this.y + mouseMargin)){
					CursorUtils.changeCursor(Constant.VERTICAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_TOP;
				}else{
					mouseState = Constant.SIDE_OTHER;
					CursorUtils.changeCursor(null, 0, 0);
				}
			}
		}
		
		private function oMouseOut(event:MouseEvent):void{
			if(resizeObj == null){
				CursorUtils.changeCursor(null, 0, 0);
			}
		}
		
		private function oResize(event:MouseEvent):void{
			if(resizeObj != null){	
				resizeObj.stopDragging();
				var xPlus:Number = Application.application.parent.mouseX - resizeObj.oPoint.x;
				var yPlus:Number = Application.application.parent.mouseY - resizeObj.oPoint.y;
			    switch(mouseState){
			    	case Constant.SIDE_RIGHT | Constant.SIDE_BOTTOM:
			    		resizeObj.width = resizeObj.oWidth + xPlus > _windowMinSize ? resizeObj.oWidth + xPlus : _windowMinSize;
		    			resizeObj.height = resizeObj.oHeight + yPlus > _windowMinSize ? resizeObj.oHeight + yPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_LEFT | Constant.SIDE_TOP:
		    			resizeObj.x = xPlus < resizeObj.oWidth - _windowMinSize ? resizeObj.oX + xPlus: resizeObj.x;
		    			resizeObj.y = yPlus < resizeObj.oHeight - _windowMinSize ? resizeObj.oY + yPlus : resizeObj.y;
			    		resizeObj.width = resizeObj.oWidth - xPlus > _windowMinSize ? resizeObj.oWidth - xPlus : _windowMinSize;
		    			resizeObj.height = resizeObj.oHeight - yPlus > _windowMinSize ? resizeObj.oHeight - yPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_LEFT | Constant.SIDE_BOTTOM:
			    		resizeObj.x = xPlus < resizeObj.oWidth - _windowMinSize ? resizeObj.oX + xPlus: resizeObj.x;
			    		resizeObj.width = resizeObj.oWidth - xPlus > _windowMinSize ? resizeObj.oWidth - xPlus : _windowMinSize;
		    			resizeObj.height = resizeObj.oHeight + yPlus > _windowMinSize ? resizeObj.oHeight + yPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_RIGHT | Constant.SIDE_TOP:
			    		resizeObj.y = yPlus < resizeObj.oHeight - _windowMinSize ? resizeObj.oY + yPlus : resizeObj.y;
			    		resizeObj.width = resizeObj.oWidth + xPlus > _windowMinSize ? resizeObj.oWidth + xPlus : _windowMinSize;
		    			resizeObj.height = resizeObj.oHeight - yPlus > _windowMinSize ? resizeObj.oHeight - yPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_RIGHT:
			    		resizeObj.width = resizeObj.oWidth + xPlus > _windowMinSize ? resizeObj.oWidth + xPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_LEFT:
			    		resizeObj.x = xPlus < resizeObj.oWidth - _windowMinSize ? resizeObj.oX + xPlus: resizeObj.x;
			    		resizeObj.width = resizeObj.oWidth - xPlus > _windowMinSize ? resizeObj.oWidth - xPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_BOTTOM:
			    		resizeObj.height = resizeObj.oHeight + yPlus > _windowMinSize ? resizeObj.oHeight + yPlus : _windowMinSize;
			    		break;
			    	case Constant.SIDE_TOP:
			    		resizeObj.y = yPlus < resizeObj.oHeight - _windowMinSize ? resizeObj.oY + yPlus : resizeObj.y;
			    		resizeObj.height = resizeObj.oHeight - yPlus > _windowMinSize ? resizeObj.oHeight - yPlus : _windowMinSize;
			    		break;
			    }
			}
		}
		
		
		private var windowCloseButton:Button;
		
		private function addButton(event:FlexEvent):void{
			if(_showWindowButtons){
				if(windowCloseButton == null){
					windowCloseButton = new Button();
					windowCloseButton.width=10;
					windowCloseButton.height=10;
					windowCloseButton.focusEnabled=false;
					windowCloseButton.setStyle("upSkin", Constant.WINDOW_CLOSE_BUTTON_1);
					windowCloseButton.setStyle("overSkin", Constant.WINDOW_CLOSE_BUTTON_2);
					windowCloseButton.setStyle("downSkin", Constant.WINDOW_CLOSE_BUTTON_2);
					windowCloseButton.addEventListener(MouseEvent.CLICK, windowCloseButton_clickHandler);
					titleBar.addChild(windowCloseButton);
				}
				layoutWindowButtons();
			}else{
				if(windowCloseButton != null){ 
					titleBar.removeChild(windowCloseButton);
					windowCloseButton = null;
				}
			}
		}
		
		private function windowCloseButton_clickHandler(event:MouseEvent):void{
			dispatchEvent(new FlexEvent("closeWindow"));
		}
		
		private function layoutWindowButtons():void{
			if(windowCloseButton != null){
				windowCloseButton.move(titleBar.width - 10 - 6, (titleBar.height - 10) / 2);
			}
		}
		
		override protected function layoutChrome(unscaledWidth:Number, unscaledHeight:Number):void{
			super.layoutChrome(unscaledWidth, unscaledHeight);
			layoutWindowButtons();
		}
	}
}