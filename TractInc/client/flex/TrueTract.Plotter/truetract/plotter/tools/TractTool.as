package truetract.plotter.tools
{
    import flash.events.Event;
    import flash.events.MouseEvent;
    import flash.geom.Point;
    
    import mx.collections.ItemResponder;
    import mx.events.FlexEvent;
    import mx.events.ValidationResultEvent;
    
    import truetract.domain.*;
    import truetract.domain.callparams.*;
    import truetract.plotter.Plotter;
    import truetract.plotter.components.TractView;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.CallTypeSelector;
    import truetract.plotter.utils.*;
    import truetract.utils.*;
    
    public class TractTool extends AbstractTool
    {

        [Bindable]
        [Embed(source="/assets/tract_tool_16_16.png")]
        private var m_toolIcon:Class;

        private var tractView:TractView;

        public function TractTool()
        {
            super();
            
            Icon = m_toolIcon;
            Description = "Draw Tract By Mouse";
        }

        override public function Activate():void 
        {
            tractView = plotter.tractView;

            if (tractView.tract && !tractView.tract.IsClosed) {
                plotter.bearingTxt.setFocus();
            }
        }

        override public function Deactivate():void 
        {
            if (tractView)
            {
                tractView.removeTempLine();
            }
        }
        
        override public function onPlotterMouseMove(event:MouseEvent):void 
        {
            var point:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            var position:GeoPosition = plotter.drawingSurface.GetGeoPosition(point);
            
		    if (tractView.tempLine) 
		    {
                var line:GeoLine = GeoLine.createByEndPosition(
                    tractView.tempLine.Shape.startPosition, position);
                tractView.tempLine.Shape = line;

				plotter.statusLabel.text = line.bearing.toString() + ", DST:" + line.distance.toFixed(2) + "'";
		    } 
		    else 
		    {
		        plotter.statusLabel.text = position.toString();
		    }
		    
		    tractView.invalidateDisplayList();
        }
        
        override public function onPlotterMouseDown(event:MouseEvent):void 
        {
            if (!tractView.tempLine) return;
			
            var point:Point = new Point(plotter.drawingSurface.mouseX, plotter.drawingSurface.mouseY);
            var position:GeoPosition = plotter.drawingSurface.GetGeoPosition(point);
			
            var line:GeoLine = GeoLine.createByEndPosition(tractView.tempLine.Shape.startPosition, position);
            tractView.tempLine.Shape = line;
            
			if (tractView.tempLine.Shape.endPosition.Equals(tractView.tempLine.Shape.startPosition))
			{
			    return; //do not create new line if distance = 0
			}

            var call:TractCall = new TractCall();

            call.Params.addItem(new BearingParam(line.bearing));
            call.Params.addItem(new DistanceParam(line.distance, UOMUtil.getInstance().defaultUOM));
		    call.CallType = TractCall.CALL_TYPE_LINE;
            call.CreatedByMouse = 1;

            plotter.AddCall(call);
        }

    }
}