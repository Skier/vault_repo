package com.llsvc.domain
{
	import com.llsvc.domain.vo.projectVO;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Project extends projectVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var client:Client;
		public var expenceItems:ArrayCollection;
		
		public function Project()
		{
			super();
			
			expenceItems = new ArrayCollection();
		}
		
		public function updateFields(value:projectVO):void 
		{
			if (value == null)
				value = new Project(); 
			
			this.projectid = value.projectid;
			this.clientid = value.clientid;
			this.projectname = value.projectname;
			this.afe = value.afe;
			this.description = value.description;
			this.status = value.status;
		}
		
		public function toVO():projectVO 
		{
			var result:projectVO = new projectVO();

			result.projectid = this.projectid;
			result.clientid = this.clientid;
			result.projectname = this.projectname;
			result.afe = this.afe;
			result.description = this.description;
			result.status = this.status;
			
			return result;
		}
		
	}
}