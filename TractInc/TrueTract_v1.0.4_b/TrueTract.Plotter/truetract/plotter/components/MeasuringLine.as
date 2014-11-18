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
import mx.effects.Fade;
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
[Event(name="adjustButtonClick", type="flash.events.Event")]
[Event(name="adjustReverseButtonClick", type="flash.events.Event")]

public class MeasuringLine extends UIComponent
{

    private static var _classConstructed:Boolean = classConstruct();
    private static function classConstruct():Boolean 
    {
        if (!StyleManager.getStyleDeclaration("MeasuringLine")) 
        {
            var newStyleDeclaration:CSSStyleDeclaration = new CSSStyleDeclaration();
            
            newStyleDeclaration.setStyle("lineColor", 0xff9900);
            newStyleDeclaration.setStyle("lineRollOverColor", 0xffcc00);
            newStyleDeclaration.setStyle("lineWidth", 1);
            newStyleDeclaration.setStyle("lineRollOverWidth", 2);

            StyleManager.setStyleDeclaration("MeasuringLine", newStyleDeclaration, true);
        }

        return true;
    }

    [Bindable] [Embed(source="/assets/arrow_left.gif")]
    private var arrowLeftIcon:Class;

    [Bindable] [Embed(source="/assets/arrow_right.gif")]
    private var arrowRightIcon:Class;

    [Bindable] [Embed(source="/assets/delete2.png")]
    private var deleteIcon:Class;

    protected var drawingShape:Sprite;

    protected var textField:UITextField;

    protected var iconBox:HBox;
    
    protected var fadeEffect:Fade;

    private var tickLength:Number = 5;
    private var labelIndent:Number = 5;

    private var valueChanged:Boolean = false;

    public var startPosition:GeoPosition;

    private var _endPosition:GeoPosition; 
    public function get endPosition():GeoPosition { return _endPosition; }
    public function set endPosition(value:GeoPosition):void
    {
        _endPosition = value;
        valueChanged = true;
        
        invalidateProperties();
    }

    private var _canvas:GeoCanvas;
    public function get canvas():GeoCanvas { return _canvas; }
    public function set canvas(value:GeoCanvas):void
    {
        _canvas = value;
        
        if (canvas){
            canvas.addEventListener(ScaleEvent.SCALE_CHANGED, canvas_ScaleChangedEvent);
        }
    }
    
    public function set iconBoxVisible(value:Boolean):void
    {
        iconBox.visible = value;
    }

    override protected function createChildren():void
    {
        super.createChildren();

        if (!textField) {

            textField = new UITextField();
		    textField.mouseEnabled = false;
            textField.embedFonts = true;
            textField.styleName = this;
		    this.addChild(textField);
        }

        if (!drawingShape) {
            drawingShape = new Sprite();
		    this.addChild(drawingShape);
        }
        
        if (!iconBox) {
            createIconBox();
        }
        
        fadeEffect = new Fade(iconBox);
        
        addEventListener(MouseEvent.ROLL_OVER, 
            function ():void { 
                fadeEffect.end();
                fadeEffect.alphaFrom = 0.2;
                fadeEffect.alphaTo = 1;
                fadeEffect.play();
            });
            
        addEventListener(MouseEvent.ROLL_OUT, 
            function ():void {
                fadeEffect.end();
                fadeEffect.alphaFrom = 1;
                fadeEffect.alphaTo = 0.2;
                fadeEffect.play();
            });
    }

    override protected function measure():void
    {
        //TODO: this method is unfinished

        super.measure();

        if (canvas && startPosition && endPosition)
        {

            var startPoint:Point = canvas.GetLocalPosition(startPosition);
            var endPoint:Point = canvas.GetLocalPosition(endPosition);

            var w:Number = Math.abs(endPoint.x - startPoint.x);
            var h:Number = Math.abs(endPoint.y - startPoint.y);

            var lineLength:Number = Math.sqrt((w * w) + (h * h));

            textField.width = textField.getExplicitOrMeasuredWidth();
            textField.height = textField.getExplicitOrMeasuredHeight();

            h+= textField.height;
            
            if (textField.width > lineLength) {
                w = textField.width;
            }

            measuredWidth = w;
            measuredHeight = h;
        }
    }

