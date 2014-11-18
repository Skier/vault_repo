package com.llsvc.admin
{
	import com.llsvc.domain.User;
	
	import mx.collections.ArrayCollection;
	
	public class LocalCash
	{
		private static var instance:LocalCash;
		
		public static function getInstance():LocalCash 
		{
			if (instance == null)
				instance = new LocalCash();
			
			return instance;
		}
		
		public function LocalCash()
		{
			if (instance != null)
				throw new Error("Singleton!");
		}
		
		private var clientList:ArrayCollection = new ArrayCollection();
		public function storeClients(value:ArrayCollection):void 
		{
			clientList.removeAll();
			for each (var obj:Object in value) 
				clientList.addItem(obj);
		}
		public function getClients():ArrayCollection 
		{
			return clientList;
		}

	}
}