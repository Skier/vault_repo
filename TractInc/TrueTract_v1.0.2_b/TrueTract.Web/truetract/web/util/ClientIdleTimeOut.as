package truetract.web.util
{
import flash.display.DisplayObject;
import flash.events.Event;
import flash.events.EventDispatcher;
import flash.events.KeyboardEvent;
import flash.events.MouseEvent;
import flash.events.TimerEvent;
import flash.utils.Timer;
import flash.utils.getTimer;

import mx.containers.Canvas;
import mx.containers.HBox;
import mx.containers.VBox;
import mx.controls.Button;
import mx.controls.Image;
import mx.controls.ProgressBar;
import mx.controls.ProgressBarMode;
import mx.controls.Spacer;
import mx.controls.Text;
import mx.core.Application;
import mx.effects.Blur;
import mx.events.DynamicEvent;
import mx.events.FlexEvent;
import mx.events.MoveEvent;
import mx.managers.PopUpManager;

[Event(name="appTimedOut", type="mx.events.DynamicEvent")]

public class ClientIdleTimeOut extends EventDispatcher
{
	public static const APP_TIME_OUT_EVENT:String = "appTimedOut";

	//--------------------------------------------------------------------------
    //
    //  Class variables
    //
    //--------------------------------------------------------------------------

	private var timeOutTimer:Timer = new Timer(1000);
	private var confirmTimer:Timer = new Timer(1000);

	private var lastActivity:Number = 0;
	
	[Embed("clientIdleTimeOutClasses/ico_alert.jpg")]
    private var alertIcon:Class;
    
    private var timeOutPrompt:HBox = null;
    private var timeOutProgressBar:ProgressBar;
    
    private var enabled:Boolean = true;
	
	//--------------------------------------------------------------------------
	//
	//  Constructor
	//
	//--------------------------------------------------------------------------

	/**
	 *  Constructor.
	 */
	public function ClientIdleTimeOut():void{
		timeOutTimer.addEventListener(TimerEvent.TIMER, timeOutTimer_tickHandler);
		confirmTimer.addEventListener(TimerEvent.TIMER, confirmTimer_tickHandler);

		Application.application.systemManager.addEventListener(MouseEvent.MOUSE_MOVE, user_activityHandler);
		Application.application.systemManager.addEventListener(MouseEvent.MOUSE_DOWN, user_activityHandler);
		Application.application.systemManager.addEventListener(KeyboardEvent.KEY_DOWN, user_activityHandler);
	}		
	
	/**
     *  Getter & Setter for the time interval of inactivity after which the UI 
     *  has to timeout. The interval passed in is converted using the
     *  <code>TIME_VALUE</code>
     *
     *  @param timeoutInterval The time interval to be passed in 
     *  <b>minutes</b> from the caller.
     */		
	private var _timeOutInterval:int = 0;
     
	public function set timeOutInterval(timeoutInterval:Number):void{
		_timeOutInterval = timeoutInterval * 60000;
	}

	public function get timeOutInterval():Number{
		return _timeOutInterval;
	}

	/**
     *  Getter & Setter for the time interval of inactivity after which 
     *  the timeout confirmation prompt expires and the application 
     *  is disabled. The interval passed in is converted using the
     *  <code>TIME_VALUE</code>
     *
     *  @param confirmInterval The time interval to be passed in 
     *  <b>minutes</b> from the caller.
     */		
	private var _confirmInterval:int = 0;
     
	public function set confirmInterval(confirmInterval:Number):void {
		_confirmInterval = confirmInterval * 60000;
	}
	
	public function get confirmInterval():Number {
		return _confirmInterval;
	}

	/**
     *  Getter & Setter for Keyboard listener. If this is set to false,
     *  the client time out will be only based on mouse movement. Even if 
     *  there are keystrokes for time interval specified, the UI will timeout
     *  as long as there are no mouse movements.
     *
     *  @param registerKeys The default value is true.
     */		
	private var _listenKeyStroke:Boolean = true;

	public function set listenKeyStroke(value:Boolean):void{
		_listenKeyStroke = value;
		if( !value ) {
		    Application.application.removeEventListener(KeyboardEvent.KEY_DOWN, user_activityHandler);
		}
	}

	public function get listenKeyStroke():Boolean{
		return _listenKeyStroke;
	}

	/**
     *  Getter & Setter for Keyboard listener. If this is set to false,
     *  the client time out will be only based on key strokes. Even if 
     *  there are mouse movements for time interval specified, the 
     *  UI will timeout as long as there are no key strokes.
     *
     *  @param registerMouse The default value is true.
     */
	private var _listenMouseMove:Boolean = false;
     
	public function set listenMouseMove(value:Boolean):void{
		_listenMouseMove = value;

		if( !value ) {
		    Application.application.removeEventListener(MouseEvent.MOUSE_MOVE, user_activityHandler);
		}
	}

	public function get listenMouseMove():Boolean{
		return _listenMouseMove;
	}
	
	/**
     *  To start the idle timer from the caller where the <code>ClientIdleTimer</code>
     *  has been used. The timer will start with the timeout intervals
     *  already passed in.
     */
	public function startTimer():void {
        lastActivity = getTimer();
	    timeOutTimer.start();
		enabled = false;
	}
	
	/**
     *  To stop the idle timer from the caller where the <code>ClientIdleTimer</code>
     *  has been used. The timer will forcefuly stop whether the timeout 
     *  intervals have reached or not.
     */		
	public function stopTimer():void{
		timeOutTimer.stop();
		enabled = true;
	}
	
	/**
	 *  @private
     *  Times out the application and then calls the 
     *  onTimeOut function specified in the caller.
     *  This method also dispatches a dynamic event of 
     *  type <b>appTimedOut</b>. The event has a 
     *  property - <code>expiryTime</code> which contains
     *  the time of timeout.
     */		
	private function timeOutApplication():void{
	    
	    enabled = true;
		confirmTimer.stop();
		timeOutTimer.stop();

        hideTimeOutPrompt();
        
        var event:DynamicEvent = new DynamicEvent(APP_TIME_OUT_EVENT);
        event.expiryTime = new Date().toTimeString();
		
		dispatchEvent(event);
		Application.application.dispatchEvent(event);
	}
	
	/**
     *  @private 
     *  Pops up either a new confirm prompt using <code>PopUpManager</code>
     *  or if its already existing, makes it visible.
     */		
	private function showTimeOutPrompt():void{
		if( timeOutPrompt == null ) {
		    var popup:HBox = createPopUp();
			PopUpManager.addPopUp(popup, DisplayObject(Application.application), true);
			PopUpManager.centerPopUp(popup);
		} else {
			timeOutPrompt.visible = true;
		}

        confirmTimer.start();
	}

	/**
     * @private 
     * Hides the confirm prompt.
     */		
	private function hideTimeOutPrompt():void{
		timeOutPrompt.visible = false;
	}

	/**
     * @private 
     * Creates the confirmation prompt.
     */		
	private function createPopUp():HBox{
		timeOutPrompt = new HBox();
		timeOutPrompt.styleName = "timeOutConfirmationDialog";
	    timeOutPrompt.verticalScrollPolicy = "off";
	    timeOutPrompt.horizontalScrollPolicy = "off";
	    
	    var imgIcon:Image = new Image();
	    imgIcon.source = alertIcon;

	    timeOutPrompt.addChild(imgIcon);
	    
	    var container:VBox = new VBox();
	    container.setStyle("verticalGap", 1);
		container.setStyle("horizontalAlign","center");
		
		var spacer:Spacer = new Spacer();
		spacer.height = 20;
		container.addChild(spacer);
		
		var promptText:Text = new Text();
		promptText.htmlText = "Move the mouse or type within <b>" + confirmInterval / 60000 + "</b> minutes <br>to stop automatic TIMEOUT.";
		promptText.setStyle("textAlign", "center");
		container.addChild(promptText);
		
		timeOutProgressBar = new ProgressBar();
		timeOutProgressBar.label = "";
		timeOutProgressBar.mode = ProgressBarMode.MANUAL;
		timeOutProgressBar.setStyle("verticalGap", 0);
		timeOutProgressBar.setStyle("fontSize", 1);
		container.addChild( timeOutProgressBar );

		var btnKeepAlive:Button = new Button();
		btnKeepAlive.name = "btnKeepAlive";
		btnKeepAlive.label = "Stay Alive!";
		btnKeepAlive.addEventListener("click", stayAlive_clickHandler);
		container.addChild(btnKeepAlive);
		container.defaultButton = btnKeepAlive;
		
		timeOutPrompt.addChild(container);

		return timeOutPrompt;
	}
	
	/**
     *  @private
     *  Resets the timer whenever a mousemove or 
     *  keystroke occurs.
     */		
	private function user_activityHandler(event:Event):void {
        if (enabled || confirmTimer.running) {
            return;
        }

		lastActivity = getTimer();
	}

	/**
     *  @private
     *  When time out occurs, a confirmation prompt comes
     *  up which stays on for a specified time interval.
     */		
	private function timeOutTimer_tickHandler(event:TimerEvent = null):void{
	 	var iCurTimer:Number = getTimer();
		var iElapsed:Number = iCurTimer - lastActivity;
		
		if ( iElapsed >= timeOutInterval ) {
			showTimeOutPrompt();
			timeOutTimer.stop();
	   }
	 }
	 
	/**
     *  @private
     *  Fired on the Timer event. When the confirmation 
     *  timeout occurs, the time out prompt is hidden and 
     *  the application is disabled forcefully.
     */		
	private function confirmTimer_tickHandler(event:TimerEvent = null):void{
		var iCurTimer:Number = getTimer();
		var iElapsed:Number = iCurTimer - lastActivity - timeOutInterval;
		
		timeOutProgressBar.setProgress(iElapsed, confirmInterval) ;

		if ( iElapsed >= confirmInterval ) {
			timeOutApplication();
	   }
	}
			
	/**
     *  @private 
     *  Executed on click of button on confirmation
     *  prompt. Hides the prompt and restarts the 
     *  idle timer.
     */		
	private function stayAlive_clickHandler(event:Event):void{
		confirmTimer.stop();
		timeOutTimer.start();
		lastActivity = getTimer();

        hideTimeOutPrompt();
	}		
}
}