    override protected function commitProperties():void
    {
        super.commitProperties();
        
        if (valueChanged)
        {
            valueChanged = false;
            
            var line:GeoLine = GeoLine.createByEndPosition(startPosition, endPosition);
            var uom:UnitOfMeasure = canvas.Scale.uom;
            
            textField.text = "Dist: " + (line.distance * uom.RateToOneFeet).toFixed(4) + " " + uom.Name;
            textField.text += ", B: " + line.bearing.Azimuth.toFixed(0);

            invalidateSize();
            invalidateDisplayList();
        }
    }

  	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        if (parent == null) return;

        var startPoint:Point = canvas.GetLocalPosition(startPosition);
        var endPoint:Point = canvas.GetLocalPosition(endPosition);
        
        var line:GeoLine = GeoLine.createByEndPosition(startPosition, endPosition);

        this.move(Math.min(startPoint.x, endPoint.x), Math.min(startPoint.y, endPoint.y));

        var localStartPoint:Point = new Point(startPoint.x - x, startPoint.y - y);
        var localEndPoint:Point = new Point(endPoint.x - x, endPoint.y - y);

        var lineColor:uint = getStyle("lineColor");
        var lineWidth:Number = getStyle("lineWidth");

		drawingShape.graphics.clear();

        //draw transparent hit area
		drawingShape.graphics.lineStyle(lineWidth + labelIndent + 16 , 0xFFFF00, 0.1);
        drawingShape.graphics.moveTo(localStartPoint.x, localStartPoint.y);
        drawingShape.graphics.lineTo(localEndPoint.x, localEndPoint.y);

        var stroke:Stroke = new Stroke(lineColor, lineWidth, 1, false, "normal", CapsStyle.NONE, 
            JointStyle.MITER);
        
        GraphicsUtil.drawDashedLine(
            drawingShape.graphics, stroke, [10, 3], 
            localStartPoint.x, localStartPoint.y,
            localEndPoint.x, localEndPoint.y);

        stroke.apply(drawingShape.graphics);
        
