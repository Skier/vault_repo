package truetract.plotter.containers
{
    import flash.display.DisplayObject;
    import flash.events.Event;
    import flash.events.KeyboardEvent;
    import flash.events.MouseEvent;
    import flash.ui.Keyboard;
    
    import mx.containers.Canvas;
    import mx.core.Application;
    import mx.core.EventPriority;
    import mx.core.UIComponent;
    import mx.managers.CursorManager;
    import truetract.plotter.events.DragEvent;
    import mx.managers.CursorManagerPriority;

    [Event(name="dragged", type="truetract.plotter.events.DragEvent")]
    
    public class DraggableCanvas extends Canvas
    {
        public static const PRIMARY_MOUSE_BUTTON:String = "primaryButton";
        public static const MIDDLE_MOUSE_BUTTON:String = "middleButton";

        [Bindable]
        [Embed(source="/assets/pan_tool_icon.png")]
        private var openHandCursor:Class;

        [Bindable]
        [Embed(source="/assets/pan_tool_icon2.png")]
        private var closedHandCursor:Class;

        private var dragCursorStartX: Number;
        private var dragCursorStartY: Number;
        private var backgroundDragInProgress: Boolean = false;

        private var app:Application;
        
        private var cursorId:Number = Number.MIN_VALUE;
        
        public function DraggableCanvas() {
            app = Application(Application.application);

            horizontalScrollPolicy = "off";
            verticalScrollPolicy = "off";
        }

        override protected function createChildren():void {
            super.createChildren();

            addEventListener(MouseEvent.ROLL_OVER, rollOverHandler);
            addEventListener(MouseEvent.ROLL_OUT, rollOutHandler);
        }

        private var _mouseButton:String = PRIMARY_MOUSE_BUTTON;

        public function set mouseButton(value:String):void {
            app.removeEventListener(MouseEvent.MOUSE_DOWN, app_mouseDownHandler);
            this.removeEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler);
            
            if (value == PRIMARY_MOUSE_BUTTON) {
                addEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler, false, EventPriority.CURSOR_MANAGEMENT);
            } else if (value == MIDDLE_MOUSE_BUTTON) {
                app.addEventListener(MouseEvent.MOUSE_DOWN, app_mouseDownHandler);
            } else {
                throw new Error("Unsupported mouse button type");
            }

            _mouseButton = value;
        }

        public function get mouseButton():String {
            return _mouseButton;
        }

        private var _ctrlKeyRequired:Boolean = false;
        
        public function set ctrlKeyRequired(value:Boolean):void {
            removeCursor();

            app.systemManager.removeEventListener(KeyboardEvent.KEY_DOWN, app_keyDownHandler);
            app.systemManager.removeEventListener(KeyboardEvent.KEY_UP, app_keyUpHandler);

            _ctrlKeyRequired = value;

            if ( value ) {
                app.systemManager.addEventListener(KeyboardEvent.KEY_DOWN, app_keyDownHandler);
                app.systemManager.addEventListener(KeyboardEvent.KEY_UP, app_keyUpHandler);
            }
        }
        
        public function get ctrlKeyRequired():Boolean {
            return _ctrlKeyRequired;
        }
        
        public function scroll(deltaX:Number, deltaY:Number):void {

            for (var i:int = 0; i < numChildren; i++) {
                var child:DisplayObject = getChildAt(i);
                child.x += deltaX;
                child.y += deltaY;
            }

            invalidateDisplayList();

            dispatchEvent(new DragEvent(DragEvent.DRAGGED, deltaX, deltaY));
        }
        
        private function dragBegin(event: MouseEvent):void {
            event.stopPropagation();
            event.stopImmediatePropagation();
            event.preventDefault();
            
            backgroundDragInProgress = true;

            dragCursorStartX = event.stageX;
            dragCursorStartY = event.stageY;
            
            if (mouseButton == PRIMARY_MOUSE_BUTTON) {
                systemManager.addEventListener(MouseEvent.MOUSE_UP, systemManager_mouseUpHandler, true);
                systemManager.addEventListener(MouseEvent.MOUSE_MOVE, systemManager_mouseMoveHandler, true);
            } else {
                app.addEventListener(MouseEvent.MOUSE_UP, app_mouseUpHandler);
            }

            showClosedHandCursor();
        }
    
        private function dragStop():void {
	        backgroundDragInProgress = false;

            systemManager.removeEventListener(MouseEvent.MOUSE_UP, systemManager_mouseUpHandler, true);
            systemManager.removeEventListener(MouseEvent.MOUSE_MOVE, systemManager_mouseMoveHandler, true);
            app.removeEventListener(MouseEvent.MOUSE_UP, app_mouseUpHandler);

            removeCursor();
        }
            
        private function dragContinue(event: MouseEvent):void  {
            var deltaX: Number = (event.stageX / scaleX) - (dragCursorStartX / scaleX);
            var deltaY: Number = (event.stageY / scaleY) - (dragCursorStartY / scaleY);
            
            dragCursorStartX = event.stageX;
            dragCursorStartY = event.stageY;

            scroll(deltaX, deltaY);
        }

        private function removeCursor():void {
            CursorManager.removeCursor(cursorId);
            cursorId = Number.MIN_VALUE;
        }
        
        private function showOpenHandCursor():void {
            removeCursor();
            cursorId = CursorManager.setCursor(openHandCursor, CursorManagerPriority.HIGH);
        }

        private function showClosedHandCursor():void {
            removeCursor();
            cursorId = CursorManager.setCursor(closedHandCursor);
        }

		protected function mouseDownHandler(event:MouseEvent):void {
		    if (_ctrlKeyRequired && !event.ctrlKey) {
		        return;
		    }

		    dragBegin(event);
		}

		protected function systemManager_mouseUpHandler(event:MouseEvent):void {
		    if (backgroundDragInProgress){
		        dragStop();
		    }
		    
		    systemManager_mouseMoveHandler(event);
		}

		protected function systemManager_mouseMoveHandler(event:MouseEvent):void {

            if (backgroundDragInProgress){
                dragContinue(event);
                return;
            }

		    if (!ctrlKeyRequired || event.ctrlKey) {
		        showOpenHandCursor();
		    } else {
		        removeCursor();
		    }
		    
		}

		protected function rollOverHandler(event:MouseEvent):void {
		    if (!ctrlKeyRequired || event.ctrlKey) {
		        showOpenHandCursor();
		    }
		}

		protected function rollOutHandler(event:MouseEvent):void {
		    if (backgroundDragInProgress) {
		        dragStop();
		    }

		    removeCursor();
		}
		
        protected function app_mouseDownHandler(event:MouseEvent):void {
            if (!event.altKey) {
                return;
            }

		    if (_ctrlKeyRequired && !event.ctrlKey) {
		        return;
		    }

            if (mouseX > 0 && mouseX < width && mouseY > 0 && mouseY < height ) 
            {
               dragBegin(event);
            }
        }

        protected function app_mouseUpHandler(event:MouseEvent):void {
 		    if (backgroundDragInProgress){
		        dragStop();
	        }
        }
        
        protected function app_keyDownHandler(event:KeyboardEvent):void {
            if (backgroundDragInProgress) {
                return;
            }

            if (event.ctrlKey) {
                showOpenHandCursor();
            }
        }

        protected function app_keyUpHandler(event:KeyboardEvent):void {
            if (backgroundDragInProgress) {
                dragStop();
            } else if (event.keyCode == Keyboard.CONTROL) {
                removeCursor();
            }
        }
        
    }
}