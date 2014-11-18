package src.deedplotter.containers.dockerClasses
{

import flash.display.DisplayObject;
import flash.display.Graphics;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.geom.Point;

import mx.containers.Box;
import mx.containers.HBox;
import mx.containers.TitleWindow;
import mx.controls.Button;
import mx.controls.Image;
import mx.core.Application;
import mx.core.Container;
import mx.core.EdgeMetrics;
import mx.core.EventPriority;
import mx.core.IUIComponent;
import mx.core.UIComponent;
import mx.core.mx_internal;
import mx.effects.Resize;
import mx.events.ResizeEvent;
import mx.logging.targets.MiniDebugTarget;
import mx.managers.PopUpManager;

import src.deedplotter.containers.Docker;
    
use namespace mx_internal;

/**
 *  Dispatched when the <code>ToolBar</code> is docked.
 * 
 *  @eventType flash.events.Event
 */
[Event(name="dock", type="flash.events.Event")]

/**
 *  Dispatched when the <code>ToolBar</code> is poped up into a floating window.
 * 
 *  @eventType flash.events.Event
 */
[Event(name="float", type="flash.events.Event")]

/**
 *  Image to be used for the dragStrip icon.
 *  If not specified, the default image is used.
 */
[Style(name="dragStripIcon", type="String", inherit="no")]

/**
 *  The DockableToolBar container is used along with the Docker 
 *  container to add individual ToolBars within a Docker context.
 *  
 *  @mxml
 *
 *  <p>The <code>&lt;fc:DockableToolBar&gt;</code> tag inherits all the tag attributes
 *  of its superclass, and adds the following tag attributes:</p>
 *
 *  <pre>
 *  &lt;fc:DockableToolBar
 *    <b>Properties</b>
 *    draggable="true"
 *    initialPosition="top"
 *
 *    <b>Events</b>
 *    dock="<i>No default</i>"
 *    float="<i>No default</i>"
 *  /&gt;
 *  </pre>
 *
 *  @includeExample ../../../../../../docs/examples/MultipleToolBars/MultipleToolBars.mxml
 *
 *  @see com.adobe.flex.extras.containers.Docker
 *
 */

public class DockableToolBar extends TitleWindow
{

	/**
	 * Flag which indicates whether this ToolBar can be dragged by user.
	 */
	public var draggable:Boolean = true;

	/**
	 * The initial location of the ToolBar. This can be either "top" or "bottom".
	 */
	public var initialPosition:String = "top";
	
	/**
	 * @private
	 * A reference to the Docker object to which this ToolBar would be Docked.
	 */
	mx_internal var docker:Docker;
	
	private var dragCursorStartX:Number;
	private var dragCursorStartY:Number;

	private var dockedHeight:Number;
	private var dockedWidth:Number;

	private var estimatedWidth:Number;
    private var toolBarHeightForRestore:Number;
    
    private var resizeEffect:Resize = new Resize(this);
    private var minimizeButton:Button;
    
	/**
	 *  Constructor.
	 */
	public function DockableToolBar()
	{
		super();
	}

	/**
	 * Indicates whether the ToolBar is currently docked.
	 */
	private var _docked:Boolean = true;

	public function get docked():Boolean {
		return _docked;
	}

	/**
	 * Indicates whether the ToolBar is currently minimized.
	 */
    private var minimizedChanged:Boolean = false;
    private var _minimized:Boolean = false;

    public function get minimized():Boolean {
        return _minimized;
    }

    public function set minimized(value:Boolean):void {

        _minimized = value;

        minimizedChanged = true;
        invalidateDisplayList();
    }
    
	override protected function createChildren():void
	{
		super.createChildren();

        if (!minimizeButton)
        {
            minimizeButton = new Button();
            minimizeButton.styleName = "dockableToolbarMinimizeButton";
            minimizeButton.explicitWidth = minimizeButton.explicitHeight = 20;
            minimizeButton.focusEnabled = false;
            minimizeButton.visible = true;
            minimizeButton.enabled = true;
            minimizeButton.addEventListener( MouseEvent.MOUSE_DOWN, minimizeButton_mouseDownHandler );

            titleBar.addChild(minimizeButton);
			minimizeButton.owner = this;
        }

        titleBar.addEventListener(MouseEvent.MOUSE_DOWN, titleBar_mouseDownHandler);
	}
    
    override protected function layoutChrome(unscaledWidth:Number, unscaledHeight:Number):void {
        super.layoutChrome(unscaledWidth, unscaledHeight);
        
        var bm:EdgeMetrics = borderMetrics;
        
        var x:Number = bm.left;
        var y:Number = bm.top;
        
        var headerHeight:Number = getHeaderHeight();
        
        if (minimizeButton && minimizeButton.visible)
        {
            minimizeButton.setActualSize(
                minimizeButton.getExplicitOrMeasuredWidth(),
                minimizeButton.getExplicitOrMeasuredHeight());

            minimizeButton.move(
                unscaledWidth - x - bm.right - 10 -
                minimizeButton.getExplicitOrMeasuredWidth(),
                (headerHeight -
                minimizeButton.getExplicitOrMeasuredHeight()) / 2);
        }
    }
    
    override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
        
        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        if ( unscaledHeight == 0 ) return;
        
        if ( minimizedChanged ) {
            minimizedChanged = false;
            
            if ( minimized ) {
                minimizePanel();
            } else {
                restorePanel();
            }
        }
    }
    
    protected function minimizePanel():void 
    {
        if (resizeEffect.isPlaying) {
            resizeEffect.reverse();
        } else {
            toolBarHeightForRestore = this.height;

            resizeEffect.heightTo = getHeaderHeight();
            resizeEffect.play();
        }

        minimizeButton.styleName = "dockableToolbarRestoreButton";
    }
    
    protected function restorePanel():void 
    {
        if (resizeEffect.isPlaying) {
            resizeEffect.reverse();
        } else {
            resizeEffect.heightTo = toolBarHeightForRestore;
            resizeEffect.play();
        }

        minimizeButton.styleName = "dockableToolbarMinimizeButton";
    }

	protected function makePanelDocked():void 
	{
/*         if (resizable)
            percentWidth = 100;
 */
        height = dockedHeight;
        
		_docked = true;

        minimizeButton.width = minimizeButton.height = 20;
        minimizeButton.enabled = true;
            
		dispatchEvent(new Event("dock"));
	}
	
	protected function makePanelFloated(pt:Point):void {

        minimizeButton.width = minimizeButton.height = 1;
        minimizeButton.enabled = false;

        invalidateDisplayList();

    	if (parent is HBox && parent.numChildren == 1)
			parent.parent.removeChild(parent);

		parent.removeChild(this);

		mx.managers.PopUpManager.addPopUp(this, docker);
        this.owner = docker;

		x = pt.x;
		y = pt.y;

		if (x < - width / 2) x = - width / 2;
		if (x > systemManager.screen.width - width / 2) x = systemManager.screen.width - width / 2;

		width = estimatedWidth;
        dockedHeight = Math.max(height, isNaN(toolBarHeightForRestore) ? height : toolBarHeightForRestore);

        validateNow();

		_docked = false;
		minimized = false;

		dispatchEvent(new Event("float"));
	}
	        
    override protected function startDragging(event:MouseEvent):void
    {
        systemManager.addEventListener( MouseEvent.MOUSE_MOVE, system_mouseMoveHandler, true );

        super.startDragging(event);
    }

    override protected function stopDragging():void
    {
        systemManager.removeEventListener( MouseEvent.MOUSE_MOVE, system_mouseMoveHandler, true );

        super.stopDragging();
    }

	protected function titleBar_mouseDownHandler(event:MouseEvent):void
	{
		if (!docker)
			return;

		var pt:Point = this.globalToLocal( new Point(event.stageX, event.stageY) );
		dragCursorStartX = pt.x;
		dragCursorStartY = pt.y;

		systemManager.addEventListener(MouseEvent.MOUSE_MOVE, system_mouseMoveHandler);
		systemManager.addEventListener(MouseEvent.MOUSE_UP, system_mouseUpHandler);
		systemManager.stage.addEventListener(Event.MOUSE_LEAVE, system_mouseLeaveHandler);

        estimatedWidth = width;

		event.stopPropagation();
	}

    protected function minimizeButton_mouseDownHandler(event:MouseEvent):void 
    {
        event.preventDefault();
        event.stopPropagation();

        minimized = !minimized;
    }

    protected function system_mouseLeaveHandler(event:Event):void 
    {
        //stop any dragging
        docker.dragProxy.graphics.clear();
        
        systemManager.removeEventListener(MouseEvent.MOUSE_UP, system_mouseUpHandler);
        systemManager.removeEventListener(MouseEvent.MOUSE_MOVE, system_mouseMoveHandler);
        systemManager.stage.removeEventListener(Event.MOUSE_LEAVE, system_mouseLeaveHandler);
    }
    
	protected function system_mouseUpHandler(event:MouseEvent):void
	{
		var prevDocked:Boolean = docked;
		
		if ( docker.dragOver(this, event, true) ) {
		    
		    if (!docked)
		        makePanelDocked();

    		docker.invalidateDisplayList();
    		docker.dragProxy.graphics.clear();
    		
		} else {
		    
		    if (docked)
		        makePanelFloated( new Point(event.stageX - dragCursorStartX, event.stageY - dragCursorStartY) );
		}

		systemManager.removeEventListener(MouseEvent.MOUSE_UP, system_mouseUpHandler);
		systemManager.removeEventListener(MouseEvent.MOUSE_MOVE, system_mouseMoveHandler);
	}
	
	protected function system_mouseMoveHandler(event:MouseEvent):void
	{
		var overDockingArea:Boolean = docker.dragOver(this, event);

        if (docked) {
    		var g:Graphics = docker.dragProxy.graphics;
    		var dragObject:UIComponent = UIComponent(this);

    		g.lineStyle(1, 0x000000, 0.5);
    		g.beginFill(0xFFFFFF, 0.45);
    		g.drawRect(event.stageX - dragCursorStartX, event.stageY - dragCursorStartY, dragObject.width, dragObject.height);
    		g.endFill();
        }
	}

}

}
