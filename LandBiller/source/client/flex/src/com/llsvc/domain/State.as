package com.llsvc.domain
{
	import com.llsvc.domain.vo.stateVO;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class State extends stateVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var counties:ArrayCollection;
		
		public function State()
		{
			super();
			
			counties = new ArrayCollection();
		}
		
		public function updateFields(value:stateVO):void 
		{
			if (value == null)
				value = new stateVO(); 
			
			this.stateid = value.stateid;
			this.name = value.name;
			this.stateabbr = value.stateabbr;
			this.statefips = value.statefips;
		}
		
		public function toVO():stateVO 
		{
			var result:stateVO = new stateVO();
			
			result.stateid = this.stateid;
			result.name = this.name;
			result.stateabbr = this.stateabbr;
			result.statefips = this.statefips;

			return result;
		}
		
	}
}