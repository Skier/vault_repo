package com.llsvc.domain
{
	import com.llsvc.domain.vo.addressVO;

	[Bindable]
	public class Address extends addressVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var state:State;
		
		public function Address()
		{
			super();
		}
		
		public function updateFields(value:addressVO):void 
		{
			if (value == null)
				value = new addressVO(); 
			
			this.addressid = value.addressid;
			this.address1 = value.address1;
			this.address2 = value.address2;
			this.city = value.city;
			this.stateid = value.stateid;
			this.zip = value.zip;
		}
		
		public function toVO():addressVO 
		{
			var result:addressVO = new addressVO();
			
			result.addressid = this.addressid;
			result.address1 = this.address1;
			result.address2 = this.address2;
			result.city = this.city;
			result.stateid = this.stateid;
			result.zip = this.zip;
			
			return result;
		}
		
	}
}