        drawTicks(line, localStartPoint, localEndPoint);
        layoutTextFieldAndButtons(line, localStartPoint, localEndPoint);
    }

    protected function createIconBox():void
    {
        if (iconBox && iconBox.parent)
            iconBox.parent.removeChild(iconBox);

        var adjustButton:Button = new Button();
        adjustButton.width = adjustButton.height = 16;
        adjustButton.toolTip = "Adjust line from the start";
        adjustButton.setStyle("icon", arrowLeftIcon);
        adjustButton.setStyle("paddingLeft", -2);
        adjustButton.addEventListener(MouseEvent.CLICK, adjustButton_clickHandler);

        var deleteButton:Button = new Button();
        deleteButton.width = deleteButton.height = 16;
        deleteButton.toolTip = "Delete Measure Line"
        deleteButton.setStyle("icon", deleteIcon);
        deleteButton.setStyle("paddingLeft", -2);
        deleteButton.addEventListener(MouseEvent.CLICK, deleteButton_clickHandler);

        var adjustReverseButton:Button = new Button();
        adjustReverseButton.width = adjustReverseButton.height = 16;
        adjustReverseButton.toolTip = "Adjust line from the end";
        adjustReverseButton.setStyle("icon", arrowRightIcon);
        adjustReverseButton.setStyle("paddingLeft", -2);
        adjustReverseButton.addEventListener(MouseEvent.CLICK, adjustReverseButton_clickHandler);

        iconBox = new HBox();
        iconBox.setStyle("horizontalGap", 1);
        iconBox.alpha = 0.2;
        iconBox.visible = false;
        iconBox.addChild(adjustButton);
        iconBox.addChild(deleteButton);
        iconBox.addChild(adjustReverseButton);
        
        this.addChild(iconBox);

        iconBox.validateNow();
        iconBox.setActualSize(iconBox.getExplicitOrMeasuredWidth(), iconBox.getExplicitOrMeasuredHeight());

        //make transparent background for iconbox. This will add all hbox area to isRollOver calculation
        iconBox.graphics.beginFill(0, 0);
        iconBox.graphics.drawRect(0,0, iconBox.width, iconBox.height);
        iconBox.graphics.endFill();
    }

    private function drawTicks(line:GeoLine, startPoint:Point, endPoint:Point):void
    {
		var edgeLine:GeoLine = new GeoLine(GeoBearing.CreateByAzimuth(line.bearing.Azimuth - 90), 4);
        
        drawingShape.graphics.moveTo(
            startPoint.x - edgeLine.endPosition.Easting, 
            startPoint.y + edgeLine.endPosition.Northing);

        drawingShape.graphics.lineTo(
            startPoint.x + edgeLine.endPosition.Easting, 
            startPoint.y - edgeLine.endPosition.Northing);

        drawingShape.graphics.moveTo(
            endPoint.x - edgeLine.endPosition.Easting, 
            endPoint.y + edgeLine.endPosition.Northing);

        drawingShape.graphics.lineTo(
            endPoint.x + edgeLine.endPosition.Easting, 
            endPoint.y - edgeLine.endPosition.Northing);
    }
    
    private function layoutTextFieldAndButtons(line:GeoLine, startPoint:Point, endPoint:Point):void
    {
        var labelUpIndentAngle:Number = line.bearing.Azimuth + 90;
        if (labelUpIndentAngle > 360) labelUpIndentAngle -= 360;
        labelUpIndentAngle *= (Math.PI/180);

        var labelDownIndentAngle:Number = line.bearing.Azimuth - 90;
        if (labelDownIndentAngle < 0) labelDownIndentAngle += 360;
        labelDownIndentAngle *= (Math.PI/180);

        var labelRotation:Number = line.bearing.Azimuth < 180 
            ? line.bearing.Azimuth - 90 
            : line.bearing.Azimuth + 90;

        textField.rotation = labelRotation;
        iconBox.rotation = labelRotation;

        if (line.bearing.Azimuth >= 180){
            iconBox.x = endPoint.x + ((labelIndent + iconBox.height) * Math.sin(labelUpIndentAngle));
            iconBox.y = endPoint.y - ((labelIndent + iconBox.height) * Math.cos(labelUpIndentAngle));

            textField.x = endPoint.x + (labelIndent * Math.sin(labelDownIndentAngle));
            textField.y = endPoint.y - (labelIndent * Math.cos(labelDownIndentAngle));
        } else {
            iconBox.x = startPoint.x + ((labelIndent + iconBox.height) * Math.sin(labelDownIndentAngle));
            iconBox.y = startPoint.y - ((labelIndent + iconBox.height) * Math.cos(labelDownIndentAngle));
            
            textField.x = startPoint.x + (labelIndent * Math.sin(labelUpIndentAngle));
            textField.y = startPoint.y - (labelIndent * Math.cos(labelUpIndentAngle));
        }
    }

    protected function adjustButton_clickHandler(event:MouseEvent):void
    {
        dispatchEvent(new Event("adjustButtonClick"));
    }

    protected function adjustReverseButton_clickHandler(event:MouseEvent):void
    {
        dispatchEvent(new Event("adjustReverseButtonClick"));
    }

    protected function deleteButton_clickHandler(event:MouseEvent):void
    {
        dispatchEvent(new Event("deleteButtonClick"));
    }

    private function canvas_ScaleChangedEvent(event:ScaleEvent):void
    {
        valueChanged = true;
        invalidateProperties();
    }
}
}
