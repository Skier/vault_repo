package com.llsvc.services
{
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;

	public class MainDispatcher extends EventDispatcher
	{
		private static var _instance:InvoiceService;
		
		public function MainDispatcher(target:IEventDispatcher=null)
		{
			if (_instance != null)
				throw new Error("Singleton !");
				
			super(target);
		}
		
		public static function get instance():MainDispatcher 
		{
			if (_instance == null)
				_instance = new MainDispatcher();
			
			return _instance;
		}
		
	}
}