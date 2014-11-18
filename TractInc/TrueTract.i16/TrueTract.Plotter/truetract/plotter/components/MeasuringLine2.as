package truetract.plotter.components
{
import flash.display.CapsStyle;
import flash.display.JointStyle;
import flash.display.Sprite;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.geom.Point;

import mx.containers.HBox;
import mx.controls.Button;
import mx.core.UIComponent;
import mx.core.UITextField;
import mx.graphics.Stroke;
import mx.styles.CSSStyleDeclaration;
import mx.styles.StyleManager;

import truetract.plotter.containers.GeoCanvas;
import truetract.plotter.events.ScaleEvent;
import truetract.plotter.utils.GeoBearing;
import truetract.plotter.utils.GeoLine;
import truetract.plotter.utils.GeoPosition;
import truetract.plotter.utils.GraphicsUtil;
import truetract.plotter.utils.UnitOfMeasure;

[Event(name="deleteButtonClick", type="flash.events.Event")]
[Event(name="adjustFromEndButtonClick", type="flash.events.Event")]
[Event(name="adjustFtomStartButtonClick", type="flash.events.Event")]

public class MeasuringLine2 extends MeasuringLine
{

    private var adjustButtonStyleNameProp:String = "adjustButtonStyleName";

    private var adjustButton:MeasuringLineAdjustButton;
    private var adjustReverseButton:MeasuringLineAdjustButton;

    private var buttonWidth:Number = 10;

    override protected function createChildren():void
    {
        super.createChildren();

        if (!adjustButton) {
            adjustButton = createAdjustButton();
        }

        if (!adjustReverseButton) {
            adjustReverseButton = createAdjustButton();
        }
    }

    private function createAdjustButton():MeasuringLineAdjustButton {
        var button:MeasuringLineAdjustButton = new MeasuringLineAdjustButton();

        button.addEventListener(MouseEvent.CLICK, adjustButton_clickHandler);
        button.alpha = iconBox.alpha;
        button.setStyle("lineWidth", getStyle("lineWidth"));
        button.setStyle("lineColor", getStyle("lineColor"));
        button.setActualSize(10, 10);

        fadeEffect.targets.push(button);
    
        addChild(button);

        return button;
    }
    
  	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);

        var startPoint:Point = canvas.GetLocalPosition(startPosition);
        var endPoint:Point = canvas.GetLocalPosition(endPosition);
        var line:GeoLine = GeoLine.createByEndPosition(startPosition, endPosition);
        var localStartPoint:Point = new Point(startPoint.x - x, startPoint.y - y);
        var localEndPoint:Point = new Point(endPoint.x - x, endPoint.y - y);

        layoutButtons(line, localStartPoint, localEndPoint);
    }

    private function layoutButtons(line:GeoLine, startPoint:Point, endPoint:Point):void
    {
        var halfButtonWidth:Number = buttonWidth / 2;

        var angle:Number = line.bearing.Radian;

        var reverseAngle:Number = line.bearing.Azimuth - 180;
        reverseAngle += reverseAngle < 0 ? 360 : 0;
        reverseAngle *= (Math.PI/180); //radian
        
        var centerAllignAngle:Number = line.bearing.Azimuth - 90;
        centerAllignAngle += centerAllignAngle < 0 ? 360 : 0;
        centerAllignAngle *= (Math.PI/180);

        var startButtonPos:Point = new Point();
        var endButtonPos:Point = new Point();

        var buttonIndent:Number = buttonWidth / 2 + 2;

        startButtonPos.x = startPoint.x + (buttonIndent * Math.sin(reverseAngle));
        startButtonPos.y = startPoint.y - (buttonIndent * Math.cos(reverseAngle));
        endButtonPos.x = endPoint.x + (buttonIndent * Math.sin(angle));
        endButtonPos.y = endPoint.y - (buttonIndent * Math.cos(angle));

/*         if (startPoint.x < endPoint.x)
        {
            startButtonPos.x = startPoint.x + ((buttonWidth + 2) * Math.sin(reverseAngle));
            startButtonPos.y = startPoint.y - ((buttonWidth + 2) * Math.cos(reverseAngle));

            endButtonPos.x = endPoint.x + (2 * Math.sin(angle));
            endButtonPos.y = endPoint.y - (2 * Math.cos(angle));
        } 
        else 
        {
            startButtonPos.x = startPoint.x + (2 * Math.sin(reverseAngle));
            startButtonPos.y = startPoint.y - (2 * Math.cos(reverseAngle));

            endButtonPos.x = endPoint.x + ((buttonWidth + 2) * Math.sin(angle));
            endButtonPos.y = endPoint.y - ((buttonWidth + 2) * Math.cos(angle));
        }
 */        
/*         //align buttons center
        endButtonPos.x += (halfButtonWidth * Math.sin(centerAllignAngle));
        endButtonPos.y -= (halfButtonWidth * Math.cos(centerAllignAngle));
        
        startButtonPos.x += (halfButtonWidth * Math.sin(centerAllignAngle)), 
        startButtonPos.y -= (halfButtonWidth * Math.cos(centerAllignAngle));
 */

        adjustReverseButton.angle = (360 + 90) - line.bearing.Azimuth;
        adjustButton.angle  = adjustReverseButton.angle + 180;

        adjustButton.move(startButtonPos.x, startButtonPos.y);
        adjustReverseButton.move(endButtonPos.x, endButtonPos.y);

        adjustButton.invalidateDisplayList();
        adjustReverseButton.invalidateDisplayList();
        
        //set buttons rotation

//        adjustButton.draw();
//        adjustReverseButton.draw();

/*         var lineColor:uint = getStyle("lineColor");
        var lineWidth:Number = getStyle("lineWidth");

        var stroke:Stroke = new Stroke(lineColor, lineWidth, 1, false, "normal", CapsStyle.NONE, 
            JointStyle.MITER);

        adjustButton.graphics.clear();
        stroke.apply(adjustButton.graphics);
        GraphicsUtil.drawPolygon(adjustButton.graphics, 0, 0, 3, 5, adjustButtonAngle);

        adjustReverseButton.graphics.clear();
        stroke.apply(adjustReverseButton.graphics);
        GraphicsUtil.drawPolygon(adjustReverseButton.graphics, 0, 0, 3, 5, adjustReverseButtonAngle);
 */    }
    
    override protected function createIconBox():void
    {
        super.createIconBox();

        iconBox.removeChildAt(0);
        iconBox.removeChildAt(iconBox.numChildren - 1);
        
        iconBox.validateNow();
        iconBox.setActualSize(iconBox.getExplicitOrMeasuredWidth(), iconBox.getExplicitOrMeasuredHeight());
    }

    override protected function adjustButton_clickHandler(event:MouseEvent):void
    {
        var button:Sprite = Sprite(event.target);
        var oppositeButton:Sprite = (event.target == adjustButton) ? adjustReverseButton : adjustButton;

        if (button.x < oppositeButton.x) {
            dispatchEvent(new Event("adjustButtonClick"));
        } else {
            dispatchEvent(new Event("adjustReverseButtonClick"));
        }
    }
    
}
}

