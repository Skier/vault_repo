package truetract.plotter.tools
{
    import flash.display.DisplayObject;
    import flash.display.Graphics;
    import flash.events.MouseEvent;
    import flash.geom.Point;
    
    import mx.core.UIComponent;
    import mx.managers.CursorManager;
    import mx.managers.PopUpManager;
    
    import truetract.plotter.Plotter;
    import truetract.plotter.components.ObjectControlFrame;
    import truetract.plotter.components.TractView;
    import truetract.plotter.components.tractViewClasses.TractTextObjectView;
    import truetract.plotter.containers.GeoCanvas;
    import truetract.plotter.utils.GeoPosition;
    
    public class ZoomInTool extends AbstractTool
    {
        
        [Bindable]
        [Embed(source="/assets/zoom_in.png")]
        private var m_toolIcon:Class;
        
        private var tractView:TractView;

        private var cursorId:Number = Number.MIN_VALUE;

        private var drawInProgress:Boolean = false;
        private var zoomX:Number;
        private var zoomY:Number;

        private var zoomProxy:UIComponent;

        public function ZoomInTool(){
            super();

            Icon = m_toolIcon;
            Description = "Zoom In";
            zoomProxy = new UIComponent();
        }
        
        override public function Activate():void 
        {
            plotter.drawingSurface.addEventListener(MouseEvent.ROLL_OUT, drawingSurface_rollOutHandler);

            zoomProxy.addEventListener(MouseEvent.MOUSE_MOVE, onPlotterMouseMove);
            zoomProxy.addEventListener(MouseEvent.MOUSE_UP, onPlotterMouseUp);

            tractView = plotter.tractView;

            PopUpManager.addPopUp(zoomProxy, plotter.drawingSurface);
            plotter.statusLabel.text = "Select region to Zoom In";
            cursorId == Number.MIN_VALUE;
        }
        
        override public function Deactivate():void {
            plotter.drawingSurface.removeEventListener(MouseEvent.ROLL_OUT, drawingSurface_rollOutHandler);

            zoomProxy.removeEventListener(MouseEvent.MOUSE_MOVE, onPlotterMouseMove);
            zoomProxy.removeEventListener(MouseEvent.MOUSE_UP, onPlotterMouseUp);

            PopUpManager.removePopUp(zoomProxy);
            CursorManager.removeCursor(cursorId);
        }

        override public function onPlotterMouseDown(event:MouseEvent):void 
        {
            zoomX = event.stageX;
            zoomY = event.stageY;

            drawInProgress = true;
        }

        override public function onPlotterMouseMove(event:MouseEvent):void 
        {
            if (CursorManager.currentCursorID != cursorId)
            {
                cursorId = CursorManager.setCursor(m_toolIcon);
            }

            if (drawInProgress) 
            {
                zoomProxy.graphics.clear();

				var pt:Point = new Point(event.stageX, event.stageY);

				var g:Graphics = zoomProxy.graphics;
				g.clear();
				g.lineStyle(1, 0xb7c6fa, 1);
				g.beginFill(0xadc6f0, 0.5);
				g.drawRect(zoomX, zoomY, pt.x - zoomX, pt.y - zoomY);
            }
        }

        override public function onPlotterMouseUp(event:MouseEvent):void 
        {
		    if (drawInProgress)
		    {

		        drawInProgress = false;
		        zoomProxy.graphics.clear();

		        var canvas:GeoCanvas = plotter.drawingSurface;

                var startPoint:Point = canvas.globalToLocal(new Point(zoomX, zoomY));
                var endPoint:Point = new Point(canvas.mouseX, canvas.mouseY);

                var topLeft:Point = new Point();
                topLeft.x = Math.min(startPoint.x, endPoint.x);
                topLeft.y = Math.min(startPoint.y, endPoint.y)

                var bottomRight:Point = new Point();
                bottomRight.x = Math.max(startPoint.x, endPoint.x);
                bottomRight.y = Math.max(startPoint.y, endPoint.y)

                var topLeftGeoPos:GeoPosition = canvas.GetGeoPosition(topLeft);
                var bottomRightGeoPos:GeoPosition = canvas.GetGeoPosition(bottomRight);

                var geoWidth:Number = bottomRightGeoPos.Easting - topLeftGeoPos.Easting;
                var geoHeight:Number = topLeftGeoPos.Northing - bottomRightGeoPos.Northing;

                if (geoWidth == 0 || geoHeight == 0) {
                    return;
                }

                var zoomCenterPosition:GeoPosition = new GeoPosition(
                    topLeftGeoPos.Easting + (geoWidth / 2) ,
                    topLeftGeoPos.Northing - (geoHeight / 2));

                canvas.Scale.PointsInOneFeet = Math.min(canvas.width / geoWidth, canvas.height / geoHeight);
                canvas.Scale = canvas.Scale;
                
                var zoomCenterAfterScaling:Point = canvas.GetLocalPosition(zoomCenterPosition);

                var deltaX:Number = (canvas.width / 2) - zoomCenterAfterScaling.x;
                var deltaY:Number = (canvas.height / 2) - zoomCenterAfterScaling.y;

                canvas.scroll(deltaX, deltaY);
		    }
        }
        
        protected function drawingSurface_rollOutHandler(event:MouseEvent):void 
        {
            if (!isMouseOverCanvas())
            {
                CursorManager.removeCursor(cursorId);
                cursorId = Number.MIN_VALUE;

                if (drawInProgress)
                {
    		        drawInProgress = false;
    		        zoomProxy.graphics.clear();
                }
                
            } 
        }

    }
} 