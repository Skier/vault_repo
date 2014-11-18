package src.deedplotter.containers
{

import flash.events.MouseEvent;
import flash.geom.Point;

import mx.containers.HBox;
import mx.containers.TitleWindow;
import mx.controls.VScrollBar;
import mx.controls.VSlider;
import mx.core.*;
import mx.events.SliderEvent;
import mx.managers.CursorManager;
import mx.managers.CursorManagerPriority;
import flash.events.Event;

[Style(name="sliderStyleName", type="String", inherit="no")]

/**
 *  Provides resizable TitleWindow. Also provide ability to user control window transparency
 */
public class ExtendedTitleWindow extends TitleWindow
{

    private static const SIDE_BOTTOM:Number = 2;
    private static const SIDE_RIGHT:Number = 8;

	//--------------------------------------------------------------------------
	//
	//  Constructor
	//
	//--------------------------------------------------------------------------
	public function ExtendedTitleWindow()
	{
		super();

        horizontalScrollPolicy = "off";
        verticalScrollPolicy = "off";

        this.addEventListener(MouseEvent.MOUSE_MOVE, mouseMoveHandler);
        this.addEventListener(MouseEvent.MOUSE_OUT, mouseOutHandler);
        this.addEventListener(MouseEvent.MOUSE_DOWN, mouseDownHandler);
	}

	//--------------------------------------------------------------------------
	//
	//  Fields
	//
	//--------------------------------------------------------------------------

    private const MOUSE_MARGIN:Number = 10;

    public var resizable:Boolean = true;

    [Embed(source="/assets/leftObliqueSize.gif")]
    private var leftObliqueSize:Class;

    private var resizeInProgress: Boolean = false;
    private var mouseOverResizeArea:Boolean = false;

    private var alphaSlider:VSlider;
    private var alphaValueChanged:Boolean = false;

    [Bindable("transparencyChange")]
    public function get transparencySliderValue():Number 
    { 
        return alphaSlider ? alphaSlider.value : 0;
    }

    public function set transparencySliderValue(value:Number):void 
    { 
        alphaSlider.setThumbValueAt(0, value);
    }
    
    
	//--------------------------------------------------------------------------
	//
	//  Methods
	//
	//--------------------------------------------------------------------------

	public function getTitleBar():UIComponent
	{
		return super.titleBar;
	}	

	override public function styleChanged(styleProp:String):void 
	{
	    var allStyles:Boolean = !styleProp || styleProp == "styleName";

        if (allStyles || styleProp == "borderAlpha" || styleProp == "backgroundAlpha")
        {
            alphaValueChanged = true;
        }

	    super.styleChanged(styleProp);
    }

    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        if (alphaValueChanged && alphaSlider)
        {
            alphaValueChanged = false;
            alphaSlider.value = getStyle("backgroundAlpha");
        }
    }

	override protected function createChildren():void 
	{
	    super.createChildren();

        var sliderStyleName:String = getStyle("sliderStyleName");

        if (!alphaSlider)
        {
    	    alphaSlider = new VSlider();
            alphaSlider.explicitHeight = 30;
            alphaSlider.minimum = 0; 
            alphaSlider.maximum = 1; 
            alphaSlider.snapInterval = 0.01;
            alphaSlider.liveDragging = true;
            alphaSlider.thumbCount = 1;
            alphaSlider.addEventListener(SliderEvent.CHANGE, alphaSlider_changeHandler);
            alphaSlider.addEventListener(MouseEvent.MOUSE_DOWN, alphaSlider_mouseDownHandler);
            alphaSlider.toolTip = "Transparency Control";

            if (sliderStyleName)
                alphaSlider.styleName = sliderStyleName;

    	    super.titleBar.addChild(alphaSlider);
        }

	}

    override protected function layoutChrome(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.layoutChrome(unscaledWidth, unscaledHeight);
        
        if (alphaSlider)
        {

            var bm:EdgeMetrics = borderMetrics;
            
            var x:Number = bm.left;
            var y:Number = bm.top;
    
            var headerHeight:Number = getHeaderHeight();
    
            var rightIndent:Number = showCloseButton ? 30 : 10;

            alphaSlider.setActualSize(
                alphaSlider.getExplicitOrMeasuredWidth(),
                alphaSlider.getExplicitOrMeasuredHeight());

            alphaSlider.move(
                unscaledWidth - x - bm.right - rightIndent -
                alphaSlider.getExplicitOrMeasuredWidth(),
                (headerHeight -
                alphaSlider.getExplicitOrMeasuredHeight()) / 2);
        }

    }

    private function doResize(event:MouseEvent):void
    {
        if (resizeInProgress)
        {
            var newWidth:Number = event.stageX - x;
            var newHeight:Number = event.stageY - y;

            width = Math.max(newWidth, minWidth);
            height = Math.max(newHeight, minHeight);
        }
    }

	protected function alphaSlider_changeHandler(event:SliderEvent):void 
	{
	    setStyle("backgroundAlpha", event.value);
	    setStyle("borderAlpha", event.value + 0.1);
        alpha = event.value + 0.3;
	    dispatchEvent(new Event("transparencyChange"));
	}

    protected function alphaSlider_mouseDownHandler(event:MouseEvent):void 
    {
        event.preventDefault();
        event.stopPropagation();
    }

    private function mouseMoveHandler(event:MouseEvent):void 
    {
        if (!resizable || !isPopUp || resizeInProgress) return;

        var resizeAreaX:Number = (x + width - MOUSE_MARGIN);
        var resizeAreaY:Number = (y + height - MOUSE_MARGIN);
        
        if(event.stageX >= resizeAreaX && event.stageY >= resizeAreaY)
        {
            CursorManager.removeCursor(CursorManager.currentCursorID);
            CursorManager.setCursor(leftObliqueSize, CursorManagerPriority.MEDIUM, -6, -6);
            mouseOverResizeArea = true;
        } 
        else 
        {
            mouseOverResizeArea = false;
            CursorManager.removeCursor(CursorManager.currentCursorID);
        }
    }
    
    private function mouseOutHandler(event:MouseEvent):void
    {
        if (!resizeInProgress) 
        {
            CursorManager.removeCursor(CursorManager.currentCursorID);
        }
    }

    private function mouseUpHandler(event:MouseEvent):void
    {
        if (resizeInProgress) 
        {
            resizeInProgress = false;

            Application.application.parent.removeEventListener(MouseEvent.MOUSE_UP, mouseUpHandler);
            Application.application.parent.removeEventListener(MouseEvent.MOUSE_MOVE, doResize);
        }
    }
    
    private function mouseDownHandler(event:MouseEvent):void
    {
        if (mouseOverResizeArea) 
        {

            Application.application.parent.addEventListener(MouseEvent.MOUSE_UP, mouseUpHandler);
            Application.application.parent.addEventListener(MouseEvent.MOUSE_MOVE, doResize);

            resizeInProgress = true;
        }
    }
    
	
}
}
