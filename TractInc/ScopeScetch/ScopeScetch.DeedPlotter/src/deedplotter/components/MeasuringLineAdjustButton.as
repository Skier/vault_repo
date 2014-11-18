package src.deedplotter.components
{
import flash.display.CapsStyle;
import flash.display.JointStyle;
import flash.events.MouseEvent;

import mx.core.UIComponent;

import src.deedplotter.utils.GraphicsUtil;

public class MeasuringLineAdjustButton extends UIComponent
{
    public function MeasuringLineAdjustButton()
    {
        addEventListener(MouseEvent.ROLL_OUT, rollOutHandler);
		addEventListener(MouseEvent.ROLL_OVER, rollOverHandler);
    }
    
    public var angle:Number = 0;
    public var buttonWidth:Number = 10;

	private var rollPhase:String = RollPhase.OUT;

    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);

        var lineColor:uint = getStyle("lineColor");
        var lineWidth:Number = getStyle("lineWidth");
        
        graphics.clear();
        graphics.lineStyle(lineWidth, lineColor, 1, false, "normal", CapsStyle.NONE, JointStyle.MITER);
        
        if (rollPhase == RollPhase.OVER) {
            graphics.beginFill(lineColor);
            GraphicsUtil.drawPolygon(graphics, 0, 0, 3, buttonWidth / 2, angle);
            graphics.endFill();
        } else {
            GraphicsUtil.drawPolygon(graphics, 0, 0, 3, buttonWidth / 2, angle);
        }
    }

	protected function rollOverHandler(event:MouseEvent):void 
	{
		rollPhase = RollPhase.OVER;
		invalidateDisplayList();
	}

	protected function rollOutHandler(event:MouseEvent):void 
	{
		rollPhase = RollPhase.OUT;			
		invalidateDisplayList();
	}

}
}

class RollPhase {
    public static var OVER:String = "over";
    public static var OUT:String = "out";
}