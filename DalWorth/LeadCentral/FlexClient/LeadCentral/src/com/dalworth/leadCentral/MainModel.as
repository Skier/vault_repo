package com.dalworth.leadCentral
{
	import com.dalworth.leadCentral.domain.ServmanCustomer;
	import com.dalworth.leadCentral.domain.User;

	[Bindable]
	public class MainModel
	{
		public static const APPLICATION_ROOT_URL:String = "https://app.theleadcentral.com/";
		public static const INIT_MODEL_COMPLETE:String = "initMainModelComplete";
		public static const SESSION_EXPIRED_STRING:String = "SESSION EXPIRED";
		public static const UPDATE_INTERFACE:String = "updateInterface";

		public var currentRealm:String;
		public var currentDb:String;
		public var currentTicket:String;

		public var currentVersion:String = "v1.00b";

		public var currentUser:User;
		public var logContent:String = "";
		public var currentCustomer:ServmanCustomer;
		public var oAuthUrl:String;
		public var paymentUrl:String;
		
		public var oAuthInited:Boolean = false;
		public var leadSourcesInited:Boolean = false;
		public var workflowsInited:Boolean = false;
		
		public function get applicationUrl():String
		{
			return "http://workplace.intuit.com/app/" + (currentDb == null ? "" : currentDb);
		}
		
		private static var _instance:MainModel;
		public static function getInstance():MainModel
		{
			if (_instance == null)
				_instance = new MainModel(new Private());
			
			return _instance;
		}
		
		public function MainModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}
