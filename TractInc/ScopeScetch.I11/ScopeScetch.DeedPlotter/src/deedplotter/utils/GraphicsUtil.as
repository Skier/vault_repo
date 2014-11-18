package src.deedplotter.utils
{
import flash.geom.Point;
import flash.display.Graphics;
import mx.graphics.IStroke;
import mx.core.UIComponent;

public class GraphicsUtil
{
    public static function drawArcEx(g:Graphics, startPoint:Point, startAngle:Number, 
        radius:Number, delta:Number, clockwise:Boolean = true):Point 
    {
            
        if (!clockwise) 
        {
            //draw Arc in counterclockwise
            var chordAngle:Number = (startAngle - (delta/2)) * Math.PI / 180;
            var chordLength:Number = 2 * (radius * Math.sin( delta / 2 * (Math.PI/180) ) );
            
            var endPoint:Point = new Point();
            
            endPoint.x = startPoint.x + (chordLength * Math.sin(chordAngle));
            endPoint.y = startPoint.y - (chordLength * Math.cos(chordAngle));
            startAngle = startAngle - 180 - delta;

            while (startAngle < 0) startAngle += 360;
            
            drawArc(g, endPoint, startAngle, radius, delta);
            
            return endPoint;
        } 
        else 
        {
            return drawArc(g, startPoint, startAngle, radius, delta);
        }
    }
    
     public static function drawArc(g:Graphics, startPoint:Point, startAngle:Number, 
        radius:Number, delta:Number):Point 
     {

        var nStartingAngle:Number = (startAngle - 180) * Math.PI / 180;
        
        var center:Point = new Point();
        center.x = startPoint.x - radius * Math.cos(nStartingAngle);
        center.y = startPoint.y - radius * Math.sin(nStartingAngle);

        var nArc:Number = delta;
        // The angle of each of the eight segments is 45 degrees (360 divided by eight),
        // which equals p/4 radians.
        if(nArc > 360) {
            nArc = 360;
        }
        
        nArc = Math.PI/180 * nArc;
        var nAngleDelta:Number = nArc/8;

        // Find the distance from the circle's center to the control points
        // for the curves.
        var nCtrlDist:Number = radius / Math.cos(nAngleDelta/2);
                    
        var nAngle:Number = nStartingAngle;
        var nCtrlX:Number;
        var nCtrlY:Number;
        var nAnchorX:Number;
        var nAnchorY:Number;

        // Move to the starting point, one radius to the right of the circle's center.
        g.moveTo(startPoint.x, startPoint.y);

        // Repeat eight times to create eight segments.
        for (var i:Number = 0; i < 8; i++) 
        {
            // Increment the angle by angleDelta (p/4) to create the whole circle (2p).
            nAngle += nAngleDelta;

            // The control points are derived using sine and cosine.
            nCtrlX = center.x + Math.cos(nAngle-(nAngleDelta/2))*(nCtrlDist);
            nCtrlY = center.y + Math.sin(nAngle-(nAngleDelta/2))*(nCtrlDist);

            // The anchor points (end points of the curve) can be found similarly to the
            // control points.
            nAnchorX = center.x + Math.cos(nAngle) * radius;
            nAnchorY = center.y + Math.sin(nAngle) * radius;

            // Draw the segment.
            g.curveTo(nCtrlX, nCtrlY, nAnchorX, nAnchorY);
        }
        
        return new Point(nAnchorX, nAnchorY);
    }
    
	public static function drawPolygon(g:Graphics, x:Number, y:Number, points:Number, radius:Number, angle:Number=0):void
	{
		
		// convert sides to positive value
		var count:int = Math.abs(points);
		
		if (count>=2) 
		{
			
			// calculate span of sides
			var step:Number = (Math.PI*2)/points;
			
			// calculate starting angle in radians
			var start:Number = (angle/180)*Math.PI;
			g.moveTo(x+(Math.cos(start)*radius), y-(Math.sin(start)*radius));
			
			// draw the polygon
			for (var i:int=1; i<=count; i++) 
			{
				g.lineTo(x+Math.cos(start+(step*i))*radius, 
				y-Math.sin(start+(step*i))*radius);
			}
			
		}
	}
    
    public static function drawStar(target:Graphics, x:Number, y:Number, points:Number, innerRadius:Number, outerRadius:Number,angle:Number=0 ):void
    {
        var count:int = Math.abs(points);
        if (count>=2) 
        {
            
            // calculate distance between points
            var step:Number = (Math.PI*2)/points;
            var halfStep:Number = step/2;
            
            // calculate starting angle in radians
            var start:Number = (angle/180)*Math.PI;
            target.moveTo(x+(Math.cos(start)*outerRadius), y-(Math.sin(start)*outerRadius));
            
            // draw lines
            for (var i:int=1; i<=count; i++) 
            {
                target.lineTo(x+Math.cos(start+(step*i)-halfStep)*innerRadius, 
                y-Math.sin(start+(step*i)-halfStep)*innerRadius);

                target.lineTo(x+Math.cos(start+(step*i))*outerRadius, 
                y-Math.sin(start+(step*i))*outerRadius);
            }
        }
    }
        
    public static function drawDashedLine(
        target:Graphics, stroke:IStroke, pattern:Array, 
        x0:Number, y0:Number, x1:Number, y1:Number):void
    {
        target.moveTo(x0,y0);
        var struct:DashStruct = new DashStruct();

        _drawDashedLine(target, stroke, pattern, struct, x0, y0, x1, y1);
    }
    
    /**
     * drawDashedLine
     * 
     * This function is from Ely Greenfield's Quietly Scheming blog where it was used to
     * draw patterned lines on charts.
     * 
     * (www.quietlyscheming.com)
     */
    protected static function _drawDashedLine(target:Graphics, stroke:IStroke,
        pattern:Array, drawingState:DashStruct, x0:Number,y0:Number,x1:Number,y1:Number):void
    {                       
        var dX:Number = x1 - x0;
        var dY:Number = y1 - y0;
        var len:Number = Math.sqrt(dX*dX + dY*dY);
        dX /= len;
        dY /= len;
        var tMax:Number = len;
        
        var t:Number = -drawingState.offset;
        var bDrawing:Boolean = drawingState.drawing;
        var patternIndex:int = drawingState.patternIndex;
        var styleInited:Boolean = drawingState.styleInited;

        while(t < tMax)
        {
            t += pattern[patternIndex];
            if(t < 0)
            {
                    var x:int = 5;
            }
            if(t >= tMax)
            {
                    drawingState.offset = pattern[patternIndex] - (t - tMax);
                    drawingState.patternIndex = patternIndex;
                    drawingState.drawing = bDrawing;
                    drawingState.styleInited = true;
                    t = tMax;
            }
            
            if(styleInited == false)
            {
                    if(bDrawing)
                            stroke.apply(target);
                    else
                            target.lineStyle(0,0,0);
            }
            else
            {
                    styleInited = false;
            }
                    
            target.lineTo(x0 + t*dX,y0 + t*dY);

            bDrawing = !bDrawing;
            patternIndex = (patternIndex + 1) % pattern.length;
        }
    }
}
}

class DashStruct
{
        public function init():void
        {
                drawing = true;
                patternIndex = 0;
                offset = 0;
        }
        public var drawing:Boolean = true;
        public var patternIndex:int = 0;
        public var offset:Number = 0;   
        public var styleInited:Boolean = false;
}
