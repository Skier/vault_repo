////////////////////////////////////////////////////////////////////////////////
//
// *Copyright (c) 2007 Uday M. Shankar
//
// The usual Yada-Yada!
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this code and associated documentation
// files (the "Code"), to deal in the Code without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Code is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Code.
//
// THE CODE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// Further it is worth to mention that no animals have been 
// harmed during the development. No trees have been cut 
// down. Womens rights have been treated with full respect.
// Mankind&apos;s safety has been ensured at every step.
//
// Peace!
//
// @file: ClientIdleTimeOut
// @authors: Uday M. Shankar, Venkatesh Ramadurai
// @date: 31-03-2007
// @description: ClientIdleTimeOut component to track user 
// inactivity and act there on.
//
////////////////////////////////////////////////////////////////////////////////
package AerSysCo.Util
{
    import flash.events.TimerEvent;
    import flash.utils.Timer;
    import flash.utils.getTimer;
    import mx.events.MoveEvent;
    import mx.core.Application;
    import flash.events.MouseEvent;
    import flash.events.KeyboardEvent;
    import flash.events.Event;
    import mx.managers.PopUpManager;
    import mx.events.FlexEvent;
    import mx.containers.VBox;
    import mx.controls.Text;
    import mx.containers.Canvas;
    import mx.effects.Blur;
    import mx.containers.HBox;
    import mx.controls.Image;
    import mx.controls.Spacer;
    import mx.controls.Button;
    import mx.events.DynamicEvent;
    import flash.events.EventDispatcher;

	[Event(name="timeOutPrompt", type="flash.events.Event")]

    /**
     *  CientIdleTimeOut is a component which when added to any flex application,
     *  timesout the application if the user is inactive for a set period of time. This 
     *  functionality is very useful in business applications which require user
     *  authentication and login. In such applications, if the user is inactive for 
     *  a certain period of time, the UI is timedout and disabled.
     *
     *  @mxml
     *
     *  <p>Using this component in the application is very simple. Use the following 
     *  syntax for basic usage:</p>
     * 
     *  &lt;flexed:ClientIdleTimeOut id="TimerId" onTimeOut="FunctionPassedFromCaller" listenKeyStroke="true|false" listenMouseMove="true|false" timeOutInterval="1" confirmInterval="1" /&gt;
     */    
    public class ClientIdleTimeOut extends EventDispatcher
    {
        
        //--------------------------------------------------------------------------
        //
        //  Class variables
        //
        //--------------------------------------------------------------------------
    
        private var _uintTimeOutInterval:uint = 0;
        private var _uintConfirmInterval:uint = 0;
        
        private var _timerTimeOut:Timer = new Timer(timeOutInterval);
        private var _timerConfirm:Timer = new Timer(confirmInterval);
        private var _iLastActivity:Number = 0;
        
        private var _mouseListener:Boolean = false;
        private var _keyListener:Boolean = true;
        
        [Embed("/assets/ico_alert.jpg")]
        private var alertIcon:Class;
        private var timoutPrompt:HBox = null;
        private var eventTimeOut:DynamicEvent = new DynamicEvent("appTimedOut");
        private var _timedOutFunction:Function = new Function();
        
        private const TIME_VALUE:uint = 60000; //as given value is to be treated as minutes
        
        //--------------------------------------------------------------------------
        //
        //  Constructor
        //
        //--------------------------------------------------------------------------

        /**
         *  Constructor.
         */
        public function ClientIdleTimeOut():void{
            _timerTimeOut.addEventListener(TimerEvent.TIMER, onTimeOutTimer);
            _timerConfirm.addEventListener(TimerEvent.TIMER, onConfirmTimer);
            
            (Application.application as Application).addEventListener(MouseEvent.MOUSE_MOVE, resetLastActivity);
            (Application.application as Application).addEventListener(KeyboardEvent.KEY_DOWN, resetLastActivity);
        }        
        
        /**
         *  To start the idle timer from the caller where the <code>ClientIdleTimer</code>
         *  has been used. The timer will start with the timeout intervals
         *  already passed in.
         */
        public function startTimer():void{
            _timerTimeOut.start();
        }
        
        /**
         *  To stop the idle timer from the caller where the <code>ClientIdleTimer</code>
         *  has been used. The timer will forcefuly stop whether the timeout 
         *  intervals have reached or not.
         */        
        public function stopTimer():void{
            _timerTimeOut.stop();
        }

        /**
         *  Getter & Setter for the time interval of inactivity after which the UI 
         *  has to timeout. The interval passed in is converted using the
         *  <code>TIME_VALUE</code>
         *
         *  @param timeoutInterval The time interval to be passed in 
         *  <b>minutes</b> from the caller.
         */        
        public function set timeOutInterval(timeoutInterval:uint):void{
            _uintTimeOutInterval = timeoutInterval * TIME_VALUE;
            _timerTimeOut.delay = timeoutInterval * TIME_VALUE;
            _timerTimeOut.start();
        }

        public function get timeOutInterval():uint{
            return _uintTimeOutInterval;
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
        public function set confirmInterval(confirmInterval:uint):void{
            _uintConfirmInterval = confirmInterval * TIME_VALUE;
            _timerConfirm.delay = confirmInterval * TIME_VALUE;
        }
        
        public function get confirmInterval():uint{
            return _uintConfirmInterval;
        }

        /**
         *  Getter & Setter for Keyboard listener. If this is set to false,
         *  the client time out will be only based on mouse movement. Even if 
         *  there are keystrokes for time interval specified, the UI will timeout
         *  as long as there are no mouse movements.
         *
         *  @param registerKeys The default value is true.
         */        
        public function set listenKeyStroke(registerKeys:Boolean):void{
            _keyListener = registerKeys;
            if(registerKeys == false) (Application.application as Application).removeEventListener(KeyboardEvent.KEY_DOWN, resetLastActivity);
        }

        public function get listenKeyStroke():Boolean{
            return _keyListener;
        }

        /**
         *  Getter & Setter for Keyboard listener. If this is set to false,
         *  the client time out will be only based on key strokes. Even if 
         *  there are mouse movements for time interval specified, the 
         *  UI will timeout as long as there are no key strokes.
         *
         *  @param registerMouse The default value is true.
         */        
        public function set listenMouseMove(registerMouse:Boolean):void{
            _mouseListener = registerMouse;
            if(registerMouse == false) (Application.application as Application).removeEventListener(MouseEvent.MOUSE_MOVE, resetLastActivity);
        }

        public function get listenMouseMove():Boolean{
            return _mouseListener;
        }
        
        /**
         *  Setter for onTimeOut function. A function from the caller can 
         *  be set here and will be executed when the idle timeout occurs.
         *
         *  @param timeoutFn Function of type void passed in by the caller.
         */        
        public function set onTimeOut(timeoutFn:Function):void{
            _timedOutFunction = timeoutFn;
        }

        /**
         *  @private
         *  Resets the timer whenever a mousemove or 
         *  keystroke occurs.
         */        
        private function resetLastActivity(event:Event):void{
            _iLastActivity = getTimer();
            if((Application.application as Application).enabled == true){
                if (_timerConfirm.running){
                    _timerConfirm.stop();
                }
                if(!_timerTimeOut.running){
                    _timerTimeOut.start();
                }
                if(timoutPrompt != null) hideTimeOutPrompt();
            }
         }

        /**
         *  @private
         *  When time out occurs, a confirmation prompt comes
         *  up which stays on for a specified time interval.
         */        
        private function onTimeOutTimer(event:TimerEvent = null):void{
             var iCurTimer:Number = getTimer();
            var iElapsed:Number = iCurTimer - _iLastActivity;
            
            if ( iElapsed >= timeOutInterval ) {
                showTimeOutPrompt();
                _timerTimeOut.stop();
           }
         }
         
        /**
         *  @private
         *  Fired on the Timer event. When the confirmation 
         *  timeout occurs, the time out prompt is hidden and 
         *  the application is disabled forcefully.
         */        
        private function onConfirmTimer(event:TimerEvent = null):void{
            var iCurTimer:Number = getTimer();
            var iElapsed:Number = iCurTimer - _iLastActivity;
            
            if ( iElapsed >= confirmInterval ) {
                hideTimeOutPrompt();
                _timerConfirm.stop();
                _timerTimeOut.stop();
                timeoutApp();
           }
        }
        
        /**
         *  @private
         *  When the confirmation prompt comes up, it starts 
         *  a timer to track the grace period.
         */        
        private function timeOutConfirmation(event:Event):void{
            _timerConfirm.start();
            _timerConfirm.addEventListener(TimerEvent.TIMER, onConfirmTimer);
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
        public function timeoutApp():void{
            eventTimeOut.expiryTime = new Date().toTimeString();
            (Application.application as Application).dispatchEvent(eventTimeOut);
            (Application.application as Application).enabled = false;
            _timedOutFunction();
        }
        
        /**
         *  @private 
         *  Executed on click of button on confirmation
         *  prompt. Hides the prompt and restarts the 
         *  idle timer.
         */        
        private function resetTimeOut(event:Event):void{
            _timerConfirm.stop();
            _timerTimeOut.start();
            (Application.application as Application).enabled = true;
            hideTimeOutPrompt();
        }

        /**
         *  @private 
         *  Pops up either a new confirm prompt using <code>PopUpManager</code>
         *  or if its already existing, makes it visible.
         */        
        private function showTimeOutPrompt():void{
            if(timoutPrompt == null){
                PopUpManager.addPopUp(createPopUp(), Application.application as Application, true);
            }else{
                timoutPrompt.visible = true;
            }
            
            dispatchEvent(new Event("timeOutPrompt"));
        }

        /**
         * @private 
         * Hides the confirm prompt.
         */        
        private function hideTimeOutPrompt():void{
            timoutPrompt.visible = false;
        }

        /**
         * @private 
         * Creates the confirmation prompt.
         */        
        private function createPopUp():HBox{
            timoutPrompt = new HBox();
            timoutPrompt.setStyle("backgroundColor",0xffffff);
            timoutPrompt.setStyle("cornerRadius",4);
            timoutPrompt.setStyle("borderStyle","solid");
            timoutPrompt.setStyle("borderColor",0xd50000);
            timoutPrompt.setStyle("borderThickness",2);
            timoutPrompt.setStyle("alpha",1);
            timoutPrompt.width = 350;
            timoutPrompt.height = 90;
            timoutPrompt.x=(Application.application.width/2) - (timoutPrompt.width/2);
            timoutPrompt.y=(Application.application.height/2) - (timoutPrompt.height/2);
            timoutPrompt.verticalScrollPolicy = "off";
            timoutPrompt.horizontalScrollPolicy = "off";
            
            var imgIcon:Image = new Image();imgIcon.source = alertIcon;
            timoutPrompt.addChild(imgIcon);
            
            var txtCon:VBox = new VBox();
            txtCon.setStyle("verticalGap",1);
            txtCon.setStyle("horizontalAlign","center");
            txtCon.percentHeight = 100;
            txtCon.percentWidth = 100;
            
            var spacer:Spacer = new Spacer();
            spacer.height = 20;
            spacer.percentWidth = 100;
            txtCon.addChild(spacer);
            
            var promptText:Text = new Text();
            promptText.htmlText = "Move the mouse or type within <b>" + confirmInterval / TIME_VALUE + "</b> minutes <br>to stop automatic TIMEOUT.";
            promptText.setStyle("textAlign","center");
            txtCon.addChild(promptText);
            
            var btnKeepAlive:Button = new Button();
            btnKeepAlive.width = 100;
            btnKeepAlive.height = 22;
            btnKeepAlive.name = "btnKeepAlive";
            btnKeepAlive.label = "Stay Alive!";
            btnKeepAlive.addEventListener("click",resetTimeOut);
            txtCon.addChild(btnKeepAlive);
            txtCon.defaultButton = btnKeepAlive;
            
            timoutPrompt.addChild(txtCon);
            timoutPrompt.addEventListener(FlexEvent.INITIALIZE, timeOutConfirmation);
            timoutPrompt.addEventListener(FlexEvent.SHOW, timeOutConfirmation);
            
            return timoutPrompt;
        }
    }
}
