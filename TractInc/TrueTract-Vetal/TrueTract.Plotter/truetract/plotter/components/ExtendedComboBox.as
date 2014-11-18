package truetract.plotter.components
{
import mx.controls.ComboBox;
import flash.events.Event;

public class ExtendedComboBox extends ComboBox
{
    /**
     *  @private
     *  Storage for the showDataTips property.
     */
    private var _showDataTips:Boolean = false;

    [Bindable("showDataTipsChanged")]
    [Inspectable(category="Data", defaultValue="false")]

    /**
     *  A flag that indicates whether dataTips are displayed for text in the rows.
     *  If <code>true</code>, dataTips are displayed.  DataTips
     *  are tooltips designed to show the text that is too long for the row.
     *  If you set a dataTipFunction, dataTips are shown regardless of whether the
     *  text is too long for the row.
     * 
     *  @default false
     */
    public function get showDataTips():Boolean
    {
        return _showDataTips;
    }

    /**
     *  @private
     */
    public function set showDataTips(value:Boolean):void
    {
        _showDataTips = value;

        invalidateDisplayList();

        dispatchEvent(new Event("showDataTipsChanged"));
    }

    private var _keepOptimalDropDownWidth:Boolean = false;

    [Bindable]
    [Inspectable(category="Size", defaultValue="False")]
    public function get keepOptimalDropDownWidth():Boolean { return _keepOptimalDropDownWidth; }
    public function set keepOptimalDropDownWidth(value:Boolean):void
    {
        _keepOptimalDropDownWidth = true;

        invalidateDisplayList();
    }

    override protected function updateDisplayList(unscaleWidth:Number, unscaledHeight:Number):void 
    {
        super.updateDisplayList(unscaleWidth, unscaledHeight);
        
        if (dropdown) 
        {
            //If the ComboBox gets resized or re-styled, it destroys the dropdown and makes a new one.
            //We can't handle all this situation. 
            //So, just lets set showDataTips property every display list update cycle.
            dropdown.showDataTips = showDataTips;
        }

        if (dataProvider && keepOptimalDropDownWidth)
        {
            dropdownWidth = Math.max(
                calculatePreferredSizeFromData(dataProvider.length).width,
                unscaleWidth);
        }
        
    }
}
}