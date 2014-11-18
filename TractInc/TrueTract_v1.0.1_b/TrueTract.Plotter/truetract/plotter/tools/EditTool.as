package truetract.plotter.tools
{
    import flash.display.DisplayObject;
    import flash.events.MouseEvent;
    import flash.geom.Point;
    
    import mx.core.UIComponent;
    import mx.managers.CursorManager;
    
    import truetract.plotter.Plotter;
    import truetract.plotter.components.ObjectControlFrame;
    import truetract.plotter.components.TractView;
    import truetract.plotter.components.tractViewClasses.TractTextObjectView;
    import truetract.plotter.utils.GeoPosition;
    
    public class EditTool extends AbstractTool
    {
        
        [Bindable]
        [Embed(source="/assets/edit_tool_16_16.png")]
        private var m_toolIcon:Class;

        [Embed(source="/assets/edit_tool_cursor.png")]
        private var draggingCursor:Class;

        [Embed(source="/assets/rotate_cursor.png")]
        private var rotationCursor:Class;
        
        private var tractView:TractView;

        private var editObject:UIComponent;

        private var controlFrame:ObjectControlFrame;
        
        private var dragCursorStartX: int;
        private var dragCursorStartY: int;
        
        private var objectDragInProgress: Boolean = false;
        private var objectRotateInProgress:Boolean = false;

        private var cursorId:Number = Number.MIN_VALUE;
        
        public function EditTool(){
            super();
            
            Icon = m_toolIcon;
            Description = "Edit Tract Calls, TextObjects ";

            controlFrame = new ObjectControlFrame()
            controlFrame.addEventListener("rotationAreaMouseDown", rotationArea_mouseDownHandler);
            controlFrame.addEventListener("rotationAreaRollOver", rotationArea_rollOverHandler);
            controlFrame.addEventListener("rotationAreaRollOut", rotationArea_rollOutHandler);
        }
        
        override public function Activate():void {
            tractView = plotter.tractView;
            
            for each (var textView:TractTextObjectView in tractView.textViewList) {
                registerTextViewEventHandlers(textView);
            }
        }
        
        override public function Deactivate():void {
            for each (var textView:TractTextObjectView in tractView.textViewList) {
                unRegisterTextViewEventHandlers(textView);
            }
            
            SetEditObject(null);
        }
        
        private function registerTextViewEventHandlers(textView:TractTextObjectView):void {
            textView.addEventListener(MouseEvent.ROLL_OVER, textObject_rollOverHandler);
            textView.addEventListener(MouseEvent.ROLL_OUT, textObject_rollOutHandler);
            textView.addEventListener(MouseEvent.MOUSE_DOWN, textObject_mouseDownHandler);
        }

        private function unRegisterTextViewEventHandlers(textView:TractTextObjectView):void {
            textView.removeEventListener(MouseEvent.ROLL_OVER, textObject_rollOverHandler);
            textView.removeEventListener(MouseEvent.ROLL_OUT, textObject_rollOutHandler);
            textView.removeEventListener(MouseEvent.MOUSE_DOWN, textObject_mouseDownHandler);
        }
        
        override public function onPlotterMouseMove(event:MouseEvent):void {
            if (objectDragInProgress){
                objectDragContinue(event);
            } else if (objectRotateInProgress) {
                objectRotateContinue(event);
            }
        }
        
        override public function onPlotterMouseUp(event:MouseEvent):void {
		    if (objectDragInProgress){
		        objectDragInProgress = false;
		    } else if (objectRotateInProgress){
		        objectRotateInProgress = false;
		    }
		    
		    if (!editObject) {
		        return;
		    }
		    
		    if (editObject.mouseX < 0 || editObject.mouseX > editObject.width ||
		        editObject.mouseY < 0 || editObject.mouseY > editObject.height) 
		    {
	            removeCursor();
		    }
        }
        
        protected function textObject_rollOverHandler(event:MouseEvent):void {

            if (objectDragInProgress || objectRotateInProgress) {
                return;
            }

            if (CursorManager.currentCursorID != cursorId) {
                cursorId = CursorManager.setCursor(draggingCursor);
            }
        }

        protected function textObject_rollOutHandler(event:MouseEvent):void {
            if (!objectDragInProgress && !objectRotateInProgress)
                removeCursor();
        }

        protected function textObject_mouseDownHandler(event:MouseEvent):void {
            event.preventDefault();
            event.stopImmediatePropagation();

            SetEditObject(UIComponent(event.currentTarget));

            objectDragBegin(event);
        }
        
        protected function rotationArea_mouseDownHandler(event:Event):void {
            objectRotateInProgress = true;
        }

        protected function rotationArea_rollOverHandler(event:Event):void {
            if (!objectDragInProgress && !objectRotateInProgress) {
                removeCursor();
                cursorId = CursorManager.setCursor(rotationCursor);
            }
        }

        protected function rotationArea_rollOutHandler(event:Event):void {
            if (!objectDragInProgress && !objectRotateInProgress)
                removeCursor();
        }
        
        private function objectDragBegin(event:MouseEvent):void  {
            dragCursorStartX = event.stageX;
            dragCursorStartY = event.stageY;

            objectDragInProgress = true;
        }
    
        private function objectDragContinue(event: MouseEvent):void  {

            var point:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            var position:GeoPosition = plotter.drawingSurface.GetGeoPosition(point);

            var deltaX: int = (event.stageX - dragCursorStartX) / plotter.drawingSurface.scaleX;
            var deltaY: int = (event.stageY - dragCursorStartY) / plotter.drawingSurface.scaleY;
            
            dragCursorStartX = event.stageX;
            dragCursorStartY = event.stageY;
            
            editObject.x += deltaX;
            editObject.y += deltaY;
            
            if (editObject is TractTextObjectView) {
                var textView:TractTextObjectView = TractTextObjectView(editObject);
                textView.Model.Position = plotter.drawingSurface.GetGeoPosition(
                    new Point(tractView.x + textView.x, tractView.y + textView.y));
            }
            
            tractView.invalidateDisplayList();
        }

        private function objectRotateContinue(event:MouseEvent):void {
            var mousePoint:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            
            mousePoint = plotter.drawingSurface.localToGlobal(mousePoint);
            var rotationCenter:Point = controlFrame.localToGlobal(new Point(0,0));

			var h:Number = mousePoint.x - rotationCenter.x;
			var w:Number = rotationCenter.y - mousePoint.y;
			
			var distance:Number = Math.sqrt( (h * h) + (w * w) );
			
			var azimuth:Number = 0;
			
			if (distance > 0){
                azimuth = Math.acos( h / distance ) / (Math.PI/180);

                 if ( mousePoint.y < rotationCenter.y ){
                    azimuth = 360 - azimuth;
                }
			}
			
            controlFrame.parent.rotation = azimuth;
            
            if (controlFrame.parent is TractTextObjectView) {
                var textView:TractTextObjectView = TractTextObjectView(editObject);
                textView.Model.Rotation = azimuth;
            }
            
            tractView.invalidateDisplayList();
        }

        private function removeCursor():void {
            CursorManager.removeCursor(cursorId);
            cursorId = Number.MIN_VALUE;
        }

        private function SetEditObject (value:UIComponent):void {
            
            editObject = value;
            
            if (controlFrame.parent) {
                controlFrame.parent.removeChild(controlFrame);
            }
            
            if (editObject) {
                editObject.addChild(controlFrame);
            }
        }

    }
} 