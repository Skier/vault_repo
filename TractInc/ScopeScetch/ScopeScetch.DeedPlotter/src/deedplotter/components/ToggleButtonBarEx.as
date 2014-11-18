package src.deedplotter.components
{
import mx.controls.ToggleButtonBar;
import mx.events.FlexEvent;

//In the TogleButtonBar it is not possible to set selectedIndex = -1;
//This class allows to do this.
public class ToggleButtonBarEx extends ToggleButtonBar
{
    override public function set selectedIndex(value:int):void
    {
        if (value == selectedIndex)
            return;

        if (value < numChildren)
        {
            hiliteSelectedNavItem(value);
            dispatchEvent(new FlexEvent(FlexEvent.VALUE_COMMIT));
        }
    }
        
    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
        
        var index:Number = selectedIndex;

        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        hiliteSelectedNavItem(index);
    }
}
}