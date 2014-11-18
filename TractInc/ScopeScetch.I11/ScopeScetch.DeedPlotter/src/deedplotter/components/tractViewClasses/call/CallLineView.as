package src.deedplotter.components.tractViewClasses.call
{
    
    import flash.geom.Point;
    
    import src.deedplotter.utils.*;

    public class CallLineView extends CallView
    {

        public function CallLineView(shape:IGeoShape) 
        {
            super(shape);
        }

  		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	    {
	        super.updateDisplayList(unscaledWidth, unscaledHeight);
	        
	        if (parent == null) return;
	        
            var color:uint = (rollPhase == "over") ? getStyle("lineRollOverColor") : getStyle("lineColor") ;

            var line:GeoLine = GeoLine(Shape);
            
            var pointsPerFeet:Number = RelatedTractView.surfaceScale.PointsInOneFeet;
            
            var startPoint:Point = new Point(0,0);
            
            var endPoint:Point = new Point(
                startPoint.x + line.boundWidth * pointsPerFeet,
			    startPoint.y - line.boundHight * pointsPerFeet);
            
			drawingShape.graphics.clear();
			
            //draw transparent hit area
			drawingShape.graphics.lineStyle(4, 0, 0);
            drawingShape.graphics.moveTo(startPoint.x, startPoint.y);
            drawingShape.graphics.lineTo(endPoint.x, endPoint.y);

            //draw call
            drawingShape.graphics.lineStyle(0, color, 1);
            drawingShape.graphics.moveTo(startPoint.x, startPoint.y);
            drawingShape.graphics.lineTo(endPoint.x, endPoint.y);
			
            drawingShape.graphics.endFill();

            //annotation labels positioning
            var upperLabelIndentAngle:Number = line.bearing.Azimuth - 90;
            if (upperLabelIndentAngle < 0) upperLabelIndentAngle += 360;
            upperLabelIndentAngle *= (Math.PI/180);

            var lowerLabelIndentAngle:Number = line.bearing.Azimuth + 90;
            if (lowerLabelIndentAngle < 0) lowerLabelIndentAngle += 360;
            lowerLabelIndentAngle *= (Math.PI/180);

            var labelRotation:Number = line.bearing.Azimuth < 180 
                ? line.bearing.Azimuth - 90 
                : line.bearing.Azimuth + 90;

            upperLabel.text = line.bearing.toString();
            lowerLabel.text = line.distance.toFixed(2);

            upperLabel.rotation = lowerLabel.rotation = labelRotation;
            upperLabel.width = lowerLabel.width = line.distance * pointsPerFeet;
            upperLabel.height = upperLabel.textHeight;
            lowerLabel.height = lowerLabel.textHeight;
            upperLabel.validateNow();

            var upperLabelTextHeight:Number = upperLabel.textHeight;

            if (line.bearing.Azimuth >= 180){
                upperLabel.x = endPoint.x + ((labelIndent + upperLabelTextHeight) * Math.sin(lowerLabelIndentAngle));
                upperLabel.y = endPoint.y - ((labelIndent + upperLabelTextHeight) * Math.cos(lowerLabelIndentAngle));
                lowerLabel.x = endPoint.x + (labelIndent * Math.sin(upperLabelIndentAngle));
                lowerLabel.y = endPoint.y - (labelIndent * Math.cos(upperLabelIndentAngle));
            } else {
                upperLabel.x = startPoint.x + ((labelIndent + upperLabelTextHeight) * Math.sin(upperLabelIndentAngle));
                upperLabel.y = startPoint.y - ((labelIndent + upperLabelTextHeight) * Math.cos(upperLabelIndentAngle));
                lowerLabel.x = startPoint.x + (labelIndent * Math.sin(lowerLabelIndentAngle));
                lowerLabel.y = startPoint.y - (labelIndent * Math.cos(lowerLabelIndentAngle));
            }

            if (upperLabel.textWidth > (line.distance * pointsPerFeet) - 10) {
                lowerLabel.text = "";
                
                upperLabel.text = Model.AnnotationId;
                
                if (upperLabel.textWidth > (line.distance * pointsPerFeet) - 10) {
                    upperLabel.text = "";
                }
            }

        }
		
    }
}