package com.llsvc.domain
{
	import com.llsvc.domain.vo.companyVO;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Company extends companyVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var user:User;
		public var clients:ArrayCollection;
		
		public function Company()
		{
			super();
			
			clients = new ArrayCollection();
		}
		
		public function updateFields(value:companyVO):void 
		{
			if (value == null)
				value = new companyVO(); 
			
			this.companyid = value.companyid;
			this.userid = value.userid;
			this.name = value.name;
			this.description = value.description;
		}
		
		public function toVO():companyVO 
		{
			var result:companyVO = new companyVO();
			
			result.companyid = this.companyid;
			result.userid = this.userid;
			result.name = this.name;
			result.description = this.description;
			
			return result;
		}
		
	}
}