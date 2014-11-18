package com.ebs.eroof.control.section
{
	import com.ebs.eroof.dto.Sections_DTO;
	import com.quickbase.idn.model.QuickBaseDTOArrayCollection;
	
	import flash.events.EventDispatcher;
	import flash.events.IEventDispatcher;

	public class FacilityServiceController extends EventDispatcher
	{
		private static var _instance:FacilityServiceController;
		public static function getInstance():FacilityServiceController
		{
			if (_instance == null)
				_instance = new FacilityServiceController(new Private());
		
			return _instance;
		}
	
		public function FacilityServiceController(accessPrivate:Private, target:IEventDispatcher=null) 
		{
			super(target);
		}

		public function getAllNames():QuickBaseDTOArrayCollection
		{
			var result:QuickBaseDTOArrayCollection;
			
			result = new QuickBaseDTOArrayCollection(Sections_DTO, true, "", "RoofName", "RoofName");
			result.initData();
			
			return result;
		}

		public function getSectionPage(pageNo:int, pageSize:int):QuickBaseDTOArrayCollection
		{
			var result:QuickBaseDTOArrayCollection;
			
			var skip:int = (pageNo - 1) * pageSize;
			
			result = new QuickBaseDTOArrayCollection(Sections_DTO, true, "", "", "rid", "sortorder-DA.num-" + pageSize.toString() + ".skp-" + skip.toString());
			result.initData();
			
			return result;
		}

		public function getAll():QuickBaseDTOArrayCollection 
		{
			var result:QuickBaseDTOArrayCollection;
			
			result = new QuickBaseDTOArrayCollection(Sections_DTO);
			result.initData();
			
			return result;
		}

   }
}

class Private {}
