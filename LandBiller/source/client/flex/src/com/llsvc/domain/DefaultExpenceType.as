package com.llsvc.domain
{
	import com.llsvc.domain.vo.defaultexpencetypeVO;

	[Bindable]
	public class DefaultExpenceType extends defaultexpencetypeVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public function DefaultExpenceType()
		{
			super();
		}
		
		public function updateFields(value:defaultexpencetypeVO):void 
		{
			if (value == null)
				value = new defaultexpencetypeVO(); 
			
			this.defaultexpencetypeid = value.defaultexpencetypeid;
			this.itemname = value.itemname;
			this.defaultrate = value.defaultrate;
		}
		
		public function toVO():defaultexpencetypeVO 
		{
			var result:defaultexpencetypeVO = new defaultexpencetypeVO();
			
			result.defaultexpencetypeid = this.defaultexpencetypeid;
			result.itemname = this.itemname;
			result.defaultrate = this.defaultrate;
			
			return result;
		}
		
	}
}