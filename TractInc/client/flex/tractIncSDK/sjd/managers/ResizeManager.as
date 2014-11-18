package sjd.managers{
	
	import flash.events.MouseEvent;
	import flash.geom.Point;
	import mx.core.Application;
	import mx.core.UIComponent;
	import mx.events.FlexEvent;
	import mx.containers.Panel;
	import sjd.utils.Constant;
	import sjd.utils.CursorUtils;
	import mx.controls.Button;
	
	/**
	 * @class ResizeManager
	 * @brief Enable any UIComponent to resize
	 * @author Jove
	 * @version 1.2
	 */
	public class ResizeManager{
		
		
		private static var resizeObj:UIComponent;
		private static var mouseState:Number = 0;
		
		public static var isResizing:Boolean = false;
		public static var mouseMargin:Number = 4;
		public static var defaultCursor:Class = null;
		public static var defaultCursorOffX:Number = 0;
		public static var defaultCursorOffY:Number = 0;
		
		public static function setDefaultCursor(cursor:Class = null, offX:Number = 0, offY:Number = 0):void{
			defaultCursor = cursor;
			defaultCursorOffX = offX;
			defaultCursorOffY = offY;
		}
		
		/**
		 * Enable the UIComponent to have resize capability.
		 * @param targetObj The UIComponent to be enabled resize capability
		 * @param minSize The min size of the UIComponent when resizing
		 */
		public static function enableResize(targetObj:UIComponent, minSize:Number):void{
			//Application.application.parent:SystemManager
			Application.application.parent.addEventListener(MouseEvent.MOUSE_UP, doMouseUp);
			Application.application.parent.addEventListener(MouseEvent.MOUSE_MOVE, doResize);
			
			initPosition(targetObj);
			
			targetObj.setStyle(Constant.RESIZE_OLD_POINT, new Point());
			targetObj.setStyle(Constant.RESIZE_MIN_SIZE, minSize);
			targetObj.setStyle(Constant.RESIZE_IS_POPUPE, targetObj.isPopUp);
			
			targetObj.addEventListener(MouseEvent.MOUSE_DOWN, doMouseDown);
			//targetObj.addEventListener(MouseEvent.MOUSE_UP, doMouseUp);
			targetObj.addEventListener(MouseEvent.MOUSE_MOVE, doMouseMove);
			targetObj.addEventListener(MouseEvent.MOUSE_OUT, doMouseOut);
			//targetObj.addEventListener(MouseEvent.MOUSE_OUT, doMouseOut, false, 10);
		}
		
		/**
		 * Disable the UIComponent to have resize capability.
		 * @param targetObj The UIComponent to be disabled resize capability
		 */
		public static function disableResize(targetObj:UIComponent):void{
			targetObj.removeEventListener(MouseEvent.MOUSE_DOWN, doMouseDown);
			//targetObj.removeEventListener(MouseEvent.MOUSE_UP, doMouseUp);
			targetObj.removeEventListener(MouseEvent.MOUSE_MOVE, doMouseMove);
			targetObj.removeEventListener(MouseEvent.MOUSE_OUT, doMouseOut);
		}
		
				
		private static function initPosition(obj:Object):void{
			obj.setStyle(Constant.RESIZE_OLD_HEIGHT, obj.height);
			obj.setStyle(Constant.RESIZE_OLD_WIDTH, obj.width);
			obj.setStyle(Constant.RESIZE_OLD_X, obj.x);
			obj.setStyle(Constant.RESIZE_OLD_Y, obj.y);
		}
		
		/**
		 * Set the first global point that mouse down on the resizeObj.
		 * @param The MouseEvent.MOUSE_DOWN
		 */
		private static function doMouseDown(event:MouseEvent):void{
			
			if(mouseState != Constant.SIDE_OTHER){
				
				resizeObj = UIComponent(event.currentTarget);
				
				initPosition(resizeObj);
				
				var point:Point = new Point();
				point.x = resizeObj.mouseX;
				point.y = resizeObj.mouseY;

				point = resizeObj.localToGlobal(point);
				resizeObj.setStyle(Constant.RESIZE_OLD_POINT, point);
			}
		}
		
		/**
		 * Clear the resizeObj and also set the latest position.
		 * @param The MouseEvent.MOUSE_UP
		 */
		private static function doMouseUp(event:MouseEvent):void{
			isResizing = false;
			if(resizeObj != null){
				initPosition(resizeObj);
			}
			resizeObj = null;
		}
		
		/**
		 * Show the mouse arrow when not draging.
		 * Call doResize(event) to resize resizeObj when mouse is inside the resizeObj area.
		 * @param The MouseEvent.MOUSE_MOVE
		 */
		private static function doMouseMove(event:MouseEvent):void{
			
			
			var thisObj:UIComponent = UIComponent(event.currentTarget);
			var point:Point = new Point();
				
			point = thisObj.localToGlobal(point);
			
			isResizing = true;
			
			if(resizeObj == null){
				var xPosition:Number = Application.application.parent.mouseX;
				var yPosition:Number = Application.application.parent.mouseY;
				if(xPosition >= (point.x + thisObj.width - mouseMargin) && yPosition >= (point.y + thisObj.height - mouseMargin)){
					CursorUtils.changeCursor(Constant.LEFT_OBLIQUE_SIZE, -6, -6); 
					mouseState = Constant.SIDE_RIGHT | Constant.SIDE_BOTTOM;
				}else if(xPosition <= (point.x + mouseMargin) && yPosition <= (point.y + mouseMargin)){
					CursorUtils.changeCursor(Constant.LEFT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_LEFT | Constant.SIDE_TOP;
				}else if(xPosition <= (point.x + mouseMargin) && yPosition >= (point.y + thisObj.height - mouseMargin)){
					CursorUtils.changeCursor(Constant.RIGHT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_LEFT | Constant.SIDE_BOTTOM;
				}else if(xPosition >= (point.x + thisObj.width - mouseMargin) && yPosition <= (point.y + mouseMargin)){
					CursorUtils.changeCursor(Constant.RIGHT_OBLIQUE_SIZE, -6, -6);
					mouseState = Constant.SIDE_RIGHT | Constant.SIDE_TOP;
				}else if(xPosition >= (point.x + thisObj.width - mouseMargin)){
					CursorUtils.changeCursor(Constant.HORIZONTAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_RIGHT;
				}else if(xPosition <= (point.x + mouseMargin)){
					CursorUtils.changeCursor(Constant.HORIZONTAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_LEFT;
				}else if(yPosition >= (point.y + thisObj.height - mouseMargin)){
					CursorUtils.changeCursor(Constant.VERTICAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_BOTTOM;
				}else if(yPosition <= (point.y + mouseMargin)){
					CursorUtils.changeCursor(Constant.VERTICAL_SIZE, -9, -9);
					mouseState = Constant.SIDE_TOP;
				}else{
					CursorUtils.changeCursor(defaultCursor, defaultCursorOffX, defaultCursorOffY);
					mouseState = Constant.SIDE_OTHER;
					isResizing = false;
				}
				
				if(thisObj.getStyle(Constant.RESIZE_IS_POPUPE)){
					//When cursor is move arrow, disable popup
					if(mouseState != Constant.SIDE_OTHER){
						thisObj.isPopUp = false;
					}else{
						thisObj.isPopUp = true;
					}
				}
			}
			
			//Use SystemManager to listen the mouse reize event, so we needn't handle the event at the current object.
			//doResize(event);
		}
		
		/**
		 * Hide the arrow when not draging and moving out the resizeObj.
		 * @param The MouseEvent.MOUSE_MOVE
		 */
		private static function doMouseOut(event:MouseEvent):void{
			if(resizeObj == null){
				isResizing = false;
				CursorUtils.changeCursor(defaultCursor, defaultCursorOffX, defaultCursorOffY);
				mouseState = Constant.SIDE_OTHER;
			}
		}
		
		/**
		 * Resize when the draging resizeObj, resizeObj is not null.
		 * @param The MouseEvent.MOUSE_MOVE
		 */
		private static function doResize(event:MouseEvent):void{
			
			if(resizeObj != null){
				
				var point:Point = Point(resizeObj.getStyle(Constant.RESIZE_OLD_POINT));
				
				var xPlus:Number = Application.application.parent.mouseX - point.x;
				var yPlus:Number = Application.application.parent.mouseY - point.y;
				
				var windowMinSize:Number = Number(resizeObj.getStyle(Constant.RESIZE_MIN_SIZE));
				
				var ow:Number = Number(resizeObj.getStyle(Constant.RESIZE_OLD_WIDTH));
				var oh:Number = Number(resizeObj.getStyle(Constant.RESIZE_OLD_HEIGHT));
				var oX:Number = Number(resizeObj.getStyle(Constant.RESIZE_OLD_X));
				var oY:Number = Number(resizeObj.getStyle(Constant.RESIZE_OLD_Y))
				
			    switch(mouseState){
			    	case Constant.SIDE_RIGHT | Constant.SIDE_BOTTOM:
			    		resizeObj.width = ow + xPlus > windowMinSize ? ow + xPlus : windowMinSize;
		    			resizeObj.height = oh + yPlus > windowMinSize ? oh + yPlus : windowMinSize;
			    		break;
			    	case Constant.SIDE_LEFT | Constant.SIDE_TOP:
			    		resizeObj.width = ow - xPlus > windowMinSize ? ow - xPlus : windowMinSize;
		    			resizeObj.height = oh - yPlus > windowMinSize ? oh - yPlus : windowMinSize;
		    			resizeObj.x = xPlus < ow - windowMinSize ? oX + xPlus: resizeObj.x;
		    			resizeObj.y = yPlus < oh - windowMinSize ? oY + yPlus : resizeObj.y;
			    		break;
			    	case Constant.SIDE_LEFT | Constant.SIDE_BOTTOM:
			    		resizeObj.width = ow - xPlus > windowMinSize ? ow - xPlus : windowMinSize;
		    			resizeObj.height = oh + yPlus > windowMinSize ? oh + yPlus : windowMinSize;
			    		resizeObj.x = xPlus < ow - windowMinSize ? oX + xPlus: resizeObj.x;
			    		break;
			    	case Constant.SIDE_RIGHT | Constant.SIDE_TOP:
			    		resizeObj.width = ow + xPlus > windowMinSize ? ow + xPlus : windowMinSize;
		    			resizeObj.height = oh - yPlus > windowMinSize ? oh - yPlus : windowMinSize;
			    		resizeObj.y = yPlus < oh - windowMinSize ? oY + yPlus : resizeObj.y;
			    		break;
			    	case Constant.SIDE_RIGHT:
			    		resizeObj.width = ow + xPlus > windowMinSize ? ow + xPlus : windowMinSize;
			    		break;
			    	case Constant.SIDE_LEFT:
			    		resizeObj.width = ow - xPlus > windowMinSize ? ow - xPlus : windowMinSize;
			    		resizeObj.x = xPlus < ow - windowMinSize ? oX + xPlus: resizeObj.x;
			    		break;
			    	case Constant.SIDE_BOTTOM:
			    		resizeObj.height = oh + yPlus > windowMinSize ? oh + yPlus : windowMinSize;
			    		break;
			    	case Constant.SIDE_TOP:
			    		resizeObj.height = oh - yPlus > windowMinSize ? oh - yPlus : windowMinSize;
			    		resizeObj.y = yPlus < oh - windowMinSize ? oY + yPlus : resizeObj.y;
			    		break;
			    }
			    
			}
			
		}
		
		
		
		
		
		
		
		
		
		
	
		
	}
	
}