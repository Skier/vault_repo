package src.common
{
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.TimerEvent;
	import flash.utils.Timer;
	
	import mx.controls.Alert;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	import mx.rpc.remoting.RemoteObject;
	
	public class Pinger extends EventDispatcher
	{
		public static const EVENT_PING_OK:String = "ping_ok";
		public static const EVENT_PING_FAILED:String = "ping_failed";
		
		private var remoteObject:RemoteObject;
		private var timer:Timer;
		private var connected:Boolean = true;
		
		public function Pinger(delay:int):void {
			remoteObject = new RemoteObject("GenericDestination");
			remoteObject.source = "TractInc.ScopeScetch.Pinger";
			remoteObject.addEventListener(ResultEvent.RESULT, onPingOk);
			remoteObject.addEventListener(FaultEvent.FAULT, onPingFault);
			
			timer = new Timer(delay, 0);
            timer.addEventListener(TimerEvent.TIMER, pingWeborb);
		}
		
		public function Start():void {
			timer.start();
			remoteObject.ping();
		}
		
		public function Stop():void {
			timer.stop();
		}
		
		private function pingWeborb(event:TimerEvent):void {
			remoteObject.ping();
		}
		
		private function onPingOk(event:ResultEvent):void {
			dispatchEvent(new Event(EVENT_PING_OK));
/* 			if (!connected) {
				connected = true;
				dispatchEvent(new Event(EVENT_PING_OK));
			}
 */
		}

		private function onPingFault(event:FaultEvent):void {
		    trace("ping failed. reponse was: " + event.fault.faultString + "; message: " + event.message);
			dispatchEvent(new Event(EVENT_PING_FAILED));
/* 			if (connected) {
				connected = false;
				dispatchEvent(new Event(EVENT_PING_FAILED));
			}
 */	
	 	}		
		
	}
}