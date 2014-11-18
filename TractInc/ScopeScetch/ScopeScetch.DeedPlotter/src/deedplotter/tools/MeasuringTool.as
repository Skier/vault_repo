package src.deedplotter.tools
{
    import flash.events.Event;
    import flash.events.MouseEvent;
    import flash.geom.Point;
    
    import mx.controls.Menu;
    import mx.controls.PopUpMenuButton;
    import mx.core.UIComponent;
    import mx.managers.CursorManager;
    import mx.managers.PopUpManager;
    
    import src.deedplotter.components.MeasuringLine;
    import src.deedplotter.components.MeasuringLine2;
    import src.deedplotter.containers.GeoCanvas;
    import src.deedplotter.utils.GeoLine;
    import src.deedplotter.utils.GeoPosition;
    
    
    public class MeasuringTool extends AbstractTool
    {

        public function MeasuringTool(){
            super();

            Icon = m_toolIcon;
            Description = "Measuring";
        }

        [Bindable] [Embed(source="/assets/measuring_tool.png")]
        private var m_toolIcon:Class;

        [Bindable] [Embed(source="/assets/measuring_tool_cursor.png")]
        private var m_toolCursor:Class;

        private var cursorId:Number = Number.MIN_VALUE;

        private var activeLine:MeasuringLine;

        override public function Activate():void 
        {
            plotter.drawingSurface.addEventListener(MouseEvent.ROLL_OUT, drawingSurface_rollOutHandler);

            plotter.statusLabel.text = "Select Start Point";
            cursorId == Number.MIN_VALUE;
        }
        
        override public function Deactivate():void {
            plotter.drawingSurface.removeEventListener(MouseEvent.ROLL_OUT, drawingSurface_rollOutHandler);

            removeCursor();
        }

        override public function onPlotterMouseDown(event:MouseEvent):void 
        {
            var point:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            var position:GeoPosition = plotter.drawingSurface.GetGeoPosition(point);

            if (activeLine)
            {
		        if (activeLine.startPosition.Equals(activeLine.endPosition)){
		            plotter.drawingSurface.removeChild(activeLine);
		        }
                
                activeLine.iconBoxVisible = true;
                activeLine = null;
                
                plotter.statusLabel.text = "Select Start Point";
            } 
            else if (event.target == plotter.drawingSurface)
            {
                var measureLine:MeasuringLine;
                
                if (event.altKey) {
                    measureLine = new MeasuringLine2();
                } else {
                    measureLine = new MeasuringLine();
                }

                measureLine.startPosition = position;
                measureLine.endPosition = position.clone();
                measureLine.canvas = plotter.drawingSurface;
                measureLine.addEventListener(MouseEvent.ROLL_OVER, mesureLine_rollOverHandler);
                measureLine.addEventListener("deleteButtonClick", mesureLine_deleteClickHandler);
                measureLine.addEventListener("adjustButtonClick", mesureLine_adjustButtonClickHandler);
                measureLine.addEventListener("adjustReverseButtonClick", mesureLine_adjustReverseButtonClickHandler);

                plotter.drawingSurface.addChild(measureLine);

                activeLine = measureLine;

                plotter.statusLabel.text = "Select End Point";
            }
        }

        override public function onPlotterMouseMove(event:MouseEvent):void 
        {
            var point:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            var position:GeoPosition = plotter.drawingSurface.GetGeoPosition(point);

            if (CursorManager.currentCursorID != cursorId)
            {
                if (activeLine || event.target == plotter.drawingSurface)
                    cursorId = CursorManager.setCursor(m_toolCursor, 2, -22, -22);
            }

            if (activeLine) 
            {
                activeLine.endPosition = position;

                plotter.statusLabel.text = "Distance: " + 
                    GeoLine.calculateDistance(activeLine.startPosition, activeLine.endPosition);
            }
        }

        private function mesureLine_rollOverHandler(event:MouseEvent):void
        {
            if (activeLine)
                return;

            removeCursor();
        }

        private function mesureLine_deleteClickHandler(event:Event):void
        {
            var line:MeasuringLine = MeasuringLine(event.target);
            plotter.drawingSurface.removeChild(line);
        }

        private function mesureLine_adjustButtonClickHandler(event:Event):void
        {
            var startPos:GeoPosition;
            var endPos:GeoPosition;

            var line:MeasuringLine = MeasuringLine(event.target);

            if (plotter.activeTool != this) {
                plotter.activeTool = this;
            }
            
            if (line.startPosition.Easting > line.endPosition.Easting) {
                startPos = line.startPosition;
                endPos = line.endPosition;
            } else if (line.startPosition.Easting < line.endPosition.Easting) {
                startPos = line.endPosition
                endPos = line.startPosition;
            } else {
                if (line.startPosition.Northing > line.endPosition.Northing) {
                    startPos = line.startPosition;
                    endPos = line.endPosition;
                } else {
                    startPos = line.endPosition
                    endPos = line.startPosition;
                }
            }
            
            line.startPosition = startPos;
            line.endPosition = endPos;

            activeLine = line;
        }

        private function removeCursor():void
        {
            if (cursorId != Number.MIN_VALUE)
            {
                CursorManager.removeCursor(cursorId);
                cursorId = Number.MIN_VALUE;
            }
        }
        
        private function mesureLine_adjustReverseButtonClickHandler(event:Event):void
        {
            var startPos:GeoPosition;
            var endPos:GeoPosition;

            var line:MeasuringLine = MeasuringLine(event.target);

            if (plotter.activeTool != this) {
                plotter.activeTool = this;
            }
            
            if (line.startPosition.Easting < line.endPosition.Easting) {
                startPos = line.startPosition;
                endPos = line.endPosition;
            } else if (line.startPosition.Easting > line.endPosition.Easting) {
                startPos = line.endPosition
                endPos = line.startPosition;
            } else {
                if (line.startPosition.Northing < line.endPosition.Northing) {
                    startPos = line.startPosition;
                    endPos = line.endPosition;
                } else {
                    startPos = line.endPosition
                    endPos = line.startPosition;
                }
            }
            
            line.startPosition = startPos;
            line.endPosition = endPos;

            activeLine = line;
        }

        protected function drawingSurface_rollOutHandler(event:MouseEvent):void 
        {
            if (!isMouseOverCanvas())
            {
                removeCursor();
            } 
        }
    }
} 