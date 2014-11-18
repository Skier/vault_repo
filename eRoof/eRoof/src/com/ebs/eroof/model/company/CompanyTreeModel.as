package com.ebs.eroof.model.company
{
   	import com.adobe.cairngorm.model.IModelLocator;
   	import com.ebs.eroof.model.wrapper.Company;
	
	[Bindable]
	public class CompanyTreeModel implements IModelLocator
	{
		public var company:Company = new Company();
		public var isLoaded:Boolean = false;
		public var isLoading:Boolean = false;
		
		private static var _instance:CompanyTreeModel;
        public static function getInstance():CompanyTreeModel
        {
			if (_instance == null)
            	_instance = new CompanyTreeModel(new Private());
			
			return _instance;
		}
         
		public function CompanyTreeModel(accessPrivate:Private) {}
	}
}

class Private {}
