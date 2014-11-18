package com.llsvc.domain
{
	import com.llsvc.domain.vo.countyVO;

	[Bindable]
	public class County extends countyVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var state:State;
		
		public function County()
		{
			super();
		}
		
		public function updateFields(value:countyVO):void 
		{
			if (value == null)
				value = new countyVO(); 
			
			this.countyid = value.countyid;
			this.stateid = value.stateid;
			this.name = value.name;
			this.countyfips = value.countyfips;
			this.statefips = value.statefips;
			this.fullfips = value.fullfips;
		}
		
		public function toVO():countyVO 
		{
			var result:countyVO = new countyVO();
			
			result.countyid = this.countyid;
			result.stateid = this.stateid;
			result.name = this.name;
			result.countyfips = this.countyfips;
			result.statefips = this.statefips;
			result.fullfips = this.fullfips;
			
			return result;
		
		}
		
	}
}