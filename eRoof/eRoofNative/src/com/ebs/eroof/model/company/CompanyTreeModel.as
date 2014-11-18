package com.ebs.eroof.model.company
{
   	import com.adobe.cairngorm.model.IModelLocator;
	
	[Bindable]
	public class CompanyTreeModel implements IModelLocator
	{
		public var company:Company = new Company();
		
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
