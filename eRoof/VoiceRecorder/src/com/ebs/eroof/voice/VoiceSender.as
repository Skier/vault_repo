package com.ebs.eroof.voice
{
	import flash.events.Event;
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;
	import flash.net.URLLoader;
	import flash.net.URLLoaderDataFormat;
	import flash.net.URLRequest;
	import flash.net.URLRequestHeader;
	import flash.net.URLRequestMethod;
	import flash.utils.ByteArray;
	
	import mx.events.DynamicEvent;
	import mx.utils.Base64Decoder;
	import mx.utils.Base64Encoder;
	
	public class VoiceSender extends EventDispatcher
	{
		private var ticket:String;
		private var errtext:String;
		
		private var tableKey:String;
		private var soundFieldId:String;
		private var soundFileName:String;
		
		public function send(sound:ByteArray):void 
		{
			if (ticket)
			{
				sendSound(sound);
			} else 
			{
				addEventListener("loginResult", 
					function (event:Event):void 
					{
						dispatchEvent(new Event("loginComplete"));
						sendSound(sound);
					});
				addEventListener("loginFault", 
					function (event:Event):void 
					{
						dispatchFaultEvent("Login Fault");
					});
				login(Config.DEFAULT_USERNAME, Config.DEFAULT_PASSWORD);
			}
		}
		
		private function dispatchFaultEvent(value:String):void
		{
			var event:DynamicEvent = new DynamicEvent("voiceSenderFault");
			event.errtext = value;
			dispatchEvent(event);
		}
		
		private function login(username:String, password:String):void 
		{
			var request:URLRequest = new URLRequest();
			request.url = Config.WORKPLACE_URL + "/db/main?a=API_Authenticate&username=" + username + "&password=" + password;
			request.method = URLRequestMethod.GET;
			
			var loader:URLLoader = new URLLoader(request);
			loader.dataFormat = URLLoaderDataFormat.TEXT;
			loader.addEventListener(Event.COMPLETE, 
				function (event:Event):void 
				{
					var xml:XML = new XML(loader.data);
					if (xml.errcode == 0)
					{
						ticket = xml.ticket;
						dispatchEvent(new Event("loginResult"));
					} else 
					{
						dispatchFaultEvent(xml.errtext);
					}
				});
		}
		
		private function sendSound(sound:ByteArray):void 
		{
			var soundStr:String = encodeSound(sound);
			
			var request:URLRequest = new URLRequest();
			request.url = Config.WORKPLACE_URL + "/db/" + Config.PHOTO_TABLE_ID + "?act=API_AddRecord";
			request.method = URLRequestMethod.POST;
			request.contentType = "application/xml";
			request.data = getRequestContent(soundStr);
			
			var loader:URLLoader = new URLLoader(request);
			loader.dataFormat = URLLoaderDataFormat.TEXT;
			loader.addEventListener(Event.COMPLETE, 
				function (event:Event):void 
				{
					var xml:XML = new XML(loader.data);
					if (xml.errcode == 0)
					{
						dispatchEvent(new Event("sendComplete"));
					} else 
					{
						dispatchFaultEvent(xml.errtext);
					}
				});
		}
		
		private function encodeSound(sound:ByteArray):String 
		{
			var encoder:Base64Encoder = new Base64Encoder();
			encoder.insertNewLines = false;
			encoder.encodeBytes(sound);
			return encoder.toString();
		}
		
		private function getRequestContent(soundStr:String):String 
		{
			var result:String = "";
			
			result += "<qdbapi>";
			result += ("<ticket>" + ticket + "</ticket>");
			result += ("<apptoken>" + Config.APPLICATION_TOKEN + "</apptoken>");
			result += "<field name=\"PhotoDate\">06-29-2010 09:22 AM</field>";
			result += "<field name=\"Annotation\">annotation</field>";
			result += "<field name=\"PhotoLatLng\">43.425414,-80.438632</field>";
			result += "<field fid=\"10\">1</field>";
			result += "<field fid=\"" + soundFieldId + "\" filename=\"" + soundFileName + "\">";
			result += soundStr;
			result += "</field>";
			result += "</qdbapi>";
			
			return result;
		}
		
		public function VoiceSender(tableKey:String, soundFieldId:String, soundFileName:String)
		{
			super();
			
			this.tableKey = tableKey;
			this.soundFieldId = soundFieldId;
			this.soundFileName = soundFileName;
		}
	}
}