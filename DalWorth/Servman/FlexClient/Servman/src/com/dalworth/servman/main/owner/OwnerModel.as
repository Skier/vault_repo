package com.dalworth.servman.main.owner
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class OwnerModel
	{
		public var ownerMenuContent:ArrayCollection;
		
		private static var _instance:OwnerModel;
		public static function getInstance():OwnerModel
		{
			if (_instance == null)
				_instance = new OwnerModel(new Private());
			
			return _instance;
		}
		
		public function OwnerModel(accessPrivate:Private) 
		{
			initOwnerMenu();
		}
		
		private function initOwnerMenu():void 
		{
			if (!ownerMenuContent)
				ownerMenuContent = new ArrayCollection();
			
			ownerMenuContent.removeAll();
			ownerMenuContent.addItem({label:"Home"});
			ownerMenuContent.addItem({label:"Leads"});
			ownerMenuContent.addItem({label:"Projects"});
			ownerMenuContent.addItem({label:"Reports"});
			ownerMenuContent.addItem({label:"Setting"});
		}
	}
}	

class Private {}
