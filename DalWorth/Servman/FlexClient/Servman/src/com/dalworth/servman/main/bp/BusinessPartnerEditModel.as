package com.dalworth.servman.main.bp
{
	import com.dalworth.servman.domain.BusinessPartner;
	
	[Bindable]
	public class BusinessPartnerEditModel
	{
		public var businessPartner:BusinessPartner;
		
		public var isBusy:Boolean;

		private static var _instance:BusinessPartnerEditModel;
		public static function getInstance():BusinessPartnerEditModel
		{
			if (_instance == null)
				_instance = new BusinessPartnerEditModel(new Private());
			
			return _instance;
		}
		
		public function BusinessPartnerEditModel(accessPrivate:Private) 
		{
		}
	}
}

class Private {}