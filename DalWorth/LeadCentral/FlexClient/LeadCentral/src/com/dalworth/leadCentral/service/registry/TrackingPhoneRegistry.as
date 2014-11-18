package com.dalworth.leadCentral.service.registry
{
	public class TrackingPhoneRegistry extends BaseRegistry
	{
		private static var _instance:TrackingPhoneRegistry;
		public static function getInstance():TrackingPhoneRegistry
		{
			if (_instance == null)
				_instance = new TrackingPhoneRegistry(new Private());
			
			return _instance;
		}
		
		public function TrackingPhoneRegistry(accessPrivate:Private) 
		{
			super();
		}
	}
}

class Private {}
