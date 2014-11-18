package com.llsvc.registry
{
	import com.llsvc.domain.User;
	import com.llsvc.services.UserService;
	
	import mx.controls.Alert;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	
	import src.com.llsvc.domain.vo.userVO;
	
	public class UserRegistry
	{
		private static var _instance:UserService;
		
		private var localCash:Object;
		
		[Bindable]
		public var serviceIsBusy:Boolean = false; 
		
		public function UserService(target:IEventDispatcher=null)
		{
			if (_instance != null)
				throw new Error("Singleton !");
				
			super(target);
			
			localCash = new Object();
		}
		
		public static function get instance():UserService 
		{
			if (_instance == null)
				_instance = new UserService();
			
			return _instance;
		}
		
		public function getUser(userId:int):User 
		{
			var result:User = localCash[userId] as User;
			if (result != null && result.isLoaded && !result.isLoading)
				return result;
			
			UserService.instance.getUser(userId).addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						storeInLocalCash(event.result as userVO);
					}, 
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}
				)
			)
		}
		
		public function saveUser(user:User):void 
		{
			var result:User = localCash[userId] as User;
			if (result != null && result.isLoaded && !result.isLoading)
				return result;
			
			UserService.instance.getUser(userId).addResponder(
				new Responder(
					function(event:ResultEvent):void 
					{
						storeInLocalCash(event.result as userVO);
					}, 
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}
				)
			)
		}
		
		private function storeInLocalCash(u:userVO):void 
		{
			var stored:User = localCash[u.userid] as User;
			if (stored == null) 
			{
				stored = new User();
				localCash[u.userid] = stored;
			}
			stored.updateFields(u);
		}
		
	}
}