package src.deedplotter.components
{
import flash.events.Event;
import flash.events.MouseEvent;
import flash.geom.Point;

import mx.controls.Button;
import mx.controls.ColorPicker;
import mx.controls.Image;
import mx.controls.colorPickerClasses.SwatchPanel;
import mx.core.mx_internal;
import mx.events.FlexEvent;
import mx.managers.ISystemManager;
import mx.managers.PopUpManager;
import mx.skins.halo.SwatchPanelSkin;

use namespace mx_internal;

/**
 * This Color Picker contains addition icon for AdvancedColorPicker calls.
 */
public class ExtendedColorPicker extends ColorPicker
{

    [Embed(source="/assets/colorWheel.png")]
    [Bindable]
    public var imgCls:Class;
    
    private var image:Image;

    override mx_internal function getDropdown():SwatchPanel 
    {
        super.getDropdown();

        dropdown.addEventListener(FlexEvent.CREATION_COMPLETE, dropdown_creationCompleteHandler);

        return dropdown;
    }   

    private function dropdown_creationCompleteHandler(event:FlexEvent):void
    {
        image = new Image();
        image.source = imgCls;
        image.toolTip = "Advanced Color Picker";
        image.addEventListener(MouseEvent.CLICK, colorWheel_mouseClickHandler);

        dropdown.addChildAt(image, 1);

        var paddingRight:Number = dropdown.getStyle("paddingRight");
        var paddingTop:Number = dropdown.getStyle("paddingTop");

        image.setActualSize(16, 16);
        image.move(dropdown.width - image.width - paddingRight, paddingTop);
    }

    private function colorWheel_mouseClickHandler(event:MouseEvent):void
    {
        close();

		stage.frameRate = 120;
		var pop1:* = PopUpManager.createPopUp(this, AdvancedColorPicker, true);
		pop1.setColorRGB(selectedColor);
		pop1.lastColor = selectedColor;
		pop1.addEventListener(MouseEvent.CLICK, setColor);

        var initY:Number;
        var dropdownGap:Number = 6;
        var point:Point = localToGlobal(new Point(0, 0));

        // Position: top or bottom
        var yOffset:Number = point.y;
        var sm:ISystemManager = systemManager;

        if (point.y + pop1.height > sm.screen.height && point.y > (height + pop1.height)) // Up
        {
            // Dropdown opens up instead of down
            yOffset -= dropdownGap + pop1.height;
        }
        else // Down
        {
            yOffset += dropdownGap + height;
        }

        // Position: left or right
        var xOffset:Number = point.x;
        if (point.x + pop1.width > sm.screen.width && point.x > (width + pop1.width))
        {
            // Dropdown appears to the left instead of right
            xOffset -= (pop1.width - width);
        }

        // Position the popup
        callLater(pop1.move, [xOffset, yOffset]);

		function setColor():void {
			selectedColor = pop1.getColorRGB();
		}
    }

}
}