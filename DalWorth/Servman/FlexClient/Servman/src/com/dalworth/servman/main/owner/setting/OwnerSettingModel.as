package com.dalworth.servman.main.owner.setting
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class OwnerSettingModel
	{
		public var owners:ArrayCollection;
		public var salesReps:ArrayCollection;
		public var businessPartners:ArrayCollection;
		public var customerServiceReps:ArrayCollection;
		
		public var leadTypes:ArrayCollection;
		
		public static const MODEL_INITED:String = "OwnerSettingModelInited";
		
		public var settingMainMenuContent:ArrayCollection;
		
		private static var _instance:OwnerSettingModel;
		public static function getInstance():OwnerSettingModel
		{
			if (_instance == null)
				_instance = new OwnerSettingModel(new Private());
			
			return _instance;
		}
		
		public function OwnerSettingModel(accessPrivate:Private) 
		{
			initOwnerSettingMenu();
		}
		
		private function initOwnerSettingMenu():void 
		{
			if (!settingMainMenuContent)
				settingMainMenuContent = new ArrayCollection();
			
			settingMainMenuContent.removeAll();
			settingMainMenuContent.addItem({label:"Business partners"});
			settingMainMenuContent.addItem({label:"Customers"});
			settingMainMenuContent.addItem({label:"Project Types"});
		}
	}
}	

class Private {}
