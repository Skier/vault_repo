package com.dalworth.leadCentral.owner.home
{
	import com.dalworth.leadCentral.domain.ServmanCustomer;
	
	import mx.binding.utils.ChangeWatcher;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class OwnerHomeModel
	{
		public var isBusy:Boolean = false;
		
        public var startDate:Date;
        public var endDate:Date;
        
        public var leads:ArrayCollection;
        
        public var isWelcomeShow:Boolean = false;

        public var customer:ServmanCustomer;

		private static var _instance:OwnerHomeModel;
		public static function getInstance():OwnerHomeModel
		{
			if (_instance == null)
				_instance = new OwnerHomeModel(new Private());
			
			return _instance;
		}
		
		public function OwnerHomeModel(accessPrivate:Private) 
		{
			leads = new ArrayCollection();
		}
		
	}
}	

class Private {}
