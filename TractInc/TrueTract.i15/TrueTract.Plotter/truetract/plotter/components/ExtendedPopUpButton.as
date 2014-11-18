package truetract.plotter.components
{
import mx.controls.PopUpButton;
import mx.core.IUIComponent;
import mx.core.mx_internal;

use namespace mx_internal;

public class ExtendedPopUpButton extends PopUpButton
{
    public var popUpWidth:Number = 0;

    override mx_internal function getPopUp():IUIComponent
    {
        var result:IUIComponent = super.popUp;
        
        if (result && popUpWidth > 0)
        {
            result.width = popUpWidth;
        }
        
        return result;
    }
}
}