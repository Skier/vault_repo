package AerSysCo.UI.Containers
{
import mx.containers.Panel;
import mx.controls.Button;
import mx.effects.Resize;
import mx.core.EdgeMetrics;
import flash.events.MouseEvent;
import mx.events.FlexEvent;
import mx.containers.HBox;
import flash.display.DisplayObject;
import mx.core.Container;
import mx.effects.Move;
import mx.events.EffectEvent;

public class ExtPanel extends Panel
{
    private var headerToolbar:HBox;

    public function addHeaderItem(item:DisplayObject):void
    {
        if (item){
            headerToolbar.addChild(item);

            headerToolbar.validateNow();

            var toolBarHeight:Number = headerToolbar.getExplicitOrMeasuredHeight();
            var toolBarWidth:Number = headerToolbar.getExplicitOrMeasuredWidth();

            headerToolbar.setActualSize(toolBarWidth, toolBarHeight);
        }
    }

    override protected function createChildren():void
    {
        super.createChildren();

        if (!headerToolbar) {
            headerToolbar = new HBox();
            headerToolbar.horizontalScrollPolicy = "off";
            headerToolbar.setStyle("horisontalAlign", "right");
            headerToolbar.setStyle("verticalAlign", "middle");

            titleBar.addChild(headerToolbar);
        }
    }

    override protected function layoutChrome(unscaledWidth:Number, unscaledHeight:Number):void 
    {
        super.layoutChrome(unscaledWidth, unscaledHeight);

        var bm:EdgeMetrics = borderMetrics;

        var headerHeight:Number = getHeaderHeight();

        var panelTitleWidth:Number = titleTextField.getExplicitOrMeasuredWidth();
        var toolBarHeight:Number = headerToolbar.getExplicitOrMeasuredHeight();

        headerToolbar.move(
            titleTextField.x + panelTitleWidth + 10,
            (headerHeight - toolBarHeight) / 2);
    }
}
}