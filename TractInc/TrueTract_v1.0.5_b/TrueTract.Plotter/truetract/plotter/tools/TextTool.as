package truetract.plotter.tools
{
    import flash.events.MouseEvent;
    import flash.geom.Point;
    
    import mx.controls.TextArea;
    import mx.core.UITextField;
    import mx.managers.CursorManager;
    
    import truetract.plotter.Plotter;
    import truetract.plotter.components.AutoResizingTextArea;
    import truetract.plotter.components.TractView;
    import truetract.plotter.components.tractViewClasses.TractTextObjectView;
    import truetract.plotter.utils.GeoPosition;
    import truetract.plotter.domain.TractTextObject;
    
    public class TextTool extends AbstractTool
    {
        
        [Embed(source="/assets/font.png")]
        private var m_toolIcon:Class;

        [Embed(source="/assets/text_tool_cursor.png")]
        private var textToolCursor:Class;

        private var cursorId:Number = Number.MIN_VALUE;
        
        private var tractView:TractView;
        
        private var activeTextView:TractTextObjectView;
        
        public function TextTool(){
            super();
            
            Icon = m_toolIcon;
            Description = "Create text objects";
        }
        
        public function SetActiveTextView(textView:TractTextObjectView):void {
            if (activeTextView) {
                activeTextView.SwitchToView();
            }
            
            activeTextView = textView;
            
            if (activeTextView) {
                activeTextView.SwitchToEditor(true);    
            }
        }
        
        override public function Activate():void {
            tractView = plotter.tractView;
            
            plotter.drawingSurface.addEventListener(MouseEvent.ROLL_OUT, drawingSurface_rollOutHandler);
            
            for each (var textView:TractTextObjectView in tractView.textViewList) {
                registerTextViewEventHandlers(textView);
            }
        }
        
        override public function Deactivate():void {
            plotter.drawingSurface.removeEventListener(MouseEvent.ROLL_OUT, drawingSurface_rollOutHandler);
            
            for each (var textView:TractTextObjectView in tractView.textViewList) {
                unRegisterTextViewEventHandlers(textView);
            }
            
            SetActiveTextView(null);
        }
        
        
        override public function onPlotterMouseDown(event:MouseEvent):void {
            removeCursor();
            
            if (activeTextView) {
                activeTextView.SwitchToView();
            }
            
            var point:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            var position:GeoPosition = plotter.drawingSurface.GetGeoPosition(point);

            var textObject:TractTextObject = new TractTextObject();
            textObject.TractId = plotter.Model.TractId; //TODO: WDM bug workarroung. remove once this will be fixed
            textObject.Position = position;
            
            var textView:TractTextObjectView = tractView.AddTextObject(textObject);

            registerTextViewEventHandlers(textView);
            
            SetActiveTextView(textView);
        }

        override public function onPlotterMouseMove(event:MouseEvent):void
        {
            plotter.statusLabel.text = "Select Start Point for text object insertion";

            if (CursorManager.currentCursorID != cursorId)
            {
                cursorId = CursorManager.setCursor(textToolCursor);
            }
        }

        private function registerTextViewEventHandlers(textView:TractTextObjectView):void
        {
            textView.addEventListener(MouseEvent.ROLL_OVER, textObject_rollOverHandler);
            textView.addEventListener(MouseEvent.ROLL_OUT, textObject_rollOutHandler);
            textView.addEventListener(MouseEvent.MOUSE_DOWN, textObject_mouseDownHandler);
        }

        private function unRegisterTextViewEventHandlers(textView:TractTextObjectView):void
        {
            textView.removeEventListener(MouseEvent.ROLL_OVER, textObject_rollOverHandler);
            textView.removeEventListener(MouseEvent.ROLL_OUT, textObject_rollOutHandler);
            textView.removeEventListener(MouseEvent.MOUSE_DOWN, textObject_mouseDownHandler);
        }
        
        private function removeCursor():void
        {
            CursorManager.removeCursor(cursorId);
            cursorId = Number.MIN_VALUE;
        }
        
        protected function drawingSurface_rollOutHandler(event:MouseEvent):void
        {
            removeCursor();
        }
        
        protected function textObject_rollOverHandler(event:MouseEvent):void
        {
            var target:TractTextObjectView = TractTextObjectView(event.target);
            
            if (target != activeTextView)
            {
                TractTextObjectView(event.target).SwitchToEditor();
            }
        }

        protected function textObject_rollOutHandler(event:MouseEvent):void
        {
            var target:TractTextObjectView = TractTextObjectView(event.target);
            
            if (target != activeTextView)
            {
                TractTextObjectView(event.target).SwitchToView();
            }
        }

        protected function textObject_mouseDownHandler(event:MouseEvent):void
        {
            event.preventDefault();
            event.stopImmediatePropagation();
            
            SetActiveTextView(TractTextObjectView(event.currentTarget));
        }
        
    }
}