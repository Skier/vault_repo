package truetract.plotter.components.tractViewClasses.call
{
    
    import flash.geom.Point;
    
    import truetract.utils.*;
    import truetract.plotter.utils.GraphicsUtil;

    public class CallCurveView extends CallView
    {

        public function CallCurveView(shape:IGeoShape) 
        {
            super(shape);
        }

        override protected function createChildren():void {
            super.createChildren();
            
            upperLabel.setStyle("text-align", "left");
            lowerLabel.setStyle("text-align", "left");
        }
        
  		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	    {
	        super.updateDisplayList(unscaledWidth, unscaledHeight);
	        
	        if (parent == null) return;
	        
            var color:uint = (rollPhase == "over") ? getStyle("lineRollOverColor") : getStyle("lineColor") ;

            var curve:GeoCurve = GeoCurve(shape);
            
            var pointsPerFeet:Number = RelatedTractView.surfaceScale.PointsInOneFeet;
            
            var startPoint:Point = new Point(0,0);
            var endPoint:Point = new Point();
            
			drawingShape.graphics.clear();

            //draw transparent hit area
			drawingShape.graphics.lineStyle(4, 0, 0);
            drawingShape.graphics.moveTo(startPoint.x, startPoint.y);
            
            GraphicsUtil.drawArcEx(drawingShape.graphics, startPoint, curve.tangentIn.Azimuth, 
                curve.radius * pointsPerFeet, curve.delta, curve.direction == GeoCurve.RIGHT);
			
            //draw curve
            drawingShape.graphics.lineStyle(0, color, 1);
            drawingShape.graphics.moveTo(startPoint.x, startPoint.y);
            
            endPoint = GraphicsUtil.drawArcEx(drawingShape.graphics, startPoint, curve.tangentIn.Azimuth, 
                curve.radius * pointsPerFeet, curve.delta, curve.direction == GeoCurve.RIGHT);
			
            drawingShape.graphics.endFill();

        //---- annotation labels positioning

            upperLabel.text = callModel.AnnotationId;
            upperLabel.width = upperLabel.textWidth + 10;

            var upperLabelIndentAngle:Number = curve.chord.bearing.Azimuth - 90;
            if (upperLabelIndentAngle < 0) upperLabelIndentAngle += 360;
            upperLabelIndentAngle *= (Math.PI/180);

            var lowerLabelIndentAngle:Number = curve.chord.bearing.Azimuth + 90;
            lowerLabelIndentAngle *= (Math.PI/180);

            var labelRotation:Number = curve.chord.bearing.Azimuth < 180 
                ? curve.chord.bearing.Azimuth - 90 
                : curve.chord.bearing.Azimuth + 90;
            
            upperLabel.rotation = lowerLabel.rotation = labelRotation;
            
            var arcCenterPosition:GeoPosition = curve.getArcMiddlePosition();

            var arcCenterPoint:Point = new Point();
            arcCenterPoint.x = startPoint.x + ((arcCenterPosition.Easting * pointsPerFeet) 
                - (curve.startPosition.Easting * pointsPerFeet));
            arcCenterPoint.y = startPoint.y - ((arcCenterPosition.Northing * pointsPerFeet) 
                - (curve.startPosition.Northing * pointsPerFeet));

            var halfTextWidth:Number = upperLabel.textWidth / 2;
            
            var labelStartPoint:Point = new Point (
                arcCenterPoint.x - (halfTextWidth * Math.sin(curve.chord.bearing.Radian)),
                arcCenterPoint.y + (halfTextWidth * Math.cos(curve.chord.bearing.Radian)));

            var labelEndPoint:Point = new Point (
                arcCenterPoint.x + (halfTextWidth * Math.sin(curve.chord.bearing.Radian)),
                arcCenterPoint.y - (halfTextWidth * Math.cos(curve.chord.bearing.Radian)));
            
            var upperLabelTextHeight:Number = upperLabel.textHeight;
            
           if (curve.chord.bearing.Azimuth > 180){
                upperLabel.x = labelEndPoint.x + ((labelIndent + upperLabelTextHeight) * Math.sin(lowerLabelIndentAngle));
                upperLabel.y = labelEndPoint.y - ((labelIndent + upperLabelTextHeight) * Math.cos(lowerLabelIndentAngle));
                lowerLabel.x = labelEndPoint.x + (labelIndent * Math.sin(upperLabelIndentAngle));
                lowerLabel.y = labelEndPoint.y - (labelIndent * Math.cos(upperLabelIndentAngle));
            } else {
                upperLabel.x = labelStartPoint.x + ((labelIndent + upperLabelTextHeight) * Math.sin(upperLabelIndentAngle));
                upperLabel.y = labelStartPoint.y - ((labelIndent + upperLabelTextHeight) * Math.cos(upperLabelIndentAngle));
                lowerLabel.x = labelStartPoint.x + (labelIndent * Math.sin(lowerLabelIndentAngle));
                lowerLabel.y = labelStartPoint.y - (labelIndent * Math.cos(lowerLabelIndentAngle));
            }
            
            var curveBounds:BoundRectangle = curve.getBounds();
            var curveArea:Number = (curveBounds.height * pointsPerFeet) * 
                                   (curveBounds.width * pointsPerFeet);

            //it is no good. hardcoded for now
            if ( curveArea < 500 ) {
                upperLabel.visible = false;
            } else {
                upperLabel.visible = true;
            }
        // ------ end labels positions
//            drawCurvePoints();
        }
        
        private function drawCurvePoints():void 
        {
            var pointsPerFeet:Number = RelatedTractView.surfaceScale.PointsInOneFeet;

            var points:Array = convertGeoPointsToLocal(GeoCurve(shape).getPoints());

            for (var i:int = 0; i < points.length - 1; i++) 
            {
                var startPoint:Point = points[i] as Point;
                var endPoint:Point = points[i+1] as Point;

                drawingShape.graphics.lineStyle(0, 0xFF00FF + (1000 * i) );
                drawingShape.graphics.moveTo(startPoint.x, startPoint.y);
                drawingShape.graphics.lineTo(endPoint.x, endPoint.y);
                
                drawingShape.graphics.lineStyle(0, 0xFF00FF );
                drawingShape.graphics.drawRect(startPoint.x - 2, startPoint.y - 2, 4, 4);
            }
        }
        
        //Convert Curve GeoPoints to local coordinates of CallCurveView
        private function convertGeoPointsToLocal(points:Array):Array {
            if (points.length == 0)
                return [];
        
            var pointsPerFeet:Number = RelatedTractView.surfaceScale.PointsInOneFeet;    
            var result:Array = [];
            
            var startPoint:Point = points[0] as Point;

            for each (var point:Point in points) {
                var localPoint:Point = new Point();
                localPoint.x = (point.x - startPoint.x) * pointsPerFeet;
                localPoint.y = - (point.y - startPoint.y) * pointsPerFeet;
                
                result.push(localPoint);
            }
            
            return result;
        }
        
    }
}