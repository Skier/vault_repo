package com.fxcomponents.controls
{
import com.fxcomponents.controls.fxvideo.PlayPauseButton;
import com.fxcomponents.controls.fxvideo.StopButton;
import com.fxcomponents.controls.fxvideo.VolumeButton;

import flash.events.ContextMenuEvent;
import flash.events.MouseEvent;
import flash.events.ProgressEvent;
import flash.net.URLRequest;
import flash.net.navigateToURL;
import flash.text.TextField;
import flash.text.TextFieldAutoSize;
import flash.text.TextFormat;
import flash.ui.ContextMenu;
import flash.ui.ContextMenuBuiltInItems;
import flash.ui.ContextMenuItem;

import mx.controls.VideoDisplay;
import mx.core.UIComponent;
import mx.events.MetadataEvent;
import mx.events.SliderEvent;
import mx.events.VideoEvent;

/**
 *  The color of the control bar. 
 *  
 *  @default 0x555555
 */

[Style(name="backColor", type="uint", format="Color", inherit="no")]

/**
 *  The color of the buttons on the control bar. 
 *  
 *  @default 0xeeeeee
 */

[Style(name="frontColor", type="uint", format="Color", inherit="no")]

/**
 *  The height of the control bar. Odd values look better. 
 *  
 *  @default 21
 */

[Style(name="controlBarHeight", type="Number", inherit="no")]

/**
 *  The name of the font used in the timer.
 *
 *  @default "Verdana"
 */
[Style(name="timerFontName", type="String", inherit="no")]

/**
 *  The size of the font used in the timer. 
 *  
 *  @default 9
 */

[Style(name="timerFontSize", type="Number", inherit="no")]

/**
 *  The FXVideo control lets you play an FLV file in a Flex application. 
 *  It supports progressive download over HTTP, streaming from the Flash Media
 *  Server, and streaming from a Camera object.
 * 
 *  @mxml
 *
 *  <p>The <code>&lt;controls:FXVideo&gt;</code> tag inherits all the tag
 *  attributes of its superclass, and adds the following tag attributes:</p>
 *
 *  <pre>
 *  &lt;controls:FXVideo
 *    
 *    <b>Styles</b>
 *    backColor="0x555555"
 *    frontColor="0xeeeeee"
 *    controlBarHeight="21"
 *    timerFontName="Verdana"
 *    timerFontSize="9"
 *
 *  /&gt;
 *  </pre>
 *
 */

public class FXVideo extends VideoDisplay
{
	
	/**
     *  Constructor.
     */
	
	public function FXVideo() 
	{
		super();
		
		textFormat = new TextFormat();
		
		var newContextMenu:ContextMenu;
		newContextMenu = new ContextMenu();
		
		newContextMenu.hideBuiltInItems();
        var defaultItems:ContextMenuBuiltInItems = newContextMenu.builtInItems;
        defaultItems.print = true;
		
		var item:ContextMenuItem = new ContextMenuItem("About FX Video Player");
		item.addEventListener(ContextMenuEvent.MENU_ITEM_SELECT, onMenuItemSelect);
		newContextMenu.customItems.push(item);
		
        contextMenu = newContextMenu;
	}
	
	private var textFormat:TextFormat;
	private var thumbHookedToPlayhead:Boolean = true;
	private var volumeBeforeMute:Number = 0;
	private var loadProgress:Number = 0;
	private var flo:Boolean = true;
	
	/** display objects */
	
	private var controlBar:UIComponent;
	private var playheadSlider:FXProgressSlider;
	private var volumeSlider:FXSlider;
	private var videoArea:UIComponent;
	private var ppButton:PlayPauseButton;
	private var stopButton:StopButton;
	private var volumeButton:VolumeButton;
	private var timerTextField:TextField;
	
	/** style */
	
	private var _frontColor:uint;
	private var _backColor:uint;
	private var _controlBarHeight:uint;
	private var _timerFontName:String;
	private var _timerFontSize:Number;
	
	/** properties */
	
	private var _adjustVolumeOnScroll:Boolean = true;
	
	/**
     *  Specifies whether the volume should adjust when users
     *  scroll over the video.
     *
     *  @default true
     */
	public function set adjustVolumeOnScroll(value:Boolean):void
	{
		_adjustVolumeOnScroll = value;
	}
	
	/**
     *  @private
     */
	
	public function get adjustVolumeOnScroll():Boolean
	{
		return _adjustVolumeOnScroll;
	}
	
	/** */
	
	private var _playPressed:Boolean;
	
	private function set playPressed(value:Boolean):void
	{
		_playPressed = value;
		
		(value) ? ppButton.state = "pause" : ppButton.state = "play"
	}
	
	private function get playPressed():Boolean
	{
		return _playPressed;
	}
	
	/**
	 * Creates any child components of the component. For example, the
	 * ComboBox control contains a TextInput control and a Button control
	 * as child components.
	 */
	
	override protected function createChildren():void
	{
		super.createChildren();
		
		// sets default values for styles
		
		_frontColor = getStyle("frontColor");
		if(getStyle("frontColor") == undefined)
			_frontColor = 0xcccccc;
		
		_backColor = getStyle("backColor");
		if(getStyle("backColor") == undefined)
			_backColor = 0x555555;
		
		_controlBarHeight = getStyle("controlBarHeight");
		if(!_controlBarHeight)
			_controlBarHeight = 21;
		
		_timerFontName = getStyle("timerFontName");
        if(!_timerFontName)
        	_timerFontName = "Verdana";
		
		_timerFontSize = getStyle("timerFontSize");
        if(!_timerFontSize)
        	_timerFontSize = 9;
		
		addEventListener(MetadataEvent.METADATA_RECEIVED, onMetadataReceived);
		addEventListener(MouseEvent.MOUSE_WHEEL, onMouseWheel);
		addEventListener(ProgressEvent.PROGRESS, onProgress);
		addEventListener(VideoEvent.PLAYHEAD_UPDATE, onPlayheadUpdate);
		addEventListener(VideoEvent.STATE_CHANGE, onStateChange);
		addEventListener(VideoEvent.REWIND, onRewind);
		addEventListener(VideoEvent.COMPLETE, onComplete);
		addEventListener(VideoEvent.READY, onReady);
		
		videoArea = new UIComponent();
		addChild(videoArea);
		
		videoArea.addEventListener(MouseEvent.CLICK, pp_onClick);
		
		controlBar = new UIComponent();
		addChild(controlBar);
		
		playheadSlider = new FXProgressSlider();
		controlBar.addChild(playheadSlider);
		
		playheadSlider.addEventListener(SliderEvent.CHANGE, playhead_onChange);
		playheadSlider.addEventListener(SliderEvent.THUMB_PRESS, onThumbPress);
		playheadSlider.addEventListener(SliderEvent.THUMB_RELEASE, onThumbRelease);
		playheadSlider.addEventListener(MouseEvent.MOUSE_DOWN, onMouseDown);
		playheadSlider.addEventListener(SliderEvent.THUMB_DRAG, onThumbDrag);
		
		volumeSlider = new FXSlider();
		controlBar.addChild(volumeSlider);
		
		volumeSlider.addEventListener(SliderEvent.CHANGE, volume_onChange);
		
		ppButton = new PlayPauseButton();
		controlBar.addChild(ppButton);
		
		ppButton.addEventListener(MouseEvent.CLICK, pp_onClick);
		
		stopButton = new StopButton();
		controlBar.addChild(stopButton);
		
		stopButton.addEventListener(MouseEvent.CLICK, stop_onClick);
		
		volumeButton = new VolumeButton();
		controlBar.addChild(volumeButton);
		
		volumeButton.addEventListener(MouseEvent.CLICK, volume_onClick);
		
		timerTextField = new TextField();
		controlBar.addChild(timerTextField);
        
        (autoPlay) ? playPressed = true : playPressed = false
	}
	
	/**
	 * Commits any changes to component properties, either to make the 
	 * changes occur at the same time, or to ensure that properties are set in 
	 * a specific order.
	 */
	
	override protected function commitProperties():void
	{
		super.commitProperties();
		
		ppButton.iconColor = _frontColor;
        stopButton.iconColor = _frontColor;
        volumeButton.iconColor = _frontColor;
		
		playheadSlider.setStyle("thumbColor", _frontColor);
		playheadSlider.setStyle("thumbOutlineColor", _backColor);
		
		volumeSlider.maximum = 1;
		volumeSlider.value = volume;
		volumeSlider.setStyle("thumbColor", _frontColor);
		volumeSlider.setStyle("thumbOutlineColor", _backColor);
		
		textFormat.color = _frontColor;
		textFormat.font = _timerFontName;
		textFormat.size = _timerFontSize;
		
		timerTextField.defaultTextFormat = textFormat;
		timerTextField.text = "Loading";
		timerTextField.selectable = false;
		timerTextField.autoSize = TextFieldAutoSize.LEFT;
	}
	
	/**
	 * Sizes and positions the children of the component on the screen based on 
	 * all previous property and style settings, and draws any skins or graphic 
	 * elements used by the component. The parent container for the component 
	 * determines the size of the component itself.
	 */
	
	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
    {
        super.updateDisplayList(unscaledWidth, unscaledHeight);
        
        var h:uint = _controlBarHeight;
        
        // draw
        
        videoArea.graphics.clear();
        videoArea.graphics.beginFill(0xffcccc, 0);
        videoArea.graphics.drawRect(0, 0, unscaledWidth, unscaledHeight);
        
        controlBar.graphics.clear();
		controlBar.graphics.beginFill(_backColor);
		controlBar.graphics.drawRect(0, 0, unscaledWidth, h);
        
        // size
        
        controlBar.setActualSize(unscaledWidth, h);
        ppButton.setActualSize(h, h);
        stopButton.setActualSize(h, h);
        volumeButton.setActualSize(h, h);
        playheadSlider.setActualSize(unscaledWidth - ppButton.width - stopButton.width - volumeButton.width - 180, 9);
        volumeSlider.setActualSize(80, 9);
        
        // position
        
		controlBar.x = 0;
		controlBar.y = unscaledHeight - 1;
        
        ppButton.x = 0;
        ppButton.y = 0;
        
        stopButton.x = ppButton.width;
        stopButton.y = 0;
        
        playheadSlider.x = stopButton.x + stopButton.width + 6;
        playheadSlider.y = (controlBar.height - playheadSlider.height)/2;
		
        timerTextField.x = playheadSlider.x + playheadSlider.width + 10;
        timerTextField.y = (controlBar.height - timerTextField.height)/2;
        
        volumeButton.x = unscaledWidth - volumeSlider.width - 12 - volumeButton.width;
        volumeButton.y = 0;
        
        volumeSlider.x = unscaledWidth - volumeSlider.width - 6;
        volumeSlider.y = (controlBar.height - volumeSlider.height)/2;;
    }
    
    /** MouseEvent */
    
	private function pp_onClick(event:MouseEvent):void
	{
		
		if(playPressed)
		{
			pause();
			playPressed = false;
		}
		else
		{
			play();
			playPressed = true;
		}
	}
	
	private function stop_onClick(event:MouseEvent):void
	{
		stop();
		playPressed = false;
	}
	
	private function volume_onClick(event:MouseEvent):void
	{
		if(volume == 0)
		{
			volume = volumeSlider.value = volumeBeforeMute;
		}
		else
		{
			volumeBeforeMute = volume;
			volume = volumeSlider.value = 0;
		}
	}
	
	private function onMouseDown(event:MouseEvent):void
	{
		thumbHookedToPlayhead = false;
	}
	
	private function onMouseWheel(event:MouseEvent):void
	{
		if(!_adjustVolumeOnScroll)
			return;
			
		volume += event.delta/Math.abs(event.delta)*.05;
		volumeSlider.value = volume
	}
	
	/** SliderEvent */
	
	private function onThumbPress(event:SliderEvent):void
	{
		thumbHookedToPlayhead = false;
	}
	
	private function onThumbRelease(event:SliderEvent):void
	{
		thumbHookedToPlayhead = true;
	}
	
	private function onThumbDrag(event:SliderEvent):void
	{
		timerTextField.text = formatTime(event.value)+" / "+formatTime(totalTime);
	}
	
	private function playhead_onChange(event:SliderEvent):void
	{
		thumbHookedToPlayhead = true;
		playheadTime = event.currentTarget.value;
	}
	
	private function volume_onChange(event:SliderEvent):void
	{
		volume = event.currentTarget.value;
	}
	
	/** */
	
	private function onRewind(event:VideoEvent):void
	{
		
	}
	
	private function onMetadataReceived(event:MetadataEvent):void
	{
		playheadSlider.maximum = Math.round(totalTime);
		trace("totalTime: "+ totalTime);
		play();
		
		updateTimer();
	}
	
	private function onPlayheadUpdate(event:VideoEvent):void
	{
		if(thumbHookedToPlayhead)
		{
			playheadSlider.value = Math.round(event.playheadTime);
			
			updateTimer();
		}
		
		if(flo)
		{
			thumbHookedToPlayhead = false;
		}
		
		if(flo && playheadTime > 0)
		{
			thumbHookedToPlayhead = true;
			
			stop();
			
			flo = false;
			
			if(_playPressed)
			{	
				play();
			}
		}
	}
	
	private function onStateChange(event:VideoEvent):void
	{
		//trace("state: "+event.state+" : "+event.stateResponsive);
		
		if(event.state == VideoEvent.CONNECTION_ERROR)
		{
			timerTextField.text = "Conn Error";
		}
	}
	
	private function onReady(event:VideoEvent):void
	{
		play();
	}
	
	private function onComplete(event:VideoEvent):void
	{
		playPressed = false;
	}
	
	private function onProgress(event:ProgressEvent):void
	{
		loadProgress = Math.floor(event.bytesLoaded/event.bytesTotal*100);
		var playheadProgress:Number = Math.floor(playheadTime/totalTime*100);
		
		playheadSlider.progress = loadProgress;
	}
	
	private function onMenuItemSelect(event:ContextMenuEvent):void
	{
		navigateToURL(new URLRequest("http://www.fxcomponents.com/?p=29"));
	}
	
	/** functions */
	
	private function formatTime(value:int):String
	{
		var result:String = (value % 60).toString();
        if (result.length == 1)
            result = Math.floor(value / 60).toString() + ":0" + result;
        else 
            result = Math.floor(value / 60).toString() + ":" + result;
        return result;
	}
	
	private function formatVolume(value:Number):Number
	{
		var result:Number = Math.round(value*100);
		
		return result;
	}
	
	private function updateTimer():void
	{
		timerTextField.text = formatTime(playheadTime)+" / "+formatTime(totalTime);
	}
}
}