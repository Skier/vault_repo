package truetract.plotter.components.tractViewClasses
{
	
import flash.events.MouseEvent;

import mx.core.UIComponent;
import mx.styles.CSSStyleDeclaration;
import mx.styles.StyleManager;

import truetract.plotter.utils.BoundRectangle;
import truetract.plotter.utils.GeoPosition;

[Style(name="bgColor", type="Number", format="Color", inherit="no")]
[Style(name="bgRollOverColor", type="Number", format="Color", inherit="no")]
[Style(name="borderColor", type="Number", format="Color", inherit="no")]
[Style(name="borderRollOverColor", type="Number", format="Color", inherit="no")]
[Style(name="width", type="Nmber", format="Number", inherit="no")]

public class ControlPointView extends UIComponent
{

    private static var classConstructed:Boolean = classConstruct();

    private static function classConstruct():Boolean {
    	
        if (!StyleManager.getStyleDeclaration("ControlPointView")) {
            var newStyleDeclaration:CSSStyleDeclaration = new CSSStyleDeclaration();
            newStyleDeclaration.setStyle("backgroundColor", 0xFFCC00);
            newStyleDeclaration.setStyle("borderColor", 0x666666);
            newStyleDeclaration.setStyle("backgroundRollOverColor", 0xFF0000);
            newStyleDeclaration.setStyle("borderRollOverColor", 0x666666);
            newStyleDeclaration.setStyle("width", 8);
                            
            StyleManager.setStyleDeclaration("TractPointView", newStyleDeclaration, true);
        }

        return true;
    }

	public function ControlPointView(position:GeoPosition) {
	    super();
	    
	    startPosition = position;
	    
 		addEventListener(MouseEvent.ROLL_OUT, rollOutHandler);
		addEventListener(MouseEvent.ROLL_OVER, rollOverHandler);
	}

    public var startPosition:GeoPosition;
    		
    protected var pointWidth:Number;

	protected var _phase:String = "rolledOut";

    protected var _highlighted:Boolean;
	
    override public function invalidateProperties():void {
	    super.invalidateProperties();
	    
        invalidateDisplayList();
    }

	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        graphics.clear();
        
        pointWidth = getStyle("width");

        if (_phase == "rolledOver"){
			graphics.beginFill(getStyle("backgroundRollOverColor"));
            graphics.lineStyle(0, getStyle("borderRollOverColor"));
            pointWidth += 2;
        } else {
			graphics.beginFill(getStyle("backgroundColor"));
            graphics.lineStyle(0, getStyle("borderColor"));
        }

        var halfPoint:Number = pointWidth/2;
        graphics.moveTo(-halfPoint, 0);
        graphics.lineTo(0, -halfPoint);
        graphics.lineTo(halfPoint, 0);
        graphics.lineTo(0, halfPoint);
        graphics.lineTo(-halfPoint, 0);
        graphics.endFill();
    }

    public function get boundRectangle():BoundRectangle {
        var result:BoundRectangle = new BoundRectangle();
        result.minX = (startPosition.Easting - pointWidth / 2);
        result.minY = (startPosition.Northing - pointWidth / 2);
        result.maxX = result.maxX + pointWidth;
        result.maxY = result.maxY + pointWidth;
        
        return result;
    }
    
	protected function rollOverHandler(event:MouseEvent):void 
	{
		_phase = "rolledOver";
		updateDisplayList(this.unscaledWidth, this.unscaledHeight);
	}

	protected function rollOutHandler(event:MouseEvent):void 
	{
		_phase = "rolledOut";			
		updateDisplayList(this.unscaledWidth, this.unscaledHeight);
	}        
}
}