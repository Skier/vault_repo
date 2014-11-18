package truetract.web.containers
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

[Style(name="collapseButtonStyleName", type="String", inherit="no")]
[Style(name="expandButtonStyleName", type="String", inherit="no")]

public class CollapsiblePanel extends Panel
{

    public function CollapsiblePanel()
    {
        addEventListener(FlexEvent.CREATION_COMPLETE, creationCompleteHandler);
        verticalScrollPolicy = "off";
    }
    
    public var collapsedContainer:Container;
    public var expandedContainer:Container;

    private var collapseButton:Button;
    private var headerToolbar:HBox;

    private var resizeEffect:Resize = new Resize(this);
    private var moveEffect:Move = new Move(this);

    private var expandedPanelHeight:int;

    private var collapseButtonStyleNameProp:String = "collapseButtonStyleName";
    private var expandButtonStyleNameProp:String = "expandButtonStyleName";

    /**
     * Indicates whether the ToolBar is currently collapsed.
     */
    private var collapsedChanged:Boolean = false;
    private var _collapsed:Boolean = false;
    [Bindable] public function get collapsed():Boolean { return _collapsed; }
    public function set collapsed(value:Boolean):void 
    {
        _collapsed = value;

        collapsedChanged = true;
        invalidateDisplayList();
    }
    
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

    public function addHeaderToolButton(icon:Class, tooltip:String):Button
    {
        var btn:Button = new Button();
        btn.width = 20;
        btn.height = 20;
        btn.styleName = "toolButton";
        btn.setStyle("icon", icon);
        btn.toolTip = tooltip;

        addHeaderItem(btn);

        return btn;
    }

    override protected function createChildren():void
    {
        super.createChildren();

        if (!collapseButton)
        {
            collapseButton = new Button();
            collapseButton.explicitWidth = collapseButton.explicitHeight = 20;
            collapseButton.focusEnabled = false;
            collapseButton.visible = true;
            collapseButton.enabled = true;
            collapseButton.addEventListener(MouseEvent.MOUSE_DOWN, collapseButton_mouseDownHandler);
            collapseButton.owner = this;

            titleBar.addChild(collapseButton);
        }
        
        if (!headerToolbar) {
            headerToolbar = new HBox();
            headerToolbar.horizontalScrollPolicy = "off";
            headerToolbar.setStyle("verticalAlign", "middle");

            titleBar.addChild(headerToolbar);
        }
    }

    override protected function layoutChrome(unscaledWidth:Number, unscaledHeight:Number):void 
    {
        super.layoutChrome(unscaledWidth, unscaledHeight);

        var bm:EdgeMetrics = borderMetrics;

        var headerHeight:Number = getHeaderHeight();

        if (collapseButton && collapseButton.visible)
        {
            var collapseButtonWidth:Number = collapseButton.getExplicitOrMeasuredWidth();
            var collapseButtonHeight:Number = collapseButton.getExplicitOrMeasuredHeight();

            collapseButton.setActualSize(collapseButtonWidth, collapseButtonHeight);

            collapseButton.move(
                unscaledWidth - bm.left - 10 - bm.right - collapseButtonWidth,
                (headerHeight - collapseButtonHeight) / 2);
        }
        
        var panelTitleWidth:Number = titleTextField.getExplicitOrMeasuredWidth();
        var toolBarHeight:Number = headerToolbar.getExplicitOrMeasuredHeight();

        headerToolbar.move(
            titleTextField.x + panelTitleWidth + 10,
            (headerHeight - toolBarHeight) / 2);
    }

    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);

        if (unscaledHeight == 0) return;

        if (collapsedChanged)
        {
            collapsedChanged = false;

            if (collapsed)
                collapsePanel();
            else
                expandPanel(); 
        }
    }

    protected function collapsePanel():void 
    {
        if (resizeEffect.isPlaying)
        {
            resizeEffect.reverse();
        } 
        else 
        {
            expandedPanelHeight = this.height;

            resizeEffect.heightTo = getHeaderHeight();
            resizeEffect.play();
        }
        
		var expandButtonStyleName:String = getStyle(expandButtonStyleNameProp);
		if (expandButtonStyleName) {
		    collapseButton.styleName = expandButtonStyleName;
		}
    }
    
    protected function expandPanel():void 
    {
/* 
        if (expandedPanelHeight == getHeaderHeight()) {
            expandedPanelHeight = getExplicitOrMeasuredHeight();
        }
 */
        expandedPanelHeight = measuredHeight;

        if (resizeEffect.isPlaying) {
            resizeEffect.reverse();
        } else {
            resizeEffect.heightTo = expandedPanelHeight;
            resizeEffect.play();
        }

		var collapseButtonStyleName:String = getStyle(collapseButtonStyleNameProp);
		if (collapseButtonStyleName) {
		    collapseButton.styleName = collapseButtonStyleName;
		}
    }

    protected function collapseButton_mouseDownHandler(event:MouseEvent):void 
    {
        event.preventDefault();
        event.stopPropagation();

        collapsed = !collapsed;
    }

    private function creationCompleteHandler(event:FlexEvent):void
    {
        collapsed = collapsed;
    }

}
}