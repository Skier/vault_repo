package com.dalworth.servman.main.owner.project
{
	import mx.collections.ArrayCollection;

	[Bindable]
	public class OwnerProjectsModel
	{
		public static const MODEL_INITED:String = "OwnerProjectsModelInited";
		
		public var businessPartners:ArrayCollection;
		public var projects:ArrayCollection;
		
		private static var _instance:OwnerProjectsModel;
		public static function getInstance():OwnerProjectsModel
		{
			if (_instance == null)
				_instance = new OwnerProjectsModel(new Private());
			
			return _instance;
		}
		
		public function OwnerProjectsModel(accessPrivate:Private) 
		{
			projects = new ArrayCollection();
		}
	}
}	

class Private {